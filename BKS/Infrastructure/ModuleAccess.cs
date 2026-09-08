namespace BKS;
public static class ModuleAccess
{
    public static bool IsAdmin(string? role) => string.Equals(role, "ADMIN", StringComparison.OrdinalIgnoreCase) || string.Equals(role,
    "ADMİN", StringComparison.OrdinalIgnoreCase);
    public static IEnumerable<string> Resolve(IEnumerable<string> known, IEnumerable<string>? granted, string? role)
    {
        if (string.IsNullOrWhiteSpace(role) || granted is null) return Array.Empty<string>();
        var names = known.ToArray();
        if (IsAdmin(role)) return names;
        var allowed = new HashSet<string>(granted, StringComparer.OrdinalIgnoreCase);
        return names.Where(allowed.Contains).ToArray();
    }
}
