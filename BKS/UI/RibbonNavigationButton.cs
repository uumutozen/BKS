namespace BKS;

internal sealed class RibbonNavigationButton : Button
{
    private readonly RibbonCommand command;
    private Image glyph;
    private readonly ToolTip tooltip = new();
    private bool hovered;
    private bool pressed;
    public RibbonNavigationButton(RibbonCommand command)
    {
        this.command = command;
        Text = command.Text;
        AccessibleName = Text;
        Font = new Font("Segoe UI", 9F);
        glyph = CreateGlyph();
        Cursor = Cursors.Hand;
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        SetStyle(ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
        MouseEnter += (_, _) =>
        {
            hovered = true;
            Invalidate();
        };
        MouseLeave += (_, _) =>
        {
            hovered = false;
            pressed = false;
            Invalidate();
        };
        MouseDown += (_, _) =>
        {
            pressed = true;
            Invalidate();
        };
        MouseUp += (_, _) =>
        {
            pressed = false;
            Invalidate();
        };
        Click += (_, _) => command.Invoke();
        command.StateChanged += CommandStateChanged;
        var shortcut = command.Shortcut == Keys.None ? "": $" ({new KeysConverter().ConvertToString(command.Shortcut)})";
        tooltip.SetToolTip(this, (string.IsNullOrWhiteSpace(command.Tooltip) ? Text: command.Tooltip) + shortcut);
        RefreshState();
    }
    private Image CreateGlyph() => MenuGlyph.Create(command.Icon,
        Math.Max(1, (int)Math.Ceiling((command.Size == RibbonButtonSize.Large ? 30 : 18) * DeviceDpi / 96F)));

    protected override void OnDpiChangedAfterParent(EventArgs e)
    {
        base.OnDpiChangedAfterParent(e);
        glyph.Dispose();
        glyph = CreateGlyph();
        Invalidate();
    }

    private void CommandStateChanged(object? sender, EventArgs e) => RefreshState();
    public void RefreshState()
    {
        Enabled = command.IsEnabled;
        Invalidate();
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        var back = pressed || command.IsSelected?.Invoke() == true ? RibbonPalette.Pressed
        : hovered && Enabled ? RibbonPalette.HoverTop: RibbonPalette.Group;
        e.Graphics.Clear(back);
        int Px(int n) => (int) Math.Ceiling(n * DeviceDpi / 96F);
        bool large = command.Size == RibbonButtonSize.Large;
        int size = Px(large ? 30: 18);
        var icon = large ? new Rectangle((Width - size) / 2, Px(5), size, size)
        : new Rectangle(Px(4), (Height - size) / 2, size, size);
        if (Enabled) e.Graphics.DrawImage(glyph, icon);
        else
        {
            using var faded = new System.Drawing.Imaging.ImageAttributes();
            var matrix = new System.Drawing.Imaging.ColorMatrix
            {
                Matrix33 = .35F
            };
            faded.SetColorMatrix(matrix);
            e.Graphics.DrawImage(glyph, icon, 0, 0, glyph.Width, glyph.Height, GraphicsUnit.Pixel, faded);
        }
        var textBounds = large ? new Rectangle(Px(2), Px(38), Math.Max(0, Width - Px(4)), Math.Max(0, Height - Px(38)))
        : new Rectangle(Px(27), 0, Math.Max(0, Width - Px(29)), Height);
        var flags = TextFormatFlags.VerticalCenter | (large
        ? TextFormatFlags.HorizontalCenter | TextFormatFlags.WordBreak: TextFormatFlags.Left | TextFormatFlags.SingleLine);
        TextRenderer.DrawText(e.Graphics, command.IsExecuting ? "İşleniyor…": Text, Font, textBounds,
        Enabled ? RibbonPalette.Text: RibbonPalette.Disabled, flags);
        if (Focused && ShowFocusCues)
        ControlPaint.DrawFocusRectangle(e.Graphics, Rectangle.Inflate(ClientRectangle, - 2, - 2));
    }
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            command.StateChanged -= CommandStateChanged;
            tooltip.Dispose();
            glyph.Dispose();
        }
        base.Dispose(disposing);
    }
}
