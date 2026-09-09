# BKS 1.4.0 — Teslim ve doğrulama raporu

## Uygulanan düzenlemeler

1. Ribbon sayfası ve aktif belge birbirinden ayrıldı. `SelectRibbonModule` ve belge Ribbon enjeksiyonu kaldırıldı. Sekme seçimi modül açmıyor; belge seçimi Ribbon sayfasını değiştirmiyor.
2. Öğrenci/personel Ara komutlarının gizli modül navigasyonu kaldırıldı. Düzenleme, silme, rapor çalıştırma ve yenileme komutları uygun aktif belge bağlamıyla sınırlandı.
3. Açık belgelerin tek instance yönetimi, kapatma menüsü ve kaydedilmemiş değişiklik denetimi korundu. Belge sekmelerine hover görünümü eklendi.
4. Açık tema, ince ayraçlar, 134 px kompakt Ribbon, büyük/küçük ikonlu komutlar ve seçili sayfa vurgusu uygulandı.
5. Kayıt formlarının sabit yerleşimleri gerçek Designer kontrollerine taşındı. Kaydet/Güncelle/Pasife al/Temizle/Kapat ve fotoğraf komutları form içi araç çubuklarında.
6. Ortak grid: 36 px başlık, 32 px satır, Segoe UI, hafif yatay çizgiler, açık mavi seçim, teknik kolon gizleme, Türkçe tarih/para formatı, sütun seçimi ve filtreler. Düzenlenebilir fatura kalemleri ile ödeme çoklu seçimi korundu.
7. Fotoğraf seçme/kaldırma davranışı, Designer'ın oluşturduğu kontrollere bağlandı. Personel fotoğraf yüklemesinin iptal/dispose davranışı korundu.
8. SQL şeması ve mevcut `.resx` dosyaları değiştirilmedi. İşlem kodları yerinde bırakıldı; ödeme formlarında mevcut SQL işlemleri yeni Designer form callback'lerine bağlandı.

## Designer'a taşınan formlar

- OgrenciForm: Genel Bilgiler, Veli Bilgileri, Ücret ve Durum, Notlar; fotoğraf ve işlem araç çubuğu.
- PersonelForm: Kimlik Bilgileri, İletişim, Çalışma Bilgileri, Eğitim, Ücret ve Sigorta, İşten Ayrılma; fotoğraf ve işlem araç çubuğu. Çalışma şekli/üniversite/sigorta alanları görünür hiyerarşiye dahil edildi.
- FormFatura: fatura bilgileri, statik kalem kolonları, kayıtlı faturalar ve araç çubuğu.
- arsivForm: mevcut Designer yükleme/filtre/özet/statü alanları yeniden kullanıldı; runtime ekran değiştirme kaldırıldı, statik kolonlar Designer'a alındı.
- OzelRapor ve RaporCalistirForm: sabit rapor alanları, sorgu/parametre/sonuç sekmeleri, liste araçları. Veriden türeyen parametreler ayrılmış panelde dinamik kalır.
- Form2: Öğrenciler, Personel, Ön Kayıt, Sınıflar, Tahsilatlar, Gelir/Gider, Raporlar ve Ana Sayfa iskeleti.
- PaymentDetailsForm, PaymentPlanForm, DataListForm, ConnectionSettingsForm: ayrı `.cs/.Designer.cs/.resx` form üçlüleri.

`OgrenciForm.Layout.cs`, `PersonelForm.Layout.cs`, `Form2.Layout.cs` kaldırıldı.
Form2'nin olay bağlama kodu `Form2.ModuleBehavior.cs` içine ayrıldı.
Parametresiz Designer constructor'ları ve csproj DependentUpon ilişkileri eklendi.
Bu liste Designer kaynak yapısının tamamlandığını belirtir; Visual Studio'da açılarak doğrulama bu ortamda yapılmadı.

## Gerçekten çalıştırılan doğrulamalar

- .NET SDK 8.0.408 ile Release derlemesi: başarılı.
- Windows x64 yayınlama: başarılı. Hem NuGet bağımlılıklarıyla hem paketteki `Dependencies` DLL'leriyle derleme yapıldı. Teslim edilen BKS.exe, son kaynak ve paket DLL'leriyle üretilmiştir.
- Son yayınlama: **0 hata, 210 uyarı**. Uyarılar gizlenmedi veya bastırılmadı.
- `BKS.Checks`: **42 kontrol geçti**. Tek instance, filtreler, parasal hesaplar, veri dönüştürmeleri, XLSX okuma ve 160 sayısal yerleşim/DPI kombinasyonu dahil.
- Orijinal `.resx` dosyaları ve Database dosyaları: byte karşılaştırmasıyla korunmuş durumda.
- Öğrenci kartının tüm 17 giriş alanı ve tüm bağlı personel giriş alanları korundu. Personelde kullanılmayan/bağsız `textBox1` temizlendi.
- Canlı SQL/API/CRUD çağrısı veya Windows GUI çalıştırılmadı. PDF motoru değiştirilmedi; PDF paketi bu çalışma kapsamında yeniden çalıştırılmadı.

## Sekiz kabul senaryosunun durumu

