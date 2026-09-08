using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BKS
{
    public partial class arsivForm : Form
    {
<<<<<<< HEAD
        private static string ApiBaseUrl => AppConfiguration.Api("api/dosya-arsiv").AbsoluteUri;
        private BksRibbon archiveRibbon;
        private readonly Guid UserId;
        private readonly Guid OgrenciId;
        private readonly string connectionString;
        private readonly HttpClient httpClient = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(90)
        };
        private readonly BindingSource dosyaBindingSource = new BindingSource();
        private List<DosyaArsivModel> tumDosyalar = new List<DosyaArsivModel>();
        private bool isBusy;
=======
        private const string ApiBaseUrl = "https://randevu.aslancan.com.tr/api/dosya-arsiv";

        private readonly Guid UserId;
        private readonly Guid OgrenciId;
        private readonly string connectionString;
        private readonly HttpClient httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(90) };
        private readonly BindingSource dosyaBindingSource = new BindingSource();

        private List<DosyaArsivModel> tumDosyalar = new List<DosyaArsivModel>();
        private bool isBusy;

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
        public arsivForm(Guid userId, string connStr, Guid ogrenciId)
        {
            UserId = userId;
            connectionString = connStr;
            OgrenciId = ogrenciId;

            InitializeComponent();
<<<<<<< HEAD
            BuildRibbonArchiveLayout();
            ConfigureScreen();
            RegisterEvents();
            Shown += async(_, _) =>
            {
                if (AppConfiguration.DesignPreview) return;
                try
                {
                    LoadOgrenciler();
                    await LoadOgrenciDosyalariAsync();
                }
                catch (Exception ex)
                {
                    UiActions.ShowError(ex);
                }
            };
        }
=======
            ConfigureScreen();
            RegisterEvents();

            LoadOgrenciler();
            _ = LoadOgrenciDosyalariAsync();
        }

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
        private void ConfigureScreen()
        {
            ConfigureGrid();
            ConfigureFilters();
            ConfigureDragDrop();
            UpdateDateFilterEnabled();
            UpdateSummary();
            SetStatus("Hazır.");
        }
