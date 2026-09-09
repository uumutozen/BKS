namespace BKS;

/// <summary>Her kayıt formuna aynı gömülü pencere kurallarını uygular.</summary>
internal static class DocumentFormHost
{
    public static void Attach(Form form, TabPage page)
    {
        form.SuspendLayout();
        form.TopLevel = false;
        form.AutoSize = false;
        form.WindowState = FormWindowState.Normal;
        form.FormBorderStyle = FormBorderStyle.None;
        form.StartPosition = FormStartPosition.Manual;
        form.MinimumSize = Size.Empty;
        form.MaximumSize = Size.Empty;
        form.Margin = Padding.Empty;
        form.Padding = Padding.Empty;
        form.Dock = DockStyle.Fill;
        form.Controls.OfType<RibbonWorkspace>().FirstOrDefault()?.Embed();
        page.Controls.Add(form);
        form.ResumeLayout(true);
    }

    public static BksRibbon? RibbonFor(Form? form) =>
        form?.Controls.OfType<RibbonWorkspace>().FirstOrDefault()?.Ribbon;
}
