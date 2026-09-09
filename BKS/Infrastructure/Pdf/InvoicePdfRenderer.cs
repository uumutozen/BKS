using System.Globalization;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace BKS;

/// <summary>Başlık, tablo, sayfa geçişi ve toplamların yerleşimi. Veritabanına erişmez.</summary>
internal sealed class InvoicePdfRenderer : IDisposable
{
    private const double Left = 40;
    private const double LineHeight = 14;
    private const double CellPadding = 6;
    private const double MinimumRowHeight = 32;
    private static readonly double[] ColumnWidths = { 215, 55, 85, 60, 100 };
    private static readonly string[] ColumnTitles = { "Açıklama", "Miktar", "Birim fiyat", "KDV %", "Toplam" };
    private static readonly CultureInfo Turkish = CultureInfo.GetCultureInfo("tr-TR");

    private readonly PdfDocument document = new();
    private readonly XFont normal = new("Arial", 10, XFontStyle.Regular);
    private readonly XFont bold = new("Arial", 10, XFontStyle.Bold);
    private readonly XFont heading = new("Arial", 20, XFontStyle.Bold);
    private readonly string number;
    private readonly string title;
    private readonly string tax;
    private readonly DateTime date;
    private XGraphics? graphics;
    private double pageHeight;
    private double y;
    private double TableBottom => pageHeight - 75;
    private XGraphics Graphics => graphics ?? throw new InvalidOperationException("PDF sayfası hazırlanmadı.");

    public InvoicePdfRenderer(string number, string title, string tax, DateTime date)
    {
        this.number = number;
        this.title = title;
        this.tax = tax;
        this.date = date;
        document.Info.Title = "Fatura kaydı " + number;
    }

    public void Write(string path, IReadOnlyList<InvoiceLine> lines)
    {
        NewPage();
        foreach (var line in lines) DrawRow(line);
        DrawTotals(lines);
        graphics?.Dispose();
        graphics = null;
        document.Save(path);
    }

    private void NewPage()
    {
        graphics?.Dispose();
        var page = document.AddPage();
        page.Size = PdfSharpCore.PageSize.A4;
        pageHeight = page.Height.Point;
        graphics = XGraphics.FromPdfPage(page);

        Graphics.DrawString("FATURA KAYDI", heading, XBrushes.DarkBlue, new XPoint(Left, 48));
        Graphics.DrawString(number + "  /  " + date.ToString("dd.MM.yyyy", Turkish), normal,
            XBrushes.Black, new XPoint(Left, 72));

        var recipient = PdfText.Wrap(Graphics, title, normal, 515)
            .Concat(PdfText.Wrap(Graphics, "VKN / TCKN: " + tax, normal, 515)).ToArray();
        PdfText.DrawLines(Graphics, recipient, normal, XBrushes.Black, Left, 86, LineHeight);
        y = Math.Max(140, 86 + recipient.Length * LineHeight + 16);
        if (y + 60 >= TableBottom)
            throw new InvalidOperationException("Alıcı bilgisi sayfaya sığmıyor. Gereksiz satır sonlarını kaldırın.");

        double x = Left;
        for (int i = 0; i < ColumnWidths.Length; i++)
        {
            Graphics.DrawRectangle(XBrushes.LightGray, x, y, ColumnWidths[i], 25);
            Graphics.DrawString(ColumnTitles[i], bold, XBrushes.Black,
                new XPoint(x + 4, y + 6), XStringFormats.TopLeft);
            x += ColumnWidths[i];
        }
        y += 30;
        Graphics.DrawString("Yerel belge kaydı · GİB onayı veya e-fatura gönderimi içermez.", normal,
            XBrushes.Gray, new XPoint(Left, pageHeight - 34));
        Graphics.DrawString("Sayfa " + document.PageCount, normal, XBrushes.Gray,
            new XPoint(page.Width.Point - 95, pageHeight - 34));
    }

    private void DrawRow(InvoiceLine line)
    {
        var values = new[]
        {
            line.Description,
            line.Quantity.ToString("N2", Turkish),
            line.UnitPrice.ToString("N2", Turkish),
            line.VatRate.ToString("N2", Turkish),
            line.Total.ToString("N2", Turkish)
        };
        var cells = values.Select((value, index) =>
            PdfText.Wrap(Graphics, value, normal, ColumnWidths[index] - CellPadding * 2)).ToArray();
        int lineCount = cells.Max(cell => cell.Count);
        int offset = 0;

        while (offset < lineCount)
        {
            if (TableBottom - y < MinimumRowHeight) NewPage();
            int capacity = (int)Math.Floor((TableBottom - y - CellPadding * 2) / LineHeight);
            if (capacity < 1) { NewPage(); continue; }
            int count = Math.Min(capacity, lineCount - offset);
            double height = Math.Max(MinimumRowHeight, count * LineHeight + CellPadding * 2);
            double x = Left;

            for (int column = 0; column < cells.Length; column++)
            {
                Graphics.DrawRectangle(XPens.LightGray, x, y, ColumnWidths[column], height);
                PdfText.DrawLines(Graphics, cells[column].Skip(offset).Take(count), normal,
                    XBrushes.Black, x + CellPadding, y + CellPadding, LineHeight);
                x += ColumnWidths[column];
            }
            offset += count;
            y += height;
            if (offset < lineCount) NewPage();
        }
    }

    private void DrawTotals(IReadOnlyList<InvoiceLine> lines)
    {
        if (y + 104 > TableBottom) NewPage();
        y += 22;
        DrawTotal("Ara toplam", lines.Sum(line => line.Net), XBrushes.Black);
        DrawTotal("KDV", lines.Sum(line => line.Vat), XBrushes.Black);
        DrawTotal("Genel toplam", lines.Sum(line => line.Total), XBrushes.DarkBlue);
    }

    private void DrawTotal(string label, decimal amount, XBrush brush)
    {
        string text = label + ": " + amount.ToString("N2", Turkish) + " TL";
        double width = Graphics.MeasureString(text, bold).Width;
        Graphics.DrawString(text, bold, brush, new XPoint(Math.Max(Left, Left + 515 - width), y));
        y += 24;
    }

    public void Dispose()
    {
        graphics?.Dispose();
        document.Dispose();
    }
}