<<<<<<< HEAD
        private void BuildRibbonArchiveLayout()
        {
            var fields = new ResponsiveFields(("Kayıt", cmbOgrenciler), ("Yüklenecek dosya", txtDosyaYolu), ("Dosya ara", txtAra),
            ("Dosya türü", cmbTur), ("Tarih filtresi", chkTarih), ("Başlangıç", dtBaslangic), ("Bitiş", dtBitis));
            archiveRibbon = Screens.Ribbon("Arşiv", new RibbonCommand("Dosya seç", RibbonIcon.Folder, () => BtnDosyaSec_Click(this,
            EventArgs.Empty), () => !isBusy), new RibbonCommand("Yükle", RibbonIcon.Export, () => _ = YukleAsync(), () => !isBusy),
            new RibbonCommand("İndir", RibbonIcon.Backup, () => _ = IndirAsync(true), () => !isBusy), new RibbonCommand("Masaüstüne indir",
            RibbonIcon.Backup, () => _ = IndirAsync(false), () => !isBusy), new RibbonCommand("Yenile", RibbonIcon.Refresh,
            () => _ = LoadOgrenciDosyalariAsync(), () => !isBusy), new RibbonCommand("Filtre temizle", RibbonIcon.Search, ClearFilters),
            new RibbonCommand("Kapat", RibbonIcon.Restore, Close));
            Screens.Install(this, Screens.WithEditor(fields, dgvDosyalar, .40F), archiveRibbon, "Dosya arşivi");
            if (Controls[0] is RibbonWorkspace workspace) workspace.SetFooter(statusStrip);
            fields.AllowDrop = true;
            fields.DragEnter += Upload_DragEnter;
            fields.DragDrop += Upload_DragDrop;
        }
        private void RegisterEvents()
        {
            cmbOgrenciler.SelectedIndexChanged += async(s, e) => await LoadOgrenciDosyalariAsync();
            btnDosyaSec.Click += BtnDosyaSec_Click;
            btnYukle.Click += async(s, e) => await YukleAsync();
            btnIndir.Click += async(s, e) => await IndirAsync(true);
            btnMasaustuIndir.Click += async(s, e) => await IndirAsync(false);
            btnKopyala.Click += (s, e) => CopySelectedFileName();
            btnYenile.Click += async(s, e) => await LoadOgrenciDosyalariAsync();
            btnFiltreTemizle.Click += (s, e) => ClearFilters();
=======

        private void RegisterEvents()
        {
            cmbOgrenciler.SelectedIndexChanged += async (s, e) => await LoadOgrenciDosyalariAsync();
            btnDosyaSec.Click += BtnDosyaSec_Click;
            btnYukle.Click += async (s, e) => await YukleAsync();
            btnIndir.Click += async (s, e) => await IndirAsync(true);
            btnMasaustuIndir.Click += async (s, e) => await IndirAsync(false);
            btnKopyala.Click += (s, e) => CopySelectedFileName();
            btnYenile.Click += async (s, e) => await LoadOgrenciDosyalariAsync();
            btnFiltreTemizle.Click += (s, e) => ClearFilters();

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            txtAra.TextChanged += (s, e) => ApplyFilters();
            cmbTur.SelectedIndexChanged += (s, e) => ApplyFilters();
            chkTarih.CheckedChanged += (s, e) =>
            {
                UpdateDateFilterEnabled();
                ApplyFilters();
            };
            dtBaslangic.ValueChanged += (s, e) => ApplyFilters();
            dtBitis.ValueChanged += (s, e) => ApplyFilters();
<<<<<<< HEAD
            dgvDosyalar.SelectionChanged += (s, e) => UpdateSelectedCard();
            dgvDosyalar.CellDoubleClick += async(s, e) =>
            {
                if (e.RowIndex >= 0)
                await IndirAsync(true);
            };
            dgvDosyalar.CellMouseDown += DgvDosyalar_CellMouseDown;
            menuIndir.Click += async(s, e) => await IndirAsync(true);
            menuMasaustuneIndir.Click += async(s, e) => await IndirAsync(false);
            menuDosyaAdiniKopyala.Click += (s, e) => CopySelectedFileName();
            menuYenile.Click += async(s, e) => await LoadOgrenciDosyalariAsync();
            FormClosed += (s, e) => httpClient.Dispose();
        }
=======

            dgvDosyalar.SelectionChanged += (s, e) => UpdateSelectedCard();
            dgvDosyalar.CellDoubleClick += async (s, e) =>
            {
                if (e.RowIndex >= 0)
                    await IndirAsync(true);
            };
            dgvDosyalar.CellMouseDown += DgvDosyalar_CellMouseDown;

            menuIndir.Click += async (s, e) => await IndirAsync(true);
            menuMasaustuneIndir.Click += async (s, e) => await IndirAsync(false);
            menuDosyaAdiniKopyala.Click += (s, e) => CopySelectedFileName();
            menuYenile.Click += async (s, e) => await LoadOgrenciDosyalariAsync();

            FormClosed += (s, e) => httpClient.Dispose();
        }

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
        private void ConfigureGrid()
        {
            dgvDosyalar.AutoGenerateColumns = false;
            dgvDosyalar.DataSource = dosyaBindingSource;
            dgvDosyalar.Columns.Clear();
<<<<<<< HEAD
=======

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            dgvDosyalar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = nameof(DosyaArsivModel.DosyaAdi),
                HeaderText = "Dosya Adı",
                FillWeight = 46,
                MinimumWidth = 240
            });
<<<<<<< HEAD
=======

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            dgvDosyalar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = nameof(DosyaArsivModel.DosyaTipi),
                HeaderText = "Tür",
                FillWeight = 16,
                MinimumWidth = 110
            });
<<<<<<< HEAD
=======

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            dgvDosyalar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = nameof(DosyaArsivModel.Uzanti),
                HeaderText = "Uzantı",
                FillWeight = 10,
                MinimumWidth = 85
            });
<<<<<<< HEAD
=======

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            dgvDosyalar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = nameof(DosyaArsivModel.Eklenme),
                HeaderText = "Eklenme Tarihi",
                FillWeight = 18,
                MinimumWidth = 150
            });
        }
<<<<<<< HEAD
=======

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
        private void ConfigureFilters()
        {
            cmbTur.Items.Clear();
            cmbTur.Items.AddRange(new object[]
            {
                "Tümü",
                "PDF",
                "Word",
                "Excel",
                "Resim",
                "Sunum",
                "Sıkıştırılmış",
                "Diğer"
            });
            cmbTur.SelectedIndex = 0;
<<<<<<< HEAD
            dtBaslangic.Value = DateTime.Today.AddMonths( - 1);
            dtBitis.Value = DateTime.Today;
        }
