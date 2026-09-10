using BKS;
using PdfSharpCore.Pdf.IO;

// Gerçek PDF üreticisini çalıştırır; örnek verilerdir, SQL/API kullanılmaz.
string directory = Path.GetFullPath(args.FirstOrDefault() ?? "Pdf_Sonuclari");
Directory.CreateDirectory(directory);
int passed = 0;
void Check(string name, string title, IReadOnlyList<InvoiceLine> lines, int minimumPages = 1)
{
    string path = Path.Combine(directory, name + ".pdf");
    InvoicePdf.Write(path, "BKS-TEST-001", title, "0000000000", new DateTime(2026, 1, 15), lines);
    using var document = PdfReader.Open(path, PdfDocumentOpenMode.Import);
    if (document.PageCount < minimumPages || new FileInfo(path).Length < 500)
        throw new InvalidOperationException("PDF doğrulaması başarısız: " + name);
    Console.WriteLine($"PASS {name}: {document.PageCount} sayfa — {path}");
    passed++;
}

Check("01_SatirSonu", "Örnek Anaokulu", new[] { new InvoiceLine("Eğitim bedeli", 1, 1250.55M, 20) });
Check("02_CokSatirli", "Örnek Anaokulu\r\nİstanbul Şubesi\n\nMuhasebe", new[]
{
    new InvoiceLine("Eğitim bedeli\r\nİkinci satır\n\nEk açıklama", 2, 1250.55M, 20),
    new InvoiceLine(new string('İ', 240), 1, 150, 10)
});
Check("03_SayfaGecisi", "Örnek Anaokulu", Enumerable.Range(1, 90)
    .Select(index => new InvoiceLine($"{index}. dönem eğitim bedeli\nÖğrenci kayıt açıklaması", 1, 500M + index, 20)).ToArray(), 3);
Console.WriteLine($"{passed} gerçek PDF üretim kontrolü geçti. PDF'leri açıp Türkçe karakterleri ve satır yerleşimini de inceleyin.");
