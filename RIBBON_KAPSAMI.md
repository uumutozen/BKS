# Gönderilen ribbon talimatının uygulama eşlemesi

| Talimat | Uygulanan karşılık |
|---|---|
| Dosya düğmesi olmasın | Yok; ilk sekme Ana Sayfa |
| Ölçüler | 30 px sekme + 96 px komut alanı; DPI ile ölçeklenir |
| Büyük / küçük komut | 68 px büyük düğme, 30 px simge; 18 px küçük simge, üçlü küçük komut sütunları |
| Renk / yazı | Belirtilen palette düz yüzeyler; Segoe UI 8.5–9.5 pt |
| Dar ekran | Sekmeler ve komut grupları yazılı Diğer menüsüne geçer; yatay ribbon kaydırması yok |
| Daraltma | Görünür düğme, Ctrl+F1; kullanıcı kimliğine göre yerel tercih |
| Açık belgeler | Tek örnek, kimlikle detay, kapatma simgesi ve sekme sağ tık menüsü |
| Formlar | Öğrenci, personel, ödeme detayı, fatura ve raporlar gömülü belge; kısa ödeme planı modal |
| Aynı komut / sağ tık | Ortak komut tanımı ve simge ailesi |
| Yetki | Mevcut API anahtarları, doğrulama tamamlanmadan kayıt komutları kapalı |
| Ana Sayfa | Gerçek yetkilere bağlı bölüm bağlantıları; uydurma istatistik yok |
| Kaydedilmemiş veri | EditSession; kayıt doğrulaması veya SQL hatası kapanışı durdurur |
| DPI / çözünürlük | Kontrol senaryoları eklendi; bu ortamda Windows görsel testi çalıştırılmadı |

## Menü ve veri karşılığı

- Ana Sayfa: izinli mevcut ekranlara erişim.
- Öğrenciler: öğrenci kartları, ön kayıt, kesin kayıt dönüşümü, arşiv, Excel içe alma.
- Sınıf & Eğitim: mevcut sınıf tanımları ve öğretmen ataması; eğitim programı/yoklama modeli yok.
- Veliler: ayrı bir veli listesi, entity veya yetki modülü kaynak projede yok. Veli
  bilgileri mevcut öğrenci kartındaki Veli bilgileri bölümünde korunur; ayrı sekme üretilmedi.
- Finans: tahsilatlar, öğrenci ödeme detayları, gelir/gider ve fatura merkezi.
- Personel: personel kartları, fotoğraf, arşiv ve Excel içe alma.
- Yemek & Servis: gerçek model/ekran/servis yok; menü kaydedilmedi.
- Etkinlikler: gerçek model/ekran/servis yok; menü kaydedilmedi.
- Raporlar: mevcut raporları çalıştırma; yönetici için mevcut tasarım ekranı.
- Ayarlar: yetki yenileme, bağlantı bilgisi ve çıkış; bağlantı düzenlemesi giriş ekranında.

MD'deki SMS, yoklama, beslenme, servis güzergâhı, toplu veli iletişimi, gelişim
raporları, sınıf programları ve diğer eksik işlevler yapılmış gibi gösterilmedi.
Yeni iş modülleri için eklenme noktası Form2.Commands.cs / ModuleReader'dır.
Mevcut SQL yazma işlemlerinin tamamını arka plan servislerine taşıma bu sürümde
tamamlanmış değildir. Derleme ve canlı veri doğrulaması DOGRULAMA.txt'de ayrıdır.
