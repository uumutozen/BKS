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
    private void LoadPaymentData(Guid userId)
    {
        dataOgrVw.DataSource = ExecuteDataTable(
        @"SELECT 
                  OgrenciAdi = (SELECT Name FROM AYSStudents a WHERE a.Id = StudentId),
                  PaymentDate,
                  Amount
              FROM AYSFeePayments
              WHERE SchoolId = (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId)
              ORDER BY StudentId",
        CommandType.Text,
        DbParam("@UserId", userId));
    }
    private DataTable OdemeLoad(Guid userId, Guid studentId)
    {
        return ExecuteDataTable(
        @"SELECT p.Id,
                     'Ödeme Tutarı' = p.Amount,
                     'Ödeme Tarihi' = p.PaymentDate,
                     p.IsApproved,
                     'Ödendi Mi?' = CASE WHEN p.IsApproved = 1 THEN 'Ödendi' ELSE 'Ödenecek' END,
                     'Ödenen Tarihi' = p.ApprovedDate
              FROM AYSFeePayments p
              WHERE p.StudentId = @StudentId
                AND p.IsDeleted = 0
                AND p.SchoolId = (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId)
              ORDER BY p.PaymentDate",
        CommandType.Text,
        DbParam("@UserId", userId),
        DbParam("@StudentId", studentId));
    }
    private void ShowPaymentDetails(Guid studentId)
    {
        if (!_allowedModules.Contains(tabPageSatis.Name)) return;
        _documents.OpenDocument("payment:" + studentId, "Öğrenci ödemeleri", () =>
        {
            var paymentForm = new PaymentDetailsForm
            {
                AddPayment = (amount, date, grid) => AddPayment(studentId, amount, date, grid),
                ApprovePayment = grid => ApprovePayments(grid, studentId),
                OpenPlan = grid => ShowPaymentPlanForm(studentId, grid),
                RefreshPayments = grid => grid.DataSource = OdemeLoad(UserId, studentId)
            };
            paymentForm.PaymentGrid.DataSource = OdemeLoad(UserId, studentId);
            return paymentForm;
        }, tabPageSatis.Name);
    }
    private void ShowPaymentPlanForm(Guid studentId, DataGridView paymentGrid)
    {
        using var planForm = new PaymentPlanForm();
        Action createPlan = () =>
        {
            if (!decimal.TryParse(planForm.AmountText, out decimal tutar) || tutar <= 0)
            {
                MessageBox.Show("Geçerli bir tutar giriniz.");
                return;
            }
            int aySayisi = planForm.Months;
            using (SqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand insertCmd = new SqlCommand(@"INSERT INTO AYSFeePayments (Id, StudentId, Amount, PaymentDate, SchoolId)
                                                                      VALUES (@Id, @StudentId, @Amount, @PaymentDate,
                                                                      (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId))",
                        conn, tran))
                        {
                            insertCmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                            insertCmd.Parameters.Add("@StudentId", SqlDbType.UniqueIdentifier).Value = studentId;
                            insertCmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                            insertCmd.Parameters.Add("@PaymentDate", SqlDbType.DateTime);
                            insertCmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = UserId;
                            for (int i = 0; i<aySayisi; i++)
                            {
                                insertCmd.Parameters["@Id"].Value = Guid.NewGuid();
                                insertCmd.Parameters["@Amount"].Value = tutar;
                                insertCmd.Parameters["@PaymentDate"].Value = DateTime.Now.AddMonths(i);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
            paymentGrid.DataSource = OdemeLoad(UserId, studentId);
            MessageBox.Show("Aylık ödeme planı başarıyla oluşturuldu.");
            planForm.Close();
        };
        planForm.CreatePlan = createPlan;
        planForm.ShowDialog(this);
    }
    private void AddPayment(Guid studentId, string amountText, DateTime paymentDate, DataGridView paymentGrid)
    {
        if (!decimal.TryParse(amountText, out decimal amount))
        {
            MessageBox.Show("Geçerli bir tutar giriniz.");
            return;
        }
        ExecuteNonQueryCommand(
        @"INSERT INTO AYSFeePayments (Id, StudentId, Amount, PaymentDate, SchoolId)
              VALUES (@Id, @StudentId, @Amount, @PaymentDate,
              (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId))",
        CommandType.Text,
        DbParam("@Id", Guid.NewGuid()),
        DbParam("@StudentId", studentId),
        DbParam("@Amount", amount),
        DbParam("@PaymentDate", paymentDate),
        DbParam("@UserId", UserId));
        MessageBox.Show("Ödeme başarıyla eklendi.");
        paymentGrid.DataSource = OdemeLoad(UserId, studentId);
    }
    private void ApprovePayments(DataGridView paymentGrid, Guid studentId)
    {
        if (paymentGrid.SelectedRows.Count == 0)
        {
            MessageBox.Show("Önce ödeme seçin.");
            return;
        }
        if (MessageBox.Show("Seçili ödemeler onaylansın mı?", "Ödeme onayı", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
        using var conn = CreateConnection();
        conn.Open();
        using var transaction = conn.BeginTransaction();
        using var cmd = new SqlCommand("UPDATE AYSFeePayments SET IsApproved=1,ApprovedDate=@Now WHERE Id=@Id AND StudentId=@StudentId AND SchoolId=dbo.GetSirketIdByUserId(@UserId)",
        conn, transaction);
        cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
        cmd.Parameters.AddWithValue("@Now", DateTime.Now);
        cmd.Parameters.AddWithValue("@StudentId", studentId);
        cmd.Parameters.AddWithValue("@UserId", UserId);
        foreach (DataGridViewRow row in paymentGrid.SelectedRows)
        {
            if (row.IsNewRow || !Guid.TryParse(Convert.ToString(row.Cells["Id"].Value), out var id)) throw new InvalidOperationException("Geçersiz ödeme seçimi.");
            cmd.Parameters["@Id"].Value = id;
            if (cmd.ExecuteNonQuery() != 1) throw new InvalidOperationException("Ödeme bulunamadı veya erişiminiz yok.");
        }
        transaction.Commit();
        paymentGrid.DataSource = OdemeLoad(UserId, studentId);
        SetRibbonStatus("Seçili ödemeler onaylandı.");
    }
    private void btnMakeSale_Click(object sender, EventArgs e)
    {
        if (comboBoxStok.SelectedItem is not ComboBoxItem item || !Guid.TryParse(Convert.ToString(item.Value), out var studentId) || numericQuantitySold.Value <= 0)
        {
            MessageBox.Show("Öğrenci seçin ve sıfırdan büyük bir ödeme tutarı girin.");
            return;
        }
        int affected = ExecuteNonQueryCommand(@"INSERT INTO AYSFeePayments(Id,StudentId,Amount,PaymentDate,SchoolId)
            SELECT @Id,Id,@Amount,@Date,SchoolId FROM AYSStudents
            WHERE Id=@StudentId AND SchoolId=dbo.GetSirketIdByUserId(@UserId) AND ISNULL(IsDeleted,0)=0",
        CommandType.Text, DbParam("@Id", Guid.NewGuid()), DbParam("@StudentId", studentId), DbParam("@Amount", numericQuantitySold.Value),
        DbParam("@Date", DateTime.Now), DbParam("@UserId", UserId));
        if (affected != 1) throw new InvalidOperationException("Öğrenci kaydı bulunamadı veya erişim yetkiniz yok.");
        numericQuantitySold.Value = 0;
        _pageEdits[tabPageSatis.Name].AcceptChanges();
        LoadPaymentData(UserId);
        SetRibbonStatus("Ödeme kaydedildi.");
    }
}
