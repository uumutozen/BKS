using MaterialSkin.Controls;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BKS
{
    public partial class PersonelForm: MaterialForm
    {
        public event EventHandler RefreshData;
        Form2 _form2 = new Form2();
        public string connectionString = "Server=31.186.11.161;Database=asl2e6ancomtr_PaymentDBDB;User Id=asl2e6ancomtr_aslan;Password=Aslan123.@;TrustServerCertificate=True;";
        public PersonelForm(Form2 form2)
        {
            InitializeComponent();
            _form2 = form2;
        }
        public void PersonelForm_Load(object sender, EventArgs e)
        {
            dtpPersonelCıkısTarihi.Enabled = false;
            txtPersonelAyrilmaNedeni.Enabled = false;
            txtPersonelKidemTazminat.Enabled = false;
            txtPersonelKimlik.Enabled = false;
            lblKimlikNum.Visible = false;
            
        }

        public bool IsValidTCKimlikNo(string tc)
        {
            if (tc.Length != 11 || !tc.All(char.IsDigit) || tc.StartsWith("0"))
                return false;

            int[] digits = tc.Select(ch => int.Parse(ch.ToString())).ToArray();

            int toplam1 = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
            int toplam2 = digits[1] + digits[3] + digits[5] + digits[7];

            int onuncu = ((toplam1 * 7) - toplam2) % 10;
            int onbirinci = digits.Take(10).Sum() % 10;

            return digits[9] == onuncu && digits[10] == onbirinci;
        }
        private void pbxPersonelPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fotoğraf |*.png;*.jpeg";
            openFileDialog.Title = "Bir Fotoğraf Seçin";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbxPersonelPicture.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);

                if (pbxPersonelPicture.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pbxPersonelPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbxPersonelPicture.Image.Save(ms, pbxPersonelPicture.Image.RawFormat);
                        Photo = ms.ToArray();
                    }
                }

            }
        }
        private void btnPersonelKaydet_Click(object sender, EventArgs e)
        {

           
            if (string.IsNullOrWhiteSpace(txtPersonelKimlik.Text))
            {
                MessageBox.Show("Lütfen kimlik numarası giriniz.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPersonelMail.Text))
            {
                MessageBox.Show("Lütfen e-posta adresi giriniz.");
                return;
            }
            if (!rbtPersonelErkek.Checked && !rbtPersonelKadin.Checked)
            {
                MessageBox.Show("Lütfen cinsiyet seçiniz.");
                return;
            }
            if (!rbtPersonelEvli.Checked && !rbtPersonelBekar.Checked)
            {
                MessageBox.Show("Lütfen medeni durum seçiniz.");
                return;
            }
            if (!rbtPersonelEgitimGorevlisiEvet.Checked && !rbtPersonelEgitimGorevlisiHayir.Checked)
            {
                MessageBox.Show("Lütfen eğitim görevlisi olup olmadığını seçiniz.");
                return;
            }
            if (cbxPersonelUyruk.SelectedItem?.ToString() == "T.C")
            {
                string kimlikNo = txtPersonelKimlik.Text.Trim();

                if (!IsValidTCKimlikNo(kimlikNo))
                {
                    MessageBox.Show("Geçersiz TC Kimlik Numarası!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPersonelKimlik.Focus();
                    return;
                }
            }

            

            string Kimlik = txtPersonelKimlik.Text.Trim();
            string Mail = txtPersonelMail.Text.Trim();

            // Kayıtlı kimlik veya e-mail var mı kontrolü (SQL ile)
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Kimlik Numarası kontrolü
                using (SqlCommand checkKimlik = new SqlCommand("SELECT COUNT(*) FROM Personel WHERE IdentityNumber = @kimlik", conn))
                {
                    checkKimlik.Parameters.AddWithValue("@kimlik", Kimlik);
                    int kimlikVar = (int)checkKimlik.ExecuteScalar();
                    if (kimlikVar > 0)
                    {
                        MessageBox.Show("Bu kimlik numarası zaten kayıtlı.");
                        return;
                    }
                }

                // Email kontrolü
                using (SqlCommand checkMail = new SqlCommand("SELECT COUNT(*) FROM Personel WHERE Email = @mail", conn))
                {
                    checkMail.Parameters.AddWithValue("@mail", Mail);
                    int mailVar = (int)checkMail.ExecuteScalar();
                    if (mailVar > 0)
                    {
                        MessageBox.Show("Bu e-posta adresi zaten kayıtlı.");
                        return;
                    }
                }

                // Kayıt ekleme işlemi
                try
                {
                    Guid PersonelId = Guid.NewGuid();
                    Guid userid = UserId;
                    string Adi = txtPersonelAd.Text.Trim();
                    string Soyadi = txtPersonelSoyad.Text.Trim();
                    DateTime DogumTarihi = dtpPersonelDG.Value;
                    string Uyruk = cbxPersonelUyruk.Text.Trim();
                    string Cinsiyet = rbtPersonelErkek.Checked ? "Erkek" : rbtPersonelKadin.Checked ? "Kadın" : "";
                    bool MedeniDurum = rbtPersonelEvli.Checked ? true : false;
                    string Telefon = txtPersonelTel.Text.Trim();
                    string AcilDurumIletisim = txtPersonelIletişimAcilDurum.Text.Trim();
                    string Adres = txtPersonelAdres.Text.Trim();
                    DateTime IseBaslamaTarihi = dtpPersonelIseBaslamaTarihi.Value;
                    string Departman = txtPersonelDepartman.Text.Trim();
                    string Gorev = txtPersonelGorev.Text.Trim();
                    string CalismaSekli = cbxPersonelCalismaSekli.Text.Trim();
                    string PersonelNo = txtPersonelPersonelNo.Text.Trim();
                    string SigortaDurumu = cbxPersonelSigorta.Text.Trim();
                    decimal Maas = decimal.TryParse(txtPersonelMaas.Text.Trim(), out decimal maasVal) ? maasVal : 0;
                    decimal PrimVeEk = decimal.TryParse(txtPersonelPrimVeEk.Text.Trim(), out decimal primVal) ? primVal : 0;
                    decimal YemekYol = decimal.TryParse(txtPersonelYemekYol.Text.Trim(), out decimal yemekVal) ? yemekVal : 0;
                    string SGKSicilNumarasi = txtPersonelSGKSicilNum.Text.Trim();
                    string SaglikSigortasi = txtPersonelSaglikSigorta.Text.Trim();

                    string Emeklilik = txtPersonelEmeklilik.Text.Trim();
                    bool EğitimGörevlisiMi = rbtPersonelEgitimGorevlisiEvet.Checked ? true : false;
                    string EgitimDurumu = cbxPersonelEgitimDurumu.Text.Trim();
                    string Universite = cbxPersonelUniversite.Text.Trim();
                    string UniBolum = txtPersonelUniBolum.Text.Trim();
                    string Sertifika = txtPersonelSertifika.Text.Trim();
                    string YabanciDil = txtPersonelYabanciDil.Text.Trim();
                    bool IsAyrildi = cbxPersoneIIsAyrıldı.Checked;
                    DateTime? CikisTarihi = IsAyrildi ? dtpPersonelCıkısTarihi.Value : (DateTime?)null;
                    string AyrilmaNedeni = txtPersonelAyrilmaNedeni.Text.Trim();
                    decimal KidemTazminat = decimal.TryParse(txtPersonelKidemTazminat.Text.Trim(), out decimal kidemVal) ? kidemVal : 0;

                    SqlCommand cmd = new SqlCommand("AddPersonel", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", PersonelId);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@FirstName", Adi);
                    cmd.Parameters.AddWithValue("@LastName", Soyadi);
                    cmd.Parameters.AddWithValue("@Email", Mail);
                    cmd.Parameters.AddWithValue("@Phone", Telefon);
                    cmd.Parameters.AddWithValue("@Address", Adres);
                    cmd.Parameters.AddWithValue("@City", "");
                    cmd.Parameters.AddWithValue("@Country", "");
                    cmd.Parameters.AddWithValue("@Birthdate", DogumTarihi);
                    cmd.Parameters.AddWithValue("@Gender", Cinsiyet);
                    cmd.Parameters.AddWithValue("@IdentityNumber", Kimlik);
                    cmd.Parameters.AddWithValue("@JobTitle", Gorev);
                    cmd.Parameters.AddWithValue("@Department", Departman);
                    cmd.Parameters.AddWithValue("@HireDate", IseBaslamaTarihi);
                    cmd.Parameters.AddWithValue("@Salary", Maas);
                    cmd.Parameters.AddWithValue("@IsActive", true);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@IsTeacher", EğitimGörevlisiMi);
                    cmd.Parameters.AddWithValue("@IsMaried", MedeniDurum);
                    cmd.Parameters.AddWithValue("@Education", EgitimDurumu);
                    cmd.Parameters.AddWithValue("@IsQuitWork", IsAyrildi);
                    cmd.Parameters.AddWithValue("@QuitWorkDate", (object?)CikisTarihi ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@QuitWorkReason", AyrilmaNedeni);
                    cmd.Parameters.AddWithValue("@Compensation", KidemTazminat);
                    cmd.Parameters.AddWithValue("@AdditionalPayment", PrimVeEk);
                    cmd.Parameters.AddWithValue("@FoodandTransportFee", YemekYol);
                    cmd.Parameters.AddWithValue("@SgkSicil", SGKSicilNumarasi);
                    cmd.Parameters.AddWithValue("@SaglikSigortasiBilgileri", SaglikSigortasi);
                    cmd.Parameters.AddWithValue("@EmeklilikBilgileri", Emeklilik);
                    cmd.Parameters.AddWithValue("@PersonelNumarasi", PersonelNo);
                    cmd.Parameters.AddWithValue("@UniversityDepartment", UniBolum);
                    cmd.Parameters.AddWithValue("@SertifikaAndEducation", Sertifika);
                    cmd.Parameters.AddWithValue("@ForeignLanguage", YabanciDil);
                    cmd.Parameters.AddWithValue("@EmergenyFamilies", AcilDurumIletisim);
                    cmd.Parameters.AddWithValue("@Uyruk", Uyruk);
                    SqlParameter photoParam = new SqlParameter("@photo", SqlDbType.VarBinary, -1);
                    photoParam.Value = Photo != null ? (object)Photo : DBNull.Value;
                    cmd.Parameters.Add(photoParam);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Personel başarıyla kaydedildi.");

                    MessageBox.Show("Personel başarıyla kaydedildi.");
                    RefreshData?.Invoke(this, EventArgs.Empty); // Ekle
                    _form2.PersonelYonetimiLoad(userid);
                    this.Close(); // isteğe bağlı kapatabilirsin
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kayıt sırasında bir hata oluştu: " + ex.Message);
                }
            }
        }
        private void btnPersonelSil_Click(object sender, EventArgs e)
        {
           
         

            // Emin misiniz? diye de sorabilirsin (isteğe bağlı)
            if (MessageBox.Show("Seçili personeli silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

         
            string query = @"
UPDATE Personel SET IsActive = 0
WHERE PersonelId = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", PersonelId);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Personel bilgileri başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _form2.DeleteAndLog("Aysstudents", "Id", PersonelId, UserId, "1", "DELETE");
            RefreshData.Invoke(this, new EventArgs());
            _form2.dgvPersonelYonetimi.DataSource = _form2.LoadPersonelRefresh(UserId);
          
            this.Close();
            
        }
        private void btnPersonelGuncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPersonelKimlik.Text))
            {
                MessageBox.Show("Lütfen kimlik numarası giriniz.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPersonelMail.Text))
            {
                MessageBox.Show("Lütfen e-posta adresi giriniz.");
                return;
            }
            if (!rbtPersonelErkek.Checked && !rbtPersonelKadin.Checked)
            {
                MessageBox.Show("Lütfen cinsiyet seçiniz.");
                return;
            }
            if (!rbtPersonelEvli.Checked && !rbtPersonelBekar.Checked)
            {
                MessageBox.Show("Lütfen medeni durum seçiniz.");
                return;
            }
            if (!rbtPersonelEgitimGorevlisiEvet.Checked && !rbtPersonelEgitimGorevlisiHayir.Checked)
            {
                MessageBox.Show("Lütfen eğitim görevlisi olup olmadığını seçiniz.");
                return;
            }
            if (cbxPersonelUyruk.SelectedItem?.ToString() == "T.C")
            {
                string kimlikNo = txtPersonelKimlik.Text.Trim();

                if (!IsValidTCKimlikNo(kimlikNo))
                {
                    MessageBox.Show("Geçersiz TC Kimlik Numarası!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPersonelKimlik.Focus();
                    return;
                }
            }
            // Emin misiniz? diye de sorabilirsin (isteğe bağlı)
            if (MessageBox.Show("Seçili personeli güncellemek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            string Adi = txtPersonelAd.Text.Trim();
            string Soyadi = txtPersonelSoyad.Text.Trim();
            DateTime DogumTarihi = dtpPersonelDG.Value;
            string Uyruk = cbxPersonelUyruk.Text.Trim();
            string Kimlik = txtPersonelKimlik.Text.Trim();
            string Cinsiyet = rbtPersonelErkek.Checked ? "Erkek" : rbtPersonelKadin.Checked ? "Kadın" : "";
            bool? EgitimGorevlisi = rbtPersonelEgitimGorevlisiEvet.Checked ? true : rbtPersonelEgitimGorevlisiHayir.Checked ? false : (bool?)null;
            bool MedeniDurum = rbtPersonelEvli.Checked;
            string Telefon = txtPersonelTel.Text.Trim();
            string Mail = txtPersonelMail.Text.Trim();
            string AcilDurumIletisim = txtPersonelIletişimAcilDurum.Text.Trim();
            string Adres = txtPersonelAdres.Text.Trim();
            DateTime IseBaslamaTarihi = dtpPersonelIseBaslamaTarihi.Value;
            string Departman = txtPersonelDepartman.Text.Trim();
            string Gorev = txtPersonelGorev.Text.Trim();
            string PersonelNo = txtPersonelPersonelNo.Text.Trim();
            decimal Maas = decimal.TryParse(txtPersonelMaas.Text.Trim(), out decimal maasVal) ? maasVal : 0;
            decimal PrimVeEk = decimal.TryParse(txtPersonelPrimVeEk.Text.Trim(), out decimal primVal) ? primVal : 0;
            decimal YemekYol = decimal.TryParse(txtPersonelYemekYol.Text.Trim(), out decimal yemekVal) ? yemekVal : 0;
            string SGKSicilNumarasi = txtPersonelSGKSicilNum.Text.Trim();
            string SaglikSigortasi = txtPersonelSaglikSigorta.Text.Trim();
            string Emeklilik = txtPersonelEmeklilik.Text.Trim();
            string EgitimDurumu = cbxPersonelEgitimDurumu.Text.Trim();
            string Universite = cbxPersonelUniversite.Text.Trim();
            string UniBolum = txtPersonelUniBolum.Text.Trim();
            string Sertifika = txtPersonelSertifika.Text.Trim();
            string YabanciDil = txtPersonelYabanciDil.Text.Trim();
            bool IsAyrildi = cbxPersoneIIsAyrıldı.Checked;
            DateTime? CikisTarihi = IsAyrildi ? dtpPersonelCıkısTarihi.Value : (DateTime?)null;
            string AyrilmaNedeni = txtPersonelAyrilmaNedeni.Text.Trim();
            decimal KidemTazminat = decimal.TryParse(txtPersonelKidemTazminat.Text.Trim(), out decimal kidemVal) ? kidemVal : 0;

            string query = @"
        UPDATE Personel SET 
            FirstName = @Adi,
            LastName = @Soyadi,
            BirthDate = @DogumTarihi,
            Uyruk = @Uyruk,
            IdentityNumber = @Kimlik,
            Gender = @Cinsiyet,
            IsMaried = @MedeniDurum,
            IsTeacher = @EgitimGorevlisi,
            Phone = @Telefon,
            Email = @Mail,
            EmergenyFamilies = @AcilDurumIletisim,
            Address = @Adres,
            HireDate = @IseBaslamaTarihi,
            Department = @Departman,
            JobTitle = @Gorev,
            Salary = @Maas,
            AdditionalPayment = @PrimVeEk,
            FoodandTransportFee = @YemekYol,
            SgkSicil = @SGKSicil,
            SaglikSigortasiBilgileri = @SaglikSigortasi,
            EmeklilikBilgileri = @Emeklilik,
            Education = @EgitimDurumu,
            UniversityDepartment = @UniBolum,
            SertifikaAndEducation = @Sertifika,
            ForeignLanguage = @YabanciDil,
            IsQuitWork = @IsAyrildi,
            QuitWorkDate = @CikisTarihi,
            QuitWorkReason = @AyrilmaNedeni,
            Compensation = @KidemTazminat,
            UpdatedAt = GETDATE()
        WHERE PersonelId = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Adi", Adi);
                    cmd.Parameters.AddWithValue("@Soyadi", Soyadi);
                    cmd.Parameters.AddWithValue("@DogumTarihi", DogumTarihi);
                    cmd.Parameters.AddWithValue("@Uyruk", Uyruk);
                    cmd.Parameters.AddWithValue("@Kimlik", Kimlik);
                    cmd.Parameters.AddWithValue("@Cinsiyet", Cinsiyet);
                    cmd.Parameters.AddWithValue("@MedeniDurum", MedeniDurum);
                    cmd.Parameters.AddWithValue("@Telefon", Telefon);
                    cmd.Parameters.AddWithValue("@Mail", Mail);
                    cmd.Parameters.AddWithValue("@EgitimGorevlisi", EgitimGorevlisi);
                    cmd.Parameters.AddWithValue("@AcilDurumIletisim", AcilDurumIletisim);
                    cmd.Parameters.AddWithValue("@Adres", Adres);
                    cmd.Parameters.AddWithValue("@IseBaslamaTarihi", IseBaslamaTarihi);
                    cmd.Parameters.AddWithValue("@Departman", Departman);
                    cmd.Parameters.AddWithValue("@Gorev", Gorev);
                    cmd.Parameters.AddWithValue("@Maas", Maas);
                    cmd.Parameters.AddWithValue("@PrimVeEk", PrimVeEk);
                    cmd.Parameters.AddWithValue("@YemekYol", YemekYol);
                    cmd.Parameters.AddWithValue("@SGKSicil", SGKSicilNumarasi);
                    cmd.Parameters.AddWithValue("@SaglikSigortasi", SaglikSigortasi);
                    cmd.Parameters.AddWithValue("@Emeklilik", Emeklilik);
                    cmd.Parameters.AddWithValue("@EgitimDurumu", EgitimDurumu);
                    cmd.Parameters.AddWithValue("@UniBolum", UniBolum);
                    cmd.Parameters.AddWithValue("@Sertifika", Sertifika);
                    cmd.Parameters.AddWithValue("@YabanciDil", YabanciDil);
                    cmd.Parameters.AddWithValue("@IsAyrildi", IsAyrildi);
                    cmd.Parameters.AddWithValue("@CikisTarihi", (object?)CikisTarihi ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AyrilmaNedeni", AyrilmaNedeni);
                    cmd.Parameters.AddWithValue("@KidemTazminat", KidemTazminat);
                    cmd.Parameters.AddWithValue("@id", PersonelId);
                    cmd.ExecuteNonQuery();
                }

            }

            MessageBox.Show("Personel bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshData?.Invoke(this, EventArgs.Empty); // Ekle
            _form2.PersonelYonetimiLoad(UserId);
            this.Close(); // isteğe bağlı
        }
        private void btnPersonelTemizle_Click(object sender, EventArgs e)
        {
            // TextBox temizleme
            txtPersonelAd.Clear();
            txtPersonelSoyad.Clear();
            txtPersonelTel.Clear();
            txtPersonelIletişimAcilDurum.Clear();
            txtPersonelAdres.Clear();
            txtPersonelDepartman.Clear();
            txtPersonelGorev.Clear();
            txtPersonelPersonelNo.Clear();
            txtPersonelMaas.Clear();
            txtPersonelPrimVeEk.Clear();
            txtPersonelYemekYol.Clear();
            txtPersonelSGKSicilNum.Clear();
            txtPersonelSaglikSigorta.Clear();
            txtPersonelEmeklilik.Clear();
            txtPersonelUniBolum.Clear();
            txtPersonelSertifika.Clear();
            txtPersonelYabanciDil.Clear();
            txtPersonelAyrilmaNedeni.Clear();
            txtPersonelKidemTazminat.Clear();

            // ComboBox temizleme
            cbxPersonelUyruk.SelectedIndex = -1;
            cbxPersonelCalismaSekli.SelectedIndex = -1;
            cbxPersonelSigorta.SelectedIndex = -1;
            cbxPersonelEgitimDurumu.SelectedIndex = -1;
            cbxPersonelUniversite.SelectedIndex = -1;

            // DateTimePicker sıfırlama
            dtpPersonelDG.Value = DateTime.Now;
            dtpPersonelIseBaslamaTarihi.Value = DateTime.Now;
            dtpPersonelCıkısTarihi.Value = DateTime.Now;

            // RadioButton temizleme
            rbtPersonelErkek.Checked = false;
            rbtPersonelKadin.Checked = false;
            rbtPersonelEvli.Checked = false;
            rbtPersonelBekar.Checked = false;
            rbtPersonelEgitimGorevlisiEvet.Checked = false;
            rbtPersonelEgitimGorevlisiHayir.Checked = false;

            // CheckBox temizleme
            cbxPersoneIIsAyrıldı.Checked = false;
        }
        public void cbxPersoneIIsAyrıldı_CheckedChanged(object sender, EventArgs e)
        {
            bool ayrildi = cbxPersoneIIsAyrıldı.Checked;


            dtpPersonelCıkısTarihi.Enabled = ayrildi;
            txtPersonelAyrilmaNedeni.Enabled = ayrildi;
            txtPersonelKidemTazminat.Enabled = ayrildi;

        }

        public void cbxPersonelUyruk_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbxPersonelUyruk.SelectedItem != null && cbxPersonelUyruk.SelectedItem.ToString() == "T.C")
            {
                lblKimlikNum.Visible = true;
                txtPersonelKimlik.Enabled = true;
                lblKimlikNum.Text = "Kimlik Numarası :";

            }
            else
            {
                lblKimlikNum.Visible = true;
                txtPersonelKimlik.Enabled = true;
                lblKimlikNum.Text = "Pasaport Numarası :";

            }
        }


        public Guid UserId { get; set; }
        public Guid PersonelId{ get; set; }
        public byte[] Photo { get; set; }
    }

}
