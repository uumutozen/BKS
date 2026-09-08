namespace BKS;
/// <summary>Owns document tabs and embedded forms, including single-instance activation.</summary>
internal sealed class DocumentWorkspace : Panel
{
    private sealed record Document(TabPage Page, Form? Form, string? Permission, bool KeepAlive);
    private readonly DocumentRegistry<Document> documents = new();
    private readonly TabControl tabs;
    public Func<string, bool>? CanClosePage
    {
        get;
        set;
    }
    public event Action<string, BksRibbon?>? ActiveDocumentChanged;
    public string? ActiveKey => documents.Entries.FirstOrDefault(p => p.Value.Page == tabs.SelectedTab).Key;
    public DocumentWorkspace(TabControl tabs)
    {
        this.tabs = tabs;
        Dock = DockStyle.Fill;
        BackColor = RibbonPalette.Workspace;
        tabs.Parent?.Controls.Remove(tabs);
        tabs.TabPages.Clear();
        tabs.Dock = DockStyle.Fill;
        tabs.Appearance = TabAppearance.Normal;
        tabs.DrawMode = TabDrawMode.OwnerDrawFixed;
        tabs.SizeMode = TabSizeMode.Fixed;
        tabs.ItemSize = new Size(180, 30);
        tabs.Padding = new Point(10, 4);
        tabs.DrawItem += DrawTab;
        tabs.MouseDown += TabMouseDown;
        tabs.SelectedIndexChanged += (_, _) => NotifyActive();
        Controls.Add(tabs);
        ResizeTabs();
    }
    public void OpenPage(string key, string title, TabPage page, string? permission = null)
    {
        var document = documents.GetOrCreate(key, () => new Document(page, null, permission, true));
        document.Page.Text = title;
        Activate(document);
    }
    public void OpenDocument(string key, string title, Func<Form> factory, string? permission = null)
    {
        var document = documents.GetOrCreate(key, () =>
        {
            var form = factory();
            var page = new TabPage(title)
            {
                Name = key,
                Padding = Padding.Empty,
                BackColor = Color.White
            };
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.MinimumSize = Size.Empty;
            form.Dock = DockStyle.Fill;
            var workspace = form.Controls.OfType<RibbonWorkspace>().FirstOrDefault();
            workspace?.Embed();
            page.Controls.Add(form);
            form.FormClosed += (_, _) => RemoveClosed(key, page);
            return new Document(page, form, permission, false);
        });
        Activate(document);
        document.Form?.Show();
        NotifyActive();
    }
    public bool Activate(string key)
    {
        if (!documents.TryGet(key, out var document) || document == null) return false;
        Activate(document);
        return true;
    }
    private void Activate(Document document)
    {
        if (!tabs.TabPages.Contains(document.Page)) tabs.TabPages.Add(document.Page);
        tabs.SelectedTab = document.Page;
        document.Page.Focus();
        NotifyActive();
    }
    public void ApplyAccess(ISet<string> permissions)
    {
        foreach (var entry in documents.Entries)
        {
            var document = entry.Value;
            bool allowed = document.Permission == null || permissions.Contains(document.Permission);
            document.Page.Enabled = allowed;
            // Detach inaccessible documents without discarding unsaved edits during a permission refresh.
            if (!allowed) tabs.TabPages.Remove(document.Page);
        }
    }
    public bool CloseDocument(string key)
    {
        if (key == "home" || !documents.TryGet(key, out var document) || document == null) return true;
        if (document.Form != null)
        {
            document.Form.Close();
            return document.Form.IsDisposed;
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
    private void RemoveClosed(string key, TabPage page)
    {
        documents.Remove(key);
        tabs.TabPages.Remove(page);
        page.Dispose();
        NotifyActive();
    }
    private void NotifyActive()
    {
        var entry = documents.Entries.FirstOrDefault(p => p.Value.Page == tabs.SelectedTab);
        if (entry.Value == null) return;
        var ribbon = entry.Value.Form?.Controls.OfType<RibbonWorkspace>().FirstOrDefault()?.Ribbon;
        ActiveDocumentChanged?.Invoke(entry.Key, ribbon);
    }
    private void DrawTab(object? sender, DrawItemEventArgs e)
    {
        var page = tabs.TabPages[e.Index];
        bool active = tabs.SelectedIndex == e.Index;
        using var fill = new SolidBrush(active ? Color.White: RibbonPalette.DocumentTab);
        e.Graphics.FillRectangle(fill, e.Bounds);
        var text = Rectangle.Inflate(e.Bounds, - 8, 0);
        text.Width -= 22;
        TextRenderer.DrawText(e.Graphics, page.Text, tabs.Font, text, RibbonPalette.Text,
        TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        if (page.Name != "home")
        TextRenderer.DrawText(e.Graphics, "×", tabs.Font, CloseBounds(e.Index), RibbonPalette.CaptionText,
        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        using var border = new Pen(active ? RibbonPalette.Accent: RibbonPalette.Border);
        e.Graphics.DrawLine(border, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
    }
    private Rectangle CloseBounds(int index)
    {
        var rect = tabs.GetTabRect(index);
        int width = (int) Math.Ceiling(24 * DeviceDpi / 96F);
        return new Rectangle(rect.Right - width, rect.Top, width, rect.Height);
    }
    private void TabMouseDown(object? sender, MouseEventArgs e)
    {
        for (int i = 0; i<tabs.TabCount; i++)
        {
            if (!tabs.GetTabRect(i).Contains(e.Location)) continue;
            var key = documents.Entries.First(p => p.Value.Page == tabs.TabPages[i]).Key;
            if (e.Button == MouseButtons.Middle || (e.Button == MouseButtons.Left && CloseBounds(i).Contains(e.Location)))
            CloseDocument(key);
            else if (e.Button == MouseButtons.Right) ShowTabMenu(key, i, e.Location);
            break;
        }
    }
    private void ShowTabMenu(string key, int index, Point point)
    {
        var menu = new ContextMenuStrip();
        void Add(string caption, IEnumerable<string> keys)
        {
            var snapshot = keys.ToArray();
            menu.Items.Add(caption, null, (_, _) =>
            {
                foreach (var item in snapshot) if (!CloseDocument(item)) break;
            });
        }
        var open = tabs.TabPages.Cast<TabPage>()
        .Select(page => documents.Entries.First(p => p.Value.Page == page).Key).ToArray();
        Add("Kapat", new[]
        {
            key
        });
        Add("Diğerlerini kapat", open.Where(k => k != key));
        Add("Sağdakileri kapat", open.Skip(index + 1));
        Add("Tümünü kapat", open);
        menu.Closed += (_, _) => menu.Dispose();
        menu.Show(tabs, point);
    }
    private void ResizeTabs() => tabs.ItemSize = new Size((int)(180 * DeviceDpi / 96F), (int)(30 * DeviceDpi / 96F));
    protected override void OnDpiChangedAfterParent(EventArgs e)
    {
        base.OnDpiChangedAfterParent(e);
        ResizeTabs();
    }
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        foreach (var entry in documents.Entries)
        if (!entry.Value.KeepAlive && !entry.Value.Page.IsDisposed) entry.Value.Page.Dispose();
        base.Dispose(disposing);
    }
}
