

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
            bttnLgn = new MaterialSkin.Controls.MaterialButton();
            passWord = new MaterialSkin.Controls.MaterialTextBox();
            userName = new MaterialSkin.Controls.MaterialTextBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // bttnLgn
            // 
            bttnLgn.AutoSize = false;
            bttnLgn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnLgn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnLgn.Depth = 0;
            bttnLgn.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            bttnLgn.HighEmphasis = true;
            bttnLgn.Icon = null;
            bttnLgn.Location = new Point(684, 335);
            bttnLgn.Margin = new Padding(4, 6, 4, 6);
            bttnLgn.MouseState = MaterialSkin.MouseState.HOVER;
            bttnLgn.Name = "bttnLgn";
            bttnLgn.NoAccentTextColor = Color.Empty;
            bttnLgn.Size = new Size(96, 36);
            bttnLgn.TabIndex = 3;
            bttnLgn.Text = "Giriş Yap";
            bttnLgn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnLgn.UseAccentColor = false;
            bttnLgn.UseVisualStyleBackColor = true;
            bttnLgn.Click += bttnLgn_Click;
            // 
            // passWord
            // 
            passWord.AnimateReadOnly = false;
            passWord.BorderStyle = BorderStyle.None;
            passWord.Depth = 0;
            passWord.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            passWord.LeadingIcon = null;
            passWord.Location = new Point(6, 24);
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
            userName.Location = new Point(6, 24);
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
            groupBox1.Controls.Add(userName);
            groupBox1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            groupBox1.Location = new Point(453, 129);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 79);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Kullanıcı Adı";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(passWord);
            groupBox2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            groupBox2.Location = new Point(453, 217);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 79);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Şifre";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(bttnLgn);
            Name = "Form1";
            Text = "Anaokulu Yönetim Sistemi";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private MaterialSkin.Controls.MaterialButton bttnLgn;
        private MaterialSkin.Controls.MaterialTextBox passWord;
        private MaterialSkin.Controls.MaterialTextBox userName;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
    }
}
