using System.Globalization;
namespace BKS;
public static class DataValues
{
    public static bool Boolean(object? value) => value is bool flag ? flag: value is not null && value != DBNull.Value && new[]
    {
        "1",
        "true",
        "evet"
    }.Contains(Convert.ToString(value)?.Trim().ToLowerInvariant());
    public static string EscapeLike(string value) => string.Concat(value.Select(c => c switch
    {
        '\'' => "''",
        '[' => "[[]",
        ']' => "[]]",
        '%' => "[%]",
        '*' => "[*]",
        _ => c.ToString()
    }));
    public static decimal Money(object? value)
    {
        if (value is decimal money) return money;
        if (value is double number) return(decimal) number;
        if (decimal.TryParse(Convert.ToString(value), NumberStyles.Number, CultureInfo.GetCultureInfo("tr-TR"), out var result)) return result;
        throw new FormatException("Geçerli bir tutar girin (örnek: 1250,50).");
    }
}
