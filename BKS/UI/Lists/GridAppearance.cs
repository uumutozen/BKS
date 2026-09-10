using System.Runtime.CompilerServices;

namespace BKS;

/// <summary>Liste görünümü, satır ölçüleri ve türlere göre sütun biçimleri.</summary>
internal sealed class GridAppearance
{
    private static readonly ConditionalWeakTable<DataGridView, GridAppearance> Instances = new();
    private readonly DataGridView grid;
    private bool measuring;

    public static void Apply(DataGridView grid) => Instances.GetValue(grid, item => new GridAppearance(item)).ApplyStyle();

    private GridAppearance(DataGridView grid)
    {
        this.grid = grid;
        grid.DataBindingComplete += (_, _) => FormatColumns();
        grid.CellPainting += PaintRowNumber;
        grid.Paint += PaintEmptyState;
        grid.DpiChangedAfterParent += (_, _) => ApplyStyle();
    }

    private int Px(int value) => (int)Math.Ceiling(value * grid.DeviceDpi / 96F);

    private void ApplyStyle()
    {
        grid.BorderStyle = BorderStyle.None;
        grid.BackgroundColor = RibbonPalette.Surface;
        grid.EnableHeadersVisualStyles = false;
        grid.GridColor = RibbonPalette.GridLine;
        grid.ColumnHeadersDefaultCellStyle.BackColor = RibbonPalette.GridHeader;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = RibbonPalette.Text;
        grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F);
        grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(Px(8), 0, Px(8), 0);
        grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = RibbonPalette.GridHeader;
        grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = RibbonPalette.Text;
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        grid.ColumnHeadersHeight = Px(36);
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
        grid.DefaultCellStyle.Padding = new Padding(Px(8), Px(3), Px(8), Px(3));
        grid.DefaultCellStyle.ForeColor = RibbonPalette.Text;
        grid.DefaultCellStyle.BackColor = RibbonPalette.GridRow;
        grid.DefaultCellStyle.SelectionBackColor = RibbonPalette.SelectedRow;
        grid.DefaultCellStyle.SelectionForeColor = RibbonPalette.Text;
        grid.AlternatingRowsDefaultCellStyle.BackColor = RibbonPalette.AlternateRow;
        grid.RowTemplate.Height = Px(32);
        grid.RowHeadersVisible = true;
        grid.RowHeadersWidth = Px(44);
        grid.RowHeadersDefaultCellStyle.BackColor = RibbonPalette.Surface;
        grid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AllowUserToOrderColumns = true;
        grid.AllowUserToResizeColumns = true;
        grid.AllowUserToResizeRows = false;
        grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        FormatColumns();
    }

    internal static bool IsTechnical(DataGridViewColumn column) => column.ValueType == typeof(byte[])
        || column.ValueType == typeof(Guid)
        || new[] { "Id", "PersonelId", "StudentId", "SchoolId", "CompanyId", "SirketId", "Sorgu", "FotoId", "Photo", "photobinary" }
            .Contains(column.Name, StringComparer.OrdinalIgnoreCase);

    private void FormatColumns()
    {
        if (measuring || grid.Columns.Count == 0) return;
        measuring = true;
        try
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                if (IsTechnical(column)) { column.Visible = false; continue; }
                column.MinimumWidth = Px(70);
                if (column is DataGridViewTextBoxColumn)
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                if (column.ValueType == typeof(DateTime) && string.IsNullOrEmpty(column.DefaultCellStyle.Format))
                    column.DefaultCellStyle.Format = "dd.MM.yyyy";
                column.DefaultCellStyle.FormatProvider = System.Globalization.CultureInfo.GetCultureInfo("tr-TR");
                if (column.ValueType == typeof(int) || column.ValueType == typeof(long) || column.ValueType == typeof(short))
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (column.ValueType == typeof(decimal) || column.ValueType == typeof(double) || column.ValueType == typeof(float))
                {
                    column.DefaultCellStyle.Format = "N2";
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                // Kullanıcının sonradan verdiği genişliği her seçimde yeniden ölçmeyin.
                if (column.Width == 100)
                {
                    int preferred = column.GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, true);
                    column.Width = Math.Clamp(preferred, Px(100), Px(320));
                }
            }
        }
        finally { measuring = false; }
    }

    private void PaintRowNumber(object? sender, DataGridViewCellPaintingEventArgs e)
    {
        if (e.ColumnIndex != -1 || e.RowIndex < 0) return;
        e.PaintBackground(e.CellBounds, true);
        bool selected = grid.Rows[e.RowIndex].Selected;
        using var fill = new SolidBrush(selected ? RibbonPalette.Pressed : RibbonPalette.Surface);
        e.Graphics.FillRectangle(fill, e.CellBounds);
        if (selected)
        {
            using var accent = new SolidBrush(RibbonPalette.Accent);
            e.Graphics.FillRectangle(accent, e.CellBounds.Left, e.CellBounds.Top, Px(3), e.CellBounds.Height);
        }
        TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), grid.Font, e.CellBounds,
            RibbonPalette.CaptionText, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        e.Handled = true;
    }

    private void PaintEmptyState(object? sender, PaintEventArgs e)
    {
        if (grid.Rows.Count > (grid.AllowUserToAddRows ? 1 : 0)) return;
        var rectangle = new Rectangle(Px(20), grid.ColumnHeadersHeight + Px(24), Math.Max(0, grid.Width - Px(40)),
            Math.Max(0, grid.Height - grid.ColumnHeadersHeight - Px(48)));
        TextRenderer.DrawText(e.Graphics, "Gösterilecek kayıt bulunamadı.", grid.Font,
            rectangle, RibbonPalette.CaptionText, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
    }
}
