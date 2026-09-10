using System.Text.RegularExpressions;
namespace BKS;

public sealed record InvoiceDraft(string Prefix, string Recipient, string TaxNumber, DateTime Date, IReadOnlyList<InvoiceLine> Lines)
{
    public void Validate()
    {
        if (!Regex.IsMatch(Prefix, @"^[A-Z0-9]{1,12}$")) throw new InvalidOperationException("Fatura öneki 1–12 harf/rakam olmalıdır.");
        if (string.IsNullOrWhiteSpace(Recipient) || Recipient.Length > 500) throw new InvalidOperationException("Alıcı unvanı 1–500 karakter olmalıdır.");
        if (!Regex.IsMatch(TaxNumber, @"^(\d{10}|\d{11})$")) throw new InvalidOperationException("VKN / TCKN 10 veya 11 rakam olmalıdır.");
        if (Lines.Count == 0) throw new InvalidOperationException("En az bir fatura kalemi ekleyin.");
        foreach (var line in Lines) line.Validate();
    }
    public string Number(int previous)
    {
        if (previous is < 0 or >= 99999) throw new InvalidOperationException("Bu önek için numara sınırına ulaşıldı; başka bir önek seçin.");
        return Prefix + Date.ToString("yy", System.Globalization.CultureInfo.InvariantCulture) + (previous + 1).ToString("D5");
    }
}
