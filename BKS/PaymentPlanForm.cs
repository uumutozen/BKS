namespace BKS;
public partial class PaymentPlanForm : Form
{
    private readonly PaymentRepository? repository;
    private readonly Guid student;
    private readonly Func<bool> canAccess;
    private readonly FormOperation operation;
    public PaymentPlanForm() : this(null, Guid.Empty, () => false) { }
    internal PaymentPlanForm(PaymentRepository? repository, Guid student, Func<bool> canAccess)
    {
        this.repository = repository; this.student = student; this.canAccess = canAccess;
        InitializeComponent();
        operation = new FormOperation(this);
        Screens.PrepareDesignerForm(this);
        GridAppearance.Apply(gridPreview);
        gridPreview.DataBindingComplete += (_, _) =>
        {
            gridPreview.RowHeadersVisible = false;
            gridPreview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        };
        cmbFrequency.SelectedIndex = 0;
        UpdatePreview();
    }
    private IReadOnlyList<ScheduledPayment> ReadPlan() => PaymentSchedule.Create(dtStart.Value,
        (int)nudPeriods.Value, nudAmount.Value, cmbFrequency.SelectedIndex == 1 ? PaymentFrequency.Yearly : PaymentFrequency.Monthly);
    private void PlanChanged(object? sender, EventArgs e) => UpdatePreview();
    private void FrequencyChanged(object? sender, EventArgs e)
    {
        nudPeriods.Value = cmbFrequency.SelectedIndex == 1 ? 1 : 12;
        UpdatePreview();
    }
    private void UpdatePreview()
    {
        try
        {
            var plan = ReadPlan();
            gridPreview.DataSource = plan.Select((payment, i) => new { Dönem = i + 1, Tarih = payment.DueDate, Tutar = payment.Amount }).ToList();
            lblTotal.Text = $"{plan.Count} dönem    •    Toplam: {plan.Sum(p => p.Amount):N2} TL    •    Son vade: {plan[^1].DueDate:dd.MM.yyyy}";
            btnCreate.Enabled = repository != null && canAccess() && !AppConfiguration.DesignPreview;
        }
        catch (InvalidOperationException ex) { gridPreview.DataSource = null; lblTotal.Text = ex.Message; btnCreate.Enabled = false; }
    }
    private async void Create_Click(object? sender, EventArgs e)
    {
        bool saved = false;
        await operation.RunAsync(async () =>
        {
            if (repository == null || !canAccess()) throw new InvalidOperationException("Ödeme planı için erişim yetkisi bulunamadı.");
            var plan = ReadPlan();
            if (MessageBox.Show($"{plan.Count} yeni ödeme, toplam {plan.Sum(p => p.Amount):N2} TL oluşturulsun mu?\nMevcut ödemelere eklenir.",
                "Planı kaydet", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            await Task.Run(() => repository.Create(student, plan));
            saved = true;
        });
        if (saved && !IsDisposed) { DialogResult = DialogResult.OK; Close(); }
    }
    private void Close_Click(object? sender, EventArgs e) => Close();
}
