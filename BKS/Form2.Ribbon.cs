namespace BKS;

public partial class Form2
{
    private readonly Dictionary<string, EditSession> _pageEdits = new();
    private BksRibbon _ribbon = null!;
    private DocumentWorkspace _documents = null!;
    private ToolStripStatusLabel _ribbonStatus = null!;
    private TextBox _personnelSearch = null!;
    private readonly TabPage _homePage = new("Ana Sayfa")
    {
        Name = "home"
    };
    private readonly TabPage _classesPage = new("Sınıflar")
    {
        Name = "classes"
    };

    private void BuildRibbonWorkspace()
    {
        BuildModuleLayouts();
        _ribbon = new BksRibbon();
        _documents = new DocumentWorkspace(tabControl);
        RegisterRibbonCommands();
        _pageEdits[tabPageOgrenciOnKayit.Name] = new EditSession(tabPageOgrenciOnKayit, RunPreRegistrationAdd);
        _pageEdits["classes"] = new EditSession(_classesPage, () =>
        {
            if (sinifid == Guid.Empty) RunClassSave();
            else RunClassUpdate();
        });
        _pageEdits[tabPageSatis.Name] = new EditSession(tabPageSatis, RunPaymentEntry);
        _pageEdits[tabPageGelirGider.Name] = new EditSession(tabPageGelirGider, RunIncomeExpenseSave);
        _documents.CanClosePage = key => !_pageEdits.TryGetValue(key, out var edits) || edits.ConfirmClose();
        _ribbon.PageSelected += SelectRibbonModule;
        _ribbon.ExpandedChanged += (_, _) => UiPreferences.SaveRibbonExpanded(UserId, _ribbon.IsExpanded);
        _documents.ActiveDocumentChanged += ActiveDocumentChanged;
        var status = new StatusStrip
        {
            BackColor = RibbonPalette.TabStrip,
            SizingGrip = false
        };
        _ribbonStatus = new ToolStripStatusLabel("Yetkiler doğrulanıyor…")
        {
            Spring = true,
            TextAlign = ContentAlignment.MiddleLeft
        };
        status.Items.Add(_ribbonStatus);
        status.Items.Add(new ToolStripStatusLabel("BKS  •  v1.2.0"));
        Screens.Mount(this, _documents, _ribbon, footer: status);
        _documents.OpenPage("home", "Ana Sayfa", _homePage);
        ApplyAccess(Array.Empty<string>(), "");
        FormClosed += (_, _) =>
        {
            foreach (var page in _allModulePages.Append(_classesPage))
            if (!page.IsDisposed) page.Dispose();
            _sessionCancellation.Dispose();
        };
    }

    private void SelectRibbonModule(string key)
    {
        switch (key)
        {
            case "home":
            _documents.Activate("home");
            break;
            case "system":
            break;
            case "classes":
            OpenClasses();
            break;
            case "finance":
            SelectModuleByKey(_allowedModules.Contains(tabPageSatis.Name) ? tabPageSatis.Name: tabPageGelirGider.Name);
            break;
            case "tabPageStok":
            SelectModuleByKey(_allowedModules.Contains(tabPageStok.Name) ? tabPageStok.Name: tabPageOgrenciOnKayit.Name);
            break;
            default:
            SelectModuleByKey(key);
            break;
        }
    }

    private void ActiveDocumentChanged(string key, BksRibbon? documentRibbon)
    {
        if (documentRibbon != null)
        {
            _ribbon.SetDocumentRibbon(documentRibbon);
            SetRibbonStatus(tabControl.SelectedTab?.Text ?? "Kayıt kartı");
            return;
        }
        var ribbonKey = key switch
        {
            "tabPageOgrenciOnKayit" => "tabPageStok",
            "tabPageSatis" or "tabPageGelirGider" => "finance",
            _ => key
        };
        _ribbon.SelectPage(ribbonKey, false);
        _ribbon.RefreshCommands();
    }

    private void OpenClasses()
    {
        if (!_allowedModules.Contains(tabPageStok.Name)) return;
        _documents.OpenPage("classes", "Sınıflar", _classesPage, tabPageStok.Name);
        if (!_loadedModules.Contains(tabPageStok.Name)) _ = LoadModuleAsync(tabPageStok.Name);
    }

    private void RefreshHome()
    {
        if (_homePage.IsDisposed) return;
        foreach (Control old in _homePage.Controls.Cast<Control>().ToArray()) old.Dispose();
        var list = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(24),
            BackColor = RibbonPalette.Workspace
        };
        list.Controls.Add(new Label
        {
            Text = "Anaokulu Yönetim Sistemi",
            AutoSize = true,
            Font = new Font("Segoe UI", 16F, FontStyle.Bold),
            Margin = new Padding(0, 0, 0, 12)
        });
        list.Controls.Add(new Label
        {
            AutoSize = true,
            MaximumSize = new Size(560, 0),
            Margin = new Padding(0, 0, 0, 20),
            Text = _allowedModules.Count == 0 ? "Erişim yetkileri doğrulandığında bölümleriniz burada görünür."
            : "Çalışmak istediğiniz bölümü seçin. Açtığınız ekranlar üstteki belge sekmelerinde kalır."
        });
        foreach (var page in _allModulePages.Where(p => _allowedModules.Contains(p.Name)))
        {
            var key = page.Name;
            var link = new LinkLabel
            {
                Text = _titles[key],
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 16)
            };
            link.LinkClicked += (_, _) => SelectModuleByKey(key);
            list.Controls.Add(link);
        }
        _homePage.Controls.Add(list);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == (Keys.Control | Keys.F1))
        {
            _ribbon.ToggleExpanded();
            return true;
        }
        if (keyData == Keys.Escape && _documents.ActiveKey is
        {
        }
        key && key != "home")
        {
            _documents.CloseDocument(key);
            return true;
        }
        if (_ribbon.TryShortcut(keyData)) return true;
        return base.ProcessCmdKey(ref msg, keyData);
    }
}
