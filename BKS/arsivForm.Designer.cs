namespace BKS
{
    partial class arsivForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblHeaderBadge;
        private System.Windows.Forms.FlowLayoutPanel summaryPanel;
        private System.Windows.Forms.Panel pnlTotalCard;
        private System.Windows.Forms.Label lblTotalTitle;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Panel pnlSelectedCard;
        private System.Windows.Forms.Label lblSelectedTitle;
        private System.Windows.Forms.Label lblSelectedValue;
        private System.Windows.Forms.Panel pnlLastCard;
        private System.Windows.Forms.Label lblLastTitle;
        private System.Windows.Forms.Label lblLastValue;
        private System.Windows.Forms.GroupBox grpUpload;
        private System.Windows.Forms.TableLayoutPanel uploadLayout;
        private System.Windows.Forms.Label lblOgrenci;
        private System.Windows.Forms.ComboBox cmbOgrenciler;
        private System.Windows.Forms.Label lblDosya;
        private System.Windows.Forms.TextBox txtDosyaYolu;
        private System.Windows.Forms.Button btnDosyaSec;
        private System.Windows.Forms.Button btnYukle;
        private System.Windows.Forms.Label lblDropHint;
        private System.Windows.Forms.GroupBox grpFilters;
        private System.Windows.Forms.TableLayoutPanel filterLayout;
        private System.Windows.Forms.Label lblAra;
        private System.Windows.Forms.TextBox txtAra;
        private System.Windows.Forms.Label lblTur;
        private System.Windows.Forms.ComboBox cmbTur;
        private System.Windows.Forms.CheckBox chkTarih;
        private System.Windows.Forms.DateTimePicker dtBaslangic;
        private System.Windows.Forms.DateTimePicker dtBitis;
        private System.Windows.Forms.Button btnFiltreTemizle;
        private System.Windows.Forms.Button btnYenile;
        private System.Windows.Forms.DataGridView dgvDosyalar;
        private System.Windows.Forms.FlowLayoutPanel actionPanel;
        private System.Windows.Forms.Button btnIndir;
        private System.Windows.Forms.Button btnMasaustuIndir;
        private System.Windows.Forms.Button btnKopyala;
        private System.Windows.Forms.ContextMenuStrip dgvContextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuIndir;
        private System.Windows.Forms.ToolStripMenuItem menuMasaustuneIndir;
        private System.Windows.Forms.ToolStripMenuItem menuDosyaAdiniKopyala;
        private System.Windows.Forms.ToolStripSeparator menuSeparator;
        private System.Windows.Forms.ToolStripMenuItem menuYenile;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar progressBar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.DataGridViewTextBoxColumn colDosyaAdi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDosyaTipi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUzanti;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEklenme;
        private System.Windows.Forms.Button btnColumns;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var dataGridViewCellStyle1 = new DataGridViewCellStyle();
            var dataGridViewCellStyle3 = new DataGridViewCellStyle();
            var dataGridViewCellStyle2 = new DataGridViewCellStyle();
            rootLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            lblHeaderBadge = new Label();
            lblSubtitle = new Label();
            lblTitle = new Label();
            summaryPanel = new FlowLayoutPanel();
            pnlTotalCard = new Panel();
            lblTotalValue = new Label();
            lblTotalTitle = new Label();
            pnlSelectedCard = new Panel();
            lblSelectedValue = new Label();
            lblSelectedTitle = new Label();
            pnlLastCard = new Panel();
            lblLastValue = new Label();
            lblLastTitle = new Label();
            grpUpload = new GroupBox();
            uploadLayout = new TableLayoutPanel();
            lblOgrenci = new Label();
            cmbOgrenciler = new ComboBox();
            btnDosyaSec = new Button();
            btnYukle = new Button();
            lblDosya = new Label();
            txtDosyaYolu = new TextBox();
            lblDropHint = new Label();
            grpFilters = new GroupBox();
            filterLayout = new TableLayoutPanel();
            lblAra = new Label();
            txtAra = new TextBox();
            lblTur = new Label();
            cmbTur = new ComboBox();
            chkTarih = new CheckBox();
            dtBaslangic = new DateTimePicker();
            dtBitis = new DateTimePicker();
            btnFiltreTemizle = new Button();
            btnYenile = new Button();
            dgvDosyalar = new DataGridView();
            colDosyaAdi = new DataGridViewTextBoxColumn();
            colDosyaTipi = new DataGridViewTextBoxColumn();
            colUzanti = new DataGridViewTextBoxColumn();
            colEklenme = new DataGridViewTextBoxColumn();
            dgvContextMenu = new ContextMenuStrip(components);
            menuIndir = new ToolStripMenuItem();
            menuMasaustuneIndir = new ToolStripMenuItem();
            menuDosyaAdiniKopyala = new ToolStripMenuItem();
            menuSeparator = new ToolStripSeparator();
            menuYenile = new ToolStripMenuItem();
            actionPanel = new FlowLayoutPanel();
            btnColumns = new Button();
            btnIndir = new Button();
            btnMasaustuIndir = new Button();
            btnKopyala = new Button();
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            progressBar = new ToolStripProgressBar();
            rootLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            summaryPanel.SuspendLayout();
            pnlTotalCard.SuspendLayout();
            pnlSelectedCard.SuspendLayout();
            pnlLastCard.SuspendLayout();
            grpUpload.SuspendLayout();
            uploadLayout.SuspendLayout();
            grpFilters.SuspendLayout();
            filterLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDosyalar).BeginInit();
            dgvContextMenu.SuspendLayout();
            actionPanel.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.AutoScroll = true;
            rootLayout.AutoScrollMinSize = new Size(960, 690);
            rootLayout.BackColor = Color.FromArgb(246, 248, 252);
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(headerPanel, 0, 0);
            rootLayout.Controls.Add(summaryPanel, 0, 1);
            rootLayout.Controls.Add(grpUpload, 0, 2);
            rootLayout.Controls.Add(grpFilters, 0, 3);
            rootLayout.Controls.Add(dgvDosyalar, 0, 4);
            rootLayout.Controls.Add(actionPanel, 0, 5);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(18, 16, 18, 8);
            rootLayout.RowCount = 6;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 104F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 86F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 126F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            rootLayout.Size = new Size(1184, 739);
            rootLayout.TabIndex = 0;
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(21, 32, 55);
            headerPanel.Controls.Add(lblHeaderBadge);
            headerPanel.Controls.Add(lblSubtitle);
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Location = new Point(21, 19);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new Padding(24, 18, 24, 18);
            headerPanel.Size = new Size(1142, 98);
            headerPanel.TabIndex = 0;
            // 
            // lblHeaderBadge
            // 
            lblHeaderBadge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblHeaderBadge.BackColor = Color.FromArgb(39, 174, 96);
            lblHeaderBadge.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblHeaderBadge.ForeColor = Color.White;
            lblHeaderBadge.Location = new Point(958, 22);
            lblHeaderBadge.Name = "lblHeaderBadge";
            lblHeaderBadge.Size = new Size(160, 30);
            lblHeaderBadge.TabIndex = 2;
            lblHeaderBadge.Text = "Dosya Arşivi";
            lblHeaderBadge.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSubtitle.Font = new Font("Segoe UI", 10.5F);
            lblSubtitle.ForeColor = Color.FromArgb(68, 87, 111);
            lblSubtitle.Location = new Point(26, 56);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(850, 25);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Öğrenci/personel dosyalarını tek ekrandan filtrele, yükle, indir ve yönet.";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(37, 54, 75);
            lblTitle.Location = new Point(24, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(261, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Akıllı Dosya Arşivi";
            // 
            // summaryPanel
            // 
            summaryPanel.AutoScroll = true;
            summaryPanel.BackColor = Color.Transparent;
            summaryPanel.Controls.Add(pnlTotalCard);
            summaryPanel.Controls.Add(pnlSelectedCard);
            summaryPanel.Controls.Add(pnlLastCard);
            summaryPanel.Dock = DockStyle.Fill;
            summaryPanel.Location = new Point(18, 120);
            summaryPanel.Margin = new Padding(0);
            summaryPanel.Name = "summaryPanel";
            summaryPanel.Padding = new Padding(2, 10, 2, 6);
            summaryPanel.Size = new Size(1148, 86);
            summaryPanel.TabIndex = 1;
            // 
            // pnlTotalCard
            // 
            pnlTotalCard.BackColor = Color.White;
            pnlTotalCard.Controls.Add(lblTotalValue);
            pnlTotalCard.Controls.Add(lblTotalTitle);
            pnlTotalCard.Location = new Point(8, 16);
            pnlTotalCard.Margin = new Padding(6);
            pnlTotalCard.Name = "pnlTotalCard";
            pnlTotalCard.Padding = new Padding(18, 10, 18, 10);
            pnlTotalCard.Size = new Size(260, 62);
            pnlTotalCard.TabIndex = 0;
            // 
            // lblTotalValue
            // 
            lblTotalValue.AutoSize = true;
            lblTotalValue.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblTotalValue.ForeColor = Color.FromArgb(37, 54, 75);
            lblTotalValue.Location = new Point(17, 25);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new Size(27, 32);
            lblTotalValue.TabIndex = 1;
            lblTotalValue.Text = "0";
            // 
            // lblTotalTitle
            // 
            lblTotalTitle.AutoSize = true;
            lblTotalTitle.Font = new Font("Segoe UI", 9F);
            lblTotalTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblTotalTitle.Location = new Point(18, 9);
            lblTotalTitle.Name = "lblTotalTitle";
            lblTotalTitle.Size = new Size(136, 15);
            lblTotalTitle.TabIndex = 0;
            lblTotalTitle.Text = "Görünen / toplam dosya";
            // 
            // pnlSelectedCard
            // 
            pnlSelectedCard.BackColor = Color.White;
            pnlSelectedCard.Controls.Add(lblSelectedValue);
            pnlSelectedCard.Controls.Add(lblSelectedTitle);
            pnlSelectedCard.Location = new Point(280, 16);
            pnlSelectedCard.Margin = new Padding(6);
            pnlSelectedCard.Name = "pnlSelectedCard";
            pnlSelectedCard.Padding = new Padding(18, 10, 18, 10);
            pnlSelectedCard.Size = new Size(360, 62);
            pnlSelectedCard.TabIndex = 1;
            // 
            // lblSelectedValue
            // 
            lblSelectedValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSelectedValue.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblSelectedValue.ForeColor = Color.FromArgb(37, 54, 75);
            lblSelectedValue.Location = new Point(18, 30);
            lblSelectedValue.Name = "lblSelectedValue";
            lblSelectedValue.Size = new Size(320, 23);
            lblSelectedValue.TabIndex = 1;
            lblSelectedValue.Text = "-";
            // 
            // lblSelectedTitle
            // 
            lblSelectedTitle.AutoSize = true;
            lblSelectedTitle.Font = new Font("Segoe UI", 9F);
            lblSelectedTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSelectedTitle.Location = new Point(18, 9);
            lblSelectedTitle.Name = "lblSelectedTitle";
            lblSelectedTitle.Size = new Size(60, 15);
            lblSelectedTitle.TabIndex = 0;
            lblSelectedTitle.Text = "Aktif kayıt";
            // 
            // pnlLastCard
            // 
            pnlLastCard.BackColor = Color.White;
            pnlLastCard.Controls.Add(lblLastValue);
            pnlLastCard.Controls.Add(lblLastTitle);
            pnlLastCard.Location = new Point(652, 16);
            pnlLastCard.Margin = new Padding(6);
            pnlLastCard.Name = "pnlLastCard";
            pnlLastCard.Padding = new Padding(18, 10, 18, 10);
            pnlLastCard.Size = new Size(260, 62);
            pnlLastCard.TabIndex = 2;
            // 
            // lblLastValue
            // 
            lblLastValue.AutoSize = true;
            lblLastValue.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblLastValue.ForeColor = Color.FromArgb(37, 54, 75);
            lblLastValue.Location = new Point(18, 30);
            lblLastValue.Name = "lblLastValue";
            lblLastValue.Size = new Size(16, 21);
            lblLastValue.TabIndex = 1;
            lblLastValue.Text = "-";
            // 
            // lblLastTitle
            // 
            lblLastTitle.AutoSize = true;
            lblLastTitle.Font = new Font("Segoe UI", 9F);
            lblLastTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblLastTitle.Location = new Point(18, 9);
            lblLastTitle.Name = "lblLastTitle";
            lblLastTitle.Size = new Size(98, 15);
            lblLastTitle.TabIndex = 0;
            lblLastTitle.Text = "Son eklenen tarih";
            // 
            // grpUpload
            // 
            grpUpload.Controls.Add(uploadLayout);
            grpUpload.Dock = DockStyle.Fill;
            grpUpload.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            grpUpload.ForeColor = Color.FromArgb(30, 41, 59);
            grpUpload.Location = new Point(21, 209);
            grpUpload.Name = "grpUpload";
            grpUpload.Padding = new Padding(14, 12, 14, 12);
            grpUpload.Size = new Size(1142, 120);
            grpUpload.TabIndex = 2;
            grpUpload.TabStop = false;
            grpUpload.Text = " Dosya yükleme alanı ";
            // 
            // uploadLayout
            // 
            uploadLayout.ColumnCount = 4;
            uploadLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 118F));
            uploadLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            uploadLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 138F));
            uploadLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            uploadLayout.Controls.Add(lblOgrenci, 0, 0);
            uploadLayout.Controls.Add(cmbOgrenciler, 1, 0);
            uploadLayout.Controls.Add(btnDosyaSec, 2, 0);
            uploadLayout.Controls.Add(btnYukle, 3, 0);
            uploadLayout.Controls.Add(lblDosya, 0, 1);
            uploadLayout.Controls.Add(txtDosyaYolu, 1, 1);
            uploadLayout.Controls.Add(lblDropHint, 2, 1);
            uploadLayout.Dock = DockStyle.Fill;
            uploadLayout.Location = new Point(14, 30);
            uploadLayout.Name = "uploadLayout";
            uploadLayout.RowCount = 2;
            uploadLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            uploadLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            uploadLayout.Size = new Size(1114, 78);
            uploadLayout.TabIndex = 0;
            // 
            // lblOgrenci
            // 
            lblOgrenci.AutoSize = true;
            lblOgrenci.Dock = DockStyle.Fill;
            lblOgrenci.Font = new Font("Segoe UI", 10F);
            lblOgrenci.ForeColor = Color.FromArgb(71, 85, 105);
            lblOgrenci.Location = new Point(3, 0);
            lblOgrenci.Name = "lblOgrenci";
            lblOgrenci.Size = new Size(112, 39);
            lblOgrenci.TabIndex = 0;
            lblOgrenci.Text = "Kayıt";
            lblOgrenci.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbOgrenciler
            // 
            cmbOgrenciler.Dock = DockStyle.Fill;
            cmbOgrenciler.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOgrenciler.Font = new Font("Segoe UI", 10F);
            cmbOgrenciler.FormattingEnabled = true;
            cmbOgrenciler.Location = new Point(121, 7);
            cmbOgrenciler.Margin = new Padding(3, 7, 12, 3);
            cmbOgrenciler.Name = "cmbOgrenciler";
            cmbOgrenciler.Size = new Size(723, 25);
            cmbOgrenciler.TabIndex = 1;
            // 
            // btnDosyaSec
            // 
            btnDosyaSec.BackColor = Color.FromArgb(71, 85, 105);
            btnDosyaSec.Dock = DockStyle.Fill;
            btnDosyaSec.FlatAppearance.BorderSize = 0;
            btnDosyaSec.FlatStyle = FlatStyle.Flat;
            btnDosyaSec.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnDosyaSec.ForeColor = Color.White;
            btnDosyaSec.Location = new Point(859, 5);
            btnDosyaSec.Margin = new Padding(3, 5, 12, 5);
            btnDosyaSec.Name = "btnDosyaSec";
            btnDosyaSec.Size = new Size(123, 29);
            btnDosyaSec.TabIndex = 2;
            btnDosyaSec.Text = "Dosya Seç";
            btnDosyaSec.UseVisualStyleBackColor = false;
            // 
            // btnYukle
            // 
            btnYukle.BackColor = Color.FromArgb(22, 163, 74);
            btnYukle.Dock = DockStyle.Fill;
            btnYukle.FlatAppearance.BorderSize = 0;
            btnYukle.FlatStyle = FlatStyle.Flat;
            btnYukle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnYukle.ForeColor = Color.White;
            btnYukle.Location = new Point(997, 5);
            btnYukle.Margin = new Padding(3, 5, 3, 5);
            btnYukle.Name = "btnYukle";
            btnYukle.Size = new Size(114, 29);
            btnYukle.TabIndex = 3;
            btnYukle.Text = "Yükle";
            btnYukle.UseVisualStyleBackColor = false;
            // 
            // lblDosya
            // 
            lblDosya.AutoSize = true;
            lblDosya.Dock = DockStyle.Fill;
            lblDosya.Font = new Font("Segoe UI", 10F);
            lblDosya.ForeColor = Color.FromArgb(71, 85, 105);
            lblDosya.Location = new Point(3, 39);
            lblDosya.Name = "lblDosya";
            lblDosya.Size = new Size(112, 39);
            lblDosya.TabIndex = 4;
            lblDosya.Text = "Seçilen dosya";
            lblDosya.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDosyaYolu
            // 
            txtDosyaYolu.Dock = DockStyle.Fill;
            txtDosyaYolu.Font = new Font("Segoe UI", 10F);
            txtDosyaYolu.Location = new Point(121, 46);
            txtDosyaYolu.Margin = new Padding(3, 7, 12, 3);
            txtDosyaYolu.Name = "txtDosyaYolu";
            txtDosyaYolu.ReadOnly = true;
            txtDosyaYolu.Size = new Size(723, 25);
            txtDosyaYolu.TabIndex = 5;
            // 
            // lblDropHint
            // 
            lblDropHint.AutoSize = true;
            uploadLayout.SetColumnSpan(lblDropHint, 2);
            lblDropHint.Dock = DockStyle.Fill;
            lblDropHint.Font = new Font("Segoe UI", 9F);
            lblDropHint.ForeColor = Color.FromArgb(100, 116, 139);
            lblDropHint.Location = new Point(859, 39);
            lblDropHint.Name = "lblDropHint";
            lblDropHint.Size = new Size(252, 39);
            lblDropHint.TabIndex = 6;
            lblDropHint.Text = "Dosyayı buraya sürükleyip bırakabilirsin.";
            lblDropHint.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpFilters
            // 
            grpFilters.Controls.Add(filterLayout);
            grpFilters.Dock = DockStyle.Fill;
            grpFilters.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            grpFilters.ForeColor = Color.FromArgb(30, 41, 59);
            grpFilters.Location = new Point(21, 335);
            grpFilters.Name = "grpFilters";
            grpFilters.Padding = new Padding(14, 12, 14, 12);
            grpFilters.Size = new Size(1142, 84);
            grpFilters.TabIndex = 3;
            grpFilters.TabStop = false;
            grpFilters.Text = " Arama ve filtreler ";
            // 
            // filterLayout
            // 
            filterLayout.ColumnCount = 9;
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 55F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 118F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
            filterLayout.Controls.Add(lblAra, 0, 0);
            filterLayout.Controls.Add(txtAra, 1, 0);
            filterLayout.Controls.Add(lblTur, 2, 0);
            filterLayout.Controls.Add(cmbTur, 3, 0);
            filterLayout.Controls.Add(chkTarih, 4, 0);
            filterLayout.Controls.Add(dtBaslangic, 5, 0);
            filterLayout.Controls.Add(dtBitis, 6, 0);
            filterLayout.Controls.Add(btnFiltreTemizle, 7, 0);
            filterLayout.Controls.Add(btnYenile, 8, 0);
            filterLayout.Dock = DockStyle.Fill;
            filterLayout.Location = new Point(14, 30);
            filterLayout.Name = "filterLayout";
            filterLayout.RowCount = 1;
            filterLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            filterLayout.Size = new Size(1114, 42);
            filterLayout.TabIndex = 0;
            // 
            // lblAra
            // 
            lblAra.AutoSize = true;
            lblAra.Dock = DockStyle.Fill;
            lblAra.Font = new Font("Segoe UI", 10F);
            lblAra.ForeColor = Color.FromArgb(71, 85, 105);
            lblAra.Location = new Point(3, 0);
            lblAra.Name = "lblAra";
            lblAra.Size = new Size(49, 42);
            lblAra.TabIndex = 0;
            lblAra.Text = "Ara";
            lblAra.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtAra
            // 
            txtAra.Dock = DockStyle.Fill;
            txtAra.Font = new Font("Segoe UI", 10F);
            txtAra.Location = new Point(58, 8);
            txtAra.Margin = new Padding(3, 8, 12, 3);
            txtAra.Name = "txtAra";
            txtAra.PlaceholderText = "Dosya adı veya uzantı ara...";
            txtAra.Size = new Size(276, 25);
            txtAra.TabIndex = 1;
            // 
            // lblTur
            // 
            lblTur.AutoSize = true;
            lblTur.Dock = DockStyle.Fill;
            lblTur.Font = new Font("Segoe UI", 10F);
            lblTur.ForeColor = Color.FromArgb(71, 85, 105);
            lblTur.Location = new Point(349, 0);
            lblTur.Name = "lblTur";
            lblTur.Size = new Size(34, 42);
            lblTur.TabIndex = 2;
            lblTur.Text = "Tür";
            lblTur.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbTur
            // 
            cmbTur.Dock = DockStyle.Fill;
            cmbTur.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTur.Font = new Font("Segoe UI", 10F);
            cmbTur.FormattingEnabled = true;
            cmbTur.Location = new Point(389, 8);
            cmbTur.Margin = new Padding(3, 8, 12, 3);
            cmbTur.Name = "cmbTur";
            cmbTur.Size = new Size(115, 25);
            cmbTur.TabIndex = 3;
            // 
            // chkTarih
            // 
            chkTarih.AutoSize = true;
            chkTarih.Dock = DockStyle.Fill;
            chkTarih.Font = new Font("Segoe UI", 10F);
            chkTarih.ForeColor = Color.FromArgb(71, 85, 105);
            chkTarih.Location = new Point(519, 3);
            chkTarih.Name = "chkTarih";
            chkTarih.Size = new Size(109, 36);
            chkTarih.TabIndex = 4;
            chkTarih.Text = "Tarih aralığı";
            chkTarih.UseVisualStyleBackColor = true;
            // 
            // dtBaslangic
            // 
            dtBaslangic.Dock = DockStyle.Fill;
            dtBaslangic.Font = new Font("Segoe UI", 9.5F);
            dtBaslangic.Format = DateTimePickerFormat.Short;
            dtBaslangic.Location = new Point(634, 8);
            dtBaslangic.Margin = new Padding(3, 8, 12, 3);
            dtBaslangic.Name = "dtBaslangic";
            dtBaslangic.Size = new Size(115, 24);
            dtBaslangic.TabIndex = 5;
            // 
            // dtBitis
            // 
            dtBitis.Dock = DockStyle.Fill;
            dtBitis.Font = new Font("Segoe UI", 9.5F);
            dtBitis.Format = DateTimePickerFormat.Short;
            dtBitis.Location = new Point(764, 8);
            dtBitis.Margin = new Padding(3, 8, 12, 3);
            dtBitis.Name = "dtBitis";
            dtBitis.Size = new Size(115, 24);
            dtBitis.TabIndex = 6;
            // 
            // btnFiltreTemizle
            // 
            btnFiltreTemizle.BackColor = Color.FromArgb(226, 232, 240);
            btnFiltreTemizle.Dock = DockStyle.Fill;
            btnFiltreTemizle.FlatAppearance.BorderSize = 0;
            btnFiltreTemizle.FlatStyle = FlatStyle.Flat;
            btnFiltreTemizle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnFiltreTemizle.ForeColor = Color.FromArgb(30, 41, 59);
            btnFiltreTemizle.Location = new Point(894, 6);
            btnFiltreTemizle.Margin = new Padding(3, 6, 10, 5);
            btnFiltreTemizle.Name = "btnFiltreTemizle";
            btnFiltreTemizle.Size = new Size(105, 31);
            btnFiltreTemizle.TabIndex = 7;
            btnFiltreTemizle.Text = "Temizle";
            btnFiltreTemizle.UseVisualStyleBackColor = false;
            // 
            // btnYenile
            // 
            btnYenile.BackColor = Color.FromArgb(37, 99, 235);
            btnYenile.Dock = DockStyle.Fill;
            btnYenile.FlatAppearance.BorderSize = 0;
            btnYenile.FlatStyle = FlatStyle.Flat;
            btnYenile.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnYenile.ForeColor = Color.White;
            btnYenile.Location = new Point(1012, 6);
            btnYenile.Margin = new Padding(3, 6, 3, 5);
            btnYenile.Name = "btnYenile";
            btnYenile.Size = new Size(99, 31);
            btnYenile.TabIndex = 8;
            btnYenile.Text = "Yenile";
            btnYenile.UseVisualStyleBackColor = false;
            // 
            // dgvDosyalar
            // 
            dgvDosyalar.AllowUserToAddRows = false;
            dgvDosyalar.AllowUserToDeleteRows = false;
            dgvDosyalar.AllowUserToResizeRows = false;
            dgvDosyalar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDosyalar.BackgroundColor = Color.White;
            dgvDosyalar.BorderStyle = BorderStyle.None;
            dgvDosyalar.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDosyalar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(244, 247, 251);
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(37, 54, 75);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(244, 247, 251);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(37, 54, 75);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvDosyalar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvDosyalar.ColumnHeadersHeight = 42;
            dgvDosyalar.Columns.AddRange(new DataGridViewColumn[] { colDosyaAdi, colDosyaTipi, colUzanti, colEklenme });
            dgvDosyalar.ContextMenuStrip = dgvContextMenu;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(37, 54, 75);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvDosyalar.DefaultCellStyle = dataGridViewCellStyle3;
            dgvDosyalar.Dock = DockStyle.Fill;
            dgvDosyalar.EnableHeadersVisualStyles = false;
            dgvDosyalar.GridColor = Color.FromArgb(226, 232, 240);
            dgvDosyalar.Location = new Point(21, 425);
            dgvDosyalar.MultiSelect = false;
            dgvDosyalar.Name = "dgvDosyalar";
            dgvDosyalar.ReadOnly = true;
            dgvDosyalar.RowHeadersVisible = false;
            dgvDosyalar.RowTemplate.Height = 38;
            dgvDosyalar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDosyalar.Size = new Size(1142, 245);
            dgvDosyalar.TabIndex = 4;
            // 
            // colDosyaAdi
            // 
            colDosyaAdi.DataPropertyName = "DosyaAdi";
            colDosyaAdi.HeaderText = "Dosya Adı";
            colDosyaAdi.Name = "colDosyaAdi";
            colDosyaAdi.ReadOnly = true;
            // 
            // colDosyaTipi
            // 
            colDosyaTipi.DataPropertyName = "DosyaTipi";
            colDosyaTipi.HeaderText = "Tür";
            colDosyaTipi.Name = "colDosyaTipi";
            colDosyaTipi.ReadOnly = true;
            // 
            // colUzanti
            // 
            colUzanti.DataPropertyName = "Uzanti";
            colUzanti.HeaderText = "Uzantı";
            colUzanti.Name = "colUzanti";
            colUzanti.ReadOnly = true;
            // 
            // colEklenme
            // 
            colEklenme.DataPropertyName = "Eklenme";
            dataGridViewCellStyle2.Format = "dd.MM.yyyy HH:mm";
            colEklenme.DefaultCellStyle = dataGridViewCellStyle2;
            colEklenme.HeaderText = "Eklenme Tarihi";
            colEklenme.Name = "colEklenme";
            colEklenme.ReadOnly = true;
            // 
            // dgvContextMenu
            // 
            dgvContextMenu.Items.AddRange(new ToolStripItem[] { menuIndir, menuMasaustuneIndir, menuDosyaAdiniKopyala, menuSeparator, menuYenile });
            dgvContextMenu.Name = "dgvContextMenu";
            dgvContextMenu.Size = new Size(180, 98);
            // 
            // menuIndir
            // 
            menuIndir.Name = "menuIndir";
            menuIndir.Size = new Size(179, 22);
            menuIndir.Text = "Farklı kaydet / indir";
            // 
            // menuMasaustuneIndir
            // 
            menuMasaustuneIndir.Name = "menuMasaustuneIndir";
            menuMasaustuneIndir.Size = new Size(179, 22);
            menuMasaustuneIndir.Text = "Masaüstüne indir";
            // 
            // menuDosyaAdiniKopyala
            // 
            menuDosyaAdiniKopyala.Name = "menuDosyaAdiniKopyala";
            menuDosyaAdiniKopyala.Size = new Size(179, 22);
            menuDosyaAdiniKopyala.Text = "Dosya adını kopyala";
            // 
            // menuSeparator
            // 
            menuSeparator.Name = "menuSeparator";
            menuSeparator.Size = new Size(176, 6);
            // 
            // menuYenile
            // 
            menuYenile.Name = "menuYenile";
            menuYenile.Size = new Size(179, 22);
            menuYenile.Text = "Listeyi yenile";
            // 
            // actionPanel
            // 
            actionPanel.Controls.Add(btnColumns);
            actionPanel.Controls.Add(btnIndir);
            actionPanel.Controls.Add(btnMasaustuIndir);
            actionPanel.Controls.Add(btnKopyala);
            actionPanel.Dock = DockStyle.Fill;
            actionPanel.FlowDirection = FlowDirection.RightToLeft;
            actionPanel.Location = new Point(21, 676);
            actionPanel.Name = "actionPanel";
            actionPanel.Padding = new Padding(0, 10, 0, 0);
            actionPanel.Size = new Size(1142, 52);
            actionPanel.TabIndex = 5;
            // 
            // btnColumns
            // 
            btnColumns.FlatAppearance.BorderColor = Color.FromArgb(220, 226, 233);
            btnColumns.FlatStyle = FlatStyle.Flat;
            btnColumns.Location = new Point(1039, 13);
            btnColumns.Name = "btnColumns";
            btnColumns.Size = new Size(100, 34);
            btnColumns.TabIndex = 0;
            btnColumns.Text = "Sütunlar";
            btnColumns.Click += Columns_Click;
            // 
            // btnIndir
            // 
            btnIndir.BackColor = Color.FromArgb(37, 99, 235);
            btnIndir.FlatAppearance.BorderSize = 0;
            btnIndir.FlatStyle = FlatStyle.Flat;
            btnIndir.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnIndir.ForeColor = Color.White;
            btnIndir.Location = new Point(876, 13);
            btnIndir.Name = "btnIndir";
            btnIndir.Size = new Size(157, 34);
            btnIndir.TabIndex = 0;
            btnIndir.Text = "Farklı Kaydet";
            btnIndir.UseVisualStyleBackColor = false;
            // 
            // btnMasaustuIndir
            // 
            btnMasaustuIndir.BackColor = Color.FromArgb(244, 247, 251);
            btnMasaustuIndir.FlatAppearance.BorderSize = 0;
            btnMasaustuIndir.FlatStyle = FlatStyle.Flat;
            btnMasaustuIndir.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnMasaustuIndir.ForeColor = Color.White;
            btnMasaustuIndir.Location = new Point(707, 13);
            btnMasaustuIndir.Name = "btnMasaustuIndir";
            btnMasaustuIndir.Size = new Size(163, 34);
            btnMasaustuIndir.TabIndex = 1;
            btnMasaustuIndir.Text = "Masaüstüne İndir";
            btnMasaustuIndir.UseVisualStyleBackColor = false;
            // 
            // btnKopyala
            // 
            btnKopyala.BackColor = Color.FromArgb(226, 232, 240);
            btnKopyala.FlatAppearance.BorderSize = 0;
            btnKopyala.FlatStyle = FlatStyle.Flat;
            btnKopyala.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnKopyala.ForeColor = Color.FromArgb(30, 41, 59);
            btnKopyala.Location = new Point(538, 13);
            btnKopyala.Name = "btnKopyala";
            btnKopyala.Size = new Size(163, 34);
            btnKopyala.TabIndex = 2;
            btnKopyala.Text = "Dosya Adını Kopyala";
            btnKopyala.UseVisualStyleBackColor = false;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus, progressBar });
            statusStrip.Location = new Point(0, 739);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1184, 22);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip";
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(37, 17);
            lblStatus.Text = "Hazır.";
            // 
            // progressBar
            // 
            progressBar.MarqueeAnimationSpeed = 35;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(120, 16);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.Visible = false;
            // 
            // arsivForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(246, 248, 252);
            ClientSize = new Size(1184, 761);
            Controls.Add(rootLayout);
            Controls.Add(statusStrip);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(980, 640);
            Name = "arsivForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Akıllı Dosya Arşivi";
            rootLayout.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            summaryPanel.ResumeLayout(false);
            pnlTotalCard.ResumeLayout(false);
            pnlTotalCard.PerformLayout();
            pnlSelectedCard.ResumeLayout(false);
            pnlSelectedCard.PerformLayout();
            pnlLastCard.ResumeLayout(false);
            pnlLastCard.PerformLayout();
            grpUpload.ResumeLayout(false);
            uploadLayout.ResumeLayout(false);
            uploadLayout.PerformLayout();
            grpFilters.ResumeLayout(false);
            filterLayout.ResumeLayout(false);
            filterLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDosyalar).EndInit();
            dgvContextMenu.ResumeLayout(false);
            actionPanel.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
