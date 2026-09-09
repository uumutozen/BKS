namespace BKS;

/// <summary>Veri bağlantısı kurmadan gömülü kartların ömrünü ve minimum boyutunu denetler.</summary>
internal static class DocumentDiagnostics
{
    public static void Run(Action<bool, string> check)
    {
        using var host = new Form { ClientSize = new Size(520, 360) };
        var tabs = new TabControl();
        var workspace = new DocumentWorkspace(tabs);
        host.Controls.Add(workspace);
        host.Show();
        var home = new TabPage { Name = "home" };
        workspace.OpenPage("home", "Ana sayfa", home);
        int created = 0;
        Form Create()
        {
            created++;
            var form = new Form();
            Screens.Install(form, new TextBox { Text = "Başlangıç" }, Screens.Ribbon("Kart"), "Test kartı");
            return form;
        }
        var first = workspace.OpenDocument("record:1", "Kayıt", Create);
        Application.DoEvents();
        var repeated = workspace.OpenDocument("RECORD:1", "Kayıt", Create);
        check(ReferenceEquals(first, repeated) && created == 1 && tabs.TabCount == 2, "Document: repeated opening activates one editor");
        check(!first.TopLevel && first.MinimumSize.IsEmpty && first.Width <= tabs.SelectedTab!.ClientSize.Width,
            "Document: shown embedded forms fit below a 600px host without minimum-size overflow");
        bool cancel = true;
        first.FormClosing += (_, e) => e.Cancel = cancel;
        check(!workspace.CloseDocument("record:1") && !first.IsDisposed, "Document: cancel preserves the tab and editor");
        cancel = false;
        check(workspace.CloseDocument("record:1") && first.IsDisposed && tabs.TabCount == 1, "Document: close releases the form and tab once");
        var fresh = workspace.OpenDocument("record:1", "Kayıt", Create);
        check(!ReferenceEquals(first, fresh) && created == 2, "Document: closed records reopen with a fresh editor");
        check(workspace.CloseAll(), "Document: close-all completes without recursive disposal");
        host.Hide();
    }
}
