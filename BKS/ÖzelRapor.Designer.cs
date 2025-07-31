namespace BKS
{
    partial class ÖzelRapor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblRaporAdi;
        private System.Windows.Forms.TextBox txtRaporAdi;
        private System.Windows.Forms.Button btnRaporKaydet;
        private System.Windows.Forms.Label lblOzelRaporlar;
        private System.Windows.Forms.ComboBox cmbOzelRaporlar;
        private System.Windows.Forms.Button btnRaporYukle;
        private System.Windows.Forms.Label lblQuery;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Button btnGenerateFields;
        private System.Windows.Forms.Button btnRunQuery;
        private System.Windows.Forms.Label lblParams;
        private System.Windows.Forms.Panel panelParametreler;
        private System.Windows.Forms.DataGridView dataGridView1;

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
            lblRaporAdi = new Label();
            txtRaporAdi = new TextBox();
            btnRaporKaydet = new Button();
            lblOzelRaporlar = new Label();
            cmbOzelRaporlar = new ComboBox();
            btnRaporYukle = new Button();
            lblQuery = new Label();
            txtQuery = new TextBox();
            btnGenerateFields = new Button();
            btnRunQuery = new Button();
            lblParams = new Label();
            panelParametreler = new Panel();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lblRaporAdi
            // 
            lblRaporAdi.AutoSize = true;
            lblRaporAdi.Location = new Point(19, 14);
            lblRaporAdi.Name = "lblRaporAdi";
            lblRaporAdi.Size = new Size(62, 15);
            lblRaporAdi.TabIndex = 0;
            lblRaporAdi.Text = "Rapor Adı:";
            // 
            // txtRaporAdi
            // 
            txtRaporAdi.Location = new Point(96, 11);
            txtRaporAdi.Margin = new Padding(3, 2, 3, 2);
            txtRaporAdi.Name = "txtRaporAdi";
            txtRaporAdi.Size = new Size(184, 23);
            txtRaporAdi.TabIndex = 1;
            // 
            // btnRaporKaydet
            // 
            btnRaporKaydet.Location = new Point(298, 10);
            btnRaporKaydet.Margin = new Padding(3, 2, 3, 2);
            btnRaporKaydet.Name = "btnRaporKaydet";
            btnRaporKaydet.Size = new Size(105, 24);
            btnRaporKaydet.TabIndex = 2;
            btnRaporKaydet.Text = "Raporu Kaydet";
            btnRaporKaydet.UseVisualStyleBackColor = true;
            // 
            // lblOzelRaporlar
            // 
            lblOzelRaporlar.AutoSize = true;
            lblOzelRaporlar.Location = new Point(420, 14);
            lblOzelRaporlar.Name = "lblOzelRaporlar";
            lblOzelRaporlar.Size = new Size(80, 15);
            lblOzelRaporlar.TabIndex = 3;
            lblOzelRaporlar.Text = "Özel Raporlar:";
            // 
            // cmbOzelRaporlar
            // 
            cmbOzelRaporlar.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOzelRaporlar.Location = new Point(516, 11);
            cmbOzelRaporlar.Margin = new Padding(3, 2, 3, 2);
            cmbOzelRaporlar.Name = "cmbOzelRaporlar";
            cmbOzelRaporlar.Size = new Size(184, 23);
            cmbOzelRaporlar.TabIndex = 4;
            // 
            // btnRaporYukle
            // 
            btnRaporYukle.Location = new Point(718, 10);
            btnRaporYukle.Margin = new Padding(3, 2, 3, 2);
            btnRaporYukle.Name = "btnRaporYukle";
            btnRaporYukle.Size = new Size(105, 24);
            btnRaporYukle.TabIndex = 5;
            btnRaporYukle.Text = "Raporu Yükle";
            btnRaporYukle.UseVisualStyleBackColor = true;
            // 
            // lblQuery
            // 
            lblQuery.AutoSize = true;
            lblQuery.Location = new Point(19, 44);
            lblQuery.Name = "lblQuery";
            lblQuery.Size = new Size(77, 15);
            lblQuery.TabIndex = 6;
            lblQuery.Text = "SQL Sorgusu:";
            // 
            // txtQuery
            // 
            txtQuery.Font = new Font("Consolas", 10F);
            txtQuery.Location = new Point(19, 61);
            txtQuery.Margin = new Padding(3, 2, 3, 2);
            txtQuery.Multiline = true;
            txtQuery.Name = "txtQuery";
            txtQuery.ScrollBars = ScrollBars.Vertical;
            txtQuery.Size = new Size(604, 46);
            txtQuery.TabIndex = 7;
            // 
            // btnGenerateFields
            // 
            btnGenerateFields.Location = new Point(639, 61);
            btnGenerateFields.Margin = new Padding(3, 2, 3, 2);
            btnGenerateFields.Name = "btnGenerateFields";
            btnGenerateFields.Size = new Size(115, 30);
            btnGenerateFields.TabIndex = 8;
            btnGenerateFields.Text = "Parametreleri Getir";
            btnGenerateFields.UseVisualStyleBackColor = true;
            // 
            // btnRunQuery
            // 
            btnRunQuery.Location = new Point(760, 61);
            btnRunQuery.Margin = new Padding(3, 2, 3, 2);
            btnRunQuery.Name = "btnRunQuery";
            btnRunQuery.Size = new Size(122, 30);
            btnRunQuery.TabIndex = 9;
            btnRunQuery.Text = "Sorguyu Çalıştır";
            btnRunQuery.UseVisualStyleBackColor = true;
            // 
            // lblParams
            // 
            lblParams.AutoSize = true;
            lblParams.Location = new Point(19, 112);
            lblParams.Name = "lblParams";
            lblParams.Size = new Size(107, 15);
            lblParams.TabIndex = 10;
            lblParams.Text = "Parametre Alanları:";
            // 
            // panelParametreler
            // 
            panelParametreler.AutoScroll = true;
            panelParametreler.BackColor = Color.WhiteSmoke;
            panelParametreler.BorderStyle = BorderStyle.FixedSingle;
            panelParametreler.Location = new Point(19, 131);
            panelParametreler.Margin = new Padding(3, 2, 3, 2);
            panelParametreler.Name = "panelParametreler";
            panelParametreler.Size = new Size(614, 522);
            panelParametreler.TabIndex = 11;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.Location = new Point(639, 131);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(827, 522);
            dataGridView1.TabIndex = 12;
            // 
            // ÖzelRapor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1492, 664);
            Controls.Add(lblRaporAdi);
            Controls.Add(txtRaporAdi);
            Controls.Add(btnRaporKaydet);
            Controls.Add(lblOzelRaporlar);
            Controls.Add(cmbOzelRaporlar);
            Controls.Add(btnRaporYukle);
            Controls.Add(lblQuery);
            Controls.Add(txtQuery);
            Controls.Add(btnGenerateFields);
            Controls.Add(btnRunQuery);
            Controls.Add(lblParams);
            Controls.Add(panelParametreler);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 2, 3, 2);
            Name = "ÖzelRapor";
            Text = "Özel SQL Raporları";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
