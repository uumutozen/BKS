namespace BKS;

/// <summary>UI'den bağımsız, anahtara göre tek nesne üreten generic kayıt deposu.</summary>
public sealed class DocumentRegistry<T> where T : class
{
    private readonly Dictionary<string, T> items = new(StringComparer.OrdinalIgnoreCase);
    private readonly HashSet<string> creating = new(StringComparer.OrdinalIgnoreCase);

    public IEnumerable<KeyValuePair<string, T>> Entries => items.ToArray();

    public T GetOrCreate(string key, Func<T> factory)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentNullException.ThrowIfNull(factory);
        if (items.TryGetValue(key, out var existing)) return existing;
        if (!creating.Add(key)) throw new InvalidOperationException("Bu ekran zaten açılıyor: " + key);
        try
        {
            var item = factory() ?? throw new InvalidOperationException("Ekran oluşturucusu boş sonuç döndürdü.");
            items.Add(key, item);
            return item;
        }
        finally { creating.Remove(key); }
    }

    public bool TryGet(string key, out T? value) => items.TryGetValue(key, out value);
    public bool Remove(string key) => items.Remove(key);
}
