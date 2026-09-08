namespace BKS;
/// <summary>Generic keyed lifetime store; the factory runs only for a new key.</summary>
public sealed class DocumentRegistry<T> where T: class
{
    private readonly Dictionary<string, T> documents = new(StringComparer.OrdinalIgnoreCase);
    public IEnumerable<KeyValuePair<string, T>> Entries => documents.ToArray();
    public T GetOrCreate(string key, Func<T> factory)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Document key is required.", nameof(key));
        if (documents.TryGetValue(key, out var existing)) return existing;
        var value = factory();
        documents.Add(key, value);
        return value;
    }
    public bool TryGet(string key, out T? value) => documents.TryGetValue(key, out value);
    public bool Remove(string key) => documents.Remove(key);
}
