using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
namespace BKS;

public sealed record ReportQuery(string Sql, IReadOnlyList<string> Parameters)
{
    public static ReportQuery Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) throw new InvalidOperationException("Rapor sorgusu boş olamaz.");
        var sql = new StringBuilder();
        var code = new StringBuilder();
        var names = new List<string>();
        for (int i = 0; i < input.Length;)
        {
            int start = i;
            char c = input[i];
            if (c is '\'' or '"' or '[')
            {
                char end = c == '[' ? ']' : c;
                i++;
                bool closed = false;
                while (i < input.Length)
                {
                    if (input[i++] != end) continue;
                    if (i < input.Length && input[i] == end) { i++; continue; }
                    closed = true; break;
                }
                if (!closed) throw new InvalidOperationException("Sorguda kapanmamış tırnak veya köşeli parantez var.");
                sql.Append(input[start..i]); code.Append(' ', i - start); continue;
            }
            if (c == '-' && i + 1 < input.Length && input[i + 1] == '-')
            {
                while (i < input.Length && input[i] != '\n') i++;
                sql.Append(input[start..i]); code.Append(' ', i - start); continue;
            }
            if (c == '/' && i + 1 < input.Length && input[i + 1] == '*')
            {
                i += 2; int depth = 1;
                while (i < input.Length && depth > 0)
                {
                    if (i + 1 < input.Length && input[i] == '/' && input[i + 1] == '*') { depth++; i += 2; }
                    else if (i + 1 < input.Length && input[i] == '*' && input[i + 1] == '/') { depth--; i += 2; }
                    else i++;
                }
                if (depth != 0) throw new InvalidOperationException("Sorguda kapanmamış açıklama var.");
                sql.Append(input[start..i]); code.Append(' ', i - start); continue;
            }
            if (c == '@' && i + 1 < input.Length && input[i + 1] == '@')
            {
                sql.Append("@@"); code.Append("  "); i += 2;
                while (i < input.Length && (char.IsLetterOrDigit(input[i]) || input[i] == '_')) { sql.Append(input[i++]); code.Append(' '); }
                continue;
            }
            if ((c == ':' || c == '@') && i + 1 < input.Length && (char.IsLetter(input[i + 1]) || input[i + 1] == '_'))
            {
                i += 2;
                while (i < input.Length && (char.IsLetterOrDigit(input[i]) || input[i] == '_')) i++;
                var name = input[(start + 1)..i];
                var canonical = names.FirstOrDefault(n => n.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (canonical == null) { canonical = name; names.Add(name); }
                sql.Append('@').Append(canonical); code.Append(' ', i - start); continue;
            }
            sql.Append(c); code.Append(c); i++;
        }
        var tokens = Regex.Matches(code.ToString(), @"\b[A-Za-z_]+\b").Select(m => m.Value.ToUpperInvariant()).ToArray();
        if (tokens.Length == 0 || tokens[0] is not ("SELECT" or "WITH") ||
            tokens.Any(t => new[] { "INSERT", "UPDATE", "DELETE", "MERGE", "DROP", "ALTER", "CREATE", "EXEC", "EXECUTE", "TRUNCATE", "INTO", "GRANT", "REVOKE", "DENY", "BACKUP", "RESTORE", "DBCC", "USE", "SET", "OPENROWSET", "OPENDATASOURCE" }.Contains(t)))
            throw new InvalidOperationException("Rapor için yalnızca veri okuyan SELECT / WITH sorguları kullanın.");
        return new ReportQuery(sql.ToString(), names);
    }
    public static object Value(string type, string text, bool isNull)
    {
        if (isNull) return DBNull.Value;
        var culture = CultureInfo.GetCultureInfo("tr-TR");
        return type switch
        {
            "Metin" => text,
            "Tam sayı" when long.TryParse(text, NumberStyles.Integer, culture, out var number) => number,
            "Tutar" when decimal.TryParse(text, NumberStyles.Number, culture, out var amount) => amount,
            "Tarih" when DateTime.TryParse(text, culture, DateTimeStyles.None, out var date) => date,
            "Evet / Hayır" when new[] { "evet", "true", "1", "hayır", "false", "0" }.Contains(text.Trim().ToLower(culture)) => new[] { "evet", "true", "1" }.Contains(text.Trim().ToLower(culture)),
            "GUID" when Guid.TryParse(text, out var guid) => guid,
            _ => throw new InvalidOperationException($"'{text}' değeri {type} türüne uygun değil.")
        };
    }
}
