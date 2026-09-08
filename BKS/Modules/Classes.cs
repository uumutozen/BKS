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
    private void YasGrubuLoad()
    {
        cbxOgrenciYonetimiYasGrubu.Items.Clear();
        cbxOgrenciYonetimiYasGrubu.Items.AddRange(new object[]
        {
            "3 YAŞ",
            "4 YAŞ",
            "5 YAŞ",
            "6 YAŞ"
        });
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
            int sonuc = (int) outputParam.Value;
            if (sonuc == 1) _pageEdits["classes"].AcceptChanges();
            MessageBox.Show(sonuc == 1 ? "Sınıf Başarıyla Eklendi.": "Bu Sınıf Zaten Mevcut.");
        }
        SinifLoad(UserId);
    }
    private void btnOgrenciYonetimiSinifGuncelle_Click(object sender, EventArgs e)
    {
        if (DgvOgrenciYonetimiSiniflar.CurrentRow == null)
        return;
        if (!TryGetSelectedGuid(DgvOgrenciYonetimiSiniflar, "Id", out var classId)) return;
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
              WHERE Id = @ClassId AND IsDeleted = 0
                AND SchoolId = dbo.GetSirketIdByUserId(@UserId)",
        CommandType.Text,
        DbParam("@ClassId", classId),
        DbParam("@SinifAdi", sinifAdi),
        DbParam("@YasGrubu", yasGrubu),
        DbParam("@OgretmenAdi", ogretmenAdi),
        DbParam("@UserId", UserId));
        _pageEdits["classes"].AcceptChanges();
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
        "UPDATE AYSClasses SET IsDeleted = 1 WHERE Id = @id AND SchoolId=dbo.GetSirketIdByUserId(@UserId)",
        CommandType.Text,
        DbParam("@id", classId), DbParam("@UserId", UserId));
        MessageBox.Show("Sınıf Silindi. Eski kayıtlar için loglara bakın.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        DeleteAndLog("AYSClasses", "Id", classId, UserId, "1", "DELETE");
        SinifLoad(UserId);
    }
    private void DgvOgrenciYonetimiSiniflar_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex<0)
        return;
        DataGridViewRow row = DgvOgrenciYonetimiSiniflar.Rows[e.RowIndex];
        DgvOgrenciYonetimiSiniflar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        DgvOgrenciYonetimiSiniflar.MultiSelect = false;
        txtOgrenciYonetimiSınıfAdı.Text = row.Cells["Sınıf"].Value?.ToString();
        cbxOgrenciYonetimiYasGrubu.Text = row.Cells["Yaş Grubu"].Value?.ToString();
        cbxOgrenciYonetimiOgretmen.Text = row.Cells["Öğretmen Adı"].Value?.ToString();
        sinifid = Guid.TryParse(Convert.ToString(row.Cells["Id"].Value), out var id) ? id: Guid.Empty;
        _pageEdits["classes"].AcceptChanges();
    }
}
