namespace BKS;

public partial class Form2
{
    private readonly Dictionary<string, EditSession> _pageEdits = new();
    private BksRibbon _ribbon = null!;
    private DocumentWorkspace _documents = null!;
    private ToolStripStatusLabel _ribbonStatus = null!;
    private string? _selectedRibbonPageKey;
    private string? _activeDocumentKey;

    private void BuildRibbonWorkspace()
    {
        InitializeModuleBehavior();
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
        _ribbon.PageSelected += RibbonPageSelected;
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
        status.Items.Add(new ToolStripStatusLabel("BKS  •  v1.4.0"));
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

    // Ribbon selection and document activation are deliberately independent.
    private void RibbonPageSelected(string key)
    {
        _selectedRibbonPageKey = key;
        _ribbon.RefreshCommands();
    }

    private void ActiveDocumentChanged(string key)
    {
        _activeDocumentKey = key;
        SetRibbonStatus(tabControl.SelectedTab?.Text ?? "Hazır");
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
        foreach (Control old in flpDashboardCards.Controls.Cast<Control>().ToArray()) old.Dispose();
        lblHomeSubtitle.Text = _allowedModules.Count == 0
            ? "Erişim yetkileri doğrulandığında bölümleriniz burada görünür."
            : "Çalışmak istediğiniz bölümü seçin. Açtığınız ekranlar belge sekmelerinde kalır.";
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
            flpDashboardCards.Controls.Add(link);
        }
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
        if (keyData == (Keys.Control | Keys.F) && ListSurface.FocusSearchIn(_documents)) return true;
        if (_ribbon.TryShortcut(keyData)) return true;
        return base.ProcessCmdKey(ref msg, keyData);
    }
}