=======

            dtBaslangic.Value = DateTime.Today.AddMonths(-1);
            dtBitis.Value = DateTime.Today;
        }

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
        private void ConfigureDragDrop()
        {
            grpUpload.AllowDrop = true;
            txtDosyaYolu.AllowDrop = true;
            lblDropHint.AllowDrop = true;
<<<<<<< HEAD
            grpUpload.DragEnter += Upload_DragEnter;
            txtDosyaYolu.DragEnter += Upload_DragEnter;
            lblDropHint.DragEnter += Upload_DragEnter;
=======

            grpUpload.DragEnter += Upload_DragEnter;
            txtDosyaYolu.DragEnter += Upload_DragEnter;
            lblDropHint.DragEnter += Upload_DragEnter;

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            grpUpload.DragDrop += Upload_DragDrop;
            txtDosyaYolu.DragDrop += Upload_DragDrop;
            lblDropHint.DragDrop += Upload_DragDrop;
        }
        private void LoadOgrenciler()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var dt = new DataTable();
                var da = new SqlDataAdapter(
<<<<<<< HEAD
                @"SELECT 
=======
                    @"SELECT 
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
                          Id,
                          CASE 
                              WHEN ISNULL(StudentCode, '') LIKE 'PRSARSIV-%'
                                  THEN '[Personel] ' + ISNULL(Name, '') + ' ' + ISNULL(Surname, '')
                              ELSE ISNULL(Name, '') + ' ' + ISNULL(Surname, '')
                          END AS AdSoyad
                      FROM Aysstudents
                      WHERE SchoolId = dbo.GetSirketIdByUserId(@UserId)
                        AND Id = @Id
<<<<<<< HEAD
                      ORDER BY Name",
                conn);
=======
                      ORDER BY Name", conn);

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
                da.SelectCommand.Parameters.AddWithValue("@UserId", UserId);
                da.SelectCommand.Parameters.AddWithValue("@Id", OgrenciId);
                da.Fill(dt);
                var items = new List<OgrenciCombo>();

                foreach (DataRow dr in dt.Rows)
                {
                    items.Add(new OgrenciCombo()
                    {
                        Id = dr["Id"].ToString() ?? string.Empty,
                        AdSoyad = dr["AdSoyad"].ToString() ?? string.Empty
                    });
                }
