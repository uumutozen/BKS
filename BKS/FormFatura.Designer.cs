namespace BKS
{
    partial class FormFatura
    {
        /// <summary>
        /// Designer tarafından gerekli değişkenler
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Kaynakları temizle
        /// </summary>
        /// <param name="disposing">yönetilen kaynaklar dispose edilecek mi?</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Form tasarımı
        /// </summary>
        private void InitializeComponent()
        {
            this.txtFaturaNo = new System.Windows.Forms.TextBox();
            this.txtAliciUnvan = new System.Windows.Forms.TextBox();
            this.txtAliciVkn = new System.Windows.Forms.TextBox();
            this.dtTarih = new System.Windows.Forms.DateTimePicker();
            this.dgKalemler = new System.Windows.Forms.DataGridView();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.dgFaturalar = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgKalemler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFaturalar)).BeginInit();
            this.SuspendLayout();

            // txtFaturaNo
            this.txtFaturaNo.Location = new System.Drawing.Point(100, 20);
            this.txtFaturaNo.Name = "txtFaturaNo";
            this.txtFaturaNo.Size = new System.Drawing.Size(200, 22);

            // txtAliciUnvan
            this.txtAliciUnvan.Location = new System.Drawing.Point(100, 50);
            this.txtAliciUnvan.Name = "txtAliciUnvan";
            this.txtAliciUnvan.Size = new System.Drawing.Size(200, 22);

            // txtAliciVkn
            this.txtAliciVkn.Location = new System.Drawing.Point(100, 80);
            this.txtAliciVkn.Name = "txtAliciVkn";
            this.txtAliciVkn.Size = new System.Drawing.Size(200, 22);

            // dtTarih
            this.dtTarih.Location = new System.Drawing.Point(100, 110);
            this.dtTarih.Name = "dtTarih";
            this.dtTarih.Size = new System.Drawing.Size(200, 22);

            // dgKalemler
            this.dgKalemler.Location = new System.Drawing.Point(20, 150);
            this.dgKalemler.Name = "dgKalemler";
            this.dgKalemler.Size = new System.Drawing.Size(500, 150);
            this.dgKalemler.AllowUserToAddRows = true;
            this.dgKalemler.AllowUserToDeleteRows = true;

            // btnKaydet
            this.btnKaydet.Location = new System.Drawing.Point(350, 110);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(150, 30);
            this.btnKaydet.Text = "Kaydet ve PDF Oluştur";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);

            // dgFaturalar
            this.dgFaturalar.Location = new System.Drawing.Point(20, 320);
            this.dgFaturalar.Name = "dgFaturalar";
            this.dgFaturalar.Size = new System.Drawing.Size(500, 150);
            this.dgFaturalar.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFaturalar_CellDoubleClick);

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Text = "Fatura No:";

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 53);
            this.label2.Text = "Alıcı Ünvan:";

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 83);
            this.label3.Text = "Alıcı VKN:";

            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 113);
            this.label4.Text = "Tarih:";

            // Form1
            this.ClientSize = new System.Drawing.Size(550, 500);
            this.Controls.Add(this.txtFaturaNo);
            this.Controls.Add(this.txtAliciUnvan);
            this.Controls.Add(this.txtAliciVkn);
            this.Controls.Add(this.dtTarih);
            this.Controls.Add(this.dgKalemler);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.dgFaturalar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Name = "Form1";
            this.Text = "E-Fatura Modülü";
            this.Load += new System.EventHandler(this.FormFatura_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgKalemler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFaturalar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtFaturaNo;
        private System.Windows.Forms.TextBox txtAliciUnvan;
        private System.Windows.Forms.TextBox txtAliciVkn;
        private System.Windows.Forms.DateTimePicker dtTarih;
        private System.Windows.Forms.DataGridView dgKalemler;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.DataGridView dgFaturalar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}