using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
namespace BKS;
internal static class InvoicePdf
{
    private static readonly object FontLock = new();
    public static void Write(string path, string no, string title, string tax, DateTime date, IReadOnlyList<InvoiceLine> lines)
    {
        lock (FontLock)
        {
            if (PdfSharpCore.Fonts.GlobalFontSettings.FontResolver is null) PdfSharpCore.Fonts.GlobalFontSettings.FontResolver = new PdfSharpCore.Utils.FontResolver();
        }
        using var doc = new PdfDocument();
        doc.Info.Title = "Fatura kaydı " + no;
        var font = new XFont("Arial", 10, XFontStyle.Regular);
        var bold = new XFont("Arial", 10, XFontStyle.Bold);
        var heading = new XFont("Arial", 20, XFontStyle.Bold);
        PdfPage page = null!;
        XGraphics gfx = null!;
        double y = 0;
        double[] widths =
        {
            215,
            55,
            85,
            60,
            100
        };
        void NewPage()
        {
            gfx?.Dispose();
            page = doc.AddPage();
            page.Size = PdfSharpCore.PageSize.A4;
            gfx = XGraphics.FromPdfPage(page);
            gfx.DrawString("FATURA KAYDI", heading, XBrushes.DarkBlue, new XPoint(40, 48));
            gfx.DrawString(no + "    •    " + date.ToString("dd.MM.yyyy"), font, XBrushes.Black, new XPoint(40, 72));
            new XTextFormatter(gfx).DrawString(title + "\nVKN / TCKN: " + tax, font, XBrushes.Black, new XRect(40, 86, 515,
            44));
            y = 140;
            double x = 40;
            var headers = new[]
            {
                "Açıklama",
                "Miktar",
                "Birim fiyat",
                "KDV %",
                "Toplam"
            };
            for (var i = 0; i<headers.Length; i++)
            {
                gfx.DrawRectangle(XBrushes.LightGray, x, y, widths[i], 25);
                gfx.DrawString(headers[i], bold, XBrushes.Black, new XRect(x + 4, y + 6, widths[i] - 8, 18), XStringFormats.TopLeft);
                x += widths[i];
            }
            y += 30;
            gfx.DrawString("Yerel belge kaydı · GİB onayı veya e-fatura gönderimi içermez.", font, XBrushes.Gray, new XPoint(40,
            page.Height - 34));
            gfx.DrawString("Sayfa " + doc.PageCount, font, XBrushes.Gray, new XPoint(page.Width - 95, page.Height - 34));
        }
        try
        {
            NewPage();
            foreach (var line in lines)
            {
                line.Validate();
                var rowHeight = Math.Max(32, Math.Ceiling(gfx.MeasureString(line.Description, font).Width / 195) * 15 + 18);
                if (y + rowHeight> page.Height - 75) NewPage();
                double x = 40;
                var values = new[]
                {
                    line.Description,
                    line.Quantity.ToString("N2"),
                    line.UnitPrice.ToString("N2"),
                    line.VatRate.ToString("N2"),
                    line.Total.ToString("N2")
                };
                for (var i = 0; i<values.Length; i++)
                {
                    gfx.DrawRectangle(XPens.LightGray, x, y, widths[i], rowHeight);
                    new XTextFormatter(gfx).DrawString(values[i], font, XBrushes.Black, new XRect(x + 5, y + 6, widths[i] - 10, rowHeight - 8));
                    x += widths[i];
                }
                y += rowHeight;
            }
            if (y + 104> page.Height - 75) NewPage();
            y += 22;
            gfx.DrawString("Ara toplam: " + lines.Sum(l => l.Net).ToString("N2") + " TL", bold, XBrushes.Black, new XPoint(340,
            y));
            y += 22;
            gfx.DrawString("KDV: " + lines.Sum(l => l.Vat).ToString("N2") + " TL", bold, XBrushes.Black, new XPoint(340, y));
            y += 24;
            gfx.DrawString("Genel toplam: " + lines.Sum(l => l.Total).ToString("N2") + " TL", bold, XBrushes.DarkBlue, new XPoint(340,
            y));
            gfx.Dispose();
            gfx = null!;
            doc.Save(path);
        }
        finally
        {
            gfx?.Dispose();
        }
    }
}
