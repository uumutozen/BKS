namespace BKS;

public partial class Form2
{
    private void NewRecord(DataGridView grid)
    {
        aktifDGV = grid;
        yeniKayitEkle(grid, EventArgs.Empty);
    }

    private void EditSelected(DataGridView grid)
    {
        if (grid.CurrentRow == null) return;
        aktifDGV = grid;
        var cell = new DataGridViewCellEventArgs(0, grid.CurrentRow.Index);
        if (grid == dgvPersonelYonetimi) dataGridViewPersonel_CellDoubleClick(grid, cell);
        else dataGridViewStok_CellDoubleClick(grid, cell);
    }

    private void DeleteSelected(DataGridView grid)
    {
        aktifDGV = grid;
        DeleteStripMenuItem_Click(grid, EventArgs.Empty);
    }

    private void ArchiveSelected(DataGridView grid)
    {
        aktifDGV = grid;
        arşivToolStripMenuItem_Click(grid, EventArgs.Empty);
    }

    private void ImportSelected(DataGridView grid)
    {
        aktifDGV = grid;
        excelAktarToolStripMenuItem_Click(grid, EventArgs.Empty);
    }
}
