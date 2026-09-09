namespace BKS;

/// <summary>Öğrenci listesinin sütun adları yalnızca bu eşleştirmede bilinir.</summary>
internal static class StudentRecordMapper
{
    public static void Fill(OgrenciForm form, RecordValues record)
    {
        form.StudentId = record.RequiredId("Id");
        var fields = new RecordFieldBinder(record);
        fields.Text(
            (form.txtOgrenciAd, "İsim"), (form.textSoyad, "Soyisim"),
            (form.txtBabaAd, "Baba Adı"), (form.txtAnneAd, "Anne Adı"),
            (form.cmbogrsınıf, "Sınıfı"), (form.textOgrenciKod, "Öğrenci Kodu"),
            (form.textOgrenciDetay, "Öğrenci Hakkında"), (form.txtBabaTel, "Baba Telefon"),
            (form.txtAnneTel, "Anne Telefon"), (form.txtBabaEvAdres, "Baba Adresi"),
            (form.txtAnneEvAdres, "Anne Adresi"));
        fields.Date(form.dateDogum, "Doğum Tarihi");
        fields.Number(form.numericPrice, "MonthlyFee");
        form.checkAktif.Checked = record.Get<bool>("Aktif Öğrenci mi");
        form.checkEvet.Checked = record.Get<bool>("Aile Ayrı Mı");
        form.checkOdemeDurum.Checked = record.Get<bool>("Ödeme Durumu")
            || record.Get("Ödeme Durumu", string.Empty) == "Ödeme Yapıldı";
        form.SetPhoto(record.Get<byte[]?>("FotoId"));
    }
}
