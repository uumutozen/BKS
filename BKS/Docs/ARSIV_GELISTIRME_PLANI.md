# Arşiv Modülü Faz Faz Geliştirme Planı

## Faz 1 - Uygulanan temel yenileme
- Arşiv formu sabit ölçüden çıkarıldı, `TableLayoutPanel` tabanlı responsive yapıya alındı.
- Başlık, özet kartları, yükleme alanı, filtre alanı, modern grid ve aksiyon barı sıfırdan tasarlandı.
- Dosya adı/uzantı/tür arama filtreleri eklendi.
- Tarih aralığı filtresi eklendi.
- Dosya türü sınıflandırması eklendi: PDF, Word, Excel, Resim, Sunum, Sıkıştırılmış, Diğer.
- Sürükle-bırak ile dosya seçme eklendi.
- Dosya indirme için farklı kaydet ve masaüstüne hızlı indir seçenekleri eklendi.
- Sağ tık menüsü eklendi: indir, masaüstüne indir, dosya adını kopyala, yenile.
- İşlem durum çubuğu ve yükleniyor göstergesi eklendi.
- Seçili dosya ve son eklenme bilgisi kartlara taşındı.

## Faz 2 - API ve veri modeli geliştirmeleri
- API tarafına dosya silme endpoint'i eklenmeli: `DELETE /api/dosya-arsiv/{id}`.
- API tarafına dosya meta güncelleme endpoint'i eklenmeli: kategori, açıklama, belge tipi, son geçerlilik tarihi.
- Dosya boyutu ve MIME type API response içine eklenmeli.
- Dosya yüklemede maksimum boyut, izinli uzantı ve kullanıcı yetki kontrolü server tarafında yapılmalı.
- Öğrenci/personel bazlı arşiv erişim logu tutulmalı.

## Faz 3 - Kurumsal arşiv yönetimi
- Belge kategorileri: kimlik, sözleşme, sağlık belgesi, veli izin formu, fatura, diğer.
- Süresi dolacak belgeler için uyarı sistemi.
- Dosya versiyonlama: aynı belge yenilendiğinde eski versiyonu saklama.
- Toplu indirme ve seçili belgeleri zip olarak alma.
- Toplu dosya yükleme kuyruğu.

## Faz 4 - Arama ve raporlama
- API tarafında sayfalama, server-side arama ve hızlı indeksleme.
- Arşiv raporu: kişi bazlı dosya sayısı, eksik belge listesi, son yüklenenler.
- Excel/PDF rapor çıktısı.
- OCR entegrasyonu ile PDF/resim içeriğinden arama.

## Faz 5 - Güvenlik ve saklama politikası
- Virüs tarama entegrasyonu.
- Dosya şifreleme ve güvenli storage provider desteği.
- Saklama süresi politikası: otomatik arşivleme/silme önerisi.
- Hassas belge erişimi için rol bazlı izin ve denetim kaydı.
- Yedekleme ve geri yükleme ekranı.
