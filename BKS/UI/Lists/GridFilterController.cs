using System.Data;
using System.Runtime.CompilerServices;

namespace BKS;

/// <summary>Arama ve sütun filtrelerini birleştirir; yenileme sırasında mevcut filtreleri korur.</summary>
internal sealed class GridFilterController
{
    private static readonly ConditionalWeakTable<DataGridView, GridFilterController> Instances = new();
    private readonly DataGridView grid;
    private readonly Dictionary<string, string> filters = new(StringComparer.OrdinalIgnoreCase);
    private DataView? currentView;
    private string baseFilter = string.Empty;
    private bool applying;

    public string Search { get; private set; } = string.Empty;
    public int FilterCount => filters.Count + (Search.Length > 0 ? 1 : 0);
    public bool CanFilter => ViewFor(grid.DataSource) is not null || ObjectSourceFor(grid.DataSource) is not null;
    public event EventHandler? Changed;

    public static GridFilterController For(DataGridView grid) => Instances.GetValue(grid, item => new GridFilterController(item));

    private GridFilterController(DataGridView grid)
    {
        this.grid = grid;
        grid.DataSourceChanged += (_, _) => Apply();
        grid.DataBindingComplete += (_, _) => Apply();
    }

    public void SetSearch(string? value)
    {
        value = (value ?? string.Empty).Trim();
        if (Search == value) return;
        Search = value;
        Apply();
    }

    public string ColumnFilter(string column) => filters.TryGetValue(column, out var value) ? value : string.Empty;

    public void SetColumnFilter(string column, string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) filters.Remove(column);
        else filters[column] = value.Trim();
        Apply();
    }

    public void Clear()
    {
        Search = string.Empty;
        filters.Clear();
        Apply();
    }

    private void Apply()
    {
        if (applying || grid.IsDisposed) return;
        applying = true;
        try
        {
            var view = ViewFor(grid.DataSource);
            if (view is not null)
            {
                if (!ReferenceEquals(view, currentView))
                {
                    currentView = view;
                    baseFilter = view.RowFilter;
                }
                var columns = view.Table!.Columns.Cast<DataColumn>().Where(column => column.DataType == typeof(string) || column.DataType.IsPrimitive || column.DataType == typeof(decimal) || column.DataType == typeof(DateTime)).ToArray();
                var availableFilters = filters.Where(filter => columns.Any(column => column.ColumnName.Equals(filter.Key, StringComparison.OrdinalIgnoreCase)))
                    .ToDictionary(filter => filter.Key, filter => filter.Value);
                var expression = TableFilterExpression.Build(columns.Select(column => column.ColumnName), Search, availableFilters, baseFilter);
                if (view.RowFilter != expression) view.RowFilter = expression;
            }
            else ObjectSourceFor(grid.DataSource)?.ApplyFilters(Search, filters);
        }
        finally { applying = false; }
        Changed?.Invoke(this, EventArgs.Empty);
    }

    private static IGridFilterSource? ObjectSourceFor(object? source) => source switch
    {
        IGridFilterSource list => list,
        BindingSource binding when !ReferenceEquals(binding.DataSource, binding) => ObjectSourceFor(binding.DataSource),
        _ => null
    };

    private static DataView? ViewFor(object? source) => source switch
    {
        DataTable table => table.DefaultView,
        DataView view => view,
        BindingSource binding when !ReferenceEquals(binding.DataSource, binding) => ViewFor(binding.DataSource) ?? binding.List as DataView,
        _ => null
    };
}
