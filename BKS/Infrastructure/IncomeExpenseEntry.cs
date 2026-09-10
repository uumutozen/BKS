namespace BKS;
public sealed record IncomeExpenseEntry(string Description, decimal Amount, string Type)
{
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Description)) throw new InvalidOperationException("Gelir / gider açıklamasını girin.");
        PaymentSchedule.ValidateAmount(Amount);
        if (Type is not ("G" or "D")) throw new InvalidOperationException("Gelir veya gider türünü seçin.");
    }
}
