# Ribbon tasarımı ve yeni sekme ekleme

Ana Ribbon özel bir `BksRibbon` kontrolüdür. Sekmeler ve düğmeler `Form2.Commands.cs` içindeki `RegisterRibbonCommands()` metodunda tanımlanır. Form Designer'da görünen belge sekmeleriyle Ribbon sekmeleri farklıdır. Yeni Ribbon sekmesini `tabControl.TabPages` içine eklemeyin.

## Örnek: Muhasebe sekmesi

`RegisterRibbonCommands()` içinde, `finance` ve `reports` değişkenlerinin tanımından sonra aşağıdaki örneği ekleyebilirsiniz:

```csharp
_ribbon.AddPage("accounting", "Muhasebe",
    ("Ekranlar", new[]
    {
        Command("accounting.invoices", "Faturalar", RibbonIcon.Print,
            RunInvoiceCenter, finance, size: RibbonButtonSize.Large),
        Navigate("accounting.reports", "Rapor listesi", reports)
    }));
```

`Form2.Session.cs` içindeki `ApplyAccess()` metodunda, `_ribbon.SetAllowed(ribbonKeys)` satırından önce sekmenin görünme koşulunu ekleyin:

```csharp
if (_allowedModules.Contains(tabPageGelirGider.Name) ||
    _allowedModules.Contains(tabPageOzelRaporlar.Name))
    ribbonKeys.Add("accounting");
```

Her sekme anahtarı ve `CommandId` benzersiz olmalı. Düğme başlığını değiştirmek yetki adını değiştirmez; `tabPageGelirGider` gibi mevcut API modül adlarını koruyun. Sekme görünürlüğü ve komutun `permission` değeri birlikte tanımlanır.

## Düğmeler ve görünüm

- `RibbonButtonSize.Large`: büyük ikonlu sütun; `Small`: üçlü düğme sütunu.
- `RibbonIcon`: mevcut simgeler (`Add`, `Edit`, `View`, `Print`, `Refresh`, `Folder` vb.).
- `enabled`: seçili kayıt, aktif ekran veya devam eden işlem koşulu.
- `shortcut`: `Keys.Control | Keys.S` gibi kısayol; aynı sekmede çakışan kısayol vermeyin.
- `tooltip`: düğmenin açıklaması.
- Sekmelerin sırası `AddPage` çağrılarının, grupların sırası verilen grup dizisinin sırasıdır.
- Renkler `UI/RibbonPalette.cs`, grup yerleşimi `UI/RibbonGroupView.cs`, düğme çizimi `UI/RibbonNavigationButton.cs` içindedir.
- Sağ tık komutları aynı Ribbon tanımlarından üretilir. Bir belgeyi yeni sekmeye eşlemek gerekiyorsa `BksRibbon.CommandsFor()` içindeki bağlam eşlemesini güncelleyin.

## Yeni bir form açma

Formu normal WinForms Form olarak oluşturun. Kontroller, alanlar, `InitializeComponent`, Dock/Anchor ve sabit olay bağlantıları `.Designer.cs` içinde kalmalı. Constructor yalnızca kontrolleri ve davranışları hazırlamalı; SQL işlemlerini constructor içinde çalıştırmayın. Liste yüklemeyi gösterimden sonra `FormOperation` ile başlatın.

Yeni formu `_documents.OpenDocument("benzersiz-belge-anahtari", "Başlık", () => new FormSinifiniz(), izin)` üzerinden açın. Aynı anahtar aynı açık belgeye döner. `OpenDocument` içine bir form örneği yerine factory verin; kullanıcı sekmeyi yeniden açınca gereksiz ikinci form yaratılmasın.

Ribbon sekmesine tıklamak içerik ekranını değiştirmez. Ekran açmak için `Navigate` veya `OpenDocument` kullanan açık bir düğme ekleyin. Kayıt formlarında ikinci bir Ribbon oluşturmayın; o formun komutları Designer araç çubuğunda kalır.

## Kontrol

`dotnet build BKS.sln` ile derleyin. `dotnet run --project BKS -- --layout-checks Kontrol_Sonuclari` canlı veriye bağlanmadan formları, sekme geçişlerini ve yerleşimleri kontrol eder. Farklı Windows ölçeklerinde ayrıca gözden geçirin.

Bebek mavisi tema için ana arka plan `228,241,252`, içerik `239,247,253`, başlıklar `218,236,250` kullanılır. Designer dosyalarında da aynı renklerin sayısal karşılıkları tutulur; böylece tasarım görünümü çalışma zamanıyla uyumludur.
