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
    public partial class OgrenciForm : Form
    {
        public Guid? PreRegistrationId
        {
            get;
            set;
        }
        public event EventHandler RefreshData;
        private Form2 _form2;
        private readonly EditSession _edits;
        public OgrenciForm(Form2 form2)
        {
            _form2 = form2;
            InitializeComponent();
            BuildModernStudentFormLayout();
<<<<<<< HEAD
            _edits = new EditSession(this, () =>
            {
                if (StudentId == Guid.Empty) RunStudentSave();
                else RunStudentUpdate();
            }, () => pictureBox1.Image);
        }
        private void RunStudentSave() => btnAddStock_Click(btnAddStock, EventArgs.Empty);
        private void RunStudentUpdate() => btnGuncelle_Click(btnGuncelle, EventArgs.Empty);
        private void RunStudentDelete() => btnOgrenciYonetimiSil_Click(btnOgrenciYonetimiSil, EventArgs.Empty);
=======
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

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
        private void OgrenciForm_Load(object sender, EventArgs e)
        {
            if (AppConfiguration.DesignPreview) return;
            var selectedClass = cmbogrsınıf.Text;
            LoadStudentClassComboBox(UserId);
            cmbogrsınıf.Text = selectedClass;
        }
        private void txtOgrenciAd_TextChanged(object sender, EventArgs e)
        {
        }
        private void LoadStudentClassComboBox(Guid UserId)
        {
            using (SqlConnection conn = new SqlConnection(_form2.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select ClassName,[Group] from AYSClasses where (OgretmenAdi is not null and OgretmenAdi !=' ' and ClassName !=' ' and ClassName is not null) and SchoolId=(Select top 1 CompanyId from CompanyUsers where UserId=@UserId) and IsDeleted =0",
                conn);
                if (!cmd.Parameters.Contains("@UserId")) cmd.Parameters.AddWithValue("@UserId", UserId);
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
        private void cmbogrsınıf_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index<0)
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
                txtBabaEvAdres.ForeColor = Color.Black;
                // Yazı rengi normal olsun
            }
        }
        private void txtBabaEvAdres_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBabaEvAdres.Text))
            {
                txtBabaEvAdres.Text = "Ev Adresi";
                txtBabaEvAdres.ForeColor = Color.Gray;
                // Placeholder gibi görünmesi için gri
            }
        }
        private void txtAnneEvAdres_Enter(object sender, EventArgs e)
        {
            if (txtAnneEvAdres.Text == "Ev Adresi")
            {
                txtAnneEvAdres.Text = "";
                txtAnneEvAdres.ForeColor = Color.Black;
                // Yazı rengi normal olsun
            }
        }
        private void txtAnneEvAdres_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAnneEvAdres.Text))
            {
                txtAnneEvAdres.Text = "Ev Adresi";
                txtAnneEvAdres.ForeColor = Color.Gray;
                // Placeholder gibi görünmesi için gri
            }
        }
        public byte[] Photo
        {
            get;
            set;
        }
        public Guid UserId
        {
            get;
            set;
        }
        public Guid StudentId
        {
            get;
            set;
        }
    }
    public class OgrUser
    {
    }
}
