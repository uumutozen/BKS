using System.Drawing.Drawing2D;
namespace BKS;
internal sealed class RecordContextMenus
{
    private readonly BksRibbon ribbon;
    private readonly ContextMenuStrip common = new();
    private readonly HashSet<Control> attached = new();
    private readonly HashSet<ContextMenuStrip> menus = new();
    private readonly Dictionary<RibbonIcon, Image> icons = new();
    private readonly Font groupFont = new("Segoe UI", 9F, FontStyle.Bold);
    private readonly object generated = new();
    public RecordContextMenus(Control root, BksRibbon ribbon)
    {
        this.ribbon = ribbon;
        Attach(root);
        root.Disposed += (_, _) =>
        {
            common.Dispose();
            foreach (var icon in icons.Values) icon.Dispose();
            groupFont.Dispose();
        };
    }
    private void Attach(Control control)
    {
        if (control is BksRibbon || control is Form && control.Controls.OfType<RibbonWorkspace>().Any()) return;
        if (!attached.Add(control)) return;
        control.Disposed += (_, _) => attached.Remove(control);
        if (control is not TextBoxBase and not ComboBox and not DateTimePicker and not NumericUpDown and not TabControl)
        {
            control.ContextMenuStrip ??= common;
            Configure(control.ContextMenuStrip);
        }
        if (control is DataGridView grid)
        grid.MouseDown += (_, e) =>
        {
            if (e.Button != MouseButtons.Right) return;
            var hit = grid.HitTest(e.X, e.Y);
            grid.ClearSelection();
            if (hit.RowIndex<0)
            {
                grid.CurrentCell = null;
                return;
            }
            var column = hit.ColumnIndex >= 0 ? grid.Columns[hit.ColumnIndex]: grid.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => c.Visible);
            if (column != null) grid.CurrentCell = grid.Rows[hit.RowIndex].Cells[column.Index];
            grid.Rows[hit.RowIndex].Selected = true;
        };
        control.ControlAdded += (_, e) =>
        {
            if (e.Control != null) Attach(e.Control);
        };
        foreach (Control child in control.Controls) Attach(child);
    }
    private Image Icon(RibbonIcon icon)
    {
        if (!icons.TryGetValue(icon, out var image))
        {
            image = MenuGlyph.Create(icon);
            icons.Add(icon, image);
        }
        return image;
    }
    private void Configure(ContextMenuStrip menu)
    {
        if (!menus.Add(menu)) return;
        menu.Font = new Font("Segoe UI", 10F);
        menu.ShowImageMargin = true;
        menu.ImageScalingSize = new Size(24, 24);
        menu.Renderer = new ToolStripProfessionalRenderer(new MenuColors());
        var existing = menu.Items.Cast<ToolStripItem>().ToArray();
        menu.Opening += (_, e) =>
        {
            foreach (var item in menu.Items.Cast<ToolStripItem>().Where(i => ReferenceEquals(i.Tag, generated)).ToArray())
            {
                menu.Items.Remove(item);
                item.Dispose();
            }
            menu.Items.Clear();
            foreach (var group in ribbon.CommandsFor(menu.SourceControl))
            {
                if (group.Commands.Length == 0) continue;
                if (menu.Items.Count> 0) menu.Items.Add(new ToolStripSeparator
                {
                    Tag = generated
                });
                menu.Items.Add(new ToolStripLabel(group.Title)
                {
                    Font = groupFont,
                    ForeColor = RibbonPalette.Accent,
                    Tag = generated
                });
                foreach (var command in group.Commands)
                {
                    var item = new ToolStripMenuItem(command.Text, Icon(command.Icon))
                    {
                        Tag = generated,
                        Padding = new Padding(4, 5, 16, 5),
                        Enabled = !AppConfiguration.DesignPreview && command.IsEnabled
                    };
                    item.Click += (_, _) => UiActions.Run(() =>
                    {
                        if (!AppConfiguration.DesignPreview && command.IsEnabled) command.Invoke();
                    });
                    menu.Items.Add(item);
                }
            }
            if (existing.Length> 0)
            {
                if (menu.Items.Count> 0) menu.Items.Add(new ToolStripSeparator
                {
                    Tag = generated
                });
                foreach (var item in existing)
                {
                    if (item is ToolStripMenuItem)
                    {
                        item.Image ??= Icon(RibbonIcon.View);
                        item.Padding = new Padding(4, 5, 16, 5);
                        item.Enabled = !AppConfiguration.DesignPreview && (menu.SourceControl is not DataGridView grid || grid.CurrentRow != null);
                    }
                    menu.Items.Add(item);
                }
            }
            e.Cancel = menu.Items.Count == 0;
        };
    }
    private sealed class MenuColors : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground => Color.FromArgb(248, 250, 254);
        public override Color ImageMarginGradientBegin => RibbonPalette.Surface;
        public override Color ImageMarginGradientMiddle => RibbonPalette.Surface;
        public override Color ImageMarginGradientEnd => RibbonPalette.Surface;
        public override Color MenuItemSelected => RibbonPalette.HoverBottom;
        public override Color MenuItemBorder => RibbonPalette.ActiveBorder;
        public override Color MenuBorder => RibbonPalette.Border;
    }
}
internal static class MenuGlyph
{
    public static Bitmap Create(RibbonIcon icon, int size = 24)
    {
        var bitmap = new Bitmap(size, size);
        using var g = Graphics.FromImage(bitmap);
        g.Clear(Color.Transparent);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.ScaleTransform(size / 24F, size / 24F);
        g.TranslateTransform(2, 2);
        g.ScaleTransform(.7F, .7F);
        using var pen = new Pen(RibbonPalette.Icon(icon), 2.3F)
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round,
            LineJoin = LineJoin.Round
        };
        switch (icon)
        {
            case RibbonIcon.Add:
            g.DrawRectangle(pen, 3, 3, 22, 24);
            g.DrawLine(pen, 14, 9, 14, 21);
            g.DrawLine(pen, 8, 15, 20, 15);
            break;
            case RibbonIcon.Edit:
            g.DrawLines(pen, new Point[]
            {
                new(4, 22),
                new(6, 15),
                new(21, 0),
                new(27, 6),
                new(12, 21),
                new(4, 22)
            });
            break;
            case RibbonIcon.Archive:
            g.DrawRectangle(pen, 3, 8, 23, 18);
            g.DrawRectangle(pen, 1, 2, 27, 6);
            g.DrawLine(pen, 11, 14, 18, 14);
            break;
            case RibbonIcon.Restore:
            g.DrawArc(pen, 4, 4, 22, 22, 210, 295);
            g.DrawLines(pen, new Point[]
            {
                new(1, 5),
                new(4, 13),
                new(12, 11)
            });
            break;
            case RibbonIcon.Refresh:
            g.DrawArc(pen, 3, 3, 23, 23, 35, 285);
            g.DrawLines(pen, new Point[]
            {
                new(20, 1),
                new(27, 8),
                new(18, 9)
            });
            break;
            case RibbonIcon.Export:
            g.DrawLines(pen, new Point[]
            {
                new(3, 18),
                new(3, 26),
                new(26, 26),
                new(26, 18)
            });
            g.DrawLine(pen, 14, 1, 14, 19);
            g.DrawLines(pen, new Point[]
            {
                new(8, 13),
                new(14, 19),
                new(20, 13)
            });
            break;
            case RibbonIcon.Backup:
            g.DrawRectangle(pen, 3, 2, 23, 25);
            g.DrawRectangle(pen, 8, 2, 13, 8);
            g.DrawRectangle(pen, 8, 17, 13, 10);
            break;
            case RibbonIcon.Folder:
            g.DrawLines(pen, new Point[]
            {
                new(1, 25),
                new(1, 3),
                new(11, 3),
                new(15, 8),
                new(27, 8),
                new(27, 25),
                new(1, 25)
            });
            break;
            case RibbonIcon.Print:
            g.DrawRectangle(pen, 1, 9, 27, 13);
            g.DrawRectangle(pen, 6, 1, 17, 8);
            g.DrawRectangle(pen, 6, 18, 17, 11);
            break;
            case RibbonIcon.Search:
            g.DrawEllipse(pen, 2, 1, 18, 18);
            g.DrawLine(pen, 18, 18, 27, 27);
            break;
            default:
            g.DrawRectangle(pen, 2, 3, 25, 23);
            g.DrawLine(pen, 2, 10, 27, 10);
            g.DrawLine(pen, 10, 10, 10, 26);
            break;
        }
        return bitmap;
    }
}
