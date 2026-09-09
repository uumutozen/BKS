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
        if (control is BksRibbon || control is Form) return;
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
            if (menu.SourceControl is DataGridView source)
            {
                var point = source.PointToClient(Cursor.Position);
                if (source.HitTest(point.X, point.Y).Type == DataGridViewHitTestType.ColumnHeader)
                {
                    e.Cancel = true;
                    return;
                }
            }
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
