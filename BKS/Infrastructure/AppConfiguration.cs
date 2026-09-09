using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Data.SqlClient;
namespace BKS;
public static class AppConfiguration
{
    public static string DataDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    "HelmSoftware", "BKS");
    private static string SettingsPath => Path.Combine(DataDirectory, "connection.json");
    private static bool designPreview;
    public static bool DesignPreview
    {
        get => designPreview || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime;
        internal set => designPreview = value;
    }
    private static Settings? cached;
    private sealed class Settings
    {
        public string ApiBaseUrl
        {
            get;
            set;
        }
        = "https://randevu.aslancan.com.tr/";
        public string ProtectedConnection
        {
            get;
            set;
        }
        = "";
    }
    private static Settings Current => cached ??= File.Exists(SettingsPath) ? JsonSerializer.Deserialize<Settings>(File.ReadAllText(SettingsPath)) ?? new Settings(): new Settings();
    public static string ConnectionString
    {
        get
        {
            if (DesignPreview) return "";
            var environment = Environment.GetEnvironmentVariable("BKS_CONNECTION_STRING");
            if (!string.IsNullOrWhiteSpace(environment)) return environment;
            if (string.IsNullOrWhiteSpace(Current.ProtectedConnection)) return "";
            try
            {
                return Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(Current.ProtectedConnection), null,
                DataProtectionScope.CurrentUser));
            }
            catch (CryptographicException)
            {
                throw new InvalidOperationException("Bağlantı ayarları bu Windows kullanıcısıyla açılamadı. Bağlantı ayarlarını yeniden kaydedin.");
            }
        }
    }
    public static Uri ApiBaseUri => ValidateApi(Environment.GetEnvironmentVariable("BKS_API_BASE_URL") ?? Current.ApiBaseUrl);
    public static Uri Api(string relative) => new(ApiBaseUri, relative.TrimStart('/'));
    private static Uri ValidateApi(string value)
    {
        if (!Uri.TryCreate(value.TrimEnd('/') + "/", UriKind.Absolute, out var uri) || uri.Scheme != "https" || !string.IsNullOrEmpty(uri.UserInfo))
        throw new InvalidOperationException("API adresi geçerli bir HTTPS adresi olmalıdır.");
        return uri;
    }
    public static void Save(string connectionString, string apiBaseUrl)
    {
        var builder = new SqlConnectionStringBuilder(connectionString);
        if (string.IsNullOrWhiteSpace(builder.DataSource) || string.IsNullOrWhiteSpace(builder.InitialCatalog)) throw new InvalidOperationException("Sunucu ve veritabanı adı zorunludur.");
        var next = new Settings
        {
            ApiBaseUrl = ValidateApi(apiBaseUrl).AbsoluteUri,
            ProtectedConnection = Convert.ToBase64String(ProtectedData.Protect(Encoding.UTF8.GetBytes(builder.ConnectionString),
            null, DataProtectionScope.CurrentUser))
        };
        Directory.CreateDirectory(DataDirectory);
        var temp = SettingsPath + ".tmp";
        File.WriteAllText(temp, JsonSerializer.Serialize(next, new JsonSerializerOptions
        {
            WriteIndented = true
        }));
        File.Move(temp, SettingsPath, true);
        cached = next;
    }
}
public static class SessionContext
{
    public static Guid UserId
    {
        get;
        set;
    }
    public static string Role
    {
        get;
        set;
    }
    = "";
    public static void Clear()
    {
        UserId = Guid.Empty;
        Role = "";
    }
}
