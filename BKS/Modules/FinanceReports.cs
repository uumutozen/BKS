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
    private void LoadSalesData()
    {
        dataGridOdeme.DataSource = ExecuteDataTable(FinanceQuery, CommandType.Text, DbParam("@UserId", UserId));
    }
    private void btnAddIncomeExpense_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtDescription.Text) || numericAmount.Value <= 0 || (!radioIncome.Checked && !radioExpense.Checked))
        {
            MessageBox.Show("Açıklama, pozitif tutar ve gelir/gider türünü belirtin.");
            return;
        }
        ExecuteNonQueryCommand("INSERT INTO GelirGider (Aciklama,Miktar,Tip,SirketId) VALUES (@Aciklama,@Miktar,@Tip,dbo.GetSirketIdByUserId(@UserId))",
        CommandType.Text, DbParam("@Aciklama", txtDescription.Text.Trim()), DbParam("@Miktar", numericAmount.Value), DbParam("@Tip",
        radioIncome.Checked ? "G": "D"), DbParam("@UserId", UserId));
        txtDescription.Clear();
        numericAmount.Value = 0;
        _pageEdits[tabPageGelirGider.Name].AcceptChanges();
        LoadSalesData();
        SetRibbonStatus("Gelir/gider kaydedildi.");
    }
    private void LoadOzelRaporlarToGrid()
    {
        salesGrid.DataSource = ExecuteDataTable("SELECT Id, RaporAdi, Sorgu, KayitTarihi FROM OzelRaporlar ORDER BY KayitTarihi DESC");
        if (salesGrid.Columns.Contains("Id")) salesGrid.Columns["Id"].Visible = false;
        if (salesGrid.Columns.Contains("Sorgu")) salesGrid.Columns["Sorgu"].Visible = false;
        if (salesGrid.Columns.Contains("RaporAdi")) salesGrid.Columns["RaporAdi"].HeaderText = "Rapor Adı";
        if (salesGrid.Columns.Contains("KayitTarihi")) salesGrid.Columns["KayitTarihi"].HeaderText = "Eklenme Tarihi";
    }
    private void salesGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex<0)
            return;
            var row = salesGrid.Rows[e.RowIndex];
            int raporId = Convert.ToInt32(row.Cells["Id"].Value);
            if (!_allowedModules.Contains(tabPageOzelRaporlar.Name)) return;
            _documents.OpenDocument("report:" + raporId, row.Cells["RaporAdi"].Value?.ToString() ?? "Rapor",
            () => new RaporCalistirForm(raporId), tabPageOzelRaporlar.Name);
        }
        catch (Exception)
        {
            MessageBox.Show("Rapor çalıştırılırken bir hata oluştu. Lütfen raporun sorgusunu kontrol edin.", "Hata", MessageBoxButtons.OK,
            MessageBoxIcon.Error);
        }
    }
}
