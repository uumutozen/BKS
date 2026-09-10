using System.Data;
namespace BKS;
public partial class Form2
{
    private bool financeSaving;
    private void LoadSalesData() => _ = LoadModuleAsync(tabPageGelirGider.Name);
    private async void btnAddIncomeExpense_Click(object sender, EventArgs e)
    {
        if (financeSaving || AppConfiguration.DesignPreview || !_allowedModules.Contains(tabPageGelirGider.Name)) return;
        var entry = new IncomeExpenseEntry(txtDescription.Text.Trim(), numericAmount.Value,
            radioIncome.Checked ? "G" : radioExpense.Checked ? "D" : "");
        try { entry.Validate(); }
        catch (Exception ex) { UiActions.ShowError(ex); return; }
        var user = UserId;
        financeSaving = true;
        tabPageGelirGider.Enabled = false;
        _ribbon.RefreshCommands();
        try
        {
            await Task.Run(() => new FinanceRepository().Save(user, entry));
            if (IsDisposed) return;
            txtDescription.Clear();
            numericAmount.Value = 0;
            _pageEdits[tabPageGelirGider.Name].AcceptChanges();
            SetRibbonStatus("Gelir / gider kaydedildi.");
        }
        catch (Exception ex) { if (!IsDisposed) UiActions.ShowError(ex); return; }
        finally
        {
            financeSaving = false;
            if (!IsDisposed) { tabPageGelirGider.Enabled = _allowedModules.Contains(tabPageGelirGider.Name); _ribbon.RefreshCommands(); }
        }
        await LoadModuleAsync(tabPageGelirGider.Name);
    }
    private void UpdateFinanceSummary(DataTable table)
    {
        var rows = table.AsEnumerable();
        decimal income = rows.Where(r => Convert.ToString(r["Tür"]) == "Gelir").Sum(r => DataValues.Money(r["Tutar"]));
        decimal expense = rows.Where(r => Convert.ToString(r["Tür"]) == "Gider").Sum(r => DataValues.Money(r["Tutar"]));
        lblFinanceSummary.Text = $"Gelir: {income:N2} TL    •    Gider: {expense:N2} TL    •    Bakiye: {income - expense:N2} TL";
    }
    private void LoadOzelRaporlarToGrid() => _ = LoadModuleAsync(tabPageOzelRaporlar.Name);
    private void salesGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => UiActions.Run(() =>
    {
        if (e.RowIndex < 0 || e.RowIndex >= salesGrid.Rows.Count || !_allowedModules.Contains(tabPageOzelRaporlar.Name)) return;
        if (!salesGrid.Columns.Contains("Id") || !int.TryParse(Convert.ToString(salesGrid.Rows[e.RowIndex].Cells["Id"].Value), out int id)) return;
        string title = Convert.ToString(salesGrid.Rows[e.RowIndex].Cells["RaporAdi"].Value) ?? "Rapor";
        _documents.OpenDocument("report:" + id, title, () => new RaporCalistirForm(id), tabPageOzelRaporlar.Name);
    });
}