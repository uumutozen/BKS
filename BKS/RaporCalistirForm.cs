namespace BKS;
public partial class RaporCalistirForm : Form
{
    private readonly int reportId;
    private readonly ReportRepository repository = new();
    private readonly FormOperation operation;
    private readonly ReportParameterBinding parameters;
    private ReportQuery? query;
    public RaporCalistirForm() : this(0) { }
    public RaporCalistirForm(int raporId)
    {
        reportId = raporId;
        InitializeComponent();
        operation = new FormOperation(this);
        parameters = new ReportParameterBinding(parameterGrid);
        Screens.PrepareDesignerForm(this);
        DesignerListBinding.Attach(gridSonuc, txtResultsSearch, pnlResultsClear, pnlResultsColumns, pnlResultsCount);
        btnCalistir.Enabled = false;
    }
    private async void RaporCalistirForm_Load(object sender, EventArgs e) => await operation.RunAsync(async () =>
    {
        var report = await Task.Run(() => repository.Read(reportId));
        query = ReportQuery.Parse(report.Query);
        Text = lblTitle.Text = report.Title;
        parameters.SetQuery(query);
        lblSubtitle.Text = query.Parameters.Count == 0 ? "Parametre gerekmiyor. Çalıştır ile listeyi getirin."
            : "Parametre türünü ve değerini girin. Boş (NULL) için ilgili kutuyu işaretleyin.";
        btnCalistir.Enabled = true;
    });
    private async void BtnCalistir_Click(object sender, EventArgs e) => await operation.RunAsync(async () =>
    {
        if (query == null) throw new InvalidOperationException("Rapor yüklenemedi. Ekranı kapatıp yeniden açın.");
        var values = parameters.Read();
        var table = await Task.Run(() => repository.Run(query, values));
        var old = gridSonuc.DataSource as System.Data.DataTable;
        gridSonuc.DataSource = table;
        old?.Dispose();
        lblSubtitle.Text = $"{table.Rows.Count:N0} kayıt    •    Son çalıştırma: {DateTime.Now:HH:mm:ss}";
    });
    private void CloseRecord_Click(object? sender, EventArgs e) => Close();
}