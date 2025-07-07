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
        private TabPage tabPageOgrenciOnKayit;
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
            tabControl = new TabControl();
            tabPageOgrenciOnKayit = new TabPage();
            panelForm = new Panel();
            txtOnKayitAd = new TextBox();
            txtOnKayitSoyad = new TextBox();
            dtpOnKayitDogumTarihi = new DateTimePicker();
            txtOnKayitVeliTel = new TextBox();
            txtOnKayitNot = new TextBox();
            btnOnKayitEkle = new Button();
            btnOnKayitSil = new Button();
            btnKesinKayitYap = new Button();
            panelGrid = new Panel();
            dgvOnKayitlar = new DataGridView();
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
            groupBox17 = new GroupBox();
            btnPersonelSil = new Button();
            btnPersonelTemizle = new Button();
            btnPersonelKaydet = new Button();
            btnPersonelGuncelle = new Button();
            pbxPersonelPicture = new PictureBox();
            groupBox25 = new GroupBox();
            txtPersonelIletişimAcilDurum = new TextBox();
            txtPersonelMail = new TextBox();
            txtPersonelTel = new TextBox();
            txtPersonelAdres = new TextBox();
            groupBox22 = new GroupBox();
            lblKimlikNum = new Label();
            label3 = new Label();
            panel2 = new Panel();
            rbtPersonelBekar = new RadioButton();
            label8 = new Label();
            rbtPersonelEvli = new RadioButton();
            panel1 = new Panel();
            label9 = new Label();
            rbtPersonelErkek = new RadioButton();
            rbtPersonelKadin = new RadioButton();
            cbxPersonelUyruk = new ComboBox();
            label1 = new Label();
            dtpPersonelDG = new DateTimePicker();
            txtPersonelKimlik = new TextBox();
            txtPersonelSoyad = new TextBox();
            txtPersonelAd = new TextBox();
            groupBox18 = new GroupBox();
            panel3 = new Panel();
            label10 = new Label();
            rbtPersonelEgitimGorevlisiEvet = new RadioButton();
            rbtPersonelEgitimGorevlisiHayir = new RadioButton();
            txtPersonelKidemTazminat = new TextBox();
            txtPersonelAyrilmaNedeni = new TextBox();
            txtPersonelEmeklilik = new TextBox();
            txtPersonelSaglikSigorta = new TextBox();
            txtPersonelSGKSicilNum = new TextBox();
            txtPersonelYemekYol = new TextBox();
            txtPersonelPrimVeEk = new TextBox();
            label7 = new Label();
            cbxPersonelSigorta = new ComboBox();
            txtPersonelPersonelNo = new TextBox();
            label6 = new Label();
            cbxPersonelCalismaSekli = new ComboBox();
            txtPersonelUniBolum = new TextBox();
            label5 = new Label();
            cbxPersonelUniversite = new ComboBox();
            txtPersonelDepartman = new TextBox();
            txtPersonelYabanciDil = new TextBox();
            txtPersonelSertifika = new TextBox();
            groupBox20 = new GroupBox();
            textBox1 = new TextBox();
            cbxPersonelEgitimDurumu = new ComboBox();
            label4 = new Label();
            txtPersonelGorev = new TextBox();
            dtpPersonelIseBaslamaTarihi = new DateTimePicker();
            cbxPersoneIIsAyrıldı = new CheckBox();
            dtpPersonelCıkısTarihi = new DateTimePicker();
            lblIstenCıkısTarihi = new Label();
            label2 = new Label();
            txtPersonelMaas = new TextBox();
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
            materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            timer1 = new System.Windows.Forms.Timer(components);
            dataGridView1 = new DataGridView();
            arşivToolStripMenuItem = new ToolStripMenuItem();
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
            groupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxPersonelPicture).BeginInit();
            groupBox25.SuspendLayout();
            groupBox22.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            groupBox18.SuspendLayout();
            panel3.SuspendLayout();
            groupBox20.SuspendLayout();
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
            tabControl.Location = new Point(3, 64);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1866, 970);
            tabControl.TabIndex = 0;
            // 
            // tabPageOgrenciOnKayit
            // 
            tabPageOgrenciOnKayit.Controls.Add(panelForm);
            tabPageOgrenciOnKayit.Controls.Add(panelGrid);
            tabPageOgrenciOnKayit.Location = new Point(4, 24);
            tabPageOgrenciOnKayit.Name = "tabPageOgrenciOnKayit";
            tabPageOgrenciOnKayit.Size = new Size(1858, 942);
            tabPageOgrenciOnKayit.TabIndex = 0;
            tabPageOgrenciOnKayit.Text = "🎓 Öğrenci Ön Kayıt";
            // 
            // panelForm
            // 
            panelForm.BackColor = Color.FromArgb(245, 248, 255);
            panelForm.Controls.Add(txtOnKayitAd);
            panelForm.Controls.Add(txtOnKayitSoyad);
            panelForm.Controls.Add(dtpOnKayitDogumTarihi);
            panelForm.Controls.Add(txtOnKayitVeliTel);
            panelForm.Controls.Add(txtOnKayitNot);
            panelForm.Controls.Add(btnOnKayitEkle);
            panelForm.Controls.Add(btnOnKayitSil);
            panelForm.Controls.Add(btnKesinKayitYap);
            panelForm.Dock = DockStyle.Top;
            panelForm.Location = new Point(0, 0);
            panelForm.Name = "panelForm";
            panelForm.Size = new Size(1858, 250);
            panelForm.TabIndex = 0;
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
            dtpOnKayitDogumTarihi.Location = new Point(590, 50);
            dtpOnKayitDogumTarihi.Name = "dtpOnKayitDogumTarihi";
            dtpOnKayitDogumTarihi.Size = new Size(200, 29);
            dtpOnKayitDogumTarihi.TabIndex = 2;
            // 
            // txtOnKayitVeliTel
            // 
            txtOnKayitVeliTel.Font = new Font("Segoe UI", 12F);
            txtOnKayitVeliTel.Location = new Point(50, 100);
            txtOnKayitVeliTel.Name = "txtOnKayitVeliTel";
            txtOnKayitVeliTel.PlaceholderText = "Veli Telefonu";
            txtOnKayitVeliTel.Size = new Size(250, 29);
            txtOnKayitVeliTel.TabIndex = 3;
            // 
            // txtOnKayitNot
            // 
            txtOnKayitNot.Font = new Font("Segoe UI", 12F);
            txtOnKayitNot.Location = new Point(320, 100);
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
            // btnOnKayitSil
            // 
            btnOnKayitSil.BackColor = Color.FromArgb(231, 76, 60);
            btnOnKayitSil.FlatStyle = FlatStyle.Flat;
            btnOnKayitSil.Font = new Font("Segoe UI Semibold", 12F);
            btnOnKayitSil.ForeColor = Color.White;
            btnOnKayitSil.Location = new Point(490, 180);
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
            btnKesinKayitYap.Location = new Point(270, 180);
            btnKesinKayitYap.Name = "btnKesinKayitYap";
            btnKesinKayitYap.Size = new Size(200, 40);
            btnKesinKayitYap.TabIndex = 6;
            btnKesinKayitYap.Text = "✅ Kesin Kayıt Yap";
            btnKesinKayitYap.UseVisualStyleBackColor = false;
            // 
            // panelGrid
            // 
            panelGrid.BackColor = Color.White;
            panelGrid.Controls.Add(dgvOnKayitlar);
            panelGrid.Dock = DockStyle.Fill;
            panelGrid.Location = new Point(0, 0);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(1858, 942);
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
            dgvOnKayitlar.Location = new Point(20, 280);
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
            dgvOnKayitlar.Size = new Size(1958, 1142);
            dgvOnKayitlar.TabIndex = 31;
            dgvOnKayitlar.Tag = 5001;
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
            tabPageStok.Size = new Size(1858, 942);
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
            contextMenuStrip1.Size = new Size(189, 236);
            contextMenuStrip1.Text = "Ödeme Detayları";
            // 
            // yeniKayıtEkleToolStripMenuItem
            // 
            yeniKayıtEkleToolStripMenuItem.Image = (Image)resources.GetObject("yeniKayıtEkleToolStripMenuItem.Image");
            yeniKayıtEkleToolStripMenuItem.Name = "yeniKayıtEkleToolStripMenuItem";
            yeniKayıtEkleToolStripMenuItem.Size = new Size(188, 30);
            yeniKayıtEkleToolStripMenuItem.Text = "Yeni Kayıt Ekle";
            yeniKayıtEkleToolStripMenuItem.Click += yeniKayitEkle;
            // 
            // kayıtSilToolStripMenuItem
            // 
            kayıtSilToolStripMenuItem.Image = (Image)resources.GetObject("kayıtSilToolStripMenuItem.Image");
            kayıtSilToolStripMenuItem.Name = "kayıtSilToolStripMenuItem";
            kayıtSilToolStripMenuItem.Size = new Size(188, 30);
            kayıtSilToolStripMenuItem.Text = "Seçili Kayıdı Sil";
            kayıtSilToolStripMenuItem.Click += DeleteStripMenuItem_Click;
            // 
            // yenileToolStripMenuItem
            // 
            yenileToolStripMenuItem.Image = (Image)resources.GetObject("yenileToolStripMenuItem.Image");
            yenileToolStripMenuItem.Name = "yenileToolStripMenuItem";
            yenileToolStripMenuItem.Size = new Size(188, 30);
            yenileToolStripMenuItem.Text = "Yenile";
            yenileToolStripMenuItem.Click += yenileToolStripMenuItem_Click;
            // 
            // ödemeDetaylarıToolStripMenuItem
            // 
            ödemeDetaylarıToolStripMenuItem.Image = (Image)resources.GetObject("ödemeDetaylarıToolStripMenuItem.Image");
            ödemeDetaylarıToolStripMenuItem.Name = "ödemeDetaylarıToolStripMenuItem";
            ödemeDetaylarıToolStripMenuItem.Size = new Size(188, 30);
            ödemeDetaylarıToolStripMenuItem.Text = "Ödeme Detayları";
            ödemeDetaylarıToolStripMenuItem.Click += ödemeDetaylarıToolStripMenuItem_Click_1;
            // 
            // excelİleAktarToolStripMenuItem
            // 
            excelİleAktarToolStripMenuItem.Image = (Image)resources.GetObject("excelİleAktarToolStripMenuItem.Image");
            excelİleAktarToolStripMenuItem.Name = "excelİleAktarToolStripMenuItem";
            excelİleAktarToolStripMenuItem.Size = new Size(188, 30);
            excelİleAktarToolStripMenuItem.Text = "Excel ile Aktar";
            excelİleAktarToolStripMenuItem.Click += excelAktarToolStripMenuItem_Click;
            // 
            // geçmişHareketToolStripMenuItem
            // 
            geçmişHareketToolStripMenuItem.Image = (Image)resources.GetObject("geçmişHareketToolStripMenuItem.Image");
            geçmişHareketToolStripMenuItem.Name = "geçmişHareketToolStripMenuItem";
            geçmişHareketToolStripMenuItem.Size = new Size(188, 30);
            geçmişHareketToolStripMenuItem.Text = "Geçmiş Hareketler";
            geçmişHareketToolStripMenuItem.Click += loglarıGörüntüleToolStripMenuItem_Click;
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
            dataGridViewStok.Size = new Size(1852, 550);
            dataGridViewStok.TabIndex = 0;
            dataGridViewStok.Tag = 4010;
            dataGridViewStok.CellClick += dataGridViewStok_CellClick;
            dataGridViewStok.CellContentClick += dataGridViewStok_CellContentClick_1;
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
            tabPagePersonelYonetimi.Controls.Add(groupBox17);
            tabPagePersonelYonetimi.Controls.Add(dgvPersonelYonetimi);
            tabPagePersonelYonetimi.Location = new Point(4, 24);
            tabPagePersonelYonetimi.Name = "tabPagePersonelYonetimi";
            tabPagePersonelYonetimi.Padding = new Padding(3);
            tabPagePersonelYonetimi.Size = new Size(1858, 942);
            tabPagePersonelYonetimi.TabIndex = 4;
            tabPagePersonelYonetimi.Text = "Personel Yönetimi";
            tabPagePersonelYonetimi.UseVisualStyleBackColor = true;
            tabPagePersonelYonetimi.Click += tabPagePersonelYonetimi_Click;
            // 
            // groupBox17
            // 
            groupBox17.Controls.Add(btnPersonelSil);
            groupBox17.Controls.Add(btnPersonelTemizle);
            groupBox17.Controls.Add(btnPersonelKaydet);
            groupBox17.Controls.Add(btnPersonelGuncelle);
            groupBox17.Controls.Add(pbxPersonelPicture);
            groupBox17.Controls.Add(groupBox25);
            groupBox17.Controls.Add(groupBox22);
            groupBox17.Controls.Add(groupBox18);
            groupBox17.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox17.ForeColor = Color.CornflowerBlue;
            groupBox17.Location = new Point(864, 6);
            groupBox17.Name = "groupBox17";
            groupBox17.Size = new Size(988, 930);
            groupBox17.TabIndex = 17;
            groupBox17.TabStop = false;
            groupBox17.Text = "Personel Bilgileri";
            // 
            // btnPersonelSil
            // 
            btnPersonelSil.BackColor = Color.Transparent;
            btnPersonelSil.Cursor = Cursors.Hand;
            btnPersonelSil.FlatAppearance.BorderSize = 0;
            btnPersonelSil.FlatStyle = FlatStyle.Flat;
            btnPersonelSil.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPersonelSil.ForeColor = Color.White;
            btnPersonelSil.Image = (Image)resources.GetObject("btnPersonelSil.Image");
            btnPersonelSil.Location = new Point(717, 832);
            btnPersonelSil.Name = "btnPersonelSil";
            btnPersonelSil.Size = new Size(72, 77);
            btnPersonelSil.TabIndex = 38;
            btnPersonelSil.UseVisualStyleBackColor = false;
            btnPersonelSil.Click += btnPersonelSil_Click;
            // 
            // btnPersonelTemizle
            // 
            btnPersonelTemizle.BackColor = SystemColors.MenuHighlight;
            btnPersonelTemizle.FlatStyle = FlatStyle.Flat;
            btnPersonelTemizle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnPersonelTemizle.ForeColor = Color.White;
            btnPersonelTemizle.Location = new Point(6, 741);
            btnPersonelTemizle.Name = "btnPersonelTemizle";
            btnPersonelTemizle.Size = new Size(120, 50);
            btnPersonelTemizle.TabIndex = 40;
            btnPersonelTemizle.Text = "Temizle \U0001f9f9";
            btnPersonelTemizle.UseVisualStyleBackColor = false;
            btnPersonelTemizle.Click += btnPersonelTemizle_Click;
            // 
            // btnPersonelKaydet
            // 
            btnPersonelKaydet.BackColor = Color.Transparent;
            btnPersonelKaydet.FlatAppearance.BorderSize = 0;
            btnPersonelKaydet.FlatStyle = FlatStyle.Flat;
            btnPersonelKaydet.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPersonelKaydet.ForeColor = Color.Black;
            btnPersonelKaydet.Image = (Image)resources.GetObject("btnPersonelKaydet.Image");
            btnPersonelKaydet.Location = new Point(811, 832);
            btnPersonelKaydet.Name = "btnPersonelKaydet";
            btnPersonelKaydet.Size = new Size(72, 77);
            btnPersonelKaydet.TabIndex = 37;
            btnPersonelKaydet.TextAlign = ContentAlignment.MiddleRight;
            btnPersonelKaydet.UseVisualStyleBackColor = false;
            btnPersonelKaydet.Click += btnPersonelKaydet_Click;
            // 
            // btnPersonelGuncelle
            // 
            btnPersonelGuncelle.BackColor = Color.Transparent;
            btnPersonelGuncelle.Cursor = Cursors.Hand;
            btnPersonelGuncelle.FlatAppearance.BorderSize = 0;
            btnPersonelGuncelle.FlatStyle = FlatStyle.Flat;
            btnPersonelGuncelle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPersonelGuncelle.ForeColor = Color.White;
            btnPersonelGuncelle.Image = (Image)resources.GetObject("btnPersonelGuncelle.Image");
            btnPersonelGuncelle.Location = new Point(906, 832);
            btnPersonelGuncelle.Name = "btnPersonelGuncelle";
            btnPersonelGuncelle.Size = new Size(72, 77);
            btnPersonelGuncelle.TabIndex = 39;
            btnPersonelGuncelle.UseVisualStyleBackColor = false;
            btnPersonelGuncelle.Click += btnPersonelGuncelle_Click;
            // 
            // pbxPersonelPicture
            // 
            pbxPersonelPicture.Location = new Point(6, 22);
            pbxPersonelPicture.Name = "pbxPersonelPicture";
            pbxPersonelPicture.Size = new Size(152, 162);
            pbxPersonelPicture.TabIndex = 29;
            pbxPersonelPicture.TabStop = false;
            pbxPersonelPicture.Click += pbxPersonelPicture_Click;
            // 
            // groupBox25
            // 
            groupBox25.Controls.Add(txtPersonelIletişimAcilDurum);
            groupBox25.Controls.Add(txtPersonelMail);
            groupBox25.Controls.Add(txtPersonelTel);
            groupBox25.Controls.Add(txtPersonelAdres);
            groupBox25.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox25.ForeColor = Color.CornflowerBlue;
            groupBox25.Location = new Point(6, 190);
            groupBox25.Name = "groupBox25";
            groupBox25.Size = new Size(493, 155);
            groupBox25.TabIndex = 26;
            groupBox25.TabStop = false;
            groupBox25.Text = "İletişim Bilgileri";
            // 
            // txtPersonelIletişimAcilDurum
            // 
            txtPersonelIletişimAcilDurum.Location = new Point(6, 51);
            txtPersonelIletişimAcilDurum.Name = "txtPersonelIletişimAcilDurum";
            txtPersonelIletişimAcilDurum.PlaceholderText = "Acil Durumda Ulaşılacak Kişi";
            txtPersonelIletişimAcilDurum.Size = new Size(481, 23);
            txtPersonelIletişimAcilDurum.TabIndex = 7;
            // 
            // txtPersonelMail
            // 
            txtPersonelMail.Location = new Point(239, 22);
            txtPersonelMail.Name = "txtPersonelMail";
            txtPersonelMail.PlaceholderText = "Mail";
            txtPersonelMail.Size = new Size(248, 23);
            txtPersonelMail.TabIndex = 6;
            // 
            // txtPersonelTel
            // 
            txtPersonelTel.Location = new Point(6, 22);
            txtPersonelTel.Name = "txtPersonelTel";
            txtPersonelTel.PlaceholderText = "Telefon Numarası";
            txtPersonelTel.Size = new Size(226, 23);
            txtPersonelTel.TabIndex = 1;
            // 
            // txtPersonelAdres
            // 
            txtPersonelAdres.Location = new Point(6, 80);
            txtPersonelAdres.Multiline = true;
            txtPersonelAdres.Name = "txtPersonelAdres";
            txtPersonelAdres.PlaceholderText = "Adres";
            txtPersonelAdres.Size = new Size(481, 69);
            txtPersonelAdres.TabIndex = 5;
            // 
            // groupBox22
            // 
            groupBox22.Controls.Add(lblKimlikNum);
            groupBox22.Controls.Add(label3);
            groupBox22.Controls.Add(panel2);
            groupBox22.Controls.Add(panel1);
            groupBox22.Controls.Add(cbxPersonelUyruk);
            groupBox22.Controls.Add(label1);
            groupBox22.Controls.Add(dtpPersonelDG);
            groupBox22.Controls.Add(txtPersonelKimlik);
            groupBox22.Controls.Add(txtPersonelSoyad);
            groupBox22.Controls.Add(txtPersonelAd);
            groupBox22.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox22.ForeColor = Color.CornflowerBlue;
            groupBox22.Location = new Point(164, 22);
            groupBox22.Name = "groupBox22";
            groupBox22.Size = new Size(818, 162);
            groupBox22.TabIndex = 20;
            groupBox22.TabStop = false;
            groupBox22.Text = "Kişisel Bilgiler";
            // 
            // lblKimlikNum
            // 
            lblKimlikNum.AutoSize = true;
            lblKimlikNum.Location = new Point(347, 106);
            lblKimlikNum.Name = "lblKimlikNum";
            lblKimlikNum.Size = new Size(100, 15);
            lblKimlikNum.TabIndex = 33;
            lblKimlikNum.Text = "Kimlik Numarası :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(347, 51);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 32;
            label3.Text = "Uyruğu :";
            // 
            // panel2
            // 
            panel2.Controls.Add(rbtPersonelBekar);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(rbtPersonelEvli);
            panel2.Location = new Point(681, 98);
            panel2.Name = "panel2";
            panel2.Size = new Size(129, 51);
            panel2.TabIndex = 31;
            // 
            // rbtPersonelBekar
            // 
            rbtPersonelBekar.AutoSize = true;
            rbtPersonelBekar.ForeColor = Color.Black;
            rbtPersonelBekar.Location = new Point(68, 26);
            rbtPersonelBekar.Name = "rbtPersonelBekar";
            rbtPersonelBekar.Size = new Size(54, 19);
            rbtPersonelBekar.TabIndex = 31;
            rbtPersonelBekar.TabStop = true;
            rbtPersonelBekar.Text = "Bekar";
            rbtPersonelBekar.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(36, 8);
            label8.Name = "label8";
            label8.Size = new Size(68, 15);
            label8.TabIndex = 13;
            label8.Text = "Medeni Hal";
            // 
            // rbtPersonelEvli
            // 
            rbtPersonelEvli.AutoSize = true;
            rbtPersonelEvli.ForeColor = Color.Black;
            rbtPersonelEvli.Location = new Point(8, 26);
            rbtPersonelEvli.Name = "rbtPersonelEvli";
            rbtPersonelEvli.Size = new Size(43, 19);
            rbtPersonelEvli.TabIndex = 11;
            rbtPersonelEvli.Text = "Evli";
            rbtPersonelEvli.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(label9);
            panel1.Controls.Add(rbtPersonelErkek);
            panel1.Controls.Add(rbtPersonelKadin);
            panel1.Location = new Point(682, 21);
            panel1.Name = "panel1";
            panel1.Size = new Size(130, 51);
            panel1.TabIndex = 30;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(35, 8);
            label9.Name = "label9";
            label9.Size = new Size(48, 15);
            label9.TabIndex = 14;
            label9.Text = "Cinsiyet";
            // 
            // rbtPersonelErkek
            // 
            rbtPersonelErkek.AutoSize = true;
            rbtPersonelErkek.ForeColor = Color.Black;
            rbtPersonelErkek.Location = new Point(5, 26);
            rbtPersonelErkek.Name = "rbtPersonelErkek";
            rbtPersonelErkek.Size = new Size(53, 19);
            rbtPersonelErkek.TabIndex = 6;
            rbtPersonelErkek.Text = "Erkek";
            rbtPersonelErkek.UseVisualStyleBackColor = true;
            rbtPersonelErkek.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // rbtPersonelKadin
            // 
            rbtPersonelKadin.AutoSize = true;
            rbtPersonelKadin.ForeColor = Color.Black;
            rbtPersonelKadin.Location = new Point(66, 26);
            rbtPersonelKadin.Name = "rbtPersonelKadin";
            rbtPersonelKadin.Size = new Size(55, 19);
            rbtPersonelKadin.TabIndex = 7;
            rbtPersonelKadin.Text = "Kadın";
            rbtPersonelKadin.UseVisualStyleBackColor = true;
            // 
            // cbxPersonelUyruk
            // 
            cbxPersonelUyruk.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxPersonelUyruk.FormattingEnabled = true;
            cbxPersonelUyruk.Items.AddRange(new object[] { "T.C", "Yabancı" });
            cbxPersonelUyruk.Location = new Point(405, 49);
            cbxPersonelUyruk.Name = "cbxPersonelUyruk";
            cbxPersonelUyruk.Size = new Size(220, 23);
            cbxPersonelUyruk.TabIndex = 12;
            cbxPersonelUyruk.SelectedIndexChanged += cbxPersonelUyruk_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 104);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 9;
            label1.Text = "Doğum Tarihi :";
            label1.Click += label1_Click;
            // 
            // dtpPersonelDG
            // 
            dtpPersonelDG.Location = new Point(98, 98);
            dtpPersonelDG.Name = "dtpPersonelDG";
            dtpPersonelDG.Size = new Size(200, 23);
            dtpPersonelDG.TabIndex = 8;
            dtpPersonelDG.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // txtPersonelKimlik
            // 
            txtPersonelKimlik.Location = new Point(467, 101);
            txtPersonelKimlik.Name = "txtPersonelKimlik";
            txtPersonelKimlik.Size = new Size(158, 23);
            txtPersonelKimlik.TabIndex = 5;
            txtPersonelKimlik.TextChanged += txtPersonelKimlik_TextChanged;
            // 
            // txtPersonelSoyad
            // 
            txtPersonelSoyad.Location = new Point(141, 49);
            txtPersonelSoyad.Name = "txtPersonelSoyad";
            txtPersonelSoyad.PlaceholderText = "Soyadı";
            txtPersonelSoyad.Size = new Size(129, 23);
            txtPersonelSoyad.TabIndex = 2;
            // 
            // txtPersonelAd
            // 
            txtPersonelAd.Location = new Point(5, 49);
            txtPersonelAd.Name = "txtPersonelAd";
            txtPersonelAd.PlaceholderText = "Adı";
            txtPersonelAd.Size = new Size(130, 23);
            txtPersonelAd.TabIndex = 1;
            // 
            // groupBox18
            // 
            groupBox18.Controls.Add(panel3);
            groupBox18.Controls.Add(txtPersonelKidemTazminat);
            groupBox18.Controls.Add(txtPersonelAyrilmaNedeni);
            groupBox18.Controls.Add(txtPersonelEmeklilik);
            groupBox18.Controls.Add(txtPersonelSaglikSigorta);
            groupBox18.Controls.Add(txtPersonelSGKSicilNum);
            groupBox18.Controls.Add(txtPersonelYemekYol);
            groupBox18.Controls.Add(txtPersonelPrimVeEk);
            groupBox18.Controls.Add(label7);
            groupBox18.Controls.Add(cbxPersonelSigorta);
            groupBox18.Controls.Add(txtPersonelPersonelNo);
            groupBox18.Controls.Add(label6);
            groupBox18.Controls.Add(cbxPersonelCalismaSekli);
            groupBox18.Controls.Add(txtPersonelUniBolum);
            groupBox18.Controls.Add(label5);
            groupBox18.Controls.Add(cbxPersonelUniversite);
            groupBox18.Controls.Add(txtPersonelDepartman);
            groupBox18.Controls.Add(txtPersonelYabanciDil);
            groupBox18.Controls.Add(txtPersonelSertifika);
            groupBox18.Controls.Add(groupBox20);
            groupBox18.Controls.Add(cbxPersonelEgitimDurumu);
            groupBox18.Controls.Add(label4);
            groupBox18.Controls.Add(txtPersonelGorev);
            groupBox18.Controls.Add(dtpPersonelIseBaslamaTarihi);
            groupBox18.Controls.Add(cbxPersoneIIsAyrıldı);
            groupBox18.Controls.Add(dtpPersonelCıkısTarihi);
            groupBox18.Controls.Add(lblIstenCıkısTarihi);
            groupBox18.Controls.Add(label2);
            groupBox18.Controls.Add(txtPersonelMaas);
            groupBox18.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            groupBox18.ForeColor = Color.CornflowerBlue;
            groupBox18.Location = new Point(6, 351);
            groupBox18.Name = "groupBox18";
            groupBox18.Size = new Size(976, 384);
            groupBox18.TabIndex = 18;
            groupBox18.TabStop = false;
            groupBox18.Text = "Personel Özlük Bilgileri";
            groupBox18.Enter += groupBox18_Enter;
            // 
            // panel3
            // 
            panel3.Controls.Add(label10);
            panel3.Controls.Add(rbtPersonelEgitimGorevlisiEvet);
            panel3.Controls.Add(rbtPersonelEgitimGorevlisiHayir);
            panel3.Location = new Point(432, 16);
            panel3.Name = "panel3";
            panel3.Size = new Size(130, 51);
            panel3.TabIndex = 36;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(20, 8);
            label10.Name = "label10";
            label10.Size = new Size(89, 15);
            label10.TabIndex = 14;
            label10.Text = "Eğitim Görevlisi";
            // 
            // rbtPersonelEgitimGorevlisiEvet
            // 
            rbtPersonelEgitimGorevlisiEvet.AutoSize = true;
            rbtPersonelEgitimGorevlisiEvet.ForeColor = Color.Black;
            rbtPersonelEgitimGorevlisiEvet.Location = new Point(5, 26);
            rbtPersonelEgitimGorevlisiEvet.Name = "rbtPersonelEgitimGorevlisiEvet";
            rbtPersonelEgitimGorevlisiEvet.Size = new Size(47, 19);
            rbtPersonelEgitimGorevlisiEvet.TabIndex = 6;
            rbtPersonelEgitimGorevlisiEvet.Text = "Evet";
            rbtPersonelEgitimGorevlisiEvet.UseVisualStyleBackColor = true;
            // 
            // rbtPersonelEgitimGorevlisiHayir
            // 
            rbtPersonelEgitimGorevlisiHayir.AutoSize = true;
            rbtPersonelEgitimGorevlisiHayir.ForeColor = Color.Black;
            rbtPersonelEgitimGorevlisiHayir.Location = new Point(66, 26);
            rbtPersonelEgitimGorevlisiHayir.Name = "rbtPersonelEgitimGorevlisiHayir";
            rbtPersonelEgitimGorevlisiHayir.Size = new Size(53, 19);
            rbtPersonelEgitimGorevlisiHayir.TabIndex = 7;
            rbtPersonelEgitimGorevlisiHayir.Text = "Hayır";
            rbtPersonelEgitimGorevlisiHayir.UseVisualStyleBackColor = true;
            // 
            // txtPersonelKidemTazminat
            // 
            txtPersonelKidemTazminat.Location = new Point(367, 332);
            txtPersonelKidemTazminat.Name = "txtPersonelKidemTazminat";
            txtPersonelKidemTazminat.PlaceholderText = "Kıdem ve İhbar Tazminatı\n\n";
            txtPersonelKidemTazminat.Size = new Size(302, 23);
            txtPersonelKidemTazminat.TabIndex = 35;
            // 
            // txtPersonelAyrilmaNedeni
            // 
            txtPersonelAyrilmaNedeni.Location = new Point(367, 275);
            txtPersonelAyrilmaNedeni.Multiline = true;
            txtPersonelAyrilmaNedeni.Name = "txtPersonelAyrilmaNedeni";
            txtPersonelAyrilmaNedeni.PlaceholderText = "Ayrılma Nedeni";
            txtPersonelAyrilmaNedeni.Size = new Size(302, 51);
            txtPersonelAyrilmaNedeni.TabIndex = 34;
            // 
            // txtPersonelEmeklilik
            // 
            txtPersonelEmeklilik.Location = new Point(81, 342);
            txtPersonelEmeklilik.Name = "txtPersonelEmeklilik";
            txtPersonelEmeklilik.PlaceholderText = "Emeklilik Bilgileri\n\n";
            txtPersonelEmeklilik.Size = new Size(151, 23);
            txtPersonelEmeklilik.TabIndex = 33;
            // 
            // txtPersonelSaglikSigorta
            // 
            txtPersonelSaglikSigorta.Location = new Point(169, 303);
            txtPersonelSaglikSigorta.Name = "txtPersonelSaglikSigorta";
            txtPersonelSaglikSigorta.PlaceholderText = "Sağlık Sigortası Bilgileri\n\n";
            txtPersonelSaglikSigorta.Size = new Size(151, 23);
            txtPersonelSaglikSigorta.TabIndex = 32;
            // 
            // txtPersonelSGKSicilNum
            // 
            txtPersonelSGKSicilNum.Location = new Point(9, 303);
            txtPersonelSGKSicilNum.Name = "txtPersonelSGKSicilNum";
            txtPersonelSGKSicilNum.PlaceholderText = "SGK Sicil Numarası";
            txtPersonelSGKSicilNum.Size = new Size(151, 23);
            txtPersonelSGKSicilNum.TabIndex = 31;
            // 
            // txtPersonelYemekYol
            // 
            txtPersonelYemekYol.Location = new Point(81, 252);
            txtPersonelYemekYol.Name = "txtPersonelYemekYol";
            txtPersonelYemekYol.PlaceholderText = "Yemek ve Yol Yardımı";
            txtPersonelYemekYol.Size = new Size(151, 23);
            txtPersonelYemekYol.TabIndex = 30;
            // 
            // txtPersonelPrimVeEk
            // 
            txtPersonelPrimVeEk.Location = new Point(166, 219);
            txtPersonelPrimVeEk.Name = "txtPersonelPrimVeEk";
            txtPersonelPrimVeEk.PlaceholderText = "Prim ve Ek Ödemeler";
            txtPersonelPrimVeEk.Size = new Size(151, 23);
            txtPersonelPrimVeEk.TabIndex = 29;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(9, 183);
            label7.Name = "label7";
            label7.Size = new Size(85, 15);
            label7.TabIndex = 28;
            label7.Text = "Sigorta Bilgisi :";
            label7.Visible = false;
            label7.Click += label7_Click;
            // 
            // cbxPersonelSigorta
            // 
            cbxPersonelSigorta.FormattingEnabled = true;
            cbxPersonelSigorta.Location = new Point(100, 180);
            cbxPersonelSigorta.Name = "cbxPersonelSigorta";
            cbxPersonelSigorta.Size = new Size(220, 23);
            cbxPersonelSigorta.TabIndex = 27;
            // 
            // txtPersonelPersonelNo
            // 
            txtPersonelPersonelNo.Location = new Point(9, 141);
            txtPersonelPersonelNo.Name = "txtPersonelPersonelNo";
            txtPersonelPersonelNo.PlaceholderText = "Personel Numarası";
            txtPersonelPersonelNo.Size = new Size(311, 23);
            txtPersonelPersonelNo.TabIndex = 26;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(9, 107);
            label6.Name = "label6";
            label6.Size = new Size(82, 15);
            label6.TabIndex = 25;
            label6.Text = "Çalışma Şekli :";
            label6.Visible = false;
            // 
            // cbxPersonelCalismaSekli
            // 
            cbxPersonelCalismaSekli.FormattingEnabled = true;
            cbxPersonelCalismaSekli.Location = new Point(100, 104);
            cbxPersonelCalismaSekli.Name = "cbxPersonelCalismaSekli";
            cbxPersonelCalismaSekli.Size = new Size(220, 23);
            cbxPersonelCalismaSekli.TabIndex = 24;
            // 
            // txtPersonelUniBolum
            // 
            txtPersonelUniBolum.Location = new Point(361, 180);
            txtPersonelUniBolum.Name = "txtPersonelUniBolum";
            txtPersonelUniBolum.PlaceholderText = "Bölümü";
            txtPersonelUniBolum.Size = new Size(295, 23);
            txtPersonelUniBolum.TabIndex = 23;
            txtPersonelUniBolum.Visible = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(361, 141);
            label5.Name = "label5";
            label5.Size = new Size(65, 15);
            label5.TabIndex = 20;
            label5.Text = "Üniversite :";
            label5.Visible = false;
            // 
            // cbxPersonelUniversite
            // 
            cbxPersonelUniversite.FormattingEnabled = true;
            cbxPersonelUniversite.Location = new Point(432, 138);
            cbxPersonelUniversite.Name = "cbxPersonelUniversite";
            cbxPersonelUniversite.Size = new Size(224, 23);
            cbxPersonelUniversite.TabIndex = 19;
            cbxPersonelUniversite.Visible = false;
            // 
            // txtPersonelDepartman
            // 
            txtPersonelDepartman.Location = new Point(9, 65);
            txtPersonelDepartman.Name = "txtPersonelDepartman";
            txtPersonelDepartman.PlaceholderText = "Departmanı";
            txtPersonelDepartman.Size = new Size(151, 23);
            txtPersonelDepartman.TabIndex = 18;
            // 
            // txtPersonelYabanciDil
            // 
            txtPersonelYabanciDil.Location = new Point(701, 116);
            txtPersonelYabanciDil.Multiline = true;
            txtPersonelYabanciDil.Name = "txtPersonelYabanciDil";
            txtPersonelYabanciDil.PlaceholderText = "Yabancı Dil Bilgisi\n\n";
            txtPersonelYabanciDil.Size = new Size(269, 94);
            txtPersonelYabanciDil.TabIndex = 17;
            // 
            // txtPersonelSertifika
            // 
            txtPersonelSertifika.Location = new Point(700, 16);
            txtPersonelSertifika.Multiline = true;
            txtPersonelSertifika.Name = "txtPersonelSertifika";
            txtPersonelSertifika.PlaceholderText = "Sertifikalar ve Eğitimler";
            txtPersonelSertifika.Size = new Size(269, 94);
            txtPersonelSertifika.TabIndex = 16;
            // 
            // groupBox20
            // 
            groupBox20.Controls.Add(textBox1);
            groupBox20.Location = new Point(911, -6);
            groupBox20.Name = "groupBox20";
            groupBox20.Size = new Size(8, 8);
            groupBox20.TabIndex = 15;
            groupBox20.TabStop = false;
            groupBox20.Text = "groupBox20";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(-96, -7);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Görevi";
            textBox1.Size = new Size(200, 23);
            textBox1.TabIndex = 13;
            // 
            // cbxPersonelEgitimDurumu
            // 
            cbxPersonelEgitimDurumu.FormattingEnabled = true;
            cbxPersonelEgitimDurumu.Items.AddRange(new object[] { "Okuryazar Değil", "İlkokul Mezunu ", "Ortaokul Mezunu", "Lise Mezunu", "Önlisans (Devam Ediyor)", "Lisans (Devam Ediyor)", "Yüksek Lisans(Devam Ediyor)", "Doktora (Devam Ediyor)", "Önlisans Mezunu", "Lisans Mezunu", "Yüksek Lisans", "Doktora " });
            cbxPersonelEgitimDurumu.Location = new Point(462, 92);
            cbxPersonelEgitimDurumu.Name = "cbxPersonelEgitimDurumu";
            cbxPersonelEgitimDurumu.Size = new Size(194, 23);
            cbxPersonelEgitimDurumu.TabIndex = 14;
            cbxPersonelEgitimDurumu.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(361, 95);
            label4.Name = "label4";
            label4.Size = new Size(95, 15);
            label4.TabIndex = 13;
            label4.Text = "Eğitim Durumu :";
            label4.Click += label4_Click;
            // 
            // txtPersonelGorev
            // 
            txtPersonelGorev.Location = new Point(167, 65);
            txtPersonelGorev.Name = "txtPersonelGorev";
            txtPersonelGorev.PlaceholderText = "Görevi / Ünvanı\n\n";
            txtPersonelGorev.Size = new Size(153, 23);
            txtPersonelGorev.TabIndex = 12;
            txtPersonelGorev.TextChanged += txtPersonelGorev_TextChanged;
            // 
            // dtpPersonelIseBaslamaTarihi
            // 
            dtpPersonelIseBaslamaTarihi.Location = new Point(122, 29);
            dtpPersonelIseBaslamaTarihi.Name = "dtpPersonelIseBaslamaTarihi";
            dtpPersonelIseBaslamaTarihi.Size = new Size(206, 23);
            dtpPersonelIseBaslamaTarihi.TabIndex = 11;
            // 
            // cbxPersoneIIsAyrıldı
            // 
            cbxPersoneIIsAyrıldı.AutoSize = true;
            cbxPersoneIIsAyrıldı.Location = new Point(367, 221);
            cbxPersoneIIsAyrıldı.Name = "cbxPersoneIIsAyrıldı";
            cbxPersoneIIsAyrıldı.Size = new Size(89, 19);
            cbxPersoneIIsAyrıldı.TabIndex = 10;
            cbxPersoneIIsAyrıldı.Text = "İşten Ayrıldı";
            cbxPersoneIIsAyrıldı.UseVisualStyleBackColor = true;
            cbxPersoneIIsAyrıldı.CheckedChanged += cbxPersoneIIsAyrıldı_CheckedChanged;
            // 
            // dtpPersonelCıkısTarihi
            // 
            dtpPersonelCıkısTarihi.Location = new Point(469, 246);
            dtpPersonelCıkısTarihi.Name = "dtpPersonelCıkısTarihi";
            dtpPersonelCıkısTarihi.Size = new Size(200, 23);
            dtpPersonelCıkısTarihi.TabIndex = 9;
            // 
            // lblIstenCıkısTarihi
            // 
            lblIstenCıkısTarihi.AutoSize = true;
            lblIstenCıkısTarihi.Location = new Point(367, 252);
            lblIstenCıkısTarihi.Name = "lblIstenCıkısTarihi";
            lblIstenCıkısTarihi.Size = new Size(96, 15);
            lblIstenCıkısTarihi.TabIndex = 3;
            lblIstenCıkısTarihi.Text = "İşten Çıkış tarihi :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 35);
            label2.Name = "label2";
            label2.Size = new Size(107, 15);
            label2.TabIndex = 2;
            label2.Text = "İşe Başlama Tarihi :";
            label2.Click += label2_Click;
            // 
            // txtPersonelMaas
            // 
            txtPersonelMaas.Location = new Point(6, 219);
            txtPersonelMaas.Name = "txtPersonelMaas";
            txtPersonelMaas.PlaceholderText = "Maaş";
            txtPersonelMaas.Size = new Size(151, 23);
            txtPersonelMaas.TabIndex = 1;
            txtPersonelMaas.TextChanged += txtPersonelMaas_TextChanged;
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
            dgvPersonelYonetimi.Size = new Size(852, 930);
            dgvPersonelYonetimi.TabIndex = 1;
            dgvPersonelYonetimi.Tag = 4020;
            dgvPersonelYonetimi.CellContentClick += dgvPersonelYonetimi_CellContentClick;
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
            tabPageGelirGider.Size = new Size(1858, 942);
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
            materialLabel3.Location = new Point(1362, 42);
            materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel3.Name = "materialLabel3";
            materialLabel3.Size = new Size(69, 19);
            materialLabel3.TabIndex = 28;
            materialLabel3.Text = "Son Giriş:";
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
            // arşivToolStripMenuItem
            // 
            arşivToolStripMenuItem.Image = (Image)resources.GetObject("arşivToolStripMenuItem.Image");
            arşivToolStripMenuItem.Name = "arşivToolStripMenuItem";
            arşivToolStripMenuItem.Size = new Size(188, 30);
            arşivToolStripMenuItem.Text = "Arşiv";
            arşivToolStripMenuItem.Click += arşivToolStripMenuItem_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnablePreventFocusChange;
            ClientSize = new Size(1872, 1037);
            Controls.Add(materialLabel3);
            Controls.Add(tabControl);
            Font = new Font("Times New Roman", 9F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form2";
            Text = "Anaokulu Yönetimi Sistemi";
            WindowState = FormWindowState.Maximized;
            FormClosing += Form2_FormClosing;
            Load += Form2_Load;
            tabControl.ResumeLayout(false);
            tabPageOgrenciOnKayit.ResumeLayout(false);
            panelForm.ResumeLayout(false);
            panelForm.PerformLayout();
            panelGrid.ResumeLayout(false);
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
            groupBox17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbxPersonelPicture).EndInit();
            groupBox25.ResumeLayout(false);
            groupBox25.PerformLayout();
            groupBox22.ResumeLayout(false);
            groupBox22.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox18.ResumeLayout(false);
            groupBox18.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            groupBox20.ResumeLayout(false);
            groupBox20.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPersonelYonetimi).EndInit();
            tabPageGelirGider.ResumeLayout(false);
            tabPageGelirGider.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridOdeme).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericAmount).EndInit();
            tabPageOzelRaporlar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)salesGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private GroupBox groupBox25;
        private TextBox txtPersonelTel;
        private GroupBox groupBox22;
        private TextBox txtPersonelAd;
        private GroupBox groupBox18;
        private GroupBox groupBox17;
        private TextBox txtPersonelMaas;
        private DataGridView dgvPersonelYonetimi;
        private PictureBox pbxPersonelPicture;
        private TextBox txtPersonelKimlik;
        private TextBox txtPersonelSoyad;
        private RadioButton rbtPersonelKadin;
        private RadioButton rbtPersonelErkek;
        private DateTimePicker dtpPersonelDG;
        private Label label1;
        private RadioButton rbtPersonelEvli;
        private TextBox txtPersonelMail;
        private TextBox txtPersonelAdres;
        private Label lblIstenCıkısTarihi;
        private Label label2;
        private CheckBox cbxPersoneIIsAyrıldı;
        private DateTimePicker dtpPersonelCıkısTarihi;
        private ComboBox cbxPersonelUyruk;
        private TextBox txtPersonelGorev;
        private DateTimePicker dtpPersonelIseBaslamaTarihi;
        private TextBox txtPersonelIletişimAcilDurum;
        private ComboBox cbxPersonelEgitimDurumu;
        private Label label4;
        private TextBox txtPersonelYabanciDil;
        private TextBox txtPersonelSertifika;
        private GroupBox groupBox20;
        private TextBox textBox1;
        private TextBox txtPersonelDepartman;
        private TextBox txtPersonelUniBolum;
        private Label label5;
        private ComboBox cbxPersonelUniversite;
        private Label label6;
        private ComboBox cbxPersonelCalismaSekli;
        private TextBox txtPersonelPersonelNo;
        private Label label7;
        private ComboBox cbxPersonelSigorta;
        private TextBox txtPersonelYemekYol;
        private TextBox txtPersonelPrimVeEk;
        private TextBox txtPersonelEmeklilik;
        private TextBox txtPersonelSaglikSigorta;
        private TextBox txtPersonelSGKSicilNum;
        private TextBox txtPersonelAyrilmaNedeni;
        private TextBox txtPersonelKidemTazminat;
        private ToolStripMenuItem yeniKayıtEkleToolStripMenuItem;
        private ToolStripMenuItem yenileToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private ToolStripMenuItem kayıtSilToolStripMenuItem;
        public DataGridView dataGridViewStok;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ToolStripMenuItem geçmişHareketToolStripMenuItem;
        private Panel panel2;
        private RadioButton rbtPersonelBekar;
        private Label label8;
        private Panel panel1;
        private Label label9;
        private Button btnPersonelSil;
        private Button btnPersonelKaydet;
        private Button btnPersonelTemizle;

        private Button btnPersonelGuncelle;
        private Label lblKimlikNum;
        private Label label3;
        private Panel panel3;
        private Label label10;
        private RadioButton rbtPersonelEgitimGorevlisiEvet;
        private RadioButton rbtPersonelEgitimGorevlisiHayir;
        public DataGridView dataGridView1;
        private Panel panelForm;
        private Panel panelGrid;
        private Button FaturaBtn;
        private ToolStripMenuItem arşivToolStripMenuItem;
    }
}