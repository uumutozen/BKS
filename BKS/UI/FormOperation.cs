namespace BKS;

/// <summary>Serializes a form's asynchronous operations and keeps failures inside the screen.</summary>
internal sealed class FormOperation
{
    private readonly Form form;
    public bool IsBusy { get; private set; }
    public FormOperation(Form form)
    {
        this.form = form;
        form.FormClosing += (_, e) => { if (IsBusy) e.Cancel = true; };
    }
    public async Task RunAsync(Func<Task> action)
    {
        if (IsBusy || form.IsDisposed || AppConfiguration.DesignPreview ||
            System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
        IsBusy = true;
        form.UseWaitCursor = true;
        form.Enabled = false;
        try { await action(); }
        catch (Exception ex) { if (!form.IsDisposed) UiActions.ShowError(ex); }
        finally
        {
            IsBusy = false;
            if (!form.IsDisposed) { form.Enabled = true; form.UseWaitCursor = false; }
        }
    }
}
