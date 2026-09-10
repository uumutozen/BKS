using System.Data;
using System.Diagnostics;
using System.Text.Json;
namespace BKS;
public partial class FormFatura : Form
{
    public Guid UserId { get; set; }
    private readonly FormOperation operation;
    private readonly FormDraft draft;
    public FormFatura()
    {
        InitializeComponent();
        operation = new FormOperation(this);
        Screens.PrepareDesignerForm(this);
        GridAppearance.Apply(dgKalemler);
        DesignerListBinding.Attach(dgFaturalar, txtInvoiceSearch, pnlInvoiceHistoryClear, pnlInvoiceHistoryColumns, pnlInvoiceHistoryCount);
        txtFaturaNo.Text = "BKS";
        draft = new FormDraft(this, Snapshot, SaveAsync);
        dgKalemler.DefaultValuesNeeded += (_, e) =>
        {
            e.Row.Cells["Miktar"].Value = 1M; e.Row.Cells["BirimFiyat"].Value = 0M; e.Row.Cells["KDV"].Value = 20M;
        };
        dgKalemler.DataError += (_, e) =>
        {
            e.ThrowException = false;
            if (e.RowIndex >= 0 && e.RowIndex < dgKalemler.Rows.Count) dgKalemler.Rows[e.RowIndex].ErrorText = "Geçerli bir sayı girin (örnek: 1250,50).";
        };
        dgKalemler.CellValueChanged += (_, _) => UpdateTotals();
        dgKalemler.RowsRemoved += (_, _) => UpdateTotals();
        KeyDown += (_, e) =>
        {
            if (e.Control && e.KeyCode == Keys.S) { e.SuppressKeyPress = true; btnKaydet_Click(this, e); }
        };
    }
    private string Snapshot() => JsonSerializer.Serialize(new {
        Prefix = txtFaturaNo.Text, Recipient = txtAliciUnvan.Text, Tax = txtAliciVkn.Text, Date = dtTarih.Value.Date,
        Lines = dgKalemler.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow)
            .Select(r => r.Cells.Cast<DataGridViewCell>().Select(c => Convert.ToString(c.Value)).ToArray()).ToArray()
    });
    private InvoiceLine ReadLine(DataGridViewRow row) => new(Convert.ToString(row.Cells["UrunAdi"].Value)?.Trim() ?? "",
        DataValues.Money(row.Cells["Miktar"].Value), DataValues.Money(row.Cells["BirimFiyat"].Value), DataValues.Money(row.Cells["KDV"].Value));
    private List<InvoiceLine> ReadLines()
    {
        if (!dgKalemler.EndEdit()) throw new InvalidOperationException("Fatura kalemlerindeki sayısal değerleri düzeltin.");
        var lines = new List<InvoiceLine>();
        foreach (DataGridViewRow row in dgKalemler.Rows)
        {
            if (row.IsNewRow) continue;
            try { var line = ReadLine(row); line.Validate(); row.ErrorText = ""; lines.Add(line); }
            catch (Exception ex) when (ex is InvalidOperationException or FormatException or OverflowException)
            { row.ErrorText = ex.Message; throw new InvalidOperationException($"{row.Index + 1}. satır: {ex.Message}"); }
        }
        return lines;
    }
    private void UpdateTotals()
    {
        try
        {
            var lines = dgKalemler.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow).Select(ReadLine).ToArray();
            lblTotals.Text = $"Ara toplam: {lines.Sum(l => l.Net):N2} TL    •    KDV: {lines.Sum(l => l.Vat):N2} TL    •    Genel toplam: {lines.Sum(l => l.Total):N2} TL";
        }
        catch (Exception ex) when (ex is FormatException or OverflowException)
        { lblTotals.Text = "Toplamı görmek için kalemlerdeki sayısal alanları tamamlayın."; }
    }
    private async void FormFatura_Load(object sender, EventArgs e) => await operation.RunAsync(LoadInvoicesAsync);
    private async Task LoadInvoicesAsync()
    {
        var repository = new InvoiceRepository(UserId);
        var table = await Task.Run(repository.Read);
        if (IsDisposed) { table.Dispose(); return; }
        var old = dgFaturalar.DataSource as DataTable;
        dgFaturalar.DataSource = table;
        old?.Dispose();
        foreach (DataGridViewColumn column in dgFaturalar.Columns)
            column.HeaderText = column.Name switch { "FaturaNo" => "Fatura no", "AliciUnvan" => "Alıcı unvanı", "AliciVKN" => "VKN / TCKN", _ => column.Name };
    }
    private async Task<bool> SaveAsync()
    {
        bool saved = false;
        await operation.RunAsync(async () =>
        {
            var invoice = new InvoiceDraft(txtFaturaNo.Text.Trim().ToUpperInvariant(), txtAliciUnvan.Text.Trim(),
                txtAliciVkn.Text.Trim(), dtTarih.Value.Date, ReadLines());
            invoice.Validate();
            var repository = new InvoiceRepository(UserId);
            string number = await Task.Run(() => repository.Save(invoice));
            dgKalemler.Rows.Clear();
            draft.Accept();
            saved = true;
            lblSubtitle.Text = number + " kaydedildi. PDF ve kayıt aynı fatura numarasını kullanır.";
            await LoadInvoicesAsync();
        });
        return saved;
    }
    private async void btnKaydet_Click(object sender, EventArgs e) => await SaveAsync();
    private async void RefreshInvoices_Click(object? sender, EventArgs e) => await operation.RunAsync(LoadInvoicesAsync);
    private async void NewInvoice_Click(object? sender, EventArgs e)
    {
        if (operation.IsBusy || !await draft.ConfirmAsync()) return;
        txtAliciUnvan.Clear(); txtAliciVkn.Clear(); dgKalemler.Rows.Clear();
        dtTarih.Value = DateTime.Today;
        draft.Accept();
        tabInvoices.SelectedTab = tabLines;
    }
    private async void dgFaturalar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgFaturalar.Rows.Count || dgFaturalar.Rows[e.RowIndex].IsNewRow) return;
        var id = dgFaturalar.Rows[e.RowIndex].Cells["Id"].Value;
        await operation.RunAsync(async () =>
        {
            var repository = new InvoiceRepository(UserId);
            var bytes = await Task.Run(() => repository.Pdf(id));
            var folder = Path.Combine(AppConfiguration.DataDirectory, "Onizleme");
            Directory.CreateDirectory(folder);
            var path = Path.Combine(folder, Guid.NewGuid().ToString("N") + ".pdf");
            await File.WriteAllBytesAsync(path, bytes);
            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
        });
    }
    private void CloseRecord_Click(object? sender, EventArgs e) => Close();
}