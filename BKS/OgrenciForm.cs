using MaterialSkin.Controls;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BKS
{
    public partial class OgrenciForm : MaterialForm
    {
        private Form2 _form2;
        public OgrenciForm(Form2 form2)
        {
            _form2 = form2;
            InitializeComponent();
        }
        private void OgrenciForm_Load(object sender, EventArgs e)
        {
            LoadStudentClassComboBox(UserId);
        }
        private void txtOgrenciAd_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadStudentClassComboBox(Guid UserId)
        {
           
            using (SqlConnection conn = new SqlConnection(_form2.connectionString))
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
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
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
                BirthDate = @dogumTarihi,
                photobinary=@Photo
            WHERE Id = @id";

            using (SqlConnection con = new SqlConnection(_form2.connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Parametreleri Ekle
                    cmd.Parameters.AddWithValue("@id", StudentId);
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
                    cmd.Parameters.AddWithValue("@Photo", Photo);
                    cmd.ExecuteNonQuery(); // SQL sorgusunu çalıştır
                }
            }

            MessageBox.Show("Öğrenci bilgileri başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _form2.DeleteAndLog("Aysstudents", "Id", StudentId, UserId, "1", "UPDATE");
            _form2.LoadStockData(UserId); // Güncellenmiş listeyi tekrar yükle
        }
        Form2 form2 = new Form2();

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan veriler:
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
            bool IsMarried = checkEvet.Checked;
            bool odemedurum = checkOdemeDurum.Checked;
            bool aktiflik = checkAktif.Checked;
            DateTime dateTime = dateDogum.Value;
            Guid StudentIdGuid = Guid.NewGuid();

            // Zorunlu alan kontrolü:
            if (string.IsNullOrEmpty(ogrenciName) ||
                string.IsNullOrEmpty(ogrenciSurname) ||
                string.IsNullOrEmpty(classing))
            {
                MessageBox.Show("Sütunları boş bırakamazsınız...", "HATA", MessageBoxButtons.OK);
                return;
            }

            // Veritabanı bağlantısı ve INSERT sorgusu:
            using (SqlConnection conn = new SqlConnection(form2.connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Aysstudents (
                Id, Name, Surname, FatherName, BirthDate, StudentCode, 
                ClassId, PaymentStatus, MonthlyFee, IsActive, ClassName, 
                FatherAddress, MotherAddress, FatherPhoneNumber, MotherPhoneNumber, 
                IsMarried, StudentsDetails, MotherName, SchoolId, photobinary
            )
            VALUES (
                @Id, @Name, @Surname, @FatherName, @BirthDate, @StudentCode, 
                (SELECT Id FROM AYSClasses WHERE ClassName = @ClassName), 
                @PaymentStatus, @MonthlyFee, @IsActive, @ClassName, 
                @FatherAddress, @MotherAddress, @FatherPhoneNumber, @MotherPhoneNumber, 
                @IsMarried, @StudentsDetails, @MotherName, 
                (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId),
                @Photo
            )", conn);

                // Parametrelerin eklenmesi:
                cmd.Parameters.AddWithValue("@Id", StudentIdGuid);
                cmd.Parameters.AddWithValue("@Name", ogrenciName);
                cmd.Parameters.AddWithValue("@UserId", UserId); // UserId'nin tanımlı olduğunu varsayıyoruz
                cmd.Parameters.AddWithValue("@Surname", ogrenciSurname);
                cmd.Parameters.AddWithValue("@FatherName", Fathername);
                cmd.Parameters.AddWithValue("@BirthDate", dateTime);
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

                // Photo parametresi: Eğer Photo null ise DBNull.Value gönder, değilse byte[] verisini.
                SqlParameter photoParam = new SqlParameter("@Photo", SqlDbType.VarBinary, -1);
                photoParam.Value = Photo != null ? (object)Photo : DBNull.Value;
                cmd.Parameters.Add(photoParam);

                // Sorguyu çalıştır:
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Öğrenci Başarıyla Eklendi...");
            _form2.DeleteAndLog("Aysstudents", "Id", StudentIdGuid, UserId, "1", "INSERT");
            _form2.LoadStockData(UserId);
            _form2.LoadStockComboBox();
        }

        private void btnOgrenciYonetimiSil_Click(object sender, EventArgs e)
        {
            string query = @"
            Update AysStudents set IsDeleted=1
            WHERE Id = @id";

            using (SqlConnection con = new SqlConnection(_form2.connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Parametreleri Ekle
                    cmd.Parameters.AddWithValue("@id", StudentId);

                    cmd.ExecuteNonQuery(); // SQL sorgusunu çalıştır
                }
            }

            MessageBox.Show("Öğrenci Silindi Eski Kayıtlar için Loglara Bak..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _form2.DeleteAndLog("Aysstudents", "Id", StudentId, UserId, "1", "DELETE");
            _form2.LoadStockData(UserId);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fotoğraf |*.png;*.jpeg";
            openFileDialog.Title = "Bir Fotoğraf Seçin";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);

                if (pictureBox1.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                        Photo = ms.ToArray();
                    }
                }
            }
        }



        public byte[] Photo { get; set; } 
        public Guid UserId { get; set; }
        public Guid StudentId { get; set; }
    }
    public class OgrUser
    {
       
    }
}
