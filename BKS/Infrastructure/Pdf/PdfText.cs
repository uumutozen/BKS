using PdfSharpCore.Drawing;

namespace BKS;

/// <summary>PDF'ye her satırı ayrı çizer; XTextFormatter'ın satır sonu hatasına girmez.</summary>
internal static class PdfText
{
    public static IReadOnlyList<string> Wrap(XGraphics graphics, string? text, XFont font, double width)
    {
        ArgumentNullException.ThrowIfNull(graphics);
        ArgumentNullException.ThrowIfNull(font);
        return TextLineBreaker.Wrap(text, width, value => graphics.MeasureString(value, font).Width);
    }

    public static void DrawLines(XGraphics graphics, IEnumerable<string> lines, XFont font,
        XBrush brush, double x, double y, double lineHeight)
    {
        foreach (var line in lines)
        {
            if (!string.IsNullOrEmpty(line))
                graphics.DrawString(line, font, brush, new XPoint(x, y), XStringFormats.TopLeft);
            y += lineHeight;
        }
    }
}
