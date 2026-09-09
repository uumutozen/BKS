# BKS 1.4.0 UI mimarisi

## Ana pencere

`Form2.Designer.cs` modül TabPage'lerini ve tüm sabit alan/liste yerleşimlerini oluşturur.
`Form2.Ribbon.cs` ana Ribbon ile DocumentWorkspace'i shell'e bağlar.
Ribbon PageSelected yalnızca Ribbon seçimini kaydeder; modül açmaz.
ActiveDocumentChanged yalnızca aktif belge anahtarını, durum satırını ve komut etkinliğini yeniler.
Ana Ribbon'a belge Ribbon'ı enjekte eden API kaldırıldı.
`Form2.Commands.cs` navigasyon ve veri işlemi komutlarını ayrı tanımlar.
`Form2.ModuleBehavior.cs` mevcut Designer kontrollerinin olaylarını ve listelerin filtre davranışını bağlar.
`DocumentRegistry` belge anahtarına göre tek instance korur; `DocumentWorkspace` açma/kapatma ve yetkileri yönetir.
`DocumentFormHost` kayıt formlarını içeriklerini silmeden gömer.

## Kayıt formları

- `.Designer.cs`: standart kontrol alanları, InitializeComponent, Dock/Anchor, tablo satır/sütunları, sekmeler, statik grid kolonları, olay bağlantıları.
- Normal `.cs`, `.Runtime.cs`, `.Persistence.cs`, `.Validation.cs`, `.Photo.cs`: davranış, doğrulama, veri işlemleri, fotoğraf okuma ve düzenleme.
- `OgrenciForm.Layout.cs`, `PersonelForm.Layout.cs` ve `Form2.Layout.cs` kaldırıldı.
- `PhotoEditor` artık panel değildir; Designer'daki PictureBox, iki Button ve Label ile çalışan davranış sınıfıdır. Kontrol oluşturmaz/taşımaz.
- `PaymentDetailsForm`, `PaymentPlanForm`, `DataListForm`, `ConnectionSettingsForm` ayrı standart Designer formlarıdır.
- Kayıt formlarında yerel Ribbon yoktur. Komutlar Designer araç çubuğundadır.

## Listeler

`RibbonPalette` renkleri merkezileştirir. `GridAppearance` ortak grid ölçüleri, seçim, çizgiler, teknik kolonlar ve formatları uygular.
`DesignerListBinding` Designer'da bulunan arama, temizle, sütunlar ve sayaç kontrollerini bağlar; yerleşim oluşturmaz.
`GridFilterController` ve `GridColumnMenu` filtreleme, sıralama ve sütun seçimini sürdürür.
Arşiv kendi tür/tarih filtrelerini korur; ortak grid görünümünü ve sütun menüsünü kullanır.
Fatura kalemleri düzenlenebilir, ödeme detayları çoklu seçilebilir; liste stilinin uygulanması bu davranışları değiştirmez.

## Doğrulama

`Tests/` platformdan bağımsız iş kurallarını doğrular. `Form2.NavigationChecks.cs` Windows yerleşim kontrolüne eklenen Ribbon/belge senaryolarını içerir.
`UI/LayoutDiagnostics.cs` Windows'ta görünen TableLayoutPanel kontrollerinin çakışmalarını ve sekme geçişlerini de denetler.
