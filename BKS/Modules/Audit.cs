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
    public void DeleteAndLog(string tableName, string primaryKeyColumn, Guid id, Guid userId, string actions, string actionName)
    {
        bool student = tableName.Equals("Aysstudents", StringComparison.OrdinalIgnoreCase) && primaryKeyColumn == "Id";
        bool personnel = tableName.Equals("Personel", StringComparison.OrdinalIgnoreCase) && primaryKeyColumn == "PersonelId";
        bool schoolClass = tableName.Equals("AYSClasses", StringComparison.OrdinalIgnoreCase) && primaryKeyColumn == "Id";
        if (!student && !personnel && !schoolClass) throw new InvalidOperationException("Geçersiz işlem geçmişi tablosu.");
        string companyColumn = personnel ? "CompanyId": "SchoolId";
        using (SqlConnection conn = CreateConnection())
        {
            conn.Open();
            string selectQuery = $"SELECT * FROM {tableName} WHERE {primaryKeyColumn} = @Id AND {companyColumn}=dbo.GetSirketIdByUserId(@UserId)";
            string deletedDataJson = string.Empty;
            using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
            {
                selectCmd.Parameters.AddWithValue("@Id", id);
                selectCmd.Parameters.AddWithValue("@UserId", userId);
                using (SqlDataReader reader = selectCmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    if (dt.Rows.Count> 0)
                    {
                        deletedDataJson = JsonConvert.SerializeObject(dt, Formatting.Indented);
                    }
                }
            }
            if (!string.IsNullOrEmpty(deletedDataJson))
            {
                string logQuery = @"INSERT INTO DeleteLog (TableName, DeletedData, DeletedAt, DeleteUserId, Actions, ActionName, CompanyId)
                                    VALUES (@TableName, @DeletedData, @DeletedAt, @DeleteUserId, @Actions, @ActionName,
                                    (SELECT TOP 1 CompanyId FROM CompanyUsers WHERE UserId = @DeleteUserId))";
                using (SqlCommand logCmd = new SqlCommand(logQuery, conn))
                {
                    logCmd.Parameters.AddWithValue("@TableName", tableName.ToUpper());
                    logCmd.Parameters.AddWithValue("@DeletedData", deletedDataJson);
                    logCmd.Parameters.AddWithValue("@DeletedAt", DateTime.Now);
                    logCmd.Parameters.AddWithValue("@DeleteUserId", userId);
                    logCmd.Parameters.AddWithValue("@Actions", actions);
                    logCmd.Parameters.AddWithValue("@ActionName", actionName.ToUpper());
                    logCmd.ExecuteNonQuery();
                }
            }
        }
    }
    private void loglarıGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ToolStripItem menuItem = sender as ToolStripItem;
        if (menuItem?.Owner is not ContextMenuStrip owner)
        return;
        if (owner.SourceControl is not DataGridView sourceDgv)
        return;
        int modulCode = sourceDgv.Tag != null
        ? Convert.ToInt32(sourceDgv.Tag)
        : sourceDgv.Name == "dataGridViewStok" ? StudentModuleTag: 0;
        string permission = modulCode == PersonelModuleTag ? tabPagePersonelYonetimi.Name : tabPageStok.Name;
        if (!_allowedModules.Contains(permission)) return;
        string title = modulCode == PersonelModuleTag ? "Personel işlem geçmişi" : "Öğrenci işlem geçmişi";
        _documents.OpenDocument("history:" + modulCode, title, () => new DataListForm(title, () => ExecuteDataTable(
            @"SELECT * FROM deleteandlogs
              WHERE SirketId = (SELECT TOP 1 CompanyId FROM CompanyUsers WHERE UserId = @UserId)
                AND ModulKod = @ModulCode
              ORDER BY SilinmeZamani DESC",
            CommandType.Text, DbParam("@UserId", UserId), DbParam("@ModulCode", modulCode))), permission);
    }

    private void DeleteStripMenuItem_Click(object sender, EventArgs e)
    {
        int activeTag = GetActiveGridTag();
        if (activeTag == StudentModuleTag)
        {
            if (!TryGetSelectedGuid(dataGridViewStok, "Id", out Guid studentId))
            return;
            DialogResult result = MessageBox.Show("Bu Öğrenciyi Silmek İstiyor musunuz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                SoftDeleteStudent(studentId);
                MessageBox.Show("Öğrenci Silindi Eski Kayıtlar için Loglara Bak..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshStudentGrid();
            }
        }
        else if (activeTag == PersonelModuleTag)
        {
            if (!TryGetSelectedGuid(dgvPersonelYonetimi, "PersonelId", out Guid personelId))
            return;
            DialogResult result = MessageBox.Show("Bu Personeli Silmek İstiyor musunuz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                SoftDeletePersonel(personelId);
                MessageBox.Show("Personel Silindi Eski Kayıtlar için Loglara Bak..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshPersonelGrid();
            }
        }
    }
}
