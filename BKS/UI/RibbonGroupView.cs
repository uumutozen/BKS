namespace BKS;
/// <summary>Large commands occupy a column; small commands stack in groups of three.</summary>
internal sealed class RibbonGroupView : Panel
{
    private readonly List<(RibbonCommand Command, RibbonNavigationButton Button)> buttons = new();
    public string Caption { get; }
    public int PreferredWidth { get; private set; }

    public RibbonGroupView(string caption, IEnumerable<RibbonCommand> commands)
    {
        Caption = caption;
        BackColor = RibbonPalette.Group;
        Font = new Font("Segoe UI", 8.5F);
        DoubleBuffered = true;
        foreach (var command in commands.OrderBy(c => c.Order))
        {
            var button = new RibbonNavigationButton(command);
            buttons.Add((command, button));
            Controls.Add(button);
        }
        ArrangeButtons();
    }
    public void RefreshCommands()
    {
        foreach (var(_, button) in buttons) button.RefreshState();
    }
    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e);
        if (buttons != null) ArrangeButtons();
    }
    private void ArrangeButtons()
    {
        int Px(int n) => (int) Math.Ceiling(n * DeviceDpi / 96F);
        int x = Px(8);
        int index = 0;
        while (index<buttons.Count)
        {
            var item = buttons[index];
            if (item.Command.Size == RibbonButtonSize.Large)
            {
                item.Button.SetBounds(x, Px(3), Px(80), Px(76));
                x += Px(84);
                index++;
                continue;
            }
            var column = buttons.Skip(index).TakeWhile(b => b.Command.Size == RibbonButtonSize.Small).Take(3).ToArray();
            int width = column.Max(b => TextRenderer.MeasureText(b.Command.Text, b.Button.Font).Width) + Px(34);
            for (int row = 0; row<column.Length; row++)
            column[row].Button.SetBounds(x, Px(3 + row * 26), width, Px(26));
            x += width + Px(4);
            index += column.Length;
        }
        PreferredWidth = Math.Max(x + Px(8), TextRenderer.MeasureText(Caption, Font).Width + Px(16));
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        int captionHeight = (int)Math.Ceiling(20 * DeviceDpi / 96F);
        int captionTop = Math.Max(0, Height - captionHeight);
        using var captionBrush = new SolidBrush(RibbonPalette.Caption);
        using var border = new Pen(RibbonPalette.Border, Math.Max(1, DeviceDpi / 96F));
        e.Graphics.FillRectangle(captionBrush, 0, captionTop, Width, captionHeight);
        e.Graphics.DrawLine(border, Math.Max(0, Width - 1), 6, Math.Max(0, Width - 1), Math.Max(6, Height - 6));
        TextRenderer.DrawText(e.Graphics, Caption, Font,
            new Rectangle(0, captionTop, Width, captionHeight), RibbonPalette.CaptionText,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);
    }
}
