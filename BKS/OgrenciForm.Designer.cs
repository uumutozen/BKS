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
            ((System.ComponentModel.ISupportInitialize)numericPrice).BeginInit();
            SuspendLayout();
            // 
            // txtOgrenciAd
            // 
            txtOgrenciAd.Location = new Point(30, 30);
            txtOgrenciAd.Name = "txtOgrenciAd";
            txtOgrenciAd.Size = new Size(100, 23);
            txtOgrenciAd.TabIndex = 0;
            // 
            // textSoyad
            // 
            textSoyad.Location = new Point(30, 60);
            textSoyad.Name = "textSoyad";
            textSoyad.Size = new Size(100, 23);
            textSoyad.TabIndex = 1;
            // 
            // txtBabaAd
            // 
            txtBabaAd.Location = new Point(30, 90);
            txtBabaAd.Name = "txtBabaAd";
            txtBabaAd.Size = new Size(100, 23);
            txtBabaAd.TabIndex = 2;
            // 
            // txtAnneAd
            // 
            txtAnneAd.Location = new Point(30, 120);
            txtAnneAd.Name = "txtAnneAd";
            txtAnneAd.Size = new Size(100, 23);
            txtAnneAd.TabIndex = 3;
            // 
            // cmbogrsınıf
            // 
            cmbogrsınıf.Location = new Point(30, 150);
            cmbogrsınıf.Name = "cmbogrsınıf";
            cmbogrsınıf.Size = new Size(121, 23);
            cmbogrsınıf.TabIndex = 4;
            // 
            // textOgrenciKod
            // 
            textOgrenciKod.Location = new Point(30, 180);
            textOgrenciKod.Name = "textOgrenciKod";
            textOgrenciKod.Size = new Size(100, 23);
            textOgrenciKod.TabIndex = 5;
            // 
            // textOgrenciDetay
            // 
            textOgrenciDetay.Location = new Point(30, 210);
            textOgrenciDetay.Name = "textOgrenciDetay";
            textOgrenciDetay.Size = new Size(100, 23);
            textOgrenciDetay.TabIndex = 6;
            // 
            // txtBabaTel
            // 
            txtBabaTel.Location = new Point(30, 240);
            txtBabaTel.Name = "txtBabaTel";
            txtBabaTel.Size = new Size(100, 23);
            txtBabaTel.TabIndex = 7;
            // 
            // txtAnneTel
            // 
            txtAnneTel.Location = new Point(30, 270);
            txtAnneTel.Name = "txtAnneTel";
            txtAnneTel.Size = new Size(100, 23);
            txtAnneTel.TabIndex = 8;
            // 
            // txtBabaEvAdres
            // 
            txtBabaEvAdres.Location = new Point(30, 300);
            txtBabaEvAdres.Name = "txtBabaEvAdres";
            txtBabaEvAdres.Size = new Size(100, 96);
            txtBabaEvAdres.TabIndex = 9;
            txtBabaEvAdres.Text = "";
            // 
            // txtAnneEvAdres
            // 
            txtAnneEvAdres.Location = new Point(250, 300);
            txtAnneEvAdres.Name = "txtAnneEvAdres";
            txtAnneEvAdres.Size = new Size(100, 96);
            txtAnneEvAdres.TabIndex = 10;
            txtAnneEvAdres.Text = "";
            // 
            // numericPrice
            // 
            numericPrice.Location = new Point(30, 430);
            numericPrice.Name = "numericPrice";
            numericPrice.Size = new Size(120, 23);
            numericPrice.TabIndex = 11;
            // 
            // checkEvet
            // 
            checkEvet.Location = new Point(30, 460);
            checkEvet.Name = "checkEvet";
            checkEvet.Size = new Size(104, 24);
            checkEvet.TabIndex = 12;
            // 
            // checkOdemeDurum
            // 
            checkOdemeDurum.Location = new Point(30, 490);
            checkOdemeDurum.Name = "checkOdemeDurum";
            checkOdemeDurum.Size = new Size(104, 24);
            checkOdemeDurum.TabIndex = 13;
            // 
            // checkAktif
            // 
            checkAktif.Location = new Point(30, 520);
            checkAktif.Name = "checkAktif";
            checkAktif.Size = new Size(104, 24);
            checkAktif.TabIndex = 14;
            // 
            // dateDogum
            // 
            dateDogum.Location = new Point(30, 550);
            dateDogum.Name = "dateDogum";
            dateDogum.Size = new Size(200, 23);
            dateDogum.TabIndex = 15;
            // 
            // OgrenciForm
            // 
            ClientSize = new Size(1203, 631);
            Controls.Add(txtOgrenciAd);
            Controls.Add(textSoyad);
            Controls.Add(txtBabaAd);
            Controls.Add(txtAnneAd);
            Controls.Add(cmbogrsınıf);
            Controls.Add(textOgrenciKod);
            Controls.Add(textOgrenciDetay);
            Controls.Add(txtBabaTel);
            Controls.Add(txtAnneTel);
            Controls.Add(txtBabaEvAdres);
            Controls.Add(txtAnneEvAdres);
            Controls.Add(numericPrice);
            Controls.Add(checkEvet);
            Controls.Add(checkOdemeDurum);
            Controls.Add(checkAktif);
            Controls.Add(dateDogum);
            Name = "OgrenciForm";
            Text = "Öğrenci Bilgi Formu";
            ((System.ComponentModel.ISupportInitialize)numericPrice).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }

}