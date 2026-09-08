namespace BKS;
public sealed record InvoiceLine(string Description, decimal Quantity, decimal UnitPrice, decimal VatRate)
{
    public decimal Net => decimal.Round(Quantity * UnitPrice, 2, MidpointRounding.AwayFromZero);
    public decimal Vat => decimal.Round(Net * VatRate / 100, 2, MidpointRounding.AwayFromZero);
    public decimal Total => Net + Vat;
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Description) || Description.Length> 250) throw new InvalidOperationException("Kalem açıklaması 1–250 karakter olmalıdır.");
        if (Quantity <= 0 || UnitPrice<0 || VatRate<0 || VatRate> 100) throw new InvalidOperationException("Miktar pozitif, fiyat negatif olmayan, KDV 0–100 arası olmalıdır.");
    }
}
