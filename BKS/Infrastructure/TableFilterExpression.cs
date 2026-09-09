namespace BKS;

/// <summary>DataView filtre ifadelerini tek yerde ve özel karakterleri kaçırarak oluşturur.</summary>
public static class TableFilterExpression
{
    public static string Contains(string column, string value)
    {
        var name = column.Replace("\\", "\\\\").Replace("]", "\\]");
        return $"CONVERT([{name}], 'System.String') LIKE '%{DataValues.EscapeLike(value)}%'";
    }

    public static string Build(IEnumerable<string> searchableColumns, string? search,
        IReadOnlyDictionary<string, string> columnFilters, string? baseFilter = null)
    {
        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(baseFilter)) parts.Add("(" + baseFilter + ")");
        if (!string.IsNullOrWhiteSpace(search))
        {
            var expressions = searchableColumns.Select(column => Contains(column, search.Trim())).ToArray();
            if (expressions.Length > 0) parts.Add("(" + string.Join(" OR ", expressions) + ")");
        }
        parts.AddRange(columnFilters.Where(filter => !string.IsNullOrWhiteSpace(filter.Value))
            .Select(filter => "(" + Contains(filter.Key, filter.Value.Trim()) + ")"));
        return string.Join(" AND ", parts);
    }
}
