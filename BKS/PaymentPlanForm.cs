namespace BKS;
public partial class PaymentPlanForm : Form
{
    internal string AmountText => txtAmount.Text;
    internal int Months => (int)nudMonths.Value;
    internal Action? CreatePlan { get; set; }
    public PaymentPlanForm()
    {
        InitializeComponent();
        Screens.PrepareDesignerForm(this);
    }
    private void Create_Click(object? sender, EventArgs e) => UiActions.Run(() => CreatePlan?.Invoke());
    private void Close_Click(object? sender, EventArgs e) => Close();
}
