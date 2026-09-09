namespace BKS;

internal sealed class RibbonTabButton : Button
{
    private bool selected;
    private bool hovered;
    public bool Selected
    {
        get => selected;
        set
        {
            if (selected == value) return;
            selected = value;
            var previous = Font;
            Font = new Font(value ? "Segoe UI Semibold" : "Segoe UI", 9.5F);
            previous.Dispose();
            Invalidate();
        }
    }
    public RibbonTabButton()
    {
        Font = new Font("Segoe UI", 9.5F);
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        Margin = Padding.Empty;
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        MouseEnter += (_, _) =>
        {
            hovered = true;
            Invalidate();
        };
        MouseLeave += (_, _) =>
        {
            hovered = false;
            Invalidate();
        };
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.Clear(selected ? RibbonPalette.Group: hovered ? RibbonPalette.HoverTop: RibbonPalette.TabStrip);
        if (selected)
        {
            using var pen = new Pen(RibbonPalette.Accent, Math.Max(2, DeviceDpi / 48F));
            e.Graphics.DrawLine(pen, 0, Height - 2, Width, Height - 2);
        }
        TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle,
        selected ? RibbonPalette.Accent: RibbonPalette.Text,
        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);
        if (Focused && ShowFocusCues)
        ControlPaint.DrawFocusRectangle(e.Graphics, Rectangle.Inflate(ClientRectangle, - 3, - 3));
    }
}
