using System.ComponentModel;

namespace BKS;

/// <summary>Sütun başlığında sıralama ve metin filtresi; satır işlemlerinden ayrı tutulur.</summary>
internal sealed class GridColumnMenu
{
    private readonly DataGridView grid;
    private readonly GridFilterController filters;

    public GridColumnMenu(DataGridView grid, GridFilterController filters)
    {
        this.grid = grid;
        this.filters = filters;
        grid.ColumnHeaderMouseClick += (_, e) =>
        {
            if (e.Button == MouseButtons.Right && e.ColumnIndex >= 0) Show(grid.Columns[e.ColumnIndex]);
        };
    }

    private void Show(DataGridViewColumn column)
    {
        var menu = new ContextMenuStrip();
        void Sort(string caption, ListSortDirection direction)
        {
            var item = menu.Items.Add(caption, null, (_, _) => UiActions.Run(() => grid.Sort(column, direction)));
            item.Enabled = column.SortMode != DataGridViewColumnSortMode.NotSortable;
        }
        Sort("Artan sırala", ListSortDirection.Ascending);
        Sort("Azalan sırala", ListSortDirection.Descending);
        menu.Items.Add(new ToolStripSeparator());
        menu.Items.Add(new ToolStripLabel(column.HeaderText + " içinde ara"));
        var input = new ToolStripTextBox { Text = filters.ColumnFilter(column.DataPropertyName.Length > 0 ? column.DataPropertyName : column.Name), Width = 220 };
        menu.Items.Add(input);
        string key = column.DataPropertyName.Length > 0 ? column.DataPropertyName : column.Name;
        var apply = menu.Items.Add("Filtreyi uygula", null, (_, _) => filters.SetColumnFilter(key, input.Text));
        apply.Enabled = filters.CanFilter;
        menu.Items.Add("Bu sütunun filtresini temizle", null, (_, _) => filters.SetColumnFilter(key, null));
        input.KeyDown += (_, e) =>
        {
            if (e.KeyCode != Keys.Enter || !filters.CanFilter) return;
            filters.SetColumnFilter(key, input.Text);
            e.SuppressKeyPress = true;
            menu.Close();
        };
        menu.Closed += (_, _) => menu.Dispose();
        menu.Show(Cursor.Position);
    }

    public void ShowColumnChooser(Control owner)
    {
        var menu = new ContextMenuStrip();
        foreach (var column in grid.Columns.Cast<DataGridViewColumn>().Where(column => !GridAppearance.IsTechnical(column)).OrderBy(column => column.DisplayIndex))
        {
            var item = new ToolStripMenuItem(column.HeaderText) { Checked = column.Visible, CheckOnClick = true };
            item.CheckedChanged += (_, _) =>
            {
                if (!item.Checked && grid.Columns.Cast<DataGridViewColumn>().Count(candidate => candidate.Visible) <= 1)
                {
                    item.Checked = true;
                    return;
                }
                column.Visible = item.Checked;
            };
            menu.Items.Add(item);
        }
        menu.Closed += (_, _) => menu.Dispose();
        menu.Show(owner, new Point(0, owner.Height));
    }
}
