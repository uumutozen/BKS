namespace BKS
{
    partial class arsivForm
    {
        private System.ComponentModel.IContainer components = null;

        // Kontroller
        private System.Windows.Forms.ComboBox cmbOgrenciler;
        private System.Windows.Forms.Button btnDosyaSec;
        private System.Windows.Forms.TextBox txtDosyaYolu;
        private System.Windows.Forms.Button btnYukle;
        private System.Windows.Forms.DataGridView dgvDosyalar;
        private System.Windows.Forms.Button btnIndir;

        /// <summary>
        /// Temizle.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Designer kodu
        /// </summary>
        private void InitializeComponent()
        {
            cmbOgrenciler = new ComboBox();
            btnDosyaSec = new Button();
            txtDosyaYolu = new TextBox();
            btnYukle = new Button();
            dgvDosyalar = new DataGridView();
            btnIndir = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDosyalar).BeginInit();
            SuspendLayout();
            // 
            // cmbOgrenciler
            // 
            cmbOgrenciler.DropDownStyle = ComboBoxStyle.Simple;
            cmbOgrenciler.Enabled = false;
            cmbOgrenciler.FormattingEnabled = true;
            cmbOgrenciler.Location = new Point(20, 20);
            cmbOgrenciler.Name = "cmbOgrenciler";
            cmbOgrenciler.Size = new Size(200, 24);
            cmbOgrenciler.TabIndex = 0;
            // 
            // btnDosyaSec
            // 
            btnDosyaSec.Location = new Point(240, 20);
            btnDosyaSec.Name = "btnDosyaSec";
            btnDosyaSec.Size = new Size(90, 24);
            btnDosyaSec.TabIndex = 1;
            btnDosyaSec.Text = "Dosya Seç";
            btnDosyaSec.UseVisualStyleBackColor = true;
            // 
            // txtDosyaYolu
            // 
            txtDosyaYolu.Location = new Point(340, 20);
            txtDosyaYolu.Name = "txtDosyaYolu";
            txtDosyaYolu.ReadOnly = true;
            txtDosyaYolu.Size = new Size(180, 23);
            txtDosyaYolu.TabIndex = 2;
            // 
            // btnYukle
            // 
            btnYukle.Location = new Point(540, 20);
            btnYukle.Name = "btnYukle";
            btnYukle.Size = new Size(90, 24);
            btnYukle.TabIndex = 3;
            btnYukle.Text = "Yükle";
            btnYukle.UseVisualStyleBackColor = true;
            // 
            // dgvDosyalar
            // 
            dgvDosyalar.AllowUserToAddRows = false;
            dgvDosyalar.AllowUserToDeleteRows = false;
            dgvDosyalar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDosyalar.Location = new Point(20, 60);
            dgvDosyalar.Name = "dgvDosyalar";
            dgvDosyalar.ReadOnly = true;
            dgvDosyalar.RowTemplate.Height = 24;
            dgvDosyalar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDosyalar.Size = new Size(610, 260);
            dgvDosyalar.TabIndex = 4;
            // 
            // btnIndir
            // 
            btnIndir.Location = new Point(20, 330);
            btnIndir.Name = "btnIndir";
            btnIndir.Size = new Size(160, 30);
            btnIndir.TabIndex = 5;
            btnIndir.Text = "Seçili Dosyayı İndir";
            btnIndir.UseVisualStyleBackColor = true;
            // 
            // arsivForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 380);
            Controls.Add(cmbOgrenciler);
            Controls.Add(btnDosyaSec);
            Controls.Add(txtDosyaYolu);
            Controls.Add(btnYukle);
            Controls.Add(dgvDosyalar);
            Controls.Add(btnIndir);
            Name = "arsivForm";
            Text = "Öğrenci Dosya Arşiv";
            ((System.ComponentModel.ISupportInitialize)dgvDosyalar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}