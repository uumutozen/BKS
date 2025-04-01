using System.Windows.Forms;

namespace BKS
{
    partial class Form2
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageStok;
        private System.Windows.Forms.TabPage tabPageSatis;
        private System.Windows.Forms.TabPage tabPageGelirGider;
        private System.Windows.Forms.TabPage tabPagePersonelYonetimi;
        private System.Windows.Forms.DataGridView dataGridViewStok;
        private System.Windows.Forms.TextBox txtOgrenciAd;
        private System.Windows.Forms.NumericUpDown numericPrice;
        private System.Windows.Forms.Button btnAddStock;

        private System.Windows.Forms.ComboBox comboBoxStok;
        private System.Windows.Forms.NumericUpDown numericQuantitySold;
        private System.Windows.Forms.Button btnMakeSale;

        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.NumericUpDown numericAmount;
        private System.Windows.Forms.RadioButton radioIncome;
        private System.Windows.Forms.RadioButton radioExpense;
        private System.Windows.Forms.Button btnAddIncomeExpense;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tabControl = new TabControl();
            tabPageStok = new TabPage();
            groupBox16 = new GroupBox();
            cbxUserId = new CheckBox();
            btnOgrenciYonetimiSinifGuncelle = new Button();
            btnOgrenciYonetimiSinifSil = new Button();
            btnOgrenciYonetimiSinifKaydet = new Button();
            groupBox13 = new GroupBox();
            cbxOgrenciYonetimiOgretmen = new ComboBox();
            groupBox14 = new GroupBox();
            txtOgrenciYonetimiSınıfAdı = new TextBox();
            groupBox15 = new GroupBox();
            cbxOgrenciYonetimiYasGrubu = new ComboBox();
            DgvOgrenciYonetimiSiniflar = new DataGridView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            ödemeDetaylarıToolStripMenuItem = new ToolStripMenuItem();
            excelİleAktarToolStripMenuItem = new ToolStripMenuItem();
            btnOgrenciYonetimiAra = new Button();
            txtOgrenciYonetimiAra = new TextBox();
            btnGuncelle = new Button();
            groupBox11 = new GroupBox();
            textOgrenciDetay = new TextBox();
            btnOgrenciYonetimiSil = new Button();
            groupBox12 = new GroupBox();
            checkEvet = new CheckBox();
            groupBox10 = new GroupBox();
            txtAnneTel = new MaskedTextBox();
            txtAnneEvAdres = new TextBox();
            txtAnneAd = new TextBox();
            groupBox7 = new GroupBox();
            checkOdemeDurum = new CheckBox();
            checkAktif = new CheckBox();
            groupBox6 = new GroupBox();
            numericPrice = new NumericUpDown();
            groupBox5 = new GroupBox();
            dateDogum = new DateTimePicker();
            groupBox4 = new GroupBox();
            textOgrenciKod = new TextBox();
            groupBox3 = new GroupBox();
            txtOgrenciAd = new TextBox();
            groupBox2 = new GroupBox();
            textSoyad = new TextBox();
            groupBox1 = new GroupBox();
            txtBabaTel = new MaskedTextBox();
            txtBabaEvAdres = new TextBox();
            txtBabaAd = new TextBox();
            groupBox = new GroupBox();
            cmbogrsınıf = new ComboBox();
            btnAddStock = new Button();
            dataGridViewStok = new DataGridView();
            tabPageSatis = new TabPage();
            groupBox9 = new GroupBox();
            numericQuantitySold = new NumericUpDown();
            groupBox8 = new GroupBox();
            comboBoxStok = new ComboBox();
            dataOgrVw = new DataGridView();
            btnMakeSale = new Button();
            tabPageGelirGider = new TabPage();
            dataGridOdeme = new DataGridView();
            txtDescription = new TextBox();
            numericAmount = new NumericUpDown();
            radioIncome = new RadioButton();
            radioExpense = new RadioButton();
            btnAddIncomeExpense = new Button();
            tabPageOzelRaporlar = new TabPage();
            salesGrid = new DataGridView();
            tabPagePersonelYonetimi = new TabPage();
            groupBox23 = new GroupBox();
            textBox7 = new TextBox();
            groupBox24 = new GroupBox();
            textBox8 = new TextBox();
            groupBox21 = new GroupBox();
            textBox5 = new TextBox();
            groupBox19 = new GroupBox();
            textBox3 = new TextBox();
            groupBox18 = new GroupBox();
            textBox2 = new TextBox();
            groupBox17 = new GroupBox();
            pbxPersonelPicture = new PictureBox();
            groupBox25 = new GroupBox();
            textBox9 = new TextBox();
            textBox1 = new TextBox();
            groupBox22 = new GroupBox();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            textBox11 = new TextBox();
            textBox12 = new TextBox();
            textBox4 = new TextBox();
            textBox6 = new TextBox();
            dgvPersonelYonetimi = new DataGridView();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            tabControl.SuspendLayout();
            tabPageStok.SuspendLayout();
            groupBox16.SuspendLayout();
            groupBox13.SuspendLayout();
            groupBox14.SuspendLayout();
            groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvOgrenciYonetimiSiniflar).BeginInit();
            contextMenuStrip1.SuspendLayout();
            groupBox11.SuspendLayout();
            groupBox12.SuspendLayout();
            groupBox10.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericPrice).BeginInit();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewStok).BeginInit();
            tabPageSatis.SuspendLayout();
            groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericQuantitySold).BeginInit();
            groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataOgrVw).BeginInit();
            tabPageGelirGider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridOdeme).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericAmount).BeginInit();
            tabPageOzelRaporlar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)salesGrid).BeginInit();
            tabPagePersonelYonetimi.SuspendLayout();
            groupBox23.SuspendLayout();
            groupBox24.SuspendLayout();
            groupBox21.SuspendLayout();
            groupBox19.SuspendLayout();
            groupBox18.SuspendLayout();
            groupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxPersonelPicture).BeginInit();
            groupBox25.SuspendLayout();
            groupBox22.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPersonelYonetimi).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageStok);
            tabControl.Controls.Add(tabPageSatis);
            tabControl.Controls.Add(tabPageGelirGider);
            tabControl.Controls.Add(tabPageOzelRaporlar);
            tabControl.Controls.Add(tabPagePersonelYonetimi);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 162);
            tabControl.Location = new Point(3, 64);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1866, 970);
            tabControl.TabIndex = 0;
            // 
            // tabPageStok
            // 
            tabPageStok.BackColor = Color.White;
            tabPageStok.Controls.Add(groupBox16);
            tabPageStok.Controls.Add(btnOgrenciYonetimiSinifGuncelle);
            tabPageStok.Controls.Add(btnOgrenciYonetimiSinifSil);
            tabPageStok.Controls.Add(btnOgrenciYonetimiSinifKaydet);
            tabPageStok.Controls.Add(groupBox13);
            tabPageStok.Controls.Add(groupBox14);
            tabPageStok.Controls.Add(groupBox15);
            tabPageStok.Controls.Add(DgvOgrenciYonetimiSiniflar);
            tabPageStok.Controls.Add(btnOgrenciYonetimiAra);
            tabPageStok.Controls.Add(txtOgrenciYonetimiAra);
            tabPageStok.Controls.Add(btnGuncelle);
            tabPageStok.Controls.Add(groupBox11);
            tabPageStok.Controls.Add(btnOgrenciYonetimiSil);
            tabPageStok.Controls.Add(groupBox12);
            tabPageStok.Controls.Add(groupBox10);
            tabPageStok.Controls.Add(groupBox7);
            tabPageStok.Controls.Add(groupBox6);
            tabPageStok.Controls.Add(groupBox5);
            tabPageStok.Controls.Add(groupBox4);
            tabPageStok.Controls.Add(groupBox3);
            tabPageStok.Controls.Add(groupBox2);
            tabPageStok.Controls.Add(groupBox1);
            tabPageStok.Controls.Add(groupBox);
            tabPageStok.Controls.Add(btnAddStock);
            tabPageStok.Controls.Add(dataGridViewStok);
            tabPageStok.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 162);
            tabPageStok.Location = new Point(4, 24);
            tabPageStok.Name = "tabPageStok";
            tabPageStok.Size = new Size(1858, 942);
            tabPageStok.TabIndex = 0;
            tabPageStok.Text = "Öğrenci Yönetimi";
            tabPageStok.Click += tabPageStok_Click;
            // 
            // groupBox16
            // 
            groupBox16.Controls.Add(cbxUserId);
            groupBox16.FlatStyle = FlatStyle.Popup;
            groupBox16.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox16.ForeColor = Color.CornflowerBlue;
            groupBox16.Location = new Point(1055, 401);
            groupBox16.Name = "groupBox16";
            groupBox16.Size = new Size(188, 93);
            groupBox16.TabIndex = 26;
            groupBox16.TabStop = false;
            groupBox16.Text = "UserId Göster";
            // 
            // cbxUserId
            // 
            cbxUserId.AutoSize = true;
            cbxUserId.ForeColor = Color.Black;
            cbxUserId.Location = new Point(56, 43);
            cbxUserId.Name = "cbxUserId";
            cbxUserId.Size = new Size(48, 19);
            cbxUserId.TabIndex = 11;
            cbxUserId.Text = "Evet";
            cbxUserId.UseVisualStyleBackColor = true;
            cbxUserId.CheckedChanged += cbxUserId_CheckedChanged;
            // 
            // btnOgrenciYonetimiSinifGuncelle
            // 
            btnOgrenciYonetimiSinifGuncelle.BackColor = SystemColors.MenuHighlight;
            btnOgrenciYonetimiSinifGuncelle.Cursor = Cursors.Hand;
            btnOgrenciYonetimiSinifGuncelle.FlatAppearance.BorderSize = 0;
            btnOgrenciYonetimiSinifGuncelle.FlatStyle = FlatStyle.Flat;
            btnOgrenciYonetimiSinifGuncelle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOgrenciYonetimiSinifGuncelle.ForeColor = Color.White;
            btnOgrenciYonetimiSinifGuncelle.Location = new Point(1719, 890);
            btnOgrenciYonetimiSinifGuncelle.Name = "btnOgrenciYonetimiSinifGuncelle";
            btnOgrenciYonetimiSinifGuncelle.Size = new Size(114, 43);
            btnOgrenciYonetimiSinifGuncelle.TabIndex = 36;
            btnOgrenciYonetimiSinifGuncelle.Text = "Güncelle";
            btnOgrenciYonetimiSinifGuncelle.UseVisualStyleBackColor = false;
            // 
            // btnOgrenciYonetimiSinifSil
            // 
            btnOgrenciYonetimiSinifSil.BackColor = SystemColors.MenuHighlight;
            btnOgrenciYonetimiSinifSil.Cursor = Cursors.Hand;
            btnOgrenciYonetimiSinifSil.FlatAppearance.BorderSize = 0;
            btnOgrenciYonetimiSinifSil.FlatStyle = FlatStyle.Flat;
            btnOgrenciYonetimiSinifSil.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOgrenciYonetimiSinifSil.ForeColor = Color.White;
            btnOgrenciYonetimiSinifSil.Location = new Point(1479, 890);
            btnOgrenciYonetimiSinifSil.Name = "btnOgrenciYonetimiSinifSil";
            btnOgrenciYonetimiSinifSil.Size = new Size(114, 43);
            btnOgrenciYonetimiSinifSil.TabIndex = 35;
            btnOgrenciYonetimiSinifSil.Text = "Sil";
            btnOgrenciYonetimiSinifSil.UseVisualStyleBackColor = false;
            // 
            // btnOgrenciYonetimiSinifKaydet
            // 
            btnOgrenciYonetimiSinifKaydet.BackColor = SystemColors.MenuHighlight;
            btnOgrenciYonetimiSinifKaydet.Cursor = Cursors.Hand;
            btnOgrenciYonetimiSinifKaydet.FlatAppearance.BorderSize = 0;
            btnOgrenciYonetimiSinifKaydet.FlatStyle = FlatStyle.Flat;
            btnOgrenciYonetimiSinifKaydet.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOgrenciYonetimiSinifKaydet.ForeColor = Color.White;
            btnOgrenciYonetimiSinifKaydet.Location = new Point(1599, 890);
            btnOgrenciYonetimiSinifKaydet.Name = "btnOgrenciYonetimiSinifKaydet";
            btnOgrenciYonetimiSinifKaydet.Size = new Size(114, 43);
            btnOgrenciYonetimiSinifKaydet.TabIndex = 34;
            btnOgrenciYonetimiSinifKaydet.Text = "Kaydet";
            btnOgrenciYonetimiSinifKaydet.UseVisualStyleBackColor = false;
            btnOgrenciYonetimiSinifKaydet.Click += btnOgrenciYonetimiSinifKaydet_Click;
            // 
            // groupBox13
            // 
            groupBox13.Controls.Add(cbxOgrenciYonetimiOgretmen);
            groupBox13.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox13.ForeColor = Color.CornflowerBlue;
            groupBox13.Location = new Point(1349, 621);
            groupBox13.Name = "groupBox13";
            groupBox13.Size = new Size(238, 78);
            groupBox13.TabIndex = 33;
            groupBox13.TabStop = false;
            groupBox13.Text = "Öğretmen ";
            // 
            // cbxOgrenciYonetimiOgretmen
            // 
            cbxOgrenciYonetimiOgretmen.FormattingEnabled = true;
            cbxOgrenciYonetimiOgretmen.Location = new Point(6, 38);
            cbxOgrenciYonetimiOgretmen.Name = "cbxOgrenciYonetimiOgretmen";
            cbxOgrenciYonetimiOgretmen.Size = new Size(200, 23);
            cbxOgrenciYonetimiOgretmen.TabIndex = 0;
            cbxOgrenciYonetimiOgretmen.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // groupBox14
            // 
            groupBox14.Controls.Add(txtOgrenciYonetimiSınıfAdı);
            groupBox14.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox14.ForeColor = Color.CornflowerBlue;
            groupBox14.Location = new Point(861, 621);
            groupBox14.Name = "groupBox14";
            groupBox14.Size = new Size(238, 78);
            groupBox14.TabIndex = 32;
            groupBox14.TabStop = false;
            groupBox14.Text = "Sınıf Adı";
            // 
            // txtOgrenciYonetimiSınıfAdı
            // 
            txtOgrenciYonetimiSınıfAdı.Location = new Point(6, 38);
            txtOgrenciYonetimiSınıfAdı.Name = "txtOgrenciYonetimiSınıfAdı";
            txtOgrenciYonetimiSınıfAdı.PlaceholderText = "Sınıf Adı";
            txtOgrenciYonetimiSınıfAdı.Size = new Size(200, 23);
            txtOgrenciYonetimiSınıfAdı.TabIndex = 1;
            // 
            // groupBox15
            // 
            groupBox15.Controls.Add(cbxOgrenciYonetimiYasGrubu);
            groupBox15.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox15.ForeColor = Color.CornflowerBlue;
            groupBox15.Location = new Point(1105, 621);
            groupBox15.Name = "groupBox15";
            groupBox15.Size = new Size(238, 78);
            groupBox15.TabIndex = 31;
            groupBox15.TabStop = false;
            groupBox15.Text = "Yaş Grubu";
            // 
            // cbxOgrenciYonetimiYasGrubu
            // 
            cbxOgrenciYonetimiYasGrubu.FormattingEnabled = true;
            cbxOgrenciYonetimiYasGrubu.Location = new Point(6, 38);
            cbxOgrenciYonetimiYasGrubu.Name = "cbxOgrenciYonetimiYasGrubu";
            cbxOgrenciYonetimiYasGrubu.Size = new Size(200, 23);
            cbxOgrenciYonetimiYasGrubu.TabIndex = 0;
            // 
            // DgvOgrenciYonetimiSiniflar
            // 
            DgvOgrenciYonetimiSiniflar.BackgroundColor = SystemColors.ButtonHighlight;
            DgvOgrenciYonetimiSiniflar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            DgvOgrenciYonetimiSiniflar.ColumnHeadersHeight = 34;
            DgvOgrenciYonetimiSiniflar.ContextMenuStrip = contextMenuStrip1;
            DgvOgrenciYonetimiSiniflar.GridColor = Color.DodgerBlue;
            DgvOgrenciYonetimiSiniflar.Location = new Point(3, 621);
            DgvOgrenciYonetimiSiniflar.Name = "DgvOgrenciYonetimiSiniflar";
            DgvOgrenciYonetimiSiniflar.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            DgvOgrenciYonetimiSiniflar.RowHeadersVisible = false;
            DgvOgrenciYonetimiSiniflar.RowHeadersWidth = 62;
            DgvOgrenciYonetimiSiniflar.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            DgvOgrenciYonetimiSiniflar.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            DgvOgrenciYonetimiSiniflar.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;
            DgvOgrenciYonetimiSiniflar.ShowRowErrors = false;
            DgvOgrenciYonetimiSiniflar.Size = new Size(852, 312);
            DgvOgrenciYonetimiSiniflar.TabIndex = 30;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { ödemeDetaylarıToolStripMenuItem, excelİleAktarToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(163, 48);
            contextMenuStrip1.Text = "Ödeme Detayları";
            // 
            // ödemeDetaylarıToolStripMenuItem
            // 
            ödemeDetaylarıToolStripMenuItem.Name = "ödemeDetaylarıToolStripMenuItem";
            ödemeDetaylarıToolStripMenuItem.Size = new Size(162, 22);
            ödemeDetaylarıToolStripMenuItem.Text = "Ödeme Detayları";
            ödemeDetaylarıToolStripMenuItem.Click += ödemeDetaylarıToolStripMenuItem_Click_1;
            // 
            // excelİleAktarToolStripMenuItem
            // 
            excelİleAktarToolStripMenuItem.Name = "excelİleAktarToolStripMenuItem";
            excelİleAktarToolStripMenuItem.Size = new Size(162, 22);
            excelİleAktarToolStripMenuItem.Text = "Excel ile Aktar";
            excelİleAktarToolStripMenuItem.Click += excelAktarToolStripMenuItem_Click;
            // 
            // btnOgrenciYonetimiAra
            // 
            btnOgrenciYonetimiAra.Location = new Point(230, 14);
            btnOgrenciYonetimiAra.Name = "btnOgrenciYonetimiAra";
            btnOgrenciYonetimiAra.Size = new Size(99, 23);
            btnOgrenciYonetimiAra.TabIndex = 29;
            btnOgrenciYonetimiAra.Text = "Öğrenci Ara";
            btnOgrenciYonetimiAra.UseVisualStyleBackColor = true;
            btnOgrenciYonetimiAra.Click += btnOgrenciYonetimiAra_Click;
            // 
            // txtOgrenciYonetimiAra
            // 
            txtOgrenciYonetimiAra.Location = new Point(17, 15);
            txtOgrenciYonetimiAra.Name = "txtOgrenciYonetimiAra";
            txtOgrenciYonetimiAra.Size = new Size(197, 23);
            txtOgrenciYonetimiAra.TabIndex = 28;
            // 
            // btnGuncelle
            // 
            btnGuncelle.BackColor = SystemColors.MenuHighlight;
            btnGuncelle.Cursor = Cursors.Hand;
            btnGuncelle.FlatAppearance.BorderSize = 0;
            btnGuncelle.FlatStyle = FlatStyle.Flat;
            btnGuncelle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGuncelle.ForeColor = Color.White;
            btnGuncelle.Location = new Point(1719, 550);
            btnGuncelle.Name = "btnGuncelle";
            btnGuncelle.Size = new Size(114, 43);
            btnGuncelle.TabIndex = 27;
            btnGuncelle.Text = "Güncelle";
            btnGuncelle.UseVisualStyleBackColor = false;
            btnGuncelle.Click += btnGuncelle_Click;
            // 
            // groupBox11
            // 
            groupBox11.Controls.Add(textOgrenciDetay);
            groupBox11.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox11.ForeColor = Color.CornflowerBlue;
            groupBox11.Location = new Point(1105, 127);
            groupBox11.Name = "groupBox11";
            groupBox11.Size = new Size(482, 78);
            groupBox11.TabIndex = 14;
            groupBox11.TabStop = false;
            groupBox11.Text = "Öğrenci Detayları";
            // 
            // textOgrenciDetay
            // 
            textOgrenciDetay.Location = new Point(6, 22);
            textOgrenciDetay.Multiline = true;
            textOgrenciDetay.Name = "textOgrenciDetay";
            textOgrenciDetay.PlaceholderText = "Öğrenci Hakkında";
            textOgrenciDetay.Size = new Size(444, 50);
            textOgrenciDetay.TabIndex = 27;
            textOgrenciDetay.Tag = "";
            // 
            // btnOgrenciYonetimiSil
            // 
            btnOgrenciYonetimiSil.BackColor = SystemColors.MenuHighlight;
            btnOgrenciYonetimiSil.Cursor = Cursors.Hand;
            btnOgrenciYonetimiSil.FlatAppearance.BorderSize = 0;
            btnOgrenciYonetimiSil.FlatStyle = FlatStyle.Flat;
            btnOgrenciYonetimiSil.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOgrenciYonetimiSil.ForeColor = Color.White;
            btnOgrenciYonetimiSil.Location = new Point(1479, 550);
            btnOgrenciYonetimiSil.Name = "btnOgrenciYonetimiSil";
            btnOgrenciYonetimiSil.Size = new Size(114, 43);
            btnOgrenciYonetimiSil.TabIndex = 26;
            btnOgrenciYonetimiSil.Text = "Sil";
            btnOgrenciYonetimiSil.UseVisualStyleBackColor = false;
            btnOgrenciYonetimiSil.Click += btnOgrenciYonetimiSil_Click;
            // 
            // groupBox12
            // 
            groupBox12.Controls.Add(checkEvet);
            groupBox12.FlatStyle = FlatStyle.Popup;
            groupBox12.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox12.ForeColor = Color.CornflowerBlue;
            groupBox12.Location = new Point(861, 401);
            groupBox12.Name = "groupBox12";
            groupBox12.Size = new Size(188, 93);
            groupBox12.TabIndex = 25;
            groupBox12.TabStop = false;
            groupBox12.Text = "Aile Ayrı mı ?";
            // 
            // checkEvet
            // 
            checkEvet.AutoSize = true;
            checkEvet.ForeColor = Color.Black;
            checkEvet.Location = new Point(6, 43);
            checkEvet.Name = "checkEvet";
            checkEvet.Size = new Size(48, 19);
            checkEvet.TabIndex = 11;
            checkEvet.Text = "Evet";
            checkEvet.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(txtAnneTel);
            groupBox10.Controls.Add(txtAnneEvAdres);
            groupBox10.Controls.Add(txtAnneAd);
            groupBox10.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox10.ForeColor = Color.CornflowerBlue;
            groupBox10.Location = new Point(1349, 211);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(482, 184);
            groupBox10.TabIndex = 23;
            groupBox10.TabStop = false;
            groupBox10.Text = "Öğrenci Anne Bilgileri";
            // 
            // txtAnneTel
            // 
            txtAnneTel.Location = new Point(244, 38);
            txtAnneTel.Mask = "(999) 000-0000";
            txtAnneTel.Name = "txtAnneTel";
            txtAnneTel.Size = new Size(206, 23);
            txtAnneTel.TabIndex = 27;
            // 
            // txtAnneEvAdres
            // 
            txtAnneEvAdres.Location = new Point(6, 88);
            txtAnneEvAdres.Multiline = true;
            txtAnneEvAdres.Name = "txtAnneEvAdres";
            txtAnneEvAdres.PlaceholderText = "Ev Adres";
            txtAnneEvAdres.Size = new Size(444, 90);
            txtAnneEvAdres.TabIndex = 25;
            // 
            // txtAnneAd
            // 
            txtAnneAd.Location = new Point(6, 38);
            txtAnneAd.Name = "txtAnneAd";
            txtAnneAd.PlaceholderText = "Adı Soyadı";
            txtAnneAd.Size = new Size(200, 23);
            txtAnneAd.TabIndex = 8;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(checkOdemeDurum);
            groupBox7.Controls.Add(checkAktif);
            groupBox7.FlatStyle = FlatStyle.Popup;
            groupBox7.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox7.ForeColor = Color.CornflowerBlue;
            groupBox7.Location = new Point(861, 500);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(332, 93);
            groupBox7.TabIndex = 20;
            groupBox7.TabStop = false;
            groupBox7.Text = "Aktif mi ? Ödeme Durumu ?";
            // 
            // checkOdemeDurum
            // 
            checkOdemeDurum.AutoSize = true;
            checkOdemeDurum.ForeColor = Color.Black;
            checkOdemeDurum.Location = new Point(216, 44);
            checkOdemeDurum.Name = "checkOdemeDurum";
            checkOdemeDurum.Size = new Size(65, 19);
            checkOdemeDurum.TabIndex = 11;
            checkOdemeDurum.Text = "Ödendi";
            checkOdemeDurum.UseVisualStyleBackColor = true;
            // 
            // checkAktif
            // 
            checkAktif.AutoSize = true;
            checkAktif.ForeColor = Color.Black;
            checkAktif.Location = new Point(6, 44);
            checkAktif.Name = "checkAktif";
            checkAktif.Size = new Size(118, 19);
            checkAktif.TabIndex = 10;
            checkAktif.Text = "Aktif Öğrenci mi?";
            checkAktif.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(numericPrice);
            groupBox6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox6.ForeColor = Color.CornflowerBlue;
            groupBox6.Location = new Point(1199, 500);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(238, 93);
            groupBox6.TabIndex = 19;
            groupBox6.TabStop = false;
            groupBox6.Text = "Öğrenci Ödeme Tutarı";
            // 
            // numericPrice
            // 
            numericPrice.DecimalPlaces = 2;
            numericPrice.Location = new Point(6, 38);
            numericPrice.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            numericPrice.Name = "numericPrice";
            numericPrice.Size = new Size(188, 23);
            numericPrice.TabIndex = 3;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(dateDogum);
            groupBox5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox5.ForeColor = Color.CornflowerBlue;
            groupBox5.Location = new Point(1593, 43);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(238, 78);
            groupBox5.TabIndex = 18;
            groupBox5.TabStop = false;
            groupBox5.Text = "Öğrenci Doğum Tarihi";
            // 
            // dateDogum
            // 
            dateDogum.Location = new Point(6, 36);
            dateDogum.Name = "dateDogum";
            dateDogum.Size = new Size(200, 23);
            dateDogum.TabIndex = 9;
            dateDogum.ValueChanged += dateDogum_ValueChanged;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(textOgrenciKod);
            groupBox4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox4.ForeColor = Color.CornflowerBlue;
            groupBox4.Location = new Point(1349, 43);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(238, 78);
            groupBox4.TabIndex = 17;
            groupBox4.TabStop = false;
            groupBox4.Text = "Öğrenci Numarası";
            // 
            // textOgrenciKod
            // 
            textOgrenciKod.Location = new Point(6, 38);
            textOgrenciKod.Name = "textOgrenciKod";
            textOgrenciKod.PlaceholderText = "Öğrenci Numarası";
            textOgrenciKod.Size = new Size(200, 23);
            textOgrenciKod.TabIndex = 8;
            textOgrenciKod.TextChanged += textBox4_TextChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txtOgrenciAd);
            groupBox3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox3.ForeColor = Color.CornflowerBlue;
            groupBox3.Location = new Point(861, 43);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(238, 78);
            groupBox3.TabIndex = 16;
            groupBox3.TabStop = false;
            groupBox3.Text = "Öğrenci Adı";
            groupBox3.Enter += groupBox3_Enter;
            // 
            // txtOgrenciAd
            // 
            txtOgrenciAd.Location = new Point(6, 38);
            txtOgrenciAd.Name = "txtOgrenciAd";
            txtOgrenciAd.PlaceholderText = "Öğrenci Adı";
            txtOgrenciAd.Size = new Size(200, 23);
            txtOgrenciAd.TabIndex = 1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textSoyad);
            groupBox2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox2.ForeColor = Color.CornflowerBlue;
            groupBox2.Location = new Point(1105, 43);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(238, 78);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "Öğrenci Soyadı";
            // 
            // textSoyad
            // 
            textSoyad.Location = new Point(6, 38);
            textSoyad.Name = "textSoyad";
            textSoyad.PlaceholderText = "Öğrenci Soyadı";
            textSoyad.Size = new Size(200, 23);
            textSoyad.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtBabaTel);
            groupBox1.Controls.Add(txtBabaEvAdres);
            groupBox1.Controls.Add(txtBabaAd);
            groupBox1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox1.ForeColor = Color.CornflowerBlue;
            groupBox1.Location = new Point(861, 211);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(482, 184);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "Öğrenci Baba Bilgileri";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // txtBabaTel
            // 
            txtBabaTel.Location = new Point(250, 38);
            txtBabaTel.Mask = "(999) 000-0000";
            txtBabaTel.Name = "txtBabaTel";
            txtBabaTel.Size = new Size(200, 23);
            txtBabaTel.TabIndex = 26;
            // 
            // txtBabaEvAdres
            // 
            txtBabaEvAdres.Location = new Point(6, 88);
            txtBabaEvAdres.Multiline = true;
            txtBabaEvAdres.Name = "txtBabaEvAdres";
            txtBabaEvAdres.PlaceholderText = "Ev Adres";
            txtBabaEvAdres.Size = new Size(444, 90);
            txtBabaEvAdres.TabIndex = 26;
            txtBabaEvAdres.Tag = "";
            // 
            // txtBabaAd
            // 
            txtBabaAd.Location = new Point(6, 38);
            txtBabaAd.Name = "txtBabaAd";
            txtBabaAd.PlaceholderText = "Adı Soyadı";
            txtBabaAd.Size = new Size(200, 23);
            txtBabaAd.TabIndex = 6;
            txtBabaAd.TextChanged += textBabaAd_TextChanged;
            // 
            // groupBox
            // 
            groupBox.Controls.Add(cmbogrsınıf);
            groupBox.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox.ForeColor = Color.CornflowerBlue;
            groupBox.Location = new Point(861, 127);
            groupBox.Name = "groupBox";
            groupBox.Size = new Size(238, 78);
            groupBox.TabIndex = 1;
            groupBox.TabStop = false;
            groupBox.Text = "Sınıf Bilgileri";
            // 
            // cmbogrsınıf
            // 
            cmbogrsınıf.FormattingEnabled = true;
            cmbogrsınıf.Location = new Point(6, 36);
            cmbogrsınıf.Name = "cmbogrsınıf";
            cmbogrsınıf.Size = new Size(200, 23);
            cmbogrsınıf.TabIndex = 13;
            // 
            // btnAddStock
            // 
            btnAddStock.BackColor = SystemColors.MenuHighlight;
            btnAddStock.Cursor = Cursors.Hand;
            btnAddStock.FlatAppearance.BorderSize = 0;
            btnAddStock.FlatStyle = FlatStyle.Flat;
            btnAddStock.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddStock.ForeColor = Color.White;
            btnAddStock.Location = new Point(1599, 550);
            btnAddStock.Name = "btnAddStock";
            btnAddStock.Size = new Size(114, 43);
            btnAddStock.TabIndex = 4;
            btnAddStock.Text = "Kaydet";
            btnAddStock.UseVisualStyleBackColor = false;
            btnAddStock.Click += btnAddStock_Click;
            // 
            // dataGridViewStok
            // 
            dataGridViewStok.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridViewStok.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewStok.ColumnHeadersHeight = 34;
            dataGridViewStok.ContextMenuStrip = contextMenuStrip1;
            dataGridViewStok.GridColor = Color.DodgerBlue;
            dataGridViewStok.Location = new Point(3, 53);
            dataGridViewStok.Name = "dataGridViewStok";
            dataGridViewStok.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewStok.RowHeadersVisible = false;
            dataGridViewStok.RowHeadersWidth = 62;
            dataGridViewStok.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            dataGridViewStok.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            dataGridViewStok.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridViewStok.ShowRowErrors = false;
            dataGridViewStok.Size = new Size(852, 550);
            dataGridViewStok.TabIndex = 0;
            dataGridViewStok.CellClick += dataGridViewStok_CellClick;
            dataGridViewStok.CellContentClick += dataGridViewStok_CellContentClick_1;
            // 
            // tabPageSatis
            // 
            tabPageSatis.BackColor = Color.White;
            tabPageSatis.Controls.Add(groupBox9);
            tabPageSatis.Controls.Add(groupBox8);
            tabPageSatis.Controls.Add(dataOgrVw);
            tabPageSatis.Controls.Add(btnMakeSale);
            tabPageSatis.Location = new Point(4, 24);
            tabPageSatis.Name = "tabPageSatis";
            tabPageSatis.Size = new Size(1858, 942);
            tabPageSatis.TabIndex = 1;
            tabPageSatis.Text = "Öğrenci Ödeme Yönetimi";
            // 
            // groupBox9
            // 
            groupBox9.BackColor = Color.Transparent;
            groupBox9.Controls.Add(numericQuantitySold);
            groupBox9.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox9.ForeColor = Color.CornflowerBlue;
            groupBox9.Location = new Point(1089, 501);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(170, 76);
            groupBox9.TabIndex = 5;
            groupBox9.TabStop = false;
            groupBox9.Text = "Öğrenci Ücreti";
            // 
            // numericQuantitySold
            // 
            numericQuantitySold.Location = new Point(6, 36);
            numericQuantitySold.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericQuantitySold.Name = "numericQuantitySold";
            numericQuantitySold.Size = new Size(131, 23);
            numericQuantitySold.TabIndex = 1;
            // 
            // groupBox8
            // 
            groupBox8.BackColor = Color.Transparent;
            groupBox8.Controls.Add(comboBoxStok);
            groupBox8.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox8.ForeColor = Color.CornflowerBlue;
            groupBox8.Location = new Point(824, 501);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(238, 76);
            groupBox8.TabIndex = 4;
            groupBox8.TabStop = false;
            groupBox8.Text = "Öğrenci Seçimi";
            // 
            // comboBoxStok
            // 
            comboBoxStok.Location = new Point(6, 34);
            comboBoxStok.Name = "comboBoxStok";
            comboBoxStok.Size = new Size(200, 23);
            comboBoxStok.TabIndex = 0;
            // 
            // dataOgrVw
            // 
            dataOgrVw.ColumnHeadersHeight = 34;
            dataOgrVw.ContextMenuStrip = contextMenuStrip1;
            dataOgrVw.Location = new Point(4, 3);
            dataOgrVw.Name = "dataOgrVw";
            dataOgrVw.RowHeadersWidth = 62;
            dataOgrVw.Size = new Size(1446, 452);
            dataOgrVw.TabIndex = 3;
            dataOgrVw.CellContentClick += dataOgrVw_CellContentClick;
            // 
            // btnMakeSale
            // 
            btnMakeSale.BackColor = SystemColors.MenuHighlight;
            btnMakeSale.FlatStyle = FlatStyle.Flat;
            btnMakeSale.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnMakeSale.ForeColor = Color.Transparent;
            btnMakeSale.Location = new Point(1304, 501);
            btnMakeSale.Name = "btnMakeSale";
            btnMakeSale.Size = new Size(146, 76);
            btnMakeSale.TabIndex = 2;
            btnMakeSale.Text = "Ödeme Girişi";
            btnMakeSale.UseVisualStyleBackColor = false;
            btnMakeSale.Click += btnMakeSale_Click;
            // 
            // tabPageGelirGider
            // 
            tabPageGelirGider.BackColor = Color.White;
            tabPageGelirGider.Controls.Add(dataGridOdeme);
            tabPageGelirGider.Controls.Add(txtDescription);
            tabPageGelirGider.Controls.Add(numericAmount);
            tabPageGelirGider.Controls.Add(radioIncome);
            tabPageGelirGider.Controls.Add(radioExpense);
            tabPageGelirGider.Controls.Add(btnAddIncomeExpense);
            tabPageGelirGider.Location = new Point(4, 24);
            tabPageGelirGider.Name = "tabPageGelirGider";
            tabPageGelirGider.Size = new Size(1858, 942);
            tabPageGelirGider.TabIndex = 2;
            tabPageGelirGider.Text = "Gelir-Gider Yönetimi";
            // 
            // dataGridOdeme
            // 
            dataGridOdeme.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridOdeme.Location = new Point(20, 75);
            dataGridOdeme.Name = "dataGridOdeme";
            dataGridOdeme.RowHeadersWidth = 62;
            dataGridOdeme.Size = new Size(801, 493);
            dataGridOdeme.TabIndex = 5;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(20, 20);
            txtDescription.Name = "txtDescription";
            txtDescription.PlaceholderText = "Açıklama";
            txtDescription.Size = new Size(300, 23);
            txtDescription.TabIndex = 0;
            // 
            // numericAmount
            // 
            numericAmount.DecimalPlaces = 2;
            numericAmount.Location = new Point(350, 20);
            numericAmount.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericAmount.Name = "numericAmount";
            numericAmount.Size = new Size(120, 23);
            numericAmount.TabIndex = 1;
            // 
            // radioIncome
            // 
            radioIncome.Location = new Point(500, 20);
            radioIncome.Name = "radioIncome";
            radioIncome.Size = new Size(104, 24);
            radioIncome.TabIndex = 2;
            radioIncome.Text = "Gelir";
            // 
            // radioExpense
            // 
            radioExpense.Location = new Point(610, 20);
            radioExpense.Name = "radioExpense";
            radioExpense.Size = new Size(104, 24);
            radioExpense.TabIndex = 3;
            radioExpense.Text = "Gider";
            // 
            // btnAddIncomeExpense
            // 
            btnAddIncomeExpense.BackColor = SystemColors.MenuHighlight;
            btnAddIncomeExpense.FlatStyle = FlatStyle.Flat;
            btnAddIncomeExpense.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnAddIncomeExpense.ForeColor = Color.Transparent;
            btnAddIncomeExpense.Location = new Point(720, 10);
            btnAddIncomeExpense.Name = "btnAddIncomeExpense";
            btnAddIncomeExpense.Size = new Size(101, 41);
            btnAddIncomeExpense.TabIndex = 4;
            btnAddIncomeExpense.Text = "Ekle";
            btnAddIncomeExpense.UseVisualStyleBackColor = false;
            btnAddIncomeExpense.Click += btnAddIncomeExpense_Click;
            // 
            // tabPageOzelRaporlar
            // 
            tabPageOzelRaporlar.BackColor = Color.White;
            tabPageOzelRaporlar.Controls.Add(salesGrid);
            tabPageOzelRaporlar.Location = new Point(4, 24);
            tabPageOzelRaporlar.Name = "tabPageOzelRaporlar";
            tabPageOzelRaporlar.Padding = new Padding(3);
            tabPageOzelRaporlar.Size = new Size(1858, 942);
            tabPageOzelRaporlar.TabIndex = 3;
            tabPageOzelRaporlar.Text = "Özel Raporlar";
            tabPageOzelRaporlar.Click += tabPage1_Click;
            // 
            // salesGrid
            // 
            salesGrid.AllowDrop = true;
            salesGrid.AllowUserToOrderColumns = true;
            salesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            salesGrid.Location = new Point(6, 6);
            salesGrid.Name = "salesGrid";
            salesGrid.RowHeadersWidth = 62;
            salesGrid.Size = new Size(1224, 557);
            salesGrid.TabIndex = 0;
            salesGrid.CellContentClick += salesGrid_CellContentClick;
            // 
            // tabPagePersonelYonetimi
            // 
            tabPagePersonelYonetimi.Controls.Add(groupBox23);
            tabPagePersonelYonetimi.Controls.Add(groupBox24);
            tabPagePersonelYonetimi.Controls.Add(groupBox21);
            tabPagePersonelYonetimi.Controls.Add(groupBox19);
            tabPagePersonelYonetimi.Controls.Add(groupBox18);
            tabPagePersonelYonetimi.Controls.Add(groupBox17);
            tabPagePersonelYonetimi.Controls.Add(dgvPersonelYonetimi);
            tabPagePersonelYonetimi.Location = new Point(4, 24);
            tabPagePersonelYonetimi.Name = "tabPagePersonelYonetimi";
            tabPagePersonelYonetimi.Padding = new Padding(3);
            tabPagePersonelYonetimi.Size = new Size(1858, 942);
            tabPagePersonelYonetimi.TabIndex = 4;
            tabPagePersonelYonetimi.Text = "Personel Yönetimi";
            tabPagePersonelYonetimi.UseVisualStyleBackColor = true;
            // 
            // groupBox23
            // 
            groupBox23.Controls.Add(textBox7);
            groupBox23.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox23.ForeColor = Color.CornflowerBlue;
            groupBox23.Location = new Point(850, 748);
            groupBox23.Name = "groupBox23";
            groupBox23.Size = new Size(238, 78);
            groupBox23.TabIndex = 28;
            groupBox23.TabStop = false;
            groupBox23.Text = "Öğrenci Adı";
            // 
            // textBox7
            // 
            textBox7.Location = new Point(6, 38);
            textBox7.Name = "textBox7";
            textBox7.PlaceholderText = "Öğrenci Adı";
            textBox7.Size = new Size(200, 23);
            textBox7.TabIndex = 1;
            // 
            // groupBox24
            // 
            groupBox24.Controls.Add(textBox8);
            groupBox24.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox24.ForeColor = Color.CornflowerBlue;
            groupBox24.Location = new Point(606, 748);
            groupBox24.Name = "groupBox24";
            groupBox24.Size = new Size(238, 78);
            groupBox24.TabIndex = 27;
            groupBox24.TabStop = false;
            groupBox24.Text = "Öğrenci Adı";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(6, 38);
            textBox8.Name = "textBox8";
            textBox8.PlaceholderText = "Öğrenci Adı";
            textBox8.Size = new Size(200, 23);
            textBox8.TabIndex = 1;
            // 
            // groupBox21
            // 
            groupBox21.Controls.Add(textBox5);
            groupBox21.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox21.ForeColor = Color.CornflowerBlue;
            groupBox21.Location = new Point(1196, 703);
            groupBox21.Name = "groupBox21";
            groupBox21.Size = new Size(238, 78);
            groupBox21.TabIndex = 21;
            groupBox21.TabStop = false;
            groupBox21.Text = "Öğrenci Adı";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(6, 38);
            textBox5.Name = "textBox5";
            textBox5.PlaceholderText = "Öğrenci Adı";
            textBox5.Size = new Size(200, 23);
            textBox5.TabIndex = 1;
            // 
            // groupBox19
            // 
            groupBox19.Controls.Add(textBox3);
            groupBox19.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox19.ForeColor = Color.CornflowerBlue;
            groupBox19.Location = new Point(1491, 439);
            groupBox19.Name = "groupBox19";
            groupBox19.Size = new Size(238, 78);
            groupBox19.TabIndex = 19;
            groupBox19.TabStop = false;
            groupBox19.Text = "Öğrenci Adı";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(6, 38);
            textBox3.Name = "textBox3";
            textBox3.PlaceholderText = "Öğrenci Adı";
            textBox3.Size = new Size(200, 23);
            textBox3.TabIndex = 1;
            // 
            // groupBox18
            // 
            groupBox18.Controls.Add(textBox2);
            groupBox18.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox18.ForeColor = Color.CornflowerBlue;
            groupBox18.Location = new Point(1247, 439);
            groupBox18.Name = "groupBox18";
            groupBox18.Size = new Size(238, 78);
            groupBox18.TabIndex = 18;
            groupBox18.TabStop = false;
            groupBox18.Text = "Personel Adress";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(6, 38);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Öğrenci Adı";
            textBox2.Size = new Size(200, 23);
            textBox2.TabIndex = 1;
            // 
            // groupBox17
            // 
            groupBox17.Controls.Add(pbxPersonelPicture);
            groupBox17.Controls.Add(groupBox25);
            groupBox17.Controls.Add(textBox1);
            groupBox17.Controls.Add(groupBox22);
            groupBox17.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox17.ForeColor = Color.CornflowerBlue;
            groupBox17.Location = new Point(864, 6);
            groupBox17.Name = "groupBox17";
            groupBox17.Size = new Size(988, 550);
            groupBox17.TabIndex = 17;
            groupBox17.TabStop = false;
            groupBox17.Text = "Personel Bilgileri";
            // 
            // pbxPersonelPicture
            // 
            pbxPersonelPicture.Location = new Point(6, 22);
            pbxPersonelPicture.Name = "pbxPersonelPicture";
            pbxPersonelPicture.Size = new Size(152, 162);
            pbxPersonelPicture.TabIndex = 29;
            pbxPersonelPicture.TabStop = false;
            // 
            // groupBox25
            // 
            groupBox25.Controls.Add(textBox9);
            groupBox25.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox25.ForeColor = Color.CornflowerBlue;
            groupBox25.Location = new Point(6, 190);
            groupBox25.Name = "groupBox25";
            groupBox25.Size = new Size(976, 162);
            groupBox25.TabIndex = 26;
            groupBox25.TabStop = false;
            groupBox25.Text = "Personel Telefon Numarası";
            // 
            // textBox9
            // 
            textBox9.Location = new Point(33, 51);
            textBox9.Name = "textBox9";
            textBox9.PlaceholderText = "Öğrenci Adı";
            textBox9.Size = new Size(200, 23);
            textBox9.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(64, 372);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Öğrenci Adı";
            textBox1.Size = new Size(200, 23);
            textBox1.TabIndex = 1;
            // 
            // groupBox22
            // 
            groupBox22.Controls.Add(radioButton2);
            groupBox22.Controls.Add(radioButton1);
            groupBox22.Controls.Add(textBox11);
            groupBox22.Controls.Add(textBox12);
            groupBox22.Controls.Add(textBox4);
            groupBox22.Controls.Add(textBox6);
            groupBox22.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox22.ForeColor = Color.CornflowerBlue;
            groupBox22.Location = new Point(164, 22);
            groupBox22.Name = "groupBox22";
            groupBox22.Size = new Size(558, 162);
            groupBox22.TabIndex = 20;
            groupBox22.TabStop = false;
            groupBox22.Text = "Kişisel Bilgiler";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(497, 137);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(55, 19);
            radioButton2.TabIndex = 7;
            radioButton2.TabStop = true;
            radioButton2.Text = "Kadın";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(438, 137);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(53, 19);
            radioButton1.TabIndex = 6;
            radioButton1.TabStop = true;
            radioButton1.Text = "Erkek";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // textBox11
            // 
            textBox11.Location = new Point(8, 78);
            textBox11.Name = "textBox11";
            textBox11.PlaceholderText = "Adı";
            textBox11.Size = new Size(161, 23);
            textBox11.TabIndex = 5;
            // 
            // textBox12
            // 
            textBox12.Location = new Point(6, 107);
            textBox12.Name = "textBox12";
            textBox12.PlaceholderText = "Adı";
            textBox12.Size = new Size(163, 23);
            textBox12.TabIndex = 4;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(174, 38);
            textBox4.Name = "textBox4";
            textBox4.PlaceholderText = "Soyadı";
            textBox4.Size = new Size(147, 23);
            textBox4.TabIndex = 2;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(6, 38);
            textBox6.Name = "textBox6";
            textBox6.PlaceholderText = "Adı";
            textBox6.Size = new Size(147, 23);
            textBox6.TabIndex = 1;
            // 
            // dgvPersonelYonetimi
            // 
            dgvPersonelYonetimi.BackgroundColor = SystemColors.ButtonHighlight;
            dgvPersonelYonetimi.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvPersonelYonetimi.ColumnHeadersHeight = 34;
            dgvPersonelYonetimi.ContextMenuStrip = contextMenuStrip1;
            dgvPersonelYonetimi.GridColor = Color.DodgerBlue;
            dgvPersonelYonetimi.Location = new Point(6, 6);
            dgvPersonelYonetimi.Name = "dgvPersonelYonetimi";
            dgvPersonelYonetimi.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvPersonelYonetimi.RowHeadersVisible = false;
            dgvPersonelYonetimi.RowHeadersWidth = 62;
            dgvPersonelYonetimi.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            dgvPersonelYonetimi.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            dgvPersonelYonetimi.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvPersonelYonetimi.ShowRowErrors = false;
            dgvPersonelYonetimi.Size = new Size(852, 550);
            dgvPersonelYonetimi.TabIndex = 1;
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // materialLabel3
            // 
            materialLabel3.AutoSize = true;
            materialLabel3.Depth = 0;
            materialLabel3.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel3.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialLabel3.Location = new Point(1493, 42);
            materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel3.Name = "materialLabel3";
            materialLabel3.Size = new Size(69, 19);
            materialLabel3.TabIndex = 28;
            materialLabel3.Text = "Son Giriş:";
            // 
            // Form2
            // 
            AutoValidate = AutoValidate.EnablePreventFocusChange;
            ClientSize = new Size(1872, 1037);
            Controls.Add(materialLabel3);
            Controls.Add(tabControl);
            Font = new Font("Times New Roman", 9F);
            Name = "Form2";
            Text = "Anaokulu Yönetimi Sistemi";
            WindowState = FormWindowState.Maximized;
            FormClosing += Form2_FormClosing;
            Load += Form2_Load;
            tabControl.ResumeLayout(false);
            tabPageStok.ResumeLayout(false);
            tabPageStok.PerformLayout();
            groupBox16.ResumeLayout(false);
            groupBox16.PerformLayout();
            groupBox13.ResumeLayout(false);
            groupBox14.ResumeLayout(false);
            groupBox14.PerformLayout();
            groupBox15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DgvOgrenciYonetimiSiniflar).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            groupBox11.ResumeLayout(false);
            groupBox11.PerformLayout();
            groupBox12.ResumeLayout(false);
            groupBox12.PerformLayout();
            groupBox10.ResumeLayout(false);
            groupBox10.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericPrice).EndInit();
            groupBox5.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewStok).EndInit();
            tabPageSatis.ResumeLayout(false);
            groupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericQuantitySold).EndInit();
            groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataOgrVw).EndInit();
            tabPageGelirGider.ResumeLayout(false);
            tabPageGelirGider.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridOdeme).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericAmount).EndInit();
            tabPageOzelRaporlar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)salesGrid).EndInit();
            tabPagePersonelYonetimi.ResumeLayout(false);
            groupBox23.ResumeLayout(false);
            groupBox23.PerformLayout();
            groupBox24.ResumeLayout(false);
            groupBox24.PerformLayout();
            groupBox21.ResumeLayout(false);
            groupBox21.PerformLayout();
            groupBox19.ResumeLayout(false);
            groupBox19.PerformLayout();
            groupBox18.ResumeLayout(false);
            groupBox18.PerformLayout();
            groupBox17.ResumeLayout(false);
            groupBox17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbxPersonelPicture).EndInit();
            groupBox25.ResumeLayout(false);
            groupBox25.PerformLayout();
            groupBox22.ResumeLayout(false);
            groupBox22.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPersonelYonetimi).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView dataGridOdeme;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private TabPage tabPageOzelRaporlar;
        private DataGridView salesGrid;
        private TextBox txtBabaAd;
        private TextBox textSoyad;
        private DateTimePicker dateDogum;
        private TextBox textOgrenciKod;
        private CheckBox checkOdemeDurum;
        private CheckBox checkAktif;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem ödemeDetaylarıToolStripMenuItem;
        private ComboBox cmbogrsınıf;
        private GroupBox groupBox;
        private GroupBox groupBox1;
        private GroupBox groupBox7;
        private GroupBox groupBox6;
        private GroupBox groupBox5;
        private GroupBox groupBox4;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private DataGridView dataOgrVw;
        private GroupBox groupBox9;
        private GroupBox groupBox8;
        private GroupBox groupBox10;
        private TextBox txtAnneAd;
        private TextBox txtAnneEvAdres;
        private TextBox txtBabaEvAdres;
        private GroupBox groupBox12;
        private CheckBox checkEvet;
        private Button btnOgrenciYonetimiSil;
        private MaskedTextBox txtAnneTel;
        private MaskedTextBox txtBabaTel;
        private GroupBox groupBox11;
        private TextBox textOgrenciDetay;
        private Button btnGuncelle;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private Button btnOgrenciYonetimiAra;
        private TextBox txtOgrenciYonetimiAra;
        private GroupBox groupBox13;
        private GroupBox groupBox14;
        private TextBox txtOgrenciYonetimiSınıfAdı;
        private GroupBox groupBox15;
        private DataGridView DgvOgrenciYonetimiSiniflar;
        private ComboBox cbxOgrenciYonetimiYasGrubu;
        private ComboBox cbxOgrenciYonetimiOgretmen;
        private Button btnOgrenciYonetimiSinifGuncelle;
        private Button btnOgrenciYonetimiSinifSil;
        private Button btnOgrenciYonetimiSinifKaydet;
        private ToolStripMenuItem excelİleAktarToolStripMenuItem;
        private GroupBox groupBox16;
        private CheckBox cbxUserId;
   
        private GroupBox groupBox23;
        private TextBox textBox7;
        private GroupBox groupBox24;
        private TextBox textBox8;
        private GroupBox groupBox25;
        private TextBox textBox9;
        private GroupBox groupBox21;
        private TextBox textBox5;
        private GroupBox groupBox22;
        private TextBox textBox6;
        private GroupBox groupBox19;
        private TextBox textBox3;
        private GroupBox groupBox18;
        private TextBox textBox2;
        private GroupBox groupBox17;
        private TextBox textBox1;
        private DataGridView dgvPersonelYonetimi;
        private PictureBox pbxPersonelPicture;
        private TextBox textBox11;
        private TextBox textBox12;
        private TextBox textBox4;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
    }
}