using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Net;


namespace BKS
{
    public partial class Form2 : MaterialForm

    {

        public string connectionString = "Server=31.186.11.161;Database=asl2e6ancomtr_PaymentDBDB;User Id=asl2e6ancomtr_aslan;Password=Aslan123.@;TrustServerCertificate=True;";

        public Form2()
        {

            InitializeComponent();

            if (salesGrid.DataSource != null)
            {
                LoadSalesData();
            }

            LoadStockComboBox();
            LoadPaymentData();
            form1.Close();


        }
        [System.ComponentModel.Browsable(false)]
        public System.Windows.Forms.AutoScaleMode AutoScaleMode { get; set; }
        private void Form2_Load(object sender, EventArgs e)
        {


            this.Text = GetCompanyName(UserId) + " " + "Anaokulu Yönetim Sistemi".ToUpper();
            this.materialLabel3.Text = ("Merhaba " + GetLastUser(UserId) + " Son Giriş Zamanın : " + GetLastLoginTime(UserId)).ToUpper();
            LoadStockData(UserId);
            LoadModulesFromApi(UserId, Role);
            dataGridViewStok.AllowUserToAddRows = false;
            DgvOgrenciYonetimiSiniflar.AllowUserToAddRows = false;
            dgvPersonelYonetimi.AllowUserToAddRows = false;
            YasGrubuLoad();
            SinifLoad(UserId);

            dataGridViewStok.Columns["Id"].Visible = false;
            dataGridViewStok.Columns["MonthlyFee"].Visible = false;//önemli değiştirme
            dataGridViewStok.Columns["FotoId"].Visible = false;
            LoadTeacherComboBox(UserId);
            PersonelYonetimiLoad(UserId);
            dataGridViewStok.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewStok.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dtpPersonelCıkısTarihi.Enabled = false;
            txtPersonelAyrilmaNedeni.Enabled = false;
            txtPersonelKidemTazminat.Enabled = false;
            txtPersonelKimlik.Enabled = false;
            lblKimlikNum.Visible = false;
            //Image img = Image.FromFile("delete.jpg"); // resim dosya yolu
            //ResizeAndSetButtonImage(btnOgrenciYonetimiSinifSil, img);
            //timer1.Interval = 5000; // 5 saniye
            //timer1.Tick += Timer1_Tick;
            //timer1.Start();


        }
        private void ResizeAndSetButtonImage(Button button, Image image)
        {
            // Butonun boyutlarına göre yeni resim boyutunu ayarla (biraz margin bırakmak için -10 yaptık)
            int width = button.Width - 10;
            int height = button.Height - 10;

            // Yeni boyutlandırılmış resmi oluştur
            Image resized = new Bitmap(image, new Size(width, height));

            // Butona resmi ata
            button.Image = resized;
            button.ImageAlign = ContentAlignment.MiddleCenter;
            button.TextImageRelation = TextImageRelation.Overlay; // Resim üstünde yazı
        }
        Form1 form1 = new Form1();


