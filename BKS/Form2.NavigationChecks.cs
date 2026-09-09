namespace BKS;

public partial class Form2
{
    // Runs only in the existing Windows --layout-checks mode; no database or API calls.
    internal void VerifyNavigationContract(Action<bool, string> check)
    {
        if (!AppConfiguration.DesignPreview) throw new InvalidOperationException("Önizleme modu gerekli.");
        _documents.OpenPage(tabPageStok.Name, "Öğrenciler", tabPageStok);
        _ribbon.SelectPage("finance");
        check(_documents.ActiveKey == tabPageStok.Name, "Ribbon: finance tab preserves the student list");
        _documents.OpenPage(tabPagePersonelYonetimi.Name, "Personel", tabPagePersonelYonetimi);
        check(_ribbon.SelectedPageKey == "finance", "Document: opening personnel preserves selected ribbon page");
        _documents.Activate(tabPageStok.Name);
        check(_ribbon.SelectedPageKey == "finance", "Document: tab activation preserves selected ribbon page");
        var before = tabControl.TabCount;
        _ribbon.SelectPage(tabPageStok.Name);
        var list = _ribbon.ActiveGroups().SelectMany(group => group.Commands).Single(command => command.CommandId == "students.list");
        list.Invoke(); list.Invoke();
        check(_documents.ActiveKey == tabPageStok.Name && tabControl.TabCount == before,
            "Navigation: repeated student-list command activates a single document");
        _documents.OpenPage(tabPageGelirGider.Name, "Gelir / Gider", tabPageGelirGider);
        before = tabControl.TabCount;
        foreach (var key in new[] { "home", tabPageStok.Name, "classes", "finance", tabPagePersonelYonetimi.Name, "system" })
            _ribbon.SelectPage(key);
        check(tabControl.TabCount == before && _documents.ActiveKey == tabPageGelirGider.Name,
            "Ribbon: browsing all tabs preserves open documents and active content");
        var record = _documents.OpenDocument("checks:student", "Öğrenci kartı", () => new OgrenciForm(this));
        record.txtOgrenciAd.Text = "Kaydedilmemiş öğrenci";
        var root = record.Controls[0];
        _ribbon.SelectPage("finance");
        _documents.Activate(tabPagePersonelYonetimi.Name);
        _documents.Activate("checks:student");
        check(_ribbon.SelectedPageKey == "finance" && ReferenceEquals(record.Controls[0], root)
            && record.txtOgrenciAd.Text == "Kaydedilmemiş öğrenci",
            "Record: switching ribbon/documents preserves Designer root and unsaved values");
        check(!WalkRecord(record).OfType<BksRibbon>().Any(), "Record: no second ribbon in the embedded student form");
        _ribbon.SetExpanded(false); _ribbon.SetExpanded(true);
        check(_ribbon.SelectedPageKey == "finance" && _documents.ActiveKey == "checks:student",
            "Ribbon: collapse and expand preserve both selections");
        record.txtOgrenciAd.Clear();
        _documents.CloseDocument("checks:student");
    }

    private static IEnumerable<Control> WalkRecord(Control root)
    {
        yield return root;
        foreach (Control child in root.Controls)
            foreach (var control in WalkRecord(child)) yield return control;
    }
}
