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
    public DataTable LoadPersonelRefresh(Guid userId)
    {
        return GetPersonelTable(userId);
    }
    public void PersonelYonetimiLoad(Guid userId)
    {
        dgvPersonelYonetimi.DataSource = GetPersonelTable(userId);
    }
    private void LoadTeacherComboBox(Guid userId)
    {
        LoadComboBoxItems(
        cbxOgrenciYonetimiOgretmen,
        @"SELECT isim = (FirstName + ' ' + LastName), PersonelId
              FROM personel
              WHERE CompanyId = (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId)
                AND IsTeacher = 1
                AND ISNULL(IsActive,1)=1
              ORDER BY FirstName, LastName",
        reader => new ComboBoxItem
        {
            Text = reader["isim"].ToString(),
            Value = reader["PersonelId"].ToString()
        },
        DbParam("@UserId", userId));
    }
    private void dataGridViewPersonel_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvPersonelYonetimi.Rows.Count || ! _allowedModules.Contains(tabPagePersonelYonetimi.Name)) return;
        UiActions.Run(() =>
        {
            var record = RecordFieldBinder.Snapshot(dgvPersonelYonetimi.Rows[e.RowIndex]);
            var id = record.RequiredId("PersonelId");
            string name = (record.Get("Adı", string.Empty) + " " + record.Get("Soyadı", string.Empty)).Trim();
            _documents.OpenDocument("personnel:" + id, "Personel: " + name, () =>
            {
                var form = new PersonelForm(this) { UserId = UserId };
                try
                {
                    form.RefreshData += LoadPersonelRefreshEvent;
                    PersonnelRecordMapper.Fill(form, record);
                    return form;
                }
                catch { form.Dispose(); throw; }
            }, tabPagePersonelYonetimi.Name);
        });
    }

}
