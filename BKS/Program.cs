using System.Globalization;
namespace BKS;
internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("tr-TR");
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("tr-TR");
        if (args.Contains("--layout-checks"))
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            var index = Array.IndexOf(args, "--layout-checks");
            var directory = args.Length> index + 1 ? args[index + 1]: Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "HelmSoftware", "BKS", "LayoutChecks");
            Environment.ExitCode = LayoutDiagnostics.Run(directory);
            return;
        }
        Application.ThreadException += (_, e) => UiActions.ShowError(e.Exception);
        if (args.Contains("--login-preview"))
        {
            AppConfiguration.DesignPreview = true;
            Application.Run(new Form1());
            return;
        }
        if (args.Contains("--ui-preview"))
        {
            AppConfiguration.DesignPreview = true;
            Application.Run(new Form2());
            return;
        }
        try
        {
            Application.Run(new Form1());
        }
        catch (Exception ex)
        {
            UiActions.ShowError(ex);
        }
    }
}
