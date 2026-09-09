using System.Runtime.CompilerServices;

namespace BKS;

/// <summary>Tüm tablolar için arama, filtre durumu, sütun seçimi ve kayıt sayısı.</summary>
internal sealed class ListSurface : Panel
{
    private static readonly ConditionalWeakTable<DataGridView, ListSurface> Instances = new();
    private static readonly ConditionalWeakTable<DataGridView, TextBox> DesignerSearches = new();
    public static void RegisterSearch(DataGridView grid, TextBox search)
    {
        DesignerSearches.Remove(grid);
        DesignerSearches.Add(grid, search);
    }
    private readonly DataGridView grid;
    private readonly GridFilterController filters;
    private readonly GridColumnMenu columnMenu;
    private readonly Label title;
    private readonly Label count;
    private readonly Label footer;
    private readonly TextBox search;
    private readonly Button clear;
    private readonly Button columns;
    private readonly System.Windows.Forms.Timer searchDelay = new() { Interval = 180 };

    public ListSurface(DataGridView grid, string? caption = null)
    {
        this.grid = grid;
        filters = GridFilterController.For(grid);
        columnMenu = new GridColumnMenu(grid, filters);
        Instances.Remove(grid);
        Instances.Add(grid, this);
        Dock = DockStyle.Fill;
        BackColor = RibbonPalette.Group;
        title = new Label
        {
            Text = caption ?? TitleFor(grid.Name), AutoEllipsis = true,
            ForeColor = RibbonPalette.Text, Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleLeft
        };
        count = new Label { ForeColor = RibbonPalette.CaptionText, TextAlign = ContentAlignment.MiddleRight };
        footer = new Label
        {
            ForeColor = RibbonPalette.CaptionText, BackColor = RibbonPalette.Surface,
            Font = new Font("Segoe UI", 9F), TextAlign = ContentAlignment.MiddleLeft, AutoEllipsis = true
        };
        search = new TextBox
        {
            PlaceholderText = "Listede ara (Ctrl+F)", AccessibleName = "Listede ara",
            BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 10F)
        };
        clear = ToolbarButton("Temizle", "Arama ve sütun filtrelerini temizle", () => filters.Clear());
        columns = ToolbarButton("Sütunlar", "Gösterilecek sütunları seç", () => columnMenu.ShowColumnChooser(columns));
        grid.Dock = DockStyle.None;
        grid.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        Controls.AddRange(new Control[] { grid, title, count, footer, search, clear, columns });
        search.TextChanged += (_, _) => { searchDelay.Stop(); searchDelay.Start(); };
        searchDelay.Tick += (_, _) => { searchDelay.Stop(); filters.SetSearch(search.Text); };
        search.KeyDown += (_, e) =>
        {
            if (e.KeyCode != Keys.Enter) return;
            searchDelay.Stop();
            filters.SetSearch(search.Text);
            e.SuppressKeyPress = true;
        };
        filters.Changed += FiltersChanged;
        grid.DataBindingComplete += (_, _) => UpdateStatus();
        grid.RowsAdded += (_, _) => UpdateStatus();
        grid.RowsRemoved += (_, _) => UpdateStatus();
        grid.SelectionChanged += (_, _) => UpdateStatus();
        UpdateStatus();
    }

    public static bool FocusSearch(DataGridView grid)
    {
        if (DesignerSearches.TryGetValue(grid, out var search))
        {
            Screens.RevealAndFocus(search);
            search.SelectAll();
            return search.Focused;
        }
        if (!Instances.TryGetValue(grid, out var surface)) return false;
        Screens.RevealAndFocus(surface.search);
        surface.search.SelectAll();
        return surface.search.Focused;
    }

    public static bool FocusSearchIn(Control root)
    {
        if (root is DataGridView grid && grid.Visible) return FocusSearch(grid);
        if (root is ListSurface surface && surface.Visible) return FocusSearch(surface.grid);
        foreach (Control child in root.Controls)
            if (child.Visible && FocusSearchIn(child)) return true;
        return false;
    }

    private Button ToolbarButton(string caption, string description, Action action)
    {
        var button = new Button
        {
            Text = caption, AccessibleDescription = description, FlatStyle = FlatStyle.Flat,
            BackColor = RibbonPalette.Surface, ForeColor = RibbonPalette.Text, Cursor = Cursors.Hand,
            Font = new Font("Segoe UI", 9F)
        };
        button.FlatAppearance.BorderColor = RibbonPalette.Border;
        button.FlatAppearance.MouseOverBackColor = RibbonPalette.HoverTop;
        button.Click += (_, _) => UiActions.Run(action);
        return button;
    }

    private void FiltersChanged(object? sender, EventArgs e)
    {
        if (search.Text.Trim() != filters.Search) search.Text = filters.Search;
        searchDelay.Stop();
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        if (IsDisposed) return;
        int records = Math.Max(0, grid.Rows.Count - (grid.NewRowIndex >= 0 ? 1 : 0));
        count.Text = $"{records:N0} kayıt";
        clear.Enabled = filters.FilterCount > 0;
        columns.Enabled = grid.Columns.Count > 0;
        search.Enabled = filters.CanFilter;
        string selected = grid.SelectedRows.Count > 0 ? $" · {grid.SelectedRows.Count} seçili" : string.Empty;
        string filtered = filters.FilterCount > 0 ? $" · {filters.FilterCount} filtre" : string.Empty;
        footer.Text = $"{records:N0} kayıt{selected}{filtered}  |  Satır işlemleri ve sütun filtreleri için sağ tıklayın.";
        foreach (DataGridViewColumn column in grid.Columns)
        {
            string key = column.DataPropertyName.Length > 0 ? column.DataPropertyName : column.Name;
            string value = filters.ColumnFilter(key);
            column.HeaderCell.Style.BackColor = value.Length > 0 ? RibbonPalette.Pressed : RibbonPalette.GridHeader;
            column.HeaderCell.ToolTipText = value.Length > 0 ? "Etkin filtre: " + value : "Sıralama için tıklayın; filtrelemek için sağ tıklayın.";
        }
        Invalidate();
    }

    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e);
        if (grid == null || columns == null) return;
        int Px(int value) => (int)Math.Ceiling(value * DeviceDpi / 96F);
        bool singleRow = Width >= Px(900);
        int head = Height < Px(150) ? 0 : Px(singleRow ? 48 : 82);
        int foot = Height < Px(250) ? 0 : Px(28);
        foreach (var control in new Control[] { title, count, search, clear, columns }) control.Visible = head > 0;
        footer.Visible = foot > 0;
        int gap = Px(10);
        int toolsY = singleRow ? Px(8) : Px(42);
        int buttonWidth = Math.Min(Px(88), Math.Max(0, (Width - gap * 4) / 4));
        int right = Math.Max(0, Width - gap);
        columns.SetBounds(Math.Max(0, right - buttonWidth), toolsY, buttonWidth, Px(30));
        clear.SetBounds(Math.Max(0, columns.Left - gap - buttonWidth), toolsY, buttonWidth, Px(30));
        int searchLeft = singleRow ? Math.Max(Px(300), clear.Left - gap - Px(320)) : gap;
        search.SetBounds(searchLeft, toolsY + Px(3), Math.Max(0, clear.Left - gap - searchLeft), Px(26));
        int headingRight = singleRow ? searchLeft - gap : right;
        int countWidth = Math.Min(Px(100), Math.Max(0, headingRight / 3));
        count.SetBounds(Math.Max(0, headingRight - countWidth), 0, countWidth, Px(singleRow ? 48 : 36));
        title.SetBounds(gap, 0, Math.Max(0, count.Left - gap * 2), count.Height);
        grid.SetBounds(0, head, Width, Math.Max(0, Height - head - foot));
        footer.SetBounds(0, Height - foot, Width, foot);
        footer.Padding = new Padding(gap, 0, 0, 0);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (Width < 1 || Height < 1) return;
        using var pen = new Pen(RibbonPalette.Border);
        e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        e.Graphics.DrawLine(pen, 0, grid.Top, Width, grid.Top);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            searchDelay.Dispose();
            filters.Changed -= FiltersChanged;
            Instances.Remove(grid);
        }
        base.Dispose(disposing);
    }

    private static string TitleFor(string name) => name switch
    {
        "dgvOnKayitlar" => "Ön kayıtlar",
        "dataGridViewStok" => "Öğrenciler",
        "DgvOgrenciYonetimiSiniflar" => "Sınıflar",
        "dgvPersonelYonetimi" => "Personel listesi",
        "dataOgrVw" => "Ödeme hareketleri",
        "dataGridOdeme" => "Gelir ve giderler",
        "salesGrid" => "Kayıtlı raporlar",
        "dgvDosyalar" => "Dosya arşivi",
        "dgFaturalar" => "Fatura geçmişi",
        _ => "Kayıt listesi"
    };
}
