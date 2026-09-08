using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
namespace BKS;
public partial class Form1 : Form
{
    private bool _loggingIn;
    private readonly CancellationTokenSource _loginCancellation = new();
    public Form1()
    {
        InitializeComponent();
    }
    private void Form1_Load(object? sender, EventArgs e)
    {
        if (AppConfiguration.DesignPreview)
        {
            loginStatus.Text = "Tasarım önizlemesi — canlı giriş kapalı.";
            return;
        }
        // Remove legacy plaintext password storage without ever copying it to the new UI.
        Properties.Settings.Default.passWord = "";
        if (Properties.Settings.Default.cbxBeniHatirla)
        {
            userName.Text = Properties.Settings.Default.userName;
            cbxBeniHatirla.Checked = true;
        }
        Properties.Settings.Default.Save();
    }
    private async void bttnLgn_Click(object? sender, EventArgs e)
    {
        if (_loggingIn || AppConfiguration.DesignPreview) return;
        if (string.IsNullOrWhiteSpace(userName.Text) || passWord.Text.Length == 0)
        {
            loginStatus.ForeColor = Color.Firebrick;
            loginStatus.Text = "E-posta adresinizi ve parolanızı girin.";
            if (string.IsNullOrWhiteSpace(userName.Text)) userName.Focus();
            else passWord.Focus();
            return;
        }
        _loggingIn = true;
        SetLoginBusy(true);
        loginStatus.ForeColor = ModernWinForms.Muted;
        loginStatus.Text = "Giriş doğrulanıyor…";
        try
        {
            if (string.IsNullOrWhiteSpace(AppConfiguration.ConnectionString))
            {
                using var settings = new ConnectionSettingsForm();
                if (settings.ShowDialog(this) != DialogResult.OK)
                {
                    loginStatus.Text = "Devam etmek için bağlantı ayarlarını kaydedin.";
                    return;
                }
            }
            using var client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
            using var response = await client.PostAsJsonAsync(AppConfiguration.Api("api/Login"), new
            {
                Email = userName.Text.Trim(), Password = passWord.Text
            }, _loginCancellation.Token);
            if (!response.IsSuccessStatusCode)
            {
                loginStatus.Text = response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden ? "E-posta veya parola hatalı.": "Giriş hizmeti isteği tamamlayamadı. Lütfen tekrar deneyin.";
                return;
            }
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken: _loginCancellation.Token);
            if (result?.Success != true || !Guid.TryParse(result.UserId, out var id) || id == Guid.Empty || string.IsNullOrWhiteSpace(result.Role))
            {
                loginStatus.Text = result?.Success == false ? result.Message ?? "Giriş reddedildi.": "Giriş yanıtında kullanıcı veya rol bilgisi eksik.";
                return;
            }
            Properties.Settings.Default.userName = cbxBeniHatirla.Checked ? userName.Text.Trim(): "";
            Properties.Settings.Default.cbxBeniHatirla = cbxBeniHatirla.Checked;
            Properties.Settings.Default.passWord = "";
            Properties.Settings.Default.Save();
            SessionContext.UserId = id;
            SessionContext.Role = result.Role;
            using var main = new Form2
            {
                UserId = id,
                Role = result.Role
            };
            passWord.Clear();
            Hide();
            main.ShowDialog();
            SessionContext.Clear();
            Close();
        }
        catch (OperationCanceledException)
        {
            if (!IsDisposed) loginStatus.Text = "Giriş isteği zaman aşımına uğradı.";
        }
        catch (JsonException)
        {
            if (!IsDisposed) loginStatus.Text = "Sunucudan beklenen giriş yanıtı alınamadı.";
        }
        catch (Exception ex)
        {
            if (!IsDisposed) loginStatus.Text = UiActions.SafeMessage(ex);
        }
        finally
        {
            _loggingIn = false;
            if (!IsDisposed)
            {
                SetLoginBusy(false);
                loginStatus.ForeColor = Color.Firebrick;
                if (!Visible) Show();
            }
        }
    }
    public sealed class LoginResponse
    {
        public bool Success
        {
            get;
            set;
        }
        public string? UserId
        {
            get;
            set;
        }
        public string? Role
        {
            get;
            set;
        }
        public string? Message
        {
            get;
            set;
        }
    }
}
