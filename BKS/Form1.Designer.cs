#nullable enable
namespace BKS;
partial class Form1
{
    private System.ComponentModel.IContainer? components;
    private TextBox userName = null!;
    private TextBox passWord = null!;
    private CheckBox cbxBeniHatirla = null!;
    private Button bttnLgn = null!;
    private Label loginStatus = null!;
    private Button showPassword = null!;
    private Button connectionSettings = null!;
    private Label capsWarning = null!;
    private ProgressBar loginProgress = null!;
    protected override void Dispose(bool disposing) { if (disposing) { components?.Dispose(); _loginCancellation.Cancel(); _loginCancellation.Dispose(); } base.Dispose(disposing); }
    private void InitializeComponent()
    {
        SuspendLayout();
        AutoScaleDimensions = new SizeF(96F, 96F); AutoScaleMode = AutoScaleMode.Dpi;
        Text = "BKS · Helm Software"; ClientSize = new Size(1120, 740); MinimumSize = new Size(600, 500);
        StartPosition = FormStartPosition.CenterScreen; BackColor = ModernWinForms.PageBack; Font = new Font("Segoe UI", 10F);
        var card = new TableLayoutPanel { BackColor = Color.White, Padding = new Padding(28), ColumnCount = 1, RowCount = 18 };
        card.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        foreach (var height in new[] { 26, 12, 42, 42, 10, 22, 44, 14, 22, 44, 24, 32, 12, 46, 5, 56, 34, 26 })
            card.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
        Label Caption(string text, Color color, float size, FontStyle style = FontStyle.Regular) =>
            new() { Text = text, ForeColor = color, Font = new Font("Segoe UI", size, style), Dock = DockStyle.Fill, Margin = Padding.Empty };
        card.Controls.Add(Caption("BKS  /  HELM SOFTWARE", RibbonPalette.Accent, 10F, FontStyle.Bold), 0, 0);
        card.Controls.Add(Caption("Hoş geldiniz", ModernWinForms.Text, 24F, FontStyle.Bold), 0, 2);
        card.Controls.Add(Caption("Devam etmek için hesabınıza giriş yapın.", ModernWinForms.Muted, 10F), 0, 3);
        card.Controls.Add(Caption("E-posta adresi", ModernWinForms.Text, 9.5F), 0, 5);
        userName = new TextBox { PlaceholderText = "adiniz@kurumunuz.com", MaxLength = 254, AccessibleName = "E-posta adresi", TabIndex = 0 };
        card.Controls.Add(new LoginInputPanel(userName) { Dock = DockStyle.Fill, Margin = Padding.Empty, TabIndex = 0 }, 0, 6);
        card.Controls.Add(Caption("Parola", ModernWinForms.Text, 9.5F), 0, 8);
        passWord = new TextBox { UseSystemPasswordChar = true, PlaceholderText = "Parolanızı girin", MaxLength = 512, AccessibleName = "Parola", TabIndex = 0 };
        showPassword = new Button { Text = "Göster", FlatStyle = FlatStyle.Flat, ForeColor = RibbonPalette.Accent, TabIndex = 1, AccessibleName = "Parolayı göster", Cursor = Cursors.Hand, Font = new Font("Segoe UI", 9F) };
        showPassword.FlatAppearance.BorderSize = 0;
        showPassword.Click += (_, _) => { passWord.UseSystemPasswordChar = !passWord.UseSystemPasswordChar; showPassword.Text = passWord.UseSystemPasswordChar ? "Göster" : "Gizle"; showPassword.AccessibleName = passWord.UseSystemPasswordChar ? "Parolayı göster" : "Parolayı gizle"; passWord.Focus(); };
        card.Controls.Add(new LoginInputPanel(passWord, showPassword) { Dock = DockStyle.Fill, Margin = Padding.Empty, TabIndex = 1 }, 0, 9);
        capsWarning = Caption("", Color.FromArgb(165, 104, 26), 9F); card.Controls.Add(capsWarning, 0, 10);
        passWord.KeyDown += (_, _) => UpdateCapsWarning(); passWord.KeyUp += (_, _) => UpdateCapsWarning(); passWord.GotFocus += (_, _) => UpdateCapsWarning();
        passWord.LostFocus += (_, _) => { capsWarning.Text = ""; };
        cbxBeniHatirla = new CheckBox { Text = "E-posta adresimi hatırla", Dock = DockStyle.Fill, Margin = Padding.Empty, ForeColor = ModernWinForms.Muted, TabIndex = 2 };
        card.Controls.Add(cbxBeniHatirla, 0, 11);
        bttnLgn = Screens.Button("Giriş yap", () => { }); bttnLgn.Click += bttnLgn_Click; bttnLgn.Dock = DockStyle.Fill;
        bttnLgn.Margin = Padding.Empty; bttnLgn.BackColor = RibbonPalette.Accent; bttnLgn.Font = new Font("Segoe UI", 11F, FontStyle.Bold); bttnLgn.TabIndex = 3;
        card.Controls.Add(bttnLgn, 0, 13);
        loginProgress = new ProgressBar { Style = ProgressBarStyle.Marquee, MarqueeAnimationSpeed = 30, Dock = DockStyle.Fill, Margin = Padding.Empty, Visible = false, TabStop = false };
        card.Controls.Add(loginProgress, 0, 14);
        loginStatus = Caption("", Color.Firebrick, 9.5F); loginStatus.Padding = new Padding(0, 8, 0, 0); card.Controls.Add(loginStatus, 0, 15);
        connectionSettings = new Button { Text = "Bağlantı ayarları", Dock = DockStyle.Fill, FlatStyle = FlatStyle.Flat, ForeColor = RibbonPalette.Accent, BackColor = Color.White, Margin = Padding.Empty, Cursor = Cursors.Hand, TabIndex = 4 };
        connectionSettings.FlatAppearance.BorderSize = 0;
        connectionSettings.Click += (_, _) => { if (_loggingIn || AppConfiguration.DesignPreview) return; using var dialog = new ConnectionSettingsForm(); dialog.ShowDialog(this); };
        card.Controls.Add(connectionSettings, 0, 16);
        var version = Caption("Helm Software  •  BKS v1.1.4", ModernWinForms.Muted, 8.5F); version.TextAlign = ContentAlignment.BottomCenter; card.Controls.Add(version, 0, 17);
        Controls.Add(new LoginWorkspace(card)); AcceptButton = bttnLgn;
        Load += Form1_Load;
        Shown += (_, _) => { Screens.FitToScreen(this); if (string.IsNullOrWhiteSpace(userName.Text)) userName.Focus(); else passWord.Focus(); };
        ResumeLayout(true);
    }
    private void UpdateCapsWarning() => capsWarning.Text = IsKeyLocked(Keys.CapsLock) ? "Caps Lock açık" : "";
    private void SetLoginBusy(bool busy)
    {
        bttnLgn.Enabled = !busy; bttnLgn.Text = busy ? "Giriş yapılıyor…" : "Giriş yap";
        userName.Enabled = passWord.Enabled = cbxBeniHatirla.Enabled = showPassword.Enabled = connectionSettings.Enabled = !busy;
        loginProgress.Visible = busy;
    }
}
