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
        
        public OgrenciForm()
        {
            InitializeComponent();
        }

        private void txtOgrenciAd_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

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
        
            form2.LoadStockData(UserId);
            form2.LoadStockComboBox();

        }
        public Guid UserId { get; set; }
    }
    public class OgrUser
    {
       
    }
}