| # | Senaryo | Bu teslimde doğrulama |
|---|---|---|
| 1 | Finans Ribbon sekmesi öğrenci listesini değiştirmemeli | Navigasyon bağlantısı kaynakta kaldırıldı; Windows otomasyon senaryosu eklendi, burada çalıştırılmadı. |
| 2 | Ribbon sekmesi açık öğrenci kartını kapatmamalı | Yerel Ribbon enjeksiyonu kaldırıldı; Designer kökü ve kaydedilmemiş veri koruma senaryosu Windows kontrolüne eklendi, burada çalıştırılmadı. |
| 3 | Belge sekmesi Ribbon'u değiştirmemeli | ActiveDocumentChanged yalnızca durum/komut etkinliğini güncelliyor; Windows senaryosu eklendi, burada çalıştırılmadı. |
| 4 | Liste komutu modülü tek instance açmalı | DocumentRegistry regresyonu geçti; gerçek Ribbon komutunu iki kez çağıran Windows kontrolü eklendi, burada çalıştırılmadı. |
| 5 | Öğrenci/personel/finans belgeleri Ribbon gezinmesinde kalmalı | Sekme gezinme ve sayım kontrolü Windows otomasyonuna eklendi, burada çalıştırılmadı. |
| 6 | Öğrenci ve personel Designer ile düzenlenebilmeli | Sabit kontroller Designer'da, kaynak derleniyor; Visual Studio View Designer doğrulaması Windows'ta yapılmalı. |
| 7 | Gridler ortak görünümde olmalı | Merkezi stil ve Designer liste bağları uygulandı; gerçek Windows görsel incelemesi yapılmadı. |
| 8 | %100/%125/%150 DPI taşmamalı | Sayısal 160 yerleşim kombinasyonu geçti; gerçek Windows DPI/görsel testi yapılmadı. |

Windows'ta `Yerlesim_Kontrol.cmd` çalıştırın; aynı kontrolü ilgili Windows ölçeklerinde tekrar edin.
Bu komut SQL/API kullanmadan sonuçları `Yerlesim_Sonuclari/Windows_Yerlesim_Sonucu.txt` içine yazar.

## Kalan derleyici uyarıları

- CS0169: 1
- CS0618: 154
- CS0649: 1
- CS8600: 5
- CS8601: 14
- CS8602: 2
- CS8603: 5
- CS8604: 9
- CS8618: 10
- CS8622: 8
- CS8625: 1

154 uyarı mevcut System.Data.SqlClient API'lerinin obsolete işaretlenmesidir. Kalanlar nullable referanslar/olay imzaları ve kullanılmayan alanlar ile ilgilidir. Tam konumlar `Kontrol_Sonuclari/Release_WinX64_Derleme.txt` içindedir.

## Değişen/eklenen dosyalar

- `BKS/BKS.csproj`
- `BKS/ConnectionSettingsForm.Designer.cs`
- `BKS/ConnectionSettingsForm.cs`
- `BKS/ConnectionSettingsForm.resx`
- `BKS/DataListForm.Designer.cs`
- `BKS/DataListForm.cs`
- `BKS/DataListForm.resx`
- `BKS/Form2.Commands.cs`
- `BKS/Form2.Designer.cs`
- `BKS/Form2.Lifecycle.cs`
- `BKS/Form2.ModuleBehavior.cs`
- `BKS/Form2.NavigationChecks.cs`
- `BKS/Form2.Ribbon.cs`
- `BKS/Form2.cs`
- `BKS/FormFatura.Designer.cs`
- `BKS/FormFatura.cs`
- `BKS/Infrastructure/AppConfiguration.cs`
- `BKS/Infrastructure/LayoutRules.cs`
- `BKS/Modules/Payments.cs`
- `BKS/OgrenciForm.Designer.cs`
- `BKS/OgrenciForm.Runtime.cs`
- `BKS/OgrenciForm.cs`
- `BKS/OzelRapor.Designer.cs`
- `BKS/OzelRapor.cs`
- `BKS/PaymentDetailsForm.Designer.cs`
- `BKS/PaymentDetailsForm.cs`
- `BKS/PaymentDetailsForm.resx`
- `BKS/PaymentPlanForm.Designer.cs`
- `BKS/PaymentPlanForm.cs`
- `BKS/PaymentPlanForm.resx`
- `BKS/PersonelForm.Designer.cs`
- `BKS/PersonelForm.Persistence.cs`
- `BKS/PersonelForm.Photo.cs`
- `BKS/PersonelForm.Runtime.cs`
- `BKS/PersonelForm.Validation.cs`
- `BKS/PersonelForm.cs`
- `BKS/RaporCalistirForm.Designer.cs`
- `BKS/RaporCalistirForm.cs`
- `BKS/UI/BksRibbon.cs`
- `BKS/UI/Dialogs.cs`
- `BKS/UI/Documents/DocumentTabPresenter.cs`
- `BKS/UI/Documents/DocumentWorkspace.cs`
- `BKS/UI/LayoutDiagnostics.cs`
- `BKS/UI/ListSurface.cs`
- `BKS/UI/Lists/DesignerListBinding.cs`
- `BKS/UI/Lists/GridAppearance.cs`
- `BKS/UI/PhotoDiagnostics.cs`
- `BKS/UI/PhotoEditor.cs`
- `BKS/UI/RecordContextMenus.cs`
- `BKS/UI/RibbonGroupView.cs`
- `BKS/UI/RibbonPalette.cs`
- `BKS/UI/RibbonTabButton.cs`
- `BKS/UI/Screens.cs`
- `BKS/arsivForm.Designer.cs`
- `BKS/arsivForm.cs`
- `DOGRULAMA.txt`
- `KAYNAK_KONTROLU.txt`
- `MIMARI.md`
- `README.md`
- `RIBBON_KAPSAMI.md`
- `Tests/Program.cs`
- `DEGISIKLIKLER_v1.4.0.md`
- `Kontrol_Sonuclari/*`
- `Uygulama/*`: güncel Windows x64 yayın çıktısı.

## Kaldırılan kaynak dosyalar

- `BKS/Form2.Layout.cs`
- `BKS/OgrenciForm.Layout.cs`
- `BKS/PersonelForm.Layout.cs`
- `BKS/UI/DataListForm.cs`
