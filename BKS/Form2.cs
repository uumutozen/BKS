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
        private void Form2_Load(object sender, EventArgs e)
        {

            this.Text = GetCompanyName(UserId) + " " + "Anaokulu Yönetim Sistemi";
            this.materialLabel3.Text = "Merhaba "+GetLastUser(UserId)+" Son Giriş Zamanın : "+GetLastLoginTime(UserId);
            LoadStockData(UserId);
            LoadCompanyModules(UserId);
            LoadStudentClassComboBox(UserId);

        }
        Form1 form1 = new Form1();

        private void LoadCompanyModules(Guid UserId)
        {
            List<string> activeModules = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "select m.ModuleName from CompanyUsers cu  join Companies c on c.CompanyId=cu.CompanyId join CompanyModules cm on cm.CompanyId=c.CompanyId join Modules m on m.ModuleId = cm.ModuleId where cu.UserId=@UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            activeModules.Add(reader["ModuleName"].ToString());
                        }
                    }
                }
            }


            SetTabAccess(activeModules);
        }

        // Stok Yönetimi
        private void LoadStockData(Guid UserId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "select Id=Id,'İsim'=Name,'Soyisim'=Surname,'Baba Adı'=FatherName,'Doğum Tarihi'=BirthDate,'Öğrenci Kodu'=StudentCode,'Ödeme Durumu'=PaymentStatus,'Ödenen Tutar'=MonthlyFee,'Aktif Öğrenci mi'=case when IsActive=1 then'Evet' else'Hayır' end  ,'Sınıfı'=ClassName,'Baba Adresi'=FatherAddress,'Anne Adresi'=MotherAddress,'Baba Telefon'=FatherPhoneNumber,'Anne Telefon'=MotherPhonenumber, 'Aile Ayrı Mı'=case when IsMarried=1 then'Evet' else'Hayır' end ,'Öğrenci Hakkında'=StudentsDetails,'Anne Adı'= MotherName from AYSstudents where SchoolId=(select CompanyId from CompanyUsers where UserId=@UserId) and Isnull(IsDeleted,0)=0";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                dataGridViewStok.DataSource = dt;
            }
        }
        private void dataGridViewStok_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewStok.Rows[e.RowIndex];

                txtOgrenciAd.Text = row.Cells["İsim"].Value?.ToString() ?? "";
                textSoyad.Text = row.Cells["Soyisim"].Value?.ToString() ?? "";
                txtBabaAd.Text = row.Cells["Baba Adı"].Value?.ToString() ?? "";
                txtAnneAd.Text = row.Cells["Anne Adı"].Value?.ToString() ?? "";
                cmbogrsınıf.Text = row.Cells["Sınıfı"].Value?.ToString() ?? "";
                textOgrenciKod.Text = row.Cells["Öğrenci Kodu"].Value?.ToString() ?? "";
                textOgrenciDetay.Text = row.Cells["Öğrenci Hakkında"].Value?.ToString() ?? "";
                txtBabaTel.Text = row.Cells["Baba Telefon"].Value?.ToString() ?? "";
                txtAnneTel.Text = row.Cells["Anne Telefon"].Value?.ToString() ?? "";
                txtBabaEvAdres.Text = row.Cells["Baba Adresi"].Value?.ToString() ?? "";
                txtAnneEvAdres.Text = row.Cells["Anne Adresi"].Value?.ToString() ?? "";

                // Sayısal değerlerde null kontrolü ve varsayılan değer atama
                numericPrice.Value = row.Cells["Ödenen Tutar"].Value != null
                    && decimal.TryParse(row.Cells["Ödenen Tutar"].Value.ToString(), out decimal price)
                    ? price : 0;

                checkOdemeDurum.Checked = row.Cells["Ödeme Durumu"].Value?.ToString() == "True";
                checkAktif.Checked = row.Cells["Aktif Öğrenci mi"].Value?.ToString() == "Evet";
                checkEvet.Checked = row.Cells["Aile Ayrı Mı"].Value?.ToString() == "Evet";

                // Tarih değerlerinde kontrol
                if (row.Cells["Doğum Tarihi"].Value != null &&
                   DateTime.TryParse(row.Cells["Doğum Tarihi"].Value.ToString(), out DateTime birthDate) &&
                   birthDate >= dateDogum.MinDate)
                {
                    dateDogum.Value = birthDate;
                }
                else
                {
                    dateDogum.Value = DateTime.Now; // Geçersiz tarih varsa bugünün tarihi atanır
                }

            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dataGridViewStok.CurrentRow != null)
            {
                Guid id = (Guid)dataGridViewStok.CurrentRow.Cells["Id"].Value; // Seçili öğrencinin ID'sini al

                // Formdan alınan değerler
                string isim = txtOgrenciAd.Text;
                string soyisim = textSoyad.Text;
                string babaAdi = txtBabaAd.Text;
                string anneAdi = txtAnneAd.Text;
                string sinif = cmbogrsınıf.Text;
                string ogrenciKod = textOgrenciKod.Text;
                string ogrenciDetay = textOgrenciDetay.Text;
                string babaTel = txtBabaTel.Text;
                string anneTel = txtAnneTel.Text;
                string babaAdres = txtBabaEvAdres.Text;
                string anneAdres = txtAnneEvAdres.Text;
                decimal fiyat = numericPrice.Value;
                bool odemeDurumu = checkOdemeDurum.Checked;
                bool aktifMi = checkAktif.Checked;
                bool aileAyrimi = checkEvet.Checked;
                DateTime dogumTarihi = dateDogum.Value;

                // SQL Güncelleme Sorgusu
                string query = @"
            UPDATE AYSstudents SET 
                Name = @isim,
                Surname = @soyisim,
                FatherName = @babaAdi,
                MotherName = @anneAdi,
                ClassName = @sinif,
                StudentCode = @ogrenciKod,
                StudentsDetails = @ogrenciDetay,
                FatherPhoneNumber = @babaTel,
                MotherPhonenumber = @anneTel,
                FatherAddress = @babaAdres,
                MotherAddress = @anneAdres,
                MonthlyFee = @fiyat,
                PaymentStatus = @odemeDurumu,
                IsActive = @aktifMi,
                IsMarried = @aileAyrimi,
                BirthDate = @dogumTarihi
            WHERE Id = @id";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Parametreleri Ekle
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@isim", isim);
                        cmd.Parameters.AddWithValue("@soyisim", soyisim);
                        cmd.Parameters.AddWithValue("@babaAdi", babaAdi);
                        cmd.Parameters.AddWithValue("@anneAdi", anneAdi);
                        cmd.Parameters.AddWithValue("@sinif", sinif);
                        cmd.Parameters.AddWithValue("@ogrenciKod", ogrenciKod);
                        cmd.Parameters.AddWithValue("@ogrenciDetay", ogrenciDetay);
                        cmd.Parameters.AddWithValue("@babaTel", babaTel);
                        cmd.Parameters.AddWithValue("@anneTel", anneTel);
                        cmd.Parameters.AddWithValue("@babaAdres", babaAdres);
                        cmd.Parameters.AddWithValue("@anneAdres", anneAdres);
                        cmd.Parameters.AddWithValue("@fiyat", fiyat);
                        cmd.Parameters.AddWithValue("@odemeDurumu", odemeDurumu);
                        cmd.Parameters.AddWithValue("@aktifMi", aktifMi);
                        cmd.Parameters.AddWithValue("@aileAyrimi", aileAyrimi);
                        cmd.Parameters.AddWithValue("@dogumTarihi", dogumTarihi);

                        cmd.ExecuteNonQuery(); // SQL sorgusunu çalıştır
                    }
                }

                MessageBox.Show("Öğrenci bilgileri başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadStockData(UserId); // Güncellenmiş listeyi tekrar yükle
            }
        }
        public string GetLastLoginTime(Guid UserId)
        {
            string lastLoginTime = "Bilinmiyor";
            string query = "SELECT SonLogin FROM CompanyUsers WHERE UserId=@UserId";

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
                LoadStockData(UserId);
            }
        }

        private void SetTabAccess(List<string> activeModules)
        {
            foreach (TabPage tab in tabControl.TabPages)
            {
                if (activeModules.Contains(tab.Name))
                {
                    tab.Enabled = true;
                    // Aktif modüller
                }
                else
                {
                    tab.Enabled = false;
                    HideTabPage(tab);
                }
            }
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

        private void LoadStockComboBox()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(" select Id=Id,'İsim'=Name,'Soyisim'=Surname,'Baba Adı'=FatherName,'Doğum Tarihi'=BirthDate,\r\n                   'Öğrenci Kodu'=StudentCode,'Ödeme Durumu'=PaymentStatus,'Ödenen Tutar'=MonthlyFee,'Aktif Öğrenci mi'=case when IsActive=1 then'Evet' else'Hayır' end \r\n                   ,'Sınıfı'=ClassName,'Baba Adresi'=FatherAddress,'Anne Adresi'=MotherAddress,'Baba Telefon'=FatherPhoneNumber,'Anne Telefon'=MotherPhonenumber, 'Aile Ayrı Mı'=case when IsMarried=1 then'Evet' else'Hayır' end ,'Öğrenci Hakkında'=StudentsDetails,'Anne Adı'= MotherName from AYSstudents ", conn);
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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                
                conn.Open();
                SqlCommand cmd = new SqlCommand("select ClassName,[Group] from AYSClasses where SchoolId=(Select CompanyId from CompanyUsers where UserId=@UserId)", conn);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader reader = cmd.ExecuteReader();
                cmbogrsınıf.Items.Clear();
                while (reader.Read())
                {
                    cmbogrsınıf.Items.Add(new ComboBoxItem
                    {
                        Text = reader["ClassName"].ToString(),
                        Value = reader["Group"].ToString()
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
            if (e.Button == MouseButtons.Right)
            {
                // Sağ tıklama yapılan konumu tespit et
                var hitTestInfo = dataGridViewStok.HitTest(e.X, e.Y);
                if (hitTestInfo.RowIndex >= 0)
                {
                    // İlgili satırı seç
                    dataGridViewStok.ClearSelection();
                    dataGridViewStok.Rows[hitTestInfo.RowIndex].Selected = true;

                    // Menü konumunu ayarla ve göster
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
                    string query = "SELECT p.Amount, p.PaymentDate FROM AYSFeePayments p WHERE p.StudentId = @StudentId and p.Isdeleted=0 and p.SchoolId =(Select CompanyId from CompanyUsers where UserId =@UserId)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    DataTable paymentDetails = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(paymentDetails);

                    // Yeni bir form aç ve detayları göster
                    Form paymentForm = new Form
                    {
                        Text = "Ödeme Detayları",
                        Size = new Size(800, 600)
                    };

                    // Ödeme detaylarını göstermek için DataGridView
                    DataGridView paymentGrid = new DataGridView
                    {
                        DataSource = paymentDetails,
                        Dock = DockStyle.Fill,
                        Height = 400
                    };

                    // Yeni ödeme ekleme paneli
                    Panel addPaymentPanel = new Panel
                    {
                        Dock = DockStyle.Bottom,
                        Height = 100
                    };

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
                            // Ödeme ekleme işlemi
                            if (decimal.TryParse(txtAmount.Text, out decimal amount))
                            {
                                string insertQuery = "INSERT INTO AYSFeePayments (Id, StudentId, Amount, PaymentDate,SchoolId) VALUES (@Id, @StudentId, @Amount, @PaymentDate,(Select CompanyId from CompanyUsers WHERE UserId=@UserId ))";
                                using (SqlCommand insertCmd = new SqlCommand(insertQuery, connn))
                                {


                                    insertCmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
                                    insertCmd.Parameters.AddWithValue("@StudentId", studentId);
                                    insertCmd.Parameters.AddWithValue("@Amount", amount);
                                    insertCmd.Parameters.AddWithValue("@PaymentDate", dtpDate.Value);
                                    insertCmd.Parameters.AddWithValue("@UserId", UserId);
                                    insertCmd.ExecuteNonQuery();

                                    // Grid'i güncelle


                                    MessageBox.Show("Ödeme başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);



                                }

                            }

                            else
                            {
                                MessageBox.Show("Lütfen geçerli bir tutar giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                        }

                    };

                    // FlowLayoutPanel ile düzen
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

                    // Form kontrollerini ekle
                    paymentForm.Controls.Add(paymentGrid);
                    paymentForm.Controls.Add(addPaymentPanel);
                    paymentForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            string ogrenciName = txtOgrenciAd.Text;
            string ogrenciSurname = textSoyad.Text;
            string Fathername = txtBabaAd.Text;
            string MotherName = txtAnneAd.Text;
            string classing = cmbogrsınıf.Text;
            string studentcode = textOgrenciKod.Text;
            string ogrenciDetails = textOgrenciDetay.Text;
            string FatherPhoneNumber = txtBabaTel.Text;
            string MotherPhoneNumber = txtAnneTel.Text;
            string FatherAddress = txtBabaEvAdres.Text;
            string MotherAddress = txtAnneEvAdres.Text;
            decimal odenentutar = numericPrice.Value;
            bool IsMarried = checkEvet.Checked == true ? checkEvet.Checked : false;
            bool odemedurum = checkOdemeDurum.Checked;
            bool aktiflik = checkAktif.Checked;
            DateTime dateTime = dateDogum.Value;
            if (ogrenciName == null || ogrenciName == "" || ogrenciSurname == null || ogrenciSurname == "" || classing == null || classing == "")
            {
                MessageBox.Show("Sütunları boş bırakamazsınız...", "HATA", MessageBoxButtons.OK);
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Aysstudents (Id, Name, Surname, FatherName, BirthDate, StudentCode, ClassId, PaymentStatus, MonthlyFee, IsActive, ClassName, FatherAddress, MotherAddress, FatherPhoneNumber, MotherPhoneNumber, IsMarried, StudentsDetails, MotherName,SchoolId)VALUES (@Id, @Name, @Surname, @FatherName, @BirthDate, @StudentCode, (select Id from AYSClasses where ClassName =@ClassName), @PaymentStatus, @MonthlyFee, @IsActive, @ClassName, @FatherAddress, @MotherAddress, @FatherPhoneNumber, @MotherPhoneNumber, @IsMarried, @StudentsDetails, @MotherName,(select CompanyId from CompanyUsers where UserId=@UserId) )", conn);

                cmd.Parameters.AddWithValue("@Id", Guid.NewGuid()); // Örnek olarak yeni bir GUID oluşturuluyor
                cmd.Parameters.AddWithValue("@Name", ogrenciName);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Surname", ogrenciSurname);
                cmd.Parameters.AddWithValue("@FatherName", Fathername);
                cmd.Parameters.AddWithValue("@BirthDate", dateTime); // Tarih formatı
                cmd.Parameters.AddWithValue("@StudentCode", studentcode);
                cmd.Parameters.AddWithValue("@PaymentStatus", odemedurum);
                cmd.Parameters.AddWithValue("@MonthlyFee", odenentutar);
                cmd.Parameters.AddWithValue("@IsActive", aktiflik);
                cmd.Parameters.AddWithValue("@ClassName", classing);
                cmd.Parameters.AddWithValue("@MotherName", MotherName);
                cmd.Parameters.AddWithValue("@FatherAddress", FatherAddress);
                cmd.Parameters.AddWithValue("@MotherAddress", MotherAddress);
                cmd.Parameters.AddWithValue("@FatherPhoneNumber", FatherPhoneNumber);
                cmd.Parameters.AddWithValue("@StudentsDetails", ogrenciDetails);
                cmd.Parameters.AddWithValue("@MotherPhoneNumber", MotherPhoneNumber);
                cmd.Parameters.AddWithValue("@IsMarried", IsMarried);



                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Öğrenci Başarıyla Eklendi...");
            LoadStockData(UserId);
            LoadStockComboBox();
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
                SqlCommand cmd = new SqlCommand("insert into AYSFeePayments (Id,PaymentDate,Amount,StudentId) values (NEWID(),GETDATE(),@Amount,@StudentId)", conn);
                cmd.Parameters.AddWithValue("@Amount", quantitySold);
                cmd.Parameters.AddWithValue("@StudentId", productId);
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

        private void salesGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void ödemeDetaylarıToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewStok.SelectedRows.Count > 0)
            {
                Guid selectedStudentId = (Guid)dataGridViewStok.SelectedRows[0].Cells["Id"].Value;
                ShowPaymentDetails(selectedStudentId);
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

    

        public Guid UserId { get; set; }
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

