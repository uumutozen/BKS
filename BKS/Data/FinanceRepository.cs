using System.Data;
using System.Data.SqlClient;
namespace BKS;
internal sealed class FinanceRepository
{
    public void Save(Guid user, IncomeExpenseEntry entry)
    {
        entry.Validate();
        if (user == Guid.Empty) throw new InvalidOperationException("Geçerli kullanıcı oturumu gerekli.");
        var amount = new SqlParameter("@Amount", SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = entry.Amount };
        int count = new SqlDataAccess(AppConfiguration.ConnectionString).ExecuteNonQuery(@"INSERT INTO GelirGider(Aciklama,Miktar,Tip,SirketId)
            SELECT @Description,@Amount,@Type,CompanyId FROM (SELECT dbo.GetSirketIdByUserId(@User) AS CompanyId) c WHERE CompanyId IS NOT NULL",
            CommandType.Text, new SqlParameter("@Description", entry.Description.Trim()), amount,
            new SqlParameter("@Type", entry.Type), new SqlParameter("@User", user));
        if (count != 1) throw new InvalidOperationException("Şirket bulunamadığı için gelir / gider kaydedilmedi.");
    }
}
