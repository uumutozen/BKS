using System.Data;
namespace BKS;
public partial class DataListForm : Form
{
    private readonly Func<DataTable> read;
    private readonly FormOperation operation;
    public DataListForm() : this("İşlem geçmişi", () => new DataTable()) { }
    public DataListForm(string title, Func<DataTable> read)
    {
        this.read = read;
        InitializeComponent();
        operation = new FormOperation(this);
        Text = lblTitle.Text = pnlListTitle.Text = title;
        Screens.PrepareDesignerForm(this);
        DesignerListBinding.Attach(grid, txtSearch, pnlListClear, pnlListColumns, pnlListCount);
        GridAppearance.Apply(detailGrid);
        grid.SelectionChanged += (_, _) => ShowDetails();
        grid.DataBindingComplete += (_, _) => ConfigureColumns();
        grid.CellFormatting += (_, e) =>
        {
            if (e.Value is string text && text is "INSERT" or "UPDATE" or "DELETE")
            { e.Value = HistoryDetails.Format(text); e.FormattingApplied = true; }
        };
        Shown += async (_, _) => await ReloadAsync();
        KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.F5) { Refresh_Click(this, e); e.Handled = true; }
            if (e.Control && e.KeyCode == Keys.F) { txtSearch.Focus(); e.Handled = true; }
        };
    }
    internal void BindTable(DataTable table)
    {
        var old = grid.DataSource as DataTable;
        grid.DataSource = table;
        if (!ReferenceEquals(old, table)) old?.Dispose();
        ConfigureColumns();
        ShowDetails();
    }
    private void ConfigureColumns()
    {
        foreach (DataGridViewColumn column in grid.Columns)
        {
            column.HeaderText = HistoryDetails.Caption(column.Name);
            if (column.ValueType == typeof(DateTime))
            {
                column.DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss";
                column.Width = Math.Max(column.Width, (int)Math.Ceiling(190 * DeviceDpi / 96F));
            }
            if (grid.DataSource is DataTable table && table.Rows.Cast<DataRow>().Take(20).Any(row => HistoryDetails.IsJson(row[column.Name])))
                column.Visible = false;
        }
    }
    private void ShowDetails()
    {
        var row = (grid.CurrentRow?.DataBoundItem as DataRowView)?.Row;
        var old = detailGrid.DataSource as DataTable;
        detailGrid.DataSource = HistoryDetails.Read(row);
        old?.Dispose();
        detailGrid.RowHeadersVisible = false;
        detailGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        detailGrid.Columns[0].FillWeight = 40;
        detailGrid.Columns[1].FillWeight = 60;
        detailGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        detailGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        lblDetails.Text = row == null ? "Ayrıntılar için bir işlem seçin." : "Seçili işlemin kayıt ayrıntıları · salt okunur";
    }
    private async void Refresh_Click(object? sender, EventArgs e) => await ReloadAsync();
    private void Close_Click(object? sender, EventArgs e) => Close();
    public Task ReloadAsync() => operation.RunAsync(async () =>
    {
        var table = await Task.Run(read);
        if (IsDisposed) { table.Dispose(); return; }
        BindTable(table);
        lblSubtitle.Text = $"Bu bölümdeki tüm işlemler    •    Son yenileme: {DateTime.Now:HH:mm:ss}";
    });
}
