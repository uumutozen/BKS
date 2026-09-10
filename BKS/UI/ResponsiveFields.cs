namespace BKS;
/// <summary>Reflows labeled fields at the current DPI; keeps all fields reachable by scrolling.</summary>
public sealed class ResponsiveFields : Panel
{
    private readonly List<(Control Card, int Height)> fields = new();
    private bool arranging;
    public ResponsiveFields(params(string Label, Control Input)[] items)
    {
        Dock = DockStyle.Fill;
        AutoScroll = true;
        BackColor = RibbonPalette.Surface;
        Padding = new Padding(12);
        DoubleBuffered = true;
        foreach (var(label, input) in items) AddField(label, input);
    }
    public void AddField(string label, Control input)
    {
        var card = new TableLayoutPanel
        {
            BackColor = RibbonPalette.Surface,
            Padding = new Padding(2, 0, 10, 8),
            ColumnCount = 1,
            RowCount = 2,
            Margin = Padding.Empty
        };
        card.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        card.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        card.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var caption = new Label
        {
            Text = label,
            Dock = DockStyle.Fill,
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 6),
            ForeColor = ModernWinForms.Muted,
            AutoEllipsis = true
        };
        input.Parent?.Controls.Remove(input);
        // Setting Anchor after Dock silently clears Dock in WinForms.
        input.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        input.MinimumSize = Size.Empty;
        input.MaximumSize = Size.Empty;
        input.Location = Point.Empty;
        input.Dock = DockStyle.Fill;
        input.Margin = Padding.Empty;
        input.Visible = true;
        input.Font = new Font("Segoe UI", 10F);
        input.AccessibleName = label;
        if (input is TextBox tb)
        {
            tb.BorderStyle = BorderStyle.FixedSingle;
            if (!tb.Multiline) tb.PlaceholderText = label;
        }
        if (input is PictureBox pb) pb.SizeMode = PictureBoxSizeMode.Zoom;
        var height = input is PictureBox ? 182: input is TextBoxBase t && t.Multiline ? 125: input is Panel or GroupBox ? 105: 80;
        card.Controls.Add(caption, 0, 0);
        card.Controls.Add(input, 0, 1);
        fields.Add((card, height));
        Controls.Add(card);
        Arrange();
    }
    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        Arrange();
    }
    protected override void OnDpiChangedAfterParent(EventArgs e)
    {
        base.OnDpiChangedAfterParent(e);
        Arrange();
    }
    private void Arrange()
    {
        if (arranging || fields is null || ClientSize.Width <= 0) return;
        arranging = true;
        try
        {
            SuspendLayout();
            float scale = DeviceDpi / 96F;
            int available = Math.Max(120, ClientSize.Width - Padding.Horizontal - SystemInformation.VerticalScrollBarWidth);
            int columns = LayoutRules.FieldColumns(available, scale);
            int width = available / columns;
            int y = Padding.Top;
            var scroll = AutoScrollPosition;
            for (int i = 0; i<fields.Count; i += columns)
            {
                int rowHeight = (int)(fields.Skip(i).Take(columns).Max(f => f.Height) * scale);
                for (int col = 0; col<columns && i + col<fields.Count; col++)
                {
                    var field = fields[i + col];
                    field.Card.SetBounds(Padding.Left + col * width + scroll.X, y + scroll.Y, width, rowHeight);
                }
                y += rowHeight;
            }
            AutoScrollMinSize = new Size(0, y + Padding.Bottom);
        }
        finally
        {
            ResumeLayout(false);
            arranging = false;
        }
    }
}
