using System.Globalization;
using System.Text.RegularExpressions;

namespace BKS;

/// <summary>Ölçüm yönteminden bağımsız satır bölme. PDF, çıktı ve test kodu aynı kuralı kullanır.</summary>
public static class TextLineBreaker
{
    public static IReadOnlyList<string> Wrap(string? text, double width, Func<string, double> measure)
    {
        ArgumentNullException.ThrowIfNull(measure);
        if (!double.IsFinite(width) || width <= 0)
            throw new ArgumentOutOfRangeException(nameof(width));

        var result = new List<string>();
        var normalized = (text ?? string.Empty).Replace("\r\n", "\n").Replace('\r', '\n');
        foreach (var paragraph in normalized.Split('\n'))
        {
            var words = Regex.Split(paragraph.Trim(), @"\s+").Where(word => word.Length > 0);
            var line = string.Empty;

            foreach (var word in words)
            {
                var candidate = line.Length == 0 ? word : line + " " + word;
                if (measure(candidate) <= width)
                {
                    line = candidate;
                    continue;
                }

                if (line.Length > 0)
                {
                    result.Add(line);
                    line = string.Empty;
                }

                // Boşluksuz uzun kodlar ve Unicode karakterler de veri kaybı olmadan bölünür.
                var elements = StringInfo.GetTextElementEnumerator(word);
                while (elements.MoveNext())
                {
                    var element = elements.GetTextElement();
                    if (line.Length > 0 && measure(line + element) > width)
                    {
                        result.Add(line);
                        line = string.Empty;
                    }
                    line += element;
                }
            }
            result.Add(line);
        }
        return result;
    }
}
