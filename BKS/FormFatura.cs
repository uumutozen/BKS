using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace BKS;

public partial class FormFatura : Form
{
    public Guid UserId
    {
        get;
        set;
    }
    private bool saving;
    private readonly EditSession _edits;
    private readonly string connStr = AppConfiguration.ConnectionString;

    public FormFatura()
    {
        InitializeComponent();
        BuildModernInvoiceLayout();
        _edits = new EditSession(this, () => btnKaydet_Click(this, EventArgs.Empty),
        () => System.Text.Json.JsonSerializer.Serialize(dgKalemler.Rows.Cast<DataGridViewRow>()
        .Where(row => !row.IsNewRow).Select(row => row.Cells.Cast<DataGridViewCell>().Select(cell => Convert.ToString(cell.Value)).ToArray()).ToArray()));
    }

    private void BuildModernInvoiceLayout()
    {
        var info = new ResponsiveFields(("Fatura öneki (örnek: BKS)", txtFaturaNo), ("Alıcı unvanı", txtAliciUnvan), ("VKN / TCKN", txtAliciVkn),
        ("Belge tarihi", dtTarih));
        var lists = new TabControl
        {
            Dock = DockStyle.Fill
        };
        var lines = new TabPage("Fatura kalemleri");
        var history = new TabPage("Kayıtlı faturalar");
        lines.Controls.Add(dgKalemler);
        dgKalemler.Dock = DockStyle.Fill;
        history.Controls.Add(Screens.Grid(dgFaturalar));
        lists.TabPages.Add(lines);
        lists.TabPages.Add(history);
        var body = new EditorGridPanel(info, lists, .30F);
        var ribbon = Screens.Ribbon("Fatura", new RibbonCommand("Kaydet ve PDF", RibbonIcon.Backup, () => btnKaydet_Click(this,
        EventArgs.Empty), () => !saving), new RibbonCommand("Geçmişi yenile", RibbonIcon.Refresh, () => FaturalariYukle(UserId)),
        new RibbonCommand("Yeni belge", RibbonIcon.Add, () =>
        {
            txtAliciUnvan.Clear();
            txtAliciVkn.Clear();
            dgKalemler.Rows.Clear();
            lists.SelectedIndex = 0;
        }), new RibbonCommand("Kapat", RibbonIcon.Restore, Close));
        Screens.Install(this, body, ribbon, "Fatura merkezi");
        KeyPreview = true;
        KeyDown += (_, e) =>
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                UiActions.Run(() => btnKaydet_Click(this, EventArgs.Empty));
                e.SuppressKeyPress = true;
            }
        };
    }

    private void FormFatura_Load(object sender, EventArgs e)
    {
        dgKalemler.Columns.Clear();
        ModernWinForms.StyleGrid(dgKalemler);
        dgKalemler.ReadOnly = false;
        dgKalemler.AllowUserToAddRows = true;
        dgKalemler.AllowUserToDeleteRows = true;
        foreach (var(key, title) in new[]
        {
            ("UrunAdi", "Kalem açıklaması"),
            ("Miktar", "Miktar"),
            ("BirimFiyat", "Birim fiyat"),
            ("KDV", "KDV %")
        }) dgKalemler.Columns.Add(key, title);
        dgKalemler.Columns["UrunAdi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        foreach (var key in new[]
        {
            "Miktar",
            "BirimFiyat",
            "KDV"
        })
        {
            dgKalemler.Columns[key].ValueType = typeof(decimal);
            dgKalemler.Columns[key].DefaultCellStyle.Format = "N2";
        }
        dgKalemler.DefaultValuesNeeded += (_, args) =>
        {
            args.Row.Cells["Miktar"].Value = 1M;
            args.Row.Cells["BirimFiyat"].Value = 0M;
            args.Row.Cells["KDV"].Value = 20M;
        };
        dgKalemler.DataError += (_, args) =>
        {
            args.ThrowException = false;
            dgKalemler.Rows[args.RowIndex].ErrorText = "Sayısal alanlarda geçerli bir tutar girin.";
        };
        if (string.IsNullOrWhiteSpace(txtFaturaNo.Text)) txtFaturaNo.Text = "BKS";
        if (!AppConfiguration.DesignPreview) FaturalariYukle(UserId);
    }

    private List<InvoiceLine> ReadLines()
    {
        if (!dgKalemler.EndEdit()) throw new InvalidOperationException("Kalemlerdeki sayısal değerleri düzeltin.");
        var lines = new List<InvoiceLine>();
        foreach (DataGridViewRow row in dgKalemler.Rows)
        {
            if (row.IsNewRow) continue;
            var line = new InvoiceLine(Convert.ToString(row.Cells["UrunAdi"].Value)?.Trim() ?? "", DataValues.Money(row.Cells["Miktar"].Value),
            DataValues.Money(row.Cells["BirimFiyat"].Value), DataValues.Money(row.Cells["KDV"].Value));
            line.Validate();
            lines.Add(line);
        }
        if (lines.Count == 0) throw new InvalidOperationException("En az bir fatura kalemi ekleyin.");
        return lines;
    }

    private void btnKaydet_Click(object sender, EventArgs e)
    {
        if (saving) return;
        var prefix = txtFaturaNo.Text.Trim().ToUpperInvariant();
        var title = txtAliciUnvan.Text.Trim();
        var tax = txtAliciVkn.Text.Trim();
        if (!Regex.IsMatch(prefix, @"^[A-Z0-9]{1,12}$") || title.Length == 0 || !Regex.IsMatch(tax, @"^(\d{10}|\d{11})$")) throw new InvalidOperationException("Önek 1–12 harf/rakam, alıcı unvanı ve 10/11 haneli VKN/TCKN girin.");
        var lines = ReadLines();
        saving = true;
        string? path = null;
        bool committed = false;
        try
        {
            using var conn = new SqlConnection(connStr);
            conn.Open();
            using var transaction = conn.BeginTransaction(IsolationLevel.Serializable);
            var fullPrefix = prefix + dtTarih.Value.ToString("yy");
            using var numberCmd = new SqlCommand("SELECT MAX(FaturaNo) FROM Faturalar WITH (UPDLOCK,HOLDLOCK) WHERE SirketId=dbo.GetSirketIdByUserId(@UserId) AND FaturaNo LIKE @Prefix+'%'",
            conn, transaction);
            numberCmd.Parameters.AddWithValue("@UserId", UserId);
            numberCmd.Parameters.AddWithValue("@Prefix", fullPrefix);
            var previous = numberCmd.ExecuteScalar() as string;
            var number = 1;
            if (previous != null && previous.StartsWith(fullPrefix) && int.TryParse(previous[fullPrefix.Length ..], out var last)) number = checked(last + 1);
            if (number> 99999) throw new InvalidOperationException("Bu önek için numara sınırına ulaşıldı; yeni bir önek seçin.");
            var invoiceNo = fullPrefix + number.ToString("D5");
            var folder = Path.Combine(AppConfiguration.DataDirectory, "Faturalar", UserId.ToString("N"));
            Directory.CreateDirectory(folder);
            path = Path.Combine(folder, invoiceNo + "_" + Guid.NewGuid().ToString("N") + ".pdf");
            InvoicePdf.Write(path, invoiceNo, title, tax, dtTarih.Value, lines);
            using var insert = new SqlCommand("INSERT INTO Faturalar(FaturaNo,AliciUnvan,AliciVKN,Tarih,PdfYolu,PdfIcerik,SirketId) VALUES(@No,@Title,@Tax,@Date,@Path,@Pdf,dbo.GetSirketIdByUserId(@UserId))",
            conn, transaction);
            insert.Parameters.AddWithValue("@No", invoiceNo);
            insert.Parameters.AddWithValue("@Title", title);
            insert.Parameters.AddWithValue("@Tax", tax);
            insert.Parameters.AddWithValue("@Date", dtTarih.Value.Date);
            insert.Parameters.AddWithValue("@Path", path);
            insert.Parameters.Add("@Pdf", SqlDbType.VarBinary, - 1).Value = File.ReadAllBytes(path);
            insert.Parameters.AddWithValue("@UserId", UserId);
            if (insert.ExecuteNonQuery() != 1) throw new InvalidOperationException("Belge veritabanına kaydedilemedi.");
            transaction.Commit();
            committed = true;
            dgKalemler.Rows.Clear();
            _edits.AcceptChanges();
            MessageBox.Show(invoiceNo + " kaydedildi. PDF ve veritabanı numarası aynıdır.", "Fatura");
            FaturalariYukle(UserId);
        }
        catch
        {
            if (!committed && path != null && File.Exists(path)) File.Delete(path);
            throw;
        }
        finally
        {
            saving = false;
        }
    }

    private void FaturalariYukle(Guid user)
    {
        using var conn = new SqlConnection(connStr);
        using var cmd = new SqlCommand("SELECT Id,FaturaNo,AliciUnvan,AliciVKN,Tarih FROM Faturalar WHERE SirketId=dbo.GetSirketIdByUserId(@UserId) ORDER BY Tarih DESC,FaturaNo DESC",
        conn);
        cmd.Parameters.AddWithValue("@UserId", user);
        using var adapter = new SqlDataAdapter(cmd);
        var table = new DataTable();
        adapter.Fill(table);
        dgFaturalar.DataSource = table;
        if (dgFaturalar.Columns.Contains("Id")) dgFaturalar.Columns["Id"].Visible = false;
    }

    private void dgFaturalar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex<0 || dgFaturalar.Rows[e.RowIndex].IsNewRow) return;
        UiActions.Run(() =>
        {
            var row = dgFaturalar.Rows[e.RowIndex];
            using var conn = new SqlConnection(connStr);
            using var cmd = new SqlCommand("SELECT PdfIcerik FROM Faturalar WHERE Id=@Id AND SirketId=dbo.GetSirketIdByUserId(@UserId)",
            conn);
            cmd.Parameters.AddWithValue("@Id", row.Cells["Id"].Value);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            conn.Open();
            if (cmd.ExecuteScalar() is not byte[] bytes || bytes.Length == 0) throw new InvalidOperationException("Belgenin PDF içeriği bulunamadı.");
            var folder = Path.Combine(AppConfiguration.DataDirectory, "Onizleme");
            Directory.CreateDirectory(folder);
            var path = Path.Combine(folder, Guid.NewGuid().ToString("N") + ".pdf");
            File.WriteAllBytes(path, bytes);
            Process.Start(new ProcessStartInfo(path)
            {
                UseShellExecute = true
            });
        });
    }
}
