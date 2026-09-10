using System.Data;
namespace BKS;

internal static class ReworkDiagnostics
{
    public static void Run(Action<bool, string> check, string directory)
    {
        using var host = new Form { ClientSize = new Size(900, 660) };
        host.Show();
        for (int i = 0; i < 3; i++)
        {
            var menu = TransientMenu.Create(host);
            bool clicked = false;
            var item = menu.Items.Add("Test", null, (_, _) => clicked = true);
            menu.Show(host, new Point(20, 20));
            menu.Close();
            check(!menu.IsDisposed, "Menu: closing does not dispose during WinForms event dispatch");
            item.PerformClick();
            check(clicked, "Menu: closing item remains callable until dispatch completes");
            Application.DoEvents();
            check(menu.IsDisposed, "Menu: popup is disposed after the event returns");
        }
        host.Hide();
        using var history = new DataListForm();
        history.Show();
        var table = new DataTable();
        table.Columns.Add("SilinmeZamani", typeof(DateTime));
        table.Columns.Add("ActionName"); table.Columns.Add("Kullanıcı"); table.Columns.Add("DeletedData");
        table.Rows.Add(new DateTime(2026, 9, 10, 10, 30, 0), "UPDATE", "Örnek kullanıcı",
            "[{\"Name\":\"Örnek öğrenci\",\"Surname\":\"Demo\",\"Address\":\"Örnek mahalle, İstanbul\",\"IsActive\":true}]");
        history.BindTable(table);
        var details = (DataGridView)history.Controls.Find("detailGrid", true).Single();
        check(details.Rows.Count >= 6 && details.ReadOnly, "History: selected JSON snapshot renders as readable, read-only fields");
        Capture(history, Path.Combine(directory, "Islem_Gecmisi.png"));
        history.Hide();
        using var plan = new PaymentPlanForm();
        plan.Show();
        var amount = (NumericUpDown)plan.Controls.Find("nudAmount", true).Single();
        amount.Value = 1250.55M;
        var grid = (DataGridView)plan.Controls.Find("gridPreview", true).Single();
        check(grid.Rows.Count == 12, "Payment screen: monthly plan previews twelve installments");
        Capture(plan, Path.Combine(directory, "Aylik_Odeme_Plani.png"));
        var frequency = (ComboBox)plan.Controls.Find("cmbFrequency", true).Single();
        frequency.SelectedIndex = 1;
        check(grid.Rows.Count == 1, "Payment screen: yearly choice starts with one annual installment");
        plan.Hide();
        using var report = new OzelRapor();
        report.Show();
        var parameters = (DataGridView)report.Controls.Find("parameterGrid", true).Single();
        var binding = new ReportParameterBinding(parameters);
        binding.SetQuery(ReportQuery.Parse("SELECT :Tutar AS Tutar"));
        parameters.Rows[0].Cells[1].Value = "Tutar";
        parameters.Rows[0].Cells[2].Value = "1.250,55";
        binding.SetQuery(ReportQuery.Parse("SELECT :Tutar AS Tutar, :Ad AS Ad"));
        check((decimal)binding.Read()[0].Value == 1250.55M && parameters.Rows.Count == 2,
            "Report editor: rebuilding parameters preserves entered types and values");
        report.Hide();
    }
    public static void Capture(Form form, string path)
    {
        bool wasVisible = form.Visible;
        if (!wasVisible) form.Show();
        form.Size = new Size(1100, 760);
        form.PerformLayout();
        Application.DoEvents();
        using var bitmap = new Bitmap(form.Width, form.Height);
        form.DrawToBitmap(bitmap, new Rectangle(Point.Empty, bitmap.Size));
        bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        if (!wasVisible) form.Hide();
    }
}
