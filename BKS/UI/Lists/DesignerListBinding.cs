using System.Runtime.CompilerServices;

namespace BKS;

/// <summary>Connects existing Designer controls to filters and counters; creates no layout.</summary>
internal sealed class DesignerListBinding : IDisposable
{
    private static readonly ConditionalWeakTable<DataGridView, DesignerListBinding> Instances = new();
    private readonly DataGridView grid;
    private readonly TextBox search;
    private readonly Label status;
    private readonly GridFilterController filters;
    private readonly System.Windows.Forms.Timer delay = new() { Interval = 180 };

    public static void Attach(DataGridView grid, TextBox search, Button clear, Button columns, Label status) =>
        Instances.GetValue(grid, _ => new DesignerListBinding(grid, search, clear, columns, status));

    private DesignerListBinding(DataGridView grid, TextBox search, Button clear, Button columns, Label status)
    {
        this.grid = grid;
        this.search = search;
        this.status = status;
        GridAppearance.Apply(grid);
        filters = GridFilterController.For(grid);
        var menu = new GridColumnMenu(grid, filters);
        ListSurface.RegisterSearch(grid, search);
        search.TextChanged += (_, _) => { delay.Stop(); delay.Start(); };
        search.KeyDown += (_, e) =>
        {
            if (e.KeyCode != Keys.Enter) return;
            delay.Stop(); filters.SetSearch(search.Text); e.SuppressKeyPress = true;
        };
        delay.Tick += (_, _) => { delay.Stop(); filters.SetSearch(search.Text); };
        clear.Click += (_, _) => { delay.Stop(); filters.Clear(); };
        columns.Click += (_, _) => menu.ShowColumnChooser(columns);
        filters.Changed += FilterChanged;
        grid.DataBindingComplete += (_, _) => UpdateStatus();
        grid.RowsAdded += (_, _) => UpdateStatus();
        grid.RowsRemoved += (_, _) => UpdateStatus();
        grid.SelectionChanged += (_, _) => UpdateStatus();
        grid.Disposed += (_, _) => Dispose();
        UpdateStatus();
    }
    private void FilterChanged(object? sender, EventArgs e)
    {
        if (search.Text.Trim() != filters.Search) search.Text = filters.Search;
        delay.Stop(); UpdateStatus();
    }
    private void UpdateStatus()
    {
        if (grid.IsDisposed || status.IsDisposed) return;
        int count = grid.Rows.Count - (grid.AllowUserToAddRows ? 1 : 0);
        foreach (DataGridViewColumn column in grid.Columns)
        {
            string key = string.IsNullOrEmpty(column.DataPropertyName) ? column.Name : column.DataPropertyName;
            string filter = filters.ColumnFilter(key);
            column.HeaderCell.Style.BackColor = filter.Length > 0 ? RibbonPalette.Pressed : RibbonPalette.GridHeader;
            column.HeaderCell.ToolTipText = filter.Length > 0 ? "Etkin filtre: " + filter : "Sıralamak için tıklayın; filtre için sağ tıklayın.";
        }
        status.Text = $"Kayıt: {Math.Max(0, count):N0}    |    Seçili: {grid.SelectedRows.Count:N0}    |    Filtre: {filters.FilterCount}";
    }
    public void Dispose()
    {
        delay.Dispose();
        filters.Changed -= FilterChanged;
    }
}
