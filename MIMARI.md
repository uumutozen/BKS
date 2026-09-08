# Kod rehberi

## Mevcut yapı

Giriş ekranı Form1, ana pencere Form2'dir. Uygulama WinForms'tur; MVC controller,
Entity Framework veya katmanlı bir backend projesi arşivde yoktur. Giriş ve modül
listesi mevcut API üzerinden, ekran verileri SQL Server / mevcut stored procedure
sözleşmeleri üzerinden alınır. Bu sözleşmeler yeniden tasarlanmadı.

Önceki sürümde gizli TabControl ile tek liste gösteriliyor ve birçok detay formu
bağımsız açılıyordu. Form2.cs yaklaşık 2000 satırdı. Şimdi ana pencere kurulumu,
yetki yükleme, menü tanımı ve modül olayları ayrı dosyalardadır. Partial sınıflar
Designer alanlarına erişimi koruyan geçiş düzenidir; bu dosyalar bağımsız servis
olarak sunulmamıştır. İş kurallarının tamamı yeni domain katmanına taşınmış değildir.

## Ortak bileşenler

`RibbonCommand`: Kimlik, açıklama, simge, boyut, kısayol, yetki anahtarı, uygunluk
koşulu, grup ve sıra tutar. `Invoke()` tekrar giriş kontrolü ve hata sunumunu sağlar.
Ribbon, taşma menüsü ve sağ tık aynı komut nesnesini çağırır. Uzun süren mevcut API
ve liste okumalarının yüklenme durumu Form2.Session'dadır. Eski senkron yazma
metotları senkron kalmıştır; hepsi async hale getirilmiş değildir.

`DocumentRegistry<T>`: Sadece anahtar ve nesne ömrünü yönetir. WinForms bağımlılığı
yoktur. Aynı anahtarda factory yeniden çalışmaz. Factory başarısız olursa anahtar
rezerve edilmez. `DocumentWorkspace`, bu generic bileşeni WinForms sekmelerine bağlar.

`SqlDataAccess`: Bağlantı, komut ve okuyucu ömrünü tek yerde yönetir. `Execute<T>`
ve `Query<T>` tekrar kullanılabilir. Şirket filtrelerini otomatik tahmin etmez;
sorgular açıkça filtre içerir. `ModuleReader` ana liste sorgularını barındırır.
Mevcut kayıt işlemlerinin transaction sınırları ve SP parametreleri korunur.

`EditSession`: Form girdilerinin başlangıç durumunu alır. Görünmeyen SectionedForm
bölümlerini de izler. Fotoğraf ve fatura kalemleri ek durum sağlayıcısıyla izlenir.
`AcceptChanges()` yalnızca başarılı kayıt sonrası çağrılır. Arama kutuları kayıt
verisi sayılmaz. Ana pencere kapanışı tüm kayıt sekmelerinin kapanışını denetler.

## Yeni komut eklemek

Form2.Commands.cs içindeki ilgili gruba `Command(...)` ekleyin. Kimlik benzersiz
olsun; API'deki gerçek permission anahtarını ve seçili satır koşulunu verin.
İşlemi ilgili modül metoduna yönlendirin. Yeni detay için:

```csharp
_documents.OpenDocument(
    "student:" + studentId,
    "Öğrenci kartı",
    () => CreateStudentForm(studentId),
    tabPageStok.Name);
```

Örnekteki CreateStudentForm, eklenecek ekranın gerçek yükleme fabrikasını temsil
eder. Mevcut fabrikalar Modules/Students.cs ve Modules/Personnel.cs içindedir.
Kaydetme kodunu ribbon çizim sınıfına koymayın. Yeni bir iş modülü için backend
sözleşmesi, yetki anahtarı, gerçek form ve testler sağlandıktan sonra menü kaydedin.

## Kaynak dosyaları

Dokuz .resx açık ve benzersiz LogicalName / ManifestResourceName değerleriyle
kaydedilir. Varsayılan resource keşfi kapalıdır. Yeni partial dosyaları eklerken bu
ayarı kaldırmayın: önceki MSB3577 BKS.Form2.resources çakışmasını engeller.

## Korunan sınırlar

Öğrenci silme IsDeleted, personel silme IsActive ile mevcut pasife alma akışını
kullanır. Rapor tanımları kaynak projedeki ortak rapor tablosunu kullanır. Sunucu
kodları ve gerçek SQL şeması teslim edilmediği için bunların doğruluğu burada
yeniden onaylanamaz. Fotoğraf kolon uyumu ve GelirGider şirket kolonu için mevcut
Database dosyaları korunmuştur.
