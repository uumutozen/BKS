namespace BKS;
internal sealed class SectionedForm : Panel
{
    private readonly List<(string Title, string Description, Control Content)> sections = new();
    private readonly Label heading = new()
    {
        Font = new Font("Segoe UI", 13F, FontStyle.Bold),
        ForeColor = RibbonPalette.Text
    };
    private readonly Label description = new()
    {
        ForeColor = ModernWinForms.Muted,
        Font = new Font("Segoe UI", 9.5F)
    };
    private readonly Panel body = new()
    {
        BackColor = Color.White
    };
    public IEnumerable<Control> Contents => sections.Select(s => s.Content);
    public IEnumerable<string> Titles => sections.Select(s => s.Title);
    public string CurrentTitle
    {
        get;
        private set;
    }
    = "";
    public event EventHandler? SectionChanged;
    public SectionedForm()
    {
        Dock = DockStyle.Fill;
        BackColor = Color.White;
        Controls.Add(heading);
        Controls.Add(description);
        Controls.Add(body);
    }
    public SectionedForm AddSection(string title, string note, Control content)
    {
        sections.Add((title, note, content));
        if (sections.Count == 1) SelectSection(title);
        return this;
    }
    public void SelectSection(string title)
    {
        var index = sections.FindIndex(s => s.Title == title);
        if (index<0) return;
        var section = sections[index];
        CurrentTitle = title;
        heading.Text = section.Title;
        description.Text = section.Description;
        body.Controls.Clear();
        section.Content.Dock = DockStyle.Fill;
        body.Controls.Add(section.Content);
        section.Content.SelectNextControl(null, true, true, true, false);
        SectionChanged?.Invoke(this, EventArgs.Empty);
    }
    public void Reveal(Control input)
    {
        foreach (var section in sections)
        for (Control? current = input; current != null; current = current.Parent)
        if (current == section.Content)
        {
            SelectSection(section.Title);
            input.Focus();
            return;
        }
    }
    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e);
        if (body == null || heading == null) return;
        int Px(int n) => (int) Math.Ceiling(n * DeviceDpi / 96F);
        int width = Math.Max(0, Width - Px(32));
        heading.SetBounds(Px(16), Px(12), width, Px(30));
        description.SetBounds(Px(16), Px(44), width, Px(30));
        body.SetBounds(0, Px(78), Width, Math.Max(0, Height - Px(78)));
    }
    protected override void Dispose(bool disposing)
    {
        if (disposing) foreach (var section in sections) if (!section.Content.IsDisposed) section.Content.Dispose();
        base.Dispose(disposing);
    }
}
