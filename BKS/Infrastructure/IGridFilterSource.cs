namespace BKS;

/// <summary>DataTable dışındaki liste kaynaklarının ortak filtre sözleşmesi.</summary>
public interface IGridFilterSource
{
    void ApplyFilters(string search, IReadOnlyDictionary<string, string> columns);
}
