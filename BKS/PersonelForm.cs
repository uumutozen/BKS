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
        private PhotoEditor _photoEditor = null!;
        private bool _photoChanged;
        private readonly CancellationTokenSource _photoLoadCancellation = new();
        private bool _photoCancellationDisposed;
        public string connectionString = AppConfiguration.ConnectionString;

        public PersonelForm() : this(null!) { }

        public PersonelForm(Form2 form2)
        {
            InitializeComponent();
            _form2 = form2;
            InitializeRuntimeState();
            _edits = new EditSession(this, () =>
            {
                if (PersonelId == Guid.Empty) RunPersonnelSave();
                else RunPersonnelUpdate();
            }, () => _photoChanged ? Photo: null);
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
