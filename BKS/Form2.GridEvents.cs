using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.ComponentModel;
using System.Collections;

namespace BKS;

public partial class Form2
{
    private void ResizeAndSetButtonImage(Button button, Image image)
    {
        int width = button.Width - 10;
        int height = button.Height - 10;
        Image resized = new Bitmap(image, new Size(width, height));
        button.Image = resized;
        button.ImageAlign = ContentAlignment.MiddleCenter;
        button.TextImageRelation = TextImageRelation.Overlay;
    }

    private void DataGridView_MouseDown(object sender, MouseEventArgs e)
    {
        aktifDGV = sender as DataGridView;
        if (aktifDGV == null)
        return;
        DataGridView.HitTestInfo hit = aktifDGV.HitTest(e.X, e.Y);
        if (hit.RowIndex<0)
        return;
        if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
        {
            aktifDGV.ClearSelection();
            aktifDGV.Rows[hit.RowIndex].Selected = true;
            if (hit.ColumnIndex >= 0)
            aktifDGV.CurrentCell = aktifDGV.Rows[hit.RowIndex].Cells[hit.ColumnIndex];
            else if (aktifDGV.Columns.Cast<DataGridViewColumn>().FirstOrDefault(column => column.Visible) is { } first)
                aktifDGV.CurrentCell = aktifDGV.Rows[hit.RowIndex].Cells[first.Index];
        }
        if (e.Button == MouseButtons.Right)
        {
            int activeTag = GetActiveGridTag();
            if (ödemeDetaylarıToolStripMenuItem != null)
            ödemeDetaylarıToolStripMenuItem.Visible = activeTag == StudentModuleTag;
            if (arşivToolStripMenuItem != null)
            arşivToolStripMenuItem.Visible = activeTag == StudentModuleTag || activeTag == PersonelModuleTag;
            // ContextMenuStrip is opened once by WinForms; class grid keeps its own menu.
        }
    }

    private void DataStokRefresh(object sender, EventArgs e)
    {
        RefreshStudentGrid();
    }

    private void LoadPersonelRefreshEvent(object sender, EventArgs e)
    {
        RefreshPersonelGrid();
    }

    private void yeniKayitEkle(object sender, EventArgs e)
    {
        if (GetActiveGridTag() == StudentModuleTag && _allowedModules.Contains(tabPageStok.Name))
        {
            _documents.OpenDocument("student:new", "Yeni öğrenci", () =>
            {
                var form = new OgrenciForm(this)
                {
                    UserId = UserId
                };
                form.RefreshData += DataStokRefresh;
                return form;
            }, tabPageStok.Name);
        }
        else if (GetActiveGridTag() == PersonelModuleTag && _allowedModules.Contains(tabPagePersonelYonetimi.Name))
        {
            _documents.OpenDocument("personnel:new", "Yeni personel", () =>
            {
                var form = new PersonelForm(this)
                {
                    UserId = UserId
                };
                form.RefreshData += LoadPersonelRefreshEvent;
                return form;
            }, tabPagePersonelYonetimi.Name);
        }
    }

    private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        RefreshActiveGrid();
    }
}
