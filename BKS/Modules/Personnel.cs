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
        if (e.RowIndex<0)
        return;
        DataGridViewRow row = dgvPersonelYonetimi.Rows[e.RowIndex];
        if (!_allowedModules.Contains(tabPagePersonelYonetimi.Name)) return;
        string key = "personnel:" + row.Cells["PersonelId"].Value;
        _documents.OpenDocument(key, "Personel kartı", () =>
        {
            PersonelForm personelForm = new PersonelForm(this);
            personelForm.UserId = this.UserId;
            personelForm.PersonelId = (Guid) row.Cells["PersonelId"].Value;
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
            personelForm.cbxPersonelCalismaSekli.Text = row.Cells["Aktif mi?"].Value?.ToString() == "Evet" ? "Aktif": "Pasif";
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
            return personelForm;
        }, tabPagePersonelYonetimi.Name);
    }
}
