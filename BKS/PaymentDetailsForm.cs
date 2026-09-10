using System.Data;
namespace BKS;
public partial class PaymentDetailsForm : Form
{
    private readonly PaymentRepository? repository;
    private readonly Guid student;
    private readonly Func<bool> canAccess;
    private readonly FormOperation operation;
    public PaymentDetailsForm() : this(null, Guid.Empty, () => false) { }
    internal PaymentDetailsForm(PaymentRepository? repository, Guid student, Func<bool> canAccess)
    {
        this.repository = repository; this.student = student; this.canAccess = canAccess;
        InitializeComponent();
        operation = new FormOperation(this);
        Screens.PrepareDesignerForm(this);
        DesignerListBinding.Attach(paymentGrid, txtSearch, pnlPaymentsClear, pnlPaymentsColumns, pnlPaymentsCount);
        paymentGrid.SelectionChanged += (_, _) => UpdateActions();
        paymentGrid.DataBindingComplete += (_, _) =>
        {
            if (paymentGrid.Columns.Contains("IsApproved")) paymentGrid.Columns["IsApproved"].Visible = false;
            UpdateActions();
        };
        UpdateActions();
    }
    private void RequireAccess()
    {
        if (repository == null || !canAccess()) throw new InvalidOperationException("Ödemeler için erişim yetkisi bulunamadı.");
    }
    public Task ReloadAsync() => operation.RunAsync(LoadRowsAsync);
    private async Task LoadRowsAsync()
    {
        RequireAccess();
        var table = await Task.Run(() => repository!.Read(student));
        if (IsDisposed) { table.Dispose(); return; }
        var old = paymentGrid.DataSource as DataTable;
        paymentGrid.DataSource = table;
        old?.Dispose();
        var rows = table.AsEnumerable().ToArray();
        decimal paid = rows.Where(r => DataValues.Boolean(r["IsApproved"])).Sum(r => DataValues.Money(r["Tutar"]));
        decimal pending = rows.Where(r => !DataValues.Boolean(r["IsApproved"])).Sum(r => DataValues.Money(r["Tutar"]));
        var name = rows.Length > 0 ? Convert.ToString(rows[0]["Öğrenci"]) : "Öğrenci";
        lblTitle.Text = name + " · Ödemeler";
        lblSubtitle.Text = $"Bekleyen: {pending:N2} TL    •    Onaylanan: {paid:N2} TL    •    {rows.Length} ödeme";
    }
    private DataGridViewRow[] PendingSelection() => paymentGrid.SelectedRows.Cast<DataGridViewRow>()
        .Where(row => !row.IsNewRow && row.DataBoundItem is DataRowView view && !DataValues.Boolean(view["IsApproved"])).ToArray();
    private void UpdateActions()
    {
        bool allowed = repository != null && canAccess() && !AppConfiguration.DesignPreview;
        btnAdd.Enabled = btnPlan.Enabled = btnRefresh.Enabled = allowed;
        btnApprove.Enabled = allowed && PendingSelection().Length > 0;
    }
    private async void Add_Click(object? sender, EventArgs e) => await operation.RunAsync(async () =>
    {
        RequireAccess();
        var amount = DataValues.Money(txtAmount.Text);
        PaymentSchedule.ValidateAmount(amount);
        var date = dtPayment.Value.Date;
        await Task.Run(() => repository!.Create(student, new[] { new ScheduledPayment(date, amount) }));
        txtAmount.Clear();
        lblSubtitle.Text = "Ödeme kaydedildi.";
        await LoadRowsAsync();
    });
    private async void Approve_Click(object? sender, EventArgs e) => await operation.RunAsync(async () =>
    {
        RequireAccess();
        var selected = PendingSelection();
        if (selected.Length == 0) throw new InvalidOperationException("Bekleyen ödemeleri seçin.");
        var ids = selected.Select(row => (Guid)((DataRowView)row.DataBoundItem)["Id"]).ToArray();
        var total = selected.Sum(row => DataValues.Money(((DataRowView)row.DataBoundItem)["Tutar"]));
        if (MessageBox.Show($"{ids.Length} ödeme, toplam {total:N2} TL ödendi olarak onaylansın mı?",
            "Ödeme onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        await Task.Run(() => repository!.Approve(student, ids));
        lblSubtitle.Text = "Seçili ödemeler onaylandı.";
        await LoadRowsAsync();
    });
    private async void Plan_Click(object? sender, EventArgs e)
    {
        if (operation.IsBusy) return;
        try
        {
            RequireAccess();
            using var plan = new PaymentPlanForm(repository!, student, canAccess);
            if (plan.ShowDialog(this) == DialogResult.OK) await ReloadAsync();
        }
        catch (Exception ex) { UiActions.ShowError(ex); }
    }
    private async void Refresh_Click(object? sender, EventArgs e) => await ReloadAsync();
    private void Close_Click(object? sender, EventArgs e) => Close();
}