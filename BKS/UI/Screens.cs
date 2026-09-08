namespace BKS;
internal static class Screens
{
    public static BksRibbon Ribbon(string title, params RibbonCommand[] commands)
    {
        var ribbon = new BksRibbon();
        var actions = commands.Select((command, index) => command with
        {
            CommandId = "record." + index,
            Tooltip = string.IsNullOrWhiteSpace(command.Tooltip) ? command.Text: command.Tooltip,
            Size = command.Icon is RibbonIcon.Add or RibbonIcon.Backup ? RibbonButtonSize.Large: RibbonButtonSize.Small,
            Shortcut = command.Text.StartsWith("Kaydet") || command.Text == "Güncelle" ? Keys.Control | Keys.S
            : command.Text == "Yenile" ? Keys.F5: command.Shortcut,
            CanExecute = () => !AppConfiguration.DesignPreview && command.CanExecute?.Invoke() != false
        }).ToArray();
        ribbon.AddPage("record", title, ("Kayıt işlemleri", actions));
        return ribbon;
    }
    public static void Install(Form form, Control body, BksRibbon ribbon, string title)
    {
        form.SuspendLayout();
        form.Padding = Padding.Empty;
        ConfigureDpi(form);
        form.Text = title;
        Mount(form, body, ribbon);
        form.Text = title;
        form.BackColor = ModernWinForms.PageBack;
        form.Font = new Font("Segoe UI", 10F);
        form.FormBorderStyle = FormBorderStyle.Sizable;
        form.MaximizeBox = true;
        form.MinimizeBox = true;
        form.MinimumSize = new Size(600, 440);
        form.StartPosition = FormStartPosition.CenterParent;
        form.Shown += (_, _) => FitToScreen(form);
        form.ResumeLayout(true);
    }
    public static void FitToScreen(Form form)
    {
        var area = Screen.FromControl(form).WorkingArea;
        form.MinimumSize = new Size(Math.Min(600, area.Width), Math.Min(440, area.Height));
        if (form.TopLevel && form.WindowState == FormWindowState.Normal)
        {
            form.Size = new Size(Math.Min(Math.Max(form.Width, 900), area.Width), Math.Min(Math.Max(form.Height, 660), area.Height));
            form.Location = new Point(Math.Clamp(form.Left, area.Left, Math.Max(area.Left, area.Right - form.Width)), Math.Clamp(form.Top,
            area.Top, Math.Max(area.Top, area.Bottom - form.Height)));
        }
    }
    public static Control WithEditor(Control editor, DataGridView grid, float ratio = .38F)
    {
        ModernWinForms.StyleGrid(grid);
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        return new EditorGridPanel(editor, grid, ratio);
    }
    public static void ConfigureDpi(Form form)
    {
        // Discard a legacy Font baseline before changing the scaling unit to DPI.
        form.AutoScaleMode = AutoScaleMode.None;
        form.AutoScaleDimensions = new SizeF(form.DeviceDpi, form.DeviceDpi);
        form.AutoScaleMode = AutoScaleMode.Dpi;
    }
    public static void Mount(Form form, Control body, BksRibbon ribbon, Control? title = null, Control? footer = null)
    {
        if (body is SectionedForm sections)
        {
            var commands = sections.Titles.Select(title => new RibbonCommand(title, RibbonIcon.Folder, () => sections.SelectSection(title),
            IsSelected: () => sections.CurrentTitle == title)).ToArray();
            ribbon.SetNavigation("record", ("Form bölümleri", commands));
            sections.SectionChanged += (_, _) => ribbon.RefreshCommands();
        }
        else
        {
            ribbon.SetNavigation("record", ("Çalışma alanı", new[]
            {
                new RibbonCommand(form.Text, RibbonIcon.View, () => body.SelectNextControl(null, true, true, true, false))
            }));
        }
        form.Controls.Clear();
        form.Controls.Add(new RibbonWorkspace(body, ribbon, title, footer));
    }
    public static Control Grid(DataGridView grid)
    {
        ModernWinForms.StyleGrid(grid);
        grid.Dock = DockStyle.Fill;
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        return new ListSurface(grid);
    }
    public static void RevealAndFocus(Control target)
    {
        var chain = new Stack<Control>();
        for (Control? current = target; current != null; current = current.Parent) chain.Push(current);
        while (chain.Count> 0)
        {
            var child = chain.Pop();
            if (child is TabPage page && page.Parent is TabControl tabs) tabs.SelectedTab = page;
            if (child.Parent is ScrollableControl scroll) scroll.ScrollControlIntoView(child);
        }
        target.Focus();
    }
    public static Button Button(string text, Action action)
    {
        var button = new Button
        {
            Text = text,
            AutoSize = false,
            Width = 150,
            Height = 38,
            FlatStyle = FlatStyle.Flat,
            BackColor = ModernWinForms.Primary,
            ForeColor = Color.White,
            Cursor = Cursors.Hand
        };
        button.FlatAppearance.BorderSize = 0;
        button.Click += (_, _) => UiActions.Run(action);
        return button;
    }
}
