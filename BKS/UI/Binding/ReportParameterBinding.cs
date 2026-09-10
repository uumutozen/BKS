namespace BKS;

/// <summary>Uses the parameter grid owned by the form Designer; never constructs a control tree.</summary>
internal sealed class ReportParameterBinding
{
    private readonly DataGridView grid;
    public ReportParameterBinding(DataGridView grid) => this.grid = grid;
    public void SetQuery(ReportQuery query)
    {
        if (!grid.EndEdit()) throw new InvalidOperationException("Parametre değerini düzeltin.");
        var previous = grid.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow)
            .ToDictionary(r => Convert.ToString(r.Cells[0].Value)!, r => new object?[] { r.Cells[1].Value, r.Cells[2].Value, r.Cells[3].Value }, StringComparer.OrdinalIgnoreCase);
        grid.Rows.Clear();
        foreach (var name in query.Parameters)
        {
            var values = previous.GetValueOrDefault(name) ?? new object?[] { "Metin", "", false };
            grid.Rows.Add(name, values[0], values[1], values[2]);
        }
    }
    public IReadOnlyList<ReportArgument> Read()
    {
        if (!grid.EndEdit()) throw new InvalidOperationException("Parametre değerini düzeltin.");
        return grid.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow).Select(row =>
        {
            string name = Convert.ToString(row.Cells[0].Value) ?? "";
            string type = Convert.ToString(row.Cells[1].Value) ?? "Metin";
            try { return new ReportArgument(name, type, ReportQuery.Value(type, Convert.ToString(row.Cells[2].Value) ?? "", DataValues.Boolean(row.Cells[3].Value))); }
            catch (InvalidOperationException ex) { throw new InvalidOperationException(name + ": " + ex.Message); }
        }).ToArray();
    }
}
