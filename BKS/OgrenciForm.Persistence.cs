using System.Data;
using System.Data.SqlClient;

namespace BKS;

public partial class OgrenciForm
{
    private void btnGuncelle_Click(object sender, EventArgs e)
    {
        if (StudentId == Guid.Empty) return;
        if (string.IsNullOrWhiteSpace(txtOgrenciAd.Text) || string.IsNullOrWhiteSpace(textSoyad.Text) || string.IsNullOrWhiteSpace(cmbogrsınıf.Text))
        {
            MessageBox.Show("Öğrenci adı, soyadı ve sınıf zorunludur.");
            return;
        }
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
                StudentCode = @ogrenciKod,
                StudentsDetails = @ogrenciDetay,
                FatherPhoneNumber = @babaTel,
	            ClassId=(SELECT top 1 Id FROM AYSClasses WHERE ClassName = @ClassName and Isdeleted=0 and SchoolId=dbo.GetSirketIdByUserId(@UserId)),
                MotherPhonenumber = @anneTel,
                FatherAddress = @babaAdres,
                MotherAddress = @anneAdres,
                MonthlyFee = @fiyat,
                PaymentStatus = @odemeDurumu,
                IsActive = @aktifMi,
                IsMarried = @aileAyrimi,
                BirthDate = @dogumTarihi,
                photobinary=@Photo
            WHERE Id = @id AND SchoolId=dbo.GetSirketIdByUserId(@UserId)";
        using (SqlConnection con = new SqlConnection(_form2.connectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // Parametreleri Ekle
                cmd.Parameters.AddWithValue("@id", StudentId);
                if (!cmd.Parameters.Contains("@UserId")) cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@isim", isim);
                cmd.Parameters.AddWithValue("@soyisim", soyisim);
                cmd.Parameters.AddWithValue("@babaAdi", babaAdi);
                cmd.Parameters.AddWithValue("@anneAdi", anneAdi);
                if (!cmd.Parameters.Contains("@UserId")) cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ogrenciKod", ogrenciKod);
                cmd.Parameters.AddWithValue("@ogrenciDetay", ogrenciDetay);
                cmd.Parameters.AddWithValue("@ClassName", sinif);
                cmd.Parameters.AddWithValue("@babaTel", babaTel);
                cmd.Parameters.AddWithValue("@anneTel", anneTel);
                cmd.Parameters.AddWithValue("@babaAdres", babaAdres);
                cmd.Parameters.AddWithValue("@anneAdres", anneAdres);
                cmd.Parameters.AddWithValue("@fiyat", fiyat);
                cmd.Parameters.AddWithValue("@odemeDurumu", odemeDurumu);
                cmd.Parameters.AddWithValue("@aktifMi", aktifMi);
                cmd.Parameters.AddWithValue("@aileAyrimi", aileAyrimi);
                cmd.Parameters.AddWithValue("@dogumTarihi", dogumTarihi);
                SqlParameter photoParam = new SqlParameter("@Photo", SqlDbType.VarBinary, - 1);
                photoParam.Value = Photo != null ? (object) Photo: DBNull.Value;
                cmd.Parameters.Add(photoParam);
                if (cmd.ExecuteNonQuery() == 0) throw new InvalidOperationException("Öğrenci kaydı bulunamadı veya erişiminiz yok.");
                _edits.AcceptChanges();
            }
        }
        MessageBox.Show("Öğrenci bilgileri başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        _form2.DeleteAndLog("Aysstudents", "Id", StudentId, UserId, "2", "UPDATE");
        RefreshData?.Invoke(this, new EventArgs());
        _form2.dataGridViewStok.DataSource = _form2.LoadStockDataRefresh(UserId);
        _edits.AcceptChanges();
        this.Close();
        // Güncellenmiş listeyi tekrar yükle
    }

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
        using (SqlConnection conn = new SqlConnection(_form2.connectionString))
        {
            conn.Open();
            using var transaction = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Aysstudents (
                Id, Name, Surname, FatherName, BirthDate, StudentCode, 
                ClassId, PaymentStatus, MonthlyFee, IsActive, 
                FatherAddress, MotherAddress, FatherPhoneNumber, MotherPhoneNumber, 
                IsMarried, StudentsDetails, MotherName, SchoolId, photobinary
            )
            VALUES (
                @Id, @Name, @Surname, @FatherName, @BirthDate, @StudentCode, 
                (SELECT top 1 Id FROM AYSClasses WHERE ClassName = @ClassName and Isdeleted=0 and SchoolId=dbo.GetSirketIdByUserId(@UserId)), 
                @PaymentStatus, @MonthlyFee, @IsActive, 
                @FatherAddress, @MotherAddress, @FatherPhoneNumber, @MotherPhoneNumber, 
                @IsMarried, @StudentsDetails, @MotherName, 
                (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId),
                @Photo
            )",
            conn);
            cmd.Transaction = transaction;
            cmd.Parameters.AddWithValue("@Id", StudentIdGuid);
            cmd.Parameters.AddWithValue("@Name", ogrenciName);
            if (!cmd.Parameters.Contains("@UserId")) cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@Surname", ogrenciSurname);
            cmd.Parameters.AddWithValue("@FatherName", Fathername);
            cmd.Parameters.AddWithValue("@BirthDate", dateTime);
            cmd.Parameters.AddWithValue("@StudentCode", studentcode);
            cmd.Parameters.AddWithValue("@PaymentStatus", odemedurum);
            cmd.Parameters.AddWithValue("@MonthlyFee", odenentutar);
            cmd.Parameters.AddWithValue("@ClassName", classing);
            cmd.Parameters.AddWithValue("@IsActive", aktiflik);
            cmd.Parameters.AddWithValue("@MotherName", MotherName);
            cmd.Parameters.AddWithValue("@FatherAddress", FatherAddress);
            cmd.Parameters.AddWithValue("@MotherAddress", MotherAddress);
            cmd.Parameters.AddWithValue("@FatherPhoneNumber", FatherPhoneNumber);
            cmd.Parameters.AddWithValue("@StudentsDetails", ogrenciDetails);
            cmd.Parameters.AddWithValue("@MotherPhoneNumber", MotherPhoneNumber);
            cmd.Parameters.AddWithValue("@IsMarried", IsMarried);
            SqlParameter photoParam = new SqlParameter("@Photo", SqlDbType.VarBinary, - 1);
            photoParam.Value = Photo != null ? (object) Photo: DBNull.Value;
            cmd.Parameters.Add(photoParam);
            if (PreRegistrationId.HasValue)
            {
                using var closePre = new SqlCommand("UPDATE PreRegistrations SET IsActive=0 WHERE Id=@Id AND ClassId=dbo.GetSirketIdByUserId(@UserId) AND ISNULL(IsActive,1)=1",
                conn, transaction);
                closePre.Parameters.AddWithValue("@Id", PreRegistrationId.Value);
                closePre.Parameters.AddWithValue("@UserId", UserId);
                if (closePre.ExecuteNonQuery() != 1) throw new InvalidOperationException("Ön kayıt daha önce dönüştürülmüş veya erişiminiz yok.");
            }
            cmd.ExecuteNonQuery();
            transaction.Commit();
            StudentId = StudentIdGuid;
            _edits.AcceptChanges();
        }
        MessageBox.Show("Öğrenci başarıyla eklendi.");
        _form2.DeleteAndLog("Aysstudents", "Id", StudentIdGuid, UserId, "0", "INSERT");
        RefreshData?.Invoke(this, new EventArgs());
        // Ana formdaki listeyi güncelle
        _form2.dataGridViewStok.DataSource = _form2.LoadStockDataRefresh(UserId);
        // Bu formu kapat
        _edits.AcceptChanges();
        this.Close();
    }

    private void btnOgrenciYonetimiSil_Click(object sender, EventArgs e)
    {
        if (StudentId == Guid.Empty) return;
        if (MessageBox.Show("Öğrenci pasife alınsın mı?", "Öğrenci", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
        string query = @"
            Update AysStudents set IsDeleted=1
            WHERE Id = @id AND SchoolId=dbo.GetSirketIdByUserId(@UserId)";
        using (SqlConnection con = new SqlConnection(_form2.connectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // Parametreleri Ekle
                cmd.Parameters.AddWithValue("@id", StudentId);
                if (!cmd.Parameters.Contains("@UserId")) cmd.Parameters.AddWithValue("@UserId", UserId);
                if (cmd.ExecuteNonQuery() == 0) throw new InvalidOperationException("Öğrenci kaydı bulunamadı veya erişiminiz yok.");
                _edits.AcceptChanges();
            }
        }
        MessageBox.Show("Öğrenci Silindi Eski Kayıtlar için Loglara Bak..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        _form2.DeleteAndLog("Aysstudents", "Id", StudentId, UserId, "1", "DELETE");
        RefreshData?.Invoke(this, new EventArgs());
        _form2.dataGridViewStok.DataSource = _form2.LoadStockDataRefresh(UserId);
        _edits.AcceptChanges();
        this.Close();
    }
}
