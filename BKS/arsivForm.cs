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
        private static string ApiBaseUrl => AppConfiguration.Api("api/dosya-arsiv").AbsoluteUri;
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
        private GridColumnMenu _columnMenu = null!;
        public arsivForm() : this(Guid.Empty, "", Guid.Empty) { }

        public arsivForm(Guid userId, string connStr, Guid ogrenciId)
        {
            UserId = userId;
            connectionString = connStr;
            OgrenciId = ogrenciId;
            InitializeComponent();
            Screens.PrepareDesignerForm(this);
            GridAppearance.Apply(dgvDosyalar);
            _columnMenu = new GridColumnMenu(dgvDosyalar, GridFilterController.For(dgvDosyalar));
            ListSurface.RegisterSearch(dgvDosyalar, txtAra);
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
        private void Columns_Click(object? sender, EventArgs e) => _columnMenu.ShowColumnChooser(btnColumns);

        private void ConfigureScreen()
        {
            ConfigureGrid();
            ConfigureFilters();
            ConfigureDragDrop();
            UpdateDateFilterEnabled();
            UpdateSummary();
            SetStatus("Hazır.");
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
            txtAra.TextChanged += (s, e) => ApplyFilters();
            cmbTur.SelectedIndexChanged += (s, e) => ApplyFilters();
            chkTarih.CheckedChanged += (s, e) =>
            {
                UpdateDateFilterEnabled();
                ApplyFilters();
            };
            dtBaslangic.ValueChanged += (s, e) => ApplyFilters();
            dtBitis.ValueChanged += (s, e) => ApplyFilters();
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
        private void ConfigureGrid()
        {
            dgvDosyalar.AutoGenerateColumns = false;
            dgvDosyalar.DataSource = dosyaBindingSource;
        }
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
            dtBaslangic.Value = DateTime.Today.AddMonths( - 1);
            dtBitis.Value = DateTime.Today;
        }
        private void ConfigureDragDrop()
        {
            grpUpload.AllowDrop = true;
            txtDosyaYolu.AllowDrop = true;
            lblDropHint.AllowDrop = true;
            grpUpload.DragEnter += Upload_DragEnter;
            txtDosyaYolu.DragEnter += Upload_DragEnter;
            lblDropHint.DragEnter += Upload_DragEnter;
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
                @"SELECT 
                          Id,
                          CASE 
                              WHEN ISNULL(StudentCode, '') LIKE 'PRSARSIV-%'
                                  THEN '[Personel] ' + ISNULL(Name, '') + ' ' + ISNULL(Surname, '')
                              ELSE ISNULL(Name, '') + ' ' + ISNULL(Surname, '')
                          END AS AdSoyad
                      FROM Aysstudents
                      WHERE SchoolId = dbo.GetSirketIdByUserId(@UserId)
                        AND Id = @Id
                      ORDER BY Name",
                conn);
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
                if (!resp.IsSuccessStatusCode)
                {
                    tumDosyalar = new List<DosyaArsivModel>();
                    ApplyFilters();
                    MessageBox.Show("Dosyalar alınamadı: " + resp.ReasonPhrase, "Arşiv", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SetStatus("Dosyalar alınamadı.");
                    return;
                }
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
                DateTime bitis = dtBitis.Value.Date.AddDays(1).AddTicks( - 1);
                if (baslangic <= bitis)
                query = query.Where(x => x.EklenmeTarihi >= baslangic && x.EklenmeTarihi <= bitis);
            }
            var filtered = query.ToList();
            dosyaBindingSource.DataSource = new GridItems<DosyaArsivModel>(filtered);
            UpdateSummary(filtered.Count);
        }
        private void UpdateSummary(int? filteredCount = null)
        {
            int visibleCount = filteredCount ?? (dosyaBindingSource.DataSource as System.Collections.ICollection)?.Count ?? 0;
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
            GridFilterController.For(dgvDosyalar).Clear();
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
                if (ofd.ShowDialog() == DialogResult.OK)
                SetSelectedUploadFile(ofd.FileName);
            }
        }
        private async Task YukleAsync()
        {
            if (isBusy) return;
            if (cmbOgrenciler.SelectedValue == null)
            {
                MessageBox.Show("Lütfen kayıt seçin.", "Arşiv", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string ogrenciId = cmbOgrenciler.SelectedValue.ToString() ?? string.Empty;
            string dosyaYolu = txtDosyaYolu.Text;
            if (string.IsNullOrWhiteSpace(dosyaYolu))
            {
                MessageBox.Show("Lütfen yüklenecek dosyayı seçin.", "Arşiv", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!File.Exists(dosyaYolu))
            {
                MessageBox.Show("Seçilen dosya bulunamadı.", "Arşiv", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetBusy(true, "Dosya yükleniyor...");
            try
            {
                using (var form = new MultipartFormDataContent())
                using (var fs = File.OpenRead(dosyaYolu))
                {
                    form.Add(new StringContent(ogrenciId), "ogrenciId");
                    form.Add(new StreamContent(fs), "dosya", Path.GetFileName(dosyaYolu));
                    using var resp = await httpClient.PostAsync($"{ApiBaseUrl}/yukle", form);
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
                if (IsDisposed) return;
                MessageBox.Show("Yükleme sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Yükleme sırasında hata oluştu.");
            }
            finally
            {
                SetBusy(false);
            }
        }
        private async Task IndirAsync(bool askPath)
        {
            if (isBusy) return;
            var selected = GetSelectedFile();
            if (selected == null)
            {
                MessageBox.Show("Lütfen indirilecek dosyayı seçin.", "Arşiv", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetBusy(true, "Dosya indiriliyor...");
            try
            {
                using var resp = await httpClient.GetAsync($"{ApiBaseUrl}/indir/{selected.Id}");
                if (!resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("İndirme hatası: " + resp.ReasonPhrase, "Arşiv", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SetStatus("İndirme başarısız.");
                    return;
                }
                byte[] data = await resp.Content.ReadAsByteArrayAsync();
                string targetPath = askPath ? AskSavePath(selected.DosyaAdi): BuildDesktopPath(selected.DosyaAdi);
                if (string.IsNullOrWhiteSpace(targetPath))
                {
                    SetStatus("İndirme iptal edildi.");
                    return;
                }
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
                MessageBox.Show("İndirme sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("İndirme sırasında hata oluştu.");
            }
            finally
            {
                SetBusy(false);
            }
        }
        private string AskSavePath(string fileName)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Title = "Dosyayı kaydet";
                sfd.FileName = MakeSafeFileName(fileName);
                sfd.Filter = "Tüm dosyalar|*.*";
                sfd.OverwritePrompt = true;
                return sfd.ShowDialog() == DialogResult.OK ? sfd.FileName: string.Empty;
            }
        }
        private string BuildDesktopPath(string fileName)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string safeName = MakeSafeFileName(fileName);
            string path = Path.Combine(desktop, safeName);
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
        private void CopySelectedFileName()
        {
            var selected = GetSelectedFile();
            if (selected == null)
            {
                MessageBox.Show("Kopyalanacak dosyayı seçin.", "Arşiv", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Clipboard.SetText(selected.DosyaAdi ?? string.Empty);
            SetStatus("Dosya adı kopyalandı.");
        }
        private void SetSelectedUploadFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            return;
            txtDosyaYolu.Text = filePath;
            var info = new FileInfo(filePath);
            lblDropHint.Text = $"Seçildi: {info.Name} • {FormatBytes(info.Length)}";
        }
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
            {
                size /= 1024;
                suffixIndex++;
            }
            return $"{size:0.##} {suffixes[suffixIndex]}";
        }
        private void SetBusy(bool busy, string? message = null)
        {
            if (IsDisposed) return;
            isBusy = busy;
            cmbOgrenciler.Enabled = !busy;
            progressBar.Visible = busy;
            btnYukle.Enabled = !busy;
            btnIndir.Enabled = !busy;
            btnMasaustuIndir.Enabled = !busy;
            btnYenile.Enabled = !busy;
            btnDosyaSec.Enabled = !busy;
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
        private void DgvDosyalar_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvDosyalar.ClearSelection();
                dgvDosyalar.Rows[e.RowIndex].Selected = true;
                var first = dgvDosyalar.Columns.Cast<DataGridViewColumn>().FirstOrDefault(column => column.Visible);
                if (first != null) dgvDosyalar.CurrentCell = dgvDosyalar.Rows[e.RowIndex].Cells[first.Index];
            }
        }
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
            public override string ToString() => AdSoyad;
        }
        public class DosyaArsivModel
        {
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
            public string Uzanti
            {
                get
                {
                    string ext = Path.GetExtension(DosyaAdi ?? string.Empty);
                    return string.IsNullOrWhiteSpace(ext) ? "-": ext.TrimStart('.').ToUpperInvariant();
                }
            }
            public string DosyaTipi
            {
                get
                {
                    string ext = Path.GetExtension(DosyaAdi ?? string.Empty).ToLowerInvariant();
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
                        case ".jpg":
                        case ".jpeg":
                        case ".png":
                        case ".webp":
                        case ".gif":
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
            public string Eklenme
            {
                get
                {
                    return EklenmeTarihi == default ? "-": EklenmeTarihi.ToString("dd.MM.yyyy HH:mm");
                }
            }
        }
    }
}
