using System.Text.Json;
namespace BKS;
internal static class UiPreferences
{
    private sealed record Preference(bool RibbonExpanded);
    private static string FilePath(Guid userId) => Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    "HelmSoftware", "BKS", "Preferences", userId.ToString("N") + ".json");
    public static bool ReadRibbonExpanded(Guid userId)
    {
        try
        {
            return JsonSerializer.Deserialize<Preference>(File.ReadAllText(FilePath(userId)))?.RibbonExpanded ?? true;
        }
        catch (IOException)
        {
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            return true;
        }
        catch (JsonException)
        {
            return true;
        }
    }
    public static void SaveRibbonExpanded(Guid userId, bool expanded)
    {
        if (userId == Guid.Empty || AppConfiguration.DesignPreview) return;
        try
        {
            string path = FilePath(userId);
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, JsonSerializer.Serialize(new Preference(expanded)));
        }
        catch (IOException)
        {
            /* A display preference must not interrupt the user's work. */
        }
        catch (UnauthorizedAccessException)
        {
        }
    }
}
