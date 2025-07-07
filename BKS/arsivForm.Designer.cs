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
            this.cmbOgrenciler = new System.Windows.Forms.ComboBox();
            this.btnDosyaSec = new System.Windows.Forms.Button();
            this.txtDosyaYolu = new System.Windows.Forms.TextBox();
            this.btnYukle = new System.Windows.Forms.Button();
            this.dgvDosyalar = new System.Windows.Forms.DataGridView();
            this.btnIndir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDosyalar)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbOgrenciler
            // 
            this.cmbOgrenciler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOgrenciler.FormattingEnabled = true;
            this.cmbOgrenciler.Location = new System.Drawing.Point(20, 20);
            this.cmbOgrenciler.Name = "cmbOgrenciler";
            this.cmbOgrenciler.Size = new System.Drawing.Size(200, 24);
            this.cmbOgrenciler.TabIndex = 0;
            // 
            // btnDosyaSec
            // 
            this.btnDosyaSec.Location = new System.Drawing.Point(240, 20);
            this.btnDosyaSec.Name = "btnDosyaSec";
            this.btnDosyaSec.Size = new System.Drawing.Size(90, 24);
            this.btnDosyaSec.TabIndex = 1;
            this.btnDosyaSec.Text = "Dosya Seç";
            this.btnDosyaSec.UseVisualStyleBackColor = true;
            // 
            // txtDosyaYolu
            // 
            this.txtDosyaYolu.Location = new System.Drawing.Point(340, 20);
            this.txtDosyaYolu.Name = "txtDosyaYolu";
            this.txtDosyaYolu.ReadOnly = true;
            this.txtDosyaYolu.Size = new System.Drawing.Size(180, 22);
            this.txtDosyaYolu.TabIndex = 2;
            // 
            // btnYukle
            // 
            this.btnYukle.Location = new System.Drawing.Point(540, 20);
            this.btnYukle.Name = "btnYukle";
            this.btnYukle.Size = new System.Drawing.Size(90, 24);
            this.btnYukle.TabIndex = 3;
            this.btnYukle.Text = "Yükle";
            this.btnYukle.UseVisualStyleBackColor = true;
            // 
            // dgvDosyalar
            // 
            this.dgvDosyalar.AllowUserToAddRows = false;
            this.dgvDosyalar.AllowUserToDeleteRows = false;
            this.dgvDosyalar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDosyalar.Location = new System.Drawing.Point(20, 60);
            this.dgvDosyalar.Name = "dgvDosyalar";
            this.dgvDosyalar.ReadOnly = true;
            this.dgvDosyalar.RowTemplate.Height = 24;
            this.dgvDosyalar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDosyalar.Size = new System.Drawing.Size(610, 260);
            this.dgvDosyalar.TabIndex = 4;
            // 
            // btnIndir
            // 
            this.btnIndir.Location = new System.Drawing.Point(20, 330);
            this.btnIndir.Name = "btnIndir";
            this.btnIndir.Size = new System.Drawing.Size(160, 30);
            this.btnIndir.TabIndex = 5;
            this.btnIndir.Text = "Seçili Dosyayı İndir";
            this.btnIndir.UseVisualStyleBackColor = true;
            // 
            // arsivForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 380);
            this.Controls.Add(this.cmbOgrenciler);
            this.Controls.Add(this.btnDosyaSec);
            this.Controls.Add(this.txtDosyaYolu);
            this.Controls.Add(this.btnYukle);
            this.Controls.Add(this.dgvDosyalar);
            this.Controls.Add(this.btnIndir);
            this.Name = "arsivForm";
            this.Text = "Öğrenci Dosya Arşiv";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDosyalar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}