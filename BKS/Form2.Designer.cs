using System;
using System.Drawing;
using System.Windows.Forms;

namespace BKS;

partial class Form2
{
    private System.ComponentModel.IContainer components = null;

    private TabControl tabControl;
    public DataGridView dataGridViewStok;
    private TableLayoutPanel listdataGridViewStok;
    private TableLayoutPanel listdataGridViewStokToolbar;
    private Label listdataGridViewStokTitle;
    private TextBox txtOgrenciYonetimiAra;
    private Button listdataGridViewStokClear;
    private Button listdataGridViewStokColumns;
    private Label listdataGridViewStokCount;
    private TabPage tabPageStok;
    public DataGridView dgvPersonelYonetimi;
    private TableLayoutPanel listdgvPersonelYonetimi;
    private TableLayoutPanel listdgvPersonelYonetimiToolbar;
    private Label listdgvPersonelYonetimiTitle;
    private TextBox _personnelSearch;
    private Button listdgvPersonelYonetimiClear;
    private Button listdgvPersonelYonetimiColumns;
    private Label listdgvPersonelYonetimiCount;
    private TabPage tabPagePersonelYonetimi;
    private DataGridView dgvOnKayitlar;
    private TableLayoutPanel listdgvOnKayitlar;
    private TableLayoutPanel listdgvOnKayitlarToolbar;
    private Label listdgvOnKayitlarTitle;
    private TextBox txtPreRegistrationSearch;
    private Button listdgvOnKayitlarClear;
    private Button listdgvOnKayitlarColumns;
    private Label listdgvOnKayitlarCount;
    private TableLayoutPanel fieldsdgvOnKayitlar;
    private TextBox txtOnKayitAd;
    private Label lbl_txtOnKayitAd;
    private TextBox txtOnKayitSoyad;
    private Label lbl_txtOnKayitSoyad;
    private DateTimePicker dtpOnKayitDogumTarihi;
    private Label lbl_dtpOnKayitDogumTarihi;
    private TextBox txtOnKayitBabaAd;
    private Label lbl_txtOnKayitBabaAd;
    private TextBox txtOnKayitVeliTel;
    private Label lbl_txtOnKayitVeliTel;
    private TextBox txtOnKayitNot;
    private Label lbl_txtOnKayitNot;
    private FlowLayoutPanel actionsdgvOnKayitlar;
    private Button btnOnKayitEkle;
    private Button btnKesinKayitYap;
    private Button btnOnKayitSil;
    private TableLayoutPanel editordgvOnKayitlar;
    private TableLayoutPanel bodydgvOnKayitlar;
    public TabPage tabPageOgrenciOnKayit;
    private DataGridView DgvOgrenciYonetimiSiniflar;
    private TableLayoutPanel listDgvOgrenciYonetimiSiniflar;
    private TableLayoutPanel listDgvOgrenciYonetimiSiniflarToolbar;
    private Label listDgvOgrenciYonetimiSiniflarTitle;
    private TextBox txtClassesSearch;
    private Button listDgvOgrenciYonetimiSiniflarClear;
    private Button listDgvOgrenciYonetimiSiniflarColumns;
    private Label listDgvOgrenciYonetimiSiniflarCount;
    private TableLayoutPanel fieldsDgvOgrenciYonetimiSiniflar;
    private TextBox txtOgrenciYonetimiSınıfAdı;
    private Label lbl_txtOgrenciYonetimiSınıfAdı;
    private ComboBox cbxOgrenciYonetimiYasGrubu;
    private Label lbl_cbxOgrenciYonetimiYasGrubu;
    private ComboBox cbxOgrenciYonetimiOgretmen;
    private Label lbl_cbxOgrenciYonetimiOgretmen;
    private FlowLayoutPanel actionsDgvOgrenciYonetimiSiniflar;
    private Button btnOgrenciYonetimiSinifKaydet;
    private Button btnOgrenciYonetimiSinifGuncelle;
    private Button btnOgrenciYonetimiSinifSil;
    private TableLayoutPanel editorDgvOgrenciYonetimiSiniflar;
    private TableLayoutPanel bodyDgvOgrenciYonetimiSiniflar;
    private TabPage _classesPage;
    private DataGridView dataOgrVw;
    private TableLayoutPanel listdataOgrVw;
    private TableLayoutPanel listdataOgrVwToolbar;
    private Label listdataOgrVwTitle;
    private TextBox txtPaymentsSearch;
    private Button listdataOgrVwClear;
    private Button listdataOgrVwColumns;
    private Label listdataOgrVwCount;
    private TableLayoutPanel fieldsdataOgrVw;
    private ComboBox comboBoxStok;
    private Label lbl_comboBoxStok;
    private NumericUpDown numericQuantitySold;
    private Label lbl_numericQuantitySold;
    private FlowLayoutPanel actionsdataOgrVw;
    private Button btnMakeSale;
    private TableLayoutPanel editordataOgrVw;
    private TableLayoutPanel bodydataOgrVw;
    private TabPage tabPageSatis;
    private FlowLayoutPanel pnlTransactionType;
    private RadioButton radioIncome;
    private RadioButton radioExpense;
    private DataGridView dataGridOdeme;
    private TableLayoutPanel listdataGridOdeme;
    private TableLayoutPanel listdataGridOdemeToolbar;
    private Label listdataGridOdemeTitle;
    private TextBox txtFinanceSearch;
    private Button listdataGridOdemeClear;
    private Button listdataGridOdemeColumns;
    private Label listdataGridOdemeCount;
    private TableLayoutPanel fieldsdataGridOdeme;
    private TextBox txtDescription;
    private Label lbl_txtDescription;
    private NumericUpDown numericAmount;
    private Label lbl_numericAmount;
    private Label lbl_pnlTransactionType;
    private FlowLayoutPanel actionsdataGridOdeme;
    private Button btnAddIncomeExpense;
    private Button FaturaBtn;
    private TableLayoutPanel editordataGridOdeme;
    private TableLayoutPanel bodydataGridOdeme;
    private Label lblFinanceSummary;
    private TabPage tabPageGelirGider;
    private DataGridView salesGrid;
    private TableLayoutPanel listsalesGrid;
    private TableLayoutPanel listsalesGridToolbar;
    private Label listsalesGridTitle;
    private TextBox txtReportsSearch;
    private Button listsalesGridClear;
    private Button listsalesGridColumns;
    private Label listsalesGridCount;
    private TabPage tabPageOzelRaporlar;
    private TabPage _homePage;
    private TableLayoutPanel pnlHome;
    private Label lblHomeTitle;
    private Label lblHomeSubtitle;
    private FlowLayoutPanel flpDashboardCards;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem ödemeDetaylarıToolStripMenuItem;
    private ToolStripMenuItem geçmişHareketToolStripMenuItem;
    private ToolStripMenuItem excelİleAktarToolStripMenuItem;
    private ToolStripMenuItem yeniKayıtEkleToolStripMenuItem;
    private ToolStripMenuItem yenileToolStripMenuItem;
    private ToolStripMenuItem kayıtSilToolStripMenuItem;
    private ToolStripMenuItem arşivToolStripMenuItem;
    private System.Windows.Forms.Timer timer1;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        var dataGridViewCellStyle1 = new DataGridViewCellStyle();
        var dataGridViewCellStyle2 = new DataGridViewCellStyle();
        var dataGridViewCellStyle3 = new DataGridViewCellStyle();
        var dataGridViewCellStyle4 = new DataGridViewCellStyle();
        var dataGridViewCellStyle5 = new DataGridViewCellStyle();
        var dataGridViewCellStyle6 = new DataGridViewCellStyle();
        var dataGridViewCellStyle7 = new DataGridViewCellStyle();
        var dataGridViewCellStyle8 = new DataGridViewCellStyle();
        var dataGridViewCellStyle9 = new DataGridViewCellStyle();
        var dataGridViewCellStyle10 = new DataGridViewCellStyle();
        var dataGridViewCellStyle11 = new DataGridViewCellStyle();
        var dataGridViewCellStyle12 = new DataGridViewCellStyle();
        var dataGridViewCellStyle13 = new DataGridViewCellStyle();
        var dataGridViewCellStyle14 = new DataGridViewCellStyle();
        var dataGridViewCellStyle15 = new DataGridViewCellStyle();
        var dataGridViewCellStyle16 = new DataGridViewCellStyle();
        var dataGridViewCellStyle17 = new DataGridViewCellStyle();
        var dataGridViewCellStyle18 = new DataGridViewCellStyle();
        var dataGridViewCellStyle19 = new DataGridViewCellStyle();
        var dataGridViewCellStyle20 = new DataGridViewCellStyle();
        var dataGridViewCellStyle21 = new DataGridViewCellStyle();
        tabControl = new TabControl();
        tabPageStok = new TabPage();
        listdataGridViewStok = new TableLayoutPanel();
        listdataGridViewStokToolbar = new TableLayoutPanel();
        listdataGridViewStokTitle = new Label();
        txtOgrenciYonetimiAra = new TextBox();
        listdataGridViewStokClear = new Button();
        listdataGridViewStokColumns = new Button();
        dataGridViewStok = new DataGridView();
        contextMenuStrip1 = new ContextMenuStrip(components);
        ödemeDetaylarıToolStripMenuItem = new ToolStripMenuItem();
        geçmişHareketToolStripMenuItem = new ToolStripMenuItem();
        excelİleAktarToolStripMenuItem = new ToolStripMenuItem();
        yeniKayıtEkleToolStripMenuItem = new ToolStripMenuItem();
        yenileToolStripMenuItem = new ToolStripMenuItem();
        kayıtSilToolStripMenuItem = new ToolStripMenuItem();
        arşivToolStripMenuItem = new ToolStripMenuItem();
        listdataGridViewStokCount = new Label();
        tabPagePersonelYonetimi = new TabPage();
        listdgvPersonelYonetimi = new TableLayoutPanel();
        listdgvPersonelYonetimiToolbar = new TableLayoutPanel();
        listdgvPersonelYonetimiTitle = new Label();
        _personnelSearch = new TextBox();
        listdgvPersonelYonetimiClear = new Button();
        listdgvPersonelYonetimiColumns = new Button();
        dgvPersonelYonetimi = new DataGridView();
        listdgvPersonelYonetimiCount = new Label();
        tabPageOgrenciOnKayit = new TabPage();
        bodydgvOnKayitlar = new TableLayoutPanel();
        editordgvOnKayitlar = new TableLayoutPanel();
        fieldsdgvOnKayitlar = new TableLayoutPanel();
        lbl_txtOnKayitAd = new Label();
        txtOnKayitAd = new TextBox();
        lbl_txtOnKayitSoyad = new Label();
        txtOnKayitSoyad = new TextBox();
        lbl_dtpOnKayitDogumTarihi = new Label();
        dtpOnKayitDogumTarihi = new DateTimePicker();
        lbl_txtOnKayitBabaAd = new Label();
        txtOnKayitBabaAd = new TextBox();
        lbl_txtOnKayitVeliTel = new Label();
        txtOnKayitVeliTel = new TextBox();
        lbl_txtOnKayitNot = new Label();
        txtOnKayitNot = new TextBox();
        actionsdgvOnKayitlar = new FlowLayoutPanel();
        btnOnKayitEkle = new Button();
        btnKesinKayitYap = new Button();
        btnOnKayitSil = new Button();
        listdgvOnKayitlar = new TableLayoutPanel();
        listdgvOnKayitlarToolbar = new TableLayoutPanel();
        listdgvOnKayitlarTitle = new Label();
        txtPreRegistrationSearch = new TextBox();
        listdgvOnKayitlarClear = new Button();
        listdgvOnKayitlarColumns = new Button();
        dgvOnKayitlar = new DataGridView();
        listdgvOnKayitlarCount = new Label();
        _classesPage = new TabPage();
        bodyDgvOgrenciYonetimiSiniflar = new TableLayoutPanel();
        editorDgvOgrenciYonetimiSiniflar = new TableLayoutPanel();
        fieldsDgvOgrenciYonetimiSiniflar = new TableLayoutPanel();
        lbl_txtOgrenciYonetimiSınıfAdı = new Label();
        txtOgrenciYonetimiSınıfAdı = new TextBox();
        lbl_cbxOgrenciYonetimiYasGrubu = new Label();
        cbxOgrenciYonetimiYasGrubu = new ComboBox();
        lbl_cbxOgrenciYonetimiOgretmen = new Label();
        cbxOgrenciYonetimiOgretmen = new ComboBox();
        actionsDgvOgrenciYonetimiSiniflar = new FlowLayoutPanel();
        btnOgrenciYonetimiSinifKaydet = new Button();
        btnOgrenciYonetimiSinifGuncelle = new Button();
        btnOgrenciYonetimiSinifSil = new Button();
        listDgvOgrenciYonetimiSiniflar = new TableLayoutPanel();
        listDgvOgrenciYonetimiSiniflarToolbar = new TableLayoutPanel();
        listDgvOgrenciYonetimiSiniflarTitle = new Label();
        txtClassesSearch = new TextBox();
        listDgvOgrenciYonetimiSiniflarClear = new Button();
        listDgvOgrenciYonetimiSiniflarColumns = new Button();
        DgvOgrenciYonetimiSiniflar = new DataGridView();
        listDgvOgrenciYonetimiSiniflarCount = new Label();
        tabPageSatis = new TabPage();
        bodydataOgrVw = new TableLayoutPanel();
        editordataOgrVw = new TableLayoutPanel();
        fieldsdataOgrVw = new TableLayoutPanel();
        lbl_comboBoxStok = new Label();
        comboBoxStok = new ComboBox();
        lbl_numericQuantitySold = new Label();
        numericQuantitySold = new NumericUpDown();
        actionsdataOgrVw = new FlowLayoutPanel();
        btnMakeSale = new Button();
        listdataOgrVw = new TableLayoutPanel();
        listdataOgrVwToolbar = new TableLayoutPanel();
        listdataOgrVwTitle = new Label();
        txtPaymentsSearch = new TextBox();
        listdataOgrVwClear = new Button();
        listdataOgrVwColumns = new Button();
        dataOgrVw = new DataGridView();
        listdataOgrVwCount = new Label();
        tabPageGelirGider = new TabPage();
        bodydataGridOdeme = new TableLayoutPanel();
        lblFinanceSummary = new Label();
        editordataGridOdeme = new TableLayoutPanel();
        fieldsdataGridOdeme = new TableLayoutPanel();
        lbl_txtDescription = new Label();
        txtDescription = new TextBox();
        lbl_numericAmount = new Label();
        numericAmount = new NumericUpDown();
        lbl_pnlTransactionType = new Label();
        pnlTransactionType = new FlowLayoutPanel();
        radioIncome = new RadioButton();
        radioExpense = new RadioButton();
        actionsdataGridOdeme = new FlowLayoutPanel();
        btnAddIncomeExpense = new Button();
        FaturaBtn = new Button();
        listdataGridOdeme = new TableLayoutPanel();
        listdataGridOdemeToolbar = new TableLayoutPanel();
        listdataGridOdemeTitle = new Label();
        txtFinanceSearch = new TextBox();
        listdataGridOdemeClear = new Button();
        listdataGridOdemeColumns = new Button();
        dataGridOdeme = new DataGridView();
        listdataGridOdemeCount = new Label();
        tabPageOzelRaporlar = new TabPage();
        listsalesGrid = new TableLayoutPanel();
        listsalesGridToolbar = new TableLayoutPanel();
        listsalesGridTitle = new Label();
        txtReportsSearch = new TextBox();
        listsalesGridClear = new Button();
        listsalesGridColumns = new Button();
        salesGrid = new DataGridView();
        listsalesGridCount = new Label();
        _homePage = new TabPage();
        pnlHome = new TableLayoutPanel();
        lblHomeTitle = new Label();
        lblHomeSubtitle = new Label();
        flpDashboardCards = new FlowLayoutPanel();
        timer1 = new System.Windows.Forms.Timer(components);
        tabControl.SuspendLayout();
        tabPageStok.SuspendLayout();
        listdataGridViewStok.SuspendLayout();
        listdataGridViewStokToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridViewStok).BeginInit();
        contextMenuStrip1.SuspendLayout();
        tabPagePersonelYonetimi.SuspendLayout();
        listdgvPersonelYonetimi.SuspendLayout();
        listdgvPersonelYonetimiToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvPersonelYonetimi).BeginInit();
        tabPageOgrenciOnKayit.SuspendLayout();
        bodydgvOnKayitlar.SuspendLayout();
        editordgvOnKayitlar.SuspendLayout();
        fieldsdgvOnKayitlar.SuspendLayout();
        actionsdgvOnKayitlar.SuspendLayout();
        listdgvOnKayitlar.SuspendLayout();
        listdgvOnKayitlarToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvOnKayitlar).BeginInit();
        _classesPage.SuspendLayout();
        bodyDgvOgrenciYonetimiSiniflar.SuspendLayout();
        editorDgvOgrenciYonetimiSiniflar.SuspendLayout();
        fieldsDgvOgrenciYonetimiSiniflar.SuspendLayout();
        actionsDgvOgrenciYonetimiSiniflar.SuspendLayout();
        listDgvOgrenciYonetimiSiniflar.SuspendLayout();
        listDgvOgrenciYonetimiSiniflarToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)DgvOgrenciYonetimiSiniflar).BeginInit();
        tabPageSatis.SuspendLayout();
        bodydataOgrVw.SuspendLayout();
        editordataOgrVw.SuspendLayout();
        fieldsdataOgrVw.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numericQuantitySold).BeginInit();
        actionsdataOgrVw.SuspendLayout();
        listdataOgrVw.SuspendLayout();
        listdataOgrVwToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dataOgrVw).BeginInit();
        tabPageGelirGider.SuspendLayout();
        bodydataGridOdeme.SuspendLayout();
        editordataGridOdeme.SuspendLayout();
        fieldsdataGridOdeme.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numericAmount).BeginInit();
        pnlTransactionType.SuspendLayout();
        actionsdataGridOdeme.SuspendLayout();
        listdataGridOdeme.SuspendLayout();
        listdataGridOdemeToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridOdeme).BeginInit();
        tabPageOzelRaporlar.SuspendLayout();
        listsalesGrid.SuspendLayout();
        listsalesGridToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)salesGrid).BeginInit();
        _homePage.SuspendLayout();
        pnlHome.SuspendLayout();
        SuspendLayout();
        // 
        // tabControl
        // 
        tabControl.Controls.Add(tabPageStok);
        tabControl.Controls.Add(tabPagePersonelYonetimi);
        tabControl.Controls.Add(tabPageOgrenciOnKayit);
        tabControl.Controls.Add(_classesPage);
        tabControl.Controls.Add(tabPageSatis);
        tabControl.Controls.Add(tabPageGelirGider);
        tabControl.Controls.Add(tabPageOzelRaporlar);
        tabControl.Controls.Add(_homePage);
        tabControl.Dock = DockStyle.Fill;
        tabControl.Location = new Point(0, 0);
        tabControl.Name = "tabControl";
        tabControl.SelectedIndex = 0;
        tabControl.Size = new Size(1120, 760);
        tabControl.TabIndex = 1;
        // 
        // tabPageStok
        // 
        tabPageStok.BackColor = Color.FromArgb(239, 247, 253);
        tabPageStok.Controls.Add(listdataGridViewStok);
        tabPageStok.Location = new Point(4, 26);
        tabPageStok.Name = "tabPageStok";
        tabPageStok.Size = new Size(1112, 730);
        tabPageStok.TabIndex = 0;
        tabPageStok.Text = "Öğrenciler";
        tabPageStok.Click += tabPageStok_Click;
        // 
        // listdataGridViewStok
        // 
        listdataGridViewStok.BackColor = Color.FromArgb(239, 247, 253);
        listdataGridViewStok.ColumnCount = 1;
        listdataGridViewStok.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        listdataGridViewStok.Controls.Add(listdataGridViewStokToolbar, 0, 0);
        listdataGridViewStok.Controls.Add(dataGridViewStok, 0, 1);
        listdataGridViewStok.Controls.Add(listdataGridViewStokCount, 0, 2);
        listdataGridViewStok.Dock = DockStyle.Fill;
        listdataGridViewStok.Location = new Point(0, 0);
        listdataGridViewStok.Margin = new Padding(0);
        listdataGridViewStok.Name = "listdataGridViewStok";
        listdataGridViewStok.Padding = new Padding(8);
        listdataGridViewStok.RowCount = 3;
        listdataGridViewStok.RowStyles.Add(new RowStyle());
        listdataGridViewStok.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        listdataGridViewStok.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        listdataGridViewStok.Size = new Size(1112, 730);
        listdataGridViewStok.TabIndex = 0;
        // 
        // listdataGridViewStokToolbar
        // 
        listdataGridViewStokToolbar.AutoSize = true;
        listdataGridViewStokToolbar.BackColor = Color.FromArgb(218, 236, 250);
        listdataGridViewStokToolbar.ColumnCount = 4;
        listdataGridViewStokToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        listdataGridViewStokToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
        listdataGridViewStokToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        listdataGridViewStokToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        listdataGridViewStokToolbar.Controls.Add(listdataGridViewStokTitle, 0, 0);
        listdataGridViewStokToolbar.Controls.Add(txtOgrenciYonetimiAra, 1, 0);
        listdataGridViewStokToolbar.Controls.Add(listdataGridViewStokClear, 2, 0);
        listdataGridViewStokToolbar.Controls.Add(listdataGridViewStokColumns, 3, 0);
        listdataGridViewStokToolbar.Dock = DockStyle.Top;
        listdataGridViewStokToolbar.Location = new Point(8, 8);
        listdataGridViewStokToolbar.Margin = new Padding(0);
        listdataGridViewStokToolbar.Name = "listdataGridViewStokToolbar";
        listdataGridViewStokToolbar.RowCount = 1;
        listdataGridViewStokToolbar.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        listdataGridViewStokToolbar.Size = new Size(1096, 42);
        listdataGridViewStokToolbar.TabIndex = 0;
        // 
        // listdataGridViewStokTitle
        // 
        listdataGridViewStokTitle.AutoEllipsis = true;
        listdataGridViewStokTitle.Dock = DockStyle.Fill;
        listdataGridViewStokTitle.Font = new Font("Segoe UI Semibold", 11F);
        listdataGridViewStokTitle.ForeColor = Color.FromArgb(68, 87, 111);
        listdataGridViewStokTitle.Location = new Point(0, 2);
        listdataGridViewStokTitle.Margin = new Padding(0, 2, 12, 6);
        listdataGridViewStokTitle.Name = "listdataGridViewStokTitle";
        listdataGridViewStokTitle.Size = new Size(662, 34);
        listdataGridViewStokTitle.TabIndex = 0;
        listdataGridViewStokTitle.Text = "Öğrenciler";
        // 
        // txtOgrenciYonetimiAra
        // 
        txtOgrenciYonetimiAra.BorderStyle = BorderStyle.FixedSingle;
        txtOgrenciYonetimiAra.Dock = DockStyle.Fill;
        txtOgrenciYonetimiAra.Font = new Font("Segoe UI", 10F);
        txtOgrenciYonetimiAra.ForeColor = Color.FromArgb(37, 54, 75);
        txtOgrenciYonetimiAra.Location = new Point(674, 4);
        txtOgrenciYonetimiAra.Margin = new Padding(0, 4, 8, 6);
        txtOgrenciYonetimiAra.Name = "txtOgrenciYonetimiAra";
        txtOgrenciYonetimiAra.PlaceholderText = "Listede ara (Ctrl+F)";
        txtOgrenciYonetimiAra.Size = new Size(222, 25);
        txtOgrenciYonetimiAra.TabIndex = 6;
        txtOgrenciYonetimiAra.TextChanged += txtOgrenciYonetimiAra_TextChanged;
        // 
        // listdataGridViewStokClear
        // 
        listdataGridViewStokClear.AutoSize = true;
        listdataGridViewStokClear.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        listdataGridViewStokClear.BackColor = Color.FromArgb(246, 248, 251);
        listdataGridViewStokClear.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        listdataGridViewStokClear.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        listdataGridViewStokClear.FlatStyle = FlatStyle.Flat;
        listdataGridViewStokClear.ForeColor = Color.FromArgb(37, 54, 75);
        listdataGridViewStokClear.Location = new Point(904, 0);
        listdataGridViewStokClear.Margin = new Padding(0, 0, 8, 0);
        listdataGridViewStokClear.MinimumSize = new Size(96, 34);
        listdataGridViewStokClear.Name = "listdataGridViewStokClear";
        listdataGridViewStokClear.Padding = new Padding(12, 4, 12, 4);
        listdataGridViewStokClear.Size = new Size(96, 39);
        listdataGridViewStokClear.TabIndex = 7;
        listdataGridViewStokClear.Text = "Temizle";
        listdataGridViewStokClear.UseVisualStyleBackColor = false;
        // 
        // listdataGridViewStokColumns
        // 
        listdataGridViewStokColumns.AutoSize = true;
        listdataGridViewStokColumns.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        listdataGridViewStokColumns.BackColor = Color.FromArgb(246, 248, 251);
        listdataGridViewStokColumns.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        listdataGridViewStokColumns.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        listdataGridViewStokColumns.FlatStyle = FlatStyle.Flat;
        listdataGridViewStokColumns.ForeColor = Color.FromArgb(37, 54, 75);
        listdataGridViewStokColumns.Location = new Point(996, 0);
        listdataGridViewStokColumns.Margin = new Padding(0, 0, 8, 0);
        listdataGridViewStokColumns.MinimumSize = new Size(96, 34);
        listdataGridViewStokColumns.Name = "listdataGridViewStokColumns";
        listdataGridViewStokColumns.Padding = new Padding(12, 4, 12, 4);
        listdataGridViewStokColumns.Size = new Size(96, 39);
        listdataGridViewStokColumns.TabIndex = 8;
        listdataGridViewStokColumns.Text = "Sütunlar";
        listdataGridViewStokColumns.UseVisualStyleBackColor = false;
        // 
        // dataGridViewStok
        // 
        dataGridViewStok.AllowUserToAddRows = false;
        dataGridViewStok.AllowUserToDeleteRows = false;
        dataGridViewStok.AllowUserToOrderColumns = true;
        dataGridViewStok.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(239, 247, 253);
        dataGridViewStok.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        dataGridViewStok.BackgroundColor = Color.FromArgb(239, 247, 253);
        dataGridViewStok.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(218, 236, 250);
        dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 9.5F);
        dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(218, 236, 250);
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        dataGridViewStok.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        dataGridViewStok.ColumnHeadersHeight = 36;
        dataGridViewStok.ContextMenuStrip = contextMenuStrip1;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = SystemColors.Window;
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F);
        dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(200, 224, 246);
        dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(37, 54, 75);
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
        dataGridViewStok.DefaultCellStyle = dataGridViewCellStyle3;
        dataGridViewStok.Dock = DockStyle.Fill;
        dataGridViewStok.EnableHeadersVisualStyles = false;
        dataGridViewStok.Location = new Point(11, 53);
        dataGridViewStok.MultiSelect = false;
        dataGridViewStok.Name = "dataGridViewStok";
        dataGridViewStok.ReadOnly = true;
        dataGridViewStok.RowHeadersWidth = 44;
        dataGridViewStok.RowTemplate.Height = 32;
        dataGridViewStok.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridViewStok.Size = new Size(1090, 638);
        dataGridViewStok.TabIndex = 1;
        dataGridViewStok.Tag = 4010;
        dataGridViewStok.CellDoubleClick += dataGridViewStok_CellDoubleClick;
        // 
        // contextMenuStrip1
        // 
        contextMenuStrip1.Items.AddRange(new ToolStripItem[] { ödemeDetaylarıToolStripMenuItem, geçmişHareketToolStripMenuItem, excelİleAktarToolStripMenuItem, yeniKayıtEkleToolStripMenuItem, yenileToolStripMenuItem, kayıtSilToolStripMenuItem, arşivToolStripMenuItem });
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new Size(171, 158);
        contextMenuStrip1.Text = "Ödeme Detayları";
        // 
        // ödemeDetaylarıToolStripMenuItem
        // 
        ödemeDetaylarıToolStripMenuItem.Name = "ödemeDetaylarıToolStripMenuItem";
        ödemeDetaylarıToolStripMenuItem.Size = new Size(170, 22);
        ödemeDetaylarıToolStripMenuItem.Text = "Ödeme Detayları";
        ödemeDetaylarıToolStripMenuItem.Click += ödemeDetaylarıToolStripMenuItem_Click_1;
        // 
        // geçmişHareketToolStripMenuItem
        // 
        geçmişHareketToolStripMenuItem.Name = "geçmişHareketToolStripMenuItem";
        geçmişHareketToolStripMenuItem.Size = new Size(170, 22);
        geçmişHareketToolStripMenuItem.Text = "Geçmiş Hareketler";
        geçmişHareketToolStripMenuItem.Click += loglarıGörüntüleToolStripMenuItem_Click;
        // 
        // excelİleAktarToolStripMenuItem
        // 
        excelİleAktarToolStripMenuItem.Name = "excelİleAktarToolStripMenuItem";
        excelİleAktarToolStripMenuItem.Size = new Size(170, 22);
        excelİleAktarToolStripMenuItem.Text = "Excel ile Aktar";
        excelİleAktarToolStripMenuItem.Click += excelAktarToolStripMenuItem_Click;
        // 
        // yeniKayıtEkleToolStripMenuItem
        // 
        yeniKayıtEkleToolStripMenuItem.Name = "yeniKayıtEkleToolStripMenuItem";
        yeniKayıtEkleToolStripMenuItem.Size = new Size(170, 22);
        yeniKayıtEkleToolStripMenuItem.Text = "Yeni Kayıt Ekle";
        yeniKayıtEkleToolStripMenuItem.Click += yeniKayitEkle;
        // 
        // yenileToolStripMenuItem
        // 
        yenileToolStripMenuItem.Name = "yenileToolStripMenuItem";
        yenileToolStripMenuItem.Size = new Size(170, 22);
        yenileToolStripMenuItem.Text = "Yenile";
        yenileToolStripMenuItem.Click += yenileToolStripMenuItem_Click;
        // 
        // kayıtSilToolStripMenuItem
        // 
        kayıtSilToolStripMenuItem.Name = "kayıtSilToolStripMenuItem";
        kayıtSilToolStripMenuItem.Size = new Size(170, 22);
        kayıtSilToolStripMenuItem.Text = "Seçili Kayıdı Sil";
        kayıtSilToolStripMenuItem.Click += DeleteStripMenuItem_Click;
        // 
        // arşivToolStripMenuItem
        // 
        arşivToolStripMenuItem.Name = "arşivToolStripMenuItem";
        arşivToolStripMenuItem.Size = new Size(170, 22);
        arşivToolStripMenuItem.Text = "Arşiv";
        arşivToolStripMenuItem.Click += arşivToolStripMenuItem_Click;
        // 
        // listdataGridViewStokCount
        // 
        listdataGridViewStokCount.AutoSize = true;
        listdataGridViewStokCount.Dock = DockStyle.Fill;
        listdataGridViewStokCount.ForeColor = Color.FromArgb(68, 87, 111);
        listdataGridViewStokCount.Location = new Point(8, 696);
        listdataGridViewStokCount.Margin = new Padding(0, 2, 12, 6);
        listdataGridViewStokCount.Name = "listdataGridViewStokCount";
        listdataGridViewStokCount.Size = new Size(1084, 20);
        listdataGridViewStokCount.TabIndex = 2;
        listdataGridViewStokCount.Text = "Kayıt: 0";
        // 
        // tabPagePersonelYonetimi
        // 
        tabPagePersonelYonetimi.BackColor = Color.FromArgb(239, 247, 253);
        tabPagePersonelYonetimi.Controls.Add(listdgvPersonelYonetimi);
        tabPagePersonelYonetimi.Location = new Point(4, 24);
        tabPagePersonelYonetimi.Name = "tabPagePersonelYonetimi";
        tabPagePersonelYonetimi.Size = new Size(1112, 732);
        tabPagePersonelYonetimi.TabIndex = 1;
        tabPagePersonelYonetimi.Text = "Personel";
        tabPagePersonelYonetimi.Click += tabPagePersonelYonetimi_Click;
        // 
        // listdgvPersonelYonetimi
        // 
        listdgvPersonelYonetimi.BackColor = Color.FromArgb(239, 247, 253);
        listdgvPersonelYonetimi.ColumnCount = 1;
        listdgvPersonelYonetimi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        listdgvPersonelYonetimi.Controls.Add(listdgvPersonelYonetimiToolbar, 0, 0);
        listdgvPersonelYonetimi.Controls.Add(dgvPersonelYonetimi, 0, 1);
        listdgvPersonelYonetimi.Controls.Add(listdgvPersonelYonetimiCount, 0, 2);
        listdgvPersonelYonetimi.Dock = DockStyle.Fill;
        listdgvPersonelYonetimi.Location = new Point(0, 0);
        listdgvPersonelYonetimi.Margin = new Padding(0);
        listdgvPersonelYonetimi.Name = "listdgvPersonelYonetimi";
        listdgvPersonelYonetimi.Padding = new Padding(8);
        listdgvPersonelYonetimi.RowCount = 3;
        listdgvPersonelYonetimi.RowStyles.Add(new RowStyle());
        listdgvPersonelYonetimi.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        listdgvPersonelYonetimi.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        listdgvPersonelYonetimi.Size = new Size(1112, 732);
        listdgvPersonelYonetimi.TabIndex = 0;
        // 
        // listdgvPersonelYonetimiToolbar
        // 
        listdgvPersonelYonetimiToolbar.AutoSize = true;
        listdgvPersonelYonetimiToolbar.BackColor = Color.FromArgb(218, 236, 250);
        listdgvPersonelYonetimiToolbar.ColumnCount = 4;
        listdgvPersonelYonetimiToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        listdgvPersonelYonetimiToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
        listdgvPersonelYonetimiToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        listdgvPersonelYonetimiToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        listdgvPersonelYonetimiToolbar.Controls.Add(listdgvPersonelYonetimiTitle, 0, 0);
        listdgvPersonelYonetimiToolbar.Controls.Add(_personnelSearch, 1, 0);
        listdgvPersonelYonetimiToolbar.Controls.Add(listdgvPersonelYonetimiClear, 2, 0);
        listdgvPersonelYonetimiToolbar.Controls.Add(listdgvPersonelYonetimiColumns, 3, 0);
        listdgvPersonelYonetimiToolbar.Dock = DockStyle.Top;
        listdgvPersonelYonetimiToolbar.Location = new Point(8, 8);
        listdgvPersonelYonetimiToolbar.Margin = new Padding(0);
        listdgvPersonelYonetimiToolbar.Name = "listdgvPersonelYonetimiToolbar";
        listdgvPersonelYonetimiToolbar.RowCount = 1;
        listdgvPersonelYonetimiToolbar.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        listdgvPersonelYonetimiToolbar.Size = new Size(1096, 42);
        listdgvPersonelYonetimiToolbar.TabIndex = 0;
        // 
        // listdgvPersonelYonetimiTitle
        // 
        listdgvPersonelYonetimiTitle.AutoEllipsis = true;
        listdgvPersonelYonetimiTitle.Dock = DockStyle.Fill;
        listdgvPersonelYonetimiTitle.Font = new Font("Segoe UI Semibold", 11F);
        listdgvPersonelYonetimiTitle.ForeColor = Color.FromArgb(68, 87, 111);
        listdgvPersonelYonetimiTitle.Location = new Point(0, 2);
        listdgvPersonelYonetimiTitle.Margin = new Padding(0, 2, 12, 6);
        listdgvPersonelYonetimiTitle.Name = "listdgvPersonelYonetimiTitle";
        listdgvPersonelYonetimiTitle.Size = new Size(662, 34);
        listdgvPersonelYonetimiTitle.TabIndex = 0;
        listdgvPersonelYonetimiTitle.Text = "Personel";
        // 
        // _personnelSearch
        // 
        _personnelSearch.BorderStyle = BorderStyle.FixedSingle;
        _personnelSearch.Dock = DockStyle.Fill;
        _personnelSearch.Font = new Font("Segoe UI", 10F);
        _personnelSearch.ForeColor = Color.FromArgb(37, 54, 75);
        _personnelSearch.Location = new Point(674, 4);
        _personnelSearch.Margin = new Padding(0, 4, 8, 6);
        _personnelSearch.Name = "_personnelSearch";
        _personnelSearch.PlaceholderText = "Listede ara (Ctrl+F)";
        _personnelSearch.Size = new Size(222, 25);
        _personnelSearch.TabIndex = 15;
        // 
        // listdgvPersonelYonetimiClear
        // 
        listdgvPersonelYonetimiClear.AutoSize = true;
        listdgvPersonelYonetimiClear.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        listdgvPersonelYonetimiClear.BackColor = Color.FromArgb(246, 248, 251);
        listdgvPersonelYonetimiClear.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        listdgvPersonelYonetimiClear.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        listdgvPersonelYonetimiClear.FlatStyle = FlatStyle.Flat;
        listdgvPersonelYonetimiClear.ForeColor = Color.FromArgb(37, 54, 75);
        listdgvPersonelYonetimiClear.Location = new Point(904, 0);
        listdgvPersonelYonetimiClear.Margin = new Padding(0, 0, 8, 0);
        listdgvPersonelYonetimiClear.MinimumSize = new Size(96, 34);
        listdgvPersonelYonetimiClear.Name = "listdgvPersonelYonetimiClear";
        listdgvPersonelYonetimiClear.Padding = new Padding(12, 4, 12, 4);
        listdgvPersonelYonetimiClear.Size = new Size(96, 39);
        listdgvPersonelYonetimiClear.TabIndex = 16;
        listdgvPersonelYonetimiClear.Text = "Temizle";
        listdgvPersonelYonetimiClear.UseVisualStyleBackColor = false;
        // 
        // listdgvPersonelYonetimiColumns
        // 
        listdgvPersonelYonetimiColumns.AutoSize = true;
        listdgvPersonelYonetimiColumns.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        listdgvPersonelYonetimiColumns.BackColor = Color.FromArgb(246, 248, 251);
        listdgvPersonelYonetimiColumns.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        listdgvPersonelYonetimiColumns.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        listdgvPersonelYonetimiColumns.FlatStyle = FlatStyle.Flat;
        listdgvPersonelYonetimiColumns.ForeColor = Color.FromArgb(37, 54, 75);
        listdgvPersonelYonetimiColumns.Location = new Point(996, 0);
        listdgvPersonelYonetimiColumns.Margin = new Padding(0, 0, 8, 0);
        listdgvPersonelYonetimiColumns.MinimumSize = new Size(96, 34);
        listdgvPersonelYonetimiColumns.Name = "listdgvPersonelYonetimiColumns";
        listdgvPersonelYonetimiColumns.Padding = new Padding(12, 4, 12, 4);
        listdgvPersonelYonetimiColumns.Size = new Size(96, 39);
        listdgvPersonelYonetimiColumns.TabIndex = 17;
        listdgvPersonelYonetimiColumns.Text = "Sütunlar";
        listdgvPersonelYonetimiColumns.UseVisualStyleBackColor = false;
        // 
        // dgvPersonelYonetimi
        // 
        dgvPersonelYonetimi.AllowUserToAddRows = false;
        dgvPersonelYonetimi.AllowUserToDeleteRows = false;
        dgvPersonelYonetimi.AllowUserToOrderColumns = true;
        dgvPersonelYonetimi.AllowUserToResizeRows = false;
        dataGridViewCellStyle4.BackColor = Color.FromArgb(239, 247, 253);
        dgvPersonelYonetimi.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
        dgvPersonelYonetimi.BackgroundColor = Color.FromArgb(239, 247, 253);
        dgvPersonelYonetimi.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle5.BackColor = Color.FromArgb(218, 236, 250);
        dataGridViewCellStyle5.Font = new Font("Segoe UI Semibold", 9.5F);
        dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(218, 236, 250);
        dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
        dgvPersonelYonetimi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
        dgvPersonelYonetimi.ColumnHeadersHeight = 36;
        dgvPersonelYonetimi.ContextMenuStrip = contextMenuStrip1;
        dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle6.BackColor = SystemColors.Window;
        dataGridViewCellStyle6.Font = new Font("Segoe UI", 9.5F);
        dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(200, 224, 246);
        dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(37, 54, 75);
        dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
        dgvPersonelYonetimi.DefaultCellStyle = dataGridViewCellStyle6;
        dgvPersonelYonetimi.Dock = DockStyle.Fill;
        dgvPersonelYonetimi.EnableHeadersVisualStyles = false;
        dgvPersonelYonetimi.Location = new Point(11, 53);
        dgvPersonelYonetimi.MultiSelect = false;
        dgvPersonelYonetimi.Name = "dgvPersonelYonetimi";
        dgvPersonelYonetimi.ReadOnly = true;
        dgvPersonelYonetimi.RowHeadersWidth = 44;
        dgvPersonelYonetimi.RowTemplate.Height = 32;
        dgvPersonelYonetimi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPersonelYonetimi.Size = new Size(1090, 640);
        dgvPersonelYonetimi.TabIndex = 1;
        dgvPersonelYonetimi.Tag = 4020;
        dgvPersonelYonetimi.CellClick += dataGridViewStok_CellContentClick;
        dgvPersonelYonetimi.CellDoubleClick += dataGridViewPersonel_CellDoubleClick;
        // 
        // listdgvPersonelYonetimiCount
        // 
        listdgvPersonelYonetimiCount.AutoSize = true;
        listdgvPersonelYonetimiCount.Dock = DockStyle.Fill;
        listdgvPersonelYonetimiCount.ForeColor = Color.FromArgb(68, 87, 111);
        listdgvPersonelYonetimiCount.Location = new Point(8, 698);
        listdgvPersonelYonetimiCount.Margin = new Padding(0, 2, 12, 6);
        listdgvPersonelYonetimiCount.Name = "listdgvPersonelYonetimiCount";
        listdgvPersonelYonetimiCount.Size = new Size(1084, 20);
        listdgvPersonelYonetimiCount.TabIndex = 2;
        listdgvPersonelYonetimiCount.Text = "Kayıt: 0";
        // 
        // tabPageOgrenciOnKayit
        // 
        tabPageOgrenciOnKayit.BackColor = Color.FromArgb(239, 247, 253);
        tabPageOgrenciOnKayit.Controls.Add(bodydgvOnKayitlar);
        tabPageOgrenciOnKayit.Location = new Point(4, 24);
        tabPageOgrenciOnKayit.Name = "tabPageOgrenciOnKayit";
        tabPageOgrenciOnKayit.Size = new Size(1112, 732);
        tabPageOgrenciOnKayit.TabIndex = 2;
        tabPageOgrenciOnKayit.Text = "Ön Kayıt";
        // 
        // bodydgvOnKayitlar
        // 
        bodydgvOnKayitlar.BackColor = Color.FromArgb(239, 247, 253);
        bodydgvOnKayitlar.ColumnCount = 1;
        bodydgvOnKayitlar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        bodydgvOnKayitlar.Controls.Add(editordgvOnKayitlar, 0, 0);
        bodydgvOnKayitlar.Controls.Add(listdgvOnKayitlar, 0, 1);
        bodydgvOnKayitlar.Dock = DockStyle.Fill;
        bodydgvOnKayitlar.Location = new Point(0, 0);
        bodydgvOnKayitlar.Margin = new Padding(0);
        bodydgvOnKayitlar.Name = "bodydgvOnKayitlar";
        bodydgvOnKayitlar.RowCount = 2;
        bodydgvOnKayitlar.RowStyles.Add(new RowStyle(SizeType.Absolute, 238F));
        bodydgvOnKayitlar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        bodydgvOnKayitlar.Size = new Size(1112, 732);
        bodydgvOnKayitlar.TabIndex = 0;
        // 
        // editordgvOnKayitlar
        // 
        editordgvOnKayitlar.BackColor = Color.FromArgb(239, 247, 253);
        editordgvOnKayitlar.ColumnCount = 1;
        editordgvOnKayitlar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        editordgvOnKayitlar.Controls.Add(fieldsdgvOnKayitlar, 0, 0);
        editordgvOnKayitlar.Controls.Add(actionsdgvOnKayitlar, 0, 1);
        editordgvOnKayitlar.Dock = DockStyle.Fill;
        editordgvOnKayitlar.Location = new Point(0, 0);
        editordgvOnKayitlar.Margin = new Padding(0);
        editordgvOnKayitlar.Name = "editordgvOnKayitlar";
        editordgvOnKayitlar.RowCount = 2;
        editordgvOnKayitlar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        editordgvOnKayitlar.RowStyles.Add(new RowStyle());
        editordgvOnKayitlar.Size = new Size(1112, 238);
        editordgvOnKayitlar.TabIndex = 0;
        // 
        // fieldsdgvOnKayitlar
        // 
        fieldsdgvOnKayitlar.AutoScroll = true;
        fieldsdgvOnKayitlar.BackColor = Color.FromArgb(239, 247, 253);
        fieldsdgvOnKayitlar.ColumnCount = 3;
        fieldsdgvOnKayitlar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        fieldsdgvOnKayitlar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        fieldsdgvOnKayitlar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        fieldsdgvOnKayitlar.Controls.Add(lbl_txtOnKayitAd, 0, 0);
        fieldsdgvOnKayitlar.Controls.Add(txtOnKayitAd, 0, 1);
        fieldsdgvOnKayitlar.Controls.Add(lbl_txtOnKayitSoyad, 1, 0);
        fieldsdgvOnKayitlar.Controls.Add(txtOnKayitSoyad, 1, 1);
        fieldsdgvOnKayitlar.Controls.Add(lbl_dtpOnKayitDogumTarihi, 2, 0);
        fieldsdgvOnKayitlar.Controls.Add(dtpOnKayitDogumTarihi, 2, 1);
        fieldsdgvOnKayitlar.Controls.Add(lbl_txtOnKayitBabaAd, 0, 2);
        fieldsdgvOnKayitlar.Controls.Add(txtOnKayitBabaAd, 0, 3);
        fieldsdgvOnKayitlar.Controls.Add(lbl_txtOnKayitVeliTel, 1, 2);
        fieldsdgvOnKayitlar.Controls.Add(txtOnKayitVeliTel, 1, 3);
        fieldsdgvOnKayitlar.Controls.Add(lbl_txtOnKayitNot, 2, 2);
        fieldsdgvOnKayitlar.Controls.Add(txtOnKayitNot, 2, 3);
        fieldsdgvOnKayitlar.Dock = DockStyle.Fill;
        fieldsdgvOnKayitlar.Location = new Point(0, 0);
        fieldsdgvOnKayitlar.Margin = new Padding(0);
        fieldsdgvOnKayitlar.Name = "fieldsdgvOnKayitlar";
        fieldsdgvOnKayitlar.Padding = new Padding(16);
        fieldsdgvOnKayitlar.RowCount = 5;
        fieldsdgvOnKayitlar.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
        fieldsdgvOnKayitlar.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        fieldsdgvOnKayitlar.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
        fieldsdgvOnKayitlar.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        fieldsdgvOnKayitlar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        fieldsdgvOnKayitlar.Size = new Size(1112, 183);
        fieldsdgvOnKayitlar.TabIndex = 0;
        // 
        // lbl_txtOnKayitAd
        // 
        lbl_txtOnKayitAd.AutoSize = true;
        lbl_txtOnKayitAd.ForeColor = Color.FromArgb(68, 87, 111);
        lbl_txtOnKayitAd.Location = new Point(16, 18);
        lbl_txtOnKayitAd.Margin = new Padding(0, 2, 12, 6);
        lbl_txtOnKayitAd.Name = "lbl_txtOnKayitAd";
        lbl_txtOnKayitAd.Size = new Size(79, 18);
        lbl_txtOnKayitAd.TabIndex = 0;
        lbl_txtOnKayitAd.Text = "Öğrenci adı";
        // 
        // txtOnKayitAd
        // 
        txtOnKayitAd.BorderStyle = BorderStyle.FixedSingle;
        txtOnKayitAd.Dock = DockStyle.Fill;
        txtOnKayitAd.Font = new Font("Segoe UI", 10F);
        txtOnKayitAd.ForeColor = Color.FromArgb(37, 54, 75);
        txtOnKayitAd.Location = new Point(16, 42);
        txtOnKayitAd.Margin = new Padding(0, 0, 16, 10);
        txtOnKayitAd.Name = "txtOnKayitAd";
        txtOnKayitAd.PlaceholderText = "Öğrenci Adı";
        txtOnKayitAd.Size = new Size(344, 25);
        txtOnKayitAd.TabIndex = 29;
        // 
        // lbl_txtOnKayitSoyad
        // 
        lbl_txtOnKayitSoyad.AutoSize = true;
        lbl_txtOnKayitSoyad.ForeColor = Color.FromArgb(68, 87, 111);
        lbl_txtOnKayitSoyad.Location = new Point(376, 18);
        lbl_txtOnKayitSoyad.Margin = new Padding(0, 2, 12, 6);
        lbl_txtOnKayitSoyad.Name = "lbl_txtOnKayitSoyad";
        lbl_txtOnKayitSoyad.Size = new Size(49, 18);
        lbl_txtOnKayitSoyad.TabIndex = 30;
        lbl_txtOnKayitSoyad.Text = "Soyadı";
        // 
        // txtOnKayitSoyad
        // 
        txtOnKayitSoyad.BorderStyle = BorderStyle.FixedSingle;
        txtOnKayitSoyad.Dock = DockStyle.Fill;
        txtOnKayitSoyad.Font = new Font("Segoe UI", 10F);
        txtOnKayitSoyad.ForeColor = Color.FromArgb(37, 54, 75);
        txtOnKayitSoyad.Location = new Point(376, 42);
        txtOnKayitSoyad.Margin = new Padding(0, 0, 16, 10);
        txtOnKayitSoyad.Name = "txtOnKayitSoyad";
        txtOnKayitSoyad.PlaceholderText = "Öğrenci Soyadı";
        txtOnKayitSoyad.Size = new Size(344, 25);
        txtOnKayitSoyad.TabIndex = 31;
        // 
        // lbl_dtpOnKayitDogumTarihi
        // 
        lbl_dtpOnKayitDogumTarihi.AutoSize = true;
        lbl_dtpOnKayitDogumTarihi.ForeColor = Color.FromArgb(68, 87, 111);
        lbl_dtpOnKayitDogumTarihi.Location = new Point(736, 18);
        lbl_dtpOnKayitDogumTarihi.Margin = new Padding(0, 2, 12, 6);
        lbl_dtpOnKayitDogumTarihi.Name = "lbl_dtpOnKayitDogumTarihi";
        lbl_dtpOnKayitDogumTarihi.Size = new Size(90, 18);
        lbl_dtpOnKayitDogumTarihi.TabIndex = 32;
        lbl_dtpOnKayitDogumTarihi.Text = "Doğum tarihi";
        // 
        // dtpOnKayitDogumTarihi
        // 
        dtpOnKayitDogumTarihi.CustomFormat = "dd.MM.yyyy";
        dtpOnKayitDogumTarihi.Dock = DockStyle.Fill;
        dtpOnKayitDogumTarihi.Font = new Font("Segoe UI", 10F);
        dtpOnKayitDogumTarihi.ForeColor = Color.FromArgb(37, 54, 75);
        dtpOnKayitDogumTarihi.Format = DateTimePickerFormat.Custom;
        dtpOnKayitDogumTarihi.Location = new Point(736, 42);
        dtpOnKayitDogumTarihi.Margin = new Padding(0, 0, 16, 10);
        dtpOnKayitDogumTarihi.Name = "dtpOnKayitDogumTarihi";
        dtpOnKayitDogumTarihi.Size = new Size(344, 25);
        dtpOnKayitDogumTarihi.TabIndex = 33;
        // 
        // lbl_txtOnKayitBabaAd
        // 
        lbl_txtOnKayitBabaAd.AutoSize = true;
        lbl_txtOnKayitBabaAd.ForeColor = Color.FromArgb(68, 87, 111);
        lbl_txtOnKayitBabaAd.Location = new Point(16, 88);
        lbl_txtOnKayitBabaAd.Margin = new Padding(0, 2, 12, 6);
        lbl_txtOnKayitBabaAd.Name = "lbl_txtOnKayitBabaAd";
        lbl_txtOnKayitBabaAd.Size = new Size(61, 18);
        lbl_txtOnKayitBabaAd.TabIndex = 34;
        lbl_txtOnKayitBabaAd.Text = "Baba adı";
        // 
        // txtOnKayitBabaAd
        // 
        txtOnKayitBabaAd.BorderStyle = BorderStyle.FixedSingle;
        txtOnKayitBabaAd.Dock = DockStyle.Fill;
        txtOnKayitBabaAd.Font = new Font("Segoe UI", 10F);
        txtOnKayitBabaAd.ForeColor = Color.FromArgb(37, 54, 75);
        txtOnKayitBabaAd.Location = new Point(16, 112);
        txtOnKayitBabaAd.Margin = new Padding(0, 0, 16, 10);
        txtOnKayitBabaAd.Name = "txtOnKayitBabaAd";
        txtOnKayitBabaAd.PlaceholderText = "Baba Adı Soyadı";
        txtOnKayitBabaAd.Size = new Size(344, 25);
        txtOnKayitBabaAd.TabIndex = 35;
        // 
        // lbl_txtOnKayitVeliTel
        // 
        lbl_txtOnKayitVeliTel.AutoSize = true;
        lbl_txtOnKayitVeliTel.ForeColor = Color.FromArgb(68, 87, 111);
        lbl_txtOnKayitVeliTel.Location = new Point(376, 88);
        lbl_txtOnKayitVeliTel.Margin = new Padding(0, 2, 12, 6);
        lbl_txtOnKayitVeliTel.Name = "lbl_txtOnKayitVeliTel";
        lbl_txtOnKayitVeliTel.Size = new Size(84, 18);
        lbl_txtOnKayitVeliTel.TabIndex = 36;
        lbl_txtOnKayitVeliTel.Text = "Veli telefonu";
        // 
        // txtOnKayitVeliTel
        // 
        txtOnKayitVeliTel.BorderStyle = BorderStyle.FixedSingle;
        txtOnKayitVeliTel.Dock = DockStyle.Fill;
        txtOnKayitVeliTel.Font = new Font("Segoe UI", 10F);
        txtOnKayitVeliTel.ForeColor = Color.FromArgb(37, 54, 75);
        txtOnKayitVeliTel.Location = new Point(376, 112);
        txtOnKayitVeliTel.Margin = new Padding(0, 0, 16, 10);
        txtOnKayitVeliTel.Name = "txtOnKayitVeliTel";
        txtOnKayitVeliTel.PlaceholderText = "Baba Telefon";
        txtOnKayitVeliTel.Size = new Size(344, 25);
        txtOnKayitVeliTel.TabIndex = 37;
        // 
        // lbl_txtOnKayitNot
        // 
        lbl_txtOnKayitNot.AutoSize = true;
        lbl_txtOnKayitNot.ForeColor = Color.FromArgb(68, 87, 111);
        lbl_txtOnKayitNot.Location = new Point(736, 88);
        lbl_txtOnKayitNot.Margin = new Padding(0, 2, 12, 6);
        lbl_txtOnKayitNot.Name = "lbl_txtOnKayitNot";
        lbl_txtOnKayitNot.Size = new Size(47, 18);
        lbl_txtOnKayitNot.TabIndex = 38;
        lbl_txtOnKayitNot.Text = "Notlar";
        // 
        // txtOnKayitNot
        // 
        txtOnKayitNot.BorderStyle = BorderStyle.FixedSingle;
        txtOnKayitNot.Dock = DockStyle.Fill;
        txtOnKayitNot.Font = new Font("Segoe UI", 10F);
        txtOnKayitNot.ForeColor = Color.FromArgb(37, 54, 75);
        txtOnKayitNot.Location = new Point(736, 112);
        txtOnKayitNot.Margin = new Padding(0, 0, 16, 10);
        txtOnKayitNot.Multiline = true;
        txtOnKayitNot.Name = "txtOnKayitNot";
        txtOnKayitNot.PlaceholderText = "Notlar (Opsiyonel)";
        txtOnKayitNot.Size = new Size(344, 34);
        txtOnKayitNot.TabIndex = 39;
        // 
        // actionsdgvOnKayitlar
        // 
        actionsdgvOnKayitlar.AutoSize = true;
        actionsdgvOnKayitlar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        actionsdgvOnKayitlar.BackColor = Color.FromArgb(239, 247, 253);
        actionsdgvOnKayitlar.Controls.Add(btnOnKayitEkle);
        actionsdgvOnKayitlar.Controls.Add(btnKesinKayitYap);
        actionsdgvOnKayitlar.Controls.Add(btnOnKayitSil);
        actionsdgvOnKayitlar.Dock = DockStyle.Top;
        actionsdgvOnKayitlar.Location = new Point(0, 183);
        actionsdgvOnKayitlar.Margin = new Padding(0);
        actionsdgvOnKayitlar.Name = "actionsdgvOnKayitlar";
        actionsdgvOnKayitlar.Padding = new Padding(16, 8, 8, 8);
        actionsdgvOnKayitlar.Size = new Size(1112, 55);
        actionsdgvOnKayitlar.TabIndex = 1;
        // 
        // btnOnKayitEkle
        // 
        btnOnKayitEkle.AutoSize = true;
        btnOnKayitEkle.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        btnOnKayitEkle.BackColor = Color.FromArgb(246, 248, 251);
        btnOnKayitEkle.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        btnOnKayitEkle.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        btnOnKayitEkle.FlatStyle = FlatStyle.Flat;
        btnOnKayitEkle.ForeColor = Color.FromArgb(37, 54, 75);
        btnOnKayitEkle.Location = new Point(16, 8);
        btnOnKayitEkle.Margin = new Padding(0, 0, 8, 0);
        btnOnKayitEkle.MinimumSize = new Size(96, 34);
        btnOnKayitEkle.Name = "btnOnKayitEkle";
        btnOnKayitEkle.Padding = new Padding(12, 4, 12, 4);
        btnOnKayitEkle.Size = new Size(96, 39);
        btnOnKayitEkle.TabIndex = 0;
        btnOnKayitEkle.Text = "Kaydet";
        btnOnKayitEkle.UseVisualStyleBackColor = false;
        btnOnKayitEkle.Click += btnOnKayitEkle_Click;
        // 
        // btnKesinKayitYap
        // 
        btnKesinKayitYap.AutoSize = true;
        btnKesinKayitYap.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        btnKesinKayitYap.BackColor = Color.FromArgb(246, 248, 251);
        btnKesinKayitYap.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        btnKesinKayitYap.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        btnKesinKayitYap.FlatStyle = FlatStyle.Flat;
        btnKesinKayitYap.ForeColor = Color.FromArgb(37, 54, 75);
        btnKesinKayitYap.Location = new Point(120, 8);
        btnKesinKayitYap.Margin = new Padding(0, 0, 8, 0);
        btnKesinKayitYap.MinimumSize = new Size(96, 34);
        btnKesinKayitYap.Name = "btnKesinKayitYap";
        btnKesinKayitYap.Padding = new Padding(12, 4, 12, 4);
        btnKesinKayitYap.Size = new Size(149, 39);
        btnKesinKayitYap.TabIndex = 1;
        btnKesinKayitYap.Text = "Kesin kayda çevir";
        btnKesinKayitYap.UseVisualStyleBackColor = false;
        btnKesinKayitYap.Click += btnKesinKayitYap_Click_1;
        // 
        // btnOnKayitSil
        // 
        btnOnKayitSil.AutoSize = true;
        btnOnKayitSil.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        btnOnKayitSil.BackColor = Color.FromArgb(246, 248, 251);
        btnOnKayitSil.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        btnOnKayitSil.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        btnOnKayitSil.FlatStyle = FlatStyle.Flat;
        btnOnKayitSil.ForeColor = Color.FromArgb(37, 54, 75);
        btnOnKayitSil.Location = new Point(277, 8);
        btnOnKayitSil.Margin = new Padding(0, 0, 8, 0);
        btnOnKayitSil.MinimumSize = new Size(96, 34);
        btnOnKayitSil.Name = "btnOnKayitSil";
        btnOnKayitSil.Padding = new Padding(12, 4, 12, 4);
        btnOnKayitSil.Size = new Size(96, 39);
        btnOnKayitSil.TabIndex = 2;
        btnOnKayitSil.Text = "Pasife al";
        btnOnKayitSil.UseVisualStyleBackColor = false;
        btnOnKayitSil.Click += btnOnKayitSil_Click;
        // 
        // listdgvOnKayitlar
        // 
        listdgvOnKayitlar.BackColor = Color.FromArgb(239, 247, 253);
        listdgvOnKayitlar.ColumnCount = 1;
        listdgvOnKayitlar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        listdgvOnKayitlar.Controls.Add(listdgvOnKayitlarToolbar, 0, 0);
        listdgvOnKayitlar.Controls.Add(dgvOnKayitlar, 0, 1);
        listdgvOnKayitlar.Controls.Add(listdgvOnKayitlarCount, 0, 2);
        listdgvOnKayitlar.Dock = DockStyle.Fill;
        listdgvOnKayitlar.Location = new Point(0, 238);
        listdgvOnKayitlar.Margin = new Padding(0);
        listdgvOnKayitlar.Name = "listdgvOnKayitlar";
        listdgvOnKayitlar.Padding = new Padding(8);
        listdgvOnKayitlar.RowCount = 3;
        listdgvOnKayitlar.RowStyles.Add(new RowStyle());
        listdgvOnKayitlar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        listdgvOnKayitlar.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        listdgvOnKayitlar.Size = new Size(1112, 494);
        listdgvOnKayitlar.TabIndex = 1;
        // 
        // listdgvOnKayitlarToolbar
        // 
        listdgvOnKayitlarToolbar.AutoSize = true;
        listdgvOnKayitlarToolbar.BackColor = Color.FromArgb(218, 236, 250);
        listdgvOnKayitlarToolbar.ColumnCount = 4;
        listdgvOnKayitlarToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        listdgvOnKayitlarToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
        listdgvOnKayitlarToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        listdgvOnKayitlarToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        listdgvOnKayitlarToolbar.Controls.Add(listdgvOnKayitlarTitle, 0, 0);
        listdgvOnKayitlarToolbar.Controls.Add(txtPreRegistrationSearch, 1, 0);
        listdgvOnKayitlarToolbar.Controls.Add(listdgvOnKayitlarClear, 2, 0);
        listdgvOnKayitlarToolbar.Controls.Add(listdgvOnKayitlarColumns, 3, 0);
        listdgvOnKayitlarToolbar.Dock = DockStyle.Top;
        listdgvOnKayitlarToolbar.Location = new Point(8, 8);
        listdgvOnKayitlarToolbar.Margin = new Padding(0);
        listdgvOnKayitlarToolbar.Name = "listdgvOnKayitlarToolbar";
        listdgvOnKayitlarToolbar.RowCount = 1;
        listdgvOnKayitlarToolbar.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        listdgvOnKayitlarToolbar.Size = new Size(1096, 42);
        listdgvOnKayitlarToolbar.TabIndex = 0;
        // 
        // listdgvOnKayitlarTitle
        // 
        listdgvOnKayitlarTitle.AutoEllipsis = true;
        listdgvOnKayitlarTitle.Dock = DockStyle.Fill;
        listdgvOnKayitlarTitle.Font = new Font("Segoe UI Semibold", 11F);
        listdgvOnKayitlarTitle.ForeColor = Color.FromArgb(68, 87, 111);
        listdgvOnKayitlarTitle.Location = new Point(0, 2);
        listdgvOnKayitlarTitle.Margin = new Padding(0, 2, 12, 6);
        listdgvOnKayitlarTitle.Name = "listdgvOnKayitlarTitle";
        listdgvOnKayitlarTitle.Size = new Size(662, 34);
        listdgvOnKayitlarTitle.TabIndex = 0;
        listdgvOnKayitlarTitle.Text = "Ön Kayıt";
        // 
        // txtPreRegistrationSearch
        // 
        txtPreRegistrationSearch.BorderStyle = BorderStyle.FixedSingle;
        txtPreRegistrationSearch.Dock = DockStyle.Fill;
        txtPreRegistrationSearch.Font = new Font("Segoe UI", 10F);
        txtPreRegistrationSearch.ForeColor = Color.FromArgb(37, 54, 75);
        txtPreRegistrationSearch.Location = new Point(674, 4);
        txtPreRegistrationSearch.Margin = new Padding(0, 4, 8, 6);
        txtPreRegistrationSearch.Name = "txtPreRegistrationSearch";
        txtPreRegistrationSearch.PlaceholderText = "Listede ara (Ctrl+F)";
        txtPreRegistrationSearch.Size = new Size(222, 25);
        txtPreRegistrationSearch.TabIndex = 24;
        // 
        // listdgvOnKayitlarClear
        // 
        listdgvOnKayitlarClear.AutoSize = true;
        listdgvOnKayitlarClear.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        listdgvOnKayitlarClear.BackColor = Color.FromArgb(246, 248, 251);
        listdgvOnKayitlarClear.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        listdgvOnKayitlarClear.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        listdgvOnKayitlarClear.FlatStyle = FlatStyle.Flat;
        listdgvOnKayitlarClear.ForeColor = Color.FromArgb(37, 54, 75);
        listdgvOnKayitlarClear.Location = new Point(904, 0);
        listdgvOnKayitlarClear.Margin = new Padding(0, 0, 8, 0);
        listdgvOnKayitlarClear.MinimumSize = new Size(96, 34);
        listdgvOnKayitlarClear.Name = "listdgvOnKayitlarClear";
        listdgvOnKayitlarClear.Padding = new Padding(12, 4, 12, 4);
        listdgvOnKayitlarClear.Size = new Size(96, 39);
        listdgvOnKayitlarClear.TabIndex = 25;
        listdgvOnKayitlarClear.Text = "Temizle";
        listdgvOnKayitlarClear.UseVisualStyleBackColor = false;
        // 
        // listdgvOnKayitlarColumns
        // 
        listdgvOnKayitlarColumns.AutoSize = true;
        listdgvOnKayitlarColumns.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        listdgvOnKayitlarColumns.BackColor = Color.FromArgb(246, 248, 251);
        listdgvOnKayitlarColumns.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        listdgvOnKayitlarColumns.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        listdgvOnKayitlarColumns.FlatStyle = FlatStyle.Flat;
        listdgvOnKayitlarColumns.ForeColor = Color.FromArgb(37, 54, 75);
        listdgvOnKayitlarColumns.Location = new Point(996, 0);
        listdgvOnKayitlarColumns.Margin = new Padding(0, 0, 8, 0);
        listdgvOnKayitlarColumns.MinimumSize = new Size(96, 34);
        listdgvOnKayitlarColumns.Name = "listdgvOnKayitlarColumns";
        listdgvOnKayitlarColumns.Padding = new Padding(12, 4, 12, 4);
        listdgvOnKayitlarColumns.Size = new Size(96, 39);
        listdgvOnKayitlarColumns.TabIndex = 26;
        listdgvOnKayitlarColumns.Text = "Sütunlar";
        listdgvOnKayitlarColumns.UseVisualStyleBackColor = false;
        // 
        // dgvOnKayitlar
        // 
        dgvOnKayitlar.AllowUserToAddRows = false;
        dgvOnKayitlar.AllowUserToDeleteRows = false;
        dgvOnKayitlar.AllowUserToOrderColumns = true;
        dgvOnKayitlar.AllowUserToResizeRows = false;
        dataGridViewCellStyle7.BackColor = Color.FromArgb(239, 247, 253);
        dgvOnKayitlar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
        dgvOnKayitlar.BackgroundColor = Color.FromArgb(239, 247, 253);
        dgvOnKayitlar.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle8.BackColor = Color.FromArgb(218, 236, 250);
        dataGridViewCellStyle8.Font = new Font("Segoe UI Semibold", 9.5F);
        dataGridViewCellStyle8.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(218, 236, 250);
        dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
        dgvOnKayitlar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
        dgvOnKayitlar.ColumnHeadersHeight = 36;
        dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle9.BackColor = SystemColors.Window;
        dataGridViewCellStyle9.Font = new Font("Segoe UI", 9.5F);
        dataGridViewCellStyle9.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(200, 224, 246);
        dataGridViewCellStyle9.SelectionForeColor = Color.FromArgb(37, 54, 75);
        dataGridViewCellStyle9.WrapMode = DataGridViewTriState.False;
        dgvOnKayitlar.DefaultCellStyle = dataGridViewCellStyle9;
        dgvOnKayitlar.Dock = DockStyle.Fill;
        dgvOnKayitlar.EnableHeadersVisualStyles = false;
        dgvOnKayitlar.Location = new Point(11, 53);
        dgvOnKayitlar.MultiSelect = false;
        dgvOnKayitlar.Name = "dgvOnKayitlar";
        dgvOnKayitlar.ReadOnly = true;
        dgvOnKayitlar.RowHeadersWidth = 44;
        dgvOnKayitlar.RowTemplate.Height = 32;
        dgvOnKayitlar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvOnKayitlar.Size = new Size(1090, 402);
        dgvOnKayitlar.TabIndex = 1;
        dgvOnKayitlar.Tag = 5001;
        // 
        // listdgvOnKayitlarCount
        // 
        listdgvOnKayitlarCount.AutoSize = true;
        listdgvOnKayitlarCount.Dock = DockStyle.Fill;
        listdgvOnKayitlarCount.ForeColor = Color.FromArgb(68, 87, 111);
        listdgvOnKayitlarCount.Location = new Point(8, 460);
        listdgvOnKayitlarCount.Margin = new Padding(0, 2, 12, 6);
        listdgvOnKayitlarCount.Name = "listdgvOnKayitlarCount";
        listdgvOnKayitlarCount.Size = new Size(1084, 20);
        listdgvOnKayitlarCount.TabIndex = 2;
        listdgvOnKayitlarCount.Text = "Kayıt: 0";
        // 
        // _classesPage
        // 
        _classesPage.BackColor = Color.FromArgb(239, 247, 253);
        _classesPage.Controls.Add(bodyDgvOgrenciYonetimiSiniflar);
        _classesPage.Location = new Point(4, 24);
        _classesPage.Name = "_classesPage";
        _classesPage.Size = new Size(1112, 732);
        _classesPage.TabIndex = 3;
        _classesPage.Text = "Sınıflar";
        // 
        // bodyDgvOgrenciYonetimiSiniflar
        // 
        bodyDgvOgrenciYonetimiSiniflar.BackColor = Color.FromArgb(239, 247, 253);
        bodyDgvOgrenciYonetimiSiniflar.ColumnCount = 1;
        bodyDgvOgrenciYonetimiSiniflar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        bodyDgvOgrenciYonetimiSiniflar.Controls.Add(editorDgvOgrenciYonetimiSiniflar, 0, 0);
        bodyDgvOgrenciYonetimiSiniflar.Controls.Add(listDgvOgrenciYonetimiSiniflar, 0, 1);
        bodyDgvOgrenciYonetimiSiniflar.Dock = DockStyle.Fill;
        bodyDgvOgrenciYonetimiSiniflar.Location = new Point(0, 0);
        bodyDgvOgrenciYonetimiSiniflar.Margin = new Padding(0);
        bodyDgvOgrenciYonetimiSiniflar.Name = "bodyDgvOgrenciYonetimiSiniflar";
        bodyDgvOgrenciYonetimiSiniflar.RowCount = 2;
        bodyDgvOgrenciYonetimiSiniflar.RowStyles.Add(new RowStyle(SizeType.Absolute, 154F));
        bodyDgvOgrenciYonetimiSiniflar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        bodyDgvOgrenciYonetimiSiniflar.Size = new Size(1112, 732);
        bodyDgvOgrenciYonetimiSiniflar.TabIndex = 0;
        // 
        // editorDgvOgrenciYonetimiSiniflar
        // 
        editorDgvOgrenciYonetimiSiniflar.BackColor = Color.FromArgb(239, 247, 253);
        editorDgvOgrenciYonetimiSiniflar.ColumnCount = 1;
        editorDgvOgrenciYonetimiSiniflar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        editorDgvOgrenciYonetimiSiniflar.Controls.Add(fieldsDgvOgrenciYonetimiSiniflar, 0, 0);
        editorDgvOgrenciYonetimiSiniflar.Controls.Add(actionsDgvOgrenciYonetimiSiniflar, 0, 1);
        editorDgvOgrenciYonetimiSiniflar.Dock = DockStyle.Fill;
        editorDgvOgrenciYonetimiSiniflar.Location = new Point(0, 0);
        editorDgvOgrenciYonetimiSiniflar.Margin = new Padding(0);
        editorDgvOgrenciYonetimiSiniflar.Name = "editorDgvOgrenciYonetimiSiniflar";
        editorDgvOgrenciYonetimiSiniflar.RowCount = 2;
        editorDgvOgrenciYonetimiSiniflar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        editorDgvOgrenciYonetimiSiniflar.RowStyles.Add(new RowStyle());
        editorDgvOgrenciYonetimiSiniflar.Size = new Size(1112, 154);
        editorDgvOgrenciYonetimiSiniflar.TabIndex = 0;
        // 
        // fieldsDgvOgrenciYonetimiSiniflar
        // 
        fieldsDgvOgrenciYonetimiSiniflar.AutoScroll = true;
        fieldsDgvOgrenciYonetimiSiniflar.BackColor = Color.FromArgb(239, 247, 253);
        fieldsDgvOgrenciYonetimiSiniflar.ColumnCount = 3;
        fieldsDgvOgrenciYonetimiSiniflar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        fieldsDgvOgrenciYonetimiSiniflar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        fieldsDgvOgrenciYonetimiSiniflar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        fieldsDgvOgrenciYonetimiSiniflar.Controls.Add(lbl_txtOgrenciYonetimiSınıfAdı, 0, 0);
        fieldsDgvOgrenciYonetimiSiniflar.Controls.Add(txtOgrenciYonetimiSınıfAdı, 0, 1);
        fieldsDgvOgrenciYonetimiSiniflar.Controls.Add(lbl_cbxOgrenciYonetimiYasGrubu, 1, 0);
        fieldsDgvOgrenciYonetimiSiniflar.Controls.Add(cbxOgrenciYonetimiYasGrubu, 1, 1);
        fieldsDgvOgrenciYonetimiSiniflar.Controls.Add(lbl_cbxOgrenciYonetimiOgretmen, 2, 0);
        fieldsDgvOgrenciYonetimiSiniflar.Controls.Add(cbxOgrenciYonetimiOgretmen, 2, 1);
        fieldsDgvOgrenciYonetimiSiniflar.Dock = DockStyle.Fill;
        fieldsDgvOgrenciYonetimiSiniflar.Location = new Point(0, 0);
        fieldsDgvOgrenciYonetimiSiniflar.Margin = new Padding(0);
        fieldsDgvOgrenciYonetimiSiniflar.Name = "fieldsDgvOgrenciYonetimiSiniflar";
        fieldsDgvOgrenciYonetimiSiniflar.Padding = new Padding(16);
        fieldsDgvOgrenciYonetimiSiniflar.RowCount = 3;
        fieldsDgvOgrenciYonetimiSiniflar.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
        fieldsDgvOgrenciYonetimiSiniflar.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        fieldsDgvOgrenciYonetimiSiniflar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        fieldsDgvOgrenciYonetimiSiniflar.Size = new Size(1112, 99);
        fieldsDgvOgrenciYonetimiSiniflar.TabIndex = 0;
        // 
        // lbl_txtOgrenciYonetimiSınıfAdı
        // 
        lbl_txtOgrenciYonetimiSınıfAdı.AutoSize = true;
        lbl_txtOgrenciYonetimiSınıfAdı.ForeColor = Color.FromArgb(68, 87, 111);
        lbl_txtOgrenciYonetimiSınıfAdı.Location = new Point(16, 18);
        lbl_txtOgrenciYonetimiSınıfAdı.Margin = new Padding(0, 2, 12, 6);
        lbl_txtOgrenciYonetimiSınıfAdı.Name = "lbl_txtOgrenciYonetimiSınıfAdı";
        lbl_txtOgrenciYonetimiSınıfAdı.Size = new Size(56, 18);
        lbl_txtOgrenciYonetimiSınıfAdı.TabIndex = 0;
        lbl_txtOgrenciYonetimiSınıfAdı.Text = "Sınıf adı";
        // 
        // txtOgrenciYonetimiSınıfAdı
        // 
        txtOgrenciYonetimiSınıfAdı.BorderStyle = BorderStyle.FixedSingle;
        txtOgrenciYonetimiSınıfAdı.Dock = DockStyle.Fill;
        txtOgrenciYonetimiSınıfAdı.Font = new Font("Segoe UI", 10F);
        txtOgrenciYonetimiSınıfAdı.ForeColor = Color.FromArgb(37, 54, 75);
        txtOgrenciYonetimiSınıfAdı.Location = new Point(16, 42);
        txtOgrenciYonetimiSınıfAdı.Margin = new Padding(0, 0, 16, 10);
        txtOgrenciYonetimiSınıfAdı.Name = "txtOgrenciYonetimiSınıfAdı";
        txtOgrenciYonetimiSınıfAdı.PlaceholderText = "Sınıf Adı";
        txtOgrenciYonetimiSınıfAdı.Size = new Size(344, 25);
        txtOgrenciYonetimiSınıfAdı.TabIndex = 57;
        // 
        // lbl_cbxOgrenciYonetimiYasGrubu
        // 
        lbl_cbxOgrenciYonetimiYasGrubu.AutoSize = true;
        lbl_cbxOgrenciYonetimiYasGrubu.ForeColor = Color.FromArgb(68, 87, 111);
        lbl_cbxOgrenciYonetimiYasGrubu.Location = new Point(376, 18);
        lbl_cbxOgrenciYonetimiYasGrubu.Margin = new Padding(0, 2, 12, 6);
        lbl_cbxOgrenciYonetimiYasGrubu.Name = "lbl_cbxOgrenciYonetimiYasGrubu";
        lbl_cbxOgrenciYonetimiYasGrubu.Size = new Size(70, 18);
        lbl_cbxOgrenciYonetimiYasGrubu.TabIndex = 58;
        lbl_cbxOgrenciYonetimiYasGrubu.Text = "Yaş grubu";
        // 
        // cbxOgrenciYonetimiYasGrubu
        // 
        cbxOgrenciYonetimiYasGrubu.Dock = DockStyle.Fill;
        cbxOgrenciYonetimiYasGrubu.DropDownStyle = ComboBoxStyle.DropDownList;
        cbxOgrenciYonetimiYasGrubu.Font = new Font("Segoe UI", 10F);
        cbxOgrenciYonetimiYasGrubu.ForeColor = Color.FromArgb(37, 54, 75);
        cbxOgrenciYonetimiYasGrubu.Location = new Point(376, 42);
        cbxOgrenciYonetimiYasGrubu.Margin = new Padding(0, 0, 16, 10);
        cbxOgrenciYonetimiYasGrubu.Name = "cbxOgrenciYonetimiYasGrubu";
        cbxOgrenciYonetimiYasGrubu.Size = new Size(344, 25);
        cbxOgrenciYonetimiYasGrubu.TabIndex = 59;
        // 
        // lbl_cbxOgrenciYonetimiOgretmen
        // 
        lbl_cbxOgrenciYonetimiOgretmen.AutoSize = true;
        lbl_cbxOgrenciYonetimiOgretmen.ForeColor = Color.FromArgb(68, 87, 111);
        lbl_cbxOgrenciYonetimiOgretmen.Location = new Point(736, 18);
        lbl_cbxOgrenciYonetimiOgretmen.Margin = new Padding(0, 2, 12, 6);
        lbl_cbxOgrenciYonetimiOgretmen.Name = "lbl_cbxOgrenciYonetimiOgretmen";
        lbl_cbxOgrenciYonetimiOgretmen.Size = new Size(72, 18);
        lbl_cbxOgrenciYonetimiOgretmen.TabIndex = 60;
        lbl_cbxOgrenciYonetimiOgretmen.Text = "Öğretmen";
        // 
        // cbxOgrenciYonetimiOgretmen
        // 
        cbxOgrenciYonetimiOgretmen.Dock = DockStyle.Fill;
        cbxOgrenciYonetimiOgretmen.DropDownStyle = ComboBoxStyle.DropDownList;
        cbxOgrenciYonetimiOgretmen.Font = new Font("Segoe UI", 10F);
        cbxOgrenciYonetimiOgretmen.ForeColor = Color.FromArgb(37, 54, 75);
        cbxOgrenciYonetimiOgretmen.Location = new Point(736, 42);
        cbxOgrenciYonetimiOgretmen.Margin = new Padding(0, 0, 16, 10);
        cbxOgrenciYonetimiOgretmen.Name = "cbxOgrenciYonetimiOgretmen";
        cbxOgrenciYonetimiOgretmen.Size = new Size(344, 25);
        cbxOgrenciYonetimiOgretmen.TabIndex = 61;
        cbxOgrenciYonetimiOgretmen.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        // 
        // actionsDgvOgrenciYonetimiSiniflar
        // 
        actionsDgvOgrenciYonetimiSiniflar.AutoSize = true;
        actionsDgvOgrenciYonetimiSiniflar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        actionsDgvOgrenciYonetimiSiniflar.BackColor = Color.FromArgb(239, 247, 253);
        actionsDgvOgrenciYonetimiSiniflar.Controls.Add(btnOgrenciYonetimiSinifKaydet);
        actionsDgvOgrenciYonetimiSiniflar.Controls.Add(btnOgrenciYonetimiSinifGuncelle);
        actionsDgvOgrenciYonetimiSiniflar.Controls.Add(btnOgrenciYonetimiSinifSil);
        actionsDgvOgrenciYonetimiSiniflar.Dock = DockStyle.Top;
        actionsDgvOgrenciYonetimiSiniflar.Location = new Point(0, 99);
        actionsDgvOgrenciYonetimiSiniflar.Margin = new Padding(0);
        actionsDgvOgrenciYonetimiSiniflar.Name = "actionsDgvOgrenciYonetimiSiniflar";
        actionsDgvOgrenciYonetimiSiniflar.Padding = new Padding(16, 8, 8, 8);
        actionsDgvOgrenciYonetimiSiniflar.Size = new Size(1112, 55);
        actionsDgvOgrenciYonetimiSiniflar.TabIndex = 1;
        // 
        // btnOgrenciYonetimiSinifKaydet
        // 
        btnOgrenciYonetimiSinifKaydet.AutoSize = true;
        btnOgrenciYonetimiSinifKaydet.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        btnOgrenciYonetimiSinifKaydet.BackColor = Color.FromArgb(246, 248, 251);
        btnOgrenciYonetimiSinifKaydet.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        btnOgrenciYonetimiSinifKaydet.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        btnOgrenciYonetimiSinifKaydet.FlatStyle = FlatStyle.Flat;
        btnOgrenciYonetimiSinifKaydet.ForeColor = Color.FromArgb(37, 54, 75);
        btnOgrenciYonetimiSinifKaydet.Location = new Point(16, 8);
        btnOgrenciYonetimiSinifKaydet.Margin = new Padding(0, 0, 8, 0);
        btnOgrenciYonetimiSinifKaydet.MinimumSize = new Size(96, 34);
        btnOgrenciYonetimiSinifKaydet.Name = "btnOgrenciYonetimiSinifKaydet";
        btnOgrenciYonetimiSinifKaydet.Padding = new Padding(12, 4, 12, 4);
        btnOgrenciYonetimiSinifKaydet.Size = new Size(96, 39);
        btnOgrenciYonetimiSinifKaydet.TabIndex = 0;
        btnOgrenciYonetimiSinifKaydet.Text = "Kaydet";
        btnOgrenciYonetimiSinifKaydet.UseVisualStyleBackColor = false;
        btnOgrenciYonetimiSinifKaydet.Click += btnOgrenciYonetimiSinifKaydet_Click;
        // 
        // btnOgrenciYonetimiSinifGuncelle
        // 
        btnOgrenciYonetimiSinifGuncelle.AutoSize = true;
        btnOgrenciYonetimiSinifGuncelle.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        btnOgrenciYonetimiSinifGuncelle.BackColor = Color.FromArgb(246, 248, 251);
        btnOgrenciYonetimiSinifGuncelle.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        btnOgrenciYonetimiSinifGuncelle.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        btnOgrenciYonetimiSinifGuncelle.FlatStyle = FlatStyle.Flat;
        btnOgrenciYonetimiSinifGuncelle.ForeColor = Color.FromArgb(37, 54, 75);
        btnOgrenciYonetimiSinifGuncelle.Location = new Point(120, 8);
        btnOgrenciYonetimiSinifGuncelle.Margin = new Padding(0, 0, 8, 0);
        btnOgrenciYonetimiSinifGuncelle.MinimumSize = new Size(96, 34);
        btnOgrenciYonetimiSinifGuncelle.Name = "btnOgrenciYonetimiSinifGuncelle";
        btnOgrenciYonetimiSinifGuncelle.Padding = new Padding(12, 4, 12, 4);
        btnOgrenciYonetimiSinifGuncelle.Size = new Size(97, 39);
        btnOgrenciYonetimiSinifGuncelle.TabIndex = 1;
        btnOgrenciYonetimiSinifGuncelle.Text = "Güncelle";
        btnOgrenciYonetimiSinifGuncelle.UseVisualStyleBackColor = false;
        btnOgrenciYonetimiSinifGuncelle.Click += btnOgrenciYonetimiSinifGuncelle_Click;
        // 
        // btnOgrenciYonetimiSinifSil
        // 
        btnOgrenciYonetimiSinifSil.AutoSize = true;
        btnOgrenciYonetimiSinifSil.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        btnOgrenciYonetimiSinifSil.BackColor = Color.FromArgb(246, 248, 251);
        btnOgrenciYonetimiSinifSil.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        btnOgrenciYonetimiSinifSil.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        btnOgrenciYonetimiSinifSil.FlatStyle = FlatStyle.Flat;
        btnOgrenciYonetimiSinifSil.ForeColor = Color.FromArgb(37, 54, 75);
        btnOgrenciYonetimiSinifSil.Location = new Point(225, 8);
        btnOgrenciYonetimiSinifSil.Margin = new Padding(0, 0, 8, 0);
        btnOgrenciYonetimiSinifSil.MinimumSize = new Size(96, 34);
        btnOgrenciYonetimiSinifSil.Name = "btnOgrenciYonetimiSinifSil";
        btnOgrenciYonetimiSinifSil.Padding = new Padding(12, 4, 12, 4);
        btnOgrenciYonetimiSinifSil.Size = new Size(96, 39);
        btnOgrenciYonetimiSinifSil.TabIndex = 2;
        btnOgrenciYonetimiSinifSil.Text = "Sil";
        btnOgrenciYonetimiSinifSil.UseVisualStyleBackColor = false;
        btnOgrenciYonetimiSinifSil.Click += btnOgrenciYonetimiSinifSil_Click;
        // 
        // listDgvOgrenciYonetimiSiniflar
        // 
        listDgvOgrenciYonetimiSiniflar.BackColor = Color.FromArgb(239, 247, 253);
        listDgvOgrenciYonetimiSiniflar.ColumnCount = 1;
        listDgvOgrenciYonetimiSiniflar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        listDgvOgrenciYonetimiSiniflar.Controls.Add(listDgvOgrenciYonetimiSiniflarToolbar, 0, 0);
        listDgvOgrenciYonetimiSiniflar.Controls.Add(DgvOgrenciYonetimiSiniflar, 0, 1);
        listDgvOgrenciYonetimiSiniflar.Controls.Add(listDgvOgrenciYonetimiSiniflarCount, 0, 2);
        listDgvOgrenciYonetimiSiniflar.Dock = DockStyle.Fill;
        listDgvOgrenciYonetimiSiniflar.Location = new Point(0, 154);
        listDgvOgrenciYonetimiSiniflar.Margin = new Padding(0);
        listDgvOgrenciYonetimiSiniflar.Name = "listDgvOgrenciYonetimiSiniflar";
        listDgvOgrenciYonetimiSiniflar.Padding = new Padding(8);
        listDgvOgrenciYonetimiSiniflar.RowCount = 3;
        listDgvOgrenciYonetimiSiniflar.RowStyles.Add(new RowStyle());
        listDgvOgrenciYonetimiSiniflar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        listDgvOgrenciYonetimiSiniflar.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        listDgvOgrenciYonetimiSiniflar.Size = new Size(1112, 578);
        listDgvOgrenciYonetimiSiniflar.TabIndex = 1;
        // 
        // listDgvOgrenciYonetimiSiniflarToolbar
        // 
        listDgvOgrenciYonetimiSiniflarToolbar.AutoSize = true;
        listDgvOgrenciYonetimiSiniflarToolbar.BackColor = Color.FromArgb(218, 236, 250);
        listDgvOgrenciYonetimiSiniflarToolbar.ColumnCount = 4;
        listDgvOgrenciYonetimiSiniflarToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        listDgvOgrenciYonetimiSiniflarToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
        listDgvOgrenciYonetimiSiniflarToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        listDgvOgrenciYonetimiSiniflarToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        listDgvOgrenciYonetimiSiniflarToolbar.Controls.Add(listDgvOgrenciYonetimiSiniflarTitle, 0, 0);
        listDgvOgrenciYonetimiSiniflarToolbar.Controls.Add(txtClassesSearch, 1, 0);
        listDgvOgrenciYonetimiSiniflarToolbar.Controls.Add(listDgvOgrenciYonetimiSiniflarClear, 2, 0);
        listDgvOgrenciYonetimiSiniflarToolbar.Controls.Add(listDgvOgrenciYonetimiSiniflarColumns, 3, 0);
        listDgvOgrenciYonetimiSiniflarToolbar.Dock = DockStyle.Top;
        listDgvOgrenciYonetimiSiniflarToolbar.Location = new Point(8, 8);
        listDgvOgrenciYonetimiSiniflarToolbar.Margin = new Padding(0);
        listDgvOgrenciYonetimiSiniflarToolbar.Name = "listDgvOgrenciYonetimiSiniflarToolbar";
        listDgvOgrenciYonetimiSiniflarToolbar.RowCount = 1;
        listDgvOgrenciYonetimiSiniflarToolbar.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        listDgvOgrenciYonetimiSiniflarToolbar.Size = new Size(1096, 42);
        listDgvOgrenciYonetimiSiniflarToolbar.TabIndex = 0;
        // 
        // listDgvOgrenciYonetimiSiniflarTitle
        // 
        listDgvOgrenciYonetimiSiniflarTitle.AutoEllipsis = true;
        listDgvOgrenciYonetimiSiniflarTitle.Dock = DockStyle.Fill;
        listDgvOgrenciYonetimiSiniflarTitle.Font = new Font("Segoe UI Semibold", 11F);
        listDgvOgrenciYonetimiSiniflarTitle.ForeColor = Color.FromArgb(68, 87, 111);
        listDgvOgrenciYonetimiSiniflarTitle.Location = new Point(0, 2);
        listDgvOgrenciYonetimiSiniflarTitle.Margin = new Padding(0, 2, 12, 6);
        listDgvOgrenciYonetimiSiniflarTitle.Name = "listDgvOgrenciYonetimiSiniflarTitle";
        listDgvOgrenciYonetimiSiniflarTitle.Size = new Size(662, 34);
        listDgvOgrenciYonetimiSiniflarTitle.TabIndex = 0;
        listDgvOgrenciYonetimiSiniflarTitle.Text = "Sınıflar";
        // 
        // txtClassesSearch
        // 
        txtClassesSearch.BorderStyle = BorderStyle.FixedSingle;
        txtClassesSearch.Dock = DockStyle.Fill;
        txtClassesSearch.Font = new Font("Segoe UI", 10F);
        txtClassesSearch.ForeColor = Color.FromArgb(37, 54, 75);
        txtClassesSearch.Location = new Point(674, 4);
        txtClassesSearch.Margin = new Padding(0, 4, 8, 6);
        txtClassesSearch.Name = "txtClassesSearch";
        txtClassesSearch.PlaceholderText = "Listede ara (Ctrl+F)";
        txtClassesSearch.Size = new Size(222, 25);
        txtClassesSearch.TabIndex = 52;
        // 
        // listDgvOgrenciYonetimiSiniflarClear
        // 
        listDgvOgrenciYonetimiSiniflarClear.AutoSize = true;
        listDgvOgrenciYonetimiSiniflarClear.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        listDgvOgrenciYonetimiSiniflarClear.BackColor = Color.FromArgb(246, 248, 251);
        listDgvOgrenciYonetimiSiniflarClear.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        listDgvOgrenciYonetimiSiniflarClear.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        listDgvOgrenciYonetimiSiniflarClear.FlatStyle = FlatStyle.Flat;
        listDgvOgrenciYonetimiSiniflarClear.ForeColor = Color.FromArgb(37, 54, 75);
        listDgvOgrenciYonetimiSiniflarClear.Location = new Point(904, 0);
        listDgvOgrenciYonetimiSiniflarClear.Margin = new Padding(0, 0, 8, 0);
        listDgvOgrenciYonetimiSiniflarClear.MinimumSize = new Size(96, 34);
        listDgvOgrenciYonetimiSiniflarClear.Name = "listDgvOgrenciYonetimiSiniflarClear";
        listDgvOgrenciYonetimiSiniflarClear.Padding = new Padding(12, 4, 12, 4);
        listDgvOgrenciYonetimiSiniflarClear.Size = new Size(96, 39);
        listDgvOgrenciYonetimiSiniflarClear.TabIndex = 53;
        listDgvOgrenciYonetimiSiniflarClear.Text = "Temizle";
        listDgvOgrenciYonetimiSiniflarClear.UseVisualStyleBackColor = false;
        // 
        // listDgvOgrenciYonetimiSiniflarColumns
        // 
        listDgvOgrenciYonetimiSiniflarColumns.AutoSize = true;
        listDgvOgrenciYonetimiSiniflarColumns.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        listDgvOgrenciYonetimiSiniflarColumns.BackColor = Color.FromArgb(246, 248, 251);
        listDgvOgrenciYonetimiSiniflarColumns.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        listDgvOgrenciYonetimiSiniflarColumns.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        listDgvOgrenciYonetimiSiniflarColumns.FlatStyle = FlatStyle.Flat;
        listDgvOgrenciYonetimiSiniflarColumns.ForeColor = Color.FromArgb(37, 54, 75);
        listDgvOgrenciYonetimiSiniflarColumns.Location = new Point(996, 0);
        listDgvOgrenciYonetimiSiniflarColumns.Margin = new Padding(0, 0, 8, 0);
        listDgvOgrenciYonetimiSiniflarColumns.MinimumSize = new Size(96, 34);
        listDgvOgrenciYonetimiSiniflarColumns.Name = "listDgvOgrenciYonetimiSiniflarColumns";
        listDgvOgrenciYonetimiSiniflarColumns.Padding = new Padding(12, 4, 12, 4);
        listDgvOgrenciYonetimiSiniflarColumns.Size = new Size(96, 39);
        listDgvOgrenciYonetimiSiniflarColumns.TabIndex = 54;
        listDgvOgrenciYonetimiSiniflarColumns.Text = "Sütunlar";
        listDgvOgrenciYonetimiSiniflarColumns.UseVisualStyleBackColor = false;
        // 
        // DgvOgrenciYonetimiSiniflar
        // 
        DgvOgrenciYonetimiSiniflar.AllowUserToAddRows = false;
        DgvOgrenciYonetimiSiniflar.AllowUserToDeleteRows = false;
        DgvOgrenciYonetimiSiniflar.AllowUserToOrderColumns = true;
        DgvOgrenciYonetimiSiniflar.AllowUserToResizeRows = false;
        dataGridViewCellStyle10.BackColor = Color.FromArgb(239, 247, 253);
        DgvOgrenciYonetimiSiniflar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
        DgvOgrenciYonetimiSiniflar.BackgroundColor = Color.FromArgb(239, 247, 253);
        DgvOgrenciYonetimiSiniflar.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle11.BackColor = Color.FromArgb(218, 236, 250);
        dataGridViewCellStyle11.Font = new Font("Segoe UI Semibold", 9.5F);
        dataGridViewCellStyle11.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle11.SelectionBackColor = Color.FromArgb(218, 236, 250);
        dataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle11.WrapMode = DataGridViewTriState.True;
        DgvOgrenciYonetimiSiniflar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
        DgvOgrenciYonetimiSiniflar.ColumnHeadersHeight = 36;
        DgvOgrenciYonetimiSiniflar.ContextMenuStrip = contextMenuStrip1;
        dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle12.BackColor = SystemColors.Window;
        dataGridViewCellStyle12.Font = new Font("Segoe UI", 9.5F);
        dataGridViewCellStyle12.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle12.SelectionBackColor = Color.FromArgb(200, 224, 246);
        dataGridViewCellStyle12.SelectionForeColor = Color.FromArgb(37, 54, 75);
        dataGridViewCellStyle12.WrapMode = DataGridViewTriState.False;
        DgvOgrenciYonetimiSiniflar.DefaultCellStyle = dataGridViewCellStyle12;
        DgvOgrenciYonetimiSiniflar.Dock = DockStyle.Fill;
        DgvOgrenciYonetimiSiniflar.EnableHeadersVisualStyles = false;
        DgvOgrenciYonetimiSiniflar.Location = new Point(11, 53);
        DgvOgrenciYonetimiSiniflar.MultiSelect = false;
        DgvOgrenciYonetimiSiniflar.Name = "DgvOgrenciYonetimiSiniflar";
        DgvOgrenciYonetimiSiniflar.ReadOnly = true;
        DgvOgrenciYonetimiSiniflar.RowHeadersWidth = 44;
        DgvOgrenciYonetimiSiniflar.RowTemplate.Height = 32;
        DgvOgrenciYonetimiSiniflar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        DgvOgrenciYonetimiSiniflar.Size = new Size(1090, 486);
        DgvOgrenciYonetimiSiniflar.TabIndex = 1;
        DgvOgrenciYonetimiSiniflar.Tag = 4030;
        DgvOgrenciYonetimiSiniflar.CellContentClick += DgvOgrenciYonetimiSiniflar_CellContentClick;
        // 
        // listDgvOgrenciYonetimiSiniflarCount
        // 
        listDgvOgrenciYonetimiSiniflarCount.AutoSize = true;
        listDgvOgrenciYonetimiSiniflarCount.Dock = DockStyle.Fill;
        listDgvOgrenciYonetimiSiniflarCount.ForeColor = Color.FromArgb(68, 87, 111);
        listDgvOgrenciYonetimiSiniflarCount.Location = new Point(8, 544);
        listDgvOgrenciYonetimiSiniflarCount.Margin = new Padding(0, 2, 12, 6);
        listDgvOgrenciYonetimiSiniflarCount.Name = "listDgvOgrenciYonetimiSiniflarCount";
        listDgvOgrenciYonetimiSiniflarCount.Size = new Size(1084, 20);
        listDgvOgrenciYonetimiSiniflarCount.TabIndex = 2;
        listDgvOgrenciYonetimiSiniflarCount.Text = "Kayıt: 0";
        // 
        // tabPageSatis
        // 
        tabPageSatis.BackColor = Color.FromArgb(239, 247, 253);
        tabPageSatis.Controls.Add(bodydataOgrVw);
        tabPageSatis.Location = new Point(4, 24);
        tabPageSatis.Name = "tabPageSatis";
        tabPageSatis.Size = new Size(1112, 732);
        tabPageSatis.TabIndex = 4;
        tabPageSatis.Text = "Tahsilatlar";
        // 
        // bodydataOgrVw
        // 
        bodydataOgrVw.BackColor = Color.FromArgb(239, 247, 253);
        bodydataOgrVw.ColumnCount = 1;
        bodydataOgrVw.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        bodydataOgrVw.Controls.Add(editordataOgrVw, 0, 0);
        bodydataOgrVw.Controls.Add(listdataOgrVw, 0, 1);
        bodydataOgrVw.Dock = DockStyle.Fill;
        bodydataOgrVw.Location = new Point(0, 0);
        bodydataOgrVw.Margin = new Padding(0);
        bodydataOgrVw.Name = "bodydataOgrVw";
        bodydataOgrVw.RowCount = 2;
        bodydataOgrVw.RowStyles.Add(new RowStyle(SizeType.Absolute, 154F));
        bodydataOgrVw.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        bodydataOgrVw.Size = new Size(1112, 732);
        bodydataOgrVw.TabIndex = 0;
        // 
        // editordataOgrVw
        // 
        editordataOgrVw.BackColor = Color.FromArgb(239, 247, 253);
        editordataOgrVw.ColumnCount = 1;
        editordataOgrVw.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        editordataOgrVw.Controls.Add(fieldsdataOgrVw, 0, 0);
        editordataOgrVw.Controls.Add(actionsdataOgrVw, 0, 1);
        editordataOgrVw.Dock = DockStyle.Fill;
        editordataOgrVw.Location = new Point(0, 0);
        editordataOgrVw.Margin = new Padding(0);
        editordataOgrVw.Name = "editordataOgrVw";
        editordataOgrVw.RowCount = 2;
        editordataOgrVw.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        editordataOgrVw.RowStyles.Add(new RowStyle());
        editordataOgrVw.Size = new Size(1112, 154);
        editordataOgrVw.TabIndex = 0;
        // 
        // fieldsdataOgrVw
        // 
        fieldsdataOgrVw.AutoScroll = true;
        fieldsdataOgrVw.BackColor = Color.FromArgb(239, 247, 253);
        fieldsdataOgrVw.ColumnCount = 3;
        fieldsdataOgrVw.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        fieldsdataOgrVw.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        fieldsdataOgrVw.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        fieldsdataOgrVw.Controls.Add(lbl_comboBoxStok, 0, 0);
        fieldsdataOgrVw.Controls.Add(comboBoxStok, 0, 1);
        fieldsdataOgrVw.Controls.Add(lbl_numericQuantitySold, 1, 0);
        fieldsdataOgrVw.Controls.Add(numericQuantitySold, 1, 1);
        fieldsdataOgrVw.Dock = DockStyle.Fill;
        fieldsdataOgrVw.Location = new Point(0, 0);
        fieldsdataOgrVw.Margin = new Padding(0);
        fieldsdataOgrVw.Name = "fieldsdataOgrVw";
        fieldsdataOgrVw.Padding = new Padding(16);
        fieldsdataOgrVw.RowCount = 3;
        fieldsdataOgrVw.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
        fieldsdataOgrVw.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        fieldsdataOgrVw.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        fieldsdataOgrVw.Size = new Size(1112, 99);
        fieldsdataOgrVw.TabIndex = 0;
        // 
        // lbl_comboBoxStok
        // 
        lbl_comboBoxStok.AutoSize = true;
        lbl_comboBoxStok.ForeColor = Color.FromArgb(68, 87, 111);
        lbl_comboBoxStok.Location = new Point(16, 18);
        lbl_comboBoxStok.Margin = new Padding(0, 2, 12, 6);
        lbl_comboBoxStok.Name = "lbl_comboBoxStok";
        lbl_comboBoxStok.Size = new Size(57, 18);
        lbl_comboBoxStok.TabIndex = 0;
        lbl_comboBoxStok.Text = "Öğrenci";
        // 
        // comboBoxStok
        // 
        comboBoxStok.Dock = DockStyle.Fill;
        comboBoxStok.Font = new Font("Segoe UI", 10F);
        comboBoxStok.ForeColor = Color.FromArgb(37, 54, 75);
        comboBoxStok.Location = new Point(16, 42);
        comboBoxStok.Margin = new Padding(0, 0, 16, 10);
        comboBoxStok.Name = "comboBoxStok";
        comboBoxStok.Size = new Size(344, 25);
        comboBoxStok.TabIndex = 79;
        // 
        // lbl_numericQuantitySold
        // 
        lbl_numericQuantitySold.AutoSize = true;
        lbl_numericQuantitySold.ForeColor = Color.FromArgb(68, 87, 111);
        lbl_numericQuantitySold.Location = new Point(376, 18);
        lbl_numericQuantitySold.Margin = new Padding(0, 2, 12, 6);
        lbl_numericQuantitySold.Name = "lbl_numericQuantitySold";
        lbl_numericQuantitySold.Size = new Size(111, 18);
        lbl_numericQuantitySold.TabIndex = 80;
        lbl_numericQuantitySold.Text = "Ödeme tutarı (₺)";
        // 
        // numericQuantitySold
        // 
        numericQuantitySold.DecimalPlaces = 2;
        numericQuantitySold.Dock = DockStyle.Fill;
        numericQuantitySold.Font = new Font("Segoe UI", 10F);
        numericQuantitySold.ForeColor = Color.FromArgb(37, 54, 75);
        numericQuantitySold.Location = new Point(376, 42);
        numericQuantitySold.Margin = new Padding(0, 0, 16, 10);
        numericQuantitySold.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        numericQuantitySold.Name = "numericQuantitySold";
        numericQuantitySold.Size = new Size(344, 25);
        numericQuantitySold.TabIndex = 81;
        numericQuantitySold.TextAlign = HorizontalAlignment.Right;
        numericQuantitySold.ThousandsSeparator = true;
        // 
        // actionsdataOgrVw
        // 
        actionsdataOgrVw.AutoSize = true;
        actionsdataOgrVw.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        actionsdataOgrVw.BackColor = Color.FromArgb(239, 247, 253);
        actionsdataOgrVw.Controls.Add(btnMakeSale);
        actionsdataOgrVw.Dock = DockStyle.Top;
        actionsdataOgrVw.Location = new Point(0, 99);
        actionsdataOgrVw.Margin = new Padding(0);
        actionsdataOgrVw.Name = "actionsdataOgrVw";
        actionsdataOgrVw.Padding = new Padding(16, 8, 8, 8);
        actionsdataOgrVw.Size = new Size(1112, 55);
        actionsdataOgrVw.TabIndex = 1;
        // 
        // btnMakeSale
        // 
        btnMakeSale.AutoSize = true;
        btnMakeSale.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        btnMakeSale.BackColor = Color.FromArgb(246, 248, 251);
        btnMakeSale.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        btnMakeSale.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        btnMakeSale.FlatStyle = FlatStyle.Flat;
        btnMakeSale.ForeColor = Color.FromArgb(37, 54, 75);
        btnMakeSale.Location = new Point(16, 8);
        btnMakeSale.Margin = new Padding(0, 0, 8, 0);
        btnMakeSale.MinimumSize = new Size(96, 34);
        btnMakeSale.Name = "btnMakeSale";
        btnMakeSale.Padding = new Padding(12, 4, 12, 4);
        btnMakeSale.Size = new Size(134, 39);
        btnMakeSale.TabIndex = 0;
        btnMakeSale.Text = "Tahsilat kaydet";
        btnMakeSale.UseVisualStyleBackColor = false;
        btnMakeSale.Click += btnMakeSale_Click;
        // 
        // listdataOgrVw
        // 
        listdataOgrVw.BackColor = Color.FromArgb(239, 247, 253);
        listdataOgrVw.ColumnCount = 1;
        listdataOgrVw.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        listdataOgrVw.Controls.Add(listdataOgrVwToolbar, 0, 0);
        listdataOgrVw.Controls.Add(dataOgrVw, 0, 1);
        listdataOgrVw.Controls.Add(listdataOgrVwCount, 0, 2);
        listdataOgrVw.Dock = DockStyle.Fill;
        listdataOgrVw.Location = new Point(0, 154);
        listdataOgrVw.Margin = new Padding(0);
        listdataOgrVw.Name = "listdataOgrVw";
        listdataOgrVw.Padding = new Padding(8);
        listdataOgrVw.RowCount = 3;
        listdataOgrVw.RowStyles.Add(new RowStyle());
        listdataOgrVw.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        listdataOgrVw.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        listdataOgrVw.Size = new Size(1112, 578);
        listdataOgrVw.TabIndex = 1;
        // 
        // listdataOgrVwToolbar
        // 
        listdataOgrVwToolbar.AutoSize = true;
        listdataOgrVwToolbar.BackColor = Color.FromArgb(218, 236, 250);
        listdataOgrVwToolbar.ColumnCount = 4;
        listdataOgrVwToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        listdataOgrVwToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
        listdataOgrVwToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        listdataOgrVwToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        listdataOgrVwToolbar.Controls.Add(listdataOgrVwTitle, 0, 0);
        listdataOgrVwToolbar.Controls.Add(txtPaymentsSearch, 1, 0);
        listdataOgrVwToolbar.Controls.Add(listdataOgrVwClear, 2, 0);
        listdataOgrVwToolbar.Controls.Add(listdataOgrVwColumns, 3, 0);
        listdataOgrVwToolbar.Dock = DockStyle.Top;
        listdataOgrVwToolbar.Location = new Point(8, 8);
        listdataOgrVwToolbar.Margin = new Padding(0);
        listdataOgrVwToolbar.Name = "listdataOgrVwToolbar";
        listdataOgrVwToolbar.RowCount = 1;
        listdataOgrVwToolbar.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        listdataOgrVwToolbar.Size = new Size(1096, 42);
        listdataOgrVwToolbar.TabIndex = 0;
        // 
        // listdataOgrVwTitle
        // 
        listdataOgrVwTitle.AutoEllipsis = true;
        listdataOgrVwTitle.Dock = DockStyle.Fill;
        listdataOgrVwTitle.Font = new Font("Segoe UI Semibold", 11F);
        listdataOgrVwTitle.ForeColor = Color.FromArgb(68, 87, 111);
        listdataOgrVwTitle.Location = new Point(0, 2);
        listdataOgrVwTitle.Margin = new Padding(0, 2, 12, 6);
        listdataOgrVwTitle.Name = "listdataOgrVwTitle";
        listdataOgrVwTitle.Size = new Size(662, 34);
        listdataOgrVwTitle.TabIndex = 0;
        listdataOgrVwTitle.Text = "Tahsilatlar";
        // 
        // txtPaymentsSearch
        // 
        txtPaymentsSearch.BorderStyle = BorderStyle.FixedSingle;
        txtPaymentsSearch.Dock = DockStyle.Fill;
        txtPaymentsSearch.Font = new Font("Segoe UI", 10F);
        txtPaymentsSearch.ForeColor = Color.FromArgb(37, 54, 75);
        txtPaymentsSearch.Location = new Point(674, 4);
        txtPaymentsSearch.Margin = new Padding(0, 4, 8, 6);
        txtPaymentsSearch.Name = "txtPaymentsSearch";
        txtPaymentsSearch.PlaceholderText = "Listede ara (Ctrl+F)";
        txtPaymentsSearch.Size = new Size(222, 25);
        txtPaymentsSearch.TabIndex = 74;
        // 
        // listdataOgrVwClear
        // 
        listdataOgrVwClear.AutoSize = true;
        listdataOgrVwClear.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        listdataOgrVwClear.BackColor = Color.FromArgb(246, 248, 251);
        listdataOgrVwClear.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        listdataOgrVwClear.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        listdataOgrVwClear.FlatStyle = FlatStyle.Flat;
        listdataOgrVwClear.ForeColor = Color.FromArgb(37, 54, 75);
        listdataOgrVwClear.Location = new Point(904, 0);
        listdataOgrVwClear.Margin = new Padding(0, 0, 8, 0);
        listdataOgrVwClear.MinimumSize = new Size(96, 34);
        listdataOgrVwClear.Name = "listdataOgrVwClear";
        listdataOgrVwClear.Padding = new Padding(12, 4, 12, 4);
        listdataOgrVwClear.Size = new Size(96, 39);
        listdataOgrVwClear.TabIndex = 75;
        listdataOgrVwClear.Text = "Temizle";
        listdataOgrVwClear.UseVisualStyleBackColor = false;
        // 
        // listdataOgrVwColumns
        // 
        listdataOgrVwColumns.AutoSize = true;
        listdataOgrVwColumns.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        listdataOgrVwColumns.BackColor = Color.FromArgb(246, 248, 251);
        listdataOgrVwColumns.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        listdataOgrVwColumns.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        listdataOgrVwColumns.FlatStyle = FlatStyle.Flat;
        listdataOgrVwColumns.ForeColor = Color.FromArgb(37, 54, 75);
        listdataOgrVwColumns.Location = new Point(996, 0);
        listdataOgrVwColumns.Margin = new Padding(0, 0, 8, 0);
        listdataOgrVwColumns.MinimumSize = new Size(96, 34);
        listdataOgrVwColumns.Name = "listdataOgrVwColumns";
        listdataOgrVwColumns.Padding = new Padding(12, 4, 12, 4);
        listdataOgrVwColumns.Size = new Size(96, 39);
        listdataOgrVwColumns.TabIndex = 76;
        listdataOgrVwColumns.Text = "Sütunlar";
        listdataOgrVwColumns.UseVisualStyleBackColor = false;
        // 
        // dataOgrVw
        // 
        dataOgrVw.AllowUserToAddRows = false;
        dataOgrVw.AllowUserToDeleteRows = false;
        dataOgrVw.AllowUserToOrderColumns = true;
        dataOgrVw.AllowUserToResizeRows = false;
        dataGridViewCellStyle13.BackColor = Color.FromArgb(239, 247, 253);
        dataOgrVw.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
        dataOgrVw.BackgroundColor = Color.FromArgb(239, 247, 253);
        dataOgrVw.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle14.BackColor = Color.FromArgb(218, 236, 250);
        dataGridViewCellStyle14.Font = new Font("Segoe UI Semibold", 9.5F);
        dataGridViewCellStyle14.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle14.SelectionBackColor = Color.FromArgb(218, 236, 250);
        dataGridViewCellStyle14.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle14.WrapMode = DataGridViewTriState.True;
        dataOgrVw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
        dataOgrVw.ColumnHeadersHeight = 36;
        dataOgrVw.ContextMenuStrip = contextMenuStrip1;
        dataGridViewCellStyle15.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle15.BackColor = SystemColors.Window;
        dataGridViewCellStyle15.Font = new Font("Segoe UI", 9.5F);
        dataGridViewCellStyle15.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle15.SelectionBackColor = Color.FromArgb(200, 224, 246);
        dataGridViewCellStyle15.SelectionForeColor = Color.FromArgb(37, 54, 75);
        dataGridViewCellStyle15.WrapMode = DataGridViewTriState.False;
        dataOgrVw.DefaultCellStyle = dataGridViewCellStyle15;
        dataOgrVw.Dock = DockStyle.Fill;
        dataOgrVw.EnableHeadersVisualStyles = false;
        dataOgrVw.Location = new Point(11, 53);
        dataOgrVw.MultiSelect = false;
        dataOgrVw.Name = "dataOgrVw";
        dataOgrVw.ReadOnly = true;
        dataOgrVw.RowHeadersWidth = 44;
        dataOgrVw.RowTemplate.Height = 32;
        dataOgrVw.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataOgrVw.Size = new Size(1090, 486);
        dataOgrVw.TabIndex = 1;
        dataOgrVw.CellContentClick += dataOgrVw_CellContentClick;
        // 
        // listdataOgrVwCount
        // 
        listdataOgrVwCount.AutoSize = true;
        listdataOgrVwCount.Dock = DockStyle.Fill;
        listdataOgrVwCount.ForeColor = Color.FromArgb(68, 87, 111);
        listdataOgrVwCount.Location = new Point(8, 544);
        listdataOgrVwCount.Margin = new Padding(0, 2, 12, 6);
        listdataOgrVwCount.Name = "listdataOgrVwCount";
        listdataOgrVwCount.Size = new Size(1084, 20);
        listdataOgrVwCount.TabIndex = 2;
        listdataOgrVwCount.Text = "Kayıt: 0";
        // 
        // tabPageGelirGider
        // 
        tabPageGelirGider.BackColor = Color.FromArgb(239, 247, 253);
        tabPageGelirGider.Controls.Add(bodydataGridOdeme);
        tabPageGelirGider.Location = new Point(4, 24);
        tabPageGelirGider.Name = "tabPageGelirGider";
        tabPageGelirGider.Size = new Size(1112, 732);
        tabPageGelirGider.TabIndex = 5;
        tabPageGelirGider.Text = "Gelir / Gider";
        // 
        // bodydataGridOdeme
        // 
        bodydataGridOdeme.BackColor = Color.FromArgb(239, 247, 253);
        bodydataGridOdeme.ColumnCount = 1;
        bodydataGridOdeme.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        bodydataGridOdeme.Controls.Add(editordataGridOdeme, 0, 0);
        lblFinanceSummary.Name = "lblFinanceSummary";
        lblFinanceSummary.Dock = DockStyle.Fill;
        lblFinanceSummary.TextAlign = ContentAlignment.MiddleLeft;
        lblFinanceSummary.AutoEllipsis = true;
        lblFinanceSummary.Padding = new Padding(16, 0, 0, 0);
        lblFinanceSummary.BackColor = Color.FromArgb(218, 236, 250);
        lblFinanceSummary.Text = "Gelir: 0,00 TL    •    Gider: 0,00 TL    •    Bakiye: 0,00 TL";
        bodydataGridOdeme.Controls.Add(lblFinanceSummary, 0, 1);
        bodydataGridOdeme.Controls.Add(listdataGridOdeme, 0, 2);
        bodydataGridOdeme.Dock = DockStyle.Fill;
        bodydataGridOdeme.AutoScroll = true;
        bodydataGridOdeme.AutoScrollMinSize = new Size(0, 340);
        bodydataGridOdeme.Location = new Point(0, 0);
        bodydataGridOdeme.Margin = new Padding(0);
        bodydataGridOdeme.Name = "bodydataGridOdeme";
        bodydataGridOdeme.RowCount = 3;
        bodydataGridOdeme.RowStyles.Add(new RowStyle(SizeType.Absolute, 154F));
        bodydataGridOdeme.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        bodydataGridOdeme.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        bodydataGridOdeme.Size = new Size(1112, 732);
        bodydataGridOdeme.TabIndex = 0;
        // 
        // editordataGridOdeme
        // 
        editordataGridOdeme.BackColor = Color.FromArgb(239, 247, 253);
        editordataGridOdeme.ColumnCount = 1;
        editordataGridOdeme.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        editordataGridOdeme.Controls.Add(fieldsdataGridOdeme, 0, 0);
        editordataGridOdeme.Controls.Add(actionsdataGridOdeme, 0, 1);
        editordataGridOdeme.Dock = DockStyle.Fill;
        editordataGridOdeme.Location = new Point(0, 0);
        editordataGridOdeme.Margin = new Padding(0);
        editordataGridOdeme.Name = "editordataGridOdeme";
        editordataGridOdeme.RowCount = 2;
        editordataGridOdeme.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        editordataGridOdeme.RowStyles.Add(new RowStyle());
        editordataGridOdeme.Size = new Size(1112, 154);
        editordataGridOdeme.TabIndex = 0;
        // 
        // fieldsdataGridOdeme
        // 
        fieldsdataGridOdeme.AutoScroll = true;
        fieldsdataGridOdeme.BackColor = Color.FromArgb(239, 247, 253);
        fieldsdataGridOdeme.ColumnCount = 3;
        fieldsdataGridOdeme.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        fieldsdataGridOdeme.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        fieldsdataGridOdeme.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        fieldsdataGridOdeme.Controls.Add(lbl_txtDescription, 0, 0);
        fieldsdataGridOdeme.Controls.Add(txtDescription, 0, 1);
        fieldsdataGridOdeme.Controls.Add(lbl_numericAmount, 1, 0);
        fieldsdataGridOdeme.Controls.Add(numericAmount, 1, 1);
        fieldsdataGridOdeme.Controls.Add(lbl_pnlTransactionType, 2, 0);
        fieldsdataGridOdeme.Controls.Add(pnlTransactionType, 2, 1);
        fieldsdataGridOdeme.Dock = DockStyle.Fill;
        fieldsdataGridOdeme.Location = new Point(0, 0);
        fieldsdataGridOdeme.Margin = new Padding(0);
        fieldsdataGridOdeme.Name = "fieldsdataGridOdeme";
        fieldsdataGridOdeme.Padding = new Padding(16);
        fieldsdataGridOdeme.RowCount = 3;
        fieldsdataGridOdeme.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
        fieldsdataGridOdeme.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        fieldsdataGridOdeme.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        fieldsdataGridOdeme.Size = new Size(1112, 99);
        fieldsdataGridOdeme.TabIndex = 0;
        // 
        // lbl_txtDescription
        // 
        lbl_txtDescription.AutoSize = true;
        lbl_txtDescription.ForeColor = Color.FromArgb(68, 87, 111);
        lbl_txtDescription.Location = new Point(16, 18);
        lbl_txtDescription.Margin = new Padding(0, 2, 12, 6);
        lbl_txtDescription.Name = "lbl_txtDescription";
        lbl_txtDescription.Size = new Size(63, 18);
        lbl_txtDescription.TabIndex = 0;
        lbl_txtDescription.Text = "Açıklama";
        // 
        // txtDescription
        // 
        txtDescription.BorderStyle = BorderStyle.FixedSingle;
        txtDescription.Dock = DockStyle.Fill;
        txtDescription.Font = new Font("Segoe UI", 10F);
        txtDescription.ForeColor = Color.FromArgb(37, 54, 75);
        txtDescription.Location = new Point(16, 42);
        txtDescription.Margin = new Padding(0, 0, 16, 10);
        txtDescription.Name = "txtDescription";
        txtDescription.PlaceholderText = "Açıklama";
        txtDescription.Size = new Size(344, 25);
        txtDescription.TabIndex = 100;
        // 
        // lbl_numericAmount
        // 
        lbl_numericAmount.AutoSize = true;
        lbl_numericAmount.ForeColor = Color.FromArgb(68, 87, 111);
        lbl_numericAmount.Location = new Point(376, 18);
        lbl_numericAmount.Margin = new Padding(0, 2, 12, 6);
        lbl_numericAmount.Name = "lbl_numericAmount";
        lbl_numericAmount.Size = new Size(61, 18);
        lbl_numericAmount.TabIndex = 101;
        lbl_numericAmount.Text = "Tutar (₺)";
        // 
        // numericAmount
        // 
        numericAmount.DecimalPlaces = 2;
        numericAmount.Dock = DockStyle.Fill;
        numericAmount.Font = new Font("Segoe UI", 10F);
        numericAmount.ForeColor = Color.FromArgb(37, 54, 75);
        numericAmount.Location = new Point(376, 42);
        numericAmount.Margin = new Padding(0, 0, 16, 10);
        numericAmount.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        numericAmount.Name = "numericAmount";
        numericAmount.Size = new Size(344, 25);
        numericAmount.TabIndex = 102;
        numericAmount.TextAlign = HorizontalAlignment.Right;
        numericAmount.ThousandsSeparator = true;
        // 
        // lbl_pnlTransactionType
        // 
        lbl_pnlTransactionType.AutoSize = true;
        lbl_pnlTransactionType.ForeColor = Color.FromArgb(68, 87, 111);
        lbl_pnlTransactionType.Location = new Point(736, 18);
        lbl_pnlTransactionType.Margin = new Padding(0, 2, 12, 6);
        lbl_pnlTransactionType.Name = "lbl_pnlTransactionType";
        lbl_pnlTransactionType.Size = new Size(71, 18);
        lbl_pnlTransactionType.TabIndex = 103;
        lbl_pnlTransactionType.Text = "İşlem türü";
        // 
        // pnlTransactionType
        // 
        pnlTransactionType.Controls.Add(radioIncome);
        pnlTransactionType.Controls.Add(radioExpense);
        pnlTransactionType.Dock = DockStyle.Fill;
        pnlTransactionType.Location = new Point(739, 45);
        pnlTransactionType.Name = "pnlTransactionType";
        pnlTransactionType.Size = new Size(354, 38);
        pnlTransactionType.TabIndex = 104;
        // 
        // radioIncome
        // 
        radioIncome.Anchor = AnchorStyles.Left;
        radioIncome.AutoSize = true;
        radioIncome.Location = new Point(0, 3);
        radioIncome.Margin = new Padding(0, 3, 16, 6);
        radioIncome.Name = "radioIncome";
        radioIncome.Size = new Size(55, 23);
        radioIncome.TabIndex = 0;
        radioIncome.Text = "Gelir";
        // 
        // radioExpense
        // 
        radioExpense.Anchor = AnchorStyles.Left;
        radioExpense.AutoSize = true;
        radioExpense.Location = new Point(71, 3);
        radioExpense.Margin = new Padding(0, 3, 16, 6);
        radioExpense.Name = "radioExpense";
        radioExpense.Size = new Size(60, 23);
        radioExpense.TabIndex = 1;
        radioExpense.Text = "Gider";
        // 
        // actionsdataGridOdeme
        // 
        actionsdataGridOdeme.AutoSize = true;
        actionsdataGridOdeme.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        actionsdataGridOdeme.BackColor = Color.FromArgb(239, 247, 253);
        actionsdataGridOdeme.Controls.Add(btnAddIncomeExpense);
        actionsdataGridOdeme.Controls.Add(FaturaBtn);
        actionsdataGridOdeme.Dock = DockStyle.Top;
        actionsdataGridOdeme.Location = new Point(0, 99);
        actionsdataGridOdeme.Margin = new Padding(0);
        actionsdataGridOdeme.Name = "actionsdataGridOdeme";
        actionsdataGridOdeme.Padding = new Padding(16, 8, 8, 8);
        actionsdataGridOdeme.Size = new Size(1112, 55);
        actionsdataGridOdeme.TabIndex = 1;
        // 
        // btnAddIncomeExpense
        // 
        btnAddIncomeExpense.AutoSize = true;
        btnAddIncomeExpense.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        btnAddIncomeExpense.BackColor = Color.FromArgb(246, 248, 251);
        btnAddIncomeExpense.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        btnAddIncomeExpense.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        btnAddIncomeExpense.FlatStyle = FlatStyle.Flat;
        btnAddIncomeExpense.ForeColor = Color.FromArgb(37, 54, 75);
        btnAddIncomeExpense.Location = new Point(16, 8);
        btnAddIncomeExpense.Margin = new Padding(0, 0, 8, 0);
        btnAddIncomeExpense.MinimumSize = new Size(96, 34);
        btnAddIncomeExpense.Name = "btnAddIncomeExpense";
        btnAddIncomeExpense.Padding = new Padding(12, 4, 12, 4);
        btnAddIncomeExpense.Size = new Size(96, 39);
        btnAddIncomeExpense.TabIndex = 0;
        btnAddIncomeExpense.Text = "Kaydet";
        btnAddIncomeExpense.UseVisualStyleBackColor = false;
        btnAddIncomeExpense.Click += btnAddIncomeExpense_Click;
        // 
        // FaturaBtn
        // 
        FaturaBtn.AutoSize = true;
        FaturaBtn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        FaturaBtn.BackColor = Color.FromArgb(246, 248, 251);
        FaturaBtn.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        FaturaBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        FaturaBtn.FlatStyle = FlatStyle.Flat;
        FaturaBtn.ForeColor = Color.FromArgb(37, 54, 75);
        FaturaBtn.Location = new Point(120, 8);
        FaturaBtn.Margin = new Padding(0, 0, 8, 0);
        FaturaBtn.MinimumSize = new Size(96, 34);
        FaturaBtn.Name = "FaturaBtn";
        FaturaBtn.Padding = new Padding(12, 4, 12, 4);
        FaturaBtn.Size = new Size(99, 39);
        FaturaBtn.TabIndex = 1;
        FaturaBtn.Text = "Faturalar";
        FaturaBtn.UseVisualStyleBackColor = false;
        FaturaBtn.Click += FaturaBtn_Click;
        // 
        // listdataGridOdeme
        // 
        listdataGridOdeme.BackColor = Color.FromArgb(239, 247, 253);
        listdataGridOdeme.ColumnCount = 1;
        listdataGridOdeme.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        listdataGridOdeme.Controls.Add(listdataGridOdemeToolbar, 0, 0);
        listdataGridOdeme.Controls.Add(dataGridOdeme, 0, 1);
        listdataGridOdeme.Controls.Add(listdataGridOdemeCount, 0, 2);
        listdataGridOdeme.Dock = DockStyle.Fill;
        listdataGridOdeme.Location = new Point(0, 154);
        listdataGridOdeme.Margin = new Padding(0);
        listdataGridOdeme.Name = "listdataGridOdeme";
        listdataGridOdeme.Padding = new Padding(8);
        listdataGridOdeme.RowCount = 3;
        listdataGridOdeme.RowStyles.Add(new RowStyle());
        listdataGridOdeme.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        listdataGridOdeme.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        listdataGridOdeme.Size = new Size(1112, 578);
        listdataGridOdeme.TabIndex = 1;
        // 
        // listdataGridOdemeToolbar
        // 
        listdataGridOdemeToolbar.AutoSize = true;
        listdataGridOdemeToolbar.BackColor = Color.FromArgb(218, 236, 250);
        listdataGridOdemeToolbar.ColumnCount = 4;
        listdataGridOdemeToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        listdataGridOdemeToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
        listdataGridOdemeToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        listdataGridOdemeToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        listdataGridOdemeToolbar.Controls.Add(listdataGridOdemeTitle, 0, 0);
        listdataGridOdemeToolbar.Controls.Add(txtFinanceSearch, 1, 0);
        listdataGridOdemeToolbar.Controls.Add(listdataGridOdemeClear, 2, 0);
        listdataGridOdemeToolbar.Controls.Add(listdataGridOdemeColumns, 3, 0);
        listdataGridOdemeToolbar.Dock = DockStyle.Top;
        listdataGridOdemeToolbar.Location = new Point(8, 8);
        listdataGridOdemeToolbar.Margin = new Padding(0);
        listdataGridOdemeToolbar.Name = "listdataGridOdemeToolbar";
        listdataGridOdemeToolbar.RowCount = 1;
        listdataGridOdemeToolbar.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        listdataGridOdemeToolbar.Size = new Size(1096, 42);
        listdataGridOdemeToolbar.TabIndex = 0;
        // 
        // listdataGridOdemeTitle
        // 
        listdataGridOdemeTitle.AutoEllipsis = true;
        listdataGridOdemeTitle.Dock = DockStyle.Fill;
        listdataGridOdemeTitle.Font = new Font("Segoe UI Semibold", 11F);
        listdataGridOdemeTitle.ForeColor = Color.FromArgb(68, 87, 111);
        listdataGridOdemeTitle.Location = new Point(0, 2);
        listdataGridOdemeTitle.Margin = new Padding(0, 2, 12, 6);
        listdataGridOdemeTitle.Name = "listdataGridOdemeTitle";
        listdataGridOdemeTitle.Size = new Size(662, 34);
        listdataGridOdemeTitle.TabIndex = 0;
        listdataGridOdemeTitle.Text = "Gelir / Gider";
        // 
        // txtFinanceSearch
        // 
        txtFinanceSearch.BorderStyle = BorderStyle.FixedSingle;
        txtFinanceSearch.Dock = DockStyle.Fill;
        txtFinanceSearch.Font = new Font("Segoe UI", 10F);
        txtFinanceSearch.ForeColor = Color.FromArgb(37, 54, 75);
        txtFinanceSearch.Location = new Point(674, 4);
        txtFinanceSearch.Margin = new Padding(0, 4, 8, 6);
        txtFinanceSearch.Name = "txtFinanceSearch";
        txtFinanceSearch.PlaceholderText = "Listede ara (Ctrl+F)";
        txtFinanceSearch.Size = new Size(222, 25);
        txtFinanceSearch.TabIndex = 95;
        // 
        // listdataGridOdemeClear
        // 
        listdataGridOdemeClear.AutoSize = true;
        listdataGridOdemeClear.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        listdataGridOdemeClear.BackColor = Color.FromArgb(246, 248, 251);
        listdataGridOdemeClear.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        listdataGridOdemeClear.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        listdataGridOdemeClear.FlatStyle = FlatStyle.Flat;
        listdataGridOdemeClear.ForeColor = Color.FromArgb(37, 54, 75);
        listdataGridOdemeClear.Location = new Point(904, 0);
        listdataGridOdemeClear.Margin = new Padding(0, 0, 8, 0);
        listdataGridOdemeClear.MinimumSize = new Size(96, 34);
        listdataGridOdemeClear.Name = "listdataGridOdemeClear";
        listdataGridOdemeClear.Padding = new Padding(12, 4, 12, 4);
        listdataGridOdemeClear.Size = new Size(96, 39);
        listdataGridOdemeClear.TabIndex = 96;
        listdataGridOdemeClear.Text = "Temizle";
        listdataGridOdemeClear.UseVisualStyleBackColor = false;
        // 
        // listdataGridOdemeColumns
        // 
        listdataGridOdemeColumns.AutoSize = true;
        listdataGridOdemeColumns.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        listdataGridOdemeColumns.BackColor = Color.FromArgb(246, 248, 251);
        listdataGridOdemeColumns.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        listdataGridOdemeColumns.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        listdataGridOdemeColumns.FlatStyle = FlatStyle.Flat;
        listdataGridOdemeColumns.ForeColor = Color.FromArgb(37, 54, 75);
        listdataGridOdemeColumns.Location = new Point(996, 0);
        listdataGridOdemeColumns.Margin = new Padding(0, 0, 8, 0);
        listdataGridOdemeColumns.MinimumSize = new Size(96, 34);
        listdataGridOdemeColumns.Name = "listdataGridOdemeColumns";
        listdataGridOdemeColumns.Padding = new Padding(12, 4, 12, 4);
        listdataGridOdemeColumns.Size = new Size(96, 39);
        listdataGridOdemeColumns.TabIndex = 97;
        listdataGridOdemeColumns.Text = "Sütunlar";
        listdataGridOdemeColumns.UseVisualStyleBackColor = false;
        // 
        // dataGridOdeme
        // 
        dataGridOdeme.AllowUserToAddRows = false;
        dataGridOdeme.AllowUserToDeleteRows = false;
        dataGridOdeme.AllowUserToOrderColumns = true;
        dataGridOdeme.AllowUserToResizeRows = false;
        dataGridViewCellStyle16.BackColor = Color.FromArgb(239, 247, 253);
        dataGridOdeme.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
        dataGridOdeme.BackgroundColor = Color.FromArgb(239, 247, 253);
        dataGridOdeme.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle17.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle17.BackColor = Color.FromArgb(218, 236, 250);
        dataGridViewCellStyle17.Font = new Font("Segoe UI Semibold", 9.5F);
        dataGridViewCellStyle17.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle17.SelectionBackColor = Color.FromArgb(218, 236, 250);
        dataGridViewCellStyle17.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle17.WrapMode = DataGridViewTriState.True;
        dataGridOdeme.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
        dataGridOdeme.ColumnHeadersHeight = 36;
        dataGridViewCellStyle18.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle18.BackColor = SystemColors.Window;
        dataGridViewCellStyle18.Font = new Font("Segoe UI", 9.5F);
        dataGridViewCellStyle18.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle18.SelectionBackColor = Color.FromArgb(200, 224, 246);
        dataGridViewCellStyle18.SelectionForeColor = Color.FromArgb(37, 54, 75);
        dataGridViewCellStyle18.WrapMode = DataGridViewTriState.False;
        dataGridOdeme.DefaultCellStyle = dataGridViewCellStyle18;
        dataGridOdeme.Dock = DockStyle.Fill;
        dataGridOdeme.EnableHeadersVisualStyles = false;
        dataGridOdeme.Location = new Point(11, 53);
        dataGridOdeme.MultiSelect = false;
        dataGridOdeme.Name = "dataGridOdeme";
        dataGridOdeme.ReadOnly = true;
        dataGridOdeme.RowHeadersWidth = 44;
        dataGridOdeme.RowTemplate.Height = 32;
        dataGridOdeme.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridOdeme.Size = new Size(1090, 486);
        dataGridOdeme.TabIndex = 1;
        // 
        // listdataGridOdemeCount
        // 
        listdataGridOdemeCount.AutoSize = true;
        listdataGridOdemeCount.Dock = DockStyle.Fill;
        listdataGridOdemeCount.ForeColor = Color.FromArgb(68, 87, 111);
        listdataGridOdemeCount.Location = new Point(8, 544);
        listdataGridOdemeCount.Margin = new Padding(0, 2, 12, 6);
        listdataGridOdemeCount.Name = "listdataGridOdemeCount";
        listdataGridOdemeCount.Size = new Size(1084, 20);
        listdataGridOdemeCount.TabIndex = 2;
        listdataGridOdemeCount.Text = "Kayıt: 0";
        // 
        // tabPageOzelRaporlar
        // 
        tabPageOzelRaporlar.BackColor = Color.FromArgb(239, 247, 253);
        tabPageOzelRaporlar.Controls.Add(listsalesGrid);
        tabPageOzelRaporlar.Location = new Point(4, 24);
        tabPageOzelRaporlar.Name = "tabPageOzelRaporlar";
        tabPageOzelRaporlar.Size = new Size(1112, 732);
        tabPageOzelRaporlar.TabIndex = 6;
        tabPageOzelRaporlar.Text = "Raporlar";
        tabPageOzelRaporlar.Click += tabPage1_Click;
        // 
        // listsalesGrid
        // 
        listsalesGrid.BackColor = Color.FromArgb(239, 247, 253);
        listsalesGrid.ColumnCount = 1;
        listsalesGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        listsalesGrid.Controls.Add(listsalesGridToolbar, 0, 0);
        listsalesGrid.Controls.Add(salesGrid, 0, 1);
        listsalesGrid.Controls.Add(listsalesGridCount, 0, 2);
        listsalesGrid.Dock = DockStyle.Fill;
        listsalesGrid.Location = new Point(0, 0);
        listsalesGrid.Margin = new Padding(0);
        listsalesGrid.Name = "listsalesGrid";
        listsalesGrid.Padding = new Padding(8);
        listsalesGrid.RowCount = 3;
        listsalesGrid.RowStyles.Add(new RowStyle());
        listsalesGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        listsalesGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        listsalesGrid.Size = new Size(1112, 732);
        listsalesGrid.TabIndex = 0;
        // 
        // listsalesGridToolbar
        // 
        listsalesGridToolbar.AutoSize = true;
        listsalesGridToolbar.BackColor = Color.FromArgb(218, 236, 250);
        listsalesGridToolbar.ColumnCount = 4;
        listsalesGridToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        listsalesGridToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
        listsalesGridToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        listsalesGridToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        listsalesGridToolbar.Controls.Add(listsalesGridTitle, 0, 0);
        listsalesGridToolbar.Controls.Add(txtReportsSearch, 1, 0);
        listsalesGridToolbar.Controls.Add(listsalesGridClear, 2, 0);
        listsalesGridToolbar.Controls.Add(listsalesGridColumns, 3, 0);
        listsalesGridToolbar.Dock = DockStyle.Top;
        listsalesGridToolbar.Location = new Point(8, 8);
        listsalesGridToolbar.Margin = new Padding(0);
        listsalesGridToolbar.Name = "listsalesGridToolbar";
        listsalesGridToolbar.RowCount = 1;
        listsalesGridToolbar.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        listsalesGridToolbar.Size = new Size(1096, 42);
        listsalesGridToolbar.TabIndex = 0;
        // 
        // listsalesGridTitle
        // 
        listsalesGridTitle.AutoEllipsis = true;
        listsalesGridTitle.Dock = DockStyle.Fill;
        listsalesGridTitle.Font = new Font("Segoe UI Semibold", 11F);
        listsalesGridTitle.ForeColor = Color.FromArgb(68, 87, 111);
        listsalesGridTitle.Location = new Point(0, 2);
        listsalesGridTitle.Margin = new Padding(0, 2, 12, 6);
        listsalesGridTitle.Name = "listsalesGridTitle";
        listsalesGridTitle.Size = new Size(662, 34);
        listsalesGridTitle.TabIndex = 0;
        listsalesGridTitle.Text = "Raporlar";
        // 
        // txtReportsSearch
        // 
        txtReportsSearch.BorderStyle = BorderStyle.FixedSingle;
        txtReportsSearch.Dock = DockStyle.Fill;
        txtReportsSearch.Font = new Font("Segoe UI", 10F);
        txtReportsSearch.ForeColor = Color.FromArgb(37, 54, 75);
        txtReportsSearch.Location = new Point(674, 4);
        txtReportsSearch.Margin = new Padding(0, 4, 8, 6);
        txtReportsSearch.Name = "txtReportsSearch";
        txtReportsSearch.PlaceholderText = "Listede ara (Ctrl+F)";
        txtReportsSearch.Size = new Size(222, 25);
        txtReportsSearch.TabIndex = 115;
        // 
        // listsalesGridClear
        // 
        listsalesGridClear.AutoSize = true;
        listsalesGridClear.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        listsalesGridClear.BackColor = Color.FromArgb(246, 248, 251);
        listsalesGridClear.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        listsalesGridClear.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        listsalesGridClear.FlatStyle = FlatStyle.Flat;
        listsalesGridClear.ForeColor = Color.FromArgb(37, 54, 75);
        listsalesGridClear.Location = new Point(904, 0);
        listsalesGridClear.Margin = new Padding(0, 0, 8, 0);
        listsalesGridClear.MinimumSize = new Size(96, 34);
        listsalesGridClear.Name = "listsalesGridClear";
        listsalesGridClear.Padding = new Padding(12, 4, 12, 4);
        listsalesGridClear.Size = new Size(96, 39);
        listsalesGridClear.TabIndex = 116;
        listsalesGridClear.Text = "Temizle";
        listsalesGridClear.UseVisualStyleBackColor = false;
        // 
        // listsalesGridColumns
        // 
        listsalesGridColumns.AutoSize = true;
        listsalesGridColumns.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        listsalesGridColumns.BackColor = Color.FromArgb(246, 248, 251);
        listsalesGridColumns.FlatAppearance.BorderColor = Color.FromArgb(189, 214, 235);
        listsalesGridColumns.FlatAppearance.MouseOverBackColor = Color.FromArgb(221, 237, 253);
        listsalesGridColumns.FlatStyle = FlatStyle.Flat;
        listsalesGridColumns.ForeColor = Color.FromArgb(37, 54, 75);
        listsalesGridColumns.Location = new Point(996, 0);
        listsalesGridColumns.Margin = new Padding(0, 0, 8, 0);
        listsalesGridColumns.MinimumSize = new Size(96, 34);
        listsalesGridColumns.Name = "listsalesGridColumns";
        listsalesGridColumns.Padding = new Padding(12, 4, 12, 4);
        listsalesGridColumns.Size = new Size(96, 39);
        listsalesGridColumns.TabIndex = 117;
        listsalesGridColumns.Text = "Sütunlar";
        listsalesGridColumns.UseVisualStyleBackColor = false;
        // 
        // salesGrid
        // 
        salesGrid.AllowDrop = true;
        salesGrid.AllowUserToAddRows = false;
        salesGrid.AllowUserToDeleteRows = false;
        salesGrid.AllowUserToOrderColumns = true;
        salesGrid.AllowUserToResizeRows = false;
        dataGridViewCellStyle19.BackColor = Color.FromArgb(239, 247, 253);
        salesGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
        salesGrid.BackgroundColor = Color.FromArgb(239, 247, 253);
        salesGrid.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle20.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle20.BackColor = Color.FromArgb(218, 236, 250);
        dataGridViewCellStyle20.Font = new Font("Segoe UI Semibold", 9.5F);
        dataGridViewCellStyle20.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle20.SelectionBackColor = Color.FromArgb(218, 236, 250);
        dataGridViewCellStyle20.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle20.WrapMode = DataGridViewTriState.True;
        salesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
        salesGrid.ColumnHeadersHeight = 36;
        dataGridViewCellStyle21.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle21.BackColor = SystemColors.Window;
        dataGridViewCellStyle21.Font = new Font("Segoe UI", 9.5F);
        dataGridViewCellStyle21.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle21.SelectionBackColor = Color.FromArgb(200, 224, 246);
        dataGridViewCellStyle21.SelectionForeColor = Color.FromArgb(37, 54, 75);
        dataGridViewCellStyle21.WrapMode = DataGridViewTriState.False;
        salesGrid.DefaultCellStyle = dataGridViewCellStyle21;
        salesGrid.Dock = DockStyle.Fill;
        salesGrid.EnableHeadersVisualStyles = false;
        salesGrid.Location = new Point(11, 53);
        salesGrid.MultiSelect = false;
        salesGrid.Name = "salesGrid";
        salesGrid.ReadOnly = true;
        salesGrid.RowHeadersWidth = 44;
        salesGrid.RowTemplate.Height = 32;
        salesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        salesGrid.Size = new Size(1090, 640);
        salesGrid.TabIndex = 1;
        salesGrid.CellContentClick += salesGrid_CellContentClick;
        salesGrid.CellDoubleClick += salesGrid_CellDoubleClick;
        // 
        // listsalesGridCount
        // 
        listsalesGridCount.AutoSize = true;
        listsalesGridCount.Dock = DockStyle.Fill;
        listsalesGridCount.ForeColor = Color.FromArgb(68, 87, 111);
        listsalesGridCount.Location = new Point(8, 698);
        listsalesGridCount.Margin = new Padding(0, 2, 12, 6);
        listsalesGridCount.Name = "listsalesGridCount";
        listsalesGridCount.Size = new Size(1084, 20);
        listsalesGridCount.TabIndex = 2;
        listsalesGridCount.Text = "Kayıt: 0";
        // 
        // _homePage
        // 
        _homePage.BackColor = Color.FromArgb(239, 247, 253);
        _homePage.Controls.Add(pnlHome);
        _homePage.Location = new Point(4, 24);
        _homePage.Name = "_homePage";
        _homePage.Size = new Size(1112, 732);
        _homePage.TabIndex = 7;
        _homePage.Text = "Ana Sayfa";
        // 
        // pnlHome
        // 
        pnlHome.BackColor = Color.FromArgb(239, 247, 253);
        pnlHome.ColumnCount = 1;
        pnlHome.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        pnlHome.Controls.Add(lblHomeTitle, 0, 0);
        pnlHome.Controls.Add(lblHomeSubtitle, 0, 1);
        pnlHome.Controls.Add(flpDashboardCards, 0, 2);
        pnlHome.Dock = DockStyle.Fill;
        pnlHome.Location = new Point(0, 0);
        pnlHome.Margin = new Padding(0);
        pnlHome.Name = "pnlHome";
        pnlHome.Padding = new Padding(16);
        pnlHome.RowCount = 3;
        pnlHome.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
        pnlHome.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        pnlHome.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        pnlHome.Size = new Size(1112, 732);
        pnlHome.TabIndex = 0;
        // 
        // lblHomeTitle
        // 
        lblHomeTitle.AutoSize = true;
        lblHomeTitle.Font = new Font("Segoe UI Semibold", 17F);
        lblHomeTitle.ForeColor = Color.FromArgb(68, 87, 111);
        lblHomeTitle.Location = new Point(16, 18);
        lblHomeTitle.Margin = new Padding(0, 2, 12, 6);
        lblHomeTitle.Name = "lblHomeTitle";
        lblHomeTitle.Size = new Size(283, 31);
        lblHomeTitle.TabIndex = 0;
        lblHomeTitle.Text = "Anaokulu Yönetim Sistemi";
        // 
        // lblHomeSubtitle
        // 
        lblHomeSubtitle.AutoSize = true;
        lblHomeSubtitle.Dock = DockStyle.Fill;
        lblHomeSubtitle.ForeColor = Color.FromArgb(68, 87, 111);
        lblHomeSubtitle.Location = new Point(16, 64);
        lblHomeSubtitle.Margin = new Padding(0, 2, 12, 6);
        lblHomeSubtitle.Name = "lblHomeSubtitle";
        lblHomeSubtitle.Size = new Size(1068, 48);
        lblHomeSubtitle.TabIndex = 1;
        lblHomeSubtitle.Text = "Çalışmak istediğiniz bölümü seçin.";
        // 
        // flpDashboardCards
        // 
        flpDashboardCards.AutoScroll = true;
        flpDashboardCards.Dock = DockStyle.Fill;
        flpDashboardCards.FlowDirection = FlowDirection.TopDown;
        flpDashboardCards.Location = new Point(19, 121);
        flpDashboardCards.Name = "flpDashboardCards";
        flpDashboardCards.Size = new Size(1074, 592);
        flpDashboardCards.TabIndex = 2;
        flpDashboardCards.WrapContents = false;
        // 
        // timer1
        // 
        timer1.Enabled = true;
        // 
        // Form2
        // 
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BackColor = Color.FromArgb(228, 241, 252);
        ClientSize = new Size(1120, 760);
        Controls.Add(tabControl);
        Font = new Font("Segoe UI", 10F);
        KeyPreview = true;
        MinimumSize = new Size(720, 520);
        Name = "Form2";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Anaokulu Yönetim Sistemi";
        FormClosing += Form2_FormClosing;
        Load += Form2_Load;
        tabControl.ResumeLayout(false);
        tabPageStok.ResumeLayout(false);
        listdataGridViewStok.ResumeLayout(false);
        listdataGridViewStok.PerformLayout();
        listdataGridViewStokToolbar.ResumeLayout(false);
        listdataGridViewStokToolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridViewStok).EndInit();
        contextMenuStrip1.ResumeLayout(false);
        tabPagePersonelYonetimi.ResumeLayout(false);
        listdgvPersonelYonetimi.ResumeLayout(false);
        listdgvPersonelYonetimi.PerformLayout();
        listdgvPersonelYonetimiToolbar.ResumeLayout(false);
        listdgvPersonelYonetimiToolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvPersonelYonetimi).EndInit();
        tabPageOgrenciOnKayit.ResumeLayout(false);
        bodydgvOnKayitlar.ResumeLayout(false);
        editordgvOnKayitlar.ResumeLayout(false);
        editordgvOnKayitlar.PerformLayout();
        fieldsdgvOnKayitlar.ResumeLayout(false);
        fieldsdgvOnKayitlar.PerformLayout();
        actionsdgvOnKayitlar.ResumeLayout(false);
        actionsdgvOnKayitlar.PerformLayout();
        listdgvOnKayitlar.ResumeLayout(false);
        listdgvOnKayitlar.PerformLayout();
        listdgvOnKayitlarToolbar.ResumeLayout(false);
        listdgvOnKayitlarToolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvOnKayitlar).EndInit();
        _classesPage.ResumeLayout(false);
        bodyDgvOgrenciYonetimiSiniflar.ResumeLayout(false);
        editorDgvOgrenciYonetimiSiniflar.ResumeLayout(false);
        editorDgvOgrenciYonetimiSiniflar.PerformLayout();
        fieldsDgvOgrenciYonetimiSiniflar.ResumeLayout(false);
        fieldsDgvOgrenciYonetimiSiniflar.PerformLayout();
        actionsDgvOgrenciYonetimiSiniflar.ResumeLayout(false);
        actionsDgvOgrenciYonetimiSiniflar.PerformLayout();
        listDgvOgrenciYonetimiSiniflar.ResumeLayout(false);
        listDgvOgrenciYonetimiSiniflar.PerformLayout();
        listDgvOgrenciYonetimiSiniflarToolbar.ResumeLayout(false);
        listDgvOgrenciYonetimiSiniflarToolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)DgvOgrenciYonetimiSiniflar).EndInit();
        tabPageSatis.ResumeLayout(false);
        bodydataOgrVw.ResumeLayout(false);
        editordataOgrVw.ResumeLayout(false);
        editordataOgrVw.PerformLayout();
        fieldsdataOgrVw.ResumeLayout(false);
        fieldsdataOgrVw.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numericQuantitySold).EndInit();
        actionsdataOgrVw.ResumeLayout(false);
        actionsdataOgrVw.PerformLayout();
        listdataOgrVw.ResumeLayout(false);
        listdataOgrVw.PerformLayout();
        listdataOgrVwToolbar.ResumeLayout(false);
        listdataOgrVwToolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dataOgrVw).EndInit();
        tabPageGelirGider.ResumeLayout(false);
        bodydataGridOdeme.ResumeLayout(false);
        editordataGridOdeme.ResumeLayout(false);
        editordataGridOdeme.PerformLayout();
        fieldsdataGridOdeme.ResumeLayout(false);
        fieldsdataGridOdeme.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numericAmount).EndInit();
        pnlTransactionType.ResumeLayout(false);
        pnlTransactionType.PerformLayout();
        actionsdataGridOdeme.ResumeLayout(false);
        actionsdataGridOdeme.PerformLayout();
        listdataGridOdeme.ResumeLayout(false);
        listdataGridOdeme.PerformLayout();
        listdataGridOdemeToolbar.ResumeLayout(false);
        listdataGridOdemeToolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridOdeme).EndInit();
        tabPageOzelRaporlar.ResumeLayout(false);
        listsalesGrid.ResumeLayout(false);
        listsalesGrid.PerformLayout();
        listsalesGridToolbar.ResumeLayout(false);
        listsalesGridToolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)salesGrid).EndInit();
        _homePage.ResumeLayout(false);
        pnlHome.ResumeLayout(false);
        pnlHome.PerformLayout();
        ResumeLayout(false);
    }
    #endregion
}
