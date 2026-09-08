using System.Data;
using System.Data.SqlClient;

namespace BKS;

public partial class PersonelForm
{
    private void BuildModernPersonnelFormLayout()
    {
        foreach (var panel in new[]
        {
            panel1,
            panel2,
            panel3
        })
        {
            foreach (var label in panel.Controls.OfType<Label>().ToArray()) panel.Controls.Remove(label);
            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = true
            };
            foreach (var radio in panel.Controls.OfType<RadioButton>().ToArray())
            {
                radio.AutoSize = true;
                flow.Controls.Add(radio);
            }
            panel.Controls.Add(flow);
        }
        _photoEditor = new PhotoEditor(pbxPersonelPicture);
        _photoEditor.PhotoChanged += bytes =>
        {
            Photo = bytes;
            _photoChanged = true;
        };
        _sections = new SectionedForm()
        .AddSection("Kimlik bilgileri", "Personelin temel bilgilerini ve fotoğrafını düzenleyin.",
        new ProfilePanel(_photoEditor, new ResponsiveFields(("Ad *", txtPersonelAd), ("Soyad *", txtPersonelSoyad), ("Uyruk", cbxPersonelUyruk),
        ("Kimlik / pasaport numarası *", txtPersonelKimlik), ("Doğum tarihi", dtpPersonelDG), ("Cinsiyet", panel1), ("Medeni durum", panel2))))
        .AddSection("İletişim", "Telefon, e-posta, adres ve acil durumda ulaşılacak kişi.",
        new ResponsiveFields(("Telefon", txtPersonelTel), ("E-posta *", txtPersonelMail), ("Adres", txtPersonelAdres),
        ("Acil durum iletişimi", txtPersonelIletişimAcilDurum)))
        .AddSection("Çalışma bilgileri", "İşe giriş, görev ve çalışma bilgileri.",
        new ResponsiveFields(("İşe başlama", dtpPersonelIseBaslamaTarihi), ("Departman", txtPersonelDepartman), ("Görev", txtPersonelGorev),
        ("Personel numarası", txtPersonelPersonelNo), ("Eğitim görevlisi", panel3)))
        .AddSection("Eğitim", "Eğitim geçmişi, sertifikalar ve yabancı dil bilgisi.",
        new ResponsiveFields(("Eğitim durumu", cbxPersonelEgitimDurumu), ("Bölüm", txtPersonelUniBolum), ("Sertifikalar", txtPersonelSertifika),
        ("Yabancı dil", txtPersonelYabanciDil)))
        .AddSection("Ücret ve sigorta", "Ücret, ek ödemeler ve sigorta bilgileri.",
        new ResponsiveFields(("Maaş", txtPersonelMaas), ("Prim / ek ödeme", txtPersonelPrimVeEk), ("Yemek / yol", txtPersonelYemekYol),
        ("SGK sicil", txtPersonelSGKSicilNum), ("Sağlık sigortası", txtPersonelSaglikSigorta), ("Emeklilik", txtPersonelEmeklilik)))
        .AddSection("İşten ayrılma", "Ayrılış bilgileri yalnızca işten ayrılan personel için doldurulur.",
        new ResponsiveFields(("İşten ayrıldı", cbxPersoneIIsAyrıldı), ("Çıkış tarihi", dtpPersonelCıkısTarihi), ("Ayrılma nedeni", txtPersonelAyrilmaNedeni),
        ("Kıdem tazminatı", txtPersonelKidemTazminat)));
        var ribbon = Screens.Ribbon("Personel", new RibbonCommand("Kaydet", RibbonIcon.Backup, RunPersonnelSave, () => PersonelId == Guid.Empty),
        new RibbonCommand("Güncelle", RibbonIcon.Edit, RunPersonnelUpdate, () => PersonelId != Guid.Empty), new RibbonCommand("Pasife al",
        RibbonIcon.Archive, RunPersonnelDelete, () => PersonelId != Guid.Empty), new RibbonCommand("Temizle", RibbonIcon.Refresh,
        RunPersonnelClear), new RibbonCommand("Kapat", RibbonIcon.Restore, Close));
        Screens.Install(this, _sections, ribbon, "Personel kartı");
        Shown += (_, _) => ribbon.RefreshCommands();
        KeyPreview = true;
        KeyDown += (_, e) =>
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                UiActions.Run(() =>
                {
                    if (PersonelId == Guid.Empty) RunPersonnelSave();
                    else RunPersonnelUpdate();
                });
                e.SuppressKeyPress = true;
            }
        };
    }
}
