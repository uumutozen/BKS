

namespace BKS
{
    partial class Form1
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            bttnLgn.BackgroundImage = (Image)resources.GetObject("bttnLgn.BackgroundImage");
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
            groupBox1.BackgroundImage = (Image)resources.GetObject("groupBox1.BackgroundImage");
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
            groupBox2.BackgroundImage = (Image)resources.GetObject("groupBox2.BackgroundImage");
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
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
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
            groupBox3.BackgroundImage = (Image)resources.GetObject("groupBox3.BackgroundImage");
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
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
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
    }
}
