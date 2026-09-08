using System.Data;
using System.Data.SqlClient;
namespace BKS;
internal static class PersonnelPhotos
{
    // Existing deployments may call the binary column Photo or photobinary.
    // Only these fixed names and binary SQL types are eligible for interpolation.
    private const string ColumnQuery = @"SELECT TOP (1) c.name FROM sys.columns c
        WHERE c.object_id=OBJECT_ID(N'dbo.Personel')
        AND c.name IN (N'Photo',N'photo',N'photobinary',N'PhotoBinary')
        AND c.system_type_id IN (34,165,173)
        ORDER BY CASE WHEN LOWER(c.name)=N'photo' THEN 0 ELSE 1 END";
    private static string SafeColumn(object? value)
    {
        string name = Convert.ToString(value) ?? "";
        if (!new[]
        {
            "Photo",
            "photobinary",
            "PhotoBinary"
        }.Contains(name, StringComparer.OrdinalIgnoreCase))
        throw new InvalidOperationException("Personel fotoğraf alanı bulunamadı. Veritabanı yapılandırması kontrol edilmeli.");
        return "[" + name + "]";
    }
    public static string ResolveColumn(SqlConnection connection)
    {
        using var command = new SqlCommand(ColumnQuery, connection);
        return SafeColumn(command.ExecuteScalar());
    }
    public static async Task<byte[]?> ReadAsync(string connectionString, Guid personId, Guid userId, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        using var columnCommand = new SqlCommand(ColumnQuery, connection);
        string column = SafeColumn(await columnCommand.ExecuteScalarAsync(cancellationToken));
        using var command = new SqlCommand($"SELECT {column} FROM dbo.Personel WHERE PersonelId=@id AND CompanyId=dbo.GetSirketIdByUserId(@user)",
        connection);
        command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = personId;
        command.Parameters.Add("@user", SqlDbType.UniqueIdentifier).Value = userId;
        return await command.ExecuteScalarAsync(cancellationToken) as byte[];
    }
}
