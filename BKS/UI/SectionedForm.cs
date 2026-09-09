namespace BKS;

/// <summary>Geniş ekranda bölüm listesi, dar ekranda seçim kutusu gösteren ortak kayıt kartı.</summary>
internal sealed class SectionedForm : Panel
{
    private sealed record Section(string Title, string Description, Control Content);
    private readonly List<Section> sections = new();
    private readonly ListBox navigation = new()
    {
        IntegralHeight = false,
        BorderStyle = BorderStyle.None,
        DrawMode = DrawMode.OwnerDrawFixed,
        BackColor = RibbonPalette.Surface,
        Font = new Font("Segoe UI", 10F)
    };
    private readonly ComboBox compactNavigation = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly Label heading = new()
    {
        Font = new Font("Segoe UI", 13F, FontStyle.Bold),
        ForeColor = RibbonPalette.Text,
        AutoEllipsis = true
    };
    private readonly Label description = new()
    {
        Font = new Font("Segoe UI", 9.5F),
        ForeColor = RibbonPalette.CaptionText,
        AutoEllipsis = true
    };
    private readonly Panel body = new() { BackColor = Color.White };
    private bool selecting;

    public IEnumerable<Control> Contents => sections.Select(section => section.Content);
    public IEnumerable<string> Titles => sections.Select(section => section.Title);
    public string CurrentTitle { get; private set; } = string.Empty;
    public event EventHandler? SectionChanged;

    public SectionedForm()
    {
        Dock = DockStyle.Fill;
        BackColor = Color.White;
        Controls.AddRange(new Control[] { body, navigation, compactNavigation, heading, description });
        navigation.DrawItem += DrawSection;
        navigation.SelectedIndexChanged += (_, _) => SelectIndex(navigation.SelectedIndex);
        compactNavigation.SelectedIndexChanged += (_, _) => SelectIndex(compactNavigation.SelectedIndex);
    }

    public SectionedForm AddSection(string title, string note, Control content)
    {
        sections.Add(new Section(title, note, content));
        navigation.Items.Add(title);
        compactNavigation.Items.Add(title);
        if (sections.Count == 1) SelectSection(title);
        return this;
    }

    public void SelectSection(string title) => SelectIndex(sections.FindIndex(section => section.Title == title));

    private void SelectIndex(int index)
    {
        if (selecting || index < 0 || index >= sections.Count) return;
        selecting = true;
        try
        {
            var section = sections[index];
            CurrentTitle = section.Title;
            heading.Text = section.Title;
            description.Text = section.Description;
            navigation.SelectedIndex = compactNavigation.SelectedIndex = index;
            body.SuspendLayout();
            body.Controls.Clear();
            section.Content.Dock = DockStyle.Fill;
            body.Controls.Add(section.Content);
            body.ResumeLayout(true);
            SectionChanged?.Invoke(this, EventArgs.Empty);
        }
        finally { selecting = false; }
    }

    public void Reveal(Control input)
    {
        foreach (var section in sections)
        {
            if (section.Content != input && !section.Content.Contains(input)) continue;
            SelectSection(section.Title);
            Screens.RevealAndFocus(input);
            return;
        }
    }

    private void DrawSection(object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0 || e.Index >= sections.Count) return;
        bool selected = (e.State & DrawItemState.Selected) != 0;
        using var fill = new SolidBrush(selected ? RibbonPalette.SelectedRow : RibbonPalette.Surface);
        e.Graphics.FillRectangle(fill, e.Bounds);
        if (selected)
        {
            using var accent = new SolidBrush(RibbonPalette.Accent);
            e.Graphics.FillRectangle(accent, e.Bounds.Left, e.Bounds.Top, 3 * DeviceDpi / 96F, e.Bounds.Height);
        }
        TextRenderer.DrawText(e.Graphics, sections[e.Index].Title, navigation.Font,
            Rectangle.Inflate(e.Bounds, -12, 0), RibbonPalette.Text,
            TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        e.DrawFocusRectangle();
    }

    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e);
        if (body is null || navigation is null) return;
        int Px(int value) => (int)Math.Ceiling(value * DeviceDpi / 96F);
        bool wide = ClientSize.Width >= Px(760);
        int sideWidth = wide ? Px(204) : 0;
        int top = wide ? 0 : Px(42);
        navigation.Visible = wide;
        compactNavigation.Visible = !wide;
        navigation.ItemHeight = Px(42);
        navigation.SetBounds(0, 0, sideWidth, Height);
        compactNavigation.SetBounds(Px(12), Px(8), Math.Max(0, Width - Px(24)), Px(28));
        int textLeft = sideWidth + Px(16);
        int textWidth = Math.Max(0, Width - textLeft - Px(16));
        heading.SetBounds(textLeft, top + Px(12), textWidth, Px(30));
        description.SetBounds(textLeft, top + Px(43), textWidth, Px(25));
        body.SetBounds(sideWidth + Px(1), top + Px(78), Math.Max(0, Width - sideWidth - Px(1)),
            Math.Max(0, Height - top - Px(78)));
    }

    protected override void OnDpiChangedAfterParent(EventArgs e) { base.OnDpiChangedAfterParent(e); PerformLayout(); }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            foreach (var section in sections)
                if (!section.Content.IsDisposed) section.Content.Dispose();
        base.Dispose(disposing);
    }
}
