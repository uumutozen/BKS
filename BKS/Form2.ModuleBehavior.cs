namespace BKS;
public partial class Form2
{
    private void InitializeModuleBehavior()
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
        DesignerListBinding.Attach(dataGridViewStok, txtOgrenciYonetimiAra, listdataGridViewStokClear, listdataGridViewStokColumns, listdataGridViewStokCount);
        DesignerListBinding.Attach(dgvPersonelYonetimi, _personnelSearch, listdgvPersonelYonetimiClear, listdgvPersonelYonetimiColumns, listdgvPersonelYonetimiCount);
        DesignerListBinding.Attach(dgvOnKayitlar, txtPreRegistrationSearch, listdgvOnKayitlarClear, listdgvOnKayitlarColumns, listdgvOnKayitlarCount);
        DesignerListBinding.Attach(DgvOgrenciYonetimiSiniflar, txtClassesSearch, listDgvOgrenciYonetimiSiniflarClear, listDgvOgrenciYonetimiSiniflarColumns, listDgvOgrenciYonetimiSiniflarCount);
        DesignerListBinding.Attach(dataOgrVw, txtPaymentsSearch, listdataOgrVwClear, listdataOgrVwColumns, listdataOgrVwCount);
        DesignerListBinding.Attach(dataGridOdeme, txtFinanceSearch, listdataGridOdemeClear, listdataGridOdemeColumns, listdataGridOdemeCount);
        DesignerListBinding.Attach(salesGrid, txtReportsSearch, listsalesGridClear, listsalesGridColumns, listsalesGridCount);
        dataGridViewStok.Tag = StudentModuleTag;
        dgvPersonelYonetimi.Tag = PersonelModuleTag;
        dataOgrVw.Tag = 0;
        dataOgrVw.ContextMenuStrip = null;
        contextMenuStrip1.Items.Clear();
        // All student/personnel actions now come from the same Ribbon command definitions.
        dataGridViewStok.ContextMenuStrip = contextMenuStrip1;
        dgvPersonelYonetimi.ContextMenuStrip = contextMenuStrip1;
        DgvOgrenciYonetimiSiniflar.ContextMenuStrip = null;
        dgvOnKayitlar.ContextMenuStrip = null;
    }
}
