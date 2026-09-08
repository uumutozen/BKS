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
    public partial class PersonelForm : Form
    {
        public event EventHandler RefreshData;
        private Form2 _form2;
        private readonly EditSession _edits;
        private SectionedForm _sections = null!;
        private PhotoEditor _photoEditor = null!;
        private bool _photoChanged;
        private readonly CancellationTokenSource _photoLoadCancellation = new();
        private bool _photoCancellationDisposed;
        public string connectionString = AppConfiguration.ConnectionString;

        public PersonelForm(Form2 form2)
        {
            InitializeComponent();
            _form2 = form2;
            BuildModernPersonnelFormLayout();
<<<<<<< HEAD
            _edits = new EditSession(this, () =>
            {
                if (PersonelId == Guid.Empty) RunPersonnelSave();
                else RunPersonnelUpdate();
            }, () => _photoChanged ? Photo: null);
=======
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
            
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
        }

        private void RunPersonnelSave() => btnPersonelKaydet_Click(btnPersonelKaydet, EventArgs.Empty);

        private void RunPersonnelUpdate() => btnPersonelGuncelle_Click(btnPersonelGuncelle, EventArgs.Empty);

        private void RunPersonnelDelete() => btnPersonelSil_Click(btnPersonelSil, EventArgs.Empty);

        private void RunPersonnelClear() => btnPersonelTemizle_Click(btnPersonelTemizle, EventArgs.Empty);

        private void btnPersonelTemizle_Click(object sender, EventArgs e)
        {
            _photoEditor.SetBytes(null);
            Photo = null;
            _photoChanged = true;
            txtPersonelKimlik.Clear();
            txtPersonelMail.Clear();
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
            cbxPersonelUyruk.SelectedIndex = - 1;
            cbxPersonelCalismaSekli.SelectedIndex = - 1;
            cbxPersonelSigorta.SelectedIndex = - 1;
            cbxPersonelEgitimDurumu.SelectedIndex = - 1;
            cbxPersonelUniversite.SelectedIndex = - 1;
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
        public Guid UserId
        {
            get;
            set;
        }
        public Guid PersonelId
        {
            get;
            set;
        }
        public byte[]? Photo
        {
            get;
            set;
        }
    }
}
