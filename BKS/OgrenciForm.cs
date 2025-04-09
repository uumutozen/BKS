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

        private void txtOgrenciAd_TextChanged(object sender, EventArgs e)
        {

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
                BirthDate = @dogumTarihi
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

                    cmd.ExecuteNonQuery(); // SQL sorgusunu çalıştır
                }
            }

            MessageBox.Show("Öğrenci bilgileri başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _form2.LoadStockData(UserId); // Güncellenmiş listeyi tekrar yükle
        }
        Form2 form2 = new Form2();

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
            using (SqlConnection conn = new SqlConnection(form2.connectionString))
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
            _form2.DeleteAndLog("Aysstudents","Id",StudentId,UserId);
            _form2.LoadStockData(UserId);
        }

        public Guid UserId { get; set; }
        public Guid StudentId { get; set; }
    }
    public class OgrUser
    {
       
    }
}
