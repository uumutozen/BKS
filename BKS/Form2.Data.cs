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
    private Guid EnsurePersonelArchiveStudent(Guid personelId)
    {
        /*
            Personel arşivi için yeni endpoint açmadan mevcut öğrenci arşiv sistemini kullanıyoruz.

            Mantık:
            - Personel sağ tık > Arşiv
            - AysStudents tablosunda aynı Id ile gizli bir kayıt var mı bakılır.
            - Yoksa Personel bilgileriyle AysStudents içine IsDeleted = 1 olan gizli kayıt açılır.
            - arsivForm bu Id'yi öğrenci Id gibi kullanır.
            - Dosya arşiv endpointleri aynı kalır:
                GET  /api/dosya-arsiv/ogrenci/{id}
                POST /api/dosya-arsiv/yukle
        */
        object exists = ExecuteScalarValue(
        "SELECT COUNT(1) FROM AysStudents WHERE Id = @Id",
        CommandType.Text,
        DbParam("@Id", personelId));
        if (exists != null && exists != DBNull.Value && Convert.ToInt32(exists)> 0)
        return personelId;
        DataTable personelDt = ExecuteDataTable(
        @"SELECT TOP 1
          p.PersonelId,
          p.FirstName,
          p.LastName,
          p.Phone,
          p.Address,
          p.Birthdate,
          p.CompanyId
      FROM Personel p
      WHERE p.PersonelId = @PersonelId
        AND p.CompanyId = (SELECT TOP 1 CompanyId FROM CompanyUsers WHERE UserId = @UserId)",
        CommandType.Text,
        DbParam("@PersonelId", personelId),
        DbParam("@UserId", UserId));
        if (personelDt.Rows.Count == 0)
        throw new Exception("Personel bilgisi bulunamadı.");
        DataRow pRow = personelDt.Rows[0];
        string firstName = pRow["FirstName"] == DBNull.Value || string.IsNullOrWhiteSpace(pRow["FirstName"].ToString())
        ? "Personel"
        : pRow["FirstName"].ToString();
        string lastName = pRow["LastName"] == DBNull.Value || string.IsNullOrWhiteSpace(pRow["LastName"].ToString())
        ? "Arşiv"
        : pRow["LastName"].ToString();
        string phone = pRow["Phone"] == DBNull.Value ? "": pRow["Phone"].ToString();
        string address = pRow["Address"] == DBNull.Value ? "": pRow["Address"].ToString();
        DateTime birthDate = DateTime.Now;
        if (pRow["Birthdate"] != DBNull.Value &&
        DateTime.TryParse(pRow["Birthdate"].ToString(), out DateTime parsedBirthDate))
        {
            birthDate = parsedBirthDate;
        }
        Guid companyId = Guid.Parse(pRow["CompanyId"].ToString());
        ExecuteNonQueryCommand(
        @"INSERT INTO AysStudents
      (
          Id,
          Name,
          Surname,
          FatherName,
          BirthDate,
          StudentCode,
          ClassId,
          PaymentStatus,
          MonthlyFee,
          IsActive,
          FatherAddress,
          MotherAddress,
          FatherPhoneNumber,
          MotherPhoneNumber,
          IsMarried,
          StudentsDetails,
          MotherName,
          SchoolId,
          IsDeleted
      )
      VALUES
      (
          @Id,
          @Name,
          @Surname,
          @FatherName,
          @BirthDate,
          @StudentCode,
          (
              SELECT TOP 1 Id
              FROM AYSClasses
              WHERE SchoolId = @SchoolId
                AND ISNULL(IsDeleted, 0) = 0
              ORDER BY ClassName
          ),
          @PaymentStatus,
          @MonthlyFee,
          @IsActive,
          @FatherAddress,
          @MotherAddress,
          @FatherPhoneNumber,
          @MotherPhoneNumber,
          @IsMarried,
          @StudentsDetails,
          @MotherName,
          @SchoolId,
          @IsDeleted
      )",
        CommandType.Text,
        DbParam("@Id", personelId),
        DbParam("@Name", firstName),
        DbParam("@Surname", lastName),
        DbParam("@FatherName", "PERSONEL ARŞİV"),
        DbParam("@BirthDate", birthDate),
        DbParam("@StudentCode", "PRSARSIV-" + personelId.ToString("N").Substring(0, 12).ToUpper()),
        DbParam("@PaymentStatus", false),
        DbParam("@MonthlyFee", 0),
        DbParam("@IsActive", false),
        DbParam("@FatherAddress", address),
        DbParam("@MotherAddress", ""),
        DbParam("@FatherPhoneNumber", phone),
        DbParam("@MotherPhoneNumber", ""),
        DbParam("@IsMarried", false),
        DbParam("@StudentsDetails", "PERSONEL_ARSIV_KAYDI - Bu kayıt personel arşivi için otomatik oluşturulmuştur."),
        DbParam("@MotherName", ""),
        DbParam("@SchoolId", companyId),
        DbParam("@IsDeleted", true));
        return personelId;
    }

    private SqlConnection CreateConnection()
    {
        return new SqlConnection(connectionString);
    }

    private SqlParameter DbParam(string name, object value)
    {
        return new SqlParameter(name, value ?? DBNull.Value);
    }
    private SqlDataAccess Database => new(connectionString);

    private DataTable ExecuteDataTable(string query, CommandType commandType = CommandType.Text, params SqlParameter[] parameters) =>
    Database.Table(query, commandType, parameters);

    private object ExecuteScalarValue(string query, CommandType commandType = CommandType.Text, params SqlParameter[] parameters) =>
    Database.Scalar(query, commandType, parameters);

    private int ExecuteNonQueryCommand(string query, CommandType commandType = CommandType.Text, params SqlParameter[] parameters) =>
    Database.ExecuteNonQuery(query, commandType, parameters);

    private string ExecuteStringOrDefault(string query, string defaultValue = "Bilinmiyor", params SqlParameter[] parameters)
    {
        object result = ExecuteScalarValue(query, CommandType.Text, parameters);
        return result == null || result == DBNull.Value ? defaultValue: result.ToString();
    }

    private string GetStudentSearchText()
    {
        return string.IsNullOrWhiteSpace(txtOgrenciYonetimiAra.Text)
        ? null
        : txtOgrenciYonetimiAra.Text.Trim();
    }

    private int GetActiveGridTag()
    {
        if (aktifDGV?.Tag == null)
        return 0;
        return int.TryParse(aktifDGV.Tag.ToString(), out int tag) ? tag: 0;
    }

    private bool IsAdminRole(string role)
    {
        return string.Equals(role, "ADMİN", StringComparison.OrdinalIgnoreCase)
        || string.Equals(role, "ADMIN", StringComparison.OrdinalIgnoreCase);
    }

    private void RefreshStudentGrid()
    {
        dataGridViewStok.DataSource = GetStudentTable(UserId, GetStudentSearchText());
        dataGridViewStok.Refresh();
    }

    private void RefreshPersonelGrid()
    {
        dgvPersonelYonetimi.DataSource = GetPersonelTable(UserId);
        dgvPersonelYonetimi.Refresh();
    }

    private void RefreshActiveGrid()
    {
        switch (GetActiveGridTag())
        {
            case StudentModuleTag:
            RefreshStudentGrid();
            break;
            case PersonelModuleTag:
            RefreshPersonelGrid();
            break;
        }
    }

    private DataTable GetStudentTable(Guid userId, string ogrenciAdi = null)
    {
        return ExecuteDataTable(
        "EXEC GetStudent @UserId, @Name",
        CommandType.Text,
        DbParam("@UserId", userId),
        DbParam("@Name", string.IsNullOrWhiteSpace(ogrenciAdi) ? DBNull.Value: (object) ogrenciAdi));
    }

    private DataTable GetPersonelTable(Guid userId)
    {
        return ExecuteDataTable(
        "EXEC GetPersonel @UserId",
        CommandType.Text,
        DbParam("@UserId", userId));
    }

    private void LoadComboBoxItems(ComboBox comboBox, string query, Func<SqlDataReader, ComboBoxItem> map, params SqlParameter[] parameters)
    {
        var items = Database.Query(query, map, parameters);
        comboBox.BeginUpdate();
        try
        {
            comboBox.Items.Clear();
            comboBox.Items.AddRange(items.Cast<object>().ToArray());
        }
        finally
        {
            comboBox.EndUpdate();
        }
    }

    private void SoftDeleteStudent(Guid id)
    {
        ExecuteNonQueryCommand(
        "UPDATE AysStudents SET IsDeleted=1 WHERE Id=@id AND SchoolId=dbo.GetSirketIdByUserId(@UserId)",
        CommandType.Text,
        DbParam("@id", id), DbParam("@UserId", UserId));
        DeleteAndLog("Aysstudents", "Id", id, UserId, "1", "DELETE");
    }

    private void SoftDeletePersonel(Guid id)
    {
        ExecuteNonQueryCommand(
        "UPDATE Personel SET IsActive=0 WHERE PersonelId=@id AND CompanyId=dbo.GetSirketIdByUserId(@UserId)",
        CommandType.Text,
        DbParam("@id", id), DbParam("@UserId", UserId));
        DeleteAndLog("Personel", "PersonelId", id, UserId, "1", "DELETE");
    }

    private bool TryGetSelectedGuid(DataGridView grid, string columnName, out Guid id)
    {
        id = Guid.Empty;
        if (grid?.CurrentRow == null || !grid.Columns.Contains(columnName))
        return false;
        object value = grid.CurrentRow.Cells[columnName].Value;
        if (value == null || value == DBNull.Value)
        return false;
        return Guid.TryParse(value.ToString(), out id);
    }
}
