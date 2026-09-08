# BKS 1.2.0

Mevcut BKS projesinin, gönderilen ribbon talimatına göre yeniden düzenlenen kaynak sürümü.
Native WinForms kullanır; DevExpress kurulumu veya lisansı istemez.

## Başlatma

Windows'ta .NET 8 veya daha yeni SDK ile `Derle_ve_Yayinla.cmd` çalıştırın.
Ardından `Calistir.cmd` uygulamayı açar. Hedef .NET 8 olduğundan çalıştırılacak
bilgisayarda .NET 8 Desktop Runtime bulunmalıdır. Derleme dosyaları `Uygulama/`
klasörüne yazılır. Giriş ekranında mevcut bağlantı ayarlarını kullanın.

Bu pakette derlenmiş BKS.exe yoktur. Burada .NET SDK ve Windows bulunmadığı için
derleme / görsel test çalıştırılamadı. Yapılan kontroller `DOGRULAMA.txt` içindedir.

## Bu sürüm

- Dosya düğmesi olmayan, 126 piksel yüksekliğinde kompakt ribbon.
- MD'deki açık gri/beyaz renkler, mavi vurgu, büyük/küçük simgeler, alt grup adları.
- Dar ekranda kaydırma yerine yazılı “Diğer” menüsü; daraltma düğmesi ve Ctrl+F1.
- Ana Sayfa, Öğrenciler, Sınıf & Eğitim, Finans, Personel, Raporlar, Ayarlar.
- Öğrenci listesi ve sınıf listesi ayrı çalışma sekmelerinde.
- Öğrenci, personel, ödeme detayı, fatura ve raporlar tek belge yöneticisiyle açılır.
  Aynı kayıt anahtarı ikinci kez açılmaz; mevcut sekme seçilir.
- Sekme kapatma, orta tık, diğerlerini/tümünü/sağdakileri kapatma.
- Öğrenci, personel, fatura, rapor tasarımı ve kısa kayıt alanlarında değişiklik
  kontrolü; kaydet / vazgeç / iptal. Veritabanı işlemi başarısızsa kapanış iptal edilir.
- Ribbon ve sağ tık komutları aynı tanımdan çalışır; sağ tık simgeleri korunmuştur.
- Personel fotoğrafı seçme, önizleme, değiştirme ve kaldırma desteği korunmuştur.
- Sınıf güncelleme/silme şirket ve kayıt kimliğiyle sınırlandırıldı; sınıf işlem
  geçmişi için eksik tablo desteği eklendi. Öğrenci fotoğrafının yükleme sırasında
  kapanan akışa bağlı kalması ve düzenlemede kaybolması düzeltildi.

Yeni düzen ve eski servislerin ilişkisi `MIMARI.md`, talimat kapsamı ise
`RIBBON_KAPSAMI.md` içindedir. Yemek/servis ve etkinlik için bu projede veri modeli
ve işlev bulunmadığından çalışmayan menüler eklenmedi. Veli alanları öğrenci kartında.

## Kodda nereden başlanır?

| İş | Dosya / klasör |
|---|---|
| Menü başlıkları, komutlar, kısayollar | `BKS/Form2.Commands.cs` |
| Ana pencereyi birleştirme | `BKS/Form2.Ribbon.cs` |
| Ekran yerleşimleri | `BKS/Form2.Layout.cs`, `*.Layout.cs` |
| Yetki ve modül yükleme | `BKS/Form2.Session.cs` |
| Ortak komut davranışı | `BKS/UI/Commands/RibbonCommand.cs` |
| Ribbon çizimi ve taşma menüsü | `BKS/UI/BksRibbon.cs`, `RibbonGroupView.cs`, `RibbonAppearance.cs` |
| Belge açma / kapatma | `BKS/UI/Documents/DocumentWorkspace.cs` |
| Generic kayıt anahtarı yönetimi | `BKS/UI/Documents/DocumentRegistry.cs` |
| Generic SQL yürütme ve okuma | `BKS/Data/SqlDataAccess.cs` |
| Modül okuma sorguları | `BKS/Data/ModuleReader.cs` |
| Eski iş kuralları ve olay adaptörleri | `BKS/Modules/` |
| Öğrenci / personel kayıt işlemleri | `BKS/OgrenciForm.Persistence.cs`, `PersonelForm.Persistence.cs` |
| Fotoğraf | `BKS/UI/PhotoEditor.cs`, `BKS/Infrastructure/PersonnelPhotos.cs` |

`Tests/` ortak kuralların testlerini; `Yerlesim_Kontrol.cmd` Windows yerleşim
kontrolünü çalıştırır. `Database/` altındaki SQL dosyaları ihtiyaca göre yönetici
tarafından incelenmelidir; uygulama kendiliğinden tablo değiştirmez.
