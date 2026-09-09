namespace BKS;

/// <summary>Veri okuma ile WinForms alanlarını doldurma işlemini birbirinden ayırır.</summary>
internal sealed class RecordFieldBinder
{
    private readonly RecordValues values;

    public RecordFieldBinder(RecordValues values) => this.values = values;

    public static RecordValues Snapshot(DataGridViewRow row)
    {
        var values = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
        foreach (DataGridViewCell cell in row.Cells)
        {
            if (cell.OwningColumn is not { } column) continue;
            values[column.Name] = cell.Value;
            if (!string.IsNullOrEmpty(column.DataPropertyName)) values[column.DataPropertyName] = cell.Value;
        }
        return new RecordValues(values);
    }

    public void Text(params (Control Input, string Column)[] fields)
    {
        foreach (var (input, column) in fields) input.Text = values.Get(column, string.Empty);
    }

    public void Date(DateTimePicker input, string column)
    {
        var value = values.Get(column, input.Value);
        input.Value = value < input.MinDate ? input.MinDate : value > input.MaxDate ? input.MaxDate : value;
    }

    public void Number(NumericUpDown input, string column) =>
        input.Value = Math.Clamp(values.Get(column, input.Value), input.Minimum, input.Maximum);
}
