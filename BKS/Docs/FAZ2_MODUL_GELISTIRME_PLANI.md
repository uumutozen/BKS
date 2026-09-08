# Faz 2 - Personel / Öğrenci / Gelir-Gider / Fatura Modernizasyon Planı

Bu fazda arayüz tarafındaki eski sabit konumlu ve büyük butonlu yapı, responsive kart düzenine taşındı. Eski işlem butonları görünür alandan kaldırıldı; işlemler komut şeridi, sağ tık menüsü ve klavye kısayolları üzerinden kullanılacak şekilde yeniden kurgulandı.

## Faz 2.1 - Ortak Tasarım Altyapısı
- `ModernWinForms.cs` ortak UI yardımcı sınıfı eklendi.
- Ortak renk paleti, kart yapısı, başlık/subtitle düzeni, grid stilleri ve input stilleri merkezi hale getirildi.
- Responsive `TableLayoutPanel`, `FlowLayoutPanel` ve kart düzenleri için tekrar kullanılabilir helper metotları hazırlandı.
- Gridlerde başlık, seçim, satır yüksekliği ve alternatif satır rengi standardize edildi.

## Faz 2.2 - Öğrenci Ön Kayıt
- Ön kayıt ekranı başlık + komut şeridi + form kartı + liste grid düzenine taşındı.
- Eski büyük butonlar görünür ekrandan kaldırıldı.
- İşlemler yeni kullanım yapısına taşındı:
  - `Ctrl + S`: ön kayıt ekle
  - `Ctrl + Enter`: kesin kayıt yap
  - `Delete`: seçili ön kaydı sil
  - Sağ tık menüsü: ekle / kesin kayıt / sil / yenile
- Form alanları ekran genişliğine göre sarılacak şekilde flow düzene alındı.

## Faz 2.3 - Öğrenci Yönetimi
- Öğrenci listesi ve sınıf yönetimi tek kokpit yapısına taşındı.
- Liste üstüne modern arama alanı eklendi.
- Sınıf işlemlerindeki eski butonlar kaldırıldı; komut şeridi ve sağ tık menüsü üzerinden kullanılacak hale getirildi.
- Öğrenci ve sınıf gridleri responsive kart içine alındı.

## Faz 2.4 - Öğrenci Kartı
- `OgrenciForm` sıfırdan modern kart yapısına geçirildi.
- Fotoğraf, öğrenci bilgileri, baba bilgileri, anne bilgileri ve ödeme durumları ayrı responsive kartlara ayrıldı.
- Eski Kaydet / Güncelle / Sil butonları görünür alandan kaldırıldı.
- İşlem kısayolları eklendi:
  - `Ctrl + S`: kaydet
  - `Ctrl + U`: güncelle
  - `Delete`: sil
  - Sağ tık menüsü: kaydet / güncelle / sil

## Faz 2.5 - Personel Yönetimi ve Personel Kartı
- Personel listesi başlık + arama + grid kokpitine taşındı.
- `PersonelForm` kimlik, iletişim, çalışma, eğitim, maaş/yan haklar ve ayrılış bilgileri olarak bölümlendirildi.
- Eski Kaydet / Güncelle / Sil / Temizle butonları görünür alandan kaldırıldı.
- İşlem kısayolları eklendi:
  - `Ctrl + S`: kaydet
  - `Ctrl + U`: güncelle
  - `Delete`: sil
  - Sağ tık menüsü: kaydet / güncelle / sil / temizle

## Faz 2.6 - Öğrenci Ödeme Yönetimi
- Ödeme ekranı finans kokpiti düzenine geçirildi.
- Eski ödeme giriş butonu görünür alandan kaldırıldı.
- Ödeme girişi komut şeridine taşındı.
- Grid görünümü ve sağ tık kullanımı modernize edildi.

## Faz 2.7 - Gelir-Gider Yönetimi
- Gelir-gider ekranı başlık, komut şeridi, işlem formu ve grid şeklinde yeniden düzenlendi.
- Eski Ekle ve Fatura Merkezi butonları görünür alandan kaldırıldı.
- İşlem kısayolları eklendi:
  - `Ctrl + S`: gelir/gider kaydet
  - `Ctrl + F`: fatura merkezi

## Faz 2.8 - Fatura Merkezi
- Fatura ekranı tamamen modern responsive düzene taşındı.
- Fatura bilgileri, fatura kalemleri ve geçmiş faturalar ayrı kartlara ayrıldı.
- Eski kaydet butonu görünür alandan kaldırıldı.
- `Ctrl + S` ve sağ tık ile kaydet/PDF oluştur işlemi eklendi.
- Fatura kalemleri ve geçmiş faturalar gridleri modernleştirildi.

## Sonraki Faz Önerileri
1. Tüm modüller için toast/inline validasyon sistemi eklenmesi.
2. Gelir-gider ekranına toplam gelir, toplam gider, net bakiye kartları eklenmesi.
3. Fatura merkezine fatura arama, tarih aralığı ve VKN filtresi eklenmesi.
4. Personel ve öğrenci kartlarına zorunlu alan validasyon göstergeleri eklenmesi.
5. Tüm gridlere Excel dışa aktar, kolon görünürlüğü ve kaydedilmiş filtre profilleri eklenmesi.
6. Eski `MessageBox` uyarılarının modern bilgi paneline taşınması.