<<<<<<< HEAD
                cmbOgrenciler.DisplayMember = nameof(OgrenciCombo.AdSoyad);
                cmbOgrenciler.ValueMember = nameof(OgrenciCombo.Id);
                cmbOgrenciler.DataSource = items;
                if (items.Count == 0)
                SetStatus("Bu arşiv için kayıt bulunamadı.");
            }
        }
        private async Task LoadOgrenciDosyalariAsync(bool force = false)
        {
            if ((isBusy && !force) || cmbOgrenciler.SelectedValue == null)
            return;
            string ogrenciId = cmbOgrenciler.SelectedValue.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(ogrenciId))
            return;
            SetBusy(true, "Dosyalar alınıyor...");
            try
            {
                using var resp = await httpClient.GetAsync($"{ApiBaseUrl}/ogrenci/{ogrenciId}");
=======

                cmbOgrenciler.DisplayMember = nameof(OgrenciCombo.AdSoyad);
                cmbOgrenciler.ValueMember = nameof(OgrenciCombo.Id);
                cmbOgrenciler.DataSource = items;

                if (items.Count == 0)
                    SetStatus("Bu arşiv için kayıt bulunamadı.");
            }
        }

        private async Task LoadOgrenciDosyalariAsync(bool force = false)
        {
            if ((isBusy && !force) || cmbOgrenciler.SelectedValue == null)
                return;

            string ogrenciId = cmbOgrenciler.SelectedValue.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(ogrenciId))
                return;

            SetBusy(true, "Dosyalar alınıyor...");

            try
            {
                var resp = await httpClient.GetAsync($"{ApiBaseUrl}/ogrenci/{ogrenciId}");

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
                if (!resp.IsSuccessStatusCode)
                {
                    tumDosyalar = new List<DosyaArsivModel>();
                    ApplyFilters();
                    MessageBox.Show("Dosyalar alınamadı: " + resp.ReasonPhrase, "Arşiv", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SetStatus("Dosyalar alınamadı.");
                    return;
                }
<<<<<<< HEAD
                var json = await resp.Content.ReadAsStringAsync();
                var dosyalar = JsonConvert.DeserializeObject<List<DosyaArsivModel>>(json) ?? new List<DosyaArsivModel>();
                tumDosyalar = dosyalar
                .OrderByDescending(x => x.EklenmeTarihi)
                .ThenBy(x => x.DosyaAdi)
                .ToList();
                if (IsDisposed) return;
                ApplyFilters();
                SetStatus($"{tumDosyalar.Count} dosya listelendi.");
            }
            catch (Exception ex)
            {
                if (IsDisposed) return;
                MessageBox.Show("Arşiv dosyaları alınırken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Arşiv yüklenirken hata oluştu.");
            }
            finally
            {
                SetBusy(false);
=======

                var json = await resp.Content.ReadAsStringAsync();
                var dosyalar = JsonConvert.DeserializeObject<List<DosyaArsivModel>>(json) ?? new List<DosyaArsivModel>();

                tumDosyalar = dosyalar
                    .OrderByDescending(x => x.EklenmeTarihi)
                    .ThenBy(x => x.DosyaAdi)
                    .ToList();

                ApplyFilters();
                SetStatus($"{tumDosyalar.Count} dosya listelendi.");
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arşiv dosyaları alınırken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Arşiv yüklenirken hata oluştu.");
            }
            finally
            {
                SetBusy(false);
            }
        }

        private void ApplyFilters()
        {
            IEnumerable<DosyaArsivModel> query = tumDosyalar;
            string search = (txtAra.Text ?? string.Empty).Trim();
            string selectedType = cmbTur.SelectedItem?.ToString() ?? "Tümü";

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    (x.DosyaAdi ?? string.Empty).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (x.Uzanti ?? string.Empty).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (x.DosyaTipi ?? string.Empty).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.Equals(selectedType, "Tümü", StringComparison.OrdinalIgnoreCase))
                query = query.Where(x => string.Equals(x.DosyaTipi, selectedType, StringComparison.OrdinalIgnoreCase));

            if (chkTarih.Checked)
            {
                DateTime baslangic = dtBaslangic.Value.Date;
                DateTime bitis = dtBitis.Value.Date.AddDays(1).AddTicks(-1);

                if (baslangic <= bitis)
                    query = query.Where(x => x.EklenmeTarihi >= baslangic && x.EklenmeTarihi <= bitis);
            }

            var filtered = query.ToList();
            dosyaBindingSource.DataSource = filtered;
            UpdateSummary(filtered.Count);
        }

        private void UpdateSummary(int? filteredCount = null)
        {
            int visibleCount = filteredCount ?? (dosyaBindingSource.DataSource as List<DosyaArsivModel>)?.Count ?? 0;
            lblTotalValue.Text = $"{visibleCount} / {tumDosyalar.Count}";

            var lastFile = tumDosyalar.OrderByDescending(x => x.EklenmeTarihi).FirstOrDefault();
            lblLastValue.Text = lastFile == null || lastFile.EklenmeTarihi == default
                ? "-"
                : lastFile.EklenmeTarihi.ToString("dd.MM.yyyy HH:mm");

            UpdateSelectedCard();
        }

        private void UpdateSelectedCard()
        {
            var selected = GetSelectedFile();
            lblSelectedValue.Text = selected == null ? "Seçili dosya yok" : selected.DosyaAdi;
        }

        private void ClearFilters()
        {
            txtAra.Clear();
            cmbTur.SelectedIndex = 0;
            chkTarih.Checked = false;
            dtBaslangic.Value = DateTime.Today.AddMonths(-1);
            dtBitis.Value = DateTime.Today;
            ApplyFilters();
        }

        private void UpdateDateFilterEnabled()
        {
            dtBaslangic.Enabled = chkTarih.Checked;
            dtBitis.Enabled = chkTarih.Checked;
        }
        private void ApplyFilters()
        {
            IEnumerable<DosyaArsivModel> query = tumDosyalar;
            string search = (txtAra.Text ?? string.Empty).Trim();
            string selectedType = cmbTur.SelectedItem?.ToString() ?? "Tümü";
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                (x.DosyaAdi ?? string.Empty).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                (x.Uzanti ?? string.Empty).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                (x.DosyaTipi ?? string.Empty).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            if (!string.Equals(selectedType, "Tümü", StringComparison.OrdinalIgnoreCase))
            query = query.Where(x => string.Equals(x.DosyaTipi, selectedType, StringComparison.OrdinalIgnoreCase));
            if (chkTarih.Checked)
            {
                DateTime baslangic = dtBaslangic.Value.Date;
                DateTime bitis = dtBitis.Value.Date.AddDays(1).AddTicks( - 1);
                if (baslangic <= bitis)
                query = query.Where(x => x.EklenmeTarihi >= baslangic && x.EklenmeTarihi <= bitis);
            }
            var filtered = query.ToList();
            dosyaBindingSource.DataSource = filtered;
            UpdateSummary(filtered.Count);
        }
        private void UpdateSummary(int? filteredCount = null)
        {
            int visibleCount = filteredCount ?? (dosyaBindingSource.DataSource as List<DosyaArsivModel>)?.Count ?? 0;
            lblTotalValue.Text = $"{visibleCount} / {tumDosyalar.Count}";
            var lastFile = tumDosyalar.OrderByDescending(x => x.EklenmeTarihi).FirstOrDefault();
            lblLastValue.Text = lastFile == null || lastFile.EklenmeTarihi == default
            ? "-"
            : lastFile.EklenmeTarihi.ToString("dd.MM.yyyy HH:mm");
            UpdateSelectedCard();
        }
        private void UpdateSelectedCard()
        {
            var selected = GetSelectedFile();
            lblSelectedValue.Text = selected == null ? "Seçili dosya yok": selected.DosyaAdi;
        }
        private void ClearFilters()
        {
            txtAra.Clear();
            cmbTur.SelectedIndex = 0;
            chkTarih.Checked = false;
            dtBaslangic.Value = DateTime.Today.AddMonths( - 1);
            dtBitis.Value = DateTime.Today;
            ApplyFilters();
        }
        private void UpdateDateFilterEnabled()
        {
            dtBaslangic.Enabled = chkTarih.Checked;
            dtBitis.Enabled = chkTarih.Checked;
        }
        private void BtnDosyaSec_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Arşive yüklenecek dosyayı seç";
                ofd.Filter = "Tüm dosyalar|*.*|PDF|*.pdf|Word|*.doc;*.docx|Excel|*.xls;*.xlsx|Resim|*.jpg;*.jpeg;*.png;*.webp";
                ofd.Multiselect = false;
