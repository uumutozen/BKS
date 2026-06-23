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
        private const string SystemNavigationKey = "SYSTEM_EXIT";
        private readonly Dictionary<string, Button> navigationButtons = new Dictionary<string, Button>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, string> navigationTitles = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, string> navigationSubtitles = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private readonly List<TabPage> _allModulePages = new List<TabPage>();
        private TableLayoutPanel _mainLayout;
        private TableLayoutPanel _contentLayout;
        private FlowLayoutPanel _navigationList;
        private Panel _sidebarPanel;
        private Panel _topBarPanel;
        private Label _headerTitleLabel;
        private Label _headerSubtitleLabel;
        private Label _headerUserLabel;
        private bool _layoutInitialized;


        public Form2()
        {
            InitializeComponent();
            ConfigureMainWindow();
            BuildResponsiveLayout();
            LoadStockComboBox();
            this.Text = "Anaokulu Yönetim Sistemi";
        }

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
        private Guid EnsurePersonelArchiveStudent(Guid personelId)
        {
            /*
                Personel arşivi için yeni endpoint açmadan mevcut öğrenci arşiv sistemini kullanıyoruz.

                Mantık:
                - Personel sağ tık > Arşiv
                - AysStudents tablosunda aynı Id ile gizli bir kayıt var mı bakılır.
                - Yoksa Personel bilgileriyle AysStudents içine IsDeleted = 1 olan gizli kayıt açılır.
                - arsivForm bu Id'yi öğrenci Id gibi kullanır.
                - Dosya arşiv endpointleri aynı kalır:
                    GET  /api/dosya-arsiv/ogrenci/{id}
                    POST /api/dosya-arsiv/yukle
            */

            object exists = ExecuteScalarValue(
                "SELECT COUNT(1) FROM AysStudents WHERE Id = @Id",
                CommandType.Text,
                DbParam("@Id", personelId));

            if (exists != null && exists != DBNull.Value && Convert.ToInt32(exists) > 0)
                return personelId;

            DataTable personelDt = ExecuteDataTable(
                @"SELECT TOP 1
              p.PersonelId,
              p.FirstName,
              p.LastName,
              p.Phone,
              p.Address,
              p.Birthdate,
              p.CompanyId
          FROM Personel p
          WHERE p.PersonelId = @PersonelId
            AND p.CompanyId = (SELECT TOP 1 CompanyId FROM CompanyUsers WHERE UserId = @UserId)",
                CommandType.Text,
                DbParam("@PersonelId", personelId),
                DbParam("@UserId", UserId));

            if (personelDt.Rows.Count == 0)
                throw new Exception("Personel bilgisi bulunamadı.");

            DataRow pRow = personelDt.Rows[0];

            string firstName = pRow["FirstName"] == DBNull.Value || string.IsNullOrWhiteSpace(pRow["FirstName"].ToString())
                ? "Personel"
                : pRow["FirstName"].ToString();

            string lastName = pRow["LastName"] == DBNull.Value || string.IsNullOrWhiteSpace(pRow["LastName"].ToString())
                ? "Arşiv"
                : pRow["LastName"].ToString();

            string phone = pRow["Phone"] == DBNull.Value ? "" : pRow["Phone"].ToString();
            string address = pRow["Address"] == DBNull.Value ? "" : pRow["Address"].ToString();

            DateTime birthDate = DateTime.Now;

            if (pRow["Birthdate"] != DBNull.Value &&
                DateTime.TryParse(pRow["Birthdate"].ToString(), out DateTime parsedBirthDate))
            {
                birthDate = parsedBirthDate;
            }

            Guid companyId = Guid.Parse(pRow["CompanyId"].ToString());

            ExecuteNonQueryCommand(
                @"INSERT INTO AysStudents
          (
              Id,
              Name,
              Surname,
              FatherName,
              BirthDate,
              StudentCode,
              ClassId,
              PaymentStatus,
              MonthlyFee,
              IsActive,
              FatherAddress,
              MotherAddress,
              FatherPhoneNumber,
              MotherPhoneNumber,
              IsMarried,
              StudentsDetails,
              MotherName,
              SchoolId,
              IsDeleted
          )
          VALUES
          (
              @Id,
              @Name,
              @Surname,
              @FatherName,
              @BirthDate,
              @StudentCode,
              (
                  SELECT TOP 1 Id
                  FROM AYSClasses
                  WHERE SchoolId = @SchoolId
                    AND ISNULL(IsDeleted, 0) = 0
                  ORDER BY ClassName
              ),
              @PaymentStatus,
              @MonthlyFee,
              @IsActive,
              @FatherAddress,
              @MotherAddress,
              @FatherPhoneNumber,
              @MotherPhoneNumber,
              @IsMarried,
              @StudentsDetails,
              @MotherName,
              @SchoolId,
              @IsDeleted
          )",
                CommandType.Text,
                DbParam("@Id", personelId),
                DbParam("@Name", firstName),
                DbParam("@Surname", lastName),
                DbParam("@FatherName", "PERSONEL ARŞİV"),
                DbParam("@BirthDate", birthDate),
                DbParam("@StudentCode", "PRSARSIV-" + personelId.ToString("N").Substring(0, 12).ToUpper()),
                DbParam("@PaymentStatus", false),
                DbParam("@MonthlyFee", 0),
                DbParam("@IsActive", false),
                DbParam("@FatherAddress", address),
                DbParam("@MotherAddress", ""),
                DbParam("@FatherPhoneNumber", phone),
                DbParam("@MotherPhoneNumber", ""),
                DbParam("@IsMarried", false),
                DbParam("@StudentsDetails", "PERSONEL_ARSIV_KAYDI - Bu kayıt personel arşivi için otomatik oluşturulmuştur."),
                DbParam("@MotherName", ""),
                DbParam("@SchoolId", companyId),
                DbParam("@IsDeleted", true));

            return personelId;
        }
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

        #region Modern Navigation

        private void RegisterNavigationItem(string moduleKey, string title, string subtitle, Action action)
        {
            if (_navigationList == null)
                return;

            navigationTitles[moduleKey] = title;
            navigationSubtitles[moduleKey] = subtitle;

            var button = new Button
            {
                Name = "nav_" + moduleKey,
                Tag = moduleKey,
                Text = BuildNavigationText(moduleKey, false),
                Height = 58,
                Width = 222,
                Margin = new Padding(8, 4, 8, 4),
                TextAlign = ContentAlignment.MiddleLeft,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(226, 232, 240),
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 162),
                Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 59, 82);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(59, 130, 246);
            button.Click += (s, e) => action();

            navigationButtons[moduleKey] = button;
            _navigationList.Controls.Add(button);
        }

        private string BuildNavigationText(string moduleKey, bool compact)
        {
            string icon = GetNavigationIcon(moduleKey);

            if (compact)
                return icon;

            string title = navigationTitles.ContainsKey(moduleKey) ? navigationTitles[moduleKey] : moduleKey;
            string subtitle = navigationSubtitles.ContainsKey(moduleKey) ? navigationSubtitles[moduleKey] : string.Empty;

            return string.IsNullOrWhiteSpace(subtitle)
                ? $"{icon}  {title}"
                : $"{icon}  {title}\r\n     {subtitle}";
        }

        private string GetNavigationIcon(string moduleKey)
        {
            switch (moduleKey)
            {
                case "tabPageOgrenciOnKayit": return "🎓";
                case "tabPageStok": return "📚";
                case "tabPageSatis": return "💰";
                case "tabPagePersonelYonetimi": return "👤";
                case "tabPageGelirGider": return "📊";
                case "tabPageOzelRaporlar": return "📈";
                case SystemNavigationKey: return "⏻";
                default: return "•";
            }
        }

        private void BuildNavigationItems()
        {
            if (_navigationList == null)
                return;

            navigationButtons.Clear();
            navigationTitles.Clear();
            navigationSubtitles.Clear();
            _navigationList.Controls.Clear();

            RegisterNavigationItem("tabPageOgrenciOnKayit", "Öğrenci Ön Kayıt", "Aday kayıt ve kesin kayıt", () => SelectModule(tabPageOgrenciOnKayit));
            RegisterNavigationItem("tabPageStok", "Öğrenci Yönetimi", "Sınıf, öğrenci ve arşiv", () => SelectModule(tabPageStok));
            RegisterNavigationItem("tabPageSatis", "Ödeme Yönetimi", "Tahsilat ve öğrenci ödeme", () => SelectModule(tabPageSatis));
            RegisterNavigationItem("tabPagePersonelYonetimi", "Personel", "Personel kartları ve arşiv", () => SelectModule(tabPagePersonelYonetimi));
            RegisterNavigationItem("tabPageGelirGider", "Gelir / Gider", "Kasa, fatura ve finans", () => SelectModule(tabPageGelirGider));
            RegisterNavigationItem("tabPageOzelRaporlar", "Raporlar", "Özel rapor merkezi", () => SelectModule(tabPageOzelRaporlar));

            var separator = new Label
            {
                AutoSize = false,
                Height = 1,
                Width = 220,
                Margin = new Padding(8, 12, 8, 12),
                BackColor = Color.FromArgb(51, 65, 85)
            };
            _navigationList.Controls.Add(separator);

            RegisterNavigationItem(SystemNavigationKey, "Çıkış", "Programı kapat", CloseApplication);
            UpdateNavigationVisualState();
        }

        private void SelectModule(TabPage page)
        {
            if (page == null)
                return;

            if (!tabControl.TabPages.Contains(page))
                tabControl.TabPages.Add(page);

            tabControl.SelectedTab = page;
            UpdateHeaderForCurrentTab();
            UpdateNavigationVisualState();
        }

        private void UpdateNavigationVisualState()
        {
            if (tabControl == null || navigationButtons.Count == 0)
                return;

            string selectedKey = tabControl.SelectedTab?.Name ?? string.Empty;

            foreach (var pair in navigationButtons)
            {
                bool selected = string.Equals(pair.Key, selectedKey, StringComparison.OrdinalIgnoreCase);
                pair.Value.BackColor = selected ? Color.FromArgb(59, 130, 246) : Color.Transparent;
                pair.Value.ForeColor = selected ? Color.White : Color.FromArgb(226, 232, 240);
            }
        }

        private void UpdateHeaderForCurrentTab()
        {
            if (_headerTitleLabel == null || tabControl == null)
                return;

            string key = tabControl.SelectedTab?.Name ?? string.Empty;
            string title = navigationTitles.ContainsKey(key) ? navigationTitles[key] : (tabControl.SelectedTab?.Text ?? "Ana Ekran");
            string subtitle = navigationSubtitles.ContainsKey(key) ? navigationSubtitles[key] : "Modül ekranı";

            _headerTitleLabel.Text = title;
            _headerSubtitleLabel.Text = subtitle;
        }

        private void SetNavigationAccess(List<string> activeModules, string role)
        {
            if (navigationButtons.Count == 0)
                return;

            bool showAll = IsAdminRole(role) || string.IsNullOrWhiteSpace(role) || activeModules == null || activeModules.Count == 0 || !HasMatchingModuleKey(activeModules);

            foreach (var pair in navigationButtons)
            {
                bool visible = pair.Key == SystemNavigationKey || showAll || activeModules.Any(module => string.Equals(module, pair.Key, StringComparison.OrdinalIgnoreCase));
                pair.Value.Visible = visible;
            }

            UpdateNavigationVisualState();
        }

        private bool HasMatchingModuleKey(List<string> activeModules)
        {
            if (activeModules == null || activeModules.Count == 0)
                return false;

            return activeModules.Any(module => _allModulePages.Any(tab => string.Equals(module, tab.Name, StringComparison.OrdinalIgnoreCase)));
        }

        private void SetTabAccess(List<string> activeModules, string role)
        {
            if (_allModulePages.Count == 0)
                IndexModulePages();

            if (string.IsNullOrWhiteSpace(role) || activeModules == null || activeModules.Count == 0 || !HasMatchingModuleKey(activeModules))
            {
                ApplyVisibleTabPages(tab => true);
                SetNavigationAccess(activeModules, role);
                return;
            }

            if (IsAdminRole(role))
            {
                ApplyVisibleTabPages(tab => true);
                SetNavigationAccess(activeModules, role);
                return;
            }

            ApplyVisibleTabPages(tab => activeModules.Any(module => string.Equals(module, tab.Name, StringComparison.OrdinalIgnoreCase)));
            SetNavigationAccess(activeModules, role);
        }

        private void IndexModulePages()
        {
            _allModulePages.Clear();
            _allModulePages.AddRange(new[]
            {
                tabPageOgrenciOnKayit,
                tabPageStok,
                tabPageSatis,
                tabPagePersonelYonetimi,
                tabPageGelirGider,
                tabPageOzelRaporlar
            }.Where(tab => tab != null));
        }

        private void ApplyVisibleTabPages(Func<TabPage, bool> isVisible)
        {
            var selected = tabControl.SelectedTab;

            tabControl.SuspendLayout();
            tabControl.TabPages.Clear();

            foreach (var tab in _allModulePages)
            {
                bool visible = isVisible(tab);
                tab.Enabled = visible;

                if (visible)
                    tabControl.TabPages.Add(tab);
            }

            if (selected != null && tabControl.TabPages.Contains(selected))
                tabControl.SelectedTab = selected;
            else if (tabControl.TabPages.Count > 0)
                tabControl.SelectedIndex = 0;

            tabControl.ResumeLayout(true);
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


        #region Modern Responsive UI

        private void ConfigureMainWindow()
        {
            SuspendLayout();
            BackColor = Color.FromArgb(246, 248, 252);
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 162);
            MinimumSize = new Size(1180, 720);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.Sizable;
            ControlBox = true;
            MinimizeBox = true;
            MaximizeBox = true;
            KeyPreview = true;
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    CloseApplication();
                    e.Handled = true;
                }
            };
            ResumeLayout(false);
        }

        private void BuildResponsiveLayout()
        {
            if (_layoutInitialized) return;
            _layoutInitialized = true;

            SuspendLayout();
            IndexModulePages();
            UseSegoeFontRecursive(this);
            ConfigurePreRegistrationTab();
            ConfigureStudentManagementTab();
            ConfigurePaymentTab();
            ConfigurePersonnelTab();
            ConfigureIncomeExpenseTab();
            ConfigureReportsTab();
            StyleAllGrids();
            InstallMainShellLayout();
            Resize += (s, e) => ApplyAdaptiveHeights();
            ApplyAdaptiveHeights();
            ResumeLayout(true);
        }

        private void ConfigurePreRegistrationTab()
        {
            tabPageOgrenciOnKayit.SuspendLayout();
            tabPageOgrenciOnKayit.BackColor = ModernWinForms.PageBack;
            tabPageOgrenciOnKayit.Padding = new Padding(14);
            tabPageOgrenciOnKayit.AutoScroll = true;

            ModernWinForms.HideLegacyButton(btnOnKayitEkle);
            ModernWinForms.HideLegacyButton(btnOnKayitSil);
            ModernWinForms.HideLegacyButton(btnKesinKayitYap);

            var page = ModernWinForms.CreatePageLayout(4);
            page.RowStyles.Add(new RowStyle(SizeType.Absolute, 76));
            page.RowStyles.Add(new RowStyle(SizeType.Absolute, 78));
            page.RowStyles.Add(new RowStyle(SizeType.Absolute, 172));
            page.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var header = CreateModuleHeader(
                "Öğrenci Ön Kayıt",
                "Aday öğrenciyi hızlı kaydet, sağ tık menüsüyle kesin kayda çevir veya arşivle.",
                "Faz 2 • Responsive kayıt akışı");

            var commandCard = ModernWinForms.CreateCard("preRegistrationCommandCard", 0);
            var strip = ModernWinForms.CreateCommandStrip("preRegistrationCommandStrip");
            strip.Items.Add(ModernWinForms.CreateCommand("Ctrl+S Kaydet", (s, e) => RunPreRegistrationAdd()));
            strip.Items.Add(ModernWinForms.CreateCommand("Ctrl+Enter Kesin kayıt", (s, e) => RunPreRegistrationConfirm()));
            strip.Items.Add(ModernWinForms.CreateCommand("Delete Sil", (s, e) => RunPreRegistrationDelete()));
            strip.Items.Add(new ToolStripSeparator());
            strip.Items.Add(ModernWinForms.CreateCommand("Yenile", (s, e) => LoadOnKayitlar(UserId)));
            commandCard.Controls.Add(strip);

            panelForm = panelForm ?? new Panel();
            panelForm.Dock = DockStyle.Fill;
            panelForm.BackColor = Color.White;
            panelForm.Padding = new Padding(16);
            panelForm.Margin = new Padding(0, 0, 0, 14);

            var formFlow = ModernWinForms.CreateFlow("flowOnKayitForm", true);
            ModernWinForms.StyleInput(txtOnKayitAd, 230);
            ModernWinForms.StyleInput(txtOnKayitSoyad, 230);
            ModernWinForms.StyleInput(dtpOnKayitDogumTarihi, 190);
            ModernWinForms.StyleInput(txtOnKayitVeliTel, 230);
            ModernWinForms.StyleInput(txtOnKayitBabaAd, 230);
            ModernWinForms.StyleInput(txtOnKayitNot, 430, 52);
            formFlow.Controls.AddRange(new Control[]
            {
                txtOnKayitAd, txtOnKayitSoyad, dtpOnKayitDogumTarihi,
                txtOnKayitVeliTel, txtOnKayitBabaAd, txtOnKayitNot
            });
            panelForm.Controls.Clear();
            panelForm.Controls.Add(formFlow);

            panelGrid = panelGrid ?? new Panel();
            panelGrid.Dock = DockStyle.Fill;
            panelGrid.Padding = new Padding(12);
            panelGrid.BackColor = Color.White;
            dgvOnKayitlar.Dock = DockStyle.Fill;
            dgvOnKayitlar.Location = Point.Empty;
            ModernWinForms.StyleGrid(dgvOnKayitlar);
            panelGrid.Controls.Clear();
            panelGrid.Controls.Add(dgvOnKayitlar);

            dgvOnKayitlar.ContextMenuStrip = BuildPreRegistrationContextMenu();
            panelForm.ContextMenuStrip = dgvOnKayitlar.ContextMenuStrip;
            tabPageOgrenciOnKayit.ContextMenuStrip = dgvOnKayitlar.ContextMenuStrip;

            tabPageOgrenciOnKayit.KeyDown += (s, e) =>
            {
                if (e.Control && e.KeyCode == Keys.S) { RunPreRegistrationAdd(); e.Handled = true; }
                if (e.Control && e.KeyCode == Keys.Enter) { RunPreRegistrationConfirm(); e.Handled = true; }
                if (e.KeyCode == Keys.Delete) { RunPreRegistrationDelete(); e.Handled = true; }
            };

            page.Controls.Add(header, 0, 0);
            page.Controls.Add(commandCard, 0, 1);
            page.Controls.Add(panelForm, 0, 2);
            page.Controls.Add(panelGrid, 0, 3);

            tabPageOgrenciOnKayit.Controls.Clear();
            tabPageOgrenciOnKayit.Controls.Add(page);
            tabPageOgrenciOnKayit.ResumeLayout(false);
        }


        private void ConfigureStudentManagementTab()
        {
            tabPageStok.SuspendLayout();
            tabPageStok.BackColor = ModernWinForms.PageBack;
            tabPageStok.Padding = new Padding(14);
            tabPageStok.AutoScroll = true;

            ModernWinForms.HideLegacyButton(btnOgrenciYonetimiAra);
            ModernWinForms.HideLegacyButton(btnOgrenciYonetimiSinifGuncelle);
            ModernWinForms.HideLegacyButton(btnOgrenciYonetimiSinifSil);
            ModernWinForms.HideLegacyButton(btnOgrenciYonetimiSinifKaydet);

            var shell = CreateScrollShell("studentManagementShell");
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 1,
                RowCount = 5,
                BackColor = Color.Transparent,
                MinimumSize = new Size(980, 720),
                Padding = new Padding(0)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 76));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 420));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 250));

            layout.Controls.Add(CreateModuleHeader(
                "Öğrenci Yönetimi",
                "Öğrenci listesi, sınıf atamaları ve arşiv işlemleri tek ekranda; seçimler sağ tık ile yönetilir.",
                "Faz 2 • Grid + sınıf kokpiti"), 0, 0);

            var searchCard = ModernWinForms.CreateCard("studentSearchCard");
            var searchFlow = ModernWinForms.CreateFlow("studentSearchFlow", true);
            ModernWinForms.StyleInput(txtOgrenciYonetimiAra, 340);
            txtOgrenciYonetimiAra.PlaceholderText = "Öğrenci adı, soyadı, sınıf veya telefon ara...";
            txtOgrenciYonetimiAra.TextChanged += (s, e) => ModernWinForms.ApplySearchFilter(dataGridViewStok, txtOgrenciYonetimiAra.Text);
            searchFlow.Controls.Add(txtOgrenciYonetimiAra);
            searchFlow.Controls.Add(ModernWinForms.CreateBadge("Enter: ara • Sağ tık: işlem"));
            searchCard.Controls.Add(searchFlow);
            layout.Controls.Add(searchCard, 0, 1);

            PrepareGroupBox(groupBox2, "Öğrenci Listesi");
            groupBox2.Controls.Clear();
            ModernWinForms.StyleGrid(dataGridViewStok);
            dataGridViewStok.Dock = DockStyle.Fill;
            dataGridViewStok.Location = Point.Empty;
            groupBox2.Controls.Add(dataGridViewStok);
            layout.Controls.Add(groupBox2, 0, 2);

            var classStripCard = ModernWinForms.CreateCard("classCommandCard", 0);
            var classStrip = ModernWinForms.CreateCommandStrip("classCommandStrip");
            classStrip.Items.Add(ModernWinForms.CreateCommand("Sınıf kaydet", (s, e) => RunClassSave()));
            classStrip.Items.Add(ModernWinForms.CreateCommand("Sınıf güncelle", (s, e) => RunClassUpdate()));
            classStrip.Items.Add(ModernWinForms.CreateCommand("Sınıf sil", (s, e) => RunClassDelete()));
            classStrip.Items.Add(new ToolStripSeparator());
            classStrip.Items.Add(ModernWinForms.CreateCommand("Yenile", (s, e) => { LoadStockData(UserId); SinifLoad(UserId); }));
            classStripCard.Controls.Add(classStrip);
            layout.Controls.Add(classStripCard, 0, 3);

            var bottomLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent
            };
            bottomLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62));
            bottomLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38));

            PrepareGroupBox(groupBox1, "Sınıf Listesi");
            groupBox1.Controls.Clear();
            ModernWinForms.StyleGrid(DgvOgrenciYonetimiSiniflar);
            DgvOgrenciYonetimiSiniflar.Dock = DockStyle.Fill;
            DgvOgrenciYonetimiSiniflar.Location = Point.Empty;
            groupBox1.Controls.Add(DgvOgrenciYonetimiSiniflar);

            var classEditor = ModernWinForms.CreateCard("classEditorCard");
            var classFlow = ModernWinForms.CreateFlow("classEditorFlow", true);
            PrepareInlineGroupBox(groupBox14, "Sınıf Adı", 230, 82);
            PrepareInlineGroupBox(groupBox15, "Yaş Grubu", 230, 82);
            PrepareInlineGroupBox(groupBox13, "Öğretmen", 230, 82);
            ModernWinForms.StyleInput(txtOgrenciYonetimiSınıfAdı, 200);
            ModernWinForms.StyleInput(cbxOgrenciYonetimiYasGrubu, 200);
            ModernWinForms.StyleInput(cbxOgrenciYonetimiOgretmen, 200);
            classFlow.Controls.AddRange(new Control[] { groupBox14, groupBox15, groupBox13 });
            classEditor.Controls.Add(classFlow);

            bottomLayout.Controls.Add(groupBox1, 0, 0);
            bottomLayout.Controls.Add(classEditor, 1, 0);
            layout.Controls.Add(bottomLayout, 0, 4);

            dataGridViewStok.ContextMenuStrip = contextMenuStrip1;
            DgvOgrenciYonetimiSiniflar.ContextMenuStrip = BuildClassContextMenu();

            shell.Controls.Add(layout);
            tabPageStok.Controls.Clear();
            tabPageStok.Controls.Add(shell);
            tabPageStok.ResumeLayout(false);
        }


        private void ConfigurePaymentTab()
        {
            tabPageSatis.SuspendLayout();
            tabPageSatis.BackColor = ModernWinForms.PageBack;
            tabPageSatis.Padding = new Padding(14);

            ModernWinForms.HideLegacyButton(btnMakeSale);

            var layout = ModernWinForms.CreatePageLayout(4);
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 76));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 98));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            layout.Controls.Add(CreateModuleHeader(
                "Öğrenci Ödeme Yönetimi",
                "Ödeme girişi, öğrenci seçimi ve tahsilat takibi daha geniş, okunaklı ve responsive yapıya taşındı.",
                "Faz 2 • Finans kokpiti"), 0, 0);

            var commandCard = ModernWinForms.CreateCard("paymentCommandCard", 0);
            var strip = ModernWinForms.CreateCommandStrip("paymentCommandStrip");
            strip.Items.Add(ModernWinForms.CreateCommand("Ödeme girişi", (s, e) => RunPaymentEntry()));
            strip.Items.Add(ModernWinForms.CreateCommand("Yenile", (s, e) => LoadPaymentData(UserId)));
            strip.Items.Add(new ToolStripSeparator());
            strip.Items.Add(new ToolStripLabel("İpucu: Gridde sağ tık ile ödeme detayları ve arşiv işlemlerine ulaş."));
            commandCard.Controls.Add(strip);
            layout.Controls.Add(commandCard, 0, 1);

            var filters = ModernWinForms.CreateCard("paymentFiltersCard");
            var top = ModernWinForms.CreateFlow("paymentActionBar", true);
            PrepareInlineGroupBox(groupBox8, "Öğrenci Seçimi", 280, 76);
            PrepareInlineGroupBox(groupBox9, "Öğrenci Ücreti", 210, 76);
            ModernWinForms.StyleInput(comboBoxStok, 240);
            ModernWinForms.StyleInput(numericQuantitySold, 170);
            top.Controls.Add(groupBox8);
            top.Controls.Add(groupBox9);
            filters.Controls.Add(top);
            layout.Controls.Add(filters, 0, 2);

            var gridBox = CreateCardPanel("paymentGridCard");
            ModernWinForms.StyleGrid(dataOgrVw);
            dataOgrVw.Dock = DockStyle.Fill;
            dataOgrVw.Location = Point.Empty;
            dataOgrVw.ContextMenuStrip = contextMenuStrip1;
            gridBox.Controls.Add(dataOgrVw);
            layout.Controls.Add(gridBox, 0, 3);

            tabPageSatis.Controls.Clear();
            tabPageSatis.Controls.Add(layout);
            tabPageSatis.ResumeLayout(false);
        }


        private void ConfigurePersonnelTab()
        {
            tabPagePersonelYonetimi.SuspendLayout();
            tabPagePersonelYonetimi.BackColor = ModernWinForms.PageBack;
            tabPagePersonelYonetimi.Padding = new Padding(14);

            var layout = ModernWinForms.CreatePageLayout(3);
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 76));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            layout.Controls.Add(CreateModuleHeader(
                "Personel Yönetimi",
                "Personel listesi, arşiv ve hızlı kayıt işlemleri grid odaklı hale getirildi.",
                "Faz 2 • Personel kokpiti"), 0, 0);

            var searchCard = ModernWinForms.CreateCard("personnelSearchCard");
            var searchFlow = ModernWinForms.CreateFlow("personnelSearchFlow", true);
            var txtPersonelAra = new TextBox { Name = "txtPersonelAra", PlaceholderText = "Personel adı, görev, departman veya telefon ara..." };
            ModernWinForms.StyleInput(txtPersonelAra, 360);
            txtPersonelAra.TextChanged += (s, e) => ModernWinForms.ApplySearchFilter(dgvPersonelYonetimi, txtPersonelAra.Text);
            searchFlow.Controls.Add(txtPersonelAra);
            searchFlow.Controls.Add(ModernWinForms.CreateBadge("Çift tık: detay • Sağ tık: işlem"));
            searchCard.Controls.Add(searchFlow);
            layout.Controls.Add(searchCard, 0, 1);

            var gridBox = CreateCardPanel("personnelGridCard");
            ModernWinForms.StyleGrid(dgvPersonelYonetimi);
            dgvPersonelYonetimi.Dock = DockStyle.Fill;
            dgvPersonelYonetimi.Location = Point.Empty;
            dgvPersonelYonetimi.ContextMenuStrip = contextMenuStrip1;
            gridBox.Controls.Add(dgvPersonelYonetimi);
            layout.Controls.Add(gridBox, 0, 2);

            tabPagePersonelYonetimi.Controls.Clear();
            tabPagePersonelYonetimi.Controls.Add(layout);
            tabPagePersonelYonetimi.ResumeLayout(false);
        }


        private void ConfigureIncomeExpenseTab()
        {
            tabPageGelirGider.SuspendLayout();
            tabPageGelirGider.BackColor = ModernWinForms.PageBack;
            tabPageGelirGider.Padding = new Padding(14);

            ModernWinForms.HideLegacyButton(btnAddIncomeExpense);
            ModernWinForms.HideLegacyButton(FaturaBtn);

            var layout = ModernWinForms.CreatePageLayout(4);
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 76));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 92));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            layout.Controls.Add(CreateModuleHeader(
                "Gelir - Gider Yönetimi",
                "Gelir/gider kaydı, fatura merkezi ve finans hareketleri tek ekranda sadeleştirildi.",
                "Faz 2 • Finans + fatura"), 0, 0);

            var commandCard = ModernWinForms.CreateCard("incomeExpenseCommandCard", 0);
            var strip = ModernWinForms.CreateCommandStrip("incomeExpenseCommandStrip");
            strip.Items.Add(ModernWinForms.CreateCommand("Gelir/Gider kaydet", (s, e) => RunIncomeExpenseSave()));
            strip.Items.Add(ModernWinForms.CreateCommand("Fatura merkezi", (s, e) => RunInvoiceCenter()));
            strip.Items.Add(ModernWinForms.CreateCommand("Yenile", (s, e) => LoadSalesData()));
            strip.Items.Add(new ToolStripSeparator());
            strip.Items.Add(new ToolStripLabel("Ctrl+S: kaydet • Ctrl+F: fatura"));
            commandCard.Controls.Add(strip);
            layout.Controls.Add(commandCard, 0, 1);

            var entryCard = ModernWinForms.CreateCard("incomeExpenseEntryCard");
            var top = ModernWinForms.CreateFlow("incomeExpenseActionBar", true);
            ModernWinForms.StyleInput(txtDescription, 360);
            ModernWinForms.StyleInput(numericAmount, 160);
            ModernWinForms.StyleCheck(radioIncome);
            ModernWinForms.StyleCheck(radioExpense);
            txtDescription.PlaceholderText = "Açıklama / işlem notu";
            top.Controls.Add(txtDescription);
            top.Controls.Add(numericAmount);
            top.Controls.Add(radioIncome);
            top.Controls.Add(radioExpense);
            entryCard.Controls.Add(top);
            layout.Controls.Add(entryCard, 0, 2);

            var gridBox = CreateCardPanel("incomeExpenseGridCard");
            ModernWinForms.StyleGrid(dataGridOdeme);
            dataGridOdeme.Dock = DockStyle.Fill;
            dataGridOdeme.Location = Point.Empty;
            gridBox.Controls.Add(dataGridOdeme);
            layout.Controls.Add(gridBox, 0, 3);

            tabPageGelirGider.KeyDown += (s, e) =>
            {
                if (e.Control && e.KeyCode == Keys.S) { RunIncomeExpenseSave(); e.Handled = true; }
                if (e.Control && e.KeyCode == Keys.F) { RunInvoiceCenter(); e.Handled = true; }
            };

            tabPageGelirGider.Controls.Clear();
            tabPageGelirGider.Controls.Add(layout);
            tabPageGelirGider.ResumeLayout(false);
        }


        private void ConfigureReportsTab()
        {
            tabPageOzelRaporlar.SuspendLayout();
            tabPageOzelRaporlar.BackColor = ModernWinForms.PageBack;
            tabPageOzelRaporlar.Padding = new Padding(14);
            var layout = ModernWinForms.CreatePageLayout(2);
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 76));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.Controls.Add(CreateModuleHeader("Özel Raporlar", "Kaydedilmiş raporları responsive gridde incele.", "Faz 2"), 0, 0);
            var gridBox = CreateCardPanel("reportsGridCard");
            ModernWinForms.StyleGrid(salesGrid);
            salesGrid.Dock = DockStyle.Fill;
            salesGrid.Location = Point.Empty;
            gridBox.Controls.Add(salesGrid);
            layout.Controls.Add(gridBox, 0, 1);
            tabPageOzelRaporlar.Controls.Clear();
            tabPageOzelRaporlar.Controls.Add(layout);
            tabPageOzelRaporlar.ResumeLayout(false);
        }




        private void RunPreRegistrationAdd() => btnOnKayitEkle_Click(btnOnKayitEkle, EventArgs.Empty);
        private void RunPreRegistrationConfirm() => btnKesinKayitYap_Click_1(btnKesinKayitYap, EventArgs.Empty);
        private void RunPreRegistrationDelete() => btnOnKayitSil_Click(btnOnKayitSil, EventArgs.Empty);
        private void RunClassSave() => btnOgrenciYonetimiSinifKaydet_Click(btnOgrenciYonetimiSinifKaydet, EventArgs.Empty);
        private void RunClassUpdate() => btnOgrenciYonetimiSinifGuncelle_Click(btnOgrenciYonetimiSinifGuncelle, EventArgs.Empty);
        private void RunClassDelete() => btnOgrenciYonetimiSinifSil_Click(btnOgrenciYonetimiSinifSil, EventArgs.Empty);
        private void RunPaymentEntry() => btnMakeSale_Click(btnMakeSale, EventArgs.Empty);
        private void RunIncomeExpenseSave() => btnAddIncomeExpense_Click(btnAddIncomeExpense, EventArgs.Empty);
        private void RunInvoiceCenter() => FaturaBtn_Click(FaturaBtn, EventArgs.Empty);

        private Panel CreateModuleHeader(string title, string subtitle, string badgeText)
        {
            var card = ModernWinForms.CreateCard("moduleHeader", 14);
            var badge = ModernWinForms.CreateBadge(badgeText);
            badge.Dock = DockStyle.Right;
            badge.Width = 260;
            card.Controls.Add(badge);
            card.Controls.Add(ModernWinForms.CreateSubtitle(subtitle));
            card.Controls.Add(ModernWinForms.CreateTitle(title));
            return card;
        }

        private ContextMenuStrip BuildPreRegistrationContextMenu()
        {
            var menu = new ContextMenuStrip();
            menu.Items.Add("Ön kayıt ekle", null, (s, e) => RunPreRegistrationAdd());
            menu.Items.Add("Kesin kayda çevir", null, (s, e) => RunPreRegistrationConfirm());
            menu.Items.Add("Seçili ön kaydı sil", null, (s, e) => RunPreRegistrationDelete());
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("Listeyi yenile", null, (s, e) => LoadOnKayitlar(UserId));
            return menu;
        }

        private ContextMenuStrip BuildClassContextMenu()
        {
            var menu = new ContextMenuStrip();
            menu.Items.Add("Sınıf kaydet", null, (s, e) => RunClassSave());
            menu.Items.Add("Sınıf güncelle", null, (s, e) => RunClassUpdate());
            menu.Items.Add("Sınıf sil", null, (s, e) => RunClassDelete());
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("Yenile", null, (s, e) => SinifLoad(UserId));
            return menu;
        }

        private void InstallMainShellLayout()
        {
            if (_mainLayout != null) return;

            Controls.Remove(tabControl);

            _mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.FromArgb(246, 248, 252),
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            _mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 258));
            _mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            _sidebarPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(12)
            };

            var sidebarLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Color.Transparent
            };
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 104));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var brandPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            var brandTitle = new Label
            {
                Text = "BKS",
                Dock = DockStyle.Top,
                Height = 38,
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point, 162),
                TextAlign = ContentAlignment.BottomLeft
            };

            var brandSubtitle = new Label
            {
                Text = "Anaokulu Yönetim Sistemi",
                Dock = DockStyle.Top,
                Height = 42,
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 162),
                TextAlign = ContentAlignment.TopLeft
            };

            brandPanel.Controls.Add(brandSubtitle);
            brandPanel.Controls.Add(brandTitle);

            _navigationList = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 4, 0, 0)
            };

            sidebarLayout.Controls.Add(brandPanel, 0, 0);
            sidebarLayout.Controls.Add(_navigationList, 0, 1);
            _sidebarPanel.Controls.Add(sidebarLayout);

            _contentLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Color.FromArgb(246, 248, 252),
                Padding = Padding.Empty,
                Margin = Padding.Empty
            };
            _contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 86));
            _contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            _topBarPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(24, 12, 24, 10)
            };

            _headerTitleLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 34,
                ForeColor = Color.FromArgb(15, 23, 42),
                Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point, 162),
                TextAlign = ContentAlignment.MiddleLeft
            };

            _headerSubtitleLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 24,
                ForeColor = Color.FromArgb(100, 116, 139),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 162),
                TextAlign = ContentAlignment.MiddleLeft
            };

            _headerUserLabel = new Label
            {
                Dock = DockStyle.Right,
                Width = 320,
                ForeColor = Color.FromArgb(51, 65, 85),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 162),
                TextAlign = ContentAlignment.MiddleRight
            };

            _topBarPanel.Controls.Add(_headerSubtitleLabel);
            _topBarPanel.Controls.Add(_headerTitleLabel);
            _topBarPanel.Controls.Add(_headerUserLabel);

            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.ItemSize = new Size(0, 1);
            tabControl.SizeMode = TabSizeMode.Fixed;
            tabControl.Dock = DockStyle.Fill;
            tabControl.SelectedIndexChanged += (s, e) =>
            {
                UpdateHeaderForCurrentTab();
                UpdateNavigationVisualState();
            };

            _contentLayout.Controls.Add(_topBarPanel, 0, 0);
            _contentLayout.Controls.Add(tabControl, 0, 1);

            _mainLayout.Controls.Add(_sidebarPanel, 0, 0);
            _mainLayout.Controls.Add(_contentLayout, 1, 0);
            Controls.Add(_mainLayout);

            BuildNavigationItems();
            UpdateHeaderForCurrentTab();
        }

        private Panel CreateScrollShell(string name)
        {
            return new Panel
            {
                Name = name,
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent
            };
        }

        private Panel CreateCardPanel(string name)
        {
            return ModernWinForms.CreateCard(name, 12);
        }

        private FlowLayoutPanel CreateFlowPanel(string name, bool wrap)
        {
            return new FlowLayoutPanel
            {
                Name = name,
                Dock = DockStyle.Fill,
                AutoScroll = false,
                WrapContents = wrap,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.Transparent
            };
        }

        private void PrepareGroupBox(GroupBox groupBox, string text)
        {
            ModernWinForms.StyleGroupBox(groupBox, text);
        }


        private void PrepareInlineGroupBox(GroupBox groupBox, string text, int width, int height)
        {
            PrepareGroupBox(groupBox, text);
            groupBox.Dock = DockStyle.None;
            groupBox.Width = width;
            groupBox.Height = height;
            groupBox.Margin = new Padding(0, 0, 12, 12);
        }

        private void PrepareInput(Control control, int width, int height = 34)
        {
            ModernWinForms.StyleInput(control, width, height);
        }

        private void PreparePrimaryButton(Button button, string text, Color backColor, int width = 172, int height = 42)
        {
            button.Text = text;
            button.Width = width;
            button.Height = height;
            button.Margin = new Padding(0, 0, 12, 12);
            button.BackColor = backColor;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button.Cursor = Cursors.Hand;
            button.Image = null;
            button.TextImageRelation = TextImageRelation.ImageBeforeText;
            button.ImageAlign = ContentAlignment.MiddleLeft;
            button.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void StyleAllGrids()
        {
            foreach (var grid in GetAllControls(this).OfType<DataGridView>())
            {
                StyleGrid(grid);
            }
        }

        private void StyleGrid(DataGridView grid)
        {
            ModernWinForms.StyleGrid(grid);
        }

        private IEnumerable<Control> GetAllControls(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                yield return child;
                foreach (Control grandChild in GetAllControls(child))
                    yield return grandChild;
            }
        }

        private void UseSegoeFontRecursive(Control parent)
        {
            parent.Font = new Font("Segoe UI", parent.Font.Size <= 0 ? 9.5F : parent.Font.Size, parent.Font.Style, GraphicsUnit.Point, 162);
            foreach (Control child in parent.Controls)
                UseSegoeFontRecursive(child);
        }

        private void ApplyAdaptiveHeights()
        {
            if (!_layoutInitialized || panelForm == null) return;

            int width = ClientSize.Width;
            panelForm.Height = width < 1250 ? 270 : 210;

            if (_contentLayout != null && _contentLayout.RowStyles.Count > 0)
            {
                _contentLayout.RowStyles[0].Height = width < 1050 ? 72 : 86;
            }

            if (_mainLayout != null && _mainLayout.ColumnStyles.Count > 0)
            {
                bool compact = width < 1050;
                _mainLayout.ColumnStyles[0].Width = compact ? 76 : 258;

                foreach (var pair in navigationButtons)
                {
                    pair.Value.Text = BuildNavigationText(pair.Key, compact);
                    pair.Value.Width = compact ? 52 : 222;
                    pair.Value.Height = compact ? 50 : 58;
                    pair.Value.TextAlign = compact ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft;
                    pair.Value.Margin = compact ? new Padding(0, 4, 0, 4) : new Padding(8, 4, 8, 4);
                }
            }
        }

        private void CloseApplication()
        {
            var result = MessageBox.Show(
                "Program kapatılsın mı?",
                "Çıkış",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                Close();
        }

        #endregion

        #region Form Events

        private void Form2_Load(object sender, EventArgs e)
        {
            dgvPersonelYonetimi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPersonelYonetimi.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            this.Text = (GetCompanyName(UserId) + " Anaokulu Yönetim Sistemi").ToUpper();
            this.materialLabel3.Text = ("Merhaba " + GetLastUser(UserId) + " Son Giriş Zamanın : " + GetLastLoginTime(UserId)).ToUpper();
            if (_headerUserLabel != null)
                _headerUserLabel.Text = (GetLastUser(UserId) + "  •  " + GetLastLoginTime(UserId));

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

            dataGridViewStok.Tag = StudentModuleTag;
            dgvPersonelYonetimi.Tag = PersonelModuleTag;
            DgvOgrenciYonetimiSiniflar.Tag = 0;

            dataGridViewStok.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewStok.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            if (dataGridViewStok.Columns.Contains("Id")) dataGridViewStok.Columns["Id"].Visible = false;
            if (dataGridViewStok.Columns.Contains("MonthlyFee")) dataGridViewStok.Columns["MonthlyFee"].Visible = false;
            if (dataGridViewStok.Columns.Contains("FotoId")) dataGridViewStok.Columns["FotoId"].Visible = false;

            dataGridViewStok.MouseDown += DataGridView_MouseDown;
            dgvPersonelYonetimi.MouseDown += DataGridView_MouseDown;
            DgvOgrenciYonetimiSiniflar.MouseDown += DataGridView_MouseDown;

            StyleAllGrids();
            ApplyAdaptiveHeights();
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
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show("Modüller yüklenemedi!\n" + ex.Message);
                SetTabAccess(null, null);
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
            int activeTag = GetActiveGridTag();

            if (aktifDGV == null || aktifDGV.CurrentRow == null)
            {
                MessageBox.Show("Lütfen bir kayıt seçiniz!");
                return;
            }

            try
            {
                if (activeTag == StudentModuleTag)
                {
                    if (!TryGetSelectedGuid(dataGridViewStok, "Id", out Guid ogrenciId))
                    {
                        MessageBox.Show("Geçerli öğrenci bilgisi alınamadı.");
                        return;
                    }

                    using (var arsiv = new arsivForm(UserId, connectionString, ogrenciId))
                    {
                        arsiv.ShowDialog();
                    }
                }
                else if (activeTag == PersonelModuleTag)
                {
                    if (!TryGetSelectedGuid(dgvPersonelYonetimi, "PersonelId", out Guid personelId))
                    {
                        MessageBox.Show("Geçerli personel bilgisi alınamadı.");
                        return;
                    }

                    Guid arsivOgrenciId = EnsurePersonelArchiveStudent(personelId);

                    using (var arsiv = new arsivForm(UserId, connectionString, arsivOgrenciId))
                    {
                        arsiv.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Bu alanda arşiv işlemi desteklenmiyor.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arşiv açılırken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (aktifDGV == null)
                return;

            DataGridView.HitTestInfo hit = aktifDGV.HitTest(e.X, e.Y);

            if (hit.RowIndex < 0)
                return;

            if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
            {
                aktifDGV.ClearSelection();
                aktifDGV.Rows[hit.RowIndex].Selected = true;

                if (hit.ColumnIndex >= 0)
                    aktifDGV.CurrentCell = aktifDGV.Rows[hit.RowIndex].Cells[hit.ColumnIndex];
                else
                    aktifDGV.CurrentCell = aktifDGV.Rows[hit.RowIndex].Cells[0];
            }

            if (e.Button == MouseButtons.Right)
            {
                int activeTag = GetActiveGridTag();

                if (ödemeDetaylarıToolStripMenuItem != null)
                    ödemeDetaylarıToolStripMenuItem.Visible = activeTag == StudentModuleTag;

                if (arşivToolStripMenuItem != null)
                    arşivToolStripMenuItem.Visible = activeTag == StudentModuleTag || activeTag == PersonelModuleTag;

                contextMenuStrip1.Show(aktifDGV, e.Location);
            }
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
