using System.Text;
namespace BKS;
/// <summary>Windows-only regression runner. No command clicks, SQL reads or API requests.</summary>
internal static class LayoutDiagnostics
{
    public static int Run(string directory)
    {
        AppConfiguration.DesignPreview = true;
        Directory.CreateDirectory(directory);
        var log = new StringBuilder();
        int checks = 0;
        void Check(bool ok, string name)
        {
            if (!ok) throw new InvalidOperationException(name);
            checks++;
        }
        IEnumerable<Control> Walk(Control root)
        {
            yield return root;
            foreach (Control child in root.Controls)
            foreach (var control in Walk(child)) yield return control;
        }
        void Verify(Form form)
        {
            form.PerformLayout();
            Application.DoEvents();
            foreach (var workspace in Walk(form).OfType<RibbonWorkspace>())
            {
                var controls = workspace.Controls.Cast<Control>().Where(c => c.Visible).ToArray();
                for (int i = 0; i<controls.Length; i++)
                for (int j = i + 1; j<controls.Length; j++)
                Check(!controls[i].Bounds.IntersectsWith(controls[j].Bounds), form.Text + ": ribbon/content/footer overlap");
            }
            foreach (var fields in Walk(form).OfType<ResponsiveFields>().Where(c => c.Visible))
            {
                foreach (var card in fields.Controls.OfType<TableLayoutPanel>())
                {
                    var label = card.GetControlFromPosition(0, 0)!;
                    var input = card.GetControlFromPosition(0, 1)!;
                    Check(input.Dock == DockStyle.Fill, form.Text + ": input docking lost");
                    Check(label.Bottom <= input.Top, form.Text + ": label/input overlap");
                    Check(input.Width> 30 && input.Height> 12, form.Text + ": collapsed field");
                    Check(input.Left >= 0 && input.Right <= card.ClientSize.Width, form.Text + ": field outside card");
                }
            }
            foreach (var table in Walk(form).OfType<TableLayoutPanel>().Where(c => c.Visible))
            {
                var children = table.Controls.Cast<Control>().Where(c => c.Visible).ToArray();
                for (int i = 0; i < children.Length; i++)
                for (int j = i + 1; j < children.Length; j++)
                    Check(!children[i].Bounds.IntersectsWith(children[j].Bounds), form.Text + ": Designer table controls overlap: " + children[i].Name + " / " + children[j].Name);
            }
            foreach (var ribbon in Walk(form).OfType<BksRibbon>().Where(c => c.Visible))
            {
                Check(ribbon.IsExpanded && ribbon.Height >= 115 * ribbon.DeviceDpi / 96F && ribbon.Height <= 135 * ribbon.DeviceDpi / 96F,
                form.Text + ": grouped ribbon visible");
                Check(!Walk(ribbon).OfType<ScrollableControl>().Any(c => c.HorizontalScroll.Visible), form.Text + ": ribbon horizontal scrollbar");
            }
            foreach (var list in Walk(form).OfType<ListSurface>().Where(control => control.Visible))
            {
                var children = list.Controls.Cast<Control>().Where(control => control.Visible).ToArray();
                for (int i = 0; i < children.Length; i++)
                for (int j = i + 1; j < children.Length; j++)
                    Check(!children[i].Bounds.IntersectsWith(children[j].Bounds), form.Text + ": list search/header/grid overlap");
                foreach (var child in children)
                    Check(child.Left >= 0 && child.Right <= list.ClientSize.Width && child.Top >= 0 && child.Bottom <= list.ClientSize.Height,
                        form.Text + ": list toolbar outside bounds");
            }
            foreach (var panel in Walk(form).OfType<LoginInputPanel>())
            foreach (var input in panel.Controls.OfType<TextBox>())
            Check(input.Width> 30 && input.Right <= panel.ClientSize.Width, "Login input clipped");
        }
        void Exercise(Form form, string name)
        {
            form.Show();
            form.WindowState = FormWindowState.Normal;
            foreach (var size in new[]
            {
                new Size(600, 440),
                new Size(900, 660),
                new Size(1366, 768),
                new Size(1920, 1080),
                new Size(2560, 1440)
            })
            {
                form.Size = size;
                Verify(form);
                foreach (var tabs in Walk(form).OfType<TabControl>().ToArray())
                for (int i = 0; i<tabs.TabCount; i++)
                {
                    tabs.SelectedIndex = i;
                    Verify(form);
                }
                foreach (var sections in Walk(form).OfType<SectionedForm>().ToArray())
                foreach (var title in sections.Titles.ToArray())
                {
                    sections.SelectSection(title);
                    Verify(form);
                }
                foreach (var fields in Walk(form).OfType<ResponsiveFields>().Where(c => c.Visible).ToArray())
                {
                    fields.AutoScrollPosition = new Point(0, fields.AutoScrollMinSize.Height);
                    Verify(form);
                    fields.AutoScrollPosition = Point.Empty;
                }
            }
            log.AppendLine($"PASS {name}: resize, tabs, navigation, scroll; DPI={form.DeviceDpi}");
            form.Hide();
        }
        try
        {
            PhotoDiagnostics.Run(Check);
            DocumentDiagnostics.Run(Check);
            using (var login = new Form1()) Exercise(login, "Yeni giriş ekranı");
            using var main = new Form2();
            Exercise(main, "Ana pencere / tüm modüller");
            main.VerifyNavigationContract(Check);
            using (var form = new ConnectionSettingsForm()) Exercise(form, "Bağlantı ayarları");
            using (var form = new OgrenciForm(main)) Exercise(form, "Öğrenci");
            using (var form = new PersonelForm(main)) Exercise(form, "Personel");
            using (var form = new FormFatura()) Exercise(form, "Fatura");
            using (var form = new arsivForm(Guid.Empty, "", Guid.Empty)) Exercise(form, "Arşiv");
            using (var form = new OzelRapor()) Exercise(form, "Rapor tasarımı");
            using (var form = new RaporCalistirForm(0)) Exercise(form, "Rapor çalıştırma");
            using (var form = new PaymentDetailsForm()) Exercise(form, "Ödeme detayları");
            using (var form = new PaymentPlanForm()) Exercise(form, "Aylık ödeme planı");
            using (var form = new DataListForm()) Exercise(form, "İşlem geçmişi");
            log.AppendLine($"PASS: {checks} layout assertions. Repeat at Windows scaling 100%, 125%, 150%, 200%.");
            File.WriteAllText(Path.Combine(directory, "Windows_Yerlesim_Sonucu.txt"), log.ToString());
            return 0;
        }
        catch (Exception ex)
        {
            log.AppendLine("FAIL: " + ex);
            File.WriteAllText(Path.Combine(directory, "Windows_Yerlesim_Sonucu.txt"), log.ToString());
            return 1;
        }
    }
}
