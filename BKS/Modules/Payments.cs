namespace BKS;
public partial class Form2
{
    private bool paymentSaving;
    private PaymentRepository Payments => new(AppConfiguration.ConnectionString, UserId);
    private void ShowPaymentDetails(Guid studentId)
    {
        if (AppConfiguration.DesignPreview || !_allowedModules.Contains(tabPageSatis.Name)) return;
        var form = _documents.OpenDocument("payment:" + studentId, "Öğrenci ödemeleri",
            () => new PaymentDetailsForm(Payments, studentId,
                () => !_sessionLoading && _allowedModules.Contains(tabPageSatis.Name)), tabPageSatis.Name);
        _ = form.ReloadAsync();
    }
    private async void btnMakeSale_Click(object sender, EventArgs e)
    {
        if (paymentSaving || AppConfiguration.DesignPreview || !_allowedModules.Contains(tabPageSatis.Name)) return;
        if (comboBoxStok.SelectedItem is not ComboBoxItem item || !Guid.TryParse(Convert.ToString(item.Value), out var student))
        { MessageBox.Show("Öğrenci seçin."); return; }
        decimal amount = numericQuantitySold.Value;
        try { PaymentSchedule.ValidateAmount(amount); }
        catch (Exception ex) { UiActions.ShowError(ex); return; }
        paymentSaving = true;
        btnMakeSale.Enabled = false;
        var repository = Payments;
        try
        {
            await Task.Run(() => repository.Create(student, new[] { new ScheduledPayment(DateTime.Today, amount) }));
            if (IsDisposed) return;
            numericQuantitySold.Value = 0;
            _pageEdits[tabPageSatis.Name].AcceptChanges();
            SetRibbonStatus("Ödeme oluşturuldu; onay için öğrenci ödemelerini açın.");
        }
        catch (Exception ex) { if (!IsDisposed) UiActions.ShowError(ex); return; }
        finally { paymentSaving = false; if (!IsDisposed) btnMakeSale.Enabled = true; }
        await LoadModuleAsync(tabPageSatis.Name);
    }
}