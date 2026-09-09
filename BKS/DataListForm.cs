using System.Data;

namespace BKS;

/// <summary>İşlem geçmişi gibi salt okunur tablolar için ortak belge ekranı.</summary>
public partial class DataListForm : Form
{
    private readonly Func<DataTable> read;
    private bool loading;

    public DataListForm() : this("İşlem geçmişi", () => new DataTable()) { }

    public DataListForm(string title, Func<DataTable> read)
    {
        this.read = read;
        InitializeComponent();
        Text = lblTitle.Text = pnlListTitle.Text = title;
        Screens.PrepareDesignerForm(this);
        DesignerListBinding.Attach(grid, txtSearch, pnlListClear, pnlListColumns, pnlListCount);
        Shown += async (_, _) => await ReloadAsync();
    }

    private async void Refresh_Click(object? sender, EventArgs e) => await ReloadAsync();
    private void Close_Click(object? sender, EventArgs e) => Close();
    private async Task ReloadAsync()
    {
        if (loading || AppConfiguration.DesignPreview || IsDisposed) return;
        loading = true;
        UseWaitCursor = true;
        btnRefresh.Enabled = !loading;
        try
        {
            var table = await Task.Run(read);
            if (!IsDisposed) grid.DataSource = table;
        }
        catch (Exception)
        {
            if (!IsDisposed) MessageBox.Show("Liste yüklenemedi. Bağlantınızı kontrol edip Yenile'yi kullanın.",
                Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        finally
        {
            loading = false;
            if (!IsDisposed) { UseWaitCursor = false; btnRefresh.Enabled = !loading; }
        }
    }
}
