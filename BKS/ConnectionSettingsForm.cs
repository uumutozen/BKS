using System.Data.SqlClient;
namespace BKS;
public partial class ConnectionSettingsForm : Form
{
    public ConnectionSettingsForm()
    {
        InitializeComponent();
        Screens.PrepareDesignerForm(this);
        if (AppConfiguration.DesignPreview) return;
        var source = new SqlConnectionStringBuilder();
        try { source.ConnectionString = AppConfiguration.ConnectionString; } catch { }
        server.Text = source.DataSource;
        database.Text = source.InitialCatalog;
        user.Text = source.UserID;
        password.Text = source.Password;
        integrated.Checked = source.IntegratedSecurity;
        trust.Checked = source.TrustServerCertificate;
        api.Text = AppConfiguration.ApiBaseUri.AbsoluteUri;
        integrated.CheckedChanged += (_, _) => user.Enabled = password.Enabled = !integrated.Checked;
        user.Enabled = password.Enabled = !integrated.Checked;
    }
    private void Save_Click(object? sender, EventArgs e) => UiActions.Run(() =>
    {
        var connection = new SqlConnectionStringBuilder
        {
            DataSource = server.Text.Trim(), InitialCatalog = database.Text.Trim(),
            UserID = integrated.Checked ? "" : user.Text.Trim(),
            Password = integrated.Checked ? "" : password.Text,
            IntegratedSecurity = integrated.Checked, Encrypt = true,
            TrustServerCertificate = trust.Checked, ConnectTimeout = 15
        }.ConnectionString;
        AppConfiguration.Save(connection, api.Text.Trim());
        DialogResult = DialogResult.OK;
        Close();
    });
    private void Cancel_Click(object? sender, EventArgs e) { DialogResult = DialogResult.Cancel; Close(); }
}
