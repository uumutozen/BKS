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
        private PhotoEditor _photoEditor = null!;
        public OgrenciForm() : this(null!) { }

        public OgrenciForm(Form2 form2)
        {
            _form2 = form2;
            InitializeComponent();
            InitializeRuntimeState();
            _edits = new EditSession(this, () =>
            {
                if (StudentId == Guid.Empty) RunStudentSave();
                else RunStudentUpdate();
            }, () => pictureBox1.Image);
        }
        private void RunStudentSave() => btnAddStock_Click(btnAddStock, EventArgs.Empty);
        private void RunStudentUpdate() => btnGuncelle_Click(btnGuncelle, EventArgs.Empty);
        private void RunStudentDelete() => btnOgrenciYonetimiSil_Click(btnOgrenciYonetimiSil, EventArgs.Empty);
        private void OgrenciForm_Load(object sender, EventArgs e)
        {
            if (AppConfiguration.DesignPreview) return;
            var selectedClass = cmbogrsınıf.Text;
            try { LoadStudentClassComboBox(UserId); }
            catch (Exception)
            {
                MessageBox.Show("Sınıf listesi yüklenemedi. Bağlantıyı kontrol edip kartı yeniden açın.",
                    "Öğrenci kartı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
        public byte[]? Photo
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
