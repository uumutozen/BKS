namespace BKS;

public partial class Form2
{
    // API permission names stay unchanged; captions can be changed without touching access rules.

    private RibbonCommand Command(string id, string caption, RibbonIcon icon, Action execute,
    string? permission = null, Func<bool>? enabled = null, Keys shortcut = Keys.None,
    RibbonButtonSize size = RibbonButtonSize.Small, string? tooltip = null)
    {
        return new RibbonCommand(caption, icon, execute,
        () => !AppConfiguration.DesignPreview && !_sessionLoading &&
        (permission == null || (_allowedModules.Contains(permission) && !_loadingModules.Contains(permission))) && (enabled?.Invoke() ?? true))
        {
            CommandId = id,
            Permission = permission,
            Shortcut = shortcut,
            Size = size,
            Tooltip = tooltip ?? caption
        };
    }

    private RibbonCommand Navigate(string id, string caption, string module) => new(
    caption, RibbonIcon.Folder, () => SelectModuleByKey(module), () => _allowedModules.Contains(module))
    {
        CommandId = id,
        Permission = module,
        Tooltip = caption + " ekranını açar.",
        Size = RibbonButtonSize.Large
    };

    private void RegisterRibbonCommands()
    {
        string students = tabPageStok.Name;
        string personnel = tabPagePersonelYonetimi.Name;
        string preregistration = tabPageOgrenciOnKayit.Name;
        string payments = tabPageSatis.Name;
        string finance = tabPageGelirGider.Name;
        string reports = tabPageOzelRaporlar.Name;
        bool OnPage(string key) => tabControl.SelectedTab?.Name == key;
        _ribbon.AddPage("home", "Ana Sayfa", ("Hızlı erişim", new[]
        {
            Navigate("home.students", "Öğrenci listesi", students),
            Navigate("home.payments", "Tahsilatlar", payments),
            Navigate("home.personnel", "Personel listesi", personnel),
            Navigate("home.reports", "Raporlar", reports)
        }));
        _ribbon.AddPage(students, "Öğrenciler",
        ("Öğrenci", new[]
        {
            Navigate("students.list", "Öğrenci listesi", students),
            Command("students.new", "Yeni öğrenci", RibbonIcon.Add, () => NewRecord(dataGridViewStok), students,
            shortcut: Keys.Control | Keys.N, size: RibbonButtonSize.Large),
            Command("students.edit", "Düzenle", RibbonIcon.Edit, () => EditSelected(dataGridViewStok), students,
            () => OnPage(students) && dataGridViewStok.CurrentRow != null, Keys.Control | Keys.E),
            Command("students.archive", "Pasife al", RibbonIcon.Archive, () => DeleteSelected(dataGridViewStok), students,
            () => OnPage(students) && dataGridViewStok.CurrentRow != null),
            Command("students.files", "Evrak arşivi", RibbonIcon.Folder, () => ArchiveSelected(dataGridViewStok), students,
            () => OnPage(students) && dataGridViewStok.CurrentRow != null)
        }),
        ("Ön kayıt", new[]
        {
            Navigate("prereg.list", "Ön kayıt listesi", preregistration),
            Command("prereg.save", "Ön kayıt ekle", RibbonIcon.Add, RunPreRegistrationAdd, preregistration, () => OnPage(preregistration)),
            Command("prereg.confirm", "Kesin kayda çevir", RibbonIcon.Restore, RunPreRegistrationConfirm, preregistration,
            () => OnPage(preregistration) && _allowedModules.Contains(students) && dgvOnKayitlar.CurrentRow != null),
            Command("prereg.remove", "Pasife al", RibbonIcon.Archive, RunPreRegistrationDelete, preregistration,
            () => OnPage(preregistration) && dgvOnKayitlar.CurrentRow != null)
        }),
        ("Liste", new[]
        {
            Command("students.search", "Ara", RibbonIcon.Search, () =>
            {
                SelectModuleByKey(students);
                txtOgrenciYonetimiAra.Focus();
            }, students,
            shortcut: Keys.Control | Keys.F),
            Command("students.refresh", "Yenile", RibbonIcon.Refresh, ReloadCurrent, shortcut: Keys.F5),
            Command("students.import", "Excel içe al", RibbonIcon.Export, () => ImportSelected(dataGridViewStok), students)
        }));
        _ribbon.AddPage("classes", "Sınıf & Eğitim", ("Sınıflar", new[]
        {
            Command("classes.list", "Sınıf listesi", RibbonIcon.Folder, OpenClasses, students, size: RibbonButtonSize.Large),
            Command("classes.save", "Sınıf kaydet", RibbonIcon.Backup, RunClassSave, students, () => OnPage("classes"),
            Keys.Control | Keys.S, RibbonButtonSize.Large),
            Command("classes.edit", "Güncelle", RibbonIcon.Edit, RunClassUpdate, students,
            () => OnPage("classes") && DgvOgrenciYonetimiSiniflar.CurrentRow != null),
            Command("classes.remove", "Sınıf sil", RibbonIcon.Archive, RunClassDelete, students,
            () => OnPage("classes") && DgvOgrenciYonetimiSiniflar.CurrentRow != null),
            Command("classes.refresh", "Yenile", RibbonIcon.Refresh, ReloadCurrent, students, shortcut: Keys.F5)
        }));
        _ribbon.AddPage("finance", "Finans",
        ("Finans ekranları", new[]
        {
            Navigate("payments.list", "Tahsilatlar", payments),
            Navigate("finance.list", "Gelir / gider", finance),
            Command("invoices.open", "Faturalar", RibbonIcon.Print, RunInvoiceCenter, finance, size: RibbonButtonSize.Large)
        }),
        ("İşlemler", new[]
        {
            Command("payments.save", "Tahsilat kaydet", RibbonIcon.Backup, RunPaymentEntry, payments, () => OnPage(payments)),
            Command("finance.save", "Gelir / gider kaydet", RibbonIcon.Backup, RunIncomeExpenseSave, finance, () => OnPage(finance)),
            Command("finance.refresh", "Yenile", RibbonIcon.Refresh, ReloadCurrent, shortcut: Keys.F5)
        }));
        _ribbon.AddPage(personnel, "Personel",
        ("Personel", new[]
        {
            Navigate("personnel.list", "Personel listesi", personnel),
            Command("personnel.new", "Yeni personel", RibbonIcon.Add, () => NewRecord(dgvPersonelYonetimi), personnel,
            shortcut: Keys.Control | Keys.N, size: RibbonButtonSize.Large),
            Command("personnel.edit", "Düzenle", RibbonIcon.Edit, () => EditSelected(dgvPersonelYonetimi), personnel,
            () => dgvPersonelYonetimi.CurrentRow != null, Keys.Control | Keys.E),
            Command("personnel.remove", "Pasife al", RibbonIcon.Archive, () => DeleteSelected(dgvPersonelYonetimi), personnel,
            () => dgvPersonelYonetimi.CurrentRow != null),
            Command("personnel.files", "Evrak arşivi", RibbonIcon.Folder, () => ArchiveSelected(dgvPersonelYonetimi), personnel,
            () => dgvPersonelYonetimi.CurrentRow != null)
        }),
        ("Liste", new[]
        {
            Command("personnel.search", "Ara", RibbonIcon.Search, () => _personnelSearch.Focus(), personnel, shortcut: Keys.Control | Keys.F),
            Command("personnel.refresh", "Yenile", RibbonIcon.Refresh, ReloadCurrent, personnel, shortcut: Keys.F5),
            Command("personnel.import", "Excel içe al", RibbonIcon.Export, () => ImportSelected(dgvPersonelYonetimi), personnel)
        }));
        _ribbon.AddPage(reports, "Raporlar", ("Raporlar", new[]
        {
            Navigate("reports.list", "Rapor listesi", reports),
            Command("reports.run", "Rapor çalıştır", RibbonIcon.View,
            () => salesGrid_CellDoubleClick(salesGrid, new DataGridViewCellEventArgs(0, salesGrid.CurrentRow.Index)),
            reports, () => salesGrid.CurrentRow != null, size: RibbonButtonSize.Large),
            Command("reports.design", "Rapor tasarla", RibbonIcon.Edit,
            () => _documents.OpenDocument("report:designer", "Rapor tasarımı", () => new OzelRapor(), reports),
            reports, () => ModuleAccess.IsAdmin(Role)),
            Command("reports.refresh", "Yenile", RibbonIcon.Refresh, ReloadCurrent, reports, shortcut: Keys.F5)
        }));
        _ribbon.AddPage("system", "Ayarlar", ("Oturum", new[]
        {
            Command("session.refresh", "Yetkileri yenile", RibbonIcon.Refresh, () => _ = InitializeSessionAsync(), size: RibbonButtonSize.Large),
            new RibbonCommand("Bağlantı bilgisi", RibbonIcon.Help, () => MessageBox.Show(
            "Bağlantı ayarlarını giriş ekranından değiştirebilirsiniz.\nBKS 1.2.0\nCtrl+F1: Ribbon daralt / genişlet")),
            new RibbonCommand("Çıkış", RibbonIcon.Archive, CloseApplication)
        }));
    }
}
