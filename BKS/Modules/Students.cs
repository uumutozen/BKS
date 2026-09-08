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
        dataGridViewStok.DataSource = GetStudentTable(userId, GetStudentSearchText());
        dataGridViewStok.Refresh();
    }
    public DataTable LoadStockDataRefresh(Guid userId)
    {
        return GetStudentTable(userId, GetStudentSearchText());
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
    private void txtOgrenciYonetimiAra_TextChanged(object sender, EventArgs e)
    {
        RefreshStudentGrid();
    }
    private void dataGridViewStok_MouseDown(object sender, MouseEventArgs e)
    {
        DataGridView.HitTestInfo hit = dataGridViewStok.HitTest(e.X, e.Y);
        if (hit.RowIndex >= 0)
        {
            if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
            {
                dataGridViewStok.ClearSelection();
                dataGridViewStok.Rows[hit.RowIndex].Selected = true;
                dataGridViewStok.CurrentCell = dataGridViewStok.Rows[hit.RowIndex].Cells[0];
            }
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(dataGridViewStok, e.Location);
            }
        }
    }
    private void dataGridViewStok_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex<0)
        return;
        DataGridViewRow row = dataGridViewStok.Rows[e.RowIndex];
        if (!_allowedModules.Contains(tabPageStok.Name)) return;
        string key = "student:" + row.Cells["Id"].Value;
        _documents.OpenDocument(key, "Öğrenci kartı", () =>
        {
            OgrenciForm ogrForm = new OgrenciForm(this);
            ogrForm.UserId = UserId;
            ogrForm.RefreshData += DataStokRefresh;
            LoadStudentClassComboBox(ogrForm.cmbogrsınıf, UserId);
            ogrForm.txtOgrenciAd.Text = row.Cells["İsim"].Value?.ToString() ?? string.Empty;
            ogrForm.textSoyad.Text = row.Cells["Soyisim"].Value?.ToString() ?? string.Empty;
            ogrForm.txtBabaAd.Text = row.Cells["Baba Adı"].Value?.ToString() ?? string.Empty;
            ogrForm.txtAnneAd.Text = row.Cells["Anne Adı"].Value?.ToString() ?? string.Empty;
            ogrForm.cmbogrsınıf.Text = row.Cells["Sınıfı"].Value?.ToString() ?? string.Empty;
            ogrForm.textOgrenciKod.Text = row.Cells["Öğrenci Kodu"].Value?.ToString() ?? string.Empty;
            ogrForm.textOgrenciDetay.Text = row.Cells["Öğrenci Hakkında"].Value?.ToString() ?? string.Empty;
            ogrForm.txtBabaTel.Text = row.Cells["Baba Telefon"].Value?.ToString() ?? string.Empty;
            ogrForm.txtAnneTel.Text = row.Cells["Anne Telefon"].Value?.ToString() ?? string.Empty;
            ogrForm.txtBabaEvAdres.Text = row.Cells["Baba Adresi"].Value?.ToString() ?? string.Empty;
            ogrForm.txtAnneEvAdres.Text = row.Cells["Anne Adresi"].Value?.ToString() ?? string.Empty;
            if (row.Cells["FotoId"].Value is byte[] imageData && imageData.Length> 0)
            {
                using (var ms = new MemoryStream(imageData))
                {
                    using var image = Image.FromStream(ms);
                    ogrForm.pictureBox1.Image = new Bitmap(image);
                    ogrForm.Photo = imageData;
                    ogrForm.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            else
            {
                ogrForm.pictureBox1.Image = null;
            }
            ogrForm.numericPrice.Value = row.Cells["MonthlyFee"].Value != null &&
            decimal.TryParse(row.Cells["MonthlyFee"].Value.ToString(), out decimal price)
            ? price
            : 0;
            ogrForm.checkAktif.Checked = row.Cells["Aktif Öğrenci mi"].Value?.ToString() == "Evet";
            ogrForm.checkEvet.Checked = row.Cells["Aile Ayrı Mı"].Value?.ToString() == "Evet";
            ogrForm.checkOdemeDurum.Checked = row.Cells["Ödeme Durumu"].Value?.ToString() == "Ödeme Yapıldı" ||
            row.Cells["Ödeme Durumu"].Value?.ToString() == "True";
            if (row.Cells["Doğum Tarihi"].Value != null &&
            DateTime.TryParse(row.Cells["Doğum Tarihi"].Value.ToString(), out DateTime birthDate) &&
            birthDate >= ogrForm.dateDogum.MinDate && birthDate <= ogrForm.dateDogum.MaxDate)
            {
                ogrForm.dateDogum.Value = birthDate;
            }
            else
            {
                ogrForm.dateDogum.Value = DateTime.Now;
            }
            ogrForm.StudentId = (Guid) row.Cells["Id"].Value;
            return ogrForm;
        }, tabPageStok.Name);
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
                using (var arsiv = new arsivForm(UserId, connectionString, ogrenciId))
                {
                    arsiv.ShowDialog();
                }
            }
            else if (activeTag == PersonelModuleTag)
            {
                if (!TryGetSelectedGuid(dgvPersonelYonetimi, "PersonelId", out Guid personelId))
                {
                    MessageBox.Show("Geçerli personel bilgisi alınamadı.");
                    return;
                }
                Guid arsivOgrenciId = EnsurePersonelArchiveStudent(personelId);
                using (var arsiv = new arsivForm(UserId, connectionString, arsivOgrenciId))
                {
                    arsiv.ShowDialog();
                }
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
