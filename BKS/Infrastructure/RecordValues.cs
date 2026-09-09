using System.Globalization;

namespace BKS;

/// <summary>Bir kayıt anlık görüntüsündeki boş ve farklı türdeki değerleri güvenli okur.</summary>
public sealed class RecordValues
{
    private readonly IReadOnlyDictionary<string, object?> values;

    public RecordValues(IReadOnlyDictionary<string, object?> values) => this.values = values;

    public T Get<T>(string key, T fallback = default!)
    {
        if (!values.TryGetValue(key, out var value) || value is null || value == DBNull.Value) return fallback;
        if (value is T result) return result;
        try
        {
            var type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
            if (type == typeof(Guid)) return Guid.TryParse(value.ToString(), out var id) ? (T)(object)id : fallback;
            if (type == typeof(bool)) return (T)(object)DataValues.Boolean(value);
            return (T)Convert.ChangeType(value, type, CultureInfo.CurrentCulture);
        }
        catch (Exception error) when (error is FormatException or InvalidCastException or OverflowException)
        {
            return fallback;
        }
    }

    public Guid RequiredId(string key)
    {
        var id = Get<Guid>(key);
        return id != Guid.Empty ? id : throw new InvalidOperationException("Kayıt kimliği okunamadı. Listeyi yenileyip tekrar deneyin.");
    }
}
