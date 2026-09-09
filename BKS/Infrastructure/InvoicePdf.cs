using PdfSharpCore.Fonts;

namespace BKS;

/// <summary>Fatura PDF üretiminin giriş noktası: doğrulama ve font hazırlığı.</summary>
internal static class InvoicePdf
{
    private static readonly object FontLock = new();

    public static void Write(string path, string number, string title, string tax,
        DateTime date, IReadOnlyList<InvoiceLine> lines)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        ArgumentException.ThrowIfNullOrWhiteSpace(number);
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(tax);
        ArgumentNullException.ThrowIfNull(lines);
        if (lines.Count == 0) throw new InvalidOperationException("En az bir fatura kalemi girin.");
        if (title.Length > 500) throw new InvalidOperationException("Alıcı unvanı en fazla 500 karakter olabilir.");
        foreach (var line in lines)
        {
            if (line is null) throw new InvalidOperationException("Fatura kalemi boş olamaz.");
            line.Validate();
        }

        lock (FontLock)
        {
            GlobalFontSettings.FontResolver ??= new PdfSharpCore.Utils.FontResolver();
        }

        using var renderer = new InvoicePdfRenderer(number, title.Trim(), tax.Trim(), date);
        renderer.Write(path, lines);
    }
}
