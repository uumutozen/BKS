# BKS 1.4.0 — Ribbon ve Designer düzenlemesi

Tam kaynak kod, mevcut bağımlılıklar ve derlenmiş Windows x64 uygulaması bu pakettedir.
Standart WinForms kullanılır; DevExpress veya başka ticari UI bağımlılığı eklenmedi.

## Başlatma

ZIP'i yeni bir klasöre çıkarıp `Calistir.cmd` veya `Uygulama/BKS.exe` çalıştırın.
.NET 8 Desktop Runtime (x64) gerekir. Kaynak kodu düzenlemek için `BKS.sln` açın.
Yeniden derleme/yayınlama: `Derle_ve_Yayinla.cmd` (.NET 8 SDK ve Windows Forms geliştirme araçları).

## Ribbon ve belgeler

- Ribbon sekmesine basınca yalnızca üst komut grupları değişir.
- Açık modüller ve kartlar kendi belge sekmelerinde kalır.
- Belge sekmesine geçince seçili Ribbon sekmesi korunur.
- Modül açmak için Öğrenci listesi, Personel listesi, Tahsilatlar gibi açık navigasyon komutlarını kullanın.
- Kaydet, Güncelle, Pasife al ve fotoğraf işlemleri kayıt kartının kendi araç çubuğundadır.
- Dosya/Backstage sekmesi eklenmedi. Ana Sayfa kapatılamaz.

## Designer

Öğrenci, personel, fatura, arşiv, rapor ve Form2 modül yerleşimleri `.Designer.cs` içindedir.
Ödeme detayları, aylık ödeme planı, işlem geçmişi ve bağlantı ayarları da ayrı Designer formlarına taşındı.
Visual Studio'da ilgili `.cs` dosyasına sağ tıklayıp **View Designer / Tasarımcıyı Görüntüle** seçin.
Kontrollerin hiyerarşisi, sekmeler, araç çubukları, alanlar ve fotoğraf bölümleri Designer'da tanımlıdır.
SQL/API işlemleri ve event handler gövdeleri normal `.cs` dosyalarında kalır.
Veriye bağlı rapor parametreleri çalışma anında, Designer'daki ayrılmış panel içine eklenir.

## Kontroller

- Release / win-x64 yayınlama: **başarılı; 0 hata, 210 uyarı**.
- Taşınabilir regresyon paketi: **42 kontrol geçti** (160 DPI/yerleşim ölçüm kombinasyonu dahil).
- Windows arayüzü, Visual Studio Designer ve %100/%125/%150 gerçek ekran ölçekleri bu Linux ortamında açılarak test edilmedi.
- `Yerlesim_Kontrol.cmd`: Windows'ta sekmeler, Designer tabloları, kartlar, fotoğraf ve Ribbon bağımsızlığı kontrollerini çalıştırır.
- `Tasarim_Onizle.cmd`: canlı SQL/API bağlantısı kurmadan arayüz önizlemesini açar.
- `Testleri_Calistir.cmd`: taşınabilir regresyon kontrollerini yeniden çalıştırır.

Ayrıntılı değişiklik listesi, kalan uyarılar ve sekiz kabul senaryosu `DEGISIKLIKLER_v1.4.0.md` içindedir.
Derleme ve test kayıtları `Kontrol_Sonuclari/` klasöründedir.
Mevcut veritabanı şeması, SQL dosyaları ve mevcut `.resx` kaynakları korunmuştur.
