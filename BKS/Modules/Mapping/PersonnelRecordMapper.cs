namespace BKS;

/// <summary>Personel kaydını forma aktarır; eksik sütunlar ve boş tarihler kartın açılmasını engellemez.</summary>
internal static class PersonnelRecordMapper
{
    public static void Fill(PersonelForm form, RecordValues record)
    {
        form.PersonelId = record.RequiredId("PersonelId");
        var fields = new RecordFieldBinder(record);
        fields.Text(
            (form.txtPersonelAd, "Adı"), (form.txtPersonelSoyad, "Soyadı"),
            (form.cbxPersonelUyruk, "Uyruk"), (form.txtPersonelKimlik, "Kimlik No"),
            (form.txtPersonelTel, "Telefon"), (form.txtPersonelMail, "E-posta"),
            (form.txtPersonelIletişimAcilDurum, "Acil Yakınlar"), (form.txtPersonelAdres, "Adres"),
            (form.txtPersonelDepartman, "Departman"), (form.txtPersonelGorev, "İş Ünvanı"),
            (form.txtPersonelPersonelNo, "Personel Numarası"), (form.cbxPersonelSigorta, "SGK Sicil No"),
            (form.txtPersonelMaas, "Maaş"), (form.txtPersonelPrimVeEk, "Ek Ödeme"),
            (form.txtPersonelYemekYol, "Yemek ve Ulaşım Ücreti"), (form.txtPersonelSGKSicilNum, "SGK Sicil No"),
            (form.txtPersonelSaglikSigorta, "Sağlık Sigortası Bilgileri"), (form.txtPersonelEmeklilik, "Emeklilik Bilgileri"),
            (form.cbxPersonelEgitimDurumu, "Eğitim Durumu"), (form.cbxPersonelUniversite, "Üniversite Bölümü"),
            (form.txtPersonelUniBolum, "Üniversite Bölümü"), (form.txtPersonelSertifika, "Sertifika ve Eğitim Bilgileri"),
            (form.txtPersonelYabanciDil, "Yabancı Dil"), (form.txtPersonelAyrilmaNedeni, "İşten Ayrılma Nedeni"),
            (form.txtPersonelKidemTazminat, "Kıdem Tazminatı"));
        fields.Date(form.dtpPersonelDG, "Doğum Tarihi");
        fields.Date(form.dtpPersonelIseBaslamaTarihi, "İşe Başlama Tarihi");
        fields.Date(form.dtpPersonelCıkısTarihi, "İşten Ayrılma Tarihi");
        string gender = record.Get("Cinsiyet", string.Empty);
        form.rbtPersonelErkek.Checked = gender == "Erkek";
        form.rbtPersonelKadin.Checked = gender == "Kadın";
        form.rbtPersonelEvli.Checked = record.Get<bool>("Evli mi?");
        form.rbtPersonelBekar.Checked = !form.rbtPersonelEvli.Checked;
        form.rbtPersonelEgitimGorevlisiEvet.Checked = record.Get<bool>("Öğretmen mi?");
        form.rbtPersonelEgitimGorevlisiHayir.Checked = !form.rbtPersonelEgitimGorevlisiEvet.Checked;
        form.cbxPersonelCalismaSekli.Text = record.Get<bool>("Aktif mi?") ? "Aktif" : "Pasif";
        form.cbxPersoneIIsAyrıldı.Checked = record.Get<bool>("İşten Ayrıldı mı?");
    }
}
