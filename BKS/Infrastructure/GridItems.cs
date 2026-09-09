using System.ComponentModel;
using System.Globalization;

namespace BKS;

/// <summary>Nesne listeleri için filtrelenebilir ve sıralanabilir, salt okunur görünüm.</summary>
public sealed class GridItems<T> : BindingList<T>, IGridFilterSource
{
    private readonly T[] source;
    private readonly PropertyDescriptor[] properties;
    private PropertyDescriptor? sortProperty;
    private ListSortDirection sortDirection;
    private string search = string.Empty;
    private IReadOnlyDictionary<string, string> filters = new Dictionary<string, string>();

    public GridItems(IEnumerable<T> source) : base(source.ToList())
    {
        this.source = Items.ToArray();
        properties = TypeDescriptor.GetProperties(typeof(T)).Cast<PropertyDescriptor>()
            .Where(property => property.PropertyType == typeof(string) || property.PropertyType.IsPrimitive
                || property.PropertyType == typeof(decimal) || property.PropertyType == typeof(DateTime)).ToArray();
        AllowNew = AllowEdit = AllowRemove = false;
    }

    protected override bool SupportsSortingCore => true;
    protected override bool IsSortedCore => sortProperty is not null;
    protected override PropertyDescriptor? SortPropertyCore => sortProperty;
    protected override ListSortDirection SortDirectionCore => sortDirection;

    protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
    {
        sortProperty = property;
        sortDirection = direction;
        RefreshView();
    }

    protected override void RemoveSortCore() { sortProperty = null; RefreshView(); }

    public void ApplyFilters(string search, IReadOnlyDictionary<string, string> columns)
    {
        this.search = search;
        filters = columns;
        RefreshView();
    }

    private bool Contains(T item, PropertyDescriptor property, string text) =>
        CultureInfo.CurrentCulture.CompareInfo.IndexOf(
            Convert.ToString(property.GetValue(item), CultureInfo.CurrentCulture) ?? string.Empty,
            text, CompareOptions.IgnoreCase) >= 0;

    private void RefreshView()
    {
        IEnumerable<T> view = source;
        if (search.Length > 0) view = view.Where(item => properties.Any(property => Contains(item, property, search)));
        foreach (var filter in filters)
        {
            var property = properties.FirstOrDefault(candidate => candidate.Name.Equals(filter.Key, StringComparison.OrdinalIgnoreCase));
            if (property is not null) view = view.Where(item => Contains(item, property, filter.Value));
        }
        if (sortProperty is { } sort)
        {
            var comparer = Comparer<object?>.Create(System.Collections.Comparer.Default.Compare);
            view = sortDirection == ListSortDirection.Ascending
                ? view.OrderBy(item => sort.GetValue(item), comparer)
                : view.OrderByDescending(item => sort.GetValue(item), comparer);
        }
        var result = view.ToArray();
        if (result.SequenceEqual(Items)) return;
        RaiseListChangedEvents = false;
        try
        {
            Items.Clear();
            foreach (var item in result) Items.Add(item);
        }
        finally { RaiseListChangedEvents = true; }
        ResetBindings();
    }
}