<<<<<<< HEAD
                if (ofd.ShowDialog() == DialogResult.OK)
                SetSelectedUploadFile(ofd.FileName);
            }
        }
        private async Task YukleAsync()
        {
            if (isBusy) return;
=======

                if (ofd.ShowDialog() == DialogResult.OK)
                    SetSelectedUploadFile(ofd.FileName);
            }
        }

        private async Task YukleAsync()
        {
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            if (cmbOgrenciler.SelectedValue == null)
            {
                MessageBox.Show("Lütfen kayıt seçin.", "Arşiv", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
<<<<<<< HEAD
            string ogrenciId = cmbOgrenciler.SelectedValue.ToString() ?? string.Empty;
            string dosyaYolu = txtDosyaYolu.Text;
=======

            string ogrenciId = cmbOgrenciler.SelectedValue.ToString() ?? string.Empty;
            string dosyaYolu = txtDosyaYolu.Text;

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            if (string.IsNullOrWhiteSpace(dosyaYolu))
            {
                MessageBox.Show("Lütfen yüklenecek dosyayı seçin.", "Arşiv", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
<<<<<<< HEAD
=======

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            if (!File.Exists(dosyaYolu))
            {
                MessageBox.Show("Seçilen dosya bulunamadı.", "Arşiv", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
<<<<<<< HEAD
            SetBusy(true, "Dosya yükleniyor...");
=======

            SetBusy(true, "Dosya yükleniyor...");

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            try
            {
                using (var form = new MultipartFormDataContent())
                using (var fs = File.OpenRead(dosyaYolu))
                {
                    form.Add(new StringContent(ogrenciId), "ogrenciId");
                    form.Add(new StreamContent(fs), "dosya", Path.GetFileName(dosyaYolu));
<<<<<<< HEAD
                    using var resp = await httpClient.PostAsync($"{ApiBaseUrl}/yukle", form);
=======

                    var resp = await httpClient.PostAsync($"{ApiBaseUrl}/yukle", form);

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
                    if (resp.IsSuccessStatusCode)
                    {
                        txtDosyaYolu.Clear();
                        lblDropHint.Text = "Dosyayı buraya sürükleyip bırakabilirsin.";
                        await LoadOgrenciDosyalariAsync(true);
                        MessageBox.Show("Dosya başarıyla yüklendi.", "Arşiv", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetStatus("Dosya yüklendi.");
                    }
                    else
                    {
                        MessageBox.Show("Yükleme hatası: " + resp.ReasonPhrase, "Arşiv", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SetStatus("Yükleme başarısız.");
                    }
                }
            }
            catch (Exception ex)
            {
<<<<<<< HEAD
                if (IsDisposed) return;
=======
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
                MessageBox.Show("Yükleme sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Yükleme sırasında hata oluştu.");
            }
            finally
            {
                SetBusy(false);
            }
        }
<<<<<<< HEAD
        private async Task IndirAsync(bool askPath)
        {
            if (isBusy) return;
=======

        private async Task IndirAsync(bool askPath)
        {
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            var selected = GetSelectedFile();
            if (selected == null)
            {
                MessageBox.Show("Lütfen indirilecek dosyayı seçin.", "Arşiv", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
<<<<<<< HEAD
            SetBusy(true, "Dosya indiriliyor...");
            try
            {
                using var resp = await httpClient.GetAsync($"{ApiBaseUrl}/indir/{selected.Id}");
=======

            SetBusy(true, "Dosya indiriliyor...");

            try
            {
                var resp = await httpClient.GetAsync($"{ApiBaseUrl}/indir/{selected.Id}");

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
                if (!resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("İndirme hatası: " + resp.ReasonPhrase, "Arşiv", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SetStatus("İndirme başarısız.");
                    return;
                }
<<<<<<< HEAD
                byte[] data = await resp.Content.ReadAsByteArrayAsync();
                string targetPath = askPath ? AskSavePath(selected.DosyaAdi): BuildDesktopPath(selected.DosyaAdi);
=======

                byte[] data = await resp.Content.ReadAsByteArrayAsync();
                string targetPath = askPath ? AskSavePath(selected.DosyaAdi) : BuildDesktopPath(selected.DosyaAdi);

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
                if (string.IsNullOrWhiteSpace(targetPath))
                {
                    SetStatus("İndirme iptal edildi.");
                    return;
                }
<<<<<<< HEAD
                File.WriteAllBytes(targetPath, data);
                SetStatus("Dosya indirildi: " + targetPath);
                var result = MessageBox.Show("Dosya indirildi. Klasörde göstermek ister misiniz?", "Arşiv", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                ShowInExplorer(targetPath);
            }
            catch (Exception ex)
            {
                if (IsDisposed) return;
=======

                File.WriteAllBytes(targetPath, data);
                SetStatus("Dosya indirildi: " + targetPath);

                var result = MessageBox.Show("Dosya indirildi. Klasörde göstermek ister misiniz?", "Arşiv", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                    ShowInExplorer(targetPath);
            }
            catch (Exception ex)
            {
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
                MessageBox.Show("İndirme sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("İndirme sırasında hata oluştu.");
            }
            finally
            {
                SetBusy(false);
            }
        }
<<<<<<< HEAD
=======

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
        private string AskSavePath(string fileName)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Title = "Dosyayı kaydet";
                sfd.FileName = MakeSafeFileName(fileName);
                sfd.Filter = "Tüm dosyalar|*.*";
                sfd.OverwritePrompt = true;
<<<<<<< HEAD
                return sfd.ShowDialog() == DialogResult.OK ? sfd.FileName: string.Empty;
            }
        }
=======

                return sfd.ShowDialog() == DialogResult.OK ? sfd.FileName : string.Empty;
            }
        }

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
        private string BuildDesktopPath(string fileName)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string safeName = MakeSafeFileName(fileName);
            string path = Path.Combine(desktop, safeName);
<<<<<<< HEAD
            if (!File.Exists(path))
            return path;
            string name = Path.GetFileNameWithoutExtension(safeName);
            string ext = Path.GetExtension(safeName);
            for (int i = 1; i<1000; i++)
            {
                string candidate = Path.Combine(desktop, $"{name} ({i}){ext}");
                if (!File.Exists(candidate))
                return candidate;
            }
            return Path.Combine(desktop, $"{name}_{DateTime.Now:yyyyMMddHHmmss}{ext}");
        }
        private string MakeSafeFileName(string fileName)
        {
            string safeName = string.IsNullOrWhiteSpace(fileName) ? "arsiv-dosyasi": fileName;
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            safeName = safeName.Replace(invalidChar, '_');
            return safeName;
        }
        private DosyaArsivModel? GetSelectedFile()
        {
            if (dgvDosyalar.CurrentRow?.DataBoundItem is DosyaArsivModel current)
            return current;
            if (dgvDosyalar.SelectedRows.Count> 0 && dgvDosyalar.SelectedRows[0].DataBoundItem is DosyaArsivModel selected)
            return selected;
            return null;
        }
=======

            if (!File.Exists(path))
                return path;

            string name = Path.GetFileNameWithoutExtension(safeName);
            string ext = Path.GetExtension(safeName);

            for (int i = 1; i < 1000; i++)
            {
                string candidate = Path.Combine(desktop, $"{name} ({i}){ext}");
                if (!File.Exists(candidate))
                    return candidate;
            }

            return Path.Combine(desktop, $"{name}_{DateTime.Now:yyyyMMddHHmmss}{ext}");
        }

        private string MakeSafeFileName(string fileName)
        {
            string safeName = string.IsNullOrWhiteSpace(fileName) ? "arsiv-dosyasi" : fileName;
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
                safeName = safeName.Replace(invalidChar, '_');
            return safeName;
        }

        private DosyaArsivModel? GetSelectedFile()
        {
            if (dgvDosyalar.CurrentRow?.DataBoundItem is DosyaArsivModel current)
                return current;

            if (dgvDosyalar.SelectedRows.Count > 0 && dgvDosyalar.SelectedRows[0].DataBoundItem is DosyaArsivModel selected)
                return selected;

            return null;
        }

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
        private void CopySelectedFileName()
        {
            var selected = GetSelectedFile();
            if (selected == null)
            {
                MessageBox.Show("Kopyalanacak dosyayı seçin.", "Arşiv", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
<<<<<<< HEAD
            Clipboard.SetText(selected.DosyaAdi ?? string.Empty);
            SetStatus("Dosya adı kopyalandı.");
        }
        private void SetSelectedUploadFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            return;
=======

            Clipboard.SetText(selected.DosyaAdi ?? string.Empty);
            SetStatus("Dosya adı kopyalandı.");
        }

        private void SetSelectedUploadFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return;

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            txtDosyaYolu.Text = filePath;
            var info = new FileInfo(filePath);
            lblDropHint.Text = $"Seçildi: {info.Name} • {FormatBytes(info.Length)}";
        }
<<<<<<< HEAD
        private string FormatBytes(long bytes)
        {
            string[] suffixes =
            {
                "B",
                "KB",
                "MB",
                "GB"
            };
            double size = bytes;
            int suffixIndex = 0;
            while (size >= 1024 && suffixIndex<suffixes.Length - 1)
=======

        private string FormatBytes(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB" };
            double size = bytes;
            int suffixIndex = 0;

            while (size >= 1024 && suffixIndex < suffixes.Length - 1)
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            {
                size /= 1024;
                suffixIndex++;
            }
<<<<<<< HEAD
            return $"{size:0.##} {suffixes[suffixIndex]}";
        }
        private void SetBusy(bool busy, string? message = null)
        {
            if (IsDisposed) return;
            isBusy = busy;
            cmbOgrenciler.Enabled = !busy;
            archiveRibbon?.RefreshCommands();
=======

            return $"{size:0.##} {suffixes[suffixIndex]}";
        }

        private void SetBusy(bool busy, string? message = null)
        {
            isBusy = busy;
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            progressBar.Visible = busy;
            btnYukle.Enabled = !busy;
            btnIndir.Enabled = !busy;
            btnMasaustuIndir.Enabled = !busy;
            btnYenile.Enabled = !busy;
            btnDosyaSec.Enabled = !busy;
<<<<<<< HEAD
            Cursor = busy ? Cursors.WaitCursor: Cursors.Default;
            if (!string.IsNullOrWhiteSpace(message))
            SetStatus(message);
        }
        private void SetStatus(string message)
        {
            if (!IsDisposed) lblStatus.Text = message;
        }
        private void Upload_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;
            else
            e.Effect = DragDropEffects.None;
        }
        private void Upload_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data == null || !e.Data.GetDataPresent(DataFormats.FileDrop))
            return;
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files == null || files.Length == 0)
            return;
            SetSelectedUploadFile(files[0]);
        }
=======
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;

            if (!string.IsNullOrWhiteSpace(message))
                SetStatus(message);
        }

        private void SetStatus(string message)
        {
            lblStatus.Text = message;
        }

        private void Upload_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Upload_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data == null || !e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            var files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files == null || files.Length == 0)
                return;

            SetSelectedUploadFile(files[0]);
        }

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
        private void DgvDosyalar_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvDosyalar.ClearSelection();
                dgvDosyalar.Rows[e.RowIndex].Selected = true;
                dgvDosyalar.CurrentCell = dgvDosyalar.Rows[e.RowIndex].Cells[0];
            }
        }
<<<<<<< HEAD
=======

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
        private void ShowInExplorer(string filePath)
        {
            try
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = "explorer.exe",
                    Arguments = $"/select,\"{filePath}\"",
                    UseShellExecute = true
                });
            }
            catch
            {
                // Explorer açılamazsa indirme işlemini bozmayalım.
            }
        }
        class OgrenciCombo
        {
<<<<<<< HEAD
            public string Id
            {
                get;
                set;
            }
            = string.Empty;
            public string AdSoyad
            {
                get;
                set;
            }
            = string.Empty;
=======
            public string Id { get; set; } = string.Empty;
            public string AdSoyad { get; set; } = string.Empty;
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            public override string ToString() => AdSoyad;
        }

        public class DosyaArsivModel
        {
<<<<<<< HEAD
            public int Id
            {
                get;
                set;
            }
            public string DosyaAdi
            {
                get;
                set;
            }
            = string.Empty;
            public string DosyaYolu
            {
                get;
                set;
            }
            = string.Empty;
            public DateTime EklenmeTarihi
            {
                get;
                set;
            }
=======
            public int Id { get; set; }
            public string DosyaAdi { get; set; } = string.Empty;
            public string DosyaYolu { get; set; } = string.Empty;
            public DateTime EklenmeTarihi { get; set; }

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            public string Uzanti
            {
                get
                {
                    string ext = Path.GetExtension(DosyaAdi ?? string.Empty);
<<<<<<< HEAD
                    return string.IsNullOrWhiteSpace(ext) ? "-": ext.TrimStart('.').ToUpperInvariant();
                }
            }
=======
                    return string.IsNullOrWhiteSpace(ext) ? "-" : ext.TrimStart('.').ToUpperInvariant();
                }
            }

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            public string DosyaTipi
            {
                get
                {
                    string ext = Path.GetExtension(DosyaAdi ?? string.Empty).ToLowerInvariant();
<<<<<<< HEAD
                    switch (ext)
                    {
                        case ".pdf":
                        return "PDF";
                        case ".doc":
                        case ".docx":
                        return "Word";
                        case ".xls":
                        case ".xlsx":
                        case ".csv":
                        return "Excel";
=======

                    switch (ext)
                    {
                        case ".pdf":
                            return "PDF";
                        case ".doc":
                        case ".docx":
                            return "Word";
                        case ".xls":
                        case ".xlsx":
                        case ".csv":
                            return "Excel";
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
                        case ".jpg":
                        case ".jpeg":
                        case ".png":
                        case ".webp":
                        case ".gif":
<<<<<<< HEAD
                        return "Resim";
                        case ".ppt":
                        case ".pptx":
                        return "Sunum";
                        case ".zip":
                        case ".rar":
                        case ".7z":
                        return "Sıkıştırılmış";
                        default:
                        return "Diğer";
                    }
                }
            }
=======
                            return "Resim";
                        case ".ppt":
                        case ".pptx":
                            return "Sunum";
                        case ".zip":
                        case ".rar":
                        case ".7z":
                            return "Sıkıştırılmış";
                        default:
                            return "Diğer";
                    }
                }
            }

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            public string Eklenme
            {
                get
                {
<<<<<<< HEAD
                    return EklenmeTarihi == default ? "-": EklenmeTarihi.ToString("dd.MM.yyyy HH:mm");
=======
                    return EklenmeTarihi == default ? "-" : EklenmeTarihi.ToString("dd.MM.yyyy HH:mm");
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
                }
            }
        }
    }
}
