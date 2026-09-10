using System.Data;
using BKS;

internal static class WorkflowChecks
{
    public static void Run(Action<bool, string> check)
    {
        bool Rejects(Action action)
        {
            try { action(); return false; }
            catch (InvalidOperationException) { return true; }
        }
        var monthly = PaymentSchedule.Create(new DateTime(2024, 1, 31), 3, 1250.55M, PaymentFrequency.Monthly);
        check(monthly.Select(p => p.DueDate).SequenceEqual(new[] { new DateTime(2024, 1, 31), new DateTime(2024, 2, 29), new DateTime(2024, 3, 31) }),
            "Payment plan: month-end dates do not drift after February");
        var yearly = PaymentSchedule.Create(new DateTime(2024, 2, 29), 3, 15000M, PaymentFrequency.Yearly);
        check(yearly[1].DueDate == new DateTime(2025, 2, 28) && yearly.Sum(p => p.Amount) == 45000M,
            "Payment plan: yearly intervals and total are exact");
        check(Rejects(() => PaymentSchedule.ValidateAmount(-1)) && Rejects(() => PaymentSchedule.ValidateAmount(0))
            && Rejects(() => PaymentSchedule.ValidateAmount(1.001M)), "Payment: negative, zero and fractional cents are rejected");
        check(Rejects(() => PaymentSchedule.Create(DateTime.Today, 0, 10, PaymentFrequency.Monthly))
            && Rejects(() => PaymentSchedule.Create(DateTime.MaxValue.Date, 2, 10, PaymentFrequency.Yearly)),
            "Payment plan: invalid periods and date overflow are rejected before saving");
        var query = ReportQuery.Parse("SELECT ':NotAParam', [a:Column], \"b:Column\", @@ROWCOUNT FROM Test WHERE Id=:Id OR Id=@id -- :Comment\n/* :Ignore /* nested */ */");
        check(query.Parameters.SequenceEqual(new[] { "Id" }) && query.Sql.Contains("Id=@Id OR Id=@Id") && query.Sql.Contains("':NotAParam'"),
            "Report: repeated parameters normalize without rewriting strings, identifiers or comments");
        check(ReportQuery.Parse("WITH x AS (SELECT :Başlangıç AS Tarih) SELECT * FROM x").Parameters.Single() == "Başlangıç",
            "Report: CTE and Turkish parameter names are supported");
        check(Rejects(() => ReportQuery.Parse("SELECT 1; DELETE FROM Personel"))
            && Rejects(() => ReportQuery.Parse("SELECT * INTO NewTable FROM Personel"))
            && Rejects(() => ReportQuery.Parse("SELECT 'unfinished")), "Report: writes and malformed SQL text are rejected");
        check((decimal)ReportQuery.Value("Tutar", "1.250,55", false) == 1250.55M
            && (DateTime)ReportQuery.Value("Tarih", "31.12.2026", false) == new DateTime(2026, 12, 31)
            && (bool)ReportQuery.Value("Evet / Hayır", "Evet", false), "Report: parameters preserve Turkish numbers, dates and boolean values");
        check(ReferenceEquals(ReportQuery.Value("Tarih", "", true), DBNull.Value)
            && Rejects(() => ReportQuery.Value("Tam sayı", "abc", false)), "Report: NULL differs from invalid parameter values");
        var table = new DataTable(); table.Columns.Add("DeletedData"); table.Columns.Add("DeletedAt", typeof(DateTime));
        var row = table.Rows.Add("[{\"Name\":\"Çağrı\",\"Photo\":\"BINARY-DATA\",\"Address\":{\"City\":\"İstanbul\"},\"IsActive\":true}]", new DateTime(2026, 9, 9, 14, 25, 30));
        var details = HistoryDetails.Read(row);
        check(details.AsEnumerable().Any(r => (string)r[1] == "Çağrı") && details.AsEnumerable().Any(r => (string)r[1] == "İstanbul")
            && !details.AsEnumerable().Any(r => ((string)r[1]).Contains("BINARY-DATA")), "History: nested JSON becomes readable fields without dumping photo data");
        row[0] = "{broken legacy JSON";
        check(!HistoryDetails.IsJson(row[0]) && HistoryDetails.Read(row).Rows.Count == 2, "History: malformed legacy JSON remains readable and does not crash");
        var invoice = new InvoiceDraft("BKS", "Örnek", "1234567890", new DateTime(2026, 9, 9), new[] { new InvoiceLine("Hizmet", 3, 10.55M, 20) });
        invoice.Validate();
        check(invoice.Number(99) == "BKS2600100" && Rejects(() => invoice.Number(99999)), "Invoice: sequence retains padding and rejects overflow");
        check(Rejects(() => (invoice with { Lines = Array.Empty<InvoiceLine>() }).Validate()), "Invoice: an empty draft cannot be saved");
        check(Rejects(() => new IncomeExpenseEntry("", 1, "G").Validate())
            && Rejects(() => new IncomeExpenseEntry("Örnek", 1, "X").Validate()), "Finance: description and transaction type are required");
    }
}
