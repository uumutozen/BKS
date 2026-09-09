namespace BKS;

public partial class PaymentDetailsForm : Form
{
    internal DataGridView PaymentGrid => paymentGrid;
    internal Action<string, DateTime, DataGridView>? AddPayment { get; set; }
    internal Action<DataGridView>? ApprovePayment { get; set; }
    internal Action<DataGridView>? OpenPlan { get; set; }
    internal Action<DataGridView>? RefreshPayments { get; set; }
    public PaymentDetailsForm()
    {
        InitializeComponent();
        Screens.PrepareDesignerForm(this);
        DesignerListBinding.Attach(paymentGrid, txtSearch, pnlPaymentsClear, pnlPaymentsColumns, pnlPaymentsCount);
        paymentGrid.DataBindingComplete += (_, _) =>
        {
            foreach (var name in new[] { "Id", "IsApproved" })
                if (paymentGrid.Columns.Contains(name)) paymentGrid.Columns[name].Visible = false;
        };
    }
    private void Add_Click(object? sender, EventArgs e) => UiActions.Run(() => AddPayment?.Invoke(txtAmount.Text, dtPayment.Value, paymentGrid));
    private void Approve_Click(object? sender, EventArgs e) => UiActions.Run(() => ApprovePayment?.Invoke(paymentGrid));
    private void Plan_Click(object? sender, EventArgs e) => UiActions.Run(() => OpenPlan?.Invoke(paymentGrid));
    private void Refresh_Click(object? sender, EventArgs e) => UiActions.Run(() => RefreshPayments?.Invoke(paymentGrid));
    private void Close_Click(object? sender, EventArgs e) => Close();
}
