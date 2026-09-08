using System.Data;
using System.Data.SqlClient;
namespace BKS;
internal sealed class ModuleReader
{
    private readonly SqlDataAccess database;
    public ModuleReader(SqlDataAccess database) => this.database = database;
    public const string FinanceQuery = "SELECT Aciklama AS [Açıklama],Miktar AS [Tutar],CASE Tip WHEN 'G' THEN N'Gelir' ELSE N'Gider' END AS [Tür] FROM GelirGider WHERE SirketId=dbo.GetSirketIdByUserId(@UserId)";
    public List<DataTable> Read(string key, Guid user)
    {
        DataTable Query(string sql) => database.Table(sql, CommandType.Text, new SqlParameter("@UserId", user));
        return key switch
        {
            "tabPageOgrenciOnKayit" => new()
            {
                Query("SELECT Id,FirstName AS [İsim],LastName AS [Soyisim],BirthDate AS [Doğum Tarihi],ParentPhone AS [Baba Telefon],Notes AS [Notlar],FatherName AS [Baba Adı],PaymentStatus AS [Ödeme Durumu],MonthlyFee AS [Aylık Ücret] FROM PreRegistrations WHERE ClassId=dbo.GetSirketIdByUserId(@UserId) AND ISNULL(IsActive,1)=1")
            },
            "tabPageStok" => new()
            {
                Query("EXEC GetStudent @UserId, NULL"),
                Query("SELECT Id,ClassName AS [Sınıf],[Group] AS [Yaş Grubu],OgretmenAdi AS [Öğretmen Adı] FROM AYSClasses WHERE SchoolId=dbo.GetSirketIdByUserId(@UserId) AND ISNULL(IsDeleted,0)=0"),
                Query("SELECT FirstName+' '+LastName AS isim,PersonelId FROM Personel WHERE CompanyId=dbo.GetSirketIdByUserId(@UserId) AND IsTeacher=1 AND ISNULL(IsActive,1)=1")
            },
            "tabPageSatis" => new()
            {
                Query("SELECT p.Id,p.StudentId,a.Name+' '+a.Surname AS [Öğrenci],p.PaymentDate AS [Ödeme Tarihi],p.Amount AS [Tutar] FROM AYSFeePayments p JOIN AYSStudents a ON a.Id=p.StudentId WHERE p.SchoolId=dbo.GetSirketIdByUserId(@UserId) ORDER BY p.PaymentDate DESC"),
                Query("SELECT Id,Name+' '+Surname AS Name FROM AYSStudents WHERE SchoolId=dbo.GetSirketIdByUserId(@UserId) AND ISNULL(IsDeleted,0)=0 ORDER BY Name")
            },
            "tabPagePersonelYonetimi" => new()
            {
                Query("EXEC GetPersonel @UserId")
            },
            "tabPageGelirGider" => new()
            {
                Query(FinanceQuery)
            },
            "tabPageOzelRaporlar" => new()
            {
                Query("SELECT Id,RaporAdi,KayitTarihi FROM OzelRaporlar ORDER BY KayitTarihi DESC")
            },
            _ => new()
        };
    }
}
