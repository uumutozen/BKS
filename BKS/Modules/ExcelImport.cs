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
    private void excelAktarToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var importedIds = new List<(string Table, string Key, Guid Id)>();
        try
        {
            if (aktifDGV == null || aktifDGV.Rows.Count == 0)
            return;
            using (OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Dosyaları|*.xlsx",
                Title = "Bir Excel Dosyası Seçin"
            })
            {
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
                string dosyaYolu = openFileDialog.FileName;
                FileInfo fi = new FileInfo(dosyaYolu);
                using (var package = new XlsxWorkbook(fi))
                {
                    var worksheet = package.FirstSheet;
                    int row = 2;
                    int tag = GetActiveGridTag();
                    using (SqlConnection conn = CreateConnection())
                    {
                        conn.Open();
                        using (SqlTransaction tran = conn.BeginTransaction())
                        {
                            try
                            {
                                if (tag == StudentModuleTag)
                                {
                                    while (!string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].Text))
                                    {
                                        Guid ogrenciId = Guid.NewGuid();
                                        string ogrenciName = worksheet.Cells[row, 1].Text;
                                        string ogrenciSurname = worksheet.Cells[row, 2].Text;
                                        string fatherName = worksheet.Cells[row, 3].Text;
                                        DateTime birthDate = DateTime.Parse(worksheet.Cells[row, 4].Text);
                                        string studentCode = worksheet.Cells[row, 5].Text;
                                        bool paymentStatus = bool.Parse(worksheet.Cells[row, 6].Text);
                                        decimal monthlyFee = decimal.Parse(worksheet.Cells[row, 7].Text);
                                        bool isActive = bool.Parse(worksheet.Cells[row, 8].Text);
                                        string className = worksheet.Cells[row, 9].Text;
                                        string motherName = worksheet.Cells[row, 10].Text;
                                        string fatherAddress = worksheet.Cells[row, 11].Text;
                                        string motherAddress = worksheet.Cells[row, 12].Text;
                                        string fatherPhoneNumber = worksheet.Cells[row, 13].Text;
                                        string ogrenciDetails = worksheet.Cells[row, 14].Text;
                                        string motherPhoneNumber = worksheet.Cells[row, 15].Text;
                                        bool isMarried = string.IsNullOrEmpty(worksheet.Cells[row, 16].Text) || bool.Parse(worksheet.Cells[row, 16].Text);
                                        using (SqlCommand cmd = new SqlCommand(@"INSERT INTO AysStudents 
                                            (Id, Name, Surname, FatherName, BirthDate, StudentCode, ClassId, PaymentStatus, MonthlyFee, IsActive,
                                             FatherAddress, MotherAddress, FatherPhoneNumber, MotherPhoneNumber, IsMarried, StudentsDetails, MotherName, SchoolId)
                                            VALUES
                                            (@Id, @Name, @Surname, @FatherName, @BirthDate, @StudentCode,
                                             (SELECT TOP 1 Id FROM AYSClasses WHERE ClassName = @ClassName),
                                             @PaymentStatus, @MonthlyFee, @IsActive, @FatherAddress, @MotherAddress,
                                             @FatherPhoneNumber, @MotherPhoneNumber, @IsMarried, @StudentsDetails, @MotherName,
                                             (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId))",
                                        conn, tran))
                                        {
                                            cmd.Parameters.AddWithValue("@Id", ogrenciId);
                                            cmd.Parameters.AddWithValue("@Name", ogrenciName);
                                            cmd.Parameters.AddWithValue("@Surname", ogrenciSurname);
                                            cmd.Parameters.AddWithValue("@FatherName", fatherName);
                                            cmd.Parameters.AddWithValue("@BirthDate", birthDate);
                                            cmd.Parameters.AddWithValue("@StudentCode", studentCode);
                                            cmd.Parameters.AddWithValue("@ClassName", className);
                                            cmd.Parameters.AddWithValue("@PaymentStatus", paymentStatus);
                                            cmd.Parameters.AddWithValue("@MonthlyFee", monthlyFee);
                                            cmd.Parameters.AddWithValue("@IsActive", isActive);
                                            cmd.Parameters.AddWithValue("@MotherName", motherName);
                                            cmd.Parameters.AddWithValue("@FatherAddress", fatherAddress);
                                            cmd.Parameters.AddWithValue("@MotherAddress", motherAddress);
                                            cmd.Parameters.AddWithValue("@FatherPhoneNumber", fatherPhoneNumber);
                                            cmd.Parameters.AddWithValue("@StudentsDetails", ogrenciDetails);
                                            cmd.Parameters.AddWithValue("@MotherPhoneNumber", motherPhoneNumber);
                                            cmd.Parameters.AddWithValue("@IsMarried", isMarried);
                                            cmd.Parameters.AddWithValue("@UserId", UserId);
                                            cmd.ExecuteNonQuery();
                                        }
                                        importedIds.Add(("AYSSTUDENTS", "Id", ogrenciId));
                                        row++;
                                    }
                                }
                                else if (tag == PersonelModuleTag)
                                {
                                    while (!string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].Text))
                                    {
                                        Guid personelId = Guid.NewGuid();
                                        string firstName = worksheet.Cells[row, 1].Text;
                                        string lastName = worksheet.Cells[row, 2].Text;
                                        string email = worksheet.Cells[row, 3].Text;
                                        string phone = worksheet.Cells[row, 4].Text;
                                        string address = worksheet.Cells[row, 5].Text;
                                        string city = worksheet.Cells[row, 6].Text;
                                        string country = worksheet.Cells[row, 7].Text;
                                        DateTime birthDate = DateTime.Parse(worksheet.Cells[row, 8].Text);
                                        string gender = worksheet.Cells[row, 9].Text;
                                        string identityNumber = worksheet.Cells[row, 10].Text;
                                        string jobTitle = worksheet.Cells[row, 11].Text;
                                        string department = worksheet.Cells[row, 12].Text;
                                        DateTime hireDate = DateTime.Parse(worksheet.Cells[row, 13].Text);
                                        decimal salary = decimal.Parse(worksheet.Cells[row, 14].Text);
                                        bool isTeacher = worksheet.Cells[row, 15].Text == "1";
                                        string isMaried = worksheet.Cells[row, 16].Text;
                                        string education = worksheet.Cells[row, 17].Text;
                                        bool isQuitWork = worksheet.Cells[row, 18].Text == "1";
                                        DateTime? quitWorkDate = string.IsNullOrEmpty(worksheet.Cells[row, 19].Text) ? (DateTime?) null: DateTime.Parse(worksheet.Cells[row, 19].Text);
                                        string quitWorkReason = worksheet.Cells[row, 20].Text;
                                        decimal compensation = decimal.TryParse(worksheet.Cells[row, 21].Text, out var comp) ? comp: 0;
                                        decimal additionalPayment = decimal.TryParse(worksheet.Cells[row, 22].Text, out var ap) ? ap: 0;
                                        decimal foodTransport = decimal.TryParse(worksheet.Cells[row, 23].Text, out var ft) ? ft: 0;
                                        string sgkSicil = worksheet.Cells[row, 24].Text;
                                        string saglikSigorta = worksheet.Cells[row, 25].Text;
                                        string emeklilik = worksheet.Cells[row, 26].Text;
                                        string personelNo = worksheet.Cells[row, 27].Text;
                                        string uniDept = worksheet.Cells[row, 28].Text;
                                        string sertifikalar = worksheet.Cells[row, 29].Text;
                                        string language = worksheet.Cells[row, 30].Text;
                                        string emergency = worksheet.Cells[row, 31].Text;
                                        string uyruk = worksheet.Cells[row, 32].Text;
                                        using (SqlCommand cmd = new SqlCommand("[asl2e6ancomtr_aslan].[AddPersonel]", conn, tran))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@Id", personelId);
                                            cmd.Parameters.AddWithValue("@userid", UserId);
                                            cmd.Parameters.AddWithValue("@FirstName", firstName);
                                            cmd.Parameters.AddWithValue("@LastName", lastName);
                                            cmd.Parameters.AddWithValue("@Email", email);
                                            cmd.Parameters.AddWithValue("@Phone", phone);
                                            cmd.Parameters.AddWithValue("@Address", address);
                                            cmd.Parameters.AddWithValue("@City", city);
                                            cmd.Parameters.AddWithValue("@Country", country);
                                            cmd.Parameters.AddWithValue("@Birthdate", birthDate);
                                            cmd.Parameters.AddWithValue("@Gender", gender);
                                            cmd.Parameters.AddWithValue("@IdentityNumber", identityNumber);
                                            cmd.Parameters.AddWithValue("@JobTitle", jobTitle);
                                            cmd.Parameters.AddWithValue("@Department", department);
                                            cmd.Parameters.AddWithValue("@HireDate", hireDate);
                                            cmd.Parameters.AddWithValue("@Salary", salary);
                                            cmd.Parameters.AddWithValue("@IsActive", true);
                                            cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                                            cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                                            cmd.Parameters.AddWithValue("@IsTeacher", isTeacher);
                                            SqlParameter photoParam = new SqlParameter("@photo", SqlDbType.VarBinary, - 1)
                                            {
                                                Value = Photo != null ? (object) Photo: DBNull.Value
                                            };
                                            cmd.Parameters.Add(photoParam);
                                            cmd.Parameters.AddWithValue("@IsMaried", isMaried);
                                            cmd.Parameters.AddWithValue("@Education", education);
                                            cmd.Parameters.AddWithValue("@IsQuitWork", isQuitWork);
                                            cmd.Parameters.AddWithValue("@QuitWorkDate", (object?) quitWorkDate ?? DBNull.Value);
                                            cmd.Parameters.AddWithValue("@QuitWorkReason", quitWorkReason);
                                            cmd.Parameters.AddWithValue("@Compensation", compensation);
                                            cmd.Parameters.AddWithValue("@AdditionalPayment", additionalPayment);
                                            cmd.Parameters.AddWithValue("@FoodandTransportFee", foodTransport);
                                            cmd.Parameters.AddWithValue("@SgkSicil", sgkSicil);
                                            cmd.Parameters.AddWithValue("@SaglikSigortasiBilgileri", saglikSigorta);
                                            cmd.Parameters.AddWithValue("@EmeklilikBilgileri", emeklilik);
                                            cmd.Parameters.AddWithValue("@PersonelNumarasi", personelNo);
                                            cmd.Parameters.AddWithValue("@UniversityDepartment", uniDept);
                                            cmd.Parameters.AddWithValue("@SertifikaAndEducation", sertifikalar);
                                            cmd.Parameters.AddWithValue("@ForeignLanguage", language);
                                            cmd.Parameters.AddWithValue("@EmergenyFamilies", emergency);
                                            cmd.Parameters.AddWithValue("@Uyruk", uyruk);
                                            cmd.ExecuteNonQuery();
                                        }
                                        importedIds.Add(("PERSONEL", "PersonelId", personelId));
                                        row++;
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
                    if (tag == StudentModuleTag)
                    RefreshStudentGrid();
                    else if (tag == PersonelModuleTag)
                    RefreshPersonelGrid();
                }
            }
            foreach (var row in importedIds)
            {
                try
                {
                    DeleteAndLog(row.Table, row.Key, row.Id, UserId, "0", "INSERT");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Aktarım tamamlandı; işlem geçmişi yazılamadı: " + UiActions.SafeMessage(ex));
                    break;
                }
            }
            MessageBox.Show("Excel verisi başarıyla aktarıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Excel verisi aktarılamadı! " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
