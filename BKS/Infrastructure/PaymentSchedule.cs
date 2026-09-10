namespace BKS;

public enum PaymentFrequency { Monthly = 1, Yearly = 12 }
public sealed record ScheduledPayment(DateTime DueDate, decimal Amount);

public static class PaymentSchedule
{
    public static IReadOnlyList<ScheduledPayment> Create(DateTime start, int count, decimal amount, PaymentFrequency frequency)
    {
        if (count is < 1 or > 120) throw new InvalidOperationException("Dönem sayısı 1–120 arasında olmalıdır.");
        if (frequency is not PaymentFrequency.Monthly and not PaymentFrequency.Yearly)
            throw new InvalidOperationException("Aylık veya yıllık dönem seçin.");
        ValidateAmount(amount);
        try
        {
            return Enumerable.Range(0, count)
                .Select(i => new ScheduledPayment(start.Date.AddMonths(checked(i * (int)frequency)), amount)).ToArray();
        }
        catch (ArgumentOutOfRangeException) { throw new InvalidOperationException("Planın bitiş tarihi desteklenen tarih aralığını aşıyor."); }
    }
    public static void ValidateAmount(decimal amount)
    {
        if (amount <= 0 || amount > 999999999M || decimal.Round(amount, 2) != amount)
            throw new InvalidOperationException("Tutar sıfırdan büyük, en fazla 999.999.999 ve en çok iki ondalık haneli olmalıdır.");
    }
}
