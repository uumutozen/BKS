using System.Data.SqlClient;
using System.Text.RegularExpressions;
namespace BKS;
internal static class UiActions
{
    public static void Run(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }
    public static string SafeMessage(Exception ex) => Regex.Replace(ex.Message, @"(?i)(password|pwd)\s*=\s*[^;\r\n]*",
    "$1=***");
    public static void ShowError(Exception ex)
    {
        var text = ex is SqlException ? "Veritabanı işlemi tamamlanamadı. Bağlantı ve yetkileri kontrol edin.\n\n" + SafeMessage(ex): SafeMessage(ex);
        MessageBox.Show(text, "İşlem tamamlanamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
// Existing call sites use a consistent, scrollable application modal instead of native alerts.
internal static class MessageBox
{
    public static DialogResult Show(string? text, string caption = "BKS", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
    {
        using var form = new Form
        {
            Text = caption,
            ClientSize = new Size(560, 300),
            MinimumSize = new Size(420, 260),
            Font = new Font("Segoe UI", 10F),
            BackColor = Color.White,
            StartPosition = FormStartPosition.CenterParent,
            MaximizeBox = false,
            MinimizeBox = false,
            ShowInTaskbar = false,
            Padding = new Padding(24)
        };
        var footer = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 48,
            FlowDirection = FlowDirection.RightToLeft
        };
        (string, DialogResult)[] choices = buttons switch
        {
            MessageBoxButtons.YesNo => new[]
            {
                ("Vazgeç", DialogResult.No),
                ("Evet", DialogResult.Yes)
            },
            MessageBoxButtons.YesNoCancel => new[]
            {
                ("Vazgeç", DialogResult.Cancel),
                ("Hayır", DialogResult.No),
                ("Evet", DialogResult.Yes)
            },
            MessageBoxButtons.OKCancel => new[]
            {
                ("Vazgeç", DialogResult.Cancel),
                ("Tamam", DialogResult.OK)
            },
            MessageBoxButtons.RetryCancel => new[]
            {
                ("Vazgeç", DialogResult.Cancel),
                ("Tekrar dene", DialogResult.Retry)
            },
            _ => new[]
            {
                ("Tamam", DialogResult.OK)
            }
        };
        foreach (var(title, result) in choices)
        {
            var button = Screens.Button(title, () =>
            {
                form.DialogResult = result;
                form.Close();
            });
            button.Width = 115;
            button.Margin = new Padding(6, 2, 0, 0);
            footer.Controls.Add(button);
        }
        form.CancelButton = (IButtonControl) footer.Controls[0];
        form.AcceptButton = (IButtonControl) footer.Controls[0];
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
        layout.Controls.Add(new TextBox
        {
            Text = text ?? "",
            Dock = DockStyle.Fill,
            Multiline = true,
            ReadOnly = true,
            BorderStyle = BorderStyle.None,
            BackColor = Color.White,
            ForeColor = ModernWinForms.Text,
            ScrollBars = ScrollBars.Vertical
        }, 0, 0);
        footer.Dock = DockStyle.Fill;
        layout.Controls.Add(footer, 0, 1);
        form.Controls.Add(layout);
        var owner = Form.ActiveForm;
        return owner is null ? form.ShowDialog(): form.ShowDialog(owner);
    }
}
public sealed class ConnectionSettingsForm : Form
{
    public ConnectionSettingsForm()
    {
        Text = "BKS bağlantı ayarları";
        ClientSize = new Size(700, 580);
        var source = new SqlConnectionStringBuilder();
        try
        {
            source.ConnectionString = AppConfiguration.ConnectionString;
        }
        catch
        {
        }
        var server = new TextBox
        {
            Text = source.DataSource
        };
        var database = new TextBox
        {
            Text = source.InitialCatalog
        };
        var user = new TextBox
        {
            Text = source.UserID
        };
        var password = new TextBox
        {
            Text = source.Password,
            UseSystemPasswordChar = true
        };
        var integrated = new CheckBox
        {
            Text = "Windows kimliği",
            Checked = source.IntegratedSecurity
        };
        var trust = new CheckBox
        {
            Text = "Sunucu sertifikasına güven",
            Checked = source.TrustServerCertificate
        };
        var api = new TextBox
        {
            Text = AppConfiguration.ApiBaseUri.AbsoluteUri
        };
        string Build() => new SqlConnectionStringBuilder
        {
            DataSource = server.Text.Trim(),
            InitialCatalog = database.Text.Trim(),
            UserID = integrated.Checked ? "": user.Text.Trim(),
            Password = integrated.Checked ? "": password.Text,
            IntegratedSecurity = integrated.Checked,
            Encrypt = true,
            TrustServerCertificate = trust.Checked,
            ConnectTimeout = 15
        }.ConnectionString;
        var fields = new SectionedForm()
        .AddSection("Veritabanı", "Kurumunuzun veritabanı bağlantı bilgileri.", new ResponsiveFields(("Sunucu", server),
        ("Veritabanı", database), ("Kullanıcı", user), ("Parola", password), ("Kimlik doğrulama", integrated), ("Sertifika", trust)))
        .AddSection("Giriş hizmeti", "Kurumunuzun giriş hizmeti adresi.", new ResponsiveFields(("Hizmet adresi (HTTPS)", api)));
        integrated.CheckedChanged += (_, _) =>
        {
            user.Enabled = password.Enabled = !integrated.Checked;
        };
        user.Enabled = password.Enabled = !integrated.Checked;
        var ribbon = Screens.Ribbon("Bağlantı", new RibbonCommand("Kaydet", RibbonIcon.Backup, () =>
        {
            AppConfiguration.Save(Build(), api.Text.Trim());
            DialogResult = DialogResult.OK;
            Close();
        }), new RibbonCommand("Vazgeç", RibbonIcon.Restore, Close));
        Screens.Install(this, fields, ribbon, Text);
    }
}
