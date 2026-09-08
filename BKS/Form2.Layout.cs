namespace BKS;
public partial class Form2
{
    private void BuildModuleLayouts()
    {
        _allModulePages.AddRange(new[]
        {
            tabPageOgrenciOnKayit,
            tabPageStok,
            tabPageSatis,
            tabPagePersonelYonetimi,
            tabPageGelirGider,
            tabPageOzelRaporlar
        });
        foreach (var page in _allModulePages)
        {
            page.Controls.Clear();
            page.Padding = Padding.Empty;
            page.BackColor = ModernWinForms.PageBack;
        }
        tabPageOgrenciOnKayit.Controls.Add(Screens.WithEditor(new ResponsiveFields(("Öğrenci adı", txtOnKayitAd), ("Soyadı", txtOnKayitSoyad),
        ("Doğum tarihi", dtpOnKayitDogumTarihi), ("Baba adı", txtOnKayitBabaAd), ("Veli telefonu", txtOnKayitVeliTel),
        ("Notlar", txtOnKayitNot)), dgvOnKayitlar));
        var search = new ResponsiveFields(("Öğrenci ara", txtOgrenciYonetimiAra));
        txtOgrenciYonetimiAra.TextChanged += (_, _) => ModernWinForms.ApplySearchFilter(dataGridViewStok, txtOgrenciYonetimiAra.Text);
        var studentArea = Screens.WithEditor(search, dataGridViewStok, .15F);
        var classes = Screens.WithEditor(new ResponsiveFields(("Sınıf adı", txtOgrenciYonetimiSınıfAdı), ("Yaş grubu", cbxOgrenciYonetimiYasGrubu),
        ("Öğretmen", cbxOgrenciYonetimiOgretmen)), DgvOgrenciYonetimiSiniflar, .50F);
        tabPageStok.Controls.Add(studentArea);
        _classesPage.Controls.Add(classes);
        numericQuantitySold.DecimalPlaces = 2;
        numericQuantitySold.Maximum = 1000000000M;
        numericQuantitySold.Minimum = 0;
        tabPageSatis.Controls.Add(Screens.WithEditor(new ResponsiveFields(("Öğrenci", comboBoxStok), ("Ödeme tutarı (₺)", numericQuantitySold)),
        dataOgrVw, .22F));
        _personnelSearch = new TextBox
        {
            PlaceholderText = "Personel ara"
        };
        _personnelSearch.TextChanged += (_, _) => ModernWinForms.ApplySearchFilter(dgvPersonelYonetimi, _personnelSearch.Text);
        tabPagePersonelYonetimi.Controls.Add(Screens.WithEditor(new ResponsiveFields(("Personel ara", _personnelSearch)),
        dgvPersonelYonetimi, .15F));
        numericAmount.DecimalPlaces = 2;
        numericAmount.Maximum = 1000000000M;
        var type = new FlowLayoutPanel();
        type.Controls.Add(radioIncome);
        type.Controls.Add(radioExpense);
        radioIncome.AutoSize = true;
        radioExpense.AutoSize = true;
        tabPageGelirGider.Controls.Add(Screens.WithEditor(new ResponsiveFields(("Açıklama", txtDescription), ("Tutar (₺)", numericAmount),
        ("İşlem türü", type)), dataGridOdeme, .25F));
        tabPageOzelRaporlar.Controls.Add(Screens.Grid(salesGrid));
        foreach (var grid in new[]
        {
            dataGridViewStok,
            dgvPersonelYonetimi,
            dataOgrVw,
            DgvOgrenciYonetimiSiniflar,
            dgvOnKayitlar
        })
        {
            grid.MouseDown += DataGridView_MouseDown;
            grid.SelectionChanged += (_, _) => _ribbon?.RefreshCommands();
        }
        dataGridViewStok.Tag = StudentModuleTag;
        dgvPersonelYonetimi.Tag = PersonelModuleTag;
        dataOgrVw.Tag = 0;
        dataOgrVw.ContextMenuStrip = null;
        contextMenuStrip1.Items.Clear();
        contextMenuStrip1.Items.AddRange(new ToolStripItem[]
        {
            ödemeDetaylarıToolStripMenuItem,
            geçmişHareketToolStripMenuItem
        });
        dataGridViewStok.ContextMenuStrip = contextMenuStrip1;
        dgvPersonelYonetimi.ContextMenuStrip = contextMenuStrip1;
        DgvOgrenciYonetimiSiniflar.ContextMenuStrip = null;
        dgvOnKayitlar.ContextMenuStrip = null;
    }
}
