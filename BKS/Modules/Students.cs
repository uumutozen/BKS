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
    public void LoadStockData(Guid userId)
    {
        dataGridViewStok.DataSource = GetStudentTable(userId);
        dataGridViewStok.Refresh();
    }
    public DataTable LoadStockDataRefresh(Guid userId)
    {
        return GetStudentTable(userId);
    }
    private void Timer1_Tick(object sender, EventArgs e)
    {
        LoadStockData(UserId);
    }
    public void LoadStockComboBox()
    {
        LoadComboBoxItems(comboBoxStok,
        "SELECT Id, Name FROM AYSStudents WHERE SchoolId=dbo.GetSirketIdByUserId(@UserId) AND ISNULL(IsDeleted,0)=0 ORDER BY Name",
        reader => new ComboBoxItem
        {
            Text = reader["Name"].ToString(),
            Value = reader["Id"].ToString()
        }, DbParam("@UserId", UserId));
    }
    private void LoadStudentClassComboBox(ComboBox targetComboBox, Guid userId)
    {
        if (targetComboBox == null)
        return;
        LoadComboBoxItems(
        targetComboBox,
        "SELECT ClassName, [Group] FROM AYSClasses WHERE SchoolId = (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId) AND ISNULL(IsDeleted,0)=0 ORDER BY ClassName",
        reader => new ComboBoxItem
        {
            Text = reader["ClassName"].ToString(),
            Value = reader["Group"].ToString()
        },
        DbParam("@UserId", userId));
    }
    private void btnOgrenciYonetimiSil_Click(object sender, EventArgs e)
    {
        if (!TryGetSelectedGuid(dataGridViewStok, "Id", out Guid id))
        return;
        SoftDeleteStudent(id);
        MessageBox.Show("Öğrenci Silindi Eski Kayıtlar için Loglara Bak..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        RefreshStudentGrid();
    }
    private void btnOgrenciYonetimiAra_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshStudentGrid();
        }
        catch (Exception)
        {
            MessageBox.Show("Hatalı Veri Girişi", "Hata");
        }
    }
    private void txtOgrenciYonetimiAra_TextChanged(object sender, EventArgs e) =>
        ModernWinForms.ApplySearchFilter(dataGridViewStok, txtOgrenciYonetimiAra.Text);
    private void dataGridViewStok_MouseDown(object sender, MouseEventArgs e)
    {
        DataGridView.HitTestInfo hit = dataGridViewStok.HitTest(e.X, e.Y);
        if (hit.RowIndex >= 0)
        {
            if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
            {
                dataGridViewStok.ClearSelection();
                dataGridViewStok.Rows[hit.RowIndex].Selected = true;
                var first = dataGridViewStok.Columns.Cast<DataGridViewColumn>().FirstOrDefault(column => column.Visible);
                if (first != null) dataGridViewStok.CurrentCell = dataGridViewStok.Rows[hit.RowIndex].Cells[first.Index];
            }
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(dataGridViewStok, e.Location);
            }
        }
    }
    private void dataGridViewStok_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dataGridViewStok.Rows.Count || ! _allowedModules.Contains(tabPageStok.Name)) return;
        UiActions.Run(() =>
        {
            var record = RecordFieldBinder.Snapshot(dataGridViewStok.Rows[e.RowIndex]);
            var id = record.RequiredId("Id");
            string name = (record.Get("İsim", string.Empty) + " " + record.Get("Soyisim", string.Empty)).Trim();
            _documents.OpenDocument("student:" + id, "Öğrenci: " + name, () =>
            {
                var form = new OgrenciForm(this) { UserId = UserId };
                try
                {
                    form.RefreshData += DataStokRefresh;
                    StudentRecordMapper.Fill(form, record);
                    return form;
                }
                catch { form.Dispose(); throw; }
            }, tabPageStok.Name);
        });
    }
    private void ödemeDetaylarıToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
        if (!TryGetSelectedGuid(dataGridViewStok, "Id", out Guid selectedStudentId))
        {
            MessageBox.Show("Uyarı", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        ShowPaymentDetails(selectedStudentId);
    }
    private void arşivToolStripMenuItem_Click(object sender, EventArgs e)
    {
        int activeTag = GetActiveGridTag();
        if (aktifDGV == null || aktifDGV.CurrentRow == null)
        {
            MessageBox.Show("Lütfen bir kayıt seçiniz!");
            return;
        }
        try
        {
            if (activeTag == StudentModuleTag)
            {
                if (!TryGetSelectedGuid(dataGridViewStok, "Id", out Guid ogrenciId))
                {
                    MessageBox.Show("Geçerli öğrenci bilgisi alınamadı.");
                    return;
                }
                _documents.OpenDocument("archive:student:" + ogrenciId, "Öğrenci evrak arşivi",
                    () => new arsivForm(UserId, connectionString, ogrenciId), tabPageStok.Name);
            }
            else if (activeTag == PersonelModuleTag)
            {
                if (!TryGetSelectedGuid(dgvPersonelYonetimi, "PersonelId", out Guid personelId))
                {
                    MessageBox.Show("Geçerli personel bilgisi alınamadı.");
                    return;
                }
                _documents.OpenDocument("archive:personnel:" + personelId, "Personel evrak arşivi",
                    () => new arsivForm(UserId, connectionString, EnsurePersonelArchiveStudent(personelId)),
                    tabPagePersonelYonetimi.Name);
            }
            else
            {
                MessageBox.Show("Bu alanda arşiv işlemi desteklenmiyor.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Arşiv açılırken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
