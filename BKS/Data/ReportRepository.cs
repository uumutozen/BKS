using System.Data;
using System.Data.SqlClient;
namespace BKS;

internal sealed record ReportDefinition(int Id, string Title, string Query);
internal sealed record ReportArgument(string Name, string Type, object Value);
internal sealed class ReportRepository
{
    private readonly SqlDataAccess database = new(AppConfiguration.ConnectionString);
    public DataTable List() => database.Table("SELECT Id,RaporAdi,KayitTarihi FROM OzelRaporlar ORDER BY KayitTarihi DESC,Id DESC");
    public ReportDefinition Read(int id) => database.Query("SELECT Id,RaporAdi,Sorgu FROM OzelRaporlar WHERE Id=@Id",
        row => new ReportDefinition(Convert.ToInt32(row["Id"]), Convert.ToString(row["RaporAdi"]) ?? "", Convert.ToString(row["Sorgu"]) ?? ""),
        new SqlParameter("@Id", id)).SingleOrDefault() ?? throw new InvalidOperationException("Rapor bulunamadı.");
    public int Save(int? id, string title, string query)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new InvalidOperationException("Rapor adı gerekli.");
        ReportQuery.Parse(query);
        string sql = id.HasValue ? "UPDATE OzelRaporlar SET RaporAdi=@Title,Sorgu=@Query OUTPUT INSERTED.Id WHERE Id=@Id"
            : "INSERT INTO OzelRaporlar(RaporAdi,Sorgu) OUTPUT INSERTED.Id VALUES(@Title,@Query)";
        var result = database.Scalar(sql, CommandType.Text, new SqlParameter("@Title", title.Trim()), new SqlParameter("@Query", query), new SqlParameter("@Id", (object?)id ?? DBNull.Value));
        return result is null or DBNull ? throw new InvalidOperationException("Rapor kaydedilemedi veya artık mevcut değil.") : Convert.ToInt32(result);
    }
    public DataTable Run(ReportQuery query, IReadOnlyList<ReportArgument> values)
    {
        var parameters = query.Parameters.Select(name =>
        {
            var arg = values.SingleOrDefault(v => v.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                ?? throw new InvalidOperationException(name + " parametresini doldurun.");
            var type = arg.Type switch { "Tam sayı" => SqlDbType.BigInt, "Tutar" => SqlDbType.Decimal, "Tarih" => SqlDbType.DateTime2,
                "Evet / Hayır" => SqlDbType.Bit, "GUID" => SqlDbType.UniqueIdentifier, _ => SqlDbType.NVarChar };
            var parameter = new SqlParameter("@" + name, type) { Value = arg.Value };
            if (type == SqlDbType.Decimal) { parameter.Precision = 28; parameter.Scale = 8; }
            if (type == SqlDbType.NVarChar) parameter.Size = -1;
            return parameter;
        }).ToArray();
        return database.Table(query.Sql, CommandType.Text, parameters);
    }
}
