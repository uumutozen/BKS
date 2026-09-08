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
    private void btnGuncelle_Click(object sender, EventArgs e)
    {
    }

    private void btnAddStock_Click(object sender, EventArgs e)
    {
    }

    private void salesGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void Delete(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void textBox4_TextChanged(object sender, EventArgs e)
    {
    }

    private void groupBox1_Enter(object sender, EventArgs e)
    {
    }

    private void dataOgrVw_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void textBabaAd_TextChanged(object sender, EventArgs e)
    {
    }

    private void dateDogum_ValueChanged(object sender, EventArgs e)
    {
    }

    private void groupBox3_Enter(object sender, EventArgs e)
    {
    }

    private void tabPageStok_Click(object sender, EventArgs e)
    {
    }

    private void dataGridViewStok_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void tabPagePersonelYonetimi_Click(object sender, EventArgs e)
    {
    }

    private void cmbogrsınıf_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
    {
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void label2_Click(object sender, EventArgs e)
    {
    }

    private void label4_Click(object sender, EventArgs e)
    {
    }

    private void txtPersonelMaas_TextChanged(object sender, EventArgs e)
    {
    }

    private void txtPersonelGorev_TextChanged(object sender, EventArgs e)
    {
    }

    private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
    {
    }

    private void label7_Click(object sender, EventArgs e)
    {
    }

    private void groupBox18_Enter(object sender, EventArgs e)
    {
    }

    private void txtPersonelKimlik_TextChanged(object sender, EventArgs e)
    {
    }

    private void btnOnKayitEkle_Click_1(object sender, EventArgs e)
    {
    }

    private void EFatura_Click(object sender, EventArgs e)
    {
    }

    private void tabPage1_Click_2(object sender, EventArgs e)
    {
    }

    private void tabPage1_Click(object sender, EventArgs e)
    {
        LoadSalesData();
    }

    private void FaturaBtn_Click(object sender, EventArgs e)
    {
        if (!_allowedModules.Contains(tabPageGelirGider.Name)) return;
        _documents.OpenDocument("invoices", "Fatura merkezi",
        () => new FormFatura
        {
            UserId = UserId
        }, tabPageGelirGider.Name);
    }
}
