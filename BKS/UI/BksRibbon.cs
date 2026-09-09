namespace BKS;
/// <summary>Renders command definitions. It contains no database or module business logic.</summary>
public sealed class BksRibbon : UserControl
{
    private sealed record Page(RibbonTabButton Tab, (string Title, RibbonCommand[] Commands)[] Groups);
    private readonly Panel tabs = new()
    {
        BackColor = RibbonPalette.TabStrip
    };
    private readonly Panel navigation = new()
    {
        BackColor = RibbonPalette.Surface
    };
    private readonly Button collapse = new()
    {
        Text = "⌃",
        AccessibleName = "Ribbon daralt / genişlet"
    };
    private readonly Button moreTabs = new()
    {
        Text = "Diğer ▾",
        AccessibleName = "Diğer sekmeler"
    };
    private readonly Button moreGroups = new()
    {
        Text = "Diğer ▾",
        AccessibleName = "Diğer komutlar"
    };
    private readonly Dictionary<string, Page> pages = new();
    private readonly Dictionary<string, (string Title, RibbonCommand[] Commands)[]> navigationGroups = new();
    private readonly HashSet<string> allowed = new(StringComparer.OrdinalIgnoreCase);
    private readonly List<string> hiddenTabs = new();
    private readonly List<(string Title, RibbonCommand[] Commands)> hiddenGroups = new();
    private bool arranging;
    public event EventHandler? CommandsChanged;
    private bool expanded = true;
    private string? selected;
    public event Action<string>? PageSelected;
    public event EventHandler? ExpandedChanged;
    public bool IsExpanded => expanded;
    public string? SelectedPageKey => selected;
    public BksRibbon()
    {
        AutoScaleMode = AutoScaleMode.None;
        Height = 134;
        Font = new Font("Segoe UI", 9F);
        BackColor = RibbonPalette.TabStrip;
        Controls.AddRange(new Control[]
        {
            tabs,
            navigation,
            collapse,
            moreTabs
        });
        foreach (var button in new[]
        {
            collapse,
            moreTabs,
            moreGroups
        })
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = RibbonPalette.TabStrip;
        }
        collapse.Click += (_, _) => ToggleExpanded();
        moreTabs.Click += (_, _) => ShowTabOverflow();
        moreGroups.Click += (_, _) => ShowCommandOverflow();
    }
    public void AddPage(string key, string title, params(string Title, RibbonCommand[] Commands)[] groups)
    {
        var tab = new RibbonTabButton
        {
            Text = title,
            Cursor = Cursors.Hand
        };
        tab.Click += (_, _) => SelectPage(key);
        var definitions = groups.Select((group, groupIndex) => (group.Title,
        group.Commands.Select((command, commandIndex) => command with
        {
            CommandId = string.IsNullOrEmpty(command.CommandId) ? $"{key}.{groupIndex}.{commandIndex}": command.CommandId,
            Group = group.Title,
            Tooltip = string.IsNullOrEmpty(command.Tooltip) ? command.Text: command.Tooltip
        }).ToArray())).ToArray();
        pages.Add(key, new Page(tab, definitions));
        allowed.Add(key);
        tabs.Controls.Add(tab);
        if (selected == null) SelectPage(key, false);
        PerformLayout();
    }
    public void SetNavigation(string key, params(string Title, RibbonCommand[] Commands)[] groups)
    {
        navigationGroups[key] = groups;
        if (selected == key) BuildNavigation();
    }
    public void SetAllowed(IEnumerable<string> keys)
    {
        allowed.Clear();
        allowed.UnionWith(keys);
        if (selected == null || !allowed.Contains(selected))
        {
            selected = pages.Keys.FirstOrDefault(allowed.Contains);
            if (selected != null) SelectPage(selected, false);
            else BuildNavigation();
        }
        PerformLayout();
    }
    public void SelectPage(string key, bool notify = true)
    {
        if (!allowed.Contains(key) || !pages.ContainsKey(key)) return;
        selected = key;
        foreach (var entry in pages) entry.Value.Tab.Selected = entry.Key == key;
        BuildNavigation();
        if (notify) PageSelected?.Invoke(key);
    }
    internal IEnumerable<(string Title, RibbonCommand[] Commands)> ActiveGroups()
    {
        if (selected == null || !allowed.Contains(selected)) return Array.Empty<(string, RibbonCommand[])>();
        var groups = pages[selected].Groups.AsEnumerable();
        if (navigationGroups.TryGetValue(selected, out var links)) groups = links.Concat(groups);
        return groups;
    }
    internal IEnumerable<(string Title, RibbonCommand[] Commands)> CommandsFor(Control? source)
    {
        string? key = selected;
        for (var control = source; control != null; control = control.Parent)
        if (control is TabPage)
        {
            var candidate = control.Name switch
            {
                "tabPageSatis" or "tabPageGelirGider" => "finance",
                "tabPageOgrenciOnKayit" => "tabPageStok",
                _ => control.Name
            };
            if (!pages.ContainsKey(candidate)) continue;
            key = candidate;
            break;
        }
        if (key == null || !allowed.Contains(key) || !pages.TryGetValue(key, out var page))
        return Array.Empty<(string, RibbonCommand[])>();
        return page.Groups.Where(group => source?.Name switch
        {
            "DgvOgrenciYonetimiSiniflar" => group.Title == "Sınıflar",
            "dataGridViewStok" => group.Title != "Sınıflar",
            _ => true
        });
    }
    public bool TryShortcut(Keys keyData)
    {
        var command = ActiveGroups().SelectMany(g => g.Commands)
        .FirstOrDefault(c => c.Shortcut != Keys.None && c.Shortcut == keyData && c.IsEnabled);
        if (command == null) return false;
        command.Invoke();
        return true;
    }
    private void BuildNavigation()
    {
        navigation.Controls.Remove(moreGroups);
        foreach (Control old in navigation.Controls.Cast<Control>().ToArray()) old.Dispose();
        foreach (var group in ActiveGroups()) navigation.Controls.Add(new RibbonGroupView(group.Title, group.Commands));
        navigation.Controls.Add(moreGroups);
        PerformLayout();
    }
    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e);
        if (arranging || pages == null || tabs == null) return;
        arranging = true;
        try
        {
            int Px(int n) => (int) Math.Ceiling(n * DeviceDpi / 96F);
            int top = Math.Min(Height, Px(32));
            tabs.SetBounds(0, 0, Math.Max(0, Width - Px(32)), top);
            collapse.SetBounds(Math.Max(0, Width - Px(32)), 0, Px(32), top);
            collapse.Text = expanded ? "⌃": "⌄";
            navigation.SetBounds(0, top, Width, Math.Max(0, Height - top));
            navigation.Visible = expanded;
            var accessible = pages.Where(p => allowed.Contains(p.Key)).ToArray();
            int TabWidth(Page p) => TextRenderer.MeasureText(p.Tab.Text, p.Tab.Font).Width + Px(24);
            bool overflow = accessible.Sum(p => TabWidth(p.Value))> tabs.Width;
            int limit = tabs.Width - (overflow ? Px(80): 0);
            int x = 0;
            hiddenTabs.Clear();
            foreach (var entry in pages)
            {
                int width = TabWidth(entry.Value);
                bool fits = allowed.Contains(entry.Key) && x + width <= limit;
                entry.Value.Tab.Visible = fits;
                if (fits)
                {
                    entry.Value.Tab.SetBounds(x, 0, width, top);
                    x += width;
                }
                else if (allowed.Contains(entry.Key)) hiddenTabs.Add(entry.Key);
            }
            moreTabs.Visible = hiddenTabs.Count> 0;
            moreTabs.SetBounds(Math.Max(0, tabs.Width - Px(80)), 0, Px(80), top);
            x = 0;
            hiddenGroups.Clear();
            var views = navigation.Controls.OfType<RibbonGroupView>().ToArray();
            var groups = ActiveGroups().ToArray();
            overflow = views.Sum(v => v.PreferredWidth)> Width;
            limit = Width - (overflow ? Px(88): 0);
            for (int i = 0; i<views.Length; i++)
            {
                var view = views[i];
                view.PerformLayout();
                bool fits = x + view.PreferredWidth <= limit;
                view.Visible = fits;
                if (fits)
                {
                    view.SetBounds(x, 0, view.PreferredWidth, navigation.Height);
                    x += view.Width;
                }
                else hiddenGroups.Add(groups[i]);
            }
            moreGroups.Visible = hiddenGroups.Count> 0;
            moreGroups.SetBounds(Math.Max(0, Width - Px(88)), Px(8), Px(84), Px(64));
        }
        finally
        {
            arranging = false;
        }
    }
    private void ShowTabOverflow()
    {
        var menu = new ContextMenuStrip();
        foreach (var key in hiddenTabs)
        {
            var item = new ToolStripMenuItem(pages[key].Tab.Text)
            {
                Checked = key == selected
            };
            item.Click += (_, _) => SelectPage(key);
            menu.Items.Add(item);
        }
        menu.Closed += (_, _) => menu.Dispose();
        menu.Show(moreTabs, new Point(0, moreTabs.Height));
    }
    private void ShowCommandOverflow()
    {
        var menu = new ContextMenuStrip();
        var images = new List<Image>();
        foreach (var group in hiddenGroups)
        {
            var parent = new ToolStripMenuItem(group.Title);
            foreach (var command in group.Commands)
            {
                var icon = MenuGlyph.Create(command.Icon);
                images.Add(icon);
                var item = new ToolStripMenuItem(command.Text, icon)
                {
                    Enabled = command.IsEnabled,
                    ToolTipText = command.Tooltip
                };
                item.Click += (_, _) => command.Invoke();
                parent.DropDownItems.Add(item);
            }
            menu.Items.Add(parent);
        }
        menu.Closed += (_, _) =>
        {
            menu.Dispose();
            foreach (var image in images) image.Dispose();
        };
        menu.Show(moreGroups, new Point(0, moreGroups.Height));
    }
    public void RefreshCommands()
    {
        foreach (var group in navigation.Controls.OfType<RibbonGroupView>()) group.RefreshCommands();
        CommandsChanged?.Invoke(this, EventArgs.Empty);
    }
    public void SetExpanded(bool value)
    {
        if (expanded == value) return;
        expanded = value;
        ExpandedChanged?.Invoke(this, EventArgs.Empty);
        PerformLayout();
    }
    public void ToggleExpanded() => SetExpanded(!expanded);
    protected override void OnDpiChangedAfterParent(EventArgs e)
    {
        base.OnDpiChangedAfterParent(e);
        PerformLayout();
    }
}
