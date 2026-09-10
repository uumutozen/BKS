# 10 Eylül 2026 — ekran ve ödeme akışları

Öğrenci listesinde sağ tık → **Ödemeler / plan** ile ödeme ekranı açılır. **Tek ödeme ekle** bir vade oluşturur. **Aylık / yıllık plan** dönem, dönem sayısı, ilk vade ve her dönemin tutarını alır; kaydetmeden önce tüm vadeleri ve toplamı gösterir. Her dönem tutarı toplamın bölünmesi değildir. Plan mevcut ödemelere eklenir. Tahsil edilen bekleyen satırları seçip **Seçilenleri onayla** kullanın. Bir toplu onay sırasında kayıt başka bir kullanıcı tarafından değiştirilmişse işlem bütünüyle geri alınır; liste yenilenmelidir.

Öğrenci ve personelin **İşlem geçmişi** hem Ribbon hem sağ tık menüsünden açılır. Bölümün tüm geçmişi listelenir; seçilen satırın JSON kayıt görüntüsü alt alanda alan–değer olarak açılır. Eski `deleteandlogs` görünümü ve şirket/modül filtreleri korunur. Açık geçmiş yeniden çağrıldığında veri yenilenir.

Özel rapor tasarımı ve çalıştırma ortak `ReportQuery`, `ReportRepository` ve `ReportParameterBinding` kullanır. Parametreler sabit Designer tablosunda düzenlenir: metin, tam sayı, tutar, tarih, Evet/Hayır, GUID ve NULL. `:Ad` ve `@Ad` desteklenir; yorumlar, metin sabitleri ve köşeli parantezli adlar değiştirilmez. Var olan rapor yüklenip kaydedildiğinde aynı kayıt güncellenir; **Yeni rapor** ayrı kayıt başlatır. Rapor ekranı SELECT/WITH sorguları içindir; veri değiştiren SQL kabul edilmez. Bu metin doğrulaması veritabanı yetkilerinin yerine geçmez. Parametre türleri mevcut veritabanı şemasında saklanmadığı için rapor açıldığında seçilir; aynı açık ekranda sorgu değişirse eşleşen değerler korunur.

Gelir/gider kaydı ortak doğrulama ve ayrı repository üzerinden yapılır. Kaydetme sırasında aynı işlemin tekrar gönderilmesi engellenir; şirketi bulunmayan kullanıcı için kayıt eklenmez. Liste toplam gelir, gider ve bakiyeyi gösterir. Mevcut şemada olmayan tarih veya kategori alanları varsayılmadı.

Fatura ekranı `InvoiceDraft` ve `InvoiceRepository` kullanır. Satır doğrulaması ve ekranda ara toplam/KDV/genel toplam vardır. Numara veritabanı transaction'ında ayrılır, PDF aynı numarayla üretilir ve PDF içeriği veritabanına kaydedilir. Kayıt başarısız olursa transaction geri alınır; oluşturulan yerel taslak PDF temizlenir. Kayıt başarılı olup sonraki liste yenilemesi başarısız olursa yeniden kaydetmek yerine Yenile kullanılmalıdır. PDF üretimi mevcut yerel fatura kaydı kapsamındadır.

Formların sabit kontrol hiyerarşileri `.Designer.cs` içinde korunur. `FormOperation` async işlemleri sıraya koyar, hata mesajını ekranda tutar ve kayıt sürerken kapanışı engeller. `FormDraft` fatura/rapor taslaklarında kaydet–vazgeç–iptal akışını bekler. Dinamik menüler `TransientMenu` üzerinden kapanış olayının ardından temizlenir; WinForms menüyle çalışırken `Dispose` çağrılmaz.

## Doğrulama sınırları

56 iş kuralı kontrolü, 96 DPI'da 6.822 Windows açılış/yerleşim kontrolü ve üç gerçek PDF üretim kontrolü geçti. Kontrol çıktıları `Kontrol_Sonuclari/Rework` içindedir. Rapor ve ödeme ekranları örnek verilerle, canlı SQL/API çağrısı olmadan açıldı. Canlı veritabanında fatura/ödeme yazma, onay transaction'ları ve mevcut kurulumun saklı yordamları denenmedi. Şema göçü veya canlı veri değişikliği uygulanmadı.

Ribbon sekmesi eklemek için `RIBBON_TASARIM_REHBERI.md` içindeki çalışan yapı üzerinden hazırlanmış örneği izleyin.
