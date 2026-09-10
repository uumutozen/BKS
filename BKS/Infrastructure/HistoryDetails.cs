using System.Data;
using System.Globalization;
using System.Text.Json;
namespace BKS;

public static class HistoryDetails
{
    private static readonly Dictionary<string, string> Captions = new(StringComparer.OrdinalIgnoreCase)
    {
        ["DeletedData"] = "Kayıt bilgileri", ["SilinenVeri"] = "Kayıt bilgileri", ["SilinenVeriler"] = "Kayıt bilgileri",
        ["DeletedAt"] = "İşlem zamanı", ["SilinmeZamani"] = "İşlem zamanı", ["ActionName"] = "İşlem",
        ["Actions"] = "İşlem kodu", ["TableName"] = "Kayıt türü", ["DeleteUserId"] = "İşlemi yapan kullanıcı",
        ["Name"] = "Ad", ["FirstName"] = "Ad", ["Surname"] = "Soyad", ["LastName"] = "Soyad",
        ["BirthDate"] = "Doğum tarihi", ["Phone"] = "Telefon", ["Email"] = "E-posta", ["Address"] = "Adres",
        ["IsActive"] = "Aktif", ["IsDeleted"] = "Silinmiş", ["CreatedAt"] = "Kayıt tarihi", ["UpdatedAt"] = "Güncelleme tarihi",
        ["Amount"] = "Tutar", ["PaymentDate"] = "Vade tarihi", ["IdentityNumber"] = "Kimlik numarası"
    };
    public static string Caption(string name) => Captions.GetValueOrDefault(name, name);
    public static bool IsJson(object? value)
    {
        if (value is not string text || string.IsNullOrWhiteSpace(text) || text.TrimStart()[0] is not ('{' or '[')) return false;
        try { using var doc = JsonDocument.Parse(text); return true; }
        catch (JsonException) { return false; }
    }
    public static DataTable Read(DataRow? row)
    {
        var result = new DataTable();
        result.Columns.Add("Alan"); result.Columns.Add("Değer");
        if (row == null) return result;
        foreach (DataColumn column in row.Table.Columns)
        {
            var value = row[column];
            var caption = Caption(column.ColumnName);
            if (value is byte[] || IsPhoto(column.ColumnName)) { result.Rows.Add(caption, "Fotoğraf / ikili veri"); continue; }
            if (IsJson(value))
            {
                using var doc = JsonDocument.Parse((string)value);
                Flatten(doc.RootElement, caption, result);
            }
            else result.Rows.Add(caption, Format(value));
        }
        return result;
    }
    private static bool IsPhoto(string name) => new[] { "photo", "photobinary", "foto", "fotograf", "pdficerik" }.Contains(name.ToLowerInvariant());
    public static string Format(object? value) => value switch
    {
        null or DBNull => "—", DateTime date => date.ToString("dd.MM.yyyy HH:mm:ss"), bool flag => flag ? "Evet" : "Hayır",
        decimal amount => amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")),
        _ => Convert.ToString(value) switch { "INSERT" => "Ekleme", "UPDATE" => "Güncelleme", "DELETE" => "Silme / pasife alma", var text => text ?? "—" }
    };
    private static void Flatten(JsonElement value, string path, DataTable result)
    {
        if (value.ValueKind == JsonValueKind.Object)
        {
            foreach (var property in value.EnumerateObject())
            {
                string name = path + " / " + Caption(property.Name);
                if (IsPhoto(property.Name)) result.Rows.Add(name, "Fotoğraf / ikili veri");
                else Flatten(property.Value, name, result);
            }
        }
        else if (value.ValueKind == JsonValueKind.Array)
        {
            int i = 0;
            foreach (var item in value.EnumerateArray()) Flatten(item, path + " [" + ++i + "]", result);
            if (i == 0) result.Rows.Add(path, "—");
        }
        else result.Rows.Add(path, value.ValueKind switch
        {
            JsonValueKind.Null => "—", JsonValueKind.True => "Evet", JsonValueKind.False => "Hayır",
            JsonValueKind.String => value.GetString(), _ => value.ToString()
        });
    }
}
