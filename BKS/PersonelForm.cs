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
            BuildModernPersonnelFormLayout();
        }
        private void BuildModernPersonnelFormLayout()
        {
            ModernWinForms.StyleForm(this, "Personel Kartı", new Size(1040, 760));
            KeyPreview = true;

            ModernWinForms.HideLegacyButton(btnPersonelKaydet);
            ModernWinForms.HideLegacyButton(btnPersonelGuncelle);
            ModernWinForms.HideLegacyButton(btnPersonelSil);
            ModernWinForms.HideLegacyButton(btnPersonelTemizle);

            var root = ModernWinForms.CreatePageLayout(3);
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 76));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var header = ModernWinForms.CreateCard("personnelFormHeader");
            header.Controls.Add(ModernWinForms.CreateSubtitle("Kimlik, iletişim, özlük ve eğitim bilgileri responsive kart düzenine taşındı."));
            header.Controls.Add(ModernWinForms.CreateTitle("Personel Kartı"));
            root.Controls.Add(header, 0, 0);

            var commandCard = ModernWinForms.CreateCard("personnelFormCommands", 0);
            var strip = ModernWinForms.CreateCommandStrip("personnelFormCommandStrip");
            strip.Items.Add(ModernWinForms.CreateCommand("Ctrl+S Kaydet", (s, e) => RunPersonnelSave()));
            strip.Items.Add(ModernWinForms.CreateCommand("Ctrl+U Güncelle", (s, e) => RunPersonnelUpdate()));
            strip.Items.Add(ModernWinForms.CreateCommand("Delete Sil", (s, e) => RunPersonnelDelete()));
            strip.Items.Add(ModernWinForms.CreateCommand("Temizle", (s, e) => RunPersonnelClear()));
            strip.Items.Add(new ToolStripSeparator());
            strip.Items.Add(new ToolStripLabel("Fotoğrafa tıklayarak personel görseli seçebilirsin."));
            commandCard.Controls.Add(strip);
            root.Controls.Add(commandCard, 0, 1);

            var scroll = new Panel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = Color.Transparent };
            var content = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 2,
                RowCount = 4,
                BackColor = Color.Transparent,
                MinimumSize = new Size(960, 680)
            };
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 230));
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 190));
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 230));
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));

            ModernWinForms.StyleGroupBox(groupBox22, "Kimlik Bilgileri");
            var identityLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            identityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 165));
            identityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            pbxPersonelPicture.Dock = DockStyle.Fill;
            pbxPersonelPicture.Margin = new Padding(0, 0, 14, 0);
            pbxPersonelPicture.SizeMode = PictureBoxSizeMode.Zoom;
            pbxPersonelPicture.BackColor = Color.FromArgb(226, 232, 240);
            var identityFlow = ModernWinForms.CreateFlow("personnelIdentityFlow", true);
            ModernWinForms.StyleInput(txtPersonelAd, 190);
            ModernWinForms.StyleInput(txtPersonelSoyad, 190);
            ModernWinForms.StyleInput(cbxPersonelUyruk, 190);
            ModernWinForms.StyleInput(txtPersonelKimlik, 190);
            ModernWinForms.StyleInput(dtpPersonelDG, 190);
            PrepareRadioPanel(panel1, 190, 58);
            PrepareRadioPanel(panel2, 190, 58);
            identityFlow.Controls.AddRange(new Control[] { txtPersonelAd, txtPersonelSoyad, cbxPersonelUyruk, txtPersonelKimlik, dtpPersonelDG, panel1, panel2 });
            identityLayout.Controls.Add(pbxPersonelPicture, 0, 0);
            identityLayout.Controls.Add(identityFlow, 1, 0);
            groupBox22.Controls.Clear();
            groupBox22.Controls.Add(identityLayout);
            content.Controls.Add(groupBox22, 0, 0);

            ModernWinForms.StyleGroupBox(groupBox25, "İletişim Bilgileri");
            var contactFlow = ModernWinForms.CreateFlow("personnelContactFlow", true);
            ModernWinForms.StyleInput(txtPersonelTel, 220);
            ModernWinForms.StyleInput(txtPersonelMail, 260);
            ModernWinForms.StyleInput(txtPersonelIletişimAcilDurum, 360);
            ModernWinForms.StyleInput(txtPersonelAdres, 480, 82);
            contactFlow.Controls.AddRange(new Control[] { txtPersonelTel, txtPersonelMail, txtPersonelIletişimAcilDurum, txtPersonelAdres });
            groupBox25.Controls.Clear();
            groupBox25.Controls.Add(contactFlow);
            content.Controls.Add(groupBox25, 1, 0);

            ModernWinForms.StyleGroupBox(groupBox18, "Özlük / Eğitim / Finans Bilgileri");
            var personnelDetails = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                BackColor = Color.Transparent
            };
            personnelDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            personnelDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            personnelDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            personnelDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            var workCard = CreateInnerPersonnelCard("Çalışma Bilgileri", new Control[]
            {
                dtpPersonelIseBaslamaTarihi, txtPersonelDepartman, txtPersonelGorev, cbxPersonelCalismaSekli,
                txtPersonelPersonelNo, cbxPersonelSigorta
            });
            var educationCard = CreateInnerPersonnelCard("Eğitim Bilgileri", new Control[]
            {
                panel3, cbxPersonelEgitimDurumu, cbxPersonelUniversite, txtPersonelUniBolum,
                txtPersonelSertifika, txtPersonelYabanciDil
            });
            var salaryCard = CreateInnerPersonnelCard("Maaş / Yan Haklar", new Control[]
            {
                txtPersonelMaas, txtPersonelPrimVeEk, txtPersonelYemekYol,
                txtPersonelSGKSicilNum, txtPersonelSaglikSigorta, txtPersonelEmeklilik
            });
            var exitCard = CreateInnerPersonnelCard("Ayrılış Bilgileri", new Control[]
            {
                cbxPersoneIIsAyrıldı, dtpPersonelCıkısTarihi, txtPersonelAyrilmaNedeni, txtPersonelKidemTazminat
            });
            personnelDetails.Controls.Add(workCard, 0, 0);
            personnelDetails.Controls.Add(educationCard, 1, 0);
            personnelDetails.Controls.Add(salaryCard, 0, 1);
            personnelDetails.Controls.Add(exitCard, 1, 1);
            groupBox18.Controls.Clear();
            groupBox18.Controls.Add(personnelDetails);
            content.SetColumnSpan(groupBox18, 2);
            content.Controls.Add(groupBox18, 0, 1);
            content.SetRowSpan(groupBox18, 2);

            var menu = new ContextMenuStrip();
            menu.Items.Add("Kaydet", null, (s, e) => RunPersonnelSave());
            menu.Items.Add("Güncelle", null, (s, e) => RunPersonnelUpdate());
            menu.Items.Add("Sil", null, (s, e) => RunPersonnelDelete());
            menu.Items.Add("Temizle", null, (s, e) => RunPersonnelClear());
            ContextMenuStrip = menu;
            scroll.ContextMenuStrip = menu;

            KeyDown += (s, e) =>
            {
                if (e.Control && e.KeyCode == Keys.S) { RunPersonnelSave(); e.Handled = true; }
                if (e.Control && e.KeyCode == Keys.U) { RunPersonnelUpdate(); e.Handled = true; }
                if (e.KeyCode == Keys.Delete) { RunPersonnelDelete(); e.Handled = true; }
            };

            scroll.Controls.Add(content);
            root.Controls.Add(scroll, 0, 2);
            Controls.Add(root);
            root.BringToFront();
            ModernWinForms.UseSegoeRecursive(this);
        }

        private Panel CreateInnerPersonnelCard(string title, Control[] controls)
        {
            var panel = ModernWinForms.CreateCard("personnelInner" + title.Replace(" ", ""));
            panel.Dock = DockStyle.Fill;
            panel.Margin = new Padding(0, 0, 12, 12);
            var label = new Label
            {
                Dock = DockStyle.Top,
                Height = 24,
                Text = title,
                ForeColor = ModernWinForms.PrimaryDark,
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 162)
            };
            var flow = ModernWinForms.CreateFlow("personnelFlow" + title.Replace(" ", ""), true);
            flow.Dock = DockStyle.Fill;
            flow.Padding = new Padding(0, 8, 0, 0);
            foreach (var control in controls)
            {
                if (control is CheckBox || control is RadioButton)
                    ModernWinForms.StyleCheck(control);
                else if (control is Panel p)
                    PrepareRadioPanel(p, 190, 58);
                else
                    ModernWinForms.StyleInput(control, control is TextBox tb && tb.Multiline ? 330 : 190, control is TextBox t && t.Multiline ? 74 : 34);
                flow.Controls.Add(control);
            }
            panel.Controls.Add(flow);
            panel.Controls.Add(label);
            return panel;
        }

        private void PrepareRadioPanel(Panel panel, int width, int height)
        {
            panel.Width = width;
            panel.Height = height;
            panel.Margin = new Padding(0, 0, 12, 12);
            panel.BackColor = Color.FromArgb(248, 250, 252);
            foreach (Control child in panel.Controls)
            {
                if (child is RadioButton || child is Label)
                    child.ForeColor = ModernWinForms.Text;
            }
        }

        private void RunPersonnelSave() => btnPersonelKaydet_Click(btnPersonelKaydet, EventArgs.Empty);
        private void RunPersonnelUpdate() => btnPersonelGuncelle_Click(btnPersonelGuncelle, EventArgs.Empty);
        private void RunPersonnelDelete() => btnPersonelSil_Click(btnPersonelSil, EventArgs.Empty);
        private void RunPersonnelClear() => btnPersonelTemizle_Click(btnPersonelTemizle, EventArgs.Empty);

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
