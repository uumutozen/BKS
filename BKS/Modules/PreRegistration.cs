using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.ComponentModel;
using System.Collections;
namespace BKS;
public partial class Form2
{
    private void btnOnKayitEkle_Click(object sender, EventArgs e)
    {
        string firstname = string.IsNullOrWhiteSpace(txtOnKayitAd.Text) ? "veri yok": txtOnKayitAd.Text;
        string lastname = string.IsNullOrWhiteSpace(txtOnKayitSoyad.Text) ? "veri yok": txtOnKayitSoyad.Text;
        string velitel = string.IsNullOrWhiteSpace(txtOnKayitVeliTel.Text) ? "veri yok": txtOnKayitVeliTel.Text;
        string not = string.IsNullOrWhiteSpace(txtOnKayitNot.Text) ? "veri yok": txtOnKayitNot.Text;
        string babaAd = string.IsNullOrWhiteSpace(txtOnKayitBabaAd.Text) ? "veri yok": txtOnKayitBabaAd.Text;
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
                                                @IsActive = @IsActive",
        conn))
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
        _pageEdits[tabPageOgrenciOnKayit.Name].AcceptChanges();
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
        DialogResult result = MessageBox.Show("Seçili kaydı silmek istediğinizden emin misiniz?", "Kayıt Sil", MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);
        if (result != DialogResult.Yes)
        return;
        DataGridViewRow selectedRow = dgvOnKayitlar.SelectedRows[0];
        Guid kayitId = Guid.Parse(selectedRow.Cells["Id"].Value.ToString());
        try
        {
            ExecuteNonQueryCommand(
            "UPDATE PreRegistrations SET IsActive=0 WHERE Id=@Id AND ClassId=dbo.GetSirketIdByUserId(@UserId)",
            CommandType.Text,
            DbParam("@Id", kayitId), DbParam("@UserId", UserId));
            LoadOnKayitlar(UserId);
            MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void btnKesinKayitYap_Click(object sender, EventArgs e)
    {
        btnKesinKayitYap_Click_1(sender, e);
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
              WHERE p.ClassId = dbo.GetSirketIdByUserId(@ClassId) AND ISNULL(p.IsActive,1)=1",
        CommandType.Text,
        DbParam("@ClassId", classId));
    }
    private void btnKesinKayitYap_Click_1(object sender, EventArgs e)
    {
        if (dgvOnKayitlar.CurrentCell == null)
        return;
        DataGridViewRow row = dgvOnKayitlar.Rows[dgvOnKayitlar.CurrentCell.RowIndex];
        if (!_allowedModules.Contains(tabPageStok.Name)) return;
        _documents.OpenDocument("preregistration:" + row.Cells["Id"].Value, "Ön kayıttan öğrenci", () =>
        {
            OgrenciForm ogrForm = new OgrenciForm(this);
            LoadStudentClassComboBox(ogrForm.cmbogrsınıf, UserId);
            ogrForm.txtOgrenciAd.Text = row.Cells["İsim"].Value?.ToString() ?? string.Empty;
            ogrForm.textSoyad.Text = row.Cells["Soyisim"].Value?.ToString() ?? string.Empty;
            ogrForm.txtBabaAd.Text = row.Cells["Baba Adı"].Value?.ToString() ?? string.Empty;
            ogrForm.textOgrenciDetay.Text = row.Cells["Notlar"].Value?.ToString() ?? string.Empty;
            ogrForm.txtBabaTel.Text = row.Cells["Baba Telefon"].Value?.ToString() ?? string.Empty;
            ogrForm.numericPrice.Text = row.Cells["Aylık Ücret"].Value?.ToString() ?? string.Empty;
            ogrForm.checkOdemeDurum.Checked = DataValues.Boolean(row.Cells["Ödeme Durumu"].Value);
            ogrForm.dateDogum.Text = row.Cells["Doğum Tarihi"].Value?.ToString() ?? string.Empty;
            ogrForm.PreRegistrationId = Guid.Parse(row.Cells["Id"].Value.ToString());
            ogrForm.UserId = UserId;
            ogrForm.RefreshData += (_, _) =>
            {
                RefreshStudentGrid();
                LoadOnKayitlar(UserId);
            };
            return ogrForm;
        }, tabPageStok.Name);
    }
}
