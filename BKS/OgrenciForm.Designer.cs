namespace BKS
{
    partial class OgrenciForm
    {
        private System.ComponentModel.IContainer components = null;

        public System.Windows.Forms.TextBox txtOgrenciAd;
        public System.Windows.Forms.TextBox textSoyad;
        public System.Windows.Forms.TextBox txtBabaAd;
        public System.Windows.Forms.TextBox txtAnneAd;
        public System.Windows.Forms.ComboBox cmbogrsınıf;
        public System.Windows.Forms.TextBox textOgrenciKod;
        public System.Windows.Forms.TextBox textOgrenciDetay;
        public System.Windows.Forms.MaskedTextBox txtBabaTel;
        public System.Windows.Forms.MaskedTextBox txtAnneTel;
        public System.Windows.Forms.RichTextBox txtBabaEvAdres;
        public System.Windows.Forms.RichTextBox txtAnneEvAdres;
        public System.Windows.Forms.NumericUpDown numericPrice;
        public System.Windows.Forms.CheckBox checkEvet;
        public System.Windows.Forms.CheckBox checkOdemeDurum;
        public System.Windows.Forms.CheckBox checkAktif;
        public System.Windows.Forms.DateTimePicker dateDogum;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            txtOgrenciAd = new TextBox();
            textSoyad = new TextBox();
            txtBabaAd = new TextBox();
            txtAnneAd = new TextBox();
            cmbogrsınıf = new ComboBox();
            textOgrenciKod = new TextBox();
            textOgrenciDetay = new TextBox();
            txtBabaTel = new MaskedTextBox();
            txtAnneTel = new MaskedTextBox();
            txtBabaEvAdres = new RichTextBox();
            txtAnneEvAdres = new RichTextBox();
            numericPrice = new NumericUpDown();
            checkEvet = new CheckBox();
            checkOdemeDurum = new CheckBox();
            checkAktif = new CheckBox();
            dateDogum = new DateTimePicker();
            groupBox1 = new GroupBox();
            pictureBox1 = new PictureBox();
            groupBox2 = new GroupBox();
            groupBox10 = new GroupBox();
            groupBox7 = new GroupBox();
            groupBox6 = new GroupBox();
            groupBox12 = new GroupBox();
            btnGuncelle = new Button();
            btnOgrenciYonetimiSil = new Button();
            btnAddStock = new Button();
            ((System.ComponentModel.ISupportInitialize)numericPrice).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox2.SuspendLayout();
            groupBox10.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox12.SuspendLayout();
            SuspendLayout();
            // 
            // txtOgrenciAd
            // 
            txtOgrenciAd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtOgrenciAd.ForeColor = SystemColors.WindowFrame;
            txtOgrenciAd.Location = new Point(171, 25);
            txtOgrenciAd.Name = "txtOgrenciAd";
            txtOgrenciAd.PlaceholderText = "Öğrenci Adı";
            txtOgrenciAd.Size = new Size(259, 23);
            txtOgrenciAd.TabIndex = 0;
            txtOgrenciAd.TextChanged += txtOgrenciAd_TextChanged;
            // 
            // textSoyad
            // 
            textSoyad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            textSoyad.ForeColor = SystemColors.WindowFrame;
            textSoyad.Location = new Point(171, 54);
            textSoyad.Name = "textSoyad";
            textSoyad.PlaceholderText = "Öğrenci Soyadı";
            textSoyad.Size = new Size(259, 23);
            textSoyad.TabIndex = 1;
            // 
            // txtBabaAd
            // 
            txtBabaAd.ForeColor = SystemColors.WindowFrame;
            txtBabaAd.Location = new Point(6, 38);
            txtBabaAd.Name = "txtBabaAd";
            txtBabaAd.PlaceholderText = "Adı Soyadı";
            txtBabaAd.Size = new Size(200, 23);
            txtBabaAd.TabIndex = 2;
            // 
            // txtAnneAd
            // 
            txtAnneAd.ForeColor = SystemColors.WindowFrame;
            txtAnneAd.Location = new Point(6, 38);
            txtAnneAd.Name = "txtAnneAd";
            txtAnneAd.PlaceholderText = "Adı Soyadı";
            txtAnneAd.Size = new Size(206, 23);
            txtAnneAd.TabIndex = 3;
            // 
            // cmbogrsınıf
            // 
            cmbogrsınıf.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbogrsınıf.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cmbogrsınıf.ForeColor = SystemColors.WindowFrame;
            cmbogrsınıf.Location = new Point(171, 112);
            cmbogrsınıf.Name = "cmbogrsınıf";
            cmbogrsınıf.Size = new Size(259, 23);
            cmbogrsınıf.TabIndex = 4;
            // 
            // textOgrenciKod
            // 
            textOgrenciKod.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            textOgrenciKod.ForeColor = SystemColors.WindowFrame;
            textOgrenciKod.Location = new Point(171, 141);
            textOgrenciKod.Name = "textOgrenciKod";
            textOgrenciKod.PlaceholderText = "Öğrenci Numarası";
            textOgrenciKod.Size = new Size(259, 23);
            textOgrenciKod.TabIndex = 5;
            // 
            // textOgrenciDetay
            // 
            textOgrenciDetay.Location = new Point(436, 25);
            textOgrenciDetay.Multiline = true;
            textOgrenciDetay.Name = "textOgrenciDetay";
            textOgrenciDetay.Size = new Size(529, 139);
            textOgrenciDetay.TabIndex = 6;
            // 
            // txtBabaTel
            // 
            txtBabaTel.ForeColor = SystemColors.WindowFrame;
            txtBabaTel.Location = new Point(250, 38);
            txtBabaTel.Mask = "(999) 000-0000";
            txtBabaTel.Name = "txtBabaTel";
            txtBabaTel.Size = new Size(200, 23);
            txtBabaTel.TabIndex = 7;
            // 
            // txtAnneTel
            // 
            txtAnneTel.ForeColor = SystemColors.WindowFrame;
            txtAnneTel.Location = new Point(244, 38);
            txtAnneTel.Mask = "(999) 000-0000";
            txtAnneTel.Name = "txtAnneTel";
            txtAnneTel.Size = new Size(206, 23);
            txtAnneTel.TabIndex = 8;
            // 
            // txtBabaEvAdres
            // 
            txtBabaEvAdres.ForeColor = SystemColors.WindowFrame;
            txtBabaEvAdres.Location = new Point(6, 86);
            txtBabaEvAdres.Name = "txtBabaEvAdres";
            txtBabaEvAdres.Size = new Size(444, 92);
            txtBabaEvAdres.TabIndex = 9;
            txtBabaEvAdres.Text = "Ev Adresi";
            // 
            // txtAnneEvAdres
            // 
            txtAnneEvAdres.ForeColor = SystemColors.WindowFrame;
            txtAnneEvAdres.Location = new Point(6, 86);
            txtAnneEvAdres.Name = "txtAnneEvAdres";
            txtAnneEvAdres.Size = new Size(444, 92);
            txtAnneEvAdres.TabIndex = 10;
            txtAnneEvAdres.Text = "Ev Adresi";
            // 
            // numericPrice
            // 
            numericPrice.Location = new Point(15, 41);
            numericPrice.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            numericPrice.Name = "numericPrice";
            numericPrice.Size = new Size(155, 23);
            numericPrice.TabIndex = 11;
            // 
            // checkEvet
            // 
            checkEvet.Location = new Point(17, 41);
            checkEvet.Name = "checkEvet";
            checkEvet.Size = new Size(104, 24);
            checkEvet.TabIndex = 12;
            checkEvet.Text = "Evet";
            // 
            // checkOdemeDurum
            // 
            checkOdemeDurum.Location = new Point(129, 41);
            checkOdemeDurum.Name = "checkOdemeDurum";
            checkOdemeDurum.Size = new Size(89, 24);
            checkOdemeDurum.TabIndex = 13;
            checkOdemeDurum.Text = "Ödendi";
            // 
            // checkAktif
            // 
            checkAktif.Location = new Point(21, 41);
            checkAktif.Name = "checkAktif";
            checkAktif.Size = new Size(92, 24);
            checkAktif.TabIndex = 14;
            checkAktif.Text = "Evet";
            // 
            // dateDogum
            // 
            dateDogum.CalendarForeColor = SystemColors.WindowFrame;
            dateDogum.CalendarTitleForeColor = SystemColors.WindowFrame;
            dateDogum.Location = new Point(171, 83);
            dateDogum.Name = "dateDogum";
            dateDogum.Size = new Size(259, 23);
            dateDogum.TabIndex = 15;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Controls.Add(txtOgrenciAd);
            groupBox1.Controls.Add(textSoyad);
            groupBox1.Controls.Add(textOgrenciDetay);
            groupBox1.Controls.Add(textOgrenciKod);
            groupBox1.Controls.Add(cmbogrsınıf);
            groupBox1.Controls.Add(dateDogum);
            groupBox1.ForeColor = Color.CornflowerBlue;
            groupBox1.Location = new Point(6, 67);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(997, 187);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            groupBox1.Text = "Öğrenci Bilgileri";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.AppWorkspace;
            pictureBox1.Location = new Point(6, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(144, 159);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtBabaEvAdres);
            groupBox2.Controls.Add(txtBabaAd);
            groupBox2.Controls.Add(txtBabaTel);
            groupBox2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox2.ForeColor = Color.CornflowerBlue;
            groupBox2.Location = new Point(6, 260);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(482, 184);
            groupBox2.TabIndex = 17;
            groupBox2.TabStop = false;
            groupBox2.Text = "Öğrenci Baba Bilgileri";
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(txtAnneEvAdres);
            groupBox10.Controls.Add(txtAnneAd);
            groupBox10.Controls.Add(txtAnneTel);
            groupBox10.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox10.ForeColor = Color.CornflowerBlue;
            groupBox10.Location = new Point(521, 260);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(482, 184);
            groupBox10.TabIndex = 24;
            groupBox10.TabStop = false;
            groupBox10.Text = "Öğrenci Anne Bilgileri";
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(checkAktif);
            groupBox7.Controls.Add(checkOdemeDurum);
            groupBox7.FlatStyle = FlatStyle.Popup;
            groupBox7.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox7.ForeColor = Color.CornflowerBlue;
            groupBox7.Location = new Point(194, 489);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(233, 93);
            groupBox7.TabIndex = 26;
            groupBox7.TabStop = false;
            groupBox7.Text = "Aktif mi ? Ödeme Durumu ?";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(numericPrice);
            groupBox6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox6.ForeColor = Color.CornflowerBlue;
            groupBox6.Location = new Point(452, 489);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(203, 93);
            groupBox6.TabIndex = 25;
            groupBox6.TabStop = false;
            groupBox6.Text = "Öğrenci Ödeme Tutarı";
            // 
            // groupBox12
            // 
            groupBox12.Controls.Add(checkEvet);
            groupBox12.FlatStyle = FlatStyle.Popup;
            groupBox12.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox12.ForeColor = Color.CornflowerBlue;
            groupBox12.Location = new Point(6, 489);
            groupBox12.Name = "groupBox12";
            groupBox12.Size = new Size(167, 93);
            groupBox12.TabIndex = 27;
            groupBox12.TabStop = false;
            groupBox12.Text = "Aile Ayrı mı ?";
            // 
            // btnGuncelle
            // 
            btnGuncelle.BackColor = SystemColors.MenuHighlight;
            btnGuncelle.Cursor = Cursors.Hand;
            btnGuncelle.FlatAppearance.BorderSize = 0;
            btnGuncelle.FlatStyle = FlatStyle.Flat;
            btnGuncelle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGuncelle.ForeColor = Color.White;
            btnGuncelle.Location = new Point(912, 560);
            btnGuncelle.Name = "btnGuncelle";
            btnGuncelle.Size = new Size(114, 43);
            btnGuncelle.TabIndex = 30;
            btnGuncelle.Text = "Güncelle";
            btnGuncelle.UseVisualStyleBackColor = false;
            btnGuncelle.Click += btnGuncelle_Click;
            // 
            // btnOgrenciYonetimiSil
            // 
            btnOgrenciYonetimiSil.BackColor = SystemColors.MenuHighlight;
            btnOgrenciYonetimiSil.Cursor = Cursors.Hand;
            btnOgrenciYonetimiSil.FlatAppearance.BorderSize = 0;
            btnOgrenciYonetimiSil.FlatStyle = FlatStyle.Flat;
            btnOgrenciYonetimiSil.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOgrenciYonetimiSil.ForeColor = Color.White;
            btnOgrenciYonetimiSil.Location = new Point(672, 560);
            btnOgrenciYonetimiSil.Name = "btnOgrenciYonetimiSil";
            btnOgrenciYonetimiSil.Size = new Size(114, 43);
            btnOgrenciYonetimiSil.TabIndex = 29;
            btnOgrenciYonetimiSil.Text = "Sil";
            btnOgrenciYonetimiSil.UseVisualStyleBackColor = false;
            btnOgrenciYonetimiSil.Click += btnOgrenciYonetimiSil_Click;
            // 
            // btnAddStock
            // 
            btnAddStock.BackColor = SystemColors.MenuHighlight;
            btnAddStock.Cursor = Cursors.Hand;
            btnAddStock.FlatAppearance.BorderSize = 0;
            btnAddStock.FlatStyle = FlatStyle.Flat;
            btnAddStock.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddStock.ForeColor = Color.White;
            btnAddStock.Location = new Point(792, 560);
            btnAddStock.Name = "btnAddStock";
            btnAddStock.Size = new Size(114, 43);
            btnAddStock.TabIndex = 28;
            btnAddStock.Text = "Kaydet";
            btnAddStock.UseVisualStyleBackColor = false;
            btnAddStock.Click += btnAddStock_Click;
            // 
            // OgrenciForm
            // 
            ClientSize = new Size(1039, 653);
            Controls.Add(btnGuncelle);
            Controls.Add(btnOgrenciYonetimiSil);
            Controls.Add(btnAddStock);
            Controls.Add(groupBox12);
            Controls.Add(groupBox7);
            Controls.Add(groupBox6);
            Controls.Add(groupBox10);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "OgrenciForm";
            Text = "Öğrenci Bilgi Formu";
            Load += OgrenciForm_Load;
            ((System.ComponentModel.ISupportInitialize)numericPrice).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox10.ResumeLayout(false);
            groupBox10.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox12.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        public PictureBox pictureBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox10;
        private GroupBox groupBox7;
        private GroupBox groupBox6;
        private GroupBox groupBox12;
        private Button btnGuncelle;
        private Button btnOgrenciYonetimiSil;
        private Button btnAddStock;
    }

}