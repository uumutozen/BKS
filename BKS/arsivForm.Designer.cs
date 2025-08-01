namespace BKS
{
    partial class arsivForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblOgrenci;
        private System.Windows.Forms.ComboBox cmbOgrenciler;
        private System.Windows.Forms.Button btnDosyaSec;
        private System.Windows.Forms.TextBox txtDosyaYolu;
        private System.Windows.Forms.Button btnYukle;
        private System.Windows.Forms.DataGridView dgvDosyalar;
        private System.Windows.Forms.Button btnIndir;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            lblOgrenci = new Label();
            cmbOgrenciler = new ComboBox();
            btnDosyaSec = new Button();
            txtDosyaYolu = new TextBox();
            btnYukle = new Button();
            dgvDosyalar = new DataGridView();
            btnIndir = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDosyalar).BeginInit();
            SuspendLayout();
            // 
            // lblOgrenci
            // 
            lblOgrenci.AutoSize = true;
            lblOgrenci.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblOgrenci.Location = new Point(227, 35);
            lblOgrenci.Name = "lblOgrenci";
            lblOgrenci.Size = new Size(71, 20);
            lblOgrenci.TabIndex = 0;
            lblOgrenci.Text = "Öğrenci :";
            // 
            // cmbOgrenciler
            // 
            cmbOgrenciler.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOgrenciler.Font = new Font("Segoe UI", 11F);
            cmbOgrenciler.FormattingEnabled = true;
            cmbOgrenciler.Location = new Point(327, 32);
            cmbOgrenciler.Name = "cmbOgrenciler";
            cmbOgrenciler.Size = new Size(300, 28);
            cmbOgrenciler.TabIndex = 1;
            cmbOgrenciler.SelectedIndexChanged += cmbOgrenciler_SelectedIndexChanged_1;
            // 
            // btnDosyaSec
            // 
            btnDosyaSec.BackColor = Color.LightSlateGray;
            btnDosyaSec.FlatAppearance.BorderSize = 0;
            btnDosyaSec.FlatStyle = FlatStyle.Flat;
            btnDosyaSec.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnDosyaSec.ForeColor = Color.White;
            btnDosyaSec.Location = new Point(710, 31);
            btnDosyaSec.Name = "btnDosyaSec";
            btnDosyaSec.Size = new Size(120, 28);
            btnDosyaSec.TabIndex = 2;
            btnDosyaSec.Text = "Dosya Seç";
            btnDosyaSec.UseVisualStyleBackColor = false;
            // 
            // txtDosyaYolu
            // 
            txtDosyaYolu.Font = new Font("Segoe UI", 10F);
            txtDosyaYolu.Location = new Point(35, 80);
            txtDosyaYolu.Name = "txtDosyaYolu";
            txtDosyaYolu.ReadOnly = true;
            txtDosyaYolu.Size = new Size(669, 25);
            txtDosyaYolu.TabIndex = 3;
            // 
            // btnYukle
            // 
            btnYukle.BackColor = Color.MediumSeaGreen;
            btnYukle.FlatAppearance.BorderSize = 0;
            btnYukle.FlatStyle = FlatStyle.Flat;
            btnYukle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnYukle.ForeColor = Color.White;
            btnYukle.Location = new Point(710, 77);
            btnYukle.Name = "btnYukle";
            btnYukle.Size = new Size(120, 28);
            btnYukle.TabIndex = 4;
            btnYukle.Text = "Yükle";
            btnYukle.UseVisualStyleBackColor = false;
            // 
            // dgvDosyalar
            // 
            dgvDosyalar.AllowUserToAddRows = false;
            dgvDosyalar.AllowUserToDeleteRows = false;
            dgvDosyalar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDosyalar.BackgroundColor = Color.WhiteSmoke;
            dgvDosyalar.BorderStyle = BorderStyle.None;
            dgvDosyalar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.DarkSlateGray;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvDosyalar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = Color.PaleTurquoise;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvDosyalar.DefaultCellStyle = dataGridViewCellStyle4;
            dgvDosyalar.GridColor = Color.Gainsboro;
            dgvDosyalar.Location = new Point(35, 130);
            dgvDosyalar.Name = "dgvDosyalar";
            dgvDosyalar.ReadOnly = true;
            dgvDosyalar.RowTemplate.Height = 28;
            dgvDosyalar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDosyalar.Size = new Size(906, 270);
            dgvDosyalar.TabIndex = 5;
            // 
            // btnIndir
            // 
            btnIndir.BackColor = Color.SteelBlue;
            btnIndir.FlatAppearance.BorderSize = 0;
            btnIndir.FlatStyle = FlatStyle.Flat;
            btnIndir.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnIndir.ForeColor = Color.White;
            btnIndir.Location = new Point(35, 420);
            btnIndir.Name = "btnIndir";
            btnIndir.Size = new Size(200, 36);
            btnIndir.TabIndex = 6;
            btnIndir.Text = "Seçili Dosyayı İndir";
            btnIndir.UseVisualStyleBackColor = false;
            // 
            // arsivForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(976, 480);
            Controls.Add(lblOgrenci);
            Controls.Add(cmbOgrenciler);
            Controls.Add(btnDosyaSec);
            Controls.Add(txtDosyaYolu);
            Controls.Add(btnYukle);
            Controls.Add(dgvDosyalar);
            Controls.Add(btnIndir);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "arsivForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Öğrenci Dosya Arşivi";
            ((System.ComponentModel.ISupportInitialize)dgvDosyalar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
