namespace BKS;

internal sealed class FormDraft
{
    private readonly Form form;
    private readonly Func<string> snapshot;
    private readonly Func<Task<bool>> save;
    private string baseline = "";
    private bool confirming;
    private bool closeAllowed;
    public FormDraft(Form form, Func<string> snapshot, Func<Task<bool>> save)
    {
        this.form = form; this.snapshot = snapshot; this.save = save;
        Accept();
        form.FormClosing += Closing;
    }
    public void Accept() => baseline = snapshot();
    public async Task<bool> ConfirmAsync()
    {
        if (snapshot() == baseline) return true;
        var result = MessageBox.Show("Değişiklikler kaydedilsin mi?", form.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        return result == DialogResult.No || result == DialogResult.Yes && await save();
    }
    private async void Closing(object? sender, FormClosingEventArgs e)
    {
        if (e.Cancel || closeAllowed || snapshot() == baseline) return;
        e.Cancel = true;
        if (confirming) return;
        confirming = true;
        try
        {
            if (await ConfirmAsync() && !form.IsDisposed)
            {
                closeAllowed = true;
                form.BeginInvoke(new Action(form.Close));
            }
        }
        finally { confirming = false; }
    }
}
