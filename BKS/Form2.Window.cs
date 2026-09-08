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
    private void ConfigureMainWindow()
    {
        Screens.ConfigureDpi(this);
        MinimumSize = new Size(720, 500);
        StartPosition = FormStartPosition.CenterScreen;
        WindowState = FormWindowState.Maximized;
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.Sizable;
        KeyPreview = true;
    }

    private void BuildResponsiveLayout()
    {
        BuildRibbonWorkspace();
    }

    private void RunPreRegistrationAdd() => btnOnKayitEkle_Click(btnOnKayitEkle, EventArgs.Empty);

    private void RunPreRegistrationConfirm() => btnKesinKayitYap_Click_1(btnKesinKayitYap, EventArgs.Empty);

    private void RunPreRegistrationDelete() => btnOnKayitSil_Click(btnOnKayitSil, EventArgs.Empty);

    private void RunClassSave() => btnOgrenciYonetimiSinifKaydet_Click(btnOgrenciYonetimiSinifKaydet, EventArgs.Empty);

    private void RunClassUpdate() => btnOgrenciYonetimiSinifGuncelle_Click(btnOgrenciYonetimiSinifGuncelle, EventArgs.Empty);

    private void RunClassDelete() => btnOgrenciYonetimiSinifSil_Click(btnOgrenciYonetimiSinifSil, EventArgs.Empty);

    private void RunPaymentEntry() => btnMakeSale_Click(btnMakeSale, EventArgs.Empty);

    private void RunIncomeExpenseSave() => btnAddIncomeExpense_Click(btnAddIncomeExpense, EventArgs.Empty);

    private void RunInvoiceCenter() => FaturaBtn_Click(FaturaBtn, EventArgs.Empty);

    private void CloseApplication() => Close();
}
