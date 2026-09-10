namespace BKS;
public partial class OzelRapor : Form
{
    private readonly ReportRepository repository = new();
    private readonly FormOperation operation;
    private readonly ReportParameterBinding parameters;
    private readonly FormDraft draft;
    private int? reportId;
    public OzelRapor()
    {
        InitializeComponent();
        operation = new FormOperation(this);
        parameters = new ReportParameterBinding(parameterGrid);
        Screens.PrepareDesignerForm(this);
        DesignerListBinding.Attach(dataGridView1, txtResultsSearch, pnlResultsClear, pnlResultsColumns, pnlResultsCount);
        draft = new FormDraft(this, () => txtRaporAdi.Text + "\0" + txtQuery.Text, SaveAsync);
        txtQuery.KeyDown += Query_KeyDown;
    }
    private static void RequireAdmin()
    {
        if (!ModuleAccess.IsAdmin(SessionContext.Role)) throw new InvalidOperationException("Rapor tasarımı için yönetici yetkisi gerekir.");
    }
    private async void OzelRapor_Load(object sender, EventArgs e) => await operation.RunAsync(async () =>
    {
        RequireAdmin();
        await LoadListAsync();
        lblSubtitle.Text = "SELECT sorgunuzu yazın; parametre türlerini seçip sonucu önizleyin.";
    });
    private async Task LoadListAsync()
    {
        var table = await Task.Run(repository.List);
        if (IsDisposed) { table.Dispose(); return; }
        cmbOzelRaporlar.DisplayMember = "RaporAdi";
        cmbOzelRaporlar.ValueMember = "Id";
        var old = cmbOzelRaporlar.DataSource as System.Data.DataTable;
        cmbOzelRaporlar.DataSource = table;
        cmbOzelRaporlar.SelectedValue = (object?)reportId ?? -1;
        old?.Dispose();
    }
    private async Task<bool> SaveAsync()
    {
        bool saved = false;
        await operation.RunAsync(async () =>
        {
            RequireAdmin();
            string title = txtRaporAdi.Text.Trim(), query = txtQuery.Text;
            ReportQuery.Parse(query);
            if (title.Length == 0) throw new InvalidOperationException("Rapor adı gerekli.");
            var id = reportId;
            reportId = await Task.Run(() => repository.Save(id, title, query));
            draft.Accept();
            saved = true;
            lblSubtitle.Text = "Rapor kaydedildi.";
            await LoadListAsync();
        });
        return saved;
    }
    private async void btnRaporKaydet_Click(object sender, EventArgs e) => await SaveAsync();
    private async void btnRaporYukle_Click(object sender, EventArgs e)
    {
        if (operation.IsBusy || cmbOzelRaporlar.SelectedValue is not int selected) return;
        if (!await draft.ConfirmAsync()) return;
        await operation.RunAsync(async () =>
        {
            RequireAdmin();
            var report = await Task.Run(() => repository.Read(selected));
            var query = ReportQuery.Parse(report.Query);
            reportId = report.Id;
            txtRaporAdi.Text = report.Title;
            txtQuery.Text = report.Query;
            parameters.SetQuery(query);
            draft.Accept();
            tabReport.SelectedTab = tabQuery;
            lblSubtitle.Text = "Kayıtlı rapor yüklendi. Kaydet, bu raporu günceller.";
        });
    }
    private async void NewReport_Click(object? sender, EventArgs e)
    {
        if (operation.IsBusy || !await draft.ConfirmAsync()) return;
        reportId = null;
        cmbOzelRaporlar.SelectedIndex = -1;
        txtRaporAdi.Clear(); txtQuery.Clear(); parameterGrid.Rows.Clear();
        dataGridView1.DataSource = null;
        draft.Accept();
        tabReport.SelectedTab = tabQuery;
    }
    private void GenerateParameters_Click(object? sender, EventArgs e) => UiActions.Run(() =>
    {
        if (AppConfiguration.DesignPreview || operation.IsBusy) return;
        RequireAdmin();
        parameters.SetQuery(ReportQuery.Parse(txtQuery.Text));
        tabReport.SelectedTab = tabParameters;
    });
    private async void RunQuery_Click(object? sender, EventArgs e) => await operation.RunAsync(async () =>
    {
        RequireAdmin();
        var query = ReportQuery.Parse(txtQuery.Text);
        parameters.SetQuery(query);
        var values = parameters.Read();
        var table = await Task.Run(() => repository.Run(query, values));
        var old = dataGridView1.DataSource as System.Data.DataTable;
        dataGridView1.DataSource = table;
        old?.Dispose();
        tabReport.SelectedTab = tabResults;
        lblSubtitle.Text = $"{table.Rows.Count:N0} kayıt getirildi.";
    });
    private void Query_KeyDown(object? sender, KeyEventArgs e)
    {
        if (!e.Control) return;
        if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; RunQuery_Click(sender, e); }
        else if (e.KeyCode == Keys.G) { e.SuppressKeyPress = true; GenerateParameters_Click(sender, e); }
        else if (e.KeyCode == Keys.S) { e.SuppressKeyPress = true; btnRaporKaydet_Click(this, e); }
    }
    private void CloseRecord_Click(object? sender, EventArgs e) => Close();
}