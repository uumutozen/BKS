using System.IO.Compression;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;
namespace BKS;
/// <summary>Read-only OOXML import. No Excel automation or commercial UI/Excel package is required.</summary>
internal sealed class XlsxWorkbook : IDisposable
{
    public XlsxSheet FirstSheet
    {
        get;
    }
    public XlsxWorkbook(FileInfo file)
    {
        using var zip = ZipFile.OpenRead(file.FullName);
        XDocument Read(string name)
        {
            var entry = zip.GetEntry(name) ?? throw new InvalidDataException("Excel bileşeni eksik: " + name);
            if (entry.Length> 32 * 1024 * 1024) throw new InvalidDataException("Excel sayfası bu içe aktarma için çok büyük.");
            using var stream = entry.Open();
            using var reader = XmlReader.Create(stream, new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Prohibit,
                XmlResolver = null,
                MaxCharactersInDocument = 32 * 1024 * 1024
            });
            return XDocument.Load(reader);
        }
        XNamespace ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
        XNamespace rel = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
        var workbook = Read("xl/workbook.xml");
        var first = workbook.Descendants(ns + "sheet").FirstOrDefault() ?? throw new InvalidDataException("Excel dosyasında sayfa yok.");
        var id = (string?) first.Attribute(rel + "id");
        var relationship = Read("xl/_rels/workbook.xml.rels").Root!.Elements().FirstOrDefault(x => (string?) x.Attribute("Id") == id) ?? throw new InvalidDataException("Excel sayfa başvurusu eksik.");
        if ((string?) relationship.Attribute("TargetMode") == "External") throw new InvalidDataException("Harici sayfa başvurusu desteklenmiyor.");
        var target = (string?) relationship.Attribute("Target") ?? "";
        var entryPath = new Uri(new Uri("https://workbook.local/xl/"), target).AbsolutePath.TrimStart('/');
        if (!entryPath.StartsWith("xl/", StringComparison.Ordinal)) throw new InvalidDataException("Excel sayfa yolu geçersiz.");
        var strings = zip.GetEntry("xl/sharedStrings.xml") is null ? Array.Empty<string>(): Read("xl/sharedStrings.xml").Descendants(ns + "si").Select(si => string.Concat(si.Descendants(ns + "t").Select(t => t.Value))).ToArray();
        var dateStyles = new HashSet<int>();
        if (zip.GetEntry("xl/styles.xml") is not null)
        {
            var styles = Read("xl/styles.xml");
            var custom = styles.Descendants(ns + "numFmt").ToDictionary(x => (int) x.Attribute("numFmtId")!, x => (string?) x.Attribute("formatCode") ?? "");
            var i = 0;
            foreach (var xf in styles.Root?.Element(ns + "cellXfs")?.Elements(ns + "xf") ?? Enumerable.Empty<XElement>())
            {
                int fmt = (int?) xf.Attribute("numFmtId") ?? 0;
                if (fmt is >= 14 and <= 22 or >= 45 and <= 47 || custom.TryGetValue(fmt, out var code) && System.Text.RegularExpressions.Regex.IsMatch(code,
                @"(?i)[dy]")) dateStyles.Add(i);
                i++;
            }
        }
        bool date1904 = (string?) workbook.Root?.Element(ns + "workbookPr")?.Attribute("date1904") is "1" or "true";
        var cells = new Dictionary<(int, int), XlsxCell>();
        foreach (var cell in Read(entryPath).Descendants(ns + "c"))
        {
            var reference = (string?) cell.Attribute("r") ?? "";
            int column = 0, index = 0;
            while (index<reference.Length && char.IsLetter(reference[index]))
            {
                column = column * 26 + char.ToUpperInvariant(reference[index]) - 64;
                index++;
            }
            if (!int.TryParse(reference[index ..], out var row) || column<1 || row> 100000) throw new InvalidDataException("Excel satır/sütun sınırı aşıldı veya adres geçersiz.");
            var type = (string?) cell.Attribute("t");
            var raw = cell.Element(ns + "v")?.Value ?? "";
            object value = raw;
            if (type == "s")
            {
                if (!int.TryParse(raw, out var stringIndex) || stringIndex<0 || stringIndex >= strings.Length) throw new InvalidDataException("Excel metin başvurusu geçersiz.");
                value = strings[stringIndex];
            }
            else if (type == "inlineStr") value = string.Concat(cell.Descendants(ns + "t").Select(t => t.Value));
            else if (type == "b") value = raw == "1" ? "1": "0";
            else if (type == "e") throw new InvalidDataException("Excel hücresinde hata var: " + reference);
            else if (double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out var number)) value = dateStyles.Contains((int?) cell.Attribute("s") ?? 0) ? DateTime.FromOADate(number + (date1904 ? 1462: 0)): number;
            if (cell.Element(ns + "f") is not null && cell.Element(ns + "v") is null) throw new InvalidDataException("Formül sonucu kaydedilmemiş. Excel dosyasını hesaplatıp kaydedin: " + reference);
            cells[(row, column)] = new XlsxCell(value);
        }
        FirstSheet = new XlsxSheet(cells);
    }
    public void Dispose()
    {
    }
}
internal sealed class XlsxSheet
{
    public XlsxCells Cells
    {
        get;
    }
    public XlsxSheet(Dictionary<(int, int), XlsxCell> cells)
    {
        Cells = new XlsxCells(cells);
    }
}
internal sealed class XlsxCells(Dictionary<(int, int), XlsxCell> cells)
{
    public XlsxCell this[int row, int column] => cells.TryGetValue((row, column), out var cell) ? cell: new XlsxCell("");
}
internal sealed record XlsxCell(object Value)
{
    public string Text => Value is DateTime date ? date.ToString("dd.MM.yyyy", CultureInfo.GetCultureInfo("tr-TR")): Convert.ToString(Value,
    CultureInfo.GetCultureInfo("tr-TR")) ?? "";
}