        public class ModuleResponse
        {
            public List<string> modules { get; set; }
            public string role { get; set; }
        }

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
                    SetTabAccess(result.modules, result.role);
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show("Modüller yüklenemedi!\n" + ex.Message);
                SetTabAccess(null, null);
            }
        }

        // Stok Yönetimi
        public void LoadStockData(Guid UserId)
        {
            string ogrenciadi = string.IsNullOrEmpty(txtOgrenciYonetimiAra.Text) ? null : txtOgrenciYonetimiAra.Text;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "exec GetStudent @UserId,@Name";// arama modülü eklenecek
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Name", string.IsNullOrEmpty(ogrenciadi) ? (object)DBNull.Value : ogrenciadi);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                dataGridViewStok.DataSource = dt;
                dataGridViewStok.Refresh();

            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            LoadStockData(UserId);
        }

        public DataTable LoadStockDataRefresh(Guid UserId)
        {
            string ogrenciadi = string.IsNullOrEmpty(txtOgrenciYonetimiAra.Text) ? null : txtOgrenciYonetimiAra.Text;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "exec GetStudent @UserId,@Name";// arama modülü eklenecek
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Name", string.IsNullOrEmpty(ogrenciadi) ? (object)DBNull.Value : ogrenciadi);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
        }
        private void dataGridViewStok_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

        }
        public string GetLastLoginTime(Guid UserId)
        {
            string lastLoginTime = "Bilinmiyor";
            string query = "SELECT PreviousLogin FROM CompanyUsers WHERE UserId=@UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", UserId);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            lastLoginTime = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                }
            }

            return lastLoginTime;
        }
        public string GetLastUser(Guid UserId)
        {
            string Kullanici = "Bilinmiyor";
            string query = "SELECT Firstname FROM CompanyUsers WHERE UserId=@UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", UserId);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            Kullanici = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                }
            }

            return Kullanici;
        }
        private void btnOgrenciYonetimiSil_Click(object sender, EventArgs e)
        {
            if (dataGridViewStok.CurrentRow != null)
            {
                Guid id = (Guid)dataGridViewStok.CurrentRow.Cells["Id"].Value; // Seçili öğrencinin ID'sini al



                string query = @"
            Update AysStudents set IsDeleted=1
            WHERE Id = @id";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Parametreleri Ekle
                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.ExecuteNonQuery(); // SQL sorgusunu çalıştır
                    }
                }

                MessageBox.Show("Öğrenci Silindi Eski Kayıtlar için Loglara Bak..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DeleteAndLog("Aysstudents", "Id", id, UserId, "1", "DELETE");
                LoadStockData(UserId);
            }
        }
        private void SetTabAccess(List<string> activeModules, string role)
        {
            // Eğer role null/boş ya da activeModules null/boşsa tüm tab'ları gizle
            if (string.IsNullOrWhiteSpace(role) || activeModules == null || activeModules.Count == 0)
            {
                foreach (TabPage tab in tabControl.TabPages.Cast<TabPage>().ToList())
                {
                    HideTabPage(tab);
                }
                return;
            }

            // Admin ise tüm tab'leri açık bırak
            if (role.ToUpper() == "ADMİN") // Türkçe karaktere karşı hassasiyet için ToUpper
            {
                foreach (TabPage tab in tabControl.TabPages)
                {
                    tab.Enabled = true;
                    ShowTabPage(tab); // Gizlenmiş olabilir, yeniden göster
                }
                return;
            }

            // Admin değilse yetkilere göre aç/kapat
            foreach (TabPage tab in tabControl.TabPages.Cast<TabPage>().ToList())
            {
                if (activeModules.Contains(tab.Name))
                {
                    tab.Enabled = true;
                    ShowTabPage(tab);
                }
                else
                {
                    tab.Enabled = false;
                    HideTabPage(tab);
                }
            }
        }

        public void DeleteAndLog(string tableName, string primaryKeyColumn, Guid id, Guid UserId, string actions, string actionName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Silinecek kaydı önce JSON formatında al
                string selectQuery = $"SELECT * FROM {tableName} WHERE {primaryKeyColumn} = @Id";
                string deletedDataJson = "";

                using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                {
                    selectCmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader); // Bu satır zaten tüm veriyi alır
                        if (dt.Rows.Count > 0)
                        {
                            deletedDataJson = JsonConvert.SerializeObject(dt, Formatting.Indented);
                        }
                    }
                }

                // Eğer silinecek veri bulunduysa log'a kaydet
                if (!string.IsNullOrEmpty(deletedDataJson))
                {
                    string logQuery = "INSERT INTO DeleteLog (TableName, DeletedData, DeletedAt,DeleteUserId,Actions,ActionName,CompanyId) VALUES (@TableName, @DeletedData, @DeletedAt,@DeleteUserId,@Actions,@ActionName,(select top 1 CompanyId from CompanyUsers where UserId=@DeleteUserId))";
                    using (SqlCommand logCmd = new SqlCommand(logQuery, conn))
                    {
                        logCmd.Parameters.AddWithValue("@TableName", tableName.ToUpper());
                        logCmd.Parameters.AddWithValue("@DeletedData", deletedDataJson);
                        logCmd.Parameters.AddWithValue("@DeletedAt", DateTime.Now);
                        logCmd.Parameters.AddWithValue("@DeleteUserId", UserId);
                        logCmd.Parameters.AddWithValue("@Actions", actions);
                        logCmd.Parameters.AddWithValue("@ActionName", actionName.ToUpper());
                        logCmd.ExecuteNonQuery();
                    }
                }

                // Şimdi asıl kaydı sil
                //string deleteQuery = $"DELETE FROM {tableName} WHERE {primaryKeyColumn} = @Id";
                //using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                //{
                //    deleteCmd.Parameters.AddWithValue("@Id", id);
                //    deleteCmd.ExecuteNonQuery();
                //}
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
            {
                tabControl.TabPages.Remove(tabPage);
            }
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Form2 kapatıldığında tüm uygulamayı sonlandır
        }
        private void LoadPaymentData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT OgrenciAdi=(select Name from AYSStudents a where a.Id=StudentId),PaymentDate,Amount FROM AYSFeePayments order by StudentId", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataOgrVw.DataSource = dt;
            }
        }

        public void LoadStockComboBox()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(" select Id=Id,'İsim'=Name,'Soyisim'=Surname,'Baba Adı'=FatherName,'Doğum Tarihi'=BirthDate,\r\n                   'Öğrenci Kodu'=StudentCode,'Ödeme Durumu'=PaymentStatus,'Ödenen Tutar'=MonthlyFee,'Aktif Öğrenci mi'=case when IsActive=1 then'Evet' else'Hayır' end \r\n                   ,'Baba Adresi'=FatherAddress,'Anne Adresi'=MotherAddress,'Baba Telefon'=FatherPhoneNumber,'Anne Telefon'=MotherPhonenumber, 'Aile Ayrı Mı'=case when IsMarried=1 then'Evet' else'Hayır' end ,'Öğrenci Hakkında'=StudentsDetails,'Anne Adı'= MotherName from AYSstudents ", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                comboBoxStok.Items.Clear();
                while (reader.Read())
                {
                    comboBoxStok.Items.Add(new ComboBoxItem
                    {
                        Text = reader["İsim"].ToString(),
                        Value = reader["Id"].ToString()
                    });
                }
            }
        }
        private void LoadStudentClassComboBox(Guid UserId)
        {
            Form2 form2 = new Form2();
            OgrenciForm ogrForm = new OgrenciForm(form2);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand("select ClassName,[Group] from AYSClasses where SchoolId=(Select CompanyId from CompanyUsers where UserId=@UserId)", conn);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader reader = cmd.ExecuteReader();
                ogrForm.cmbogrsınıf.Items.Clear();
                while (reader.Read())
                {
                    ogrForm.cmbogrsınıf.Items.Add(new ComboBoxItem
                    {
                        Text = reader["ClassName"].ToString(),
                        Value = reader["Group"].ToString()
                    });
                }
            }
        }
        private void LoadTeacherComboBox(Guid UserId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand("select isim=(FirstName+' '+LastName), PersonelId from personel where CompanyId=(Select CompanyId from CompanyUsers where UserId=@UserId) and IsTeacher=1", conn);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader reader = cmd.ExecuteReader();
                cbxOgrenciYonetimiOgretmen.Items.Clear();
                while (reader.Read())
                {
                    cbxOgrenciYonetimiOgretmen.Items.Add(new ComboBoxItem
                    {
                        Text = reader["isim"].ToString(),
                        Value = reader["PersonelId"].ToString()
                    });
                }
            }
        }
        private void LoadSalesData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // SQL sorgusundan veri çekiliyor
                SqlDataAdapter adapter = new SqlDataAdapter("exec GetDynamicQueryResult 1", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Veri tablosunu DataGridView'e bağla
                salesGrid.DataSource = dt;

                // DataGridView başlıklarını ayarla

                salesGrid.Columns["Aciklama"].HeaderText = "Açıklama";
                salesGrid.Columns["Miktar"].HeaderText = "Miktar";
                salesGrid.Columns["Tip"].HeaderText = "Tür";
                salesGrid.Columns["Tarih"].HeaderText = "Tarih";
                salesGrid.Columns["NetMiktar"].HeaderText = "Net Miktar";

                // Başlık sıralamalarını değiştir
                // İlk sütun
                salesGrid.Columns["Aciklama"].DisplayIndex = 1;  // İkinci sütun
                salesGrid.Columns["Miktar"].DisplayIndex = 2;    // Üçüncü sütun
                salesGrid.Columns["Tip"].DisplayIndex = 3;       // Dördüncü sütun
                salesGrid.Columns["Tarih"].DisplayIndex = 4;     // Beşinci sütun
                salesGrid.Columns["NetMiktar"].DisplayIndex = 5; // Altıncı sütun

                // Kullanıcılar sütunları yeniden sıralayabilir
                salesGrid.AllowUserToOrderColumns = true;

                // Otomatik sütun genişliği ayarla
                salesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }


        private void dataGridViewStok_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dataGridViewStok.HitTest(e.X, e.Y);

            if (hit.RowIndex >= 0) // Eğer geçerli bir satıra tıklanmışsa
            {
                // Sağ veya sol tık yapıldığında satırı seç
                if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
                {
                    dataGridViewStok.ClearSelection(); // Önceki seçimleri temizle
                    dataGridViewStok.Rows[hit.RowIndex].Selected = true; // Yeni satırı seç
                    dataGridViewStok.CurrentCell = dataGridViewStok.Rows[hit.RowIndex].Cells[0]; // Seçili satırı aktif yap
                }

                // Eğer sağ tık yapıldıysa bağlam menüsünü aç
                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Show(dataGridViewStok, e.Location);
                }
            }
        }


        private void ShowPaymentDetails(Guid studentId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "select p.Id, 'Ödeme Tutarı'=p.Amount, 'Ödeme Tarihi'=p.PaymentDate, p.IsApproved,'Ödendi Mi?'=case when p.IsApproved=1 then 'Ödendi'else 'Ödenecek'end, 'Ödenen Tarihi'=p.ApprovedDate FROM AYSFeePayments p WHERE p.StudentId = @StudentId AND p.Isdeleted = 0 AND p.SchoolId = (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId) order by p.PaymentDate";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    DataTable paymentDetails = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(paymentDetails);

                    // Yeni form
                    Form paymentForm = new Form
                    {
                        Text = "Ödeme Detayları",
                        Size = new Size(900, 600)
                    };

                    // DataGridView
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
                        {
                            paymentGrid.Columns["Id"].Visible = false;
                            paymentGrid.Columns["IsApproved"].Visible = false;
                        }
                    };
                    paymentGrid.RowPrePaint += (s, e) =>
                    {
                        var grid = s as DataGridView;
                        var row = grid.Rows[e.RowIndex];
                        if (row.Cells["IsApproved"].Value != DBNull.Value &&
                            Convert.ToBoolean(row.Cells["IsApproved"].Value))
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        if (row.Cells["IsApproved"].Value != DBNull.Value &&
                           !Convert.ToBoolean(row.Cells["IsApproved"].Value))
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(191, 81, 79);
                        }
                    };
                    // Sağ tıklama menüsü
                    ContextMenuStrip contextMenu = new ContextMenuStrip();
                    ToolStripMenuItem generatePlanItem = new ToolStripMenuItem("Aylık Ödeme Planı Oluştur");
                    contextMenu.Items.Add(generatePlanItem);
                    paymentGrid.ContextMenuStrip = contextMenu;

                    generatePlanItem.Click += (s, e) =>
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
                            if (decimal.TryParse(txtTutar.Text, out decimal tutar))
                            {
                                int aySayisi = (int)nudAy.Value;
                                using (SqlConnection connn = new SqlConnection(connectionString))
                                {
                                    connn.Open();
                                    for (int i = 0; i < aySayisi; i++)
                                    {
                                        DateTime paymentDate = DateTime.Now.AddMonths(i);
                                        string insertQuery = "INSERT INTO AYSFeePayments (Id, StudentId, Amount, PaymentDate, SchoolId) VALUES (@Id, @StudentId, @Amount, @PaymentDate, (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId))";
                                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, connn))
                                        {
                                            insertCmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
                                            insertCmd.Parameters.AddWithValue("@StudentId", studentId);
                                            insertCmd.Parameters.AddWithValue("@Amount", tutar);
                                            insertCmd.Parameters.AddWithValue("@PaymentDate", paymentDate);
                                            insertCmd.Parameters.AddWithValue("@UserId", UserId);
                                            insertCmd.ExecuteNonQuery();
                                        }
                                    }

                                    paymentGrid.DataSource = OdemeLoad(UserId, studentId);
                                    MessageBox.Show("Aylık ödeme planı başarıyla oluşturuldu.");
                                    planForm.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Geçerli bir tutar giriniz.");
                            }
                        };

                        planForm.Controls.Add(lblTutar);
                        planForm.Controls.Add(txtTutar);
                        planForm.Controls.Add(lblAySayisi);
                        planForm.Controls.Add(nudAy);
                        planForm.Controls.Add(btnOlustur);

                        planForm.ShowDialog();
                    };

                    // Ödeme ekleme paneli
                    Panel addPaymentPanel = new Panel { Dock = DockStyle.Bottom, Height = 100 };

                    Label lblAmount = new Label { Text = "Tutar:", AutoSize = true, Location = new Point(20, 20) };
                    TextBox txtAmount = new TextBox { Location = new Point(100, 20), Width = 150 };

                    Label lblDate = new Label { Text = "Tarih:", AutoSize = true, Location = new Point(20, 60) };
                    DateTimePicker dtpDate = new DateTimePicker { Location = new Point(100, 60), Width = 150 };

                    Button btnAddPayment = new Button
                    {
                        Text = "Ödeme Ekle",
                        Location = new Point(300, 30),
                        Width = 150
                    };

                    btnAddPayment.Click += (s, e) =>
                    {
                        using (SqlConnection connn = new SqlConnection(connectionString))
                        {
                            connn.Open();
                            if (decimal.TryParse(txtAmount.Text, out decimal amount))
                            {
                                string insertQuery = "INSERT INTO AYSFeePayments (Id, StudentId, Amount, PaymentDate, SchoolId) VALUES (@Id, @StudentId, @Amount, @PaymentDate, (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId))";
                                using (SqlCommand insertCmd = new SqlCommand(insertQuery, connn))
                                {
                                    insertCmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
                                    insertCmd.Parameters.AddWithValue("@StudentId", studentId);
                                    insertCmd.Parameters.AddWithValue("@Amount", amount);
                                    insertCmd.Parameters.AddWithValue("@PaymentDate", dtpDate.Value);
                                    insertCmd.Parameters.AddWithValue("@UserId", UserId);
                                    insertCmd.ExecuteNonQuery();

                                    MessageBox.Show("Ödeme başarıyla eklendi.");
                                    paymentGrid.DataSource = OdemeLoad(UserId, studentId);

                                }
                            }
                            else
                            {
                                MessageBox.Show("Geçerli bir tutar giriniz.");
                            }
                        }
                    };

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

                    // Ödeme onayla butonu
                    Button btnOnayla = new Button
                    {
                        Text = "Ödemeyi Onayla",
                        Dock = DockStyle.Bottom,
                        Height = 40
                    };

                    btnOnayla.Click += (s, e) =>
                    {
                        try
                        {
                            if (paymentGrid.SelectedRows.Count > 0)
                            {
                                try
                                {
                                    using (SqlConnection connn = new SqlConnection(connectionString))
                                    {
                                        connn.Open();

                                        foreach (DataGridViewRow selectedRow in paymentGrid.SelectedRows)
                                        {
                                            if (selectedRow.IsNewRow || selectedRow.Cells[0].Value == null)
                                                continue;

                                            Guid paymentId = (Guid)selectedRow.Cells["Id"].Value;
                                            string updateQuery = "UPDATE AYSFeePayments SET IsApproved = 1, ApprovedDate = @Now WHERE Id = @Id";
                                            SqlCommand cmd = new SqlCommand(updateQuery, connn);
                                            cmd.Parameters.AddWithValue("@Id", paymentId);
                                            cmd.Parameters.AddWithValue("@Now", DateTime.Now);
                                            cmd.ExecuteNonQuery();
                                        }
                                    }

                                    if (paymentGrid.SelectedRows.Count == 1)
                                    {
                                        MessageBox.Show("Ödeme onaylandı.");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Ödemeler onaylandı.");
                                    }

                                    paymentGrid.DataSource = OdemeLoad(UserId, studentId);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("Hata oluştu. Lütfen boş veya geçersiz satır seçmeyin.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Lütfen onaylamak için en az bir ödeme seçin.");
                            }
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("Hata Boş Satır Seçmeyiniz");
                        }

                    };

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
        }


        private void btnAddStock_Click(object sender, EventArgs e)
        {
        }

        // Satış Yönetimi
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

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO AYSFeePayments (Id, StudentId, Amount, PaymentDate, SchoolId) VALUES (@Id, @StudentId, @Amount, @PaymentDate, (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId))", conn);
                cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
                cmd.Parameters.AddWithValue("@StudentId", productId);
                cmd.Parameters.AddWithValue("@Amount", quantitySold);
                cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Satış başarıyla gerçekleştirildi!");
                }
                else
                {
                    MessageBox.Show("Yeterli stok yok!");
                }
            }

            LoadStockData(UserId);
        }

        // Gelir-Gider Yönetimi

        private string GetCompanyName(Guid userId)
        {
            string companyName = "Bilinmiyor"; // Varsayılan değer
            string query = @"SELECT ad = (SELECT ce.CompanyName 
                                          FROM Companies ce 
                                          WHERE ce.CompanyId = c.CompanyId) 
                            FROM CompanyUsers c
                           
                            WHERE c.UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            companyName = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bağlantı hatası: " + ex.Message);
                }
            }

            return companyName;
        }
        private void excelAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                if (dataGridViewStok.Rows.Count == 0 || dataGridViewStok.Rows.Count > 0) // DataGridView'de veri var mı kontrol et
                {
                    // Excel dosyası seçme penceresi açılıyor
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Excel Dosyaları|*.xls;*.xlsx";
                    openFileDialog.Title = "Bir Excel Dosyası Seçin";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string dosyaYolu = openFileDialog.FileName; // Seçilen dosyanın yolu

                        // EPPlus ile Excel dosyasını aç
                        FileInfo fi = new FileInfo(dosyaYolu);
                        using (var package = new ExcelPackage(fi))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // İlk sayfayı al

                            int row = 2; // 1. satır başlık olduğu için 2. satırdan başlayalım
                            int col = 1;

                            // Veritabanı bağlantı dizesi
                            using (SqlConnection conn = new SqlConnection(connectionString))
                            {
                                conn.Open(); // Veritabanına bağlantıyı aç

                                if (worksheet.Cells[row, 1].Text == "")
                                {
                                    MessageBox.Show("Excel veri aktarılamadı", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;

                                }
                                // Excel verilerini veritabanına aktar
                                while (worksheet.Cells[row, 1].Text != "") // 1. kolon boş değilse, veri var demektir
                                {
                                    // Excel'den veri çekme
                                    string ogrenciName = worksheet.Cells[row, 1].Text;
                                    string ogrenciSurname = worksheet.Cells[row, 2].Text;
                                    string Fathername = worksheet.Cells[row, 3].Text;
                                    DateTime dateTime = DateTime.Parse(worksheet.Cells[row, 4].Text); // Tarih verisini dönüştürme
                                    string studentcode = worksheet.Cells[row, 5].Text;
                                    bool odemedurum = bool.Parse(worksheet.Cells[row, 6].Text);
                                    decimal odenentutar = decimal.Parse(worksheet.Cells[row, 7].Text);
                                    bool aktiflik = bool.Parse(worksheet.Cells[row, 8].Text); // Aktivite durumu
                                    string classing = worksheet.Cells[row, 9].Text;
                                    string MotherName = worksheet.Cells[row, 10].Text;
                                    string FatherAddress = worksheet.Cells[row, 11].Text;
                                    string MotherAddress = worksheet.Cells[row, 12].Text;
                                    string FatherPhoneNumber = worksheet.Cells[row, 13].Text;
                                    string ogrenciDetails = worksheet.Cells[row, 14].Text;
                                    string MotherPhoneNumber = worksheet.Cells[row, 15].Text;
                                    bool IsMarried = string.IsNullOrEmpty(worksheet.Cells[row, 16].Text) ? true : bool.Parse(worksheet.Cells[row, 16].Text);
                                    // Burada kullanıcı id'sini doğru şekilde ayarlayın

                                    // SQL sorgusu
                                    string sqlQuery = "INSERT INTO Aysstudents (Id, Name, Surname, FatherName, BirthDate, StudentCode, ClassId, PaymentStatus, MonthlyFee, IsActive, FatherAddress, MotherAddress, FatherPhoneNumber, MotherPhoneNumber, IsMarried, StudentsDetails, MotherName, SchoolId) " +
                                                      "VALUES (@Id, @Name, @Surname, @FatherName, @BirthDate, @StudentCode, (SELECT top 1 Id FROM AYSClasses WHERE ClassName = @ClassName), @PaymentStatus, @MonthlyFee, @IsActive, @FatherAddress, @MotherAddress, @FatherPhoneNumber, @MotherPhoneNumber, @IsMarried, @StudentsDetails, @MotherName, (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId))";

                                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                                    {
                                        // Parametreleri ekle
                                        cmd.Parameters.AddWithValue("@Id", Guid.NewGuid()); // Örnek olarak yeni bir GUID oluşturuluyor
                                        cmd.Parameters.AddWithValue("@Name", ogrenciName);
                                        cmd.Parameters.AddWithValue("@UserId", UserId);
                                        cmd.Parameters.AddWithValue("@Surname", ogrenciSurname);
                                        cmd.Parameters.AddWithValue("@FatherName", Fathername);
                                        cmd.Parameters.AddWithValue("@BirthDate", dateTime); // Tarih formatı
                                        cmd.Parameters.AddWithValue("@StudentCode", studentcode);
                                        cmd.Parameters.AddWithValue("@PaymentStatus", odemedurum);
                                        cmd.Parameters.AddWithValue("@ClassName", classing);
                                        cmd.Parameters.AddWithValue("@MonthlyFee", odenentutar);
                                        cmd.Parameters.AddWithValue("@IsActive", aktiflik);
                                        cmd.Parameters.AddWithValue("@MotherName", MotherName);
                                        cmd.Parameters.AddWithValue("@FatherAddress", FatherAddress);
                                        cmd.Parameters.AddWithValue("@MotherAddress", MotherAddress);
                                        cmd.Parameters.AddWithValue("@FatherPhoneNumber", FatherPhoneNumber);
                                        cmd.Parameters.AddWithValue("@StudentsDetails", ogrenciDetails);
                                        cmd.Parameters.AddWithValue("@MotherPhoneNumber", MotherPhoneNumber);
                                        cmd.Parameters.AddWithValue("@IsMarried", IsMarried);

                                        // SQL sorgusunu çalıştır
                                        cmd.ExecuteNonQuery();
                                    }

                                    row++; // Sonraki satıra geç
                                }

                                conn.Close(); // Veritabanı bağlantısını kapat
                            }

                            // Kullanıcıya başarı mesajı göster
                            MessageBox.Show("Excel verisi veritabanına aktarıldı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadStockData(UserId);
                        }
                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Excel verisi veritabanına aktarılamadı!!!", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAddIncomeExpense_Click(object sender, EventArgs e)
        {
            string description = txtDescription.Text;
            decimal amount = numericAmount.Value;
            string type = radioIncome.Checked ? "G" : "D";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO GelirGider (Aciklama, Miktar, Tip) VALUES (@Aciklama, @Miktar, @Tip)", conn);
                cmd.Parameters.AddWithValue("@Aciklama", description);
                cmd.Parameters.AddWithValue("@Miktar", amount);
                cmd.Parameters.AddWithValue("@Tip", type);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Gelir/Gider kaydı başarıyla eklendi!");
            LoadPaymentData();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            LoadSalesData();
        }
        private void YasGrubuLoad()
        {
            cbxOgrenciYonetimiYasGrubu.Items.Add("3 YAŞ");
            cbxOgrenciYonetimiYasGrubu.Items.Add("4 YAŞ");
            cbxOgrenciYonetimiYasGrubu.Items.Add("5 YAŞ");
            cbxOgrenciYonetimiYasGrubu.Items.Add("6 YAŞ");
        }
        private void SinifAdd(Guid UserId)
        {

        }

        private void SinifLoad(Guid UserId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "select Id,Sınıf=ClassName,'Yaş Grubu'=[Group],'Öğretmen Adı'=OgretmenAdi from aysclasses where (OgretmenAdi is not null and OgretmenAdi !=' ' and ClassName !=' ' and ClassName is not null) and SchoolId=(select CompanyId from CompanyUsers where UserId=@UserId) and IsDeleted=0";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                DgvOgrenciYonetimiSiniflar.DataSource = dt;
            }
        }
        private object OdemeLoad(Guid UserId, Guid StudentId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "select p.Id, 'Ödeme Tutarı'=p.Amount, 'Ödeme Tarihi'=p.PaymentDate,p.IsApproved, 'Ödendi Mi?'=case when p.IsApproved=1 then 'Ödendi'else 'Ödenecek'end, 'Ödenen Tarihi'=p.ApprovedDate FROM AYSFeePayments p WHERE p.StudentId = @StudentId AND p.Isdeleted = 0 AND p.SchoolId = (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId) order by p.PaymentDate";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
        }
        private void salesGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Delete(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void ödemeDetaylarıToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewStok.CurrentRow.Cells.Count > 0)
            {


                Guid selectedStudentId = (Guid)dataGridViewStok.CurrentRow.Cells["Id"].Value;
                ShowPaymentDetails(selectedStudentId);


            }
            else
            {
                MessageBox.Show("Uyarı", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataOgrVw_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBabaAd_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateDogum_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void tabPageStok_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewStok_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnOgrenciYonetimiAra_Click(object sender, EventArgs e)
        {
            try
            {
                string ogrenciadi = string.IsNullOrEmpty(txtOgrenciYonetimiAra.Text) ? null : txtOgrenciYonetimiAra.Text;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "exec GetStudent @UserId,@Name";// arama modülü eklenecek
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@Name", string.IsNullOrEmpty(ogrenciadi) ? (object)DBNull.Value : ogrenciadi);
                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridViewStok.DataSource = dt;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Hatalı Veri Girişi", "Hata");
            }
        }

        private void dataGridViewStok_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOgrenciYonetimiSinifKaydet_Click(object sender, EventArgs e)
        {
            string sınıfadi = txtOgrenciYonetimiSınıfAdı.Text.Trim();
            string yasgrubu = cbxOgrenciYonetimiYasGrubu.Text;
            string ogretmen = cbxOgrenciYonetimiOgretmen.Text;
            if (string.IsNullOrEmpty(sınıfadi) || sınıfadi == "" || string.IsNullOrEmpty(yasgrubu) || string.IsNullOrEmpty(ogretmen))
            {
                MessageBox.Show("Sınıfı Boş Geçemezsiniz...");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("AddAysClass", conn);
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

                cmd.ExecuteNonQuery();

                int sonuc = (int)outputParam.Value;
                if (sonuc == 1)
                {
                    MessageBox.Show("Sınıf Başarıyla Eklendi.");
                }
                else
                {
                    MessageBox.Show("Bu Sınıf Zaten Mevcut.");
                }

                SinifLoad(UserId);
            }

        }
        private void loglarıGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem == null) return;

            ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
            if (owner == null) return;

            Control sourceControl = owner.SourceControl;
            if (sourceControl is DataGridView sourceDgv)
            {
                int modulCode = 0;

                // Örneğin, dgv'nin Tag özelliğine modulCode eklediysen:
                if (sourceDgv.Tag != null)
                {
                    modulCode = Convert.ToInt32(sourceDgv.Tag);
                }
                else
                {
                    // Alternatif olarak dgv'ye özel bir isimle kontrol edebilirsin
                    if (sourceDgv.Name == "dataGridViewStok")
                        modulCode = 4010;

                }

                // Şimdi SQL sorgunu bu modulCode’a göre filtreleyebilirsin
                Form logForm = new Form
                {

                    Width = 800,
                    Height = 600
                };
                if (modulCode == 4010)
                {
                    logForm.Text = "Öğrenci Yönetimi Log";
                }
                else if (modulCode == 4020)
                {
                    logForm.Text = "Personel Yönetimi Log";
                }
                else
                {
                    logForm.Text = "Loglar";
                }
                DataGridView dgv = new DataGridView { Dock = DockStyle.Fill };
                logForm.Controls.Add(dgv);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"
                SELECT * FROM deleteandlogs 
                WHERE SirketId = (SELECT TOP 1 CompanyId FROM CompanyUsers WHERE UserId = @UserId)
                AND ModulKod = @ModulCode
                ORDER BY SilinmeZamani DESC", conn);

                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@ModulCode", modulCode);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv.DataSource = dt;
                }

                logForm.Show();
            }
        }


        private void cbxUserId_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pbxPersonelPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fotoğraf |*.png;*.jpeg";
            openFileDialog.Title = "Bir Fotoğraf Seçin";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Photo = openFileDialog.FileName; // Seçilen dosyanın yolu
                pbxPersonelPicture.Image = System.Drawing.Image.FromFile(Photo);
                if (pbxPersonelPicture.Image != null)
                {
                    pbxPersonelPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                }

            }
        }

        private void tabPagePersonelYonetimi_Click(object sender, EventArgs e)
        {

        }
        private void PersonelYonetimiLoad(Guid UserId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "exec GetPersonel @UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                dgvPersonelYonetimi.DataSource = dt;
            }
        }

        private void cmbogrsınıf_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtPersonelMaas_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPersonelGorev_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void groupBox18_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridViewStok_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Satırı al
                DataGridViewRow row = dataGridViewStok.Rows[e.RowIndex];

                Form2 form2 = new Form2();
                OgrenciForm ogrForm = new OgrenciForm(form2);

                // Satırdaki verileri forma aktar
                ogrForm.txtOgrenciAd.Text = row.Cells["İsim"].Value?.ToString() ?? "";
                ogrForm.textSoyad.Text = row.Cells["Soyisim"].Value?.ToString() ?? "";
                ogrForm.txtBabaAd.Text = row.Cells["Baba Adı"].Value?.ToString() ?? "";
                ogrForm.txtAnneAd.Text = row.Cells["Anne Adı"].Value?.ToString() ?? "";
                ogrForm.cmbogrsınıf.Text = row.Cells["Sınıfı"].Value?.ToString() ?? "";
                ogrForm.textOgrenciKod.Text = row.Cells["Öğrenci Kodu"].Value?.ToString() ?? "";
                ogrForm.textOgrenciDetay.Text = row.Cells["Öğrenci Hakkında"].Value?.ToString() ?? "";
                ogrForm.txtBabaTel.Text = row.Cells["Baba Telefon"].Value?.ToString() ?? "";
                ogrForm.txtAnneTel.Text = row.Cells["Anne Telefon"].Value?.ToString() ?? "";
                ogrForm.txtBabaEvAdres.Text = row.Cells["Baba Adresi"].Value?.ToString() ?? "";
                ogrForm.txtAnneEvAdres.Text = row.Cells["Anne Adresi"].Value?.ToString() ?? "";
                var imageData = row.Cells["FotoId"].Value as byte[];
                if (imageData != null)
                {
                    using (var ms = new MemoryStream(imageData))
                    {
                        ogrForm.pictureBox1.Image = Image.FromStream(ms);
                        ogrForm.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                    ogrForm.pictureBox1.Image = null; // Veya varsayılan bir resim
                }
                // Sayısal değerlerde null kontrolü ve varsayılan değer atama
                ogrForm.numericPrice.Value = row.Cells["MonthlyFee"].Value != null
                    && decimal.TryParse(row.Cells["MonthlyFee"].Value.ToString(), out decimal price)
                    ? price : 0;

                ogrForm.checkOdemeDurum.Checked = row.Cells["Ödeme Durumu"].Value?.ToString() == "True";
                ogrForm.checkAktif.Checked = row.Cells["Aktif Öğrenci mi"].Value?.ToString() == "Evet";
                ogrForm.checkEvet.Checked = row.Cells["Aile Ayrı Mı"].Value?.ToString() == "Evet";

                // Tarih değerlerinde kontrol
                if (row.Cells["Doğum Tarihi"].Value != null &&
                   DateTime.TryParse(row.Cells["Doğum Tarihi"].Value.ToString(), out DateTime birthDate) &&
                   birthDate >= ogrForm.dateDogum.MinDate)
                {
                    ogrForm.dateDogum.Value = birthDate;
                }
                else
                {
                    ogrForm.dateDogum.Value = DateTime.Now; // Geçersiz tarih varsa bugünün tarihi atanır
                }
                ogrForm.UserId = UserId;
                ogrForm.StudentId = (Guid)dataGridViewStok.CurrentRow.Cells["Id"].Value;
                ogrForm.RefreshData += DataStokRefresh;
                ogrForm.Show();

            }
        }

        private void btnOgrenciYonetimiSinifGuncelle_Click(object sender, EventArgs e)
        {
            string ClassId = (string)DgvOgrenciYonetimiSiniflar.CurrentRow.Cells["Sınıf"].Value;
            string SinifAdi = txtOgrenciYonetimiSınıfAdı.Text;
            string YasGrubu = cbxOgrenciYonetimiYasGrubu.Text;
            string OgretmenAdi = cbxOgrenciYonetimiOgretmen.Text;
            string query = @"
                                   UPDATE AYSclasses SET 
    ClassName = @SinifAdi,
    [Group] = @YasGrubu,
    OgretmenAdi = @OgretmenAdi,
     OgretmenId = (
            SELECT TOP 1 PersonelId 
            FROM personel 
            WHERE FirstName + ' ' + LastName = @OgretmenAdi 
              AND CompanyId = (select CompanyId from companyusers where UserId = @UserId)
        )
WHERE Id = (select top 1 Id from aysclasses where ClassName=@SinifAdiP and IsDeleted=0)
";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Parametreleri Ekle
                    cmd.Parameters.AddWithValue("@SinifAdiP", ClassId);
                    cmd.Parameters.AddWithValue("@SinifAdi", SinifAdi);
                    cmd.Parameters.AddWithValue("@YasGrubu", YasGrubu);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@OgretmenAdi", OgretmenAdi);

                    cmd.ExecuteNonQuery();
                }
                SinifLoad(UserId);

            }

        }


        private void DataStokRefresh(object sender, EventArgs e)
        {
            string ogrenciadi = string.IsNullOrEmpty(txtOgrenciYonetimiAra.Text) ? null : txtOgrenciYonetimiAra.Text;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "exec GetStudent @UserId,@Name";// arama modülü eklenecek
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Name", string.IsNullOrEmpty(ogrenciadi) ? (object)DBNull.Value : ogrenciadi);

                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                dataGridViewStok.DataSource = dt;


            }
        }
        private void yeniKayitEkle(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            OgrenciForm ogrForm = new OgrenciForm(form2);
            ogrForm.UserId = UserId;
            ogrForm.RefreshData += DataStokRefresh;
            ogrForm.Show();
        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ogrenciadi = string.IsNullOrEmpty(txtOgrenciYonetimiAra.Text) ? null : txtOgrenciYonetimiAra.Text;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "exec GetStudent @UserId,@Name";// arama modülü eklenecek
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Name", string.IsNullOrEmpty(ogrenciadi) ? (object)DBNull.Value : ogrenciadi);

                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                dataGridViewStok.DataSource = dt;

            }
        }
        private void DeleteStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dataGridViewStok.CurrentRow != null)
            {


                Guid id = (Guid)dataGridViewStok.CurrentRow.Cells["Id"].Value; // Seçili öğrencinin ID'sini al
                DialogResult result = MessageBox.Show("Bu Öğrenciyi Silmek İstiyor musunuz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {

                    string query = @"
            Update AysStudents set IsDeleted=1
            WHERE Id = @id";

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            // Parametreleri Ekle
                            cmd.Parameters.AddWithValue("@id", id);

                            cmd.ExecuteNonQuery(); // SQL sorgusunu çalıştır
                        }
                    }

                    MessageBox.Show("Öğrenci Silindi Eski Kayıtlar için Loglara Bak..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DeleteAndLog("Aysstudents", "Id", id, UserId, "1", "DELETE");
                    LoadStockData(UserId);

                }
            }
        }



        private void btnOgrenciYonetimiSinifSil_Click(object sender, EventArgs e)
        {
            if (DgvOgrenciYonetimiSiniflar.CurrentRow == null)
            {
                MessageBox.Show("Lütfen önce silinecek sınıfı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = DgvOgrenciYonetimiSiniflar.CurrentRow;

            var cellValue = selectedRow.Cells["Id"].Value;
            if (cellValue != null)
                sinifid = Guid.Parse(cellValue.ToString());

            if (sinifid == Guid.Empty)
            {
                MessageBox.Show("Geçersiz sınıf bilgisi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"
Update AYSClasses set IsDeleted=1
WHERE Id = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", sinifid);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Sınıf Silindi. Eski kayıtlar için loglara bakın.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DeleteAndLog("AYSClasses", "Id", sinifid, UserId, "1", "DELETE");
            SinifLoad(UserId);
        }

        private void btnPersonelKaydet_Click(object sender, EventArgs e)
        {
            // Boş alan kontrolleri (örnek: ad soyad zorunlu olsun)
            if (string.IsNullOrWhiteSpace(txtPersonelAd.Text) || string.IsNullOrWhiteSpace(txtPersonelSoyad.Text))
            {
                MessageBox.Show("Lütfen Ad ve Soyad alanlarını doldurunuz.");
                return;
            }

            // Değerleri alma
            Guid PersonelId = Guid.NewGuid();
            Guid userid = UserId;
            string Adi = txtPersonelAd.Text.Trim();
            string Soyadi = txtPersonelSoyad.Text.Trim();
            DateTime DogumTarihi = dtpPersonelDG.Value;
            string Uyruk = cbxPersonelUyruk.Text.Trim();
            string Kimlik = txtPersonelKimlik.Text.Trim();
            string Cinsiyet = rbtPersonelErkek.Checked ? "Erkek" : rbtPersonelKadin.Checked ? "Kadın" : "";
            bool MedeniDurum = rbtPersonelEvli.Checked ? true : rbtPersonelBekar.Checked ? false :false;
            string Telefon = txtPersonelTel.Text.Trim();
            string Mail = txtPersonelMail.Text.Trim();
            string AcilDurumIletisim = txtPersonelIletişimAcilDurum.Text.Trim();
            string Adres = txtPersonelAdres.Text.Trim();
            DateTime IseBaslamaTarihi = dtpPersonelIseBaslamaTarihi.Value;
            string Departman = txtPersonelDepartman.Text.Trim();
            string Gorev = txtPersonelGorev.Text.Trim();
            string CalismaSekli = cbxPersonelCalismaSekli.Text.Trim();
            string PersonelNo = txtPersonelPersonelNo.Text.Trim();
            string SigortaDurumu = cbxPersonelSigorta.Text.Trim();
            decimal Maas = decimal.TryParse(txtPersonelMaas.Text.Trim(), out decimal maasVal) ? maasVal : 0;
            decimal PrimVeEk = decimal.TryParse(txtPersonelPrimVeEk.Text.Trim(), out decimal primVal) ? primVal : 0;
            decimal YemekYol = decimal.TryParse(txtPersonelYemekYol.Text.Trim(), out decimal yemekVal) ? yemekVal : 0;
            string SGKSicilNumarasi = txtPersonelSGKSicilNum.Text.Trim();
            string SaglikSigortasi = txtPersonelSaglikSigorta.Text.Trim();
            string Emeklilik = txtPersonelEmeklilik.Text.Trim();
            string EgitimDurumu = cbxPersonelEgitimDurumu.Text.Trim();
            string Universite = cbxPersonelUniversite.Text.Trim();
            string UniBolum = txtPersonelUniBolum.Text.Trim();
            string Sertifika = txtPersonelSertifika.Text.Trim();
            string YabanciDil = txtPersonelYabanciDil.Text.Trim();
            bool IsAyrildi = cbxPersoneIIsAyrıldı.Checked;
            DateTime? CikisTarihi = IsAyrildi ? dtpPersonelCıkısTarihi.Value : (DateTime?)null;
            string AyrilmaNedeni = txtPersonelAyrilmaNedeni.Text.Trim();
            decimal KidemTazminat = decimal.TryParse(txtPersonelKidemTazminat.Text.Trim(), out decimal kidemVal) ? kidemVal : 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("AddPersonel", conn); // prosedür adını kendinize göre değiştirin
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", PersonelId);
                cmd.Parameters.AddWithValue("@userid", UserId);
                cmd.Parameters.AddWithValue("@FirstName", Adi);
                cmd.Parameters.AddWithValue("@LastName", Soyadi);
                cmd.Parameters.AddWithValue("@Email", Mail);
                cmd.Parameters.AddWithValue("@Phone", Telefon);
                cmd.Parameters.AddWithValue("@Address", Adres);
                cmd.Parameters.AddWithValue("@City", ""); // Şehir yoksa boş gönder
                cmd.Parameters.AddWithValue("@Country", ""); // Ülke yoksa boş gönder
                cmd.Parameters.AddWithValue("@Birthdate", DogumTarihi);
                cmd.Parameters.AddWithValue("@Gender", Cinsiyet);
                cmd.Parameters.AddWithValue("@IdentityNumber", Kimlik);
                cmd.Parameters.AddWithValue("@JobTitle", Gorev);
                cmd.Parameters.AddWithValue("@Department", Departman);
                cmd.Parameters.AddWithValue("@HireDate", IseBaslamaTarihi);
                cmd.Parameters.AddWithValue("@Salary", Maas);
                cmd.Parameters.AddWithValue("@IsActive", true); // Yeni personel aktif olur varsayımı
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@IsTeacher", false); // Öğretmen değilse false
              
                cmd.Parameters.AddWithValue("@IsMaried", MedeniDurum);
                cmd.Parameters.AddWithValue("@Education", EgitimDurumu);
                cmd.Parameters.AddWithValue("@IsQuitWork", IsAyrildi);
                cmd.Parameters.AddWithValue("@QuitWorkDate", (object?)CikisTarihi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@QuitWorkReason", AyrilmaNedeni);
                cmd.Parameters.AddWithValue("@Compensation", KidemTazminat);
                cmd.Parameters.AddWithValue("@AdditionalPayment", PrimVeEk);
                cmd.Parameters.AddWithValue("@FoodandTransportFee", YemekYol);
                cmd.Parameters.AddWithValue("@SgkSicil", SGKSicilNumarasi);
                cmd.Parameters.AddWithValue("@SaglikSigortasiBilgileri", SaglikSigortasi);
                cmd.Parameters.AddWithValue("@EmeklilikBilgileri", Emeklilik);
                cmd.Parameters.AddWithValue("@PersonelNumarasi", PersonelNo);
                cmd.Parameters.AddWithValue("@UniversityDepartment", UniBolum);
                cmd.Parameters.AddWithValue("@SertifikaAndEducation", Sertifika);
                cmd.Parameters.AddWithValue("@ForeignLanguage", YabanciDil);
                cmd.Parameters.AddWithValue("@EmergenyFamilies", AcilDurumIletisim);
                cmd.Parameters.AddWithValue("@Uyruk", Uyruk);
                SqlParameter photoParam = new SqlParameter("@photo", SqlDbType.VarBinary, -1);
                photoParam.Value = Photo != null ? (object)Photo : DBNull.Value;
                cmd.Parameters.Add(photoParam);



                cmd.ExecuteNonQuery();

                
                // Verileri yenileme / temizlik işlemi burada yapılabilir
            }
        }

        private void cbxPersoneIIsAyrıldı_CheckedChanged(object sender, EventArgs e)
        {
            bool ayrildi = cbxPersoneIIsAyrıldı.Checked;


            dtpPersonelCıkısTarihi.Enabled = ayrildi;
            txtPersonelAyrilmaNedeni.Enabled = ayrildi;
            txtPersonelKidemTazminat.Enabled = ayrildi;

        }

        private void cbxPersonelUyruk_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbxPersonelUyruk.SelectedItem != null && cbxPersonelUyruk.SelectedItem.ToString() == "T.C")
            {
                lblKimlikNum.Visible = true;
                txtPersonelKimlik.Enabled = true;
                lblKimlikNum.Text = "Kimlik Numarası :";

            }
            else
            {
                lblKimlikNum.Visible = true;
                txtPersonelKimlik.Enabled = true;
                lblKimlikNum.Text = "Pasaport Numarası :";

            }
        }

        private void txtPersonelKimlik_TextChanged(object sender, EventArgs e)
        {

        }

        public Guid sinifid { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public string Photo { get; set; }
    }

    // ComboBox için özel bir sınıf
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

