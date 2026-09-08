<<<<<<< HEAD
#nullable enable
namespace BKS;
partial class Form1
=======


namespace BKS
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
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
<<<<<<< HEAD
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
=======
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            bttnLgn = new MaterialSkin.Controls.MaterialButton();
            passWord = new MaterialSkin.Controls.MaterialTextBox();
            userName = new MaterialSkin.Controls.MaterialTextBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            pictureBox1 = new PictureBox();
            cbxBeniHatirla = new MaterialSkin.Controls.MaterialCheckbox();
            groupBox3 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // bttnLgn
            // 
            bttnLgn.AutoSize = false;
            bttnLgn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnLgn.BackColor = SystemColors.ActiveCaption;
            bttnLgn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnLgn.Depth = 0;
            bttnLgn.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            bttnLgn.ForeColor = SystemColors.ButtonHighlight;
            bttnLgn.HighEmphasis = true;
            bttnLgn.Icon = null;
            bttnLgn.Location = new Point(663, 170);
            bttnLgn.Margin = new Padding(4, 6, 4, 6);
            bttnLgn.MouseState = MaterialSkin.MouseState.HOVER;
            bttnLgn.Name = "bttnLgn";
            bttnLgn.NoAccentTextColor = Color.Empty;
            bttnLgn.Size = new Size(117, 50);
            bttnLgn.TabIndex = 3;
            bttnLgn.Text = "Giriş Yap";
            bttnLgn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnLgn.UseAccentColor = false;
            bttnLgn.UseVisualStyleBackColor = false;
            bttnLgn.Click += bttnLgn_Click;
            // 
            // passWord
            // 
            passWord.AnimateReadOnly = false;
            passWord.BorderStyle = BorderStyle.None;
            passWord.Depth = 0;
            passWord.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            passWord.LeadingIcon = null;
            passWord.Location = new Point(6, 23);
            passWord.MaxLength = 50;
            passWord.MouseState = MaterialSkin.MouseState.OUT;
            passWord.Multiline = false;
            passWord.Name = "passWord";
            passWord.Password = true;
            passWord.Size = new Size(188, 50);
            passWord.TabIndex = 4;
            passWord.Text = "";
            passWord.TrailingIcon = null;
            // 
            // userName
            // 
            userName.AnimateReadOnly = false;
            userName.BorderStyle = BorderStyle.None;
            userName.Depth = 0;
            userName.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            userName.LeadingIcon = null;
            userName.Location = new Point(6, 23);
            userName.MaxLength = 50;
            userName.MouseState = MaterialSkin.MouseState.OUT;
            userName.Multiline = false;
            userName.Name = "userName";
            userName.Size = new Size(188, 50);
            userName.TabIndex = 5;
            userName.Text = "";
            userName.TrailingIcon = null;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(userName);
            groupBox1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(435, 110);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 79);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Kullanıcı Adı";
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.White;
            groupBox2.Controls.Add(passWord);
            groupBox2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            groupBox2.ForeColor = SystemColors.Control;
            groupBox2.Location = new Point(435, 195);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 79);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Şifre";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(6, 67);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 460);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // cbxBeniHatirla
            // 
            cbxBeniHatirla.AutoSize = true;
            cbxBeniHatirla.Depth = 0;
            cbxBeniHatirla.Location = new Point(33, 23);
            cbxBeniHatirla.Margin = new Padding(0);
            cbxBeniHatirla.MouseLocation = new Point(-1, -1);
            cbxBeniHatirla.MouseState = MaterialSkin.MouseState.HOVER;
            cbxBeniHatirla.Name = "cbxBeniHatirla";
            cbxBeniHatirla.ReadOnly = false;
            cbxBeniHatirla.Ripple = true;
            cbxBeniHatirla.Size = new Size(35, 37);
            cbxBeniHatirla.TabIndex = 5;
            cbxBeniHatirla.UseVisualStyleBackColor = true;
            cbxBeniHatirla.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // groupBox3
            // 
            groupBox3.BackColor = Color.White;
            groupBox3.Controls.Add(cbxBeniHatirla);
            groupBox3.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            groupBox3.ForeColor = SystemColors.Control;
            groupBox3.Location = new Point(470, 289);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(106, 69);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            groupBox3.Text = "Beni Hatırla";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(812, 533);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(bttnLgn);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Anaokulu Yönetim Sistemi";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private MaterialSkin.Controls.MaterialButton bttnLgn;
        private MaterialSkin.Controls.MaterialTextBox passWord;
        private MaterialSkin.Controls.MaterialTextBox userName;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private PictureBox pictureBox1;
        private MaterialSkin.Controls.MaterialCheckbox cbxBeniHatirla;
        private GroupBox groupBox3;
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
    }
}
