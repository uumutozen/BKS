namespace BKS;
internal sealed class ListSurface : Panel
{
    private readonly DataGridView grid;
    private readonly Label title;
    private readonly Label count;
    private readonly Label footer;
    public ListSurface(DataGridView grid)
    {
        this.grid = grid;
        Dock = DockStyle.Fill;
        BackColor = Color.White;
        title = new Label
        {
            Text = TitleFor(grid.Name),
            ForeColor = RibbonPalette.Text,
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleLeft
        };
        count = new Label
        {
            ForeColor = RibbonPalette.CaptionText,
            TextAlign = ContentAlignment.MiddleRight
        };
        footer = new Label
        {
            Text = "İşlemler için satıra veya boş alana sağ tıklayın.",
            ForeColor = ModernWinForms.Muted,
            Font = new Font("Segoe UI", 9F),
            TextAlign = ContentAlignment.MiddleLeft,
            BackColor = Color.FromArgb(245, 248, 252)
        };
        grid.Dock = DockStyle.None;
        grid.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        Controls.Add(grid);
        Controls.Add(title);
        Controls.Add(count);
        Controls.Add(footer);
        grid.DataBindingComplete += (_, _) => UpdateCount();
        grid.RowsAdded += (_, _) => UpdateCount();
        grid.RowsRemoved += (_, _) => UpdateCount();
        grid.SelectionChanged += (_, _) => UpdateCount();
        UpdateCount();
    }
    private void UpdateCount()
    {
        if (IsDisposed) return;
        int records = grid.Rows.Count - (grid.NewRowIndex >= 0 ? 1: 0);
        count.Text = $"{Math.Max(0, records):N0} kayıt";
    }
    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e);
        if (grid == null || title == null) return;
        int Px(int n) => (int) Math.Ceiling(n * DeviceDpi / 96F);
        int head = Height<Px(100) ? 0: Math.Min(Height, Px(Height<Px(200) ? 32: 44));
        int foot = Height<Px(200) ? 0: Math.Min(Math.Max(0, Height - head), Px(30));
        title.Visible = count.Visible = head> 0;
        footer.Visible = foot> 0;
        int countWidth = Math.Min(Width / 3, Px(120));
        title.SetBounds(Px(12), 0, Math.Max(0, Width - countWidth - Px(24)), head);
        count.SetBounds(Math.Max(0, Width - countWidth - Px(12)), 0, countWidth, head);
        grid.SetBounds(0, head, Width, Math.Max(0, Height - head - foot));
        footer.SetBounds(0, Height - foot, Width, foot);
        footer.Padding = new Padding(Px(12), 0, 0, 0);
    }
    private static string TitleFor(string name) => name switch
    {
        "dgvOnKayitlar" => "Ön kayıtlar",
        "dataGridViewStok" => "Öğrenciler",
        "DgvOgrenciYonetimiSiniflar" => "Sınıflar",
        "dgvPersonelYonetimi" => "Personel listesi",
        "dataOgrVw" => "Ödeme hareketleri",
        "dataGridOdeme" => "Gelir ve giderler",
        "salesGrid" => "Kayıtlı raporlar",
        "dgvDosyalar" => "Dosya arşivi",
        "dgFaturalar" => "Fatura geçmişi",
        _ => "Kayıt listesi"
    };
}
