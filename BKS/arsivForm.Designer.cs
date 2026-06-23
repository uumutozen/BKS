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

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle cellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            rootLayout = new System.Windows.Forms.TableLayoutPanel();
            headerPanel = new System.Windows.Forms.Panel();
            lblHeaderBadge = new System.Windows.Forms.Label();
            lblSubtitle = new System.Windows.Forms.Label();
            lblTitle = new System.Windows.Forms.Label();
            summaryPanel = new System.Windows.Forms.FlowLayoutPanel();
            pnlTotalCard = new System.Windows.Forms.Panel();
            lblTotalValue = new System.Windows.Forms.Label();
            lblTotalTitle = new System.Windows.Forms.Label();
            pnlSelectedCard = new System.Windows.Forms.Panel();
            lblSelectedValue = new System.Windows.Forms.Label();
            lblSelectedTitle = new System.Windows.Forms.Label();
            pnlLastCard = new System.Windows.Forms.Panel();
            lblLastValue = new System.Windows.Forms.Label();
            lblLastTitle = new System.Windows.Forms.Label();
            grpUpload = new System.Windows.Forms.GroupBox();
            uploadLayout = new System.Windows.Forms.TableLayoutPanel();
            lblOgrenci = new System.Windows.Forms.Label();
            cmbOgrenciler = new System.Windows.Forms.ComboBox();
            btnDosyaSec = new System.Windows.Forms.Button();
            btnYukle = new System.Windows.Forms.Button();
            lblDosya = new System.Windows.Forms.Label();
            txtDosyaYolu = new System.Windows.Forms.TextBox();
            lblDropHint = new System.Windows.Forms.Label();
            grpFilters = new System.Windows.Forms.GroupBox();
            filterLayout = new System.Windows.Forms.TableLayoutPanel();
            lblAra = new System.Windows.Forms.Label();
            txtAra = new System.Windows.Forms.TextBox();
            lblTur = new System.Windows.Forms.Label();
            cmbTur = new System.Windows.Forms.ComboBox();
            chkTarih = new System.Windows.Forms.CheckBox();
            dtBaslangic = new System.Windows.Forms.DateTimePicker();
            dtBitis = new System.Windows.Forms.DateTimePicker();
            btnFiltreTemizle = new System.Windows.Forms.Button();
            btnYenile = new System.Windows.Forms.Button();
            dgvDosyalar = new System.Windows.Forms.DataGridView();
            dgvContextMenu = new System.Windows.Forms.ContextMenuStrip(components);
            menuIndir = new System.Windows.Forms.ToolStripMenuItem();
            menuMasaustuneIndir = new System.Windows.Forms.ToolStripMenuItem();
            menuDosyaAdiniKopyala = new System.Windows.Forms.ToolStripMenuItem();
            menuSeparator = new System.Windows.Forms.ToolStripSeparator();
            menuYenile = new System.Windows.Forms.ToolStripMenuItem();
            actionPanel = new System.Windows.Forms.FlowLayoutPanel();
            btnIndir = new System.Windows.Forms.Button();
            btnMasaustuIndir = new System.Windows.Forms.Button();
            btnKopyala = new System.Windows.Forms.Button();
            statusStrip = new System.Windows.Forms.StatusStrip();
            lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            progressBar = new System.Windows.Forms.ToolStripProgressBar();
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
            rootLayout.BackColor = System.Drawing.Color.FromArgb(246, 248, 252);
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootLayout.Controls.Add(headerPanel, 0, 0);
            rootLayout.Controls.Add(summaryPanel, 0, 1);
            rootLayout.Controls.Add(grpUpload, 0, 2);
            rootLayout.Controls.Add(grpFilters, 0, 3);
            rootLayout.Controls.Add(dgvDosyalar, 0, 4);
            rootLayout.Controls.Add(actionPanel, 0, 5);
            rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            rootLayout.Location = new System.Drawing.Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new System.Windows.Forms.Padding(18, 16, 18, 8);
            rootLayout.RowCount = 6;
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            rootLayout.Size = new System.Drawing.Size(1184, 739);
            rootLayout.TabIndex = 0;
            // 
            // headerPanel
            // 
            headerPanel.BackColor = System.Drawing.Color.FromArgb(21, 32, 55);
            headerPanel.Controls.Add(lblHeaderBadge);
            headerPanel.Controls.Add(lblSubtitle);
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            headerPanel.Location = new System.Drawing.Point(21, 19);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new System.Windows.Forms.Padding(24, 18, 24, 18);
            headerPanel.Size = new System.Drawing.Size(1142, 98);
            headerPanel.TabIndex = 0;
            // 
            // lblHeaderBadge
            // 
            lblHeaderBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblHeaderBadge.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            lblHeaderBadge.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            lblHeaderBadge.ForeColor = System.Drawing.Color.White;
            lblHeaderBadge.Location = new System.Drawing.Point(958, 22);
            lblHeaderBadge.Name = "lblHeaderBadge";
            lblHeaderBadge.Size = new System.Drawing.Size(160, 30);
            lblHeaderBadge.TabIndex = 2;
            lblHeaderBadge.Text = "Responsive Arşiv";
            lblHeaderBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
            lblSubtitle.Location = new System.Drawing.Point(26, 56);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new System.Drawing.Size(850, 25);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Öğrenci/personel dosyalarını tek ekrandan filtrele, yükle, indir ve yönet.";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.White;
            lblTitle.Location = new System.Drawing.Point(24, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(280, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Akıllı Dosya Arşivi";
            // 
            // summaryPanel
            // 
            summaryPanel.AutoScroll = true;
            summaryPanel.BackColor = System.Drawing.Color.Transparent;
            summaryPanel.Controls.Add(pnlTotalCard);
            summaryPanel.Controls.Add(pnlSelectedCard);
            summaryPanel.Controls.Add(pnlLastCard);
            summaryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            summaryPanel.Location = new System.Drawing.Point(18, 120);
            summaryPanel.Margin = new System.Windows.Forms.Padding(0);
            summaryPanel.Name = "summaryPanel";
            summaryPanel.Padding = new System.Windows.Forms.Padding(2, 10, 2, 6);
            summaryPanel.Size = new System.Drawing.Size(1148, 86);
            summaryPanel.TabIndex = 1;
            summaryPanel.WrapContents = true;
            // 
            // pnlTotalCard
            // 
            pnlTotalCard.BackColor = System.Drawing.Color.White;
            pnlTotalCard.Controls.Add(lblTotalValue);
            pnlTotalCard.Controls.Add(lblTotalTitle);
            pnlTotalCard.Location = new System.Drawing.Point(8, 16);
            pnlTotalCard.Margin = new System.Windows.Forms.Padding(6);
            pnlTotalCard.Name = "pnlTotalCard";
            pnlTotalCard.Padding = new System.Windows.Forms.Padding(18, 10, 18, 10);
            pnlTotalCard.Size = new System.Drawing.Size(260, 62);
            pnlTotalCard.TabIndex = 0;
            // 
            // lblTotalValue
            // 
            lblTotalValue.AutoSize = true;
            lblTotalValue.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            lblTotalValue.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblTotalValue.Location = new System.Drawing.Point(17, 25);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new System.Drawing.Size(27, 32);
            lblTotalValue.TabIndex = 1;
            lblTotalValue.Text = "0";
            // 
            // lblTotalTitle
            // 
            lblTotalTitle.AutoSize = true;
            lblTotalTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblTotalTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTotalTitle.Location = new System.Drawing.Point(18, 9);
            lblTotalTitle.Name = "lblTotalTitle";
            lblTotalTitle.Size = new System.Drawing.Size(122, 15);
            lblTotalTitle.TabIndex = 0;
            lblTotalTitle.Text = "Görünen / toplam dosya";
            // 
            // pnlSelectedCard
            // 
            pnlSelectedCard.BackColor = System.Drawing.Color.White;
            pnlSelectedCard.Controls.Add(lblSelectedValue);
            pnlSelectedCard.Controls.Add(lblSelectedTitle);
            pnlSelectedCard.Location = new System.Drawing.Point(280, 16);
            pnlSelectedCard.Margin = new System.Windows.Forms.Padding(6);
            pnlSelectedCard.Name = "pnlSelectedCard";
            pnlSelectedCard.Padding = new System.Windows.Forms.Padding(18, 10, 18, 10);
            pnlSelectedCard.Size = new System.Drawing.Size(360, 62);
            pnlSelectedCard.TabIndex = 1;
            // 
            // lblSelectedValue
            // 
            lblSelectedValue.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblSelectedValue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            lblSelectedValue.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblSelectedValue.Location = new System.Drawing.Point(18, 30);
            lblSelectedValue.Name = "lblSelectedValue";
            lblSelectedValue.Size = new System.Drawing.Size(320, 23);
            lblSelectedValue.TabIndex = 1;
            lblSelectedValue.Text = "-";
            // 
            // lblSelectedTitle
            // 
            lblSelectedTitle.AutoSize = true;
            lblSelectedTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblSelectedTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblSelectedTitle.Location = new System.Drawing.Point(18, 9);
            lblSelectedTitle.Name = "lblSelectedTitle";
            lblSelectedTitle.Size = new System.Drawing.Size(63, 15);
            lblSelectedTitle.TabIndex = 0;
            lblSelectedTitle.Text = "Aktif kayıt";
            // 
            // pnlLastCard
            // 
            pnlLastCard.BackColor = System.Drawing.Color.White;
            pnlLastCard.Controls.Add(lblLastValue);
            pnlLastCard.Controls.Add(lblLastTitle);
            pnlLastCard.Location = new System.Drawing.Point(652, 16);
            pnlLastCard.Margin = new System.Windows.Forms.Padding(6);
            pnlLastCard.Name = "pnlLastCard";
            pnlLastCard.Padding = new System.Windows.Forms.Padding(18, 10, 18, 10);
            pnlLastCard.Size = new System.Drawing.Size(260, 62);
            pnlLastCard.TabIndex = 2;
            // 
            // lblLastValue
            // 
            lblLastValue.AutoSize = true;
            lblLastValue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            lblLastValue.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblLastValue.Location = new System.Drawing.Point(18, 30);
            lblLastValue.Name = "lblLastValue";
            lblLastValue.Size = new System.Drawing.Size(16, 21);
            lblLastValue.TabIndex = 1;
            lblLastValue.Text = "-";
            // 
            // lblLastTitle
            // 
            lblLastTitle.AutoSize = true;
            lblLastTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblLastTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblLastTitle.Location = new System.Drawing.Point(18, 9);
            lblLastTitle.Name = "lblLastTitle";
            lblLastTitle.Size = new System.Drawing.Size(89, 15);
            lblLastTitle.TabIndex = 0;
            lblLastTitle.Text = "Son eklenen tarih";
            // 
            // grpUpload
            // 
            grpUpload.Controls.Add(uploadLayout);
            grpUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            grpUpload.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            grpUpload.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            grpUpload.Location = new System.Drawing.Point(21, 209);
            grpUpload.Name = "grpUpload";
            grpUpload.Padding = new System.Windows.Forms.Padding(14, 12, 14, 12);
            grpUpload.Size = new System.Drawing.Size(1142, 120);
            grpUpload.TabIndex = 2;
            grpUpload.TabStop = false;
            grpUpload.Text = " Dosya yükleme alanı ";
            // 
            // uploadLayout
            // 
            uploadLayout.ColumnCount = 4;
            uploadLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            uploadLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            uploadLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            uploadLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            uploadLayout.Controls.Add(lblOgrenci, 0, 0);
            uploadLayout.Controls.Add(cmbOgrenciler, 1, 0);
            uploadLayout.Controls.Add(btnDosyaSec, 2, 0);
            uploadLayout.Controls.Add(btnYukle, 3, 0);
            uploadLayout.Controls.Add(lblDosya, 0, 1);
            uploadLayout.Controls.Add(txtDosyaYolu, 1, 1);
            uploadLayout.Controls.Add(lblDropHint, 2, 1);
            uploadLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            uploadLayout.Location = new System.Drawing.Point(14, 30);
            uploadLayout.Name = "uploadLayout";
            uploadLayout.RowCount = 2;
            uploadLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            uploadLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            uploadLayout.Size = new System.Drawing.Size(1114, 78);
            uploadLayout.TabIndex = 0;
            // 
            // lblOgrenci
            // 
            lblOgrenci.AutoSize = true;
            lblOgrenci.Dock = System.Windows.Forms.DockStyle.Fill;
            lblOgrenci.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblOgrenci.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblOgrenci.Location = new System.Drawing.Point(3, 0);
            lblOgrenci.Name = "lblOgrenci";
            lblOgrenci.Size = new System.Drawing.Size(112, 39);
            lblOgrenci.TabIndex = 0;
            lblOgrenci.Text = "Kayıt";
            lblOgrenci.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbOgrenciler
            // 
            cmbOgrenciler.Dock = System.Windows.Forms.DockStyle.Fill;
            cmbOgrenciler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbOgrenciler.Font = new System.Drawing.Font("Segoe UI", 10F);
            cmbOgrenciler.FormattingEnabled = true;
            cmbOgrenciler.Location = new System.Drawing.Point(121, 7);
            cmbOgrenciler.Margin = new System.Windows.Forms.Padding(3, 7, 12, 3);
            cmbOgrenciler.Name = "cmbOgrenciler";
            cmbOgrenciler.Size = new System.Drawing.Size(620, 25);
            cmbOgrenciler.TabIndex = 1;
            // 
            // btnDosyaSec
            // 
            btnDosyaSec.BackColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnDosyaSec.Dock = System.Windows.Forms.DockStyle.Fill;
            btnDosyaSec.FlatAppearance.BorderSize = 0;
            btnDosyaSec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDosyaSec.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            btnDosyaSec.ForeColor = System.Drawing.Color.White;
            btnDosyaSec.Location = new System.Drawing.Point(756, 5);
            btnDosyaSec.Margin = new System.Windows.Forms.Padding(3, 5, 12, 5);
            btnDosyaSec.Name = "btnDosyaSec";
            btnDosyaSec.Size = new System.Drawing.Size(123, 29);
            btnDosyaSec.TabIndex = 2;
            btnDosyaSec.Text = "Dosya Seç";
            btnDosyaSec.UseVisualStyleBackColor = false;
            // 
            // btnYukle
            // 
            btnYukle.BackColor = System.Drawing.Color.FromArgb(22, 163, 74);
            btnYukle.Dock = System.Windows.Forms.DockStyle.Fill;
            btnYukle.FlatAppearance.BorderSize = 0;
            btnYukle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnYukle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            btnYukle.ForeColor = System.Drawing.Color.White;
            btnYukle.Location = new System.Drawing.Point(894, 5);
            btnYukle.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            btnYukle.Name = "btnYukle";
            btnYukle.Size = new System.Drawing.Size(217, 29);
            btnYukle.TabIndex = 3;
            btnYukle.Text = "Yükle";
            btnYukle.UseVisualStyleBackColor = false;
            // 
            // lblDosya
            // 
            lblDosya.AutoSize = true;
            lblDosya.Dock = System.Windows.Forms.DockStyle.Fill;
            lblDosya.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblDosya.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblDosya.Location = new System.Drawing.Point(3, 39);
            lblDosya.Name = "lblDosya";
            lblDosya.Size = new System.Drawing.Size(112, 39);
            lblDosya.TabIndex = 4;
            lblDosya.Text = "Seçilen dosya";
            lblDosya.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDosyaYolu
            // 
            txtDosyaYolu.Dock = System.Windows.Forms.DockStyle.Fill;
            txtDosyaYolu.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtDosyaYolu.Location = new System.Drawing.Point(121, 46);
            txtDosyaYolu.Margin = new System.Windows.Forms.Padding(3, 7, 12, 3);
            txtDosyaYolu.Name = "txtDosyaYolu";
            txtDosyaYolu.ReadOnly = true;
            txtDosyaYolu.Size = new System.Drawing.Size(620, 25);
            txtDosyaYolu.TabIndex = 5;
            // 
            // lblDropHint
            // 
            lblDropHint.AutoSize = true;
            uploadLayout.SetColumnSpan(lblDropHint, 2);
            lblDropHint.Dock = System.Windows.Forms.DockStyle.Fill;
            lblDropHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblDropHint.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblDropHint.Location = new System.Drawing.Point(756, 39);
            lblDropHint.Name = "lblDropHint";
            lblDropHint.Size = new System.Drawing.Size(355, 39);
            lblDropHint.TabIndex = 6;
            lblDropHint.Text = "Dosyayı buraya sürükleyip bırakabilirsin.";
            lblDropHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpFilters
            // 
            grpFilters.Controls.Add(filterLayout);
            grpFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            grpFilters.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            grpFilters.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            grpFilters.Location = new System.Drawing.Point(21, 335);
            grpFilters.Name = "grpFilters";
            grpFilters.Padding = new System.Windows.Forms.Padding(14, 12, 14, 12);
            grpFilters.Size = new System.Drawing.Size(1142, 84);
            grpFilters.TabIndex = 3;
            grpFilters.TabStop = false;
            grpFilters.Text = " Arama ve filtreler ";
            // 
            // filterLayout
            // 
            filterLayout.ColumnCount = 9;
            filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            filterLayout.Controls.Add(lblAra, 0, 0);
            filterLayout.Controls.Add(txtAra, 1, 0);
            filterLayout.Controls.Add(lblTur, 2, 0);
            filterLayout.Controls.Add(cmbTur, 3, 0);
            filterLayout.Controls.Add(chkTarih, 4, 0);
            filterLayout.Controls.Add(dtBaslangic, 5, 0);
            filterLayout.Controls.Add(dtBitis, 6, 0);
            filterLayout.Controls.Add(btnFiltreTemizle, 7, 0);
            filterLayout.Controls.Add(btnYenile, 8, 0);
            filterLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            filterLayout.Location = new System.Drawing.Point(14, 30);
            filterLayout.Name = "filterLayout";
            filterLayout.RowCount = 1;
            filterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            filterLayout.Size = new System.Drawing.Size(1114, 42);
            filterLayout.TabIndex = 0;
            // 
            // lblAra
            // 
            lblAra.AutoSize = true;
            lblAra.Dock = System.Windows.Forms.DockStyle.Fill;
            lblAra.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblAra.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblAra.Location = new System.Drawing.Point(3, 0);
            lblAra.Name = "lblAra";
            lblAra.Size = new System.Drawing.Size(49, 42);
            lblAra.TabIndex = 0;
            lblAra.Text = "Ara";
            lblAra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAra
            // 
            txtAra.Dock = System.Windows.Forms.DockStyle.Fill;
            txtAra.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtAra.Location = new System.Drawing.Point(58, 8);
            txtAra.Margin = new System.Windows.Forms.Padding(3, 8, 12, 3);
            txtAra.Name = "txtAra";
            txtAra.PlaceholderText = "Dosya adı veya uzantı ara...";
            txtAra.Size = new System.Drawing.Size(236, 25);
            txtAra.TabIndex = 1;
            // 
            // lblTur
            // 
            lblTur.AutoSize = true;
            lblTur.Dock = System.Windows.Forms.DockStyle.Fill;
            lblTur.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblTur.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblTur.Location = new System.Drawing.Point(309, 0);
            lblTur.Name = "lblTur";
            lblTur.Size = new System.Drawing.Size(34, 42);
            lblTur.TabIndex = 2;
            lblTur.Text = "Tür";
            lblTur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbTur
            // 
            cmbTur.Dock = System.Windows.Forms.DockStyle.Fill;
            cmbTur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbTur.Font = new System.Drawing.Font("Segoe UI", 10F);
            cmbTur.FormattingEnabled = true;
            cmbTur.Location = new System.Drawing.Point(349, 8);
            cmbTur.Margin = new System.Windows.Forms.Padding(3, 8, 12, 3);
            cmbTur.Name = "cmbTur";
            cmbTur.Size = new System.Drawing.Size(115, 25);
            cmbTur.TabIndex = 3;
            // 
            // chkTarih
            // 
            chkTarih.AutoSize = true;
            chkTarih.Dock = System.Windows.Forms.DockStyle.Fill;
            chkTarih.Font = new System.Drawing.Font("Segoe UI", 10F);
            chkTarih.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            chkTarih.Location = new System.Drawing.Point(479, 3);
            chkTarih.Name = "chkTarih";
            chkTarih.Size = new System.Drawing.Size(109, 36);
            chkTarih.TabIndex = 4;
            chkTarih.Text = "Tarih aralığı";
            chkTarih.UseVisualStyleBackColor = true;
            // 
            // dtBaslangic
            // 
            dtBaslangic.Dock = System.Windows.Forms.DockStyle.Fill;
            dtBaslangic.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dtBaslangic.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtBaslangic.Location = new System.Drawing.Point(594, 8);
            dtBaslangic.Margin = new System.Windows.Forms.Padding(3, 8, 12, 3);
            dtBaslangic.Name = "dtBaslangic";
            dtBaslangic.Size = new System.Drawing.Size(115, 24);
            dtBaslangic.TabIndex = 5;
            // 
            // dtBitis
            // 
            dtBitis.Dock = System.Windows.Forms.DockStyle.Fill;
            dtBitis.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dtBitis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtBitis.Location = new System.Drawing.Point(724, 8);
            dtBitis.Margin = new System.Windows.Forms.Padding(3, 8, 12, 3);
            dtBitis.Name = "dtBitis";
            dtBitis.Size = new System.Drawing.Size(115, 24);
            dtBitis.TabIndex = 6;
            // 
            // btnFiltreTemizle
            // 
            btnFiltreTemizle.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnFiltreTemizle.Dock = System.Windows.Forms.DockStyle.Fill;
            btnFiltreTemizle.FlatAppearance.BorderSize = 0;
            btnFiltreTemizle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnFiltreTemizle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            btnFiltreTemizle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            btnFiltreTemizle.Location = new System.Drawing.Point(854, 6);
            btnFiltreTemizle.Margin = new System.Windows.Forms.Padding(3, 6, 10, 5);
            btnFiltreTemizle.Name = "btnFiltreTemizle";
            btnFiltreTemizle.Size = new System.Drawing.Size(105, 31);
            btnFiltreTemizle.TabIndex = 7;
            btnFiltreTemizle.Text = "Temizle";
            btnFiltreTemizle.UseVisualStyleBackColor = false;
            // 
            // btnYenile
            // 
            btnYenile.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnYenile.Dock = System.Windows.Forms.DockStyle.Fill;
            btnYenile.FlatAppearance.BorderSize = 0;
            btnYenile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnYenile.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            btnYenile.ForeColor = System.Drawing.Color.White;
            btnYenile.Location = new System.Drawing.Point(972, 6);
            btnYenile.Margin = new System.Windows.Forms.Padding(3, 6, 3, 5);
            btnYenile.Name = "btnYenile";
            btnYenile.Size = new System.Drawing.Size(139, 31);
            btnYenile.TabIndex = 8;
            btnYenile.Text = "Yenile";
            btnYenile.UseVisualStyleBackColor = false;
            // 
            // dgvDosyalar
            // 
            dgvDosyalar.AllowUserToAddRows = false;
            dgvDosyalar.AllowUserToDeleteRows = false;
            dgvDosyalar.AllowUserToResizeRows = false;
            dgvDosyalar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvDosyalar.BackgroundColor = System.Drawing.Color.White;
            dgvDosyalar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvDosyalar.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDosyalar.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            headerStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.SelectionBackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            headerStyle.SelectionForeColor = System.Drawing.Color.White;
            headerStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvDosyalar.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvDosyalar.ColumnHeadersHeight = 42;
            dgvDosyalar.ContextMenuStrip = dgvContextMenu;
            cellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            cellStyle.BackColor = System.Drawing.Color.White;
            cellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            cellStyle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            cellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            cellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            cellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvDosyalar.DefaultCellStyle = cellStyle;
            dgvDosyalar.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvDosyalar.EnableHeadersVisualStyles = false;
            dgvDosyalar.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvDosyalar.Location = new System.Drawing.Point(21, 425);
            dgvDosyalar.MultiSelect = false;
            dgvDosyalar.Name = "dgvDosyalar";
            dgvDosyalar.ReadOnly = true;
            dgvDosyalar.RowHeadersVisible = false;
            dgvDosyalar.RowTemplate.Height = 38;
            dgvDosyalar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvDosyalar.Size = new System.Drawing.Size(1142, 253);
            dgvDosyalar.TabIndex = 4;
            // 
            // dgvContextMenu
            // 
            dgvContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { menuIndir, menuMasaustuneIndir, menuDosyaAdiniKopyala, menuSeparator, menuYenile });
            dgvContextMenu.Name = "dgvContextMenu";
            dgvContextMenu.Size = new System.Drawing.Size(185, 98);
            // 
            // menuIndir
            // 
            menuIndir.Name = "menuIndir";
            menuIndir.Size = new System.Drawing.Size(184, 22);
            menuIndir.Text = "Farklı kaydet / indir";
            // 
            // menuMasaustuneIndir
            // 
            menuMasaustuneIndir.Name = "menuMasaustuneIndir";
            menuMasaustuneIndir.Size = new System.Drawing.Size(184, 22);
            menuMasaustuneIndir.Text = "Masaüstüne indir";
            // 
            // menuDosyaAdiniKopyala
            // 
            menuDosyaAdiniKopyala.Name = "menuDosyaAdiniKopyala";
            menuDosyaAdiniKopyala.Size = new System.Drawing.Size(184, 22);
            menuDosyaAdiniKopyala.Text = "Dosya adını kopyala";
            // 
            // menuSeparator
            // 
            menuSeparator.Name = "menuSeparator";
            menuSeparator.Size = new System.Drawing.Size(181, 6);
            // 
            // menuYenile
            // 
            menuYenile.Name = "menuYenile";
            menuYenile.Size = new System.Drawing.Size(184, 22);
            menuYenile.Text = "Listeyi yenile";
            // 
            // actionPanel
            // 
            actionPanel.Controls.Add(btnIndir);
            actionPanel.Controls.Add(btnMasaustuIndir);
            actionPanel.Controls.Add(btnKopyala);
            actionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            actionPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            actionPanel.Location = new System.Drawing.Point(21, 684);
            actionPanel.Name = "actionPanel";
            actionPanel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            actionPanel.Size = new System.Drawing.Size(1142, 52);
            actionPanel.TabIndex = 5;
            // 
            // btnIndir
            // 
            btnIndir.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnIndir.FlatAppearance.BorderSize = 0;
            btnIndir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnIndir.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            btnIndir.ForeColor = System.Drawing.Color.White;
            btnIndir.Location = new System.Drawing.Point(982, 13);
            btnIndir.Name = "btnIndir";
            btnIndir.Size = new System.Drawing.Size(157, 34);
            btnIndir.TabIndex = 0;
            btnIndir.Text = "Farklı Kaydet";
            btnIndir.UseVisualStyleBackColor = false;
            // 
            // btnMasaustuIndir
            // 
            btnMasaustuIndir.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            btnMasaustuIndir.FlatAppearance.BorderSize = 0;
            btnMasaustuIndir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnMasaustuIndir.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            btnMasaustuIndir.ForeColor = System.Drawing.Color.White;
            btnMasaustuIndir.Location = new System.Drawing.Point(813, 13);
            btnMasaustuIndir.Name = "btnMasaustuIndir";
            btnMasaustuIndir.Size = new System.Drawing.Size(163, 34);
            btnMasaustuIndir.TabIndex = 1;
            btnMasaustuIndir.Text = "Masaüstüne İndir";
            btnMasaustuIndir.UseVisualStyleBackColor = false;
            // 
            // btnKopyala
            // 
            btnKopyala.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnKopyala.FlatAppearance.BorderSize = 0;
            btnKopyala.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnKopyala.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            btnKopyala.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            btnKopyala.Location = new System.Drawing.Point(644, 13);
            btnKopyala.Name = "btnKopyala";
            btnKopyala.Size = new System.Drawing.Size(163, 34);
            btnKopyala.TabIndex = 2;
            btnKopyala.Text = "Dosya Adını Kopyala";
            btnKopyala.UseVisualStyleBackColor = false;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { lblStatus, progressBar });
            statusStrip.Location = new System.Drawing.Point(0, 739);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new System.Drawing.Size(1184, 22);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip";
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(39, 17);
            lblStatus.Text = "Hazır.";
            // 
            // progressBar
            // 
            progressBar.MarqueeAnimationSpeed = 35;
            progressBar.Name = "progressBar";
            progressBar.Size = new System.Drawing.Size(120, 16);
            progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            progressBar.Visible = false;
            // 
            // arsivForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(246, 248, 252);
            ClientSize = new System.Drawing.Size(1184, 761);
            Controls.Add(rootLayout);
            Controls.Add(statusStrip);
            Font = new System.Drawing.Font("Segoe UI", 10F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimumSize = new System.Drawing.Size(980, 640);
            Name = "arsivForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
