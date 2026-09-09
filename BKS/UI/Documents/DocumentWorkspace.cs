namespace BKS;

/// <summary>Tek örnek açma, yetki ve kapanış akışını yönetir. Sekme çizimi ayrı sınıftadır.</summary>
internal sealed class DocumentWorkspace : Panel
{
    private readonly DocumentRegistry<DocumentEntry> documents = new();
    private readonly TabControl tabs;
    private readonly DocumentTabPresenter tabPresenter;

    public Func<string, bool>? CanClosePage { get; set; }
    public event Action<string>? ActiveDocumentChanged;
    public string? ActiveKey => FindKey(tabs.SelectedTab);

    public DocumentWorkspace(TabControl tabs)
    {
        this.tabs = tabs;
        Dock = DockStyle.Fill;
        BackColor = RibbonPalette.Workspace;
        tabs.Parent?.Controls.Remove(tabs);
        tabs.TabPages.Clear();
        tabs.Dock = DockStyle.Fill;
        tabPresenter = new DocumentTabPresenter(tabs);
        tabPresenter.CloseRequested += CloseRequested;
        tabs.SelectedIndexChanged += (_, _) => NotifyActive();
        Controls.Add(tabs);
    }

    public void OpenPage(string key, string title, TabPage page, string? permission = null)
    {
        var document = documents.GetOrCreate(key, () => new DocumentEntry(page, null, permission, true));
        document.Page.Text = document.Page.ToolTipText = title;
        Activate(document);
    }

    public TForm OpenDocument<TForm>(string key, string title, Func<TForm> factory, string? permission = null)
        where TForm : Form
    {
        if (documents.TryGet(key, out var stale) && stale?.Form?.IsDisposed == true)
            RemoveClosed(key, stale);

        bool created = false;
        var document = documents.GetOrCreate(key, () =>
        {
            var entry = CreateDocument(key, title, factory, permission);
            created = true;
            return entry;
        });
        if (document.Form is not TForm form)
            throw new InvalidOperationException("Belge anahtarı farklı bir ekran türü için kullanılmış: " + key);

        try
        {
            Activate(document);
            form.Show();
        }
        catch
        {
            if (created)
            {
                RemoveClosed(key, document);
                form.Dispose();
            }
            throw;
        }
        if (!form.IsDisposed)
        {
            Screens.FitToScreen(form);
            form.BringToFront();
            form.SelectNextControl(null, true, true, true, false);
            NotifyActive();
        }
        return form;
    }

    private DocumentEntry CreateDocument<TForm>(string key, string title, Func<TForm> factory, string? permission)
        where TForm : Form
    {
        var form = factory() ?? throw new InvalidOperationException("Belge oluşturucu boş form döndürdü.");
        var page = new TabPage(title)
        {
            Name = key, ToolTipText = title, Padding = Padding.Empty, BackColor = Color.White
        };
        try
        {
            DocumentFormHost.Attach(form, page);
            var document = new DocumentEntry(page, form, permission, false);
            form.FormClosed += (_, _) => RemoveClosed(key, document);
            return document;
        }
        catch
        {
            page.Dispose();
            form.Dispose();
            throw;
        }
    }

    public bool Activate(string key)
    {
        if (!documents.TryGet(key, out var document) || document is null) return false;
        Activate(document);
        return true;
    }

    private void Activate(DocumentEntry document)
    {
        if (!tabs.TabPages.Contains(document.Page)) tabs.TabPages.Add(document.Page);
        tabs.SelectedTab = document.Page;
        document.Page.Focus();
        NotifyActive();
    }

    public void ApplyAccess(ISet<string> permissions)
    {
        foreach (var document in documents.Entries.Select(entry => entry.Value))
        {
            bool allowed = document.Permission is null || permissions.Contains(document.Permission);
            document.Page.Enabled = allowed;
            if (!allowed) tabs.TabPages.Remove(document.Page);
        }
    }

    public bool CloseDocument(string key)
    {
        if (key == "home" || !documents.TryGet(key, out var document) || document is null) return true;
        if (document.Form is { } form)
        {
            if (document.Page.Enabled) Activate(document);
            form.Close();
            return form.IsDisposed;
        }
        if (CanClosePage?.Invoke(key) == false) return false;
        tabs.TabPages.Remove(document.Page);
        NotifyActive();
        return true;
    }

    public bool CloseAll()
    {
        foreach (var entry in documents.Entries)
            if (!CloseDocument(entry.Key)) return false;
        return true;
    }

    private void RemoveClosed(string key, DocumentEntry document)
    {
        documents.Remove(key);
        tabs.TabPages.Remove(document.Page);
        if (document.Form is not null) document.Page.Controls.Remove(document.Form);
        document.Page.Dispose();
        NotifyActive();
    }

    private string? FindKey(TabPage? page) => documents.Entries.FirstOrDefault(entry => entry.Value.Page == page).Key;

    private void NotifyActive()
    {
        if (ActiveKey is not { } key || !documents.TryGet(key, out var document) || document is null) return;
        ActiveDocumentChanged?.Invoke(key);
    }

    private void CloseRequested(TabPage page, DocumentCloseMode mode)
    {
        var open = tabs.TabPages.Cast<TabPage>().ToArray();
        int index = Array.IndexOf(open, page);
        IEnumerable<TabPage> targets = mode switch
        {
            DocumentCloseMode.Current => new[] { page },
            DocumentCloseMode.Others => open.Where(candidate => candidate != page),
            DocumentCloseMode.ToRight => open.Skip(index + 1),
            _ => open.AsEnumerable()
        };
        foreach (var target in targets.ToArray())
            if (FindKey(target) is { } key && !CloseDocument(key)) break;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            foreach (var document in documents.Entries.Select(entry => entry.Value))
                if (!document.Page.IsDisposed) document.Page.Dispose();
        base.Dispose(disposing);
    }
}
