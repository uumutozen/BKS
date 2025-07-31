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

        private System.Windows.Forms.ComboBox comboBoxStok;
        private System.Windows.Forms.NumericUpDown numericQuantitySold;
        private System.Windows.Forms.Button btnMakeSale;

        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.NumericUpDown numericAmount;
        private System.Windows.Forms.RadioButton radioIncome;
        private System.Windows.Forms.RadioButton radioExpense;
        private System.Windows.Forms.Button btnAddIncomeExpense;
        public TabPage tabPageOgrenciOnKayit;
        private TextBox txtOnKayitAd, txtOnKayitSoyad, txtOnKayitVeliTel, txtOnKayitNot;
        private DateTimePicker dtpOnKayitDogumTarihi;
        private Button btnOnKayitEkle, btnKesinKayitYap, btnOnKayitSil;
        private DataGridView dgvOnKayitlar;
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            tabControl = new TabControl();
            tabPageOgrenciOnKayit = new TabPage();
            panelForm = new Panel();
            txtOnKayitBabaAd = new TextBox();
            txtOnKayitAd = new TextBox();
            btnOnKayitSil = new Button();
            btnKesinKayitYap = new Button();
            txtOnKayitSoyad = new TextBox();
            dtpOnKayitDogumTarihi = new DateTimePicker();
            txtOnKayitVeliTel = new TextBox();
            txtOnKayitNot = new TextBox();
            btnOnKayitEkle = new Button();
            panelGrid = new Panel();
            dgvOnKayitlar = new DataGridView();
            materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            tabPageStok = new TabPage();
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
            yeniKayıtEkleToolStripMenuItem = new ToolStripMenuItem();
            kayıtSilToolStripMenuItem = new ToolStripMenuItem();
            yenileToolStripMenuItem = new ToolStripMenuItem();
            ödemeDetaylarıToolStripMenuItem = new ToolStripMenuItem();
            excelİleAktarToolStripMenuItem = new ToolStripMenuItem();
            geçmişHareketToolStripMenuItem = new ToolStripMenuItem();
            arşivToolStripMenuItem = new ToolStripMenuItem();
            btnOgrenciYonetimiAra = new Button();
            txtOgrenciYonetimiAra = new TextBox();
            dataGridViewStok = new DataGridView();
            btnOgrenciYonetimiSinifGuncelle = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            tabPageSatis = new TabPage();
            groupBox9 = new GroupBox();
            numericQuantitySold = new NumericUpDown();
            groupBox8 = new GroupBox();
            comboBoxStok = new ComboBox();
            dataOgrVw = new DataGridView();
            btnMakeSale = new Button();
            tabPagePersonelYonetimi = new TabPage();
            dgvPersonelYonetimi = new DataGridView();
            tabPageGelirGider = new TabPage();
            FaturaBtn = new Button();
            dataGridOdeme = new DataGridView();
            txtDescription = new TextBox();
            numericAmount = new NumericUpDown();
            radioIncome = new RadioButton();
            radioExpense = new RadioButton();
            btnAddIncomeExpense = new Button();
            tabPageOzelRaporlar = new TabPage();
            salesGrid = new DataGridView();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            timer1 = new System.Windows.Forms.Timer(components);
            dataGridView1 = new DataGridView();
            tabControl.SuspendLayout();
            tabPageOgrenciOnKayit.SuspendLayout();
            panelForm.SuspendLayout();
            panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOnKayitlar).BeginInit();
            tabPageStok.SuspendLayout();
            groupBox13.SuspendLayout();
            groupBox14.SuspendLayout();
            groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvOgrenciYonetimiSiniflar).BeginInit();
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewStok).BeginInit();
            tabPageSatis.SuspendLayout();
            groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericQuantitySold).BeginInit();
            groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataOgrVw).BeginInit();
            tabPagePersonelYonetimi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPersonelYonetimi).BeginInit();
            tabPageGelirGider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridOdeme).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericAmount).BeginInit();
            tabPageOzelRaporlar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)salesGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageOgrenciOnKayit);
            tabControl.Controls.Add(tabPageStok);
            tabControl.Controls.Add(tabPageSatis);
            tabControl.Controls.Add(tabPagePersonelYonetimi);
            tabControl.Controls.Add(tabPageGelirGider);
            tabControl.Controls.Add(tabPageOzelRaporlar);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 162);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1940, 777);
            tabControl.TabIndex = 0;
            // 
            // tabPageOgrenciOnKayit
            // 
            tabPageOgrenciOnKayit.Controls.Add(panelForm);
            tabPageOgrenciOnKayit.Controls.Add(panelGrid);
            tabPageOgrenciOnKayit.Location = new Point(4, 24);
            tabPageOgrenciOnKayit.Name = "tabPageOgrenciOnKayit";
            tabPageOgrenciOnKayit.Size = new Size(1932, 749);
            tabPageOgrenciOnKayit.TabIndex = 0;
            tabPageOgrenciOnKayit.Text = "🎓 Öğrenci Ön Kayıt";
            // 
            // panelForm
            // 
            panelForm.BackColor = Color.FromArgb(245, 248, 255);
            panelForm.Controls.Add(txtOnKayitBabaAd);
            panelForm.Controls.Add(txtOnKayitAd);
            panelForm.Controls.Add(btnOnKayitSil);
            panelForm.Controls.Add(btnKesinKayitYap);
            panelForm.Controls.Add(txtOnKayitSoyad);
            panelForm.Controls.Add(dtpOnKayitDogumTarihi);
            panelForm.Controls.Add(txtOnKayitVeliTel);
            panelForm.Controls.Add(txtOnKayitNot);
            panelForm.Controls.Add(btnOnKayitEkle);
            panelForm.Dock = DockStyle.Top;
            panelForm.Location = new Point(0, 0);
            panelForm.Name = "panelForm";
            panelForm.Size = new Size(1932, 250);
            panelForm.TabIndex = 0;
            // 
            // txtOnKayitBabaAd
            // 
            txtOnKayitBabaAd.Font = new Font("Segoe UI", 12F);
            txtOnKayitBabaAd.Location = new Point(320, 100);
            txtOnKayitBabaAd.Name = "txtOnKayitBabaAd";
            txtOnKayitBabaAd.PlaceholderText = "Baba Adı Soyadı";
            txtOnKayitBabaAd.Size = new Size(250, 29);
            txtOnKayitBabaAd.TabIndex = 9;
            // 
            // txtOnKayitAd
            // 
            txtOnKayitAd.Font = new Font("Segoe UI", 12F);
            txtOnKayitAd.Location = new Point(50, 50);
            txtOnKayitAd.Name = "txtOnKayitAd";
            txtOnKayitAd.PlaceholderText = "Öğrenci Adı";
            txtOnKayitAd.Size = new Size(250, 29);
            txtOnKayitAd.TabIndex = 0;
            // 
            // btnOnKayitSil
            // 
            btnOnKayitSil.BackColor = Color.FromArgb(231, 76, 60);
            btnOnKayitSil.FlatStyle = FlatStyle.Flat;
            btnOnKayitSil.Font = new Font("Segoe UI Semibold", 12F);
            btnOnKayitSil.ForeColor = Color.White;
            btnOnKayitSil.Location = new Point(1181, 180);
            btnOnKayitSil.Name = "btnOnKayitSil";
            btnOnKayitSil.Size = new Size(200, 40);
            btnOnKayitSil.TabIndex = 8;
            btnOnKayitSil.Text = "🗑️ Ön Kaydı Sil";
            btnOnKayitSil.UseVisualStyleBackColor = false;
            btnOnKayitSil.Click += btnOnKayitSil_Click;
            // 
            // btnKesinKayitYap
            // 
            btnKesinKayitYap.BackColor = Color.FromArgb(39, 174, 96);
            btnKesinKayitYap.FlatStyle = FlatStyle.Flat;
            btnKesinKayitYap.Font = new Font("Segoe UI Semibold", 12F);
            btnKesinKayitYap.ForeColor = Color.White;
            btnKesinKayitYap.Location = new Point(1412, 180);
            btnKesinKayitYap.Name = "btnKesinKayitYap";
            btnKesinKayitYap.Size = new Size(200, 40);
            btnKesinKayitYap.TabIndex = 6;
            btnKesinKayitYap.Text = "✅ Kesin Kayıt Yap";
            btnKesinKayitYap.UseVisualStyleBackColor = false;
            btnKesinKayitYap.Click += btnKesinKayitYap_Click_1;
            // 
            // txtOnKayitSoyad
            // 
            txtOnKayitSoyad.Font = new Font("Segoe UI", 12F);
            txtOnKayitSoyad.Location = new Point(320, 50);
            txtOnKayitSoyad.Name = "txtOnKayitSoyad";
            txtOnKayitSoyad.PlaceholderText = "Öğrenci Soyadı";
            txtOnKayitSoyad.Size = new Size(250, 29);
            txtOnKayitSoyad.TabIndex = 1;
            // 
            // dtpOnKayitDogumTarihi
            // 
            dtpOnKayitDogumTarihi.Font = new Font("Segoe UI", 12F);
            dtpOnKayitDogumTarihi.Format = DateTimePickerFormat.Short;
            dtpOnKayitDogumTarihi.Location = new Point(590, 47);
            dtpOnKayitDogumTarihi.Name = "dtpOnKayitDogumTarihi";
            dtpOnKayitDogumTarihi.Size = new Size(200, 29);
            dtpOnKayitDogumTarihi.TabIndex = 2;
            // 
            // txtOnKayitVeliTel
            // 
            txtOnKayitVeliTel.Font = new Font("Segoe UI", 12F);
            txtOnKayitVeliTel.Location = new Point(50, 100);
            txtOnKayitVeliTel.Name = "txtOnKayitVeliTel";
            txtOnKayitVeliTel.PlaceholderText = "Baba Telefon";
            txtOnKayitVeliTel.Size = new Size(250, 29);
            txtOnKayitVeliTel.TabIndex = 3;
            // 
            // txtOnKayitNot
            // 
            txtOnKayitNot.Font = new Font("Segoe UI", 12F);
            txtOnKayitNot.Location = new Point(320, 160);
            txtOnKayitNot.Multiline = true;
            txtOnKayitNot.Name = "txtOnKayitNot";
            txtOnKayitNot.PlaceholderText = "Notlar (Opsiyonel)";
            txtOnKayitNot.Size = new Size(470, 60);
            txtOnKayitNot.TabIndex = 4;
            // 
            // btnOnKayitEkle
            // 
            btnOnKayitEkle.BackColor = Color.FromArgb(52, 152, 219);
            btnOnKayitEkle.FlatStyle = FlatStyle.Flat;
            btnOnKayitEkle.Font = new Font("Segoe UI Semibold", 12F);
            btnOnKayitEkle.ForeColor = Color.White;
            btnOnKayitEkle.Location = new Point(50, 180);
            btnOnKayitEkle.Name = "btnOnKayitEkle";
            btnOnKayitEkle.Size = new Size(200, 40);
            btnOnKayitEkle.TabIndex = 5;
            btnOnKayitEkle.Text = "➕ Ön Kayıt Ekle";
            btnOnKayitEkle.UseVisualStyleBackColor = false;
            btnOnKayitEkle.Click += btnOnKayitEkle_Click;
            // 
            // panelGrid
            // 
            panelGrid.BackColor = Color.White;
            panelGrid.Controls.Add(dgvOnKayitlar);
            panelGrid.Controls.Add(materialLabel3);
            panelGrid.Dock = DockStyle.Fill;
            panelGrid.Location = new Point(0, 0);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(1932, 749);
            panelGrid.TabIndex = 1;
            // 
            // dgvOnKayitlar
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(240, 248, 255);
            dgvOnKayitlar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvOnKayitlar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvOnKayitlar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOnKayitlar.BackgroundColor = SystemColors.ButtonHighlight;
            dgvOnKayitlar.BorderStyle = BorderStyle.None;
            dgvOnKayitlar.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvOnKayitlar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvOnKayitlar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvOnKayitlar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvOnKayitlar.DefaultCellStyle = dataGridViewCellStyle3;
            dgvOnKayitlar.EnableHeadersVisualStyles = false;
            dgvOnKayitlar.GridColor = Color.LightGray;
            dgvOnKayitlar.Location = new Point(8, 256);
            dgvOnKayitlar.MultiSelect = false;
            dgvOnKayitlar.Name = "dgvOnKayitlar";
            dgvOnKayitlar.ReadOnly = true;
            dgvOnKayitlar.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvOnKayitlar.RowHeadersVisible = false;
            dgvOnKayitlar.RowHeadersWidth = 62;
            dgvOnKayitlar.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            dgvOnKayitlar.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            dgvOnKayitlar.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvOnKayitlar.RowTemplate.Height = 30;
            dgvOnKayitlar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOnKayitlar.ShowRowErrors = false;
            dgvOnKayitlar.Size = new Size(1916, 431);
            dgvOnKayitlar.TabIndex = 31;
            dgvOnKayitlar.Tag = 5001;
            // 
            // materialLabel3
            // 
            materialLabel3.AutoSize = true;
            materialLabel3.Depth = 0;
            materialLabel3.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel3.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialLabel3.Location = new Point(8, 973);
            materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel3.Name = "materialLabel3";
            materialLabel3.Size = new Size(69, 19);
            materialLabel3.TabIndex = 28;
            materialLabel3.Text = "Son Giriş:";
            // 
            // tabPageStok
            // 
            tabPageStok.BackColor = Color.White;
            tabPageStok.Controls.Add(btnOgrenciYonetimiSinifSil);
            tabPageStok.Controls.Add(btnOgrenciYonetimiSinifKaydet);
            tabPageStok.Controls.Add(groupBox13);
            tabPageStok.Controls.Add(groupBox14);
            tabPageStok.Controls.Add(groupBox15);
            tabPageStok.Controls.Add(DgvOgrenciYonetimiSiniflar);
            tabPageStok.Controls.Add(btnOgrenciYonetimiAra);
            tabPageStok.Controls.Add(txtOgrenciYonetimiAra);
            tabPageStok.Controls.Add(dataGridViewStok);
            tabPageStok.Controls.Add(btnOgrenciYonetimiSinifGuncelle);
            tabPageStok.Controls.Add(groupBox1);
            tabPageStok.Controls.Add(groupBox2);
            tabPageStok.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 162);
            tabPageStok.Location = new Point(4, 24);
            tabPageStok.Name = "tabPageStok";
            tabPageStok.Size = new Size(1932, 749);
            tabPageStok.TabIndex = 0;
            tabPageStok.Text = "Öğrenci Yönetimi";
            tabPageStok.Click += tabPageStok_Click;
            // 
            // btnOgrenciYonetimiSinifSil
            // 
            btnOgrenciYonetimiSinifSil.BackColor = Color.Transparent;
            btnOgrenciYonetimiSinifSil.Cursor = Cursors.Hand;
            btnOgrenciYonetimiSinifSil.FlatAppearance.BorderSize = 0;
            btnOgrenciYonetimiSinifSil.FlatStyle = FlatStyle.Flat;
            btnOgrenciYonetimiSinifSil.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOgrenciYonetimiSinifSil.ForeColor = Color.White;
            btnOgrenciYonetimiSinifSil.Image = (Image)resources.GetObject("btnOgrenciYonetimiSinifSil.Image");
            btnOgrenciYonetimiSinifSil.Location = new Point(1543, 856);
            btnOgrenciYonetimiSinifSil.Name = "btnOgrenciYonetimiSinifSil";
            btnOgrenciYonetimiSinifSil.Size = new Size(72, 77);
            btnOgrenciYonetimiSinifSil.TabIndex = 35;
            btnOgrenciYonetimiSinifSil.UseVisualStyleBackColor = false;
            btnOgrenciYonetimiSinifSil.Click += btnOgrenciYonetimiSinifSil_Click;
            // 
            // btnOgrenciYonetimiSinifKaydet
            // 
            btnOgrenciYonetimiSinifKaydet.BackColor = Color.Transparent;
            btnOgrenciYonetimiSinifKaydet.FlatAppearance.BorderSize = 0;
            btnOgrenciYonetimiSinifKaydet.FlatStyle = FlatStyle.Flat;
            btnOgrenciYonetimiSinifKaydet.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOgrenciYonetimiSinifKaydet.ForeColor = Color.Black;
            btnOgrenciYonetimiSinifKaydet.Image = (Image)resources.GetObject("btnOgrenciYonetimiSinifKaydet.Image");
            btnOgrenciYonetimiSinifKaydet.Location = new Point(1637, 856);
            btnOgrenciYonetimiSinifKaydet.Name = "btnOgrenciYonetimiSinifKaydet";
            btnOgrenciYonetimiSinifKaydet.Size = new Size(72, 77);
            btnOgrenciYonetimiSinifKaydet.TabIndex = 34;
            btnOgrenciYonetimiSinifKaydet.TextAlign = ContentAlignment.MiddleRight;
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
            cbxOgrenciYonetimiOgretmen.DropDownStyle = ComboBoxStyle.DropDownList;
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
            cbxOgrenciYonetimiYasGrubu.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxOgrenciYonetimiYasGrubu.FormattingEnabled = true;
            cbxOgrenciYonetimiYasGrubu.Location = new Point(15, 38);
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
            DgvOgrenciYonetimiSiniflar.Location = new Point(9, 631);
            DgvOgrenciYonetimiSiniflar.Name = "DgvOgrenciYonetimiSiniflar";
            DgvOgrenciYonetimiSiniflar.ReadOnly = true;
            DgvOgrenciYonetimiSiniflar.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            DgvOgrenciYonetimiSiniflar.RowHeadersVisible = false;
            DgvOgrenciYonetimiSiniflar.RowHeadersWidth = 62;
            DgvOgrenciYonetimiSiniflar.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            DgvOgrenciYonetimiSiniflar.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            DgvOgrenciYonetimiSiniflar.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;
            DgvOgrenciYonetimiSiniflar.ShowRowErrors = false;
            DgvOgrenciYonetimiSiniflar.Size = new Size(846, 302);
            DgvOgrenciYonetimiSiniflar.TabIndex = 30;
            DgvOgrenciYonetimiSiniflar.Tag = 4030;
            DgvOgrenciYonetimiSiniflar.CellContentClick += DgvOgrenciYonetimiSiniflar_CellContentClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { yeniKayıtEkleToolStripMenuItem, kayıtSilToolStripMenuItem, yenileToolStripMenuItem, ödemeDetaylarıToolStripMenuItem, excelİleAktarToolStripMenuItem, geçmişHareketToolStripMenuItem, arşivToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(179, 214);
            contextMenuStrip1.Text = "Ödeme Detayları";
            // 
            // yeniKayıtEkleToolStripMenuItem
            // 
            yeniKayıtEkleToolStripMenuItem.Image = (Image)resources.GetObject("yeniKayıtEkleToolStripMenuItem.Image");
            yeniKayıtEkleToolStripMenuItem.Name = "yeniKayıtEkleToolStripMenuItem";
            yeniKayıtEkleToolStripMenuItem.Size = new Size(178, 30);
            yeniKayıtEkleToolStripMenuItem.Text = "Yeni Kayıt Ekle";
            yeniKayıtEkleToolStripMenuItem.Click += yeniKayitEkle;
            // 
            // kayıtSilToolStripMenuItem
            // 
            kayıtSilToolStripMenuItem.Image = (Image)resources.GetObject("kayıtSilToolStripMenuItem.Image");
            kayıtSilToolStripMenuItem.Name = "kayıtSilToolStripMenuItem";
            kayıtSilToolStripMenuItem.Size = new Size(178, 30);
            kayıtSilToolStripMenuItem.Text = "Seçili Kayıdı Sil";
            kayıtSilToolStripMenuItem.Click += DeleteStripMenuItem_Click;
            // 
            // yenileToolStripMenuItem
            // 
            yenileToolStripMenuItem.Image = (Image)resources.GetObject("yenileToolStripMenuItem.Image");
            yenileToolStripMenuItem.Name = "yenileToolStripMenuItem";
            yenileToolStripMenuItem.Size = new Size(178, 30);
            yenileToolStripMenuItem.Text = "Yenile";
            yenileToolStripMenuItem.Click += yenileToolStripMenuItem_Click;
            // 
            // ödemeDetaylarıToolStripMenuItem
            // 
            ödemeDetaylarıToolStripMenuItem.Image = (Image)resources.GetObject("ödemeDetaylarıToolStripMenuItem.Image");
            ödemeDetaylarıToolStripMenuItem.Name = "ödemeDetaylarıToolStripMenuItem";
            ödemeDetaylarıToolStripMenuItem.Size = new Size(178, 30);
            ödemeDetaylarıToolStripMenuItem.Text = "Ödeme Detayları";
            ödemeDetaylarıToolStripMenuItem.Click += ödemeDetaylarıToolStripMenuItem_Click_1;
            // 
            // excelİleAktarToolStripMenuItem
            // 
            excelİleAktarToolStripMenuItem.Image = (Image)resources.GetObject("excelİleAktarToolStripMenuItem.Image");
            excelİleAktarToolStripMenuItem.Name = "excelİleAktarToolStripMenuItem";
            excelİleAktarToolStripMenuItem.Size = new Size(178, 30);
            excelİleAktarToolStripMenuItem.Text = "Excel ile Aktar";
            excelİleAktarToolStripMenuItem.Click += excelAktarToolStripMenuItem_Click;
            // 
            // geçmişHareketToolStripMenuItem
            // 
            geçmişHareketToolStripMenuItem.Image = (Image)resources.GetObject("geçmişHareketToolStripMenuItem.Image");
            geçmişHareketToolStripMenuItem.Name = "geçmişHareketToolStripMenuItem";
            geçmişHareketToolStripMenuItem.Size = new Size(178, 30);
            geçmişHareketToolStripMenuItem.Text = "Geçmiş Hareketler";
            geçmişHareketToolStripMenuItem.Click += loglarıGörüntüleToolStripMenuItem_Click;
            // 
            // arşivToolStripMenuItem
            // 
            arşivToolStripMenuItem.Image = (Image)resources.GetObject("arşivToolStripMenuItem.Image");
            arşivToolStripMenuItem.Name = "arşivToolStripMenuItem";
            arşivToolStripMenuItem.Size = new Size(178, 30);
            arşivToolStripMenuItem.Text = "Arşiv";
            arşivToolStripMenuItem.Click += arşivToolStripMenuItem_Click;
            // 
            // btnOgrenciYonetimiAra
            // 
            btnOgrenciYonetimiAra.Location = new Point(212, 3);
            btnOgrenciYonetimiAra.Name = "btnOgrenciYonetimiAra";
            btnOgrenciYonetimiAra.Size = new Size(99, 23);
            btnOgrenciYonetimiAra.TabIndex = 29;
            btnOgrenciYonetimiAra.Text = "Öğrenci Ara";
            btnOgrenciYonetimiAra.UseVisualStyleBackColor = true;
            btnOgrenciYonetimiAra.Click += btnOgrenciYonetimiAra_Click;
            // 
            // txtOgrenciYonetimiAra
            // 
            txtOgrenciYonetimiAra.Location = new Point(9, 4);
            txtOgrenciYonetimiAra.Name = "txtOgrenciYonetimiAra";
            txtOgrenciYonetimiAra.Size = new Size(197, 23);
            txtOgrenciYonetimiAra.TabIndex = 28;
            txtOgrenciYonetimiAra.TextChanged += txtOgrenciYonetimiAra_TextChanged;
            // 
            // dataGridViewStok
            // 
            dataGridViewStok.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewStok.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridViewStok.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewStok.ColumnHeadersHeight = 34;
            dataGridViewStok.ContextMenuStrip = contextMenuStrip1;
            dataGridViewStok.GridColor = Color.DodgerBlue;
            dataGridViewStok.Location = new Point(3, 53);
            dataGridViewStok.MultiSelect = false;
            dataGridViewStok.Name = "dataGridViewStok";
            dataGridViewStok.ReadOnly = true;
            dataGridViewStok.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewStok.RowHeadersVisible = false;
            dataGridViewStok.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewStok.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            dataGridViewStok.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            dataGridViewStok.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridViewStok.ShowRowErrors = false;
            dataGridViewStok.Size = new Size(1874, 550);
            dataGridViewStok.TabIndex = 0;
            dataGridViewStok.Tag = 4010;
            dataGridViewStok.CellDoubleClick += dataGridViewStok_CellDoubleClick;
            // 
            // btnOgrenciYonetimiSinifGuncelle
            // 
            btnOgrenciYonetimiSinifGuncelle.BackColor = Color.Transparent;
            btnOgrenciYonetimiSinifGuncelle.Cursor = Cursors.Hand;
            btnOgrenciYonetimiSinifGuncelle.FlatAppearance.BorderSize = 0;
            btnOgrenciYonetimiSinifGuncelle.FlatStyle = FlatStyle.Flat;
            btnOgrenciYonetimiSinifGuncelle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOgrenciYonetimiSinifGuncelle.ForeColor = Color.White;
            btnOgrenciYonetimiSinifGuncelle.Image = (Image)resources.GetObject("btnOgrenciYonetimiSinifGuncelle.Image");
            btnOgrenciYonetimiSinifGuncelle.Location = new Point(1732, 856);
            btnOgrenciYonetimiSinifGuncelle.Name = "btnOgrenciYonetimiSinifGuncelle";
            btnOgrenciYonetimiSinifGuncelle.Size = new Size(72, 77);
            btnOgrenciYonetimiSinifGuncelle.TabIndex = 36;
            btnOgrenciYonetimiSinifGuncelle.UseVisualStyleBackColor = false;
            btnOgrenciYonetimiSinifGuncelle.Click += btnOgrenciYonetimiSinifGuncelle_Click;
            // 
            // groupBox1
            // 
            groupBox1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox1.ForeColor = Color.CornflowerBlue;
            groupBox1.Location = new Point(3, 609);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(858, 333);
            groupBox1.TabIndex = 33;
            groupBox1.TabStop = false;
            groupBox1.Text = "Sınıf Listesi";
            // 
            // groupBox2
            // 
            groupBox2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox2.ForeColor = Color.CornflowerBlue;
            groupBox2.Location = new Point(3, 33);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1852, 570);
            groupBox2.TabIndex = 33;
            groupBox2.TabStop = false;
            groupBox2.Text = "Öğrenci Listesi";
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
            tabPageSatis.Size = new Size(1932, 749);
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
            numericQuantitySold.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
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
            // tabPagePersonelYonetimi
            // 
            tabPagePersonelYonetimi.Controls.Add(dgvPersonelYonetimi);
            tabPagePersonelYonetimi.Location = new Point(4, 24);
            tabPagePersonelYonetimi.Name = "tabPagePersonelYonetimi";
            tabPagePersonelYonetimi.Padding = new Padding(3);
            tabPagePersonelYonetimi.Size = new Size(1932, 749);
            tabPagePersonelYonetimi.TabIndex = 4;
            tabPagePersonelYonetimi.Text = "Personel Yönetimi";
            tabPagePersonelYonetimi.UseVisualStyleBackColor = true;
            tabPagePersonelYonetimi.Click += tabPagePersonelYonetimi_Click;
            // 
            // dgvPersonelYonetimi
            // 
            dataGridViewCellStyle4.BackColor = Color.FromArgb(240, 248, 255);
            dgvPersonelYonetimi.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvPersonelYonetimi.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPersonelYonetimi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPersonelYonetimi.BackgroundColor = SystemColors.ButtonHighlight;
            dgvPersonelYonetimi.BorderStyle = BorderStyle.None;
            dgvPersonelYonetimi.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPersonelYonetimi.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.MidnightBlue;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvPersonelYonetimi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvPersonelYonetimi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPersonelYonetimi.ContextMenuStrip = contextMenuStrip1;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvPersonelYonetimi.DefaultCellStyle = dataGridViewCellStyle6;
            dgvPersonelYonetimi.EnableHeadersVisualStyles = false;
            dgvPersonelYonetimi.GridColor = Color.LightGray;
            dgvPersonelYonetimi.Location = new Point(3, 6);
            dgvPersonelYonetimi.MultiSelect = false;
            dgvPersonelYonetimi.Name = "dgvPersonelYonetimi";
            dgvPersonelYonetimi.ReadOnly = true;
            dgvPersonelYonetimi.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvPersonelYonetimi.RowHeadersVisible = false;
            dgvPersonelYonetimi.RowHeadersWidth = 62;
            dgvPersonelYonetimi.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            dgvPersonelYonetimi.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            dgvPersonelYonetimi.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvPersonelYonetimi.RowTemplate.Height = 30;
            dgvPersonelYonetimi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPersonelYonetimi.ShowRowErrors = false;
            dgvPersonelYonetimi.Size = new Size(1867, 961);
            dgvPersonelYonetimi.TabIndex = 32;
            dgvPersonelYonetimi.Tag = 4020;
            dgvPersonelYonetimi.CellClick += dataGridViewStok_CellContentClick;
            dgvPersonelYonetimi.CellDoubleClick += dataGridViewPersonel_CellDoubleClick;
            // 
            // tabPageGelirGider
            // 
            tabPageGelirGider.BackColor = Color.White;
            tabPageGelirGider.Controls.Add(FaturaBtn);
            tabPageGelirGider.Controls.Add(dataGridOdeme);
            tabPageGelirGider.Controls.Add(txtDescription);
            tabPageGelirGider.Controls.Add(numericAmount);
            tabPageGelirGider.Controls.Add(radioIncome);
            tabPageGelirGider.Controls.Add(radioExpense);
            tabPageGelirGider.Controls.Add(btnAddIncomeExpense);
            tabPageGelirGider.Location = new Point(4, 24);
            tabPageGelirGider.Name = "tabPageGelirGider";
            tabPageGelirGider.Size = new Size(1932, 749);
            tabPageGelirGider.TabIndex = 2;
            tabPageGelirGider.Text = "Gelir-Gider Yönetimi";
            // 
            // FaturaBtn
            // 
            FaturaBtn.BackColor = SystemColors.MenuHighlight;
            FaturaBtn.FlatStyle = FlatStyle.Flat;
            FaturaBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            FaturaBtn.ForeColor = Color.Transparent;
            FaturaBtn.Location = new Point(908, 10);
            FaturaBtn.Name = "FaturaBtn";
            FaturaBtn.Size = new Size(325, 41);
            FaturaBtn.TabIndex = 6;
            FaturaBtn.Text = "Fatura Merkezi";
            FaturaBtn.UseVisualStyleBackColor = false;
            FaturaBtn.Click += FaturaBtn_Click;
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
            tabPageOzelRaporlar.Size = new Size(1932, 749);
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
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersHeight = 34;
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.GridColor = Color.DodgerBlue;
            dataGridView1.Location = new Point(361, 3);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridView1.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            dataGridView1.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.ShowRowErrors = false;
            dataGridView1.Size = new Size(300, 166);
            dataGridView1.TabIndex = 8;
            dataGridView1.Tag = 4010;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnablePreventFocusChange;
            ClientSize = new Size(1940, 777);
            Controls.Add(tabControl);
            Font = new Font("Times New Roman", 9F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form2";
            PaletteMode = Krypton.Toolkit.PaletteMode.VisualStudio2010Render2013;
            Text = "Anaokulu Yönetimi Sistemi";
            WindowState = FormWindowState.Maximized;
            FormClosing += Form2_FormClosing;
            Load += Form2_Load;
            tabControl.ResumeLayout(false);
            tabPageOgrenciOnKayit.ResumeLayout(false);
            panelForm.ResumeLayout(false);
            panelForm.PerformLayout();
            panelGrid.ResumeLayout(false);
            panelGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOnKayitlar).EndInit();
            tabPageStok.ResumeLayout(false);
            tabPageStok.PerformLayout();
            groupBox13.ResumeLayout(false);
            groupBox14.ResumeLayout(false);
            groupBox14.PerformLayout();
            groupBox15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DgvOgrenciYonetimiSiniflar).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewStok).EndInit();
            tabPageSatis.ResumeLayout(false);
            groupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericQuantitySold).EndInit();
            groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataOgrVw).EndInit();
            tabPagePersonelYonetimi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPersonelYonetimi).EndInit();
            tabPageGelirGider.ResumeLayout(false);
            tabPageGelirGider.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridOdeme).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericAmount).EndInit();
            tabPageOzelRaporlar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)salesGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        private DataGridView dataGridOdeme;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private TabPage tabPageOzelRaporlar;
        private DataGridView salesGrid;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem ödemeDetaylarıToolStripMenuItem;
        private DataGridView dataOgrVw;
        private GroupBox groupBox9;
        private GroupBox groupBox8;
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
        private ToolStripMenuItem yeniKayıtEkleToolStripMenuItem;
        private ToolStripMenuItem yenileToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private ToolStripMenuItem kayıtSilToolStripMenuItem;
        public DataGridView dataGridViewStok;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ToolStripMenuItem geçmişHareketToolStripMenuItem;
        public DataGridView dataGridView1;
        //private Krypton.Ribbon.KryptonRibbon ribbon;
        //private Krypton.Ribbon.KryptonRibbonTab tabOnKayit;
        //private Krypton.Ribbon.KryptonRibbonTab tabOgrenci;
        //private Krypton.Ribbon.KryptonRibbonTab tabOdeme;
        //private Krypton.Ribbon.KryptonRibbonTab tabPersonel;
        //private Krypton.Ribbon.KryptonRibbonTab tabGelirGider;
        //private Krypton.Ribbon.KryptonRibbonTab tabRapor;
        private Panel panelForm;
        private Panel panelGrid;
        private Button FaturaBtn;
        private ToolStripMenuItem arşivToolStripMenuItem;
        private TextBox txtOnKayitBabaAd;
        public DataGridView dgvPersonelYonetimi;
    }
}