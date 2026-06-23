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
    public partial class OgrenciForm : MaterialForm
    {
        public event EventHandler RefreshData;
        private Form2 _form2;
        public OgrenciForm(Form2 form2)
        {

            _form2 = form2;
            InitializeComponent();
            BuildModernStudentFormLayout();
        }
        private void BuildModernStudentFormLayout()
        {
            ModernWinForms.StyleForm(this, "Öğrenci Kartı", new Size(920, 680));
            KeyPreview = true;

            ModernWinForms.HideLegacyButton(btnAddStock);
            ModernWinForms.HideLegacyButton(btnGuncelle);
            ModernWinForms.HideLegacyButton(btnOgrenciYonetimiSil);

            var root = ModernWinForms.CreatePageLayout(3);
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 76));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var header = ModernWinForms.CreateCard("studentFormHeader");
            header.Controls.Add(ModernWinForms.CreateSubtitle("Öğrenci bilgisi, veli iletişimi ve ödeme durumları tek responsive kartta düzenlendi."));
            header.Controls.Add(ModernWinForms.CreateTitle("Öğrenci Kartı"));
            root.Controls.Add(header, 0, 0);

            var commandCard = ModernWinForms.CreateCard("studentFormCommands", 0);
            var strip = ModernWinForms.CreateCommandStrip("studentFormCommandStrip");
            strip.Items.Add(ModernWinForms.CreateCommand("Ctrl+S Kaydet", (s, e) => RunStudentSave()));
            strip.Items.Add(ModernWinForms.CreateCommand("Ctrl+U Güncelle", (s, e) => RunStudentUpdate()));
            strip.Items.Add(ModernWinForms.CreateCommand("Delete Sil", (s, e) => RunStudentDelete()));
            strip.Items.Add(new ToolStripSeparator());
            strip.Items.Add(new ToolStripLabel("Fotoğraf alanına tıklayarak öğrenci görseli seçebilirsin."));
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
                MinimumSize = new Size(840, 560)
            };
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 220));
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 210));
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 116));
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));

            ModernWinForms.StyleGroupBox(groupBox1, "Öğrenci Bilgileri");
            var identityLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            identityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 165));
            identityLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Margin = new Padding(0, 0, 14, 0);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.BackColor = Color.FromArgb(226, 232, 240);
            var infoFlow = ModernWinForms.CreateFlow("studentIdentityFlow", true);
            ModernWinForms.StyleInput(txtOgrenciAd, 220);
            ModernWinForms.StyleInput(textSoyad, 220);
            ModernWinForms.StyleInput(dateDogum, 220);
            ModernWinForms.StyleInput(textOgrenciKod, 220);
            ModernWinForms.StyleInput(cmbogrsınıf, 220);
            ModernWinForms.StyleInput(textOgrenciDetay, 460, 82);
            textOgrenciDetay.PlaceholderText = "Öğrenci notu / özel durum / açıklama";
            infoFlow.Controls.AddRange(new Control[] { txtOgrenciAd, textSoyad, dateDogum, textOgrenciKod, cmbogrsınıf, textOgrenciDetay });
            identityLayout.Controls.Add(pictureBox1, 0, 0);
            identityLayout.Controls.Add(infoFlow, 1, 0);
            groupBox1.Controls.Clear();
            groupBox1.Controls.Add(identityLayout);
            content.SetColumnSpan(groupBox1, 2);
            content.Controls.Add(groupBox1, 0, 0);

            ModernWinForms.StyleGroupBox(groupBox2, "Baba Bilgileri");
            var fatherFlow = ModernWinForms.CreateFlow("fatherFlow", true);
            ModernWinForms.StyleInput(txtBabaAd, 220);
            ModernWinForms.StyleInput(txtBabaTel, 220);
            ModernWinForms.StyleInput(txtBabaEvAdres, 460, 92);
            fatherFlow.Controls.AddRange(new Control[] { txtBabaAd, txtBabaTel, txtBabaEvAdres });
            groupBox2.Controls.Clear();
            groupBox2.Controls.Add(fatherFlow);
            content.Controls.Add(groupBox2, 0, 1);

            ModernWinForms.StyleGroupBox(groupBox10, "Anne Bilgileri");
            var motherFlow = ModernWinForms.CreateFlow("motherFlow", true);
            ModernWinForms.StyleInput(txtAnneAd, 220);
            ModernWinForms.StyleInput(txtAnneTel, 220);
            ModernWinForms.StyleInput(txtAnneEvAdres, 460, 92);
            motherFlow.Controls.AddRange(new Control[] { txtAnneAd, txtAnneTel, txtAnneEvAdres });
            groupBox10.Controls.Clear();
            groupBox10.Controls.Add(motherFlow);
            content.Controls.Add(groupBox10, 1, 1);

            var statusCard = ModernWinForms.CreateCard("studentStatusCard");
            var statusFlow = ModernWinForms.CreateFlow("studentStatusFlow", true);
            ModernWinForms.StyleGroupBox(groupBox12, "Aile Ayrı mı?");
            ModernWinForms.StyleGroupBox(groupBox7, "Aktif / Ödeme Durumu");
            ModernWinForms.StyleGroupBox(groupBox6, "Ödeme Tutarı");
            ModernWinForms.StyleCheck(checkEvet);
            ModernWinForms.StyleCheck(checkAktif);
            ModernWinForms.StyleCheck(checkOdemeDurum);
            ModernWinForms.StyleInput(numericPrice, 170);
            groupBox12.Dock = DockStyle.None; groupBox12.Width = 180; groupBox12.Height = 82;
            groupBox7.Dock = DockStyle.None; groupBox7.Width = 260; groupBox7.Height = 82;
            groupBox6.Dock = DockStyle.None; groupBox6.Width = 230; groupBox6.Height = 82;
            statusFlow.Controls.AddRange(new Control[] { groupBox12, groupBox7, groupBox6 });
            statusCard.Controls.Add(statusFlow);
            content.SetColumnSpan(statusCard, 2);
            content.Controls.Add(statusCard, 0, 2);

            var menu = new ContextMenuStrip();
            menu.Items.Add("Kaydet", null, (s, e) => RunStudentSave());
            menu.Items.Add("Güncelle", null, (s, e) => RunStudentUpdate());
            menu.Items.Add("Sil", null, (s, e) => RunStudentDelete());
            ContextMenuStrip = menu;
            scroll.ContextMenuStrip = menu;

            KeyDown += (s, e) =>
            {
                if (e.Control && e.KeyCode == Keys.S) { RunStudentSave(); e.Handled = true; }
                if (e.Control && e.KeyCode == Keys.U) { RunStudentUpdate(); e.Handled = true; }
                if (e.KeyCode == Keys.Delete) { RunStudentDelete(); e.Handled = true; }
            };

            scroll.Controls.Add(content);
            root.Controls.Add(scroll, 0, 2);
            Controls.Add(root);
            root.BringToFront();
            ModernWinForms.UseSegoeRecursive(this);
        }

        private void RunStudentSave() => btnAddStock_Click(btnAddStock, EventArgs.Empty);
        private void RunStudentUpdate() => btnGuncelle_Click(btnGuncelle, EventArgs.Empty);
        private void RunStudentDelete() => btnOgrenciYonetimiSil_Click(btnOgrenciYonetimiSil, EventArgs.Empty);

        private void OgrenciForm_Load(object sender, EventArgs e)
        {
            LoadStudentClassComboBox(UserId);
            cmbogrsınıf.SelectedIndex = -1;

        }
        private void txtOgrenciAd_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadStudentClassComboBox(Guid UserId)
        {

            using (SqlConnection conn = new SqlConnection(_form2.connectionString))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand("select ClassName,[Group] from AYSClasses where (OgretmenAdi is not null and OgretmenAdi !=' ' and ClassName !=' ' and ClassName is not null) and SchoolId=(Select top 1 CompanyId from CompanyUsers where UserId=@UserId) and IsDeleted =0", conn);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader reader = cmd.ExecuteReader();
                cmbogrsınıf.Items.Clear();
                while (reader.Read())
                {
                    cmbogrsınıf.Items.Add(new ComboBoxItem
                    {
                        Text = reader["ClassName"].ToString(),
                        Value = reader["Group"].ToString()
                    });
                }
            }
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string isim = txtOgrenciAd.Text;
            string soyisim = textSoyad.Text;
            string babaAdi = txtBabaAd.Text;
            string anneAdi = txtAnneAd.Text;
            string sinif = cmbogrsınıf.Text;
            string ogrenciKod = textOgrenciKod.Text;
            string ogrenciDetay = textOgrenciDetay.Text;
            string babaTel = txtBabaTel.Text;
            string anneTel = txtAnneTel.Text;
            string babaAdres = txtBabaEvAdres.Text;
            string anneAdres = txtAnneEvAdres.Text;
            decimal fiyat = numericPrice.Value;
            bool odemeDurumu = checkOdemeDurum.Checked;
            bool aktifMi = checkAktif.Checked;
            bool aileAyrimi = checkEvet.Checked;
            DateTime dogumTarihi = dateDogum.Value;

            // SQL Güncelleme Sorgusu
            string query = @"
            UPDATE AYSstudents SET 
                Name = @isim,
                Surname = @soyisim,
                FatherName = @babaAdi,
                MotherName = @anneAdi,
                StudentCode = @ogrenciKod,
                StudentsDetails = @ogrenciDetay,
                FatherPhoneNumber = @babaTel,
	            ClassId=(SELECT top 1 Id FROM AYSClasses WHERE ClassName = @ClassName and Isdeleted=0 and SchoolId=dbo.GetSirketIdByUserId(@UserId)),
                MotherPhonenumber = @anneTel,
                FatherAddress = @babaAdres,
                MotherAddress = @anneAdres,
                MonthlyFee = @fiyat,
                PaymentStatus = @odemeDurumu,
                IsActive = @aktifMi,
                IsMarried = @aileAyrimi,
                BirthDate = @dogumTarihi,
                photobinary=@Photo
            WHERE Id = @id";

            using (SqlConnection con = new SqlConnection(_form2.connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Parametreleri Ekle
                    cmd.Parameters.AddWithValue("@id", StudentId);
                    cmd.Parameters.AddWithValue("@isim", isim);
                    cmd.Parameters.AddWithValue("@soyisim", soyisim);
                    cmd.Parameters.AddWithValue("@babaAdi", babaAdi);
                    cmd.Parameters.AddWithValue("@anneAdi", anneAdi);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@ogrenciKod", ogrenciKod);
                    cmd.Parameters.AddWithValue("@ogrenciDetay", ogrenciDetay);
                    cmd.Parameters.AddWithValue("@ClassName", sinif);
                    cmd.Parameters.AddWithValue("@babaTel", babaTel);
                    cmd.Parameters.AddWithValue("@anneTel", anneTel);
                    cmd.Parameters.AddWithValue("@babaAdres", babaAdres);
                    cmd.Parameters.AddWithValue("@anneAdres", anneAdres);
                    cmd.Parameters.AddWithValue("@fiyat", fiyat);
                    cmd.Parameters.AddWithValue("@odemeDurumu", odemeDurumu);
                    cmd.Parameters.AddWithValue("@aktifMi", aktifMi);
                    cmd.Parameters.AddWithValue("@aileAyrimi", aileAyrimi);
                    cmd.Parameters.AddWithValue("@dogumTarihi", dogumTarihi);
                    SqlParameter photoParam = new SqlParameter("@Photo", SqlDbType.VarBinary, -1);
                    photoParam.Value = Photo != null ? (object)Photo : DBNull.Value;
                    cmd.Parameters.Add(photoParam);
                    cmd.ExecuteNonQuery(); // SQL sorgusunu çalıştır
                }
            }

            MessageBox.Show("Öğrenci bilgileri başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _form2.DeleteAndLog("Aysstudents", "Id", StudentId, UserId, "2", "UPDATE");
            RefreshData.Invoke(this, new EventArgs());
            _form2.dataGridViewStok.DataSource = _form2.LoadStockDataRefresh(UserId);
            this.Close();
            // Güncellenmiş listeyi tekrar yükle
        }



        private void btnAddStock_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan veriler:
            string ogrenciName = txtOgrenciAd.Text;
            string ogrenciSurname = textSoyad.Text;
            string Fathername = txtBabaAd.Text;
            string MotherName = txtAnneAd.Text;
            string classing = cmbogrsınıf.Text;
            string studentcode = textOgrenciKod.Text;
            string ogrenciDetails = textOgrenciDetay.Text;
            string FatherPhoneNumber = txtBabaTel.Text;
            string MotherPhoneNumber = txtAnneTel.Text;
            string FatherAddress = txtBabaEvAdres.Text;
            string MotherAddress = txtAnneEvAdres.Text;
            decimal odenentutar = numericPrice.Value;
            bool IsMarried = checkEvet.Checked;
            bool odemedurum = checkOdemeDurum.Checked;
            bool aktiflik = checkAktif.Checked;
            DateTime dateTime = dateDogum.Value;
            Guid StudentIdGuid = Guid.NewGuid();

            // Zorunlu alan kontrolü:
            if (string.IsNullOrEmpty(ogrenciName) ||
                string.IsNullOrEmpty(ogrenciSurname) ||
                string.IsNullOrEmpty(classing))
            {
                MessageBox.Show("Sütunları boş bırakamazsınız...", "HATA", MessageBoxButtons.OK);
                return;
            }

            // Veritabanı bağlantısı ve INSERT sorgusu:
            using (SqlConnection conn = new SqlConnection(_form2.connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Aysstudents (
                Id, Name, Surname, FatherName, BirthDate, StudentCode, 
                ClassId, PaymentStatus, MonthlyFee, IsActive, 
                FatherAddress, MotherAddress, FatherPhoneNumber, MotherPhoneNumber, 
                IsMarried, StudentsDetails, MotherName, SchoolId, photobinary
            )
            VALUES (
                @Id, @Name, @Surname, @FatherName, @BirthDate, @StudentCode, 
                (SELECT top 1 Id FROM AYSClasses WHERE ClassName = @ClassName and Isdeleted=0 and SchoolId=dbo.GetSirketIdByUserId(@UserId)), 
                @PaymentStatus, @MonthlyFee, @IsActive, 
                @FatherAddress, @MotherAddress, @FatherPhoneNumber, @MotherPhoneNumber, 
                @IsMarried, @StudentsDetails, @MotherName, 
                (SELECT CompanyId FROM CompanyUsers WHERE UserId = @UserId),
                @Photo
            )", conn);

                cmd.Parameters.AddWithValue("@Id", StudentIdGuid);
                cmd.Parameters.AddWithValue("@Name", ogrenciName);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Surname", ogrenciSurname);
                cmd.Parameters.AddWithValue("@FatherName", Fathername);
                cmd.Parameters.AddWithValue("@BirthDate", dateTime);
                cmd.Parameters.AddWithValue("@StudentCode", studentcode);
                cmd.Parameters.AddWithValue("@PaymentStatus", odemedurum);
                cmd.Parameters.AddWithValue("@MonthlyFee", odenentutar);
                cmd.Parameters.AddWithValue("@ClassName", classing);
                cmd.Parameters.AddWithValue("@IsActive", aktiflik);
                cmd.Parameters.AddWithValue("@MotherName", MotherName);
                cmd.Parameters.AddWithValue("@FatherAddress", FatherAddress);
                cmd.Parameters.AddWithValue("@MotherAddress", MotherAddress);
                cmd.Parameters.AddWithValue("@FatherPhoneNumber", FatherPhoneNumber);
                cmd.Parameters.AddWithValue("@StudentsDetails", ogrenciDetails);
                cmd.Parameters.AddWithValue("@MotherPhoneNumber", MotherPhoneNumber);
                cmd.Parameters.AddWithValue("@IsMarried", IsMarried);

                SqlParameter photoParam = new SqlParameter("@Photo", SqlDbType.VarBinary, -1);
                photoParam.Value = Photo != null ? (object)Photo : DBNull.Value;
                cmd.Parameters.Add(photoParam);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Öğrenci başarıyla eklendi.");
            _form2.DeleteAndLog("Aysstudents", "Id", StudentIdGuid, UserId, "0", "INSERT");
            RefreshData.Invoke(this, new EventArgs());
            // Ana formdaki listeyi güncelle
            _form2.dataGridViewStok.DataSource = _form2.LoadStockDataRefresh(UserId);


            // Bu formu kapat
            this.Close();

        }


        private void btnOgrenciYonetimiSil_Click(object sender, EventArgs e)
        {
            string query = @"
            Update AysStudents set IsDeleted=1
            WHERE Id = @id";

            using (SqlConnection con = new SqlConnection(_form2.connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Parametreleri Ekle
                    cmd.Parameters.AddWithValue("@id", StudentId);

                    cmd.ExecuteNonQuery(); // SQL sorgusunu çalıştır
                }
            }

            MessageBox.Show("Öğrenci Silindi Eski Kayıtlar için Loglara Bak..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _form2.DeleteAndLog("Aysstudents", "Id", StudentId, UserId, "1", "DELETE");
            RefreshData.Invoke(this, new EventArgs());
            _form2.dataGridViewStok.DataSource = _form2.LoadStockDataRefresh(UserId);
            this.Close();


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fotoğraf |*.png;*.jpeg";
            openFileDialog.Title = "Bir Fotoğraf Seçin";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);

                if (pictureBox1.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                        Photo = ms.ToArray();
                    }
                }
            }
        }

        private void cmbogrsınıf_DrawItem(object sender, DrawItemEventArgs e)
        {

            if (e.Index < 0)
            {
                // Placeholder gibi davran
                e.Graphics.DrawString("Ev adresi seçiniz",
                    new Font("Segoe UI", 9, FontStyle.Italic),
                    Brushes.Gray, e.Bounds);
                return;
            }

            e.DrawBackground();
            e.Graphics.DrawString(cmbogrsınıf.Items[e.Index].ToString(),
                e.Font, Brushes.Black, e.Bounds);

        }

        private void txtBabaEvAdres_Enter(object sender, EventArgs e)
        {
            if (txtBabaEvAdres.Text == "Ev Adresi")
            {
                txtBabaEvAdres.Text = "";
                txtBabaEvAdres.ForeColor = Color.Black; // Yazı rengi normal olsun
            }
        }

        private void txtBabaEvAdres_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBabaEvAdres.Text))
            {
                txtBabaEvAdres.Text = "Ev Adresi";
                txtBabaEvAdres.ForeColor = Color.Gray; // Placeholder gibi görünmesi için gri
            }
        }

        private void txtAnneEvAdres_Enter(object sender, EventArgs e)
        {
            if (txtAnneEvAdres.Text == "Ev Adresi")
            {
                txtAnneEvAdres.Text = "";
                txtAnneEvAdres.ForeColor = Color.Black; // Yazı rengi normal olsun
            }
        }

        private void txtAnneEvAdres_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAnneEvAdres.Text))
            {
                txtAnneEvAdres.Text = "Ev Adresi";
                txtAnneEvAdres.ForeColor = Color.Gray; // Placeholder gibi görünmesi için gri
            }
        }

        public byte[] Photo { get; set; }
        public Guid UserId { get; set; }
        public Guid StudentId { get; set; }
    }
    public class OgrUser
    {
       
    }
}
