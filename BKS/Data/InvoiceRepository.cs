using System.Data;
using System.Data.SqlClient;
namespace BKS;

internal sealed class InvoiceRepository
{
    private readonly Guid user;
    private readonly string connectionString = AppConfiguration.ConnectionString;
    public InvoiceRepository(Guid user) => this.user = user;
    public DataTable Read() => new SqlDataAccess(connectionString).Table(
        "SELECT Id,FaturaNo,AliciUnvan,AliciVKN,Tarih FROM Faturalar WHERE SirketId=dbo.GetSirketIdByUserId(@UserId) ORDER BY Tarih DESC,FaturaNo DESC",
        CommandType.Text, new SqlParameter("@UserId", user));
    public byte[] Pdf(object id) => new SqlDataAccess(connectionString).Scalar(
        "SELECT PdfIcerik FROM Faturalar WHERE Id=@Id AND SirketId=dbo.GetSirketIdByUserId(@UserId)",
        CommandType.Text, new SqlParameter("@Id", id), new SqlParameter("@UserId", user)) is byte[] { Length: > 0 } bytes
            ? bytes : throw new InvalidOperationException("Fatura bulunamadı veya PDF içeriği yok.");
    public string Save(InvoiceDraft invoice)
    {
        invoice.Validate();

        if (user == Guid.Empty)
            throw new InvalidOperationException("Geçerli kullanıcı oturumu gerekli.");

        string? path = null;
        bool committed = false;

        try
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            using var transaction =
                connection.BeginTransaction(IsolationLevel.Serializable);

            // Şirket ID
            Guid companyId;

            using (var company = new SqlCommand(
                "SELECT dbo.GetSirketIdByUserId(@User)",
                connection,
                transaction))
            {
                company.Parameters.Add(
                    "@User",
                    SqlDbType.UniqueIdentifier).Value = user;

                object? companyResult = company.ExecuteScalar();

                if (companyResult == null ||
                    companyResult == DBNull.Value ||
                    !Guid.TryParse(companyResult.ToString(), out companyId) ||
                    companyId == Guid.Empty)
                {
                    throw new InvalidOperationException(
                        "Kullanıcının şirketi bulunamadı.");
                }
            }

            // Fatura prefix
            string prefix =
                invoice.Prefix +
                invoice.Date.ToString(
                    "yy",
                    System.Globalization.CultureInfo.InvariantCulture);

            // Son fatura numarası
            using var sequence = new SqlCommand(@"
SELECT MAX(TRY_CONVERT(int, RIGHT(FaturaNo, 5)))
FROM Faturalar WITH (UPDLOCK, HOLDLOCK)
WHERE SirketId = @Company
  AND FaturaNo LIKE @Prefix + '%'
  AND LEN(FaturaNo) = LEN(@Prefix) + 5;",
                connection,
                transaction);

            sequence.Parameters.Add(
                "@Company",
                SqlDbType.UniqueIdentifier).Value = companyId;

            sequence.Parameters.Add(
                "@Prefix",
                SqlDbType.NVarChar,
                50).Value = prefix;

            object? last = sequence.ExecuteScalar();

            int lastNumber =
                last == null || last == DBNull.Value
                    ? 0
                    : Convert.ToInt32(last);

            string number = invoice.Number(lastNumber);

            // PDF klasörü
            string folder = Path.Combine(
                AppConfiguration.DataDirectory,
                "Faturalar",
                companyId.ToString("N"));

            Directory.CreateDirectory(folder);

            path = Path.Combine(
                folder,
                number + "_" + Guid.NewGuid().ToString("N") + ".pdf");

            // PDF oluştur
            InvoicePdf.Write(
                path,
                number,
                invoice.Recipient,
                invoice.TaxNumber,
                invoice.Date,
                invoice.Lines);

            if (!File.Exists(path))
                throw new InvalidOperationException(
                    "Fatura PDF dosyası oluşturulamadı.");

            byte[] pdfContent = File.ReadAllBytes(path);

            // DB kayıt
            using var insert = new SqlCommand(@"
INSERT INTO Faturalar
(
    FaturaNo,
    AliciUnvan,
    AliciVKN,
    Tarih,
    PdfYolu,
    PdfIcerik,
    SirketId
)
VALUES
(
    @No,
    @Title,
    @Tax,
    @Date,
    @Path,
    @PDF,
    @Company
);",
                connection,
                transaction);

            insert.Parameters.Add(
                "@No",
                SqlDbType.NVarChar,
                50).Value = number;

            insert.Parameters.Add(
                "@Title",
                SqlDbType.NVarChar,
                500).Value = invoice.Recipient;

            insert.Parameters.Add(
                "@Tax",
                SqlDbType.NVarChar,
                20).Value = invoice.TaxNumber;

            insert.Parameters.Add(
                "@Date",
                SqlDbType.Date).Value = invoice.Date.Date;

            insert.Parameters.Add(
                "@Path",
                SqlDbType.NVarChar,
                2000).Value = path;

            insert.Parameters.Add(
                "@PDF",
                SqlDbType.VarBinary,
                -1).Value = pdfContent;

            insert.Parameters.Add(
                "@Company",
                SqlDbType.UniqueIdentifier).Value = companyId;

            int affected = insert.ExecuteNonQuery();

            if (affected != 1)
                throw new InvalidOperationException(
                    "Fatura kaydedilemedi.");

            transaction.Commit();

            committed = true;

            return number;
        }
        finally
        {
            if (!committed &&
                !string.IsNullOrWhiteSpace(path) &&
                File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (IOException)
                {
                    // Asıl hatayı ezmemek için burada hata fırlatmıyoruz.
                }
            }
        }
    }
}
