using System.Data;
namespace BKS;
public partial class Form2
{
    private void PrepareDesignPreview()
    {
        ApplyAccess(_allModulePages.Select(p => p.Name), "DEMO");
        foreach (var grid in new[]
        {
            dataGridViewStok,
            dgvPersonelYonetimi,
            dgvOnKayitlar,
            dataOgrVw,
            dataGridOdeme,
            salesGrid
        })
        {
            var dt = new DataTable();
            dt.Columns.Add("Kayıt");
            dt.Columns.Add("Açıklama");
            dt.Rows.Add("Tasarım önizlemesi", "Canlı verilere bağlanılmaz; kayıt işlemleri kapalıdır.");
            grid.DataSource = dt;
        }
        foreach (var page in _allModulePages) _loadedModules.Add(page.Name);
        foreach (var page in _allModulePages)
        _documents.OpenPage(page.Name, _titles[page.Name], page, page.Name);
        _documents.OpenPage("classes", "Sınıflar", _classesPage, tabPageStok.Name);
        _documents.Activate("home");
        SetRibbonStatus("TASARIM ÖNİZLEMESİ • Canlı bağlantı ve kayıt işlemleri kapalı");
    }
}
