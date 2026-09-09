using System.ComponentModel;
using System.Data;
using System.Globalization;
using BKS;

internal static class RegressionChecks
{
    public static void Run(Action<bool, string> check)
    {
        CheckLineBreaks(check);
        CheckTableFilters(check);
        CheckObjectLists(check);
        CheckRecordValues(check);
        CheckDocumentFactories(check);
    }

    private static void CheckLineBreaks(Action<bool, string> check)
    {
        double Measure(string text) => new StringInfo(text).LengthInTextElements;
        var lines = TextLineBreaker.Wrap("Anaokulu\nVKN / TCKN: 1234567890", 80, Measure);
        check(lines.SequenceEqual(new[] { "Anaokulu", "VKN / TCKN: 1234567890" }), "PDF: original newline reproduction preserves both lines");
        check(TextLineBreaker.Wrap("A\r\nB\rC\n\nD", 10, Measure).SequenceEqual(new[] { "A", "B", "C", "", "D" }),
            "PDF: Windows, Unix and blank lines are normalized without null entries");
        check(TextLineBreaker.Wrap("Sınıf kayıt bedeli", 10, Measure).SequenceEqual(new[] { "Sınıf", "kayıt", "bedeli" }),
            "PDF: descriptions wrap at word boundaries");
        string token = string.Concat(Enumerable.Repeat("İe\u0301😀", 20));
        var wrapped = TextLineBreaker.Wrap(token, 7, Measure);
        check(string.Concat(wrapped) == token && wrapped.All(line => Measure(line) <= 7),
            "PDF: long tokens preserve Turkish text, combining characters and surrogate pairs");
        check(TextLineBreaker.Wrap(null, 10, Measure).SequenceEqual(new[] { "" }), "PDF: empty text never produces a null line");
    }

    private static void CheckTableFilters(Action<bool, string> check)
    {
        var data = new DataTable { Locale = CultureInfo.GetCultureInfo("tr-TR") };
        data.Columns.Add("Ad]\\Soyad");
        data.Columns.Add("Sınıf");
        data.Columns.Add("Tutar", typeof(decimal));
        data.Columns.Add("Aktif", typeof(bool));
        data.Rows.Add("O'Neil [1] % *", "Mavi", 1250.55M, true);
        data.Rows.Add("O'Neil [1] % *", "Sarı", 100M, true);
        data.Rows.Add("O'Neil [1] % *", "Mavi", 100M, false);
        var columns = data.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
        foreach (var search in new[] { "O'Neil", "[1]", "%", "*" })
        {
            data.DefaultView.RowFilter = TableFilterExpression.Build(columns, search,
                new Dictionary<string, string> { ["Sınıf"] = "Mavi" }, "Aktif = true");
            check(data.DefaultView.Count == 1, "Grid: combined filters and special column/value characters: " + search);
        }
        data.DefaultView.RowFilter = TableFilterExpression.Build(columns, "", new Dictionary<string, string>(), "Aktif = true");
        check(data.DefaultView.Count == 2, "Grid: clearing user filters retains the underlying data restriction");
    }

    private static void CheckObjectLists(Action<bool, string> check)
    {
        var original = new[] { new FileItem("Z.pdf", "PDF"), new FileItem("A.pdf", "PDF"), new FileItem("B.png", "Resim") };
        var list = new GridItems<FileItem>(original);
        list.ApplyFilters(".pdf", new Dictionary<string, string> { ["Type"] = "PDF" });
        ((IBindingList)list).ApplySort(TypeDescriptor.GetProperties(typeof(FileItem))["Name"]!, ListSortDirection.Ascending);
        check(list.Count == 2 && ReferenceEquals(list[0], original[1]), "Archive: search and sort preserve the selected model identity");
        list.ApplyFilters("", new Dictionary<string, string>());
        check(list.Count == 3 && list[0].Name == "A.pdf", "Archive: clearing filters restores all rows and retains sorting");
    }

    private static void CheckRecordValues(Action<bool, string> check)
    {
        var id = Guid.NewGuid();
        var record = new RecordValues(new Dictionary<string, object?>
        {
            ["Id"] = id.ToString(), ["Date"] = DBNull.Value, ["BadDate"] = "invalid",
            ["Active"] = "Evet", ["Amount"] = 1500.55M
        });
        var fallback = new DateTime(2024, 1, 1);
        check(record.RequiredId("Id") == id && record.Get<bool>("Active"), "Card: typed IDs and legacy boolean values can be read");
        check(record.Get("Date", fallback) == fallback && record.Get("BadDate", fallback) == fallback
            && record.Get("Missing", "") == "", "Card: absent fields and invalid dates do not crash form population");
    }

    private static void CheckDocumentFactories(Action<bool, string> check)
    {
        var registry = new DocumentRegistry<object>();
        bool rejected = false;
        try { registry.GetOrCreate("recursive", () => registry.GetOrCreate("recursive", () => new object())); }
        catch (InvalidOperationException) { rejected = true; }
        check(rejected && !registry.TryGet("recursive", out _), "Document: reentrant opening cannot create duplicate tabs");
        rejected = false;
        try { registry.GetOrCreate("null", () => null!); }
        catch (InvalidOperationException) { rejected = true; }
        check(rejected && !registry.TryGet("null", out _), "Document: a null factory result leaves no stale entry");
    }

    private sealed record FileItem(string Name, string Type);
}
