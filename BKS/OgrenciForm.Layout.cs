using System.Data;
using System.Data.SqlClient;

namespace BKS;

public partial class OgrenciForm
{
    private void BuildModernStudentFormLayout()
    {
        var fields = new SectionedForm()
        .AddSection("Öğrenci bilgileri", "Öğrencinin temel bilgileri, sınıfı ve fotoğrafı.",
        new ResponsiveFields(("Öğrenci fotoğrafı", pictureBox1), ("Öğrenci adı *", txtOgrenciAd), ("Soyadı *", textSoyad),
        ("Doğum tarihi", dateDogum), ("Öğrenci kodu", textOgrenciKod), ("Sınıf *", cmbogrsınıf)))
        .AddSection("Veli bilgileri", "Anne ve baba iletişim bilgilerini ayrı alanlarda düzenleyin.",
        new ResponsiveFields(("Baba adı", txtBabaAd), ("Baba telefonu", txtBabaTel), ("Baba adresi", txtBabaEvAdres), ("Anne adı", txtAnneAd),
        ("Anne telefonu", txtAnneTel), ("Anne adresi", txtAnneEvAdres)))
        .AddSection("Ücret ve durum", "Öğrencinin kayıt, ödeme ve ek not bilgileri.",
        new ResponsiveFields(("Aile durumu", checkEvet), ("Aktif öğrenci", checkAktif), ("Ödeme durumu", checkOdemeDurum),
        ("Aylık ücret", numericPrice), ("Öğrenci notları", textOgrenciDetay)));
        var ribbon = Screens.Ribbon("Öğrenci", new RibbonCommand("Kaydet", RibbonIcon.Backup, RunStudentSave, () => StudentId == Guid.Empty),
        new RibbonCommand("Güncelle", RibbonIcon.Edit, RunStudentUpdate, () => StudentId != Guid.Empty), new RibbonCommand("Pasife al",
        RibbonIcon.Archive, RunStudentDelete, () => StudentId != Guid.Empty), new RibbonCommand("Kapat", RibbonIcon.Restore,
        Close));
        Screens.Install(this, fields, ribbon, "Öğrenci kartı");
        Shown += (_, _) => ribbon.RefreshCommands();
        KeyPreview = true;
        KeyDown += (_, e) =>
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                UiActions.Run(() =>
                {
                    if (StudentId == Guid.Empty) RunStudentSave();
                    else RunStudentUpdate();
                });
                e.SuppressKeyPress = true;
            }
        };
    }
}
