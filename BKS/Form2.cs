using Krypton.Ribbon;
using Krypton.Toolkit;
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace BKS
{
    public partial class Form2 : Form
    {
        private const int StudentModuleTag = 4010;
        private const int PersonelModuleTag = 4020;

        public string connectionString = "Server=31.186.11.161;Database=asl2e6ancomtr_PaymentDBDB;User Id=asl2e6ancomtr_aslan;Password=Aslan123.@;TrustServerCertificate=True;";

        private DataGridView aktifDGV;
        private readonly Dictionary<string, KryptonRibbonTab> ribbonTabs = new Dictionary<string, KryptonRibbonTab>(StringComparer.OrdinalIgnoreCase);
        private KryptonRibbon ribbon;

        public Form2()
        {
            InitializeComponent();
            InitRibbon();
            LoadStockComboBox();
            this.Text = "Anaokulu Yönetim Sistemi";
        }

        [System.ComponentModel.Browsable(false)]
        public new System.Windows.Forms.AutoScaleMode AutoScaleMode { get; set; }

        public Guid sinifid { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public byte[] Photo { get; set; }

        public class ModuleResponse
        {
            public List<string> modules { get; set; }
            public string role { get; set; }
        }

        #region Helpers

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }

        private SqlParameter DbParam(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        private DataTable ExecuteDataTable(string query, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = CreateConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = commandType;

                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                conn.Open();
                adapter.Fill(dt);
            }

            return dt;
        }

        private object ExecuteScalarValue(string query, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = CreateConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandType = commandType;

                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                conn.Open();
                return cmd.ExecuteScalar();
            }
        }

        private int ExecuteNonQueryCommand(string query, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = CreateConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandType = commandType;

                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        private string ExecuteStringOrDefault(string query, string defaultValue = "Bilinmiyor", params SqlParameter[] parameters)
        {
            object result = ExecuteScalarValue(query, CommandType.Text, parameters);
            return result == null || result == DBNull.Value ? defaultValue : result.ToString();
        }

        private string GetStudentSearchText()
        {
            return string.IsNullOrWhiteSpace(txtOgrenciYonetimiAra.Text)
                ? null
                : txtOgrenciYonetimiAra.Text.Trim();
        }

        private int GetActiveGridTag()
        {
            if (aktifDGV?.Tag == null)
                return 0;

            return int.TryParse(aktifDGV.Tag.ToString(), out int tag) ? tag : 0;
        }

        private bool IsAdminRole(string role)
        {
            return string.Equals(role, "ADMİN", StringComparison.OrdinalIgnoreCase)
                || string.Equals(role, "ADMIN", StringComparison.OrdinalIgnoreCase);
        }

        private void RefreshStudentGrid()
        {
            dataGridViewStok.DataSource = GetStudentTable(UserId, GetStudentSearchText());
            dataGridViewStok.Refresh();
        }

        private void RefreshPersonelGrid()
        {
            dgvPersonelYonetimi.DataSource = GetPersonelTable(UserId);
            dgvPersonelYonetimi.Refresh();
        }

        private void RefreshActiveGrid()
        {
            switch (GetActiveGridTag())
            {
                case StudentModuleTag:
                    RefreshStudentGrid();
                    break;
                case PersonelModuleTag:
                    RefreshPersonelGrid();
                    break;
            }
        }

        private DataTable GetStudentTable(Guid userId, string ogrenciAdi = null)
        {
            return ExecuteDataTable(
                "EXEC GetStudent @UserId, @Name",
                CommandType.Text,
                DbParam("@UserId", userId),
                DbParam("@Name", string.IsNullOrWhiteSpace(ogrenciAdi) ? DBNull.Value : (object)ogrenciAdi));
        }

        private DataTable GetPersonelTable(Guid userId)
        {
            return ExecuteDataTable(
                "EXEC GetPersonel @UserId",
                CommandType.Text,
                DbParam("@UserId", userId));
        }
        private void LoadComboBoxItems(ComboBox comboBox, string query, Func<SqlDataReader, ComboBoxItem> itemFactory, params SqlParameter[] parameters)
        {
            comboBox.Items.Clear();

            using (SqlConnection conn = CreateConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox.Items.Add(itemFactory(reader));
                    }
                }
            }
        }

        private void SoftDeleteStudent(Guid id)
        {
            ExecuteNonQueryCommand(
                "UPDATE AysStudents SET IsDeleted = 1 WHERE Id = @id",
                CommandType.Text,
                DbParam("@id", id));

            DeleteAndLog("Aysstudents", "Id", id, UserId, "1", "DELETE");
        }

        private void SoftDeletePersonel(Guid id)
        {
            ExecuteNonQueryCommand(
                "UPDATE Personel SET IsActive = 0 WHERE PersonelId = @id",
                CommandType.Text,
                DbParam("@id", id));

            DeleteAndLog("Personel", "PersonelId", id, UserId, "1", "DELETE");
        }

        private bool TryGetSelectedGuid(DataGridView grid, string columnName, out Guid id)
        {
            id = Guid.Empty;

            if (grid?.CurrentRow == null || !grid.Columns.Contains(columnName))
                return false;

            object value = grid.CurrentRow.Cells[columnName].Value;
            if (value == null || value == DBNull.Value)
                return false;

            return Guid.TryParse(value.ToString(), out id);
        }

        #endregion

        #region Ribbon

        private void InitRibbon()
        {
            ribbon = new KryptonRibbon
            {
                Dock = DockStyle.Top,
                PaletteMode = PaletteMode.Office2013White,
            };

            this.Controls.Add(ribbon);
            ribbonTabs.Clear();

            AddRibbonTab("tabPageOgrenciOnKayit", "🎓 Öğrenci Ön Kayıt", "Ön Kayıt İşlemleri", ("Öğrenci Ön Kayıt", () => tabControl.SelectedTab = tabPageOgrenciOnKayit));
            AddRibbonTab("tabPageStok", "📚 Öğrenci Yönetimi", "Öğrenci İşlemleri", ("Öğrenci Yönetimi", () => tabControl.SelectedTab = tabPageStok));
            AddRibbonTab("tabPageSatis", "💰 Ödeme Yönetimi", "Ödeme İşlemleri", ("Ödeme Girişi", () => tabControl.SelectedTab = tabPageSatis));
            AddRibbonTab("tabPagePersonelYonetimi", "👨‍💼 Personel Yönetimi", "Personel İşlemleri", ("Personel Yönetimi", () => tabControl.SelectedTab = tabPagePersonelYonetimi));
            AddRibbonTab("tabPageGelirGider", "📊 Gelir-Gider", "Finans İşlemleri",
                ("Ödeme Yönetimi", () => tabControl.SelectedTab = tabPageGelirGider),
                ("Fatura Merkezi", () => FaturaBtn.PerformClick()));
            AddRibbonTab("tabPageOzelRaporlar", "📈 Özel Raporlar", "Raporlar",
                ("Özel Raporlar", () => tabControl.SelectedTab = tabPageOzelRaporlar),
                ("Özel Rapor Tasarım", ShowOzelRaporForm));

            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.ItemSize = new Size(0, 1);
            tabControl.SizeMode = TabSizeMode.Fixed;
        }

        private void AddRibbonTab(string moduleKey, string tabText, string groupName, params (string, Action)[] buttons)
        {
            var tab = new KryptonRibbonTab { Text = tabText };
            ribbon.RibbonTabs.Add(tab);
            AddGroupWithButtons(tab, groupName, buttons);
            ribbonTabs[moduleKey] = tab;
        }

        private void AddGroupWithButtons(KryptonRibbonTab tab, string groupName, params (string, Action)[] buttons)
        {
            KryptonRibbonGroup group = new KryptonRibbonGroup { TextLine1 = groupName };
            tab.Groups.Add(group);

            foreach (var (text, action) in buttons)
            {
                var triple = new KryptonRibbonGroupTriple();
                var btn = new KryptonRibbonGroupButton
                {
                    ImageSmall = GetButtonIcon(text),
                    ImageLarge = GetButtonIcon(text)
                };

                var parts = text.Split(' ');
                btn.TextLine1 = parts[0];
                btn.TextLine2 = parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : string.Empty;
                btn.Click += (s, e) => action();

                triple.Items.Add(btn);
                group.Items.Add(triple);
            }
        }

        private void ShowOzelRaporForm()
        {
            using (var form = new ÖzelRapor())
            {
                form.ShowDialog();
            }
        }

        private Image ImageFromResource(object resource)
        {
            if (resource is Image img)
                return img;

            if (resource is byte[] bytes)
            {
                using (var ms = new MemoryStream(bytes))
                {
                    return Image.FromStream(ms);
                }
            }

            return null;
        }

        private Image GetButtonIcon(string text)
        {
            switch (text)
            {
                case "Liste": return ImageFromResource(Properties.Resources.icon_liste);
                case "Yeni Kayıt": return ImageFromResource(Properties.Resources.icon_yeniKayit);
                case "Öğrenci Yönetimi": return ImageFromResource(Properties.Resources.icon_ogrenciYonetimi);
                case "Sınıf Ekle": return ImageFromResource(Properties.Resources.icon_sinifEkle);
                case "Ödeme Girişi": return ImageFromResource(Properties.Resources.icon_odemeGirisi);
                case "Ödeme Yönetimi": return ImageFromResource(Properties.Resources.icon_odemeYonetimi);
                case "Fatura Merkezi": return ImageFromResource(Properties.Resources.icon_fatura);
                case "Personel Yönetimi": return ImageFromResource(Properties.Resources.icon_personel);
                case "Özel Raporlar": return ImageFromResource(Properties.Resources.icon_rapor);
                case "Özel Rapor Tasarım": return ImageFromResource(Properties.Resources.icon_rapor);
                case "Görüntüle": return ImageFromResource(Properties.Resources.icon_goruntule);
                case "Öğrenci Ön Kayıt": return ImageFromResource(Properties.Resources.icon_ogrencionkayit);
                default: return null;
            }
        }

        private void SetTabAccess(List<string> activeModules, string role)
        {
            if (string.IsNullOrWhiteSpace(role) || activeModules == null || activeModules.Count == 0)
            {
                foreach (TabPage tab in tabControl.TabPages.Cast<TabPage>().ToList())
                {
                    HideTabPage(tab);
                }
                return;
            }

            if (IsAdminRole(role))
            {
                foreach (TabPage tab in tabControl.TabPages)
                {
                    tab.Enabled = true;
                    ShowTabPage(tab);
                }
                return;
            }

            foreach (TabPage tab in tabControl.TabPages.Cast<TabPage>().ToList())
            {
                bool hasAccess = activeModules.Contains(tab.Name);
                tab.Enabled = hasAccess;

                if (hasAccess)
                    ShowTabPage(tab);
                else
                    HideTabPage(tab);
            }
        }

        private void SetRibbonTabAccess(List<string> activeModules, string role)
        {
            if (string.IsNullOrWhiteSpace(role) || activeModules == null || activeModules.Count == 0)
            {
                foreach (var tab in ribbonTabs.Values)
                {
                    tab.Visible = false;
                }
                return;
            }

            if (IsAdminRole(role))
            {
                foreach (var tab in ribbonTabs.Values)
                {
                    tab.Visible = true;
                }
                return;
            }

            foreach (var pair in ribbonTabs)
            {
                pair.Value.Visible = activeModules.Contains(pair.Key);
            }
        }

        private void ShowTabPage(TabPage tabPage)
        {
            if (!tabControl.TabPages.Contains(tabPage))
                tabControl.TabPages.Add(tabPage);
        }

        private void HideTabPage(TabPage tabPage)
        {
            if (tabControl.TabPages.Contains(tabPage))
                tabControl.TabPages.Remove(tabPage);
        }

        #endregion

        #region Form Events

        private void Form2_Load(object sender, EventArgs e)
        {
            dgvPersonelYonetimi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPersonelYonetimi.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            this.Text = (GetCompanyName(UserId) + " Anaokulu Yönetim Sistemi").ToUpper();
            this.materialLabel3.Text = ("Merhaba " + GetLastUser(UserId) + " Son Giriş Zamanın : " + GetLastLoginTime(UserId)).ToUpper();

            LoadModulesFromApi(UserId, Role);
            LoadOnKayitlar(UserId);
            LoadStockData(UserId);
            LoadPaymentData(UserId);
            LoadTeacherComboBox(UserId);
            PersonelYonetimiLoad(UserId);
            YasGrubuLoad();
            SinifLoad(UserId);
            LoadOzelRaporlarToGrid();

            dataGridViewStok.AllowUserToAddRows = false;
            DgvOgrenciYonetimiSiniflar.AllowUserToAddRows = false;
            dgvPersonelYonetimi.AllowUserToAddRows = false;

            dataGridViewStok.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewStok.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            if (dataGridViewStok.Columns.Contains("Id")) dataGridViewStok.Columns["Id"].Visible = false;
            if (dataGridViewStok.Columns.Contains("MonthlyFee")) dataGridViewStok.Columns["MonthlyFee"].Visible = false;
            if (dataGridViewStok.Columns.Contains("FotoId")) dataGridViewStok.Columns["FotoId"].Visible = false;

            dataGridViewStok.MouseDown += DataGridView_MouseDown;
            dgvPersonelYonetimi.MouseDown += DataGridView_MouseDown;
            DgvOgrenciYonetimiSiniflar.MouseDown += DataGridView_MouseDown;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region API / Auth / User

        private void LoadModulesFromApi(Guid userId, string role)
        {
            try
            {
                string url = $"https://randevu.aslancan.com.tr/api/modules/{userId}?role={role}";

                using (WebClient client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    string json = client.DownloadString(url);

                    var result = JsonConvert.DeserializeObject<ModuleResponse>(json);
                    SetTabAccess(result?.modules, result?.role);
                    SetRibbonTabAccess(result?.modules, result?.role);
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show("Modüller yüklenemedi!\n" + ex.Message);
                SetTabAccess(null, null);
                SetRibbonTabAccess(null, null);
            }
        }

        public string GetLastLoginTime(Guid userId)
        {
            try
            {
                return ExecuteStringOrDefault(
                    "SELECT PreviousLogin FROM CompanyUsers WHERE UserId = @UserId",
                    "Bilinmiyor",
                    DbParam("@UserId", userId));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return "Bilinmiyor";
            }
        }

        public string GetLastUser(Guid userId)
        {
            try
            {
                return ExecuteStringOrDefault(
                    "SELECT Firstname FROM CompanyUsers WHERE UserId = @UserId",
                    "Bilinmiyor",
                    DbParam("@UserId", userId));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return "Bilinmiyor";
            }
        }

        private string GetCompanyName(Guid userId)
        {
            try
            {
                return ExecuteStringOrDefault(
                    @"SELECT ad = (
                          SELECT ce.CompanyName
                          FROM Companies ce
                          WHERE ce.CompanyId = c.CompanyId
                      )
                      FROM CompanyUsers c
                      WHERE c.UserId = @UserId",
                    "Bilinmiyor",
                    DbParam("@UserId", userId));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı hatası: " + ex.Message);
                return "Bilinmiyor";
            }
        }

        #endregion

        #region Öğrenci Yönetimi

        public void LoadStockData(Guid userId)
        {
            dataGridViewStok.DataSource = GetStudentTable(userId, GetStudentSearchText());
            dataGridViewStok.Refresh();
        }

        public DataTable LoadStockDataRefresh(Guid userId)
        {
            return GetStudentTable(userId, GetStudentSearchText());
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            LoadStockData(UserId);
        }

        public void LoadStockComboBox()
        {
            LoadComboBoxItems(
                comboBoxStok,
                "SELECT Id, Name FROM AYSStudents WHERE ISNULL(IsDeleted, 0) = 0 ORDER BY Name",
                reader => new ComboBoxItem
                {
                    Text = reader["Name"].ToString(),
                    Value = reader["Id"].ToString()
                });
        }

        private void LoadStudentClassComboBox(ComboBox targetComboBox, Guid userId)
        {
            if (targetComboBox == null)
                return;

            LoadComboBoxItems(
                targetComboBox,
                "SELECT ClassName, [Group] FROM AYSClasses WHERE SchoolId = (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId) AND ISNULL(IsDeleted,0)=0 ORDER BY ClassName",
                reader => new ComboBoxItem
                {
                    Text = reader["ClassName"].ToString(),
                    Value = reader["Group"].ToString()
                },
                DbParam("@UserId", userId));
        }

        private void btnOgrenciYonetimiSil_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedGuid(dataGridViewStok, "Id", out Guid id))
                return;

            SoftDeleteStudent(id);
            MessageBox.Show("Öğrenci Silindi Eski Kayıtlar için Loglara Bak..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshStudentGrid();
        }

        private void btnOgrenciYonetimiAra_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshStudentGrid();
            }
            catch (Exception)
            {
                MessageBox.Show("Hatalı Veri Girişi", "Hata");
            }
        }

        private void txtOgrenciYonetimiAra_TextChanged(object sender, EventArgs e)
        {
            RefreshStudentGrid();
        }

        private void dataGridViewStok_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dataGridViewStok.HitTest(e.X, e.Y);

            if (hit.RowIndex >= 0)
            {
                if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
                {
                    dataGridViewStok.ClearSelection();
                    dataGridViewStok.Rows[hit.RowIndex].Selected = true;
                    dataGridViewStok.CurrentCell = dataGridViewStok.Rows[hit.RowIndex].Cells[0];
                }

                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Show(dataGridViewStok, e.Location);
                }
            }
        }

        private void dataGridViewStok_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dataGridViewStok.Rows[e.RowIndex];

            OgrenciForm ogrForm = new OgrenciForm(this);
            ogrForm.UserId = UserId;
            ogrForm.RefreshData += DataStokRefresh;

            LoadStudentClassComboBox(ogrForm.cmbogrsınıf, UserId);

            ogrForm.txtOgrenciAd.Text = row.Cells["İsim"].Value?.ToString() ?? string.Empty;
            ogrForm.textSoyad.Text = row.Cells["Soyisim"].Value?.ToString() ?? string.Empty;
            ogrForm.txtBabaAd.Text = row.Cells["Baba Adı"].Value?.ToString() ?? string.Empty;
            ogrForm.txtAnneAd.Text = row.Cells["Anne Adı"].Value?.ToString() ?? string.Empty;
            ogrForm.cmbogrsınıf.Text = row.Cells["Sınıfı"].Value?.ToString() ?? string.Empty;
            ogrForm.textOgrenciKod.Text = row.Cells["Öğrenci Kodu"].Value?.ToString() ?? string.Empty;
            ogrForm.textOgrenciDetay.Text = row.Cells["Öğrenci Hakkında"].Value?.ToString() ?? string.Empty;
            ogrForm.txtBabaTel.Text = row.Cells["Baba Telefon"].Value?.ToString() ?? string.Empty;
            ogrForm.txtAnneTel.Text = row.Cells["Anne Telefon"].Value?.ToString() ?? string.Empty;
            ogrForm.txtBabaEvAdres.Text = row.Cells["Baba Adresi"].Value?.ToString() ?? string.Empty;
            ogrForm.txtAnneEvAdres.Text = row.Cells["Anne Adresi"].Value?.ToString() ?? string.Empty;

            if (row.Cells["FotoId"].Value is byte[] imageData && imageData.Length > 0)
            {
                using (var ms = new MemoryStream(imageData))
                {
                    ogrForm.pictureBox1.Image = Image.FromStream(ms);
                    ogrForm.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            else
            {
                ogrForm.pictureBox1.Image = null;
            }

            ogrForm.numericPrice.Value = row.Cells["MonthlyFee"].Value != null &&
                                         decimal.TryParse(row.Cells["MonthlyFee"].Value.ToString(), out decimal price)
                ? price
                : 0;

            ogrForm.checkAktif.Checked = row.Cells["Aktif Öğrenci mi"].Value?.ToString() == "Evet";
            ogrForm.checkEvet.Checked = row.Cells["Aile Ayrı Mı"].Value?.ToString() == "Evet";
            ogrForm.checkOdemeDurum.Checked = row.Cells["Ödeme Durumu"].Value?.ToString() == "Ödeme Yapıldı" ||
                                              row.Cells["Ödeme Durumu"].Value?.ToString() == "True";

            if (row.Cells["Doğum Tarihi"].Value != null &&
                DateTime.TryParse(row.Cells["Doğum Tarihi"].Value.ToString(), out DateTime birthDate) &&
                birthDate >= ogrForm.dateDogum.MinDate)
            {
                ogrForm.dateDogum.Value = birthDate;
            }
            else
            {
                ogrForm.dateDogum.Value = DateTime.Now;
            }

            ogrForm.StudentId = (Guid)row.Cells["Id"].Value;
            ogrForm.Show();
        }

        private void ödemeDetaylarıToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!TryGetSelectedGuid(dataGridViewStok, "Id", out Guid selectedStudentId))
            {
                MessageBox.Show("Uyarı", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ShowPaymentDetails(selectedStudentId);
        }

        private void arşivToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewStok.SelectedCells.Count <= 0)
            {
                MessageBox.Show("Lütfen bir öğrenci seçiniz!");
                return;
            }

            var cell = dataGridViewStok.SelectedCells[0];
            var row = dataGridViewStok.Rows[cell.RowIndex];
            Guid ogrenciId = Guid.Parse(row.Cells["Id"].Value.ToString());

            using (var arsiv = new arsivForm(UserId, connectionString, ogrenciId))
            {
                arsiv.ShowDialog();
            }
        }

        #endregion

        #region Ödeme Yönetimi

        private void LoadPaymentData(Guid userId)
        {
            dataOgrVw.DataSource = ExecuteDataTable(
                @"SELECT 
                      OgrenciAdi = (SELECT Name FROM AYSStudents a WHERE a.Id = StudentId),
                      PaymentDate,
                      Amount
                  FROM AYSFeePayments
                  WHERE SchoolId = (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId)
                  ORDER BY StudentId",
                CommandType.Text,
                DbParam("@UserId", userId));
        }

        private DataTable OdemeLoad(Guid userId, Guid studentId)
        {
            return ExecuteDataTable(
                @"SELECT p.Id,
                         'Ödeme Tutarı' = p.Amount,
                         'Ödeme Tarihi' = p.PaymentDate,
                         p.IsApproved,
                         'Ödendi Mi?' = CASE WHEN p.IsApproved = 1 THEN 'Ödendi' ELSE 'Ödenecek' END,
                         'Ödenen Tarihi' = p.ApprovedDate
                  FROM AYSFeePayments p
                  WHERE p.StudentId = @StudentId
                    AND p.IsDeleted = 0
                    AND p.SchoolId = (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId)
                  ORDER BY p.PaymentDate",
                CommandType.Text,
                DbParam("@UserId", userId),
                DbParam("@StudentId", studentId));
        }

        private void ShowPaymentDetails(Guid studentId)
        {
            try
            {
                DataTable paymentDetails = OdemeLoad(UserId, studentId);

                Form paymentForm = new Form
                {
                    Text = "Ödeme Detayları",
                    Size = new Size(900, 600)
                };

                DataGridView paymentGrid = new DataGridView
                {
                    DataSource = paymentDetails,
                    Dock = DockStyle.Top,
                    Height = 400,
                    ReadOnly = true,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    MultiSelect = true,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                };

                paymentGrid.DataBindingComplete += (s, e) =>
                {
                    if (paymentGrid.Columns.Contains("Id"))
                        paymentGrid.Columns["Id"].Visible = false;

                    if (paymentGrid.Columns.Contains("IsApproved"))
                        paymentGrid.Columns["IsApproved"].Visible = false;
                };

                paymentGrid.RowPrePaint += (s, e) =>
                {
                    var grid = s as DataGridView;
                    var row = grid?.Rows[e.RowIndex];
                    if (row == null || row.Cells["IsApproved"].Value == DBNull.Value)
                        return;

                    bool approved = Convert.ToBoolean(row.Cells["IsApproved"].Value);
                    row.DefaultCellStyle.BackColor = approved ? Color.LightGreen : Color.FromArgb(191, 81, 79);
                };

                ContextMenuStrip contextMenu = new ContextMenuStrip();
                ToolStripMenuItem generatePlanItem = new ToolStripMenuItem("Aylık Ödeme Planı Oluştur");
                contextMenu.Items.Add(generatePlanItem);
                paymentGrid.ContextMenuStrip = contextMenu;

                generatePlanItem.Click += (s, e) => ShowPaymentPlanForm(studentId, paymentGrid);

                Panel addPaymentPanel = new Panel { Dock = DockStyle.Bottom, Height = 100 };
                Label lblAmount = new Label { Text = "Tutar:", AutoSize = true, Location = new Point(20, 20) };
                TextBox txtAmount = new TextBox { Location = new Point(100, 20), Width = 150 };
                Label lblDate = new Label { Text = "Tarih:", AutoSize = true, Location = new Point(20, 60) };
                DateTimePicker dtpDate = new DateTimePicker { Location = new Point(100, 60), Width = 150 };
                Button btnAddPayment = new Button { Text = "Ödeme Ekle", Location = new Point(300, 30), Width = 150 };
                Button btnOnayla = new Button { Text = "Ödemeyi Onayla", Dock = DockStyle.Bottom, Height = 40 };

                btnAddPayment.Click += (s, e) => AddPayment(studentId, txtAmount.Text, dtpDate.Value, paymentGrid);
                btnOnayla.Click += (s, e) => ApprovePayments(paymentGrid, studentId);

                FlowLayoutPanel flowPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = true
                };

                flowPanel.Controls.Add(lblAmount);
                flowPanel.Controls.Add(txtAmount);
                flowPanel.Controls.Add(lblDate);
                flowPanel.Controls.Add(dtpDate);
                flowPanel.Controls.Add(btnAddPayment);
                addPaymentPanel.Controls.Add(flowPanel);

                paymentForm.Controls.Add(paymentGrid);
                paymentForm.Controls.Add(addPaymentPanel);
                paymentForm.Controls.Add(btnOnayla);
                paymentForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void ShowPaymentPlanForm(Guid studentId, DataGridView paymentGrid)
        {
            Form planForm = new Form
            {
                Text = "Aylık Ödeme Planı",
                Size = new Size(300, 200)
            };

            Label lblTutar = new Label { Text = "Aylık Tutar:", Location = new Point(10, 20), AutoSize = true };
            TextBox txtTutar = new TextBox { Location = new Point(100, 20), Width = 150 };
            Label lblAySayisi = new Label { Text = "Ay Sayısı:", Location = new Point(10, 60), AutoSize = true };
            NumericUpDown nudAy = new NumericUpDown { Location = new Point(100, 60), Width = 150, Minimum = 1, Maximum = 24 };
            Button btnOlustur = new Button { Text = "Oluştur", Location = new Point(100, 100), Width = 150 };

            btnOlustur.Click += (ss, ee) =>
            {
                if (!decimal.TryParse(txtTutar.Text, out decimal tutar))
                {
                    MessageBox.Show("Geçerli bir tutar giriniz.");
                    return;
                }

                int aySayisi = (int)nudAy.Value;

                using (SqlConnection conn = CreateConnection())
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand insertCmd = new SqlCommand(@"INSERT INTO AYSFeePayments (Id, StudentId, Amount, PaymentDate, SchoolId)
                                                                          VALUES (@Id, @StudentId, @Amount, @PaymentDate,
                                                                          (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId))", conn, tran))
                            {
                                insertCmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                                insertCmd.Parameters.Add("@StudentId", SqlDbType.UniqueIdentifier).Value = studentId;
                                insertCmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                                insertCmd.Parameters.Add("@PaymentDate", SqlDbType.DateTime);
                                insertCmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = UserId;

                                for (int i = 0; i < aySayisi; i++)
                                {
                                    insertCmd.Parameters["@Id"].Value = Guid.NewGuid();
                                    insertCmd.Parameters["@Amount"].Value = tutar;
                                    insertCmd.Parameters["@PaymentDate"].Value = DateTime.Now.AddMonths(i);
                                    insertCmd.ExecuteNonQuery();
                                }
                            }

                            tran.Commit();
                        }
                        catch
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }

                paymentGrid.DataSource = OdemeLoad(UserId, studentId);
                MessageBox.Show("Aylık ödeme planı başarıyla oluşturuldu.");
                planForm.Close();
            };

            planForm.Controls.Add(lblTutar);
            planForm.Controls.Add(txtTutar);
            planForm.Controls.Add(lblAySayisi);
            planForm.Controls.Add(nudAy);
            planForm.Controls.Add(btnOlustur);
            planForm.ShowDialog();
        }

        private void AddPayment(Guid studentId, string amountText, DateTime paymentDate, DataGridView paymentGrid)
        {
            if (!decimal.TryParse(amountText, out decimal amount))
            {
                MessageBox.Show("Geçerli bir tutar giriniz.");
                return;
            }

            ExecuteNonQueryCommand(
                @"INSERT INTO AYSFeePayments (Id, StudentId, Amount, PaymentDate, SchoolId)
                  VALUES (@Id, @StudentId, @Amount, @PaymentDate,
                  (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId))",
                CommandType.Text,
                DbParam("@Id", Guid.NewGuid()),
                DbParam("@StudentId", studentId),
                DbParam("@Amount", amount),
                DbParam("@PaymentDate", paymentDate),
                DbParam("@UserId", UserId));

            MessageBox.Show("Ödeme başarıyla eklendi.");
            paymentGrid.DataSource = OdemeLoad(UserId, studentId);
        }

        private void ApprovePayments(DataGridView paymentGrid, Guid studentId)
        {
            if (paymentGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen onaylamak için en az bir ödeme seçin.");
                return;
            }

            try
            {
                using (SqlConnection conn = CreateConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE AYSFeePayments SET IsApproved = 1, ApprovedDate = @Now WHERE Id = @Id", conn))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                        cmd.Parameters.Add("@Now", SqlDbType.DateTime).Value = DateTime.Now;

                        foreach (DataGridViewRow selectedRow in paymentGrid.SelectedRows)
                        {
                            if (selectedRow.IsNewRow || selectedRow.Cells["Id"].Value == null)
                                continue;

                            cmd.Parameters["@Id"].Value = (Guid)selectedRow.Cells["Id"].Value;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show(paymentGrid.SelectedRows.Count == 1 ? "Ödeme onaylandı." : "Ödemeler onaylandı.");
                paymentGrid.DataSource = OdemeLoad(UserId, studentId);
            }
            catch
            {
                MessageBox.Show("Hata oluştu. Lütfen boş veya geçersiz satır seçmeyin.");
            }
        }

        private void btnMakeSale_Click(object sender, EventArgs e)
        {
            if (comboBoxStok.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir ürün seçin!");
                return;
            }

            ComboBoxItem selectedItem = (ComboBoxItem)comboBoxStok.SelectedItem;
            Guid productId = Guid.Parse(selectedItem.Value);
            int quantitySold = (int)numericQuantitySold.Value;

            int rowsAffected = ExecuteNonQueryCommand(
                @"INSERT INTO AYSFeePayments (Id, StudentId, Amount, PaymentDate, SchoolId)
                  VALUES (@Id, @StudentId, @Amount, @PaymentDate,
                  (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId))",
                CommandType.Text,
                DbParam("@Id", Guid.NewGuid()),
                DbParam("@StudentId", productId),
                DbParam("@Amount", quantitySold),
                DbParam("@PaymentDate", DateTime.Now),
                DbParam("@UserId", UserId));

            MessageBox.Show(rowsAffected > 0 ? "Satış başarıyla gerçekleştirildi!" : "Yeterli stok yok!");
            LoadStockData(UserId);
        }

        #endregion

        #region Personel Yönetimi

        public DataTable LoadPersonelRefresh(Guid userId)
        {
            return GetPersonelTable(userId);
        }

        public void PersonelYonetimiLoad(Guid userId)
        {
            dgvPersonelYonetimi.DataSource = GetPersonelTable(userId);
        }

        private void LoadTeacherComboBox(Guid userId)
        {
            LoadComboBoxItems(
                cbxOgrenciYonetimiOgretmen,
                @"SELECT isim = (FirstName + ' ' + LastName), PersonelId
                  FROM personel
                  WHERE CompanyId = (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId)
                    AND IsTeacher = 1
                    AND ISNULL(IsActive,1)=1
                  ORDER BY FirstName, LastName",
                reader => new ComboBoxItem
                {
                    Text = reader["isim"].ToString(),
                    Value = reader["PersonelId"].ToString()
                },
                DbParam("@UserId", userId));
        }

        private void dataGridViewPersonel_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvPersonelYonetimi.Rows[e.RowIndex];

            PersonelForm personelForm = new PersonelForm(this);
            personelForm.UserId = this.UserId;
            personelForm.PersonelId = (Guid)row.Cells["PersonelId"].Value;
            personelForm.RefreshData += LoadPersonelRefreshEvent;

            personelForm.txtPersonelAd.Text = row.Cells["Adı"].Value?.ToString();
            personelForm.txtPersonelSoyad.Text = row.Cells["Soyadı"].Value?.ToString();
            personelForm.dtpPersonelDG.Value = Convert.ToDateTime(row.Cells["Doğum Tarihi"].Value);
            personelForm.cbxPersonelUyruk.Text = row.Cells["Uyruk"].Value?.ToString();
            personelForm.txtPersonelKimlik.Text = row.Cells["Kimlik No"].Value?.ToString();

            string cinsiyet = row.Cells["Cinsiyet"].Value?.ToString();
            personelForm.rbtPersonelErkek.Checked = cinsiyet == "Erkek";
            personelForm.rbtPersonelKadin.Checked = cinsiyet == "Kadın";

            string medeniDurum = row.Cells["Evli mi?"].Value?.ToString();
            personelForm.rbtPersonelEvli.Checked = medeniDurum == "Evet";
            personelForm.rbtPersonelBekar.Checked = medeniDurum == "Hayır";

            string egitimGorevlisi = row.Cells["Öğretmen mi?"].Value?.ToString();
            personelForm.rbtPersonelEgitimGorevlisiEvet.Checked = egitimGorevlisi == "Evet";
            personelForm.rbtPersonelEgitimGorevlisiHayir.Checked = egitimGorevlisi == "Hayır";

            personelForm.txtPersonelTel.Text = row.Cells["Telefon"].Value?.ToString();
            personelForm.txtPersonelMail.Text = row.Cells["E-posta"].Value?.ToString();
            personelForm.txtPersonelIletişimAcilDurum.Text = row.Cells["Acil Yakınlar"].Value?.ToString();
            personelForm.txtPersonelAdres.Text = row.Cells["Adres"].Value?.ToString();
            personelForm.txtPersonelDepartman.Text = row.Cells["Departman"].Value?.ToString();
            personelForm.txtPersonelGorev.Text = row.Cells["İş Ünvanı"].Value?.ToString();
            personelForm.cbxPersonelCalismaSekli.Text = row.Cells["Aktif mi?"].Value?.ToString() == "Evet" ? "Aktif" : "Pasif";
            personelForm.txtPersonelPersonelNo.Text = row.Cells["Personel Numarası"].Value?.ToString();
            personelForm.cbxPersonelSigorta.Text = row.Cells["SGK Sicil No"].Value?.ToString();

            personelForm.txtPersonelMaas.Text = row.Cells["Maaş"].Value?.ToString();
            personelForm.txtPersonelPrimVeEk.Text = row.Cells["Ek Ödeme"].Value?.ToString();
            personelForm.txtPersonelYemekYol.Text = row.Cells["Yemek ve Ulaşım Ücreti"].Value?.ToString();

            personelForm.txtPersonelSGKSicilNum.Text = row.Cells["SGK Sicil No"].Value?.ToString();
            personelForm.txtPersonelSaglikSigorta.Text = row.Cells["Sağlık Sigortası Bilgileri"].Value?.ToString();
            personelForm.txtPersonelEmeklilik.Text = row.Cells["Emeklilik Bilgileri"].Value?.ToString();

            personelForm.cbxPersonelEgitimDurumu.Text = row.Cells["Eğitim Durumu"].Value?.ToString();
            personelForm.cbxPersonelUniversite.Text = row.Cells["Üniversite Bölümü"].Value?.ToString();
            personelForm.txtPersonelUniBolum.Text = row.Cells["Üniversite Bölümü"].Value?.ToString();
            personelForm.txtPersonelSertifika.Text = row.Cells["Sertifika ve Eğitim Bilgileri"].Value?.ToString();
            personelForm.txtPersonelYabanciDil.Text = row.Cells["Yabancı Dil"].Value?.ToString();

            personelForm.cbxPersoneIIsAyrıldı.Checked = row.Cells["İşten Ayrıldı mı?"].Value?.ToString() == "Evet";
            if (personelForm.cbxPersoneIIsAyrıldı.Checked && row.Cells["İşten Ayrılma Tarihi"].Value != DBNull.Value)
                personelForm.dtpPersonelCıkısTarihi.Value = Convert.ToDateTime(row.Cells["İşten Ayrılma Tarihi"].Value);

            personelForm.txtPersonelAyrilmaNedeni.Text = row.Cells["İşten Ayrılma Nedeni"].Value?.ToString();
            personelForm.txtPersonelKidemTazminat.Text = row.Cells["Kıdem Tazminatı"].Value?.ToString();
            personelForm.dtpPersonelIseBaslamaTarihi.Value = Convert.ToDateTime(row.Cells["İşe Başlama Tarihi"].Value);

            personelForm.Show();
        }

        #endregion

        #region Sınıf Yönetimi

        private void YasGrubuLoad()
        {
            cbxOgrenciYonetimiYasGrubu.Items.Clear();
            cbxOgrenciYonetimiYasGrubu.Items.AddRange(new object[] { "3 YAŞ", "4 YAŞ", "5 YAŞ", "6 YAŞ" });
        }

        private void SinifAdd(Guid userId)
        {
        }

        private void SinifLoad(Guid userId)
        {
            DgvOgrenciYonetimiSiniflar.DataSource = ExecuteDataTable(
                @"SELECT Id,
                         Sınıf = ClassName,
                         'Yaş Grubu' = [Group],
                         'Öğretmen Adı' = OgretmenAdi
                  FROM AYSClasses
                  WHERE OgretmenAdi IS NOT NULL
                    AND LTRIM(RTRIM(OgretmenAdi)) <> ''
                    AND ClassName IS NOT NULL
                    AND LTRIM(RTRIM(ClassName)) <> ''
                    AND SchoolId = (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId)
                    AND IsDeleted = 0",
                CommandType.Text,
                DbParam("@UserId", userId));
        }

        private void btnOgrenciYonetimiSinifKaydet_Click(object sender, EventArgs e)
        {
            string sınıfadi = txtOgrenciYonetimiSınıfAdı.Text.Trim();
            string yasgrubu = cbxOgrenciYonetimiYasGrubu.Text;
            string ogretmen = cbxOgrenciYonetimiOgretmen.Text;

            if (string.IsNullOrWhiteSpace(sınıfadi) || string.IsNullOrWhiteSpace(yasgrubu) || string.IsNullOrWhiteSpace(ogretmen))
            {
                MessageBox.Show("Sınıfı Boş Geçemezsiniz...");
                return;
            }

            using (SqlConnection conn = CreateConnection())
            using (SqlCommand cmd = new SqlCommand("AddAysClass", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassName", sınıfadi);
                cmd.Parameters.AddWithValue("@YasGrup", yasgrubu);
                cmd.Parameters.AddWithValue("@OgretmenAdi", ogretmen);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                SqlParameter outputParam = new SqlParameter("@Result", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                int sonuc = (int)outputParam.Value;
                MessageBox.Show(sonuc == 1 ? "Sınıf Başarıyla Eklendi." : "Bu Sınıf Zaten Mevcut.");
            }

            SinifLoad(UserId);
        }

        private void btnOgrenciYonetimiSinifGuncelle_Click(object sender, EventArgs e)
        {
            if (DgvOgrenciYonetimiSiniflar.CurrentRow == null)
                return;

            string classId = DgvOgrenciYonetimiSiniflar.CurrentRow.Cells["Sınıf"].Value?.ToString();
            string sinifAdi = txtOgrenciYonetimiSınıfAdı.Text;
            string yasGrubu = cbxOgrenciYonetimiYasGrubu.Text;
            string ogretmenAdi = cbxOgrenciYonetimiOgretmen.Text;

            ExecuteNonQueryCommand(
                @"UPDATE AYSClasses SET 
                      ClassName = @SinifAdi,
                      [Group] = @YasGrubu,
                      OgretmenAdi = @OgretmenAdi,
                      OgretmenId = (
                          SELECT TOP 1 PersonelId
                          FROM Personel
                          WHERE FirstName + ' ' + LastName = @OgretmenAdi
                            AND CompanyId = (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId)
                      )
                  WHERE Id = (
                      SELECT TOP 1 Id
                      FROM AYSClasses
                      WHERE ClassName = @SinifAdiP AND IsDeleted = 0
                  )",
                CommandType.Text,
                DbParam("@SinifAdiP", classId),
                DbParam("@SinifAdi", sinifAdi),
                DbParam("@YasGrubu", yasGrubu),
                DbParam("@OgretmenAdi", ogretmenAdi),
                DbParam("@UserId", UserId));

            SinifLoad(UserId);
        }

        private void btnOgrenciYonetimiSinifSil_Click(object sender, EventArgs e)
        {
            if (DgvOgrenciYonetimiSiniflar.CurrentRow == null)
            {
                MessageBox.Show("Lütfen önce silinecek sınıfı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            object cellValue = DgvOgrenciYonetimiSiniflar.CurrentRow.Cells["Id"].Value;
            if (cellValue == null || !Guid.TryParse(cellValue.ToString(), out Guid classId) || classId == Guid.Empty)
            {
                MessageBox.Show("Geçersiz sınıf bilgisi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ExecuteNonQueryCommand(
                "UPDATE AYSClasses SET IsDeleted = 1 WHERE Id = @id",
                CommandType.Text,
                DbParam("@id", classId));

            MessageBox.Show("Sınıf Silindi. Eski kayıtlar için loglara bakın.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DeleteAndLog("AYSClasses", "Id", classId, UserId, "1", "DELETE");
            SinifLoad(UserId);
        }

        private void DgvOgrenciYonetimiSiniflar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = DgvOgrenciYonetimiSiniflar.Rows[e.RowIndex];
            DgvOgrenciYonetimiSiniflar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvOgrenciYonetimiSiniflar.MultiSelect = false;

            txtOgrenciYonetimiSınıfAdı.Text = row.Cells["Sınıf"].Value?.ToString();
            cbxOgrenciYonetimiYasGrubu.Text = row.Cells["Yaş Grubu"].Value?.ToString();
            cbxOgrenciYonetimiOgretmen.Text = row.Cells["Öğretmen Adı"].Value?.ToString();
        }

        #endregion

        #region Gelir / Gider / Rapor

        private void LoadSalesData()
        {
            salesGrid.DataSource = ExecuteDataTable("GetDynamicQueryResult 1");

            if (salesGrid.Columns.Contains("Aciklama")) salesGrid.Columns["Aciklama"].HeaderText = "Açıklama";
            if (salesGrid.Columns.Contains("Miktar")) salesGrid.Columns["Miktar"].HeaderText = "Miktar";
            if (salesGrid.Columns.Contains("Tip")) salesGrid.Columns["Tip"].HeaderText = "Tür";
            if (salesGrid.Columns.Contains("Tarih")) salesGrid.Columns["Tarih"].HeaderText = "Tarih";
            if (salesGrid.Columns.Contains("NetMiktar")) salesGrid.Columns["NetMiktar"].HeaderText = "Net Miktar";

            if (salesGrid.Columns.Contains("Aciklama")) salesGrid.Columns["Aciklama"].DisplayIndex = 1;
            if (salesGrid.Columns.Contains("Miktar")) salesGrid.Columns["Miktar"].DisplayIndex = 2;
            if (salesGrid.Columns.Contains("Tip")) salesGrid.Columns["Tip"].DisplayIndex = 3;
            if (salesGrid.Columns.Contains("Tarih")) salesGrid.Columns["Tarih"].DisplayIndex = 4;
            if (salesGrid.Columns.Contains("NetMiktar")) salesGrid.Columns["NetMiktar"].DisplayIndex = 5;

            salesGrid.AllowUserToOrderColumns = true;
            salesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnAddIncomeExpense_Click(object sender, EventArgs e)
        {
            string description = txtDescription.Text;
            decimal amount = numericAmount.Value;
            string type = radioIncome.Checked ? "G" : "D";

            ExecuteNonQueryCommand(
                "INSERT INTO GelirGider (Aciklama, Miktar, Tip) VALUES (@Aciklama, @Miktar, @Tip)",
                CommandType.Text,
                DbParam("@Aciklama", description),
                DbParam("@Miktar", amount),
                DbParam("@Tip", type));

            MessageBox.Show("Gelir/Gider kaydı başarıyla eklendi!");
            LoadPaymentData(UserId);
        }

        private void LoadOzelRaporlarToGrid()
        {
            salesGrid.DataSource = ExecuteDataTable("SELECT Id, RaporAdi, Sorgu, KayitTarihi FROM OzelRaporlar ORDER BY KayitTarihi DESC");

            if (salesGrid.Columns.Contains("Id")) salesGrid.Columns["Id"].Visible = false;
            if (salesGrid.Columns.Contains("Sorgu")) salesGrid.Columns["Sorgu"].Visible = false;
            if (salesGrid.Columns.Contains("RaporAdi")) salesGrid.Columns["RaporAdi"].HeaderText = "Rapor Adı";
            if (salesGrid.Columns.Contains("KayitTarihi")) salesGrid.Columns["KayitTarihi"].HeaderText = "Eklenme Tarihi";
        }

        private void salesGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = salesGrid.Rows[e.RowIndex];
            int raporId = Convert.ToInt32(row.Cells["Id"].Value);

            using (RaporCalistirForm frm = new RaporCalistirForm(raporId))
            {
                frm.ShowDialog();
            }
        }

        #endregion

        #region Excel Aktarım

        private void excelAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                if (aktifDGV == null || aktifDGV.Rows.Count == 0)
                    return;

                using (OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel Dosyaları|*.xls;*.xlsx",
                    Title = "Bir Excel Dosyası Seçin"
                })
                {
                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                        return;

                    string dosyaYolu = openFileDialog.FileName;
                    FileInfo fi = new FileInfo(dosyaYolu);

                    using (var package = new ExcelPackage(fi))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        int row = 2;
                        int tag = GetActiveGridTag();

                        using (SqlConnection conn = CreateConnection())
                        {
                            conn.Open();
                            using (SqlTransaction tran = conn.BeginTransaction())
                            {
                                try
                                {
                                    if (tag == StudentModuleTag)
                                    {
                                        while (!string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].Text))
                                        {
                                            Guid ogrenciId = Guid.NewGuid();
                                            string ogrenciName = worksheet.Cells[row, 1].Text;
                                            string ogrenciSurname = worksheet.Cells[row, 2].Text;
                                            string fatherName = worksheet.Cells[row, 3].Text;
                                            DateTime birthDate = DateTime.Parse(worksheet.Cells[row, 4].Text);
                                            string studentCode = worksheet.Cells[row, 5].Text;
                                            bool paymentStatus = bool.Parse(worksheet.Cells[row, 6].Text);
                                            decimal monthlyFee = decimal.Parse(worksheet.Cells[row, 7].Text);
                                            bool isActive = bool.Parse(worksheet.Cells[row, 8].Text);
                                            string className = worksheet.Cells[row, 9].Text;
                                            string motherName = worksheet.Cells[row, 10].Text;
                                            string fatherAddress = worksheet.Cells[row, 11].Text;
                                            string motherAddress = worksheet.Cells[row, 12].Text;
                                            string fatherPhoneNumber = worksheet.Cells[row, 13].Text;
                                            string ogrenciDetails = worksheet.Cells[row, 14].Text;
                                            string motherPhoneNumber = worksheet.Cells[row, 15].Text;
                                            bool isMarried = string.IsNullOrEmpty(worksheet.Cells[row, 16].Text) || bool.Parse(worksheet.Cells[row, 16].Text);

                                            using (SqlCommand cmd = new SqlCommand(@"INSERT INTO AysStudents 
                                                (Id, Name, Surname, FatherName, BirthDate, StudentCode, ClassId, PaymentStatus, MonthlyFee, IsActive,
                                                 FatherAddress, MotherAddress, FatherPhoneNumber, MotherPhoneNumber, IsMarried, StudentsDetails, MotherName, SchoolId)
                                                VALUES
                                                (@Id, @Name, @Surname, @FatherName, @BirthDate, @StudentCode,
                                                 (SELECT TOP 1 Id FROM AYSClasses WHERE ClassName = @ClassName),
                                                 @PaymentStatus, @MonthlyFee, @IsActive, @FatherAddress, @MotherAddress,
                                                 @FatherPhoneNumber, @MotherPhoneNumber, @IsMarried, @StudentsDetails, @MotherName,
                                                 (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId))", conn, tran))
                                            {
                                                cmd.Parameters.AddWithValue("@Id", ogrenciId);
                                                cmd.Parameters.AddWithValue("@Name", ogrenciName);
                                                cmd.Parameters.AddWithValue("@Surname", ogrenciSurname);
                                                cmd.Parameters.AddWithValue("@FatherName", fatherName);
                                                cmd.Parameters.AddWithValue("@BirthDate", birthDate);
                                                cmd.Parameters.AddWithValue("@StudentCode", studentCode);
                                                cmd.Parameters.AddWithValue("@ClassName", className);
                                                cmd.Parameters.AddWithValue("@PaymentStatus", paymentStatus);
                                                cmd.Parameters.AddWithValue("@MonthlyFee", monthlyFee);
                                                cmd.Parameters.AddWithValue("@IsActive", isActive);
                                                cmd.Parameters.AddWithValue("@MotherName", motherName);
                                                cmd.Parameters.AddWithValue("@FatherAddress", fatherAddress);
                                                cmd.Parameters.AddWithValue("@MotherAddress", motherAddress);
                                                cmd.Parameters.AddWithValue("@FatherPhoneNumber", fatherPhoneNumber);
                                                cmd.Parameters.AddWithValue("@StudentsDetails", ogrenciDetails);
                                                cmd.Parameters.AddWithValue("@MotherPhoneNumber", motherPhoneNumber);
                                                cmd.Parameters.AddWithValue("@IsMarried", isMarried);
                                                cmd.Parameters.AddWithValue("@UserId", UserId);
                                                cmd.ExecuteNonQuery();
                                            }

                                            DeleteAndLog("AYSSTUDENTS", "Id", ogrenciId, UserId, "0", "INSERT");
                                            row++;
                                        }
                                    }
                                    else if (tag == PersonelModuleTag)
                                    {
                                        while (!string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].Text))
                                        {
                                            Guid personelId = Guid.NewGuid();
                                            string firstName = worksheet.Cells[row, 1].Text;
                                            string lastName = worksheet.Cells[row, 2].Text;
                                            string email = worksheet.Cells[row, 3].Text;
                                            string phone = worksheet.Cells[row, 4].Text;
                                            string address = worksheet.Cells[row, 5].Text;
                                            string city = worksheet.Cells[row, 6].Text;
                                            string country = worksheet.Cells[row, 7].Text;
                                            DateTime birthDate = DateTime.Parse(worksheet.Cells[row, 8].Text);
                                            string gender = worksheet.Cells[row, 9].Text;
                                            string identityNumber = worksheet.Cells[row, 10].Text;
                                            string jobTitle = worksheet.Cells[row, 11].Text;
                                            string department = worksheet.Cells[row, 12].Text;
                                            DateTime hireDate = DateTime.Parse(worksheet.Cells[row, 13].Text);
                                            decimal salary = decimal.Parse(worksheet.Cells[row, 14].Text);
                                            bool isTeacher = worksheet.Cells[row, 15].Text == "1";
                                            string isMaried = worksheet.Cells[row, 16].Text;
                                            string education = worksheet.Cells[row, 17].Text;
                                            bool isQuitWork = worksheet.Cells[row, 18].Text == "1";
                                            DateTime? quitWorkDate = string.IsNullOrEmpty(worksheet.Cells[row, 19].Text) ? (DateTime?)null : DateTime.Parse(worksheet.Cells[row, 19].Text);
                                            string quitWorkReason = worksheet.Cells[row, 20].Text;
                                            decimal compensation = decimal.TryParse(worksheet.Cells[row, 21].Text, out var comp) ? comp : 0;
                                            decimal additionalPayment = decimal.TryParse(worksheet.Cells[row, 22].Text, out var ap) ? ap : 0;
                                            decimal foodTransport = decimal.TryParse(worksheet.Cells[row, 23].Text, out var ft) ? ft : 0;
                                            string sgkSicil = worksheet.Cells[row, 24].Text;
                                            string saglikSigorta = worksheet.Cells[row, 25].Text;
                                            string emeklilik = worksheet.Cells[row, 26].Text;
                                            string personelNo = worksheet.Cells[row, 27].Text;
                                            string uniDept = worksheet.Cells[row, 28].Text;
                                            string sertifikalar = worksheet.Cells[row, 29].Text;
                                            string language = worksheet.Cells[row, 30].Text;
                                            string emergency = worksheet.Cells[row, 31].Text;
                                            string uyruk = worksheet.Cells[row, 32].Text;

                                            using (SqlCommand cmd = new SqlCommand("[asl2e6ancomtr_aslan].[AddPersonel]", conn, tran))
                                            {
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                cmd.Parameters.AddWithValue("@Id", personelId);
                                                cmd.Parameters.AddWithValue("@userid", UserId);
                                                cmd.Parameters.AddWithValue("@FirstName", firstName);
                                                cmd.Parameters.AddWithValue("@LastName", lastName);
                                                cmd.Parameters.AddWithValue("@Email", email);
                                                cmd.Parameters.AddWithValue("@Phone", phone);
                                                cmd.Parameters.AddWithValue("@Address", address);
                                                cmd.Parameters.AddWithValue("@City", city);
                                                cmd.Parameters.AddWithValue("@Country", country);
                                                cmd.Parameters.AddWithValue("@Birthdate", birthDate);
                                                cmd.Parameters.AddWithValue("@Gender", gender);
                                                cmd.Parameters.AddWithValue("@IdentityNumber", identityNumber);
                                                cmd.Parameters.AddWithValue("@JobTitle", jobTitle);
                                                cmd.Parameters.AddWithValue("@Department", department);
                                                cmd.Parameters.AddWithValue("@HireDate", hireDate);
                                                cmd.Parameters.AddWithValue("@Salary", salary);
                                                cmd.Parameters.AddWithValue("@IsActive", true);
                                                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                                                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                                                cmd.Parameters.AddWithValue("@IsTeacher", isTeacher);

                                                SqlParameter photoParam = new SqlParameter("@photo", SqlDbType.VarBinary, -1)
                                                {
                                                    Value = Photo != null ? (object)Photo : DBNull.Value
                                                };
                                                cmd.Parameters.Add(photoParam);

                                                cmd.Parameters.AddWithValue("@IsMaried", isMaried);
                                                cmd.Parameters.AddWithValue("@Education", education);
                                                cmd.Parameters.AddWithValue("@IsQuitWork", isQuitWork);
                                                cmd.Parameters.AddWithValue("@QuitWorkDate", (object?)quitWorkDate ?? DBNull.Value);
                                                cmd.Parameters.AddWithValue("@QuitWorkReason", quitWorkReason);
                                                cmd.Parameters.AddWithValue("@Compensation", compensation);
                                                cmd.Parameters.AddWithValue("@AdditionalPayment", additionalPayment);
                                                cmd.Parameters.AddWithValue("@FoodandTransportFee", foodTransport);
                                                cmd.Parameters.AddWithValue("@SgkSicil", sgkSicil);
                                                cmd.Parameters.AddWithValue("@SaglikSigortasiBilgileri", saglikSigorta);
                                                cmd.Parameters.AddWithValue("@EmeklilikBilgileri", emeklilik);
                                                cmd.Parameters.AddWithValue("@PersonelNumarasi", personelNo);
                                                cmd.Parameters.AddWithValue("@UniversityDepartment", uniDept);
                                                cmd.Parameters.AddWithValue("@SertifikaAndEducation", sertifikalar);
                                                cmd.Parameters.AddWithValue("@ForeignLanguage", language);
                                                cmd.Parameters.AddWithValue("@EmergenyFamilies", emergency);
                                                cmd.Parameters.AddWithValue("@Uyruk", uyruk);
                                                cmd.ExecuteNonQuery();
                                            }

                                            DeleteAndLog("PERSONEL", "PersonelId", personelId, UserId, "0", "INSERT");
                                            row++;
                                        }
                                    }

                                    tran.Commit();
                                }
                                catch
                                {
                                    tran.Rollback();
                                    throw;
                                }
                            }
                        }

                        if (tag == StudentModuleTag)
                            RefreshStudentGrid();
                        else if (tag == PersonelModuleTag)
                            RefreshPersonelGrid();
                    }
                }

                MessageBox.Show("Excel verisi başarıyla aktarıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel verisi aktarılamadı! " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Log / Delete

        public void DeleteAndLog(string tableName, string primaryKeyColumn, Guid id, Guid userId, string actions, string actionName)
        {
            using (SqlConnection conn = CreateConnection())
            {
                conn.Open();

                string selectQuery = $"SELECT * FROM {tableName} WHERE {primaryKeyColumn} = @Id";
                string deletedDataJson = string.Empty;

                using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                {
                    selectCmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        if (dt.Rows.Count > 0)
                        {
                            deletedDataJson = JsonConvert.SerializeObject(dt, Formatting.Indented);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(deletedDataJson))
                {
                    string logQuery = @"INSERT INTO DeleteLog (TableName, DeletedData, DeletedAt, DeleteUserId, Actions, ActionName, CompanyId)
                                        VALUES (@TableName, @DeletedData, @DeletedAt, @DeleteUserId, @Actions, @ActionName,
                                        (SELECT TOP 1 CompanyId FROM CompanyUsers WHERE UserId = @DeleteUserId))";

                    using (SqlCommand logCmd = new SqlCommand(logQuery, conn))
                    {
                        logCmd.Parameters.AddWithValue("@TableName", tableName.ToUpper());
                        logCmd.Parameters.AddWithValue("@DeletedData", deletedDataJson);
                        logCmd.Parameters.AddWithValue("@DeletedAt", DateTime.Now);
                        logCmd.Parameters.AddWithValue("@DeleteUserId", userId);
                        logCmd.Parameters.AddWithValue("@Actions", actions);
                        logCmd.Parameters.AddWithValue("@ActionName", actionName.ToUpper());
                        logCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void loglarıGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem?.Owner is not ContextMenuStrip owner)
                return;

            if (owner.SourceControl is not DataGridView sourceDgv)
                return;

            int modulCode = sourceDgv.Tag != null
                ? Convert.ToInt32(sourceDgv.Tag)
                : sourceDgv.Name == "dataGridViewStok" ? StudentModuleTag : 0;

            Form logForm = new Form
            {
                Width = 800,
                Height = 600,
                Text = modulCode == StudentModuleTag
                    ? "Öğrenci Yönetimi Log"
                    : modulCode == PersonelModuleTag
                        ? "Personel Yönetimi Log"
                        : "Loglar"
            };

            DataGridView dgv = new DataGridView { Dock = DockStyle.Fill };
            logForm.Controls.Add(dgv);

            dgv.DataSource = ExecuteDataTable(
                @"SELECT * FROM deleteandlogs
                  WHERE SirketId = (SELECT TOP 1 CompanyId FROM CompanyUsers WHERE UserId = @UserId)
                    AND ModulKod = @ModulCode
                  ORDER BY SilinmeZamani DESC",
                CommandType.Text,
                DbParam("@UserId", UserId),
                DbParam("@ModulCode", modulCode));

            logForm.Show();
        }

        private void DeleteStripMenuItem_Click(object sender, EventArgs e)
        {
            int activeTag = GetActiveGridTag();

            if (activeTag == StudentModuleTag)
            {
                if (!TryGetSelectedGuid(dataGridViewStok, "Id", out Guid studentId))
                    return;

                DialogResult result = MessageBox.Show("Bu Öğrenciyi Silmek İstiyor musunuz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SoftDeleteStudent(studentId);
                    MessageBox.Show("Öğrenci Silindi Eski Kayıtlar için Loglara Bak..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshStudentGrid();
                }
            }
            else if (activeTag == PersonelModuleTag)
            {
                if (!TryGetSelectedGuid(dgvPersonelYonetimi, "PersonelId", out Guid personelId))
                    return;

                DialogResult result = MessageBox.Show("Bu Personeli Silmek İstiyor musunuz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SoftDeletePersonel(personelId);
                    MessageBox.Show("Personel Silindi Eski Kayıtlar için Loglara Bak..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshPersonelGrid();
                }
            }
        }

        #endregion

        #region Ön Kayıt

        private void btnOnKayitEkle_Click(object sender, EventArgs e)
        {
            string firstname = string.IsNullOrWhiteSpace(txtOnKayitAd.Text) ? "veri yok" : txtOnKayitAd.Text;
            string lastname = string.IsNullOrWhiteSpace(txtOnKayitSoyad.Text) ? "veri yok" : txtOnKayitSoyad.Text;
            string velitel = string.IsNullOrWhiteSpace(txtOnKayitVeliTel.Text) ? "veri yok" : txtOnKayitVeliTel.Text;
            string not = string.IsNullOrWhiteSpace(txtOnKayitNot.Text) ? "veri yok" : txtOnKayitNot.Text;
            string babaAd = string.IsNullOrWhiteSpace(txtOnKayitBabaAd.Text) ? "veri yok" : txtOnKayitBabaAd.Text;
            string studentCode = Guid.NewGuid().ToString();
            bool paymentStatus = false;
            decimal monthlyFee = 0m;
            Guid classId = UserId;
            DateTime createdAt = DateTime.Now;
            bool isActive = true;

            if (firstname == "veri yok" && lastname == "veri yok" && velitel == "veri yok" && not == "veri yok")
            {
                MessageBox.Show("Eksik veya yanlış veri!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = CreateConnection())
            using (SqlCommand cmd = new SqlCommand(@"EXEC [asl2e6ancomtr_aslan].[AddPreRegistration]
                                                    @FirstName = @FirstName,
                                                    @LastName = @LastName,
                                                    @BirthDate = @BirthDate,
                                                    @ParentPhone = @ParentPhone,
                                                    @Notes = @Notes,
                                                    @CreatedAt = @CreatedAt,
                                                    @FatherName = @FatherName,
                                                    @StudentCode = @StudentCode,
                                                    @ClassId = @ClassId,
                                                    @PaymentStatus = @PaymentStatus,
                                                    @MonthlyFee = @MonthlyFee,
                                                    @IsActive = @IsActive", conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", firstname);
                cmd.Parameters.AddWithValue("@LastName", lastname);
                cmd.Parameters.AddWithValue("@BirthDate", dtpOnKayitDogumTarihi.Value.Date);
                cmd.Parameters.AddWithValue("@ParentPhone", velitel);
                cmd.Parameters.AddWithValue("@Notes", not);
                cmd.Parameters.AddWithValue("@CreatedAt", createdAt);
                cmd.Parameters.AddWithValue("@FatherName", babaAd);
                cmd.Parameters.AddWithValue("@StudentCode", studentCode);
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@PaymentStatus", paymentStatus);
                cmd.Parameters.AddWithValue("@MonthlyFee", monthlyFee);
                cmd.Parameters.AddWithValue("@IsActive", isActive);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Ön kayıt başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadOnKayitlar(UserId);
        }

        private void btnOnKayitSil_Click(object sender, EventArgs e)
        {
            if (dgvOnKayitlar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek bir kayıt seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show("Seçili kaydı silmek istediğinizden emin misiniz?", "Kayıt Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;

            DataGridViewRow selectedRow = dgvOnKayitlar.SelectedRows[0];
            Guid kayitId = Guid.Parse(selectedRow.Cells["Id"].Value.ToString());

            try
            {
                ExecuteNonQueryCommand(
                    "DELETE FROM [asl2e6ancomtr_aslan].[PreRegistrations] WHERE Id = @Id",
                    CommandType.Text,
                    DbParam("@Id", kayitId));

                dgvOnKayitlar.Rows.Remove(selectedRow);
                MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKesinKayitYap_Click(object sender, EventArgs e)
        {
            if (dgvOnKayitlar.SelectedRows.Count == 0)
                return;

            var row = dgvOnKayitlar.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["Id"].Value);
            string ad = row.Cells["FirstName"].Value.ToString();
            string soyad = row.Cells["LastName"].Value.ToString();
            DateTime dogum = Convert.ToDateTime(row.Cells["BirthDate"].Value);
            string telefon = row.Cells["ParentPhone"].Value.ToString();
            string notlar = row.Cells["Notes"].Value.ToString();

            using (SqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand insertCmd = new SqlCommand("INSERT INTO Students (FirstName, LastName, BirthDate, ParentPhone, Notes) VALUES (@FirstName, @LastName, @BirthDate, @Phone, @Notes)", conn, tran))
                        {
                            insertCmd.Parameters.AddWithValue("@FirstName", ad);
                            insertCmd.Parameters.AddWithValue("@LastName", soyad);
                            insertCmd.Parameters.AddWithValue("@BirthDate", dogum);
                            insertCmd.Parameters.AddWithValue("@Phone", telefon);
                            insertCmd.Parameters.AddWithValue("@Notes", notlar);
                            insertCmd.ExecuteNonQuery();
                        }

                        using (SqlCommand deleteCmd = new SqlCommand("DELETE FROM PreRegistrations WHERE Id = @Id", conn, tran))
                        {
                            deleteCmd.Parameters.AddWithValue("@Id", id);
                            deleteCmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }

            LoadOnKayitlar(UserId);
            MessageBox.Show("Kesin kayda aktarıldı.");
        }

        private void LoadOnKayitlar(Guid classId)
        {
            dgvOnKayitlar.DataSource = ExecuteDataTable(
                @"SELECT Id,
                         'İsim' = FirstName,
                         'Soyisim' = LastName,
                         'Doğum Tarihi' = BirthDate,
                         'Baba Telefon' = ParentPhone,
                         'Notlar' = Notes,
                         'Baba Adı' = FatherName,
                         'Ödeme Durumu' = PaymentStatus,
                         'Aylık Ücret' = MonthlyFee
                  FROM PreRegistrations p
                  WHERE p.ClassId = dbo.GetSirketIdByUserId(@ClassId)",
                CommandType.Text,
                DbParam("@ClassId", classId));
        }

        private void btnKesinKayitYap_Click_1(object sender, EventArgs e)
        {
            if (dgvOnKayitlar.CurrentCell == null)
                return;

            DataGridViewRow row = dgvOnKayitlar.Rows[dgvOnKayitlar.CurrentCell.RowIndex];
            OgrenciForm ogrForm = new OgrenciForm(this);

            ogrForm.txtOgrenciAd.Text = row.Cells["İsim"].Value?.ToString() ?? string.Empty;
            ogrForm.textSoyad.Text = row.Cells["Soyisim"].Value?.ToString() ?? string.Empty;
            ogrForm.txtBabaAd.Text = row.Cells["Baba Adı"].Value?.ToString() ?? string.Empty;
            ogrForm.textOgrenciDetay.Text = row.Cells["Notlar"].Value?.ToString() ?? string.Empty;
            ogrForm.txtBabaTel.Text = row.Cells["Baba Telefon"].Value?.ToString() ?? string.Empty;
            ogrForm.numericPrice.Text = row.Cells["Aylık Ücret"].Value?.ToString() ?? string.Empty;
            ogrForm.checkOdemeDurum.Checked = row.Cells["Ödeme Durumu"].Value?.ToString() == "Evet";
            ogrForm.dateDogum.Text = row.Cells["Doğum Tarihi"].Value?.ToString() ?? string.Empty;

            ogrForm.UserId = UserId;
            ogrForm.RefreshData += DataStokRefresh;
            ogrForm.Show();
        }

        #endregion

        #region Genel UI / Refresh

        private void ResizeAndSetButtonImage(Button button, Image image)
        {
            int width = button.Width - 10;
            int height = button.Height - 10;
            Image resized = new Bitmap(image, new Size(width, height));
            button.Image = resized;
            button.ImageAlign = ContentAlignment.MiddleCenter;
            button.TextImageRelation = TextImageRelation.Overlay;
        }

        private void DataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            aktifDGV = sender as DataGridView;
        }

        private void DataStokRefresh(object sender, EventArgs e)
        {
            RefreshStudentGrid();
        }

        private void LoadPersonelRefreshEvent(object sender, EventArgs e)
        {
            RefreshPersonelGrid();
        }

        private void yeniKayitEkle(object sender, EventArgs e)
        {
            int activeTag = GetActiveGridTag();

            if (activeTag == StudentModuleTag)
            {
                OgrenciForm ogrForm = new OgrenciForm(this);
                ogrForm.UserId = UserId;
                ogrForm.RefreshData += DataStokRefresh;
                LoadStudentClassComboBox(ogrForm.cmbogrsınıf, UserId);
                ogrForm.Show();
            }
            else if (activeTag == PersonelModuleTag)
            {
                PersonelForm personelForm = new PersonelForm(this);
                personelForm.UserId = UserId;
                personelForm.RefreshData += LoadPersonelRefreshEvent;
                personelForm.Show();
            }
        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshActiveGrid();
        }

        #endregion

        #region Empty / Existing Event Stubs

        private void btnGuncelle_Click(object sender, EventArgs e) { }
        private void btnAddStock_Click(object sender, EventArgs e) { }
        private void salesGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void Delete(object sender, DataGridViewCellEventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void dataOgrVw_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void textBabaAd_TextChanged(object sender, EventArgs e) { }
        private void dateDogum_ValueChanged(object sender, EventArgs e) { }
        private void groupBox3_Enter(object sender, EventArgs e) { }
        private void tabPageStok_Click(object sender, EventArgs e) { }
        private void dataGridViewStok_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void tabPagePersonelYonetimi_Click(object sender, EventArgs e) { }
        private void cmbogrsınıf_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void txtPersonelMaas_TextChanged(object sender, EventArgs e) { }
        private void txtPersonelGorev_TextChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void groupBox18_Enter(object sender, EventArgs e) { }
        private void txtPersonelKimlik_TextChanged(object sender, EventArgs e) { }
        private void btnOnKayitEkle_Click_1(object sender, EventArgs e) { }
        private void EFatura_Click(object sender, EventArgs e) { }
        private void tabPage1_Click_2(object sender, EventArgs e) { }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            LoadSalesData();
        }

        private void FaturaBtn_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is FormFatura)
                {
                    MessageBox.Show("Form zaten açık.");
                    frm.BringToFront();
                    return;
                }
            }

            FormFatura formFatura = new FormFatura
            {
                UserId = UserId
            };
            formFatura.Show();
        }

        #endregion
    }

    public class ComboBoxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
