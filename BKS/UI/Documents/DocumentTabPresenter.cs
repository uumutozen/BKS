namespace BKS;

internal enum DocumentCloseMode { Current, Others, ToRight, All }

/// <summary>Belge sekmelerinin görünümü ve fare davranışı. Form oluşturmaz, veri yüklemez.</summary>
internal sealed class DocumentTabPresenter
{
    private readonly TabControl tabs;
    private int hovered = -1;
    public event Action<TabPage, DocumentCloseMode>? CloseRequested;

    public DocumentTabPresenter(TabControl tabs)
    {
        this.tabs = tabs;
        tabs.Appearance = TabAppearance.Normal;
        tabs.DrawMode = TabDrawMode.OwnerDrawFixed;
        tabs.SizeMode = TabSizeMode.Fixed;
        tabs.Font = new Font("Segoe UI", 9.5F);
        tabs.ShowToolTips = true;
        tabs.Padding = new Point(10, 4);
        tabs.DrawItem += DrawTab;
        tabs.MouseDown += MouseDown;
        tabs.MouseMove += (_, e) =>
        {
            int next = -1;
            for (int i = 0; i < tabs.TabCount; i++)
                if (tabs.GetTabRect(i).Contains(e.Location)) { next = i; break; }
            if (hovered == next) return;
            hovered = next; tabs.Invalidate();
        };
        tabs.MouseLeave += (_, _) => { hovered = -1; tabs.Invalidate(); };
        tabs.DpiChangedAfterParent += (_, _) => ResizeTabs();
        ResizeTabs();
    }

    private void ResizeTabs() => tabs.ItemSize = new Size(Px(210), Px(32));
    private int Px(int value) => (int)Math.Ceiling(value * tabs.DeviceDpi / 96F);

    private void DrawTab(object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0 || e.Index >= tabs.TabCount) return;
        var page = tabs.TabPages[e.Index];
        bool selected = tabs.SelectedIndex == e.Index;
        using var fill = new SolidBrush(selected ? Color.White : hovered == e.Index ? RibbonPalette.HoverTop : RibbonPalette.DocumentTab);
        using var border = new Pen(RibbonPalette.Border);
        e.Graphics.FillRectangle(fill, e.Bounds);
        e.Graphics.DrawRectangle(border, e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
        if (selected)
        {
            using var accent = new SolidBrush(RibbonPalette.Accent);
            e.Graphics.FillRectangle(accent, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, Px(3));
        }
        var text = new Rectangle(e.Bounds.X + Px(10), e.Bounds.Y, Math.Max(0, e.Bounds.Width - Px(40)), e.Bounds.Height);
        TextRenderer.DrawText(e.Graphics, page.Text, tabs.Font, text, RibbonPalette.Text,
            TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        if (page.Name != "home")
            TextRenderer.DrawText(e.Graphics, "×", tabs.Font, CloseBounds(e.Index), RibbonPalette.CaptionText,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    }

    private Rectangle CloseBounds(int index)
    {
        var rect = tabs.GetTabRect(index);
        return new Rectangle(rect.Right - Px(28), rect.Top, Px(26), rect.Height);
    }

    private void MouseDown(object? sender, MouseEventArgs e)
    {
        for (int index = 0; index < tabs.TabCount; index++)
        {
            if (!tabs.GetTabRect(index).Contains(e.Location)) continue;
            var page = tabs.TabPages[index];
            if (page.Name == "home") return;
            if (e.Button == MouseButtons.Middle || e.Button == MouseButtons.Left && CloseBounds(index).Contains(e.Location))
                CloseRequested?.Invoke(page, DocumentCloseMode.Current);
            else if (e.Button == MouseButtons.Right) ShowMenu(page, e.Location);
            return;
        }
    }

    private void ShowMenu(TabPage page, Point location)
    {
        var menu = new ContextMenuStrip();
        foreach (var option in new[]
        {
            ("Kapat", DocumentCloseMode.Current), ("Diğerlerini kapat", DocumentCloseMode.Others),
            ("Sağdakileri kapat", DocumentCloseMode.ToRight), ("Tümünü kapat", DocumentCloseMode.All)
        })
            menu.Items.Add(option.Item1, null, (_, _) => CloseRequested?.Invoke(page, option.Item2));
        menu.Closed += (_, _) => menu.Dispose();
        menu.Show(tabs, location);
    }
}
