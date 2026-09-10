using System.Data;
using System.Data.SqlClient;

namespace BKS;

internal sealed class PaymentRepository
{
    private readonly string connectionString;
    private readonly Guid user;
    public PaymentRepository(string connectionString, Guid user) { this.connectionString = connectionString; this.user = user; }
    public const string ListSql = @"SELECT p.Id,p.StudentId,a.Name+' '+a.Surname AS [Öğrenci],
        p.PaymentDate AS [Vade tarihi],p.Amount AS [Tutar],p.IsApproved,
        CASE WHEN p.IsApproved=1 THEN N'Ödendi' ELSE N'Bekliyor' END AS [Durum],p.ApprovedDate AS [Onay tarihi]
        FROM AYSFeePayments p JOIN AYSStudents a ON a.Id=p.StudentId AND a.SchoolId=p.SchoolId
        WHERE p.SchoolId=dbo.GetSirketIdByUserId(@UserId) AND ISNULL(p.IsDeleted,0)=0";
    public DataTable Read(Guid? student = null)
    {
        var args = new List<SqlParameter> { new("@UserId", user) };
        if (student.HasValue) args.Add(new SqlParameter("@StudentId", student.Value));
        return new SqlDataAccess(connectionString).Table(ListSql + (student.HasValue ? " AND p.StudentId=@StudentId" : "")
            + " ORDER BY p.PaymentDate DESC,p.Id", CommandType.Text, args.ToArray());
    }
    public void Create(Guid student, IReadOnlyList<ScheduledPayment> payments)
    {
        if (user == Guid.Empty || student == Guid.Empty) throw new InvalidOperationException("Öğrenci ve kullanıcı bilgisi gerekli.");
        if (payments.Count is < 1 or > 120) throw new InvalidOperationException("Geçerli bir ödeme planı oluşturun.");
        foreach (var payment in payments) PaymentSchedule.ValidateAmount(payment.Amount);
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();
        using var command = new SqlCommand(@"INSERT INTO AYSFeePayments(Id,StudentId,Amount,PaymentDate,SchoolId,IsApproved,IsDeleted)
            SELECT @Id,Id,@Amount,@Date,SchoolId,0,0 FROM AYSStudents
            WHERE Id=@StudentId AND SchoolId=dbo.GetSirketIdByUserId(@UserId) AND ISNULL(IsDeleted,0)=0", connection, transaction);
        command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
        command.Parameters.Add("@StudentId", SqlDbType.UniqueIdentifier).Value = student;
        var amount = command.Parameters.Add("@Amount", SqlDbType.Decimal); amount.Precision = 18; amount.Scale = 2;
        command.Parameters.Add("@Date", SqlDbType.DateTime);
        command.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = user;
        foreach (var payment in payments)
        {
            command.Parameters["@Id"].Value = Guid.NewGuid();
            amount.Value = payment.Amount;
            command.Parameters["@Date"].Value = payment.DueDate;
            if (command.ExecuteNonQuery() != 1) throw new InvalidOperationException("Öğrenci bulunamadı veya erişiminiz yok. Plan kaydedilmedi.");
        }
        transaction.Commit();
    }
    public int Approve(Guid student, IReadOnlyCollection<Guid> paymentIds)
    {
        if (user == Guid.Empty || student == Guid.Empty || paymentIds.Count == 0 || paymentIds.Contains(Guid.Empty))
            throw new InvalidOperationException("Onaylanacak ödemeleri seçin.");
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();
        using var command = new SqlCommand(@"UPDATE AYSFeePayments SET IsApproved=1,ApprovedDate=@Now
            WHERE Id=@Id AND StudentId=@StudentId AND SchoolId=dbo.GetSirketIdByUserId(@UserId)
            AND ISNULL(IsDeleted,0)=0 AND ISNULL(IsApproved,0)=0", connection, transaction);
        command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
        command.Parameters.Add("@StudentId", SqlDbType.UniqueIdentifier).Value = student;
        command.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = user;
        command.Parameters.Add("@Now", SqlDbType.DateTime).Value = DateTime.Now;
        int changed = 0;
        foreach (var id in paymentIds.Distinct())
        {
            command.Parameters["@Id"].Value = id;
            if (command.ExecuteNonQuery() != 1)
                throw new InvalidOperationException("Bir ödeme değişmiş, onaylanmış veya erişime kapanmış. Listeyi yenileyip yeniden seçin; toplu onay kaydedilmedi.");
            changed++;
        }
        transaction.Commit();
        return changed;
    }
}
