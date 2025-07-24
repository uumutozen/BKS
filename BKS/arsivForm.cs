using Newtonsoft.Json;
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
 
    public partial class arsivForm : Form
    {
        private Guid UserId;
        private Guid OgrenciId;
        private string connectionString;

        public arsivForm(Guid userId, string connStr,Guid ogrenciId)
        {
            UserId = userId;
            connectionString = connStr;
            OgrenciId = ogrenciId;
            InitializeComponent();
            LoadOgrenciler();
            cmbOgrenciler.SelectedIndexChanged += CmbOgrenciler_SelectedIndexChanged;
            btnDosyaSec.Click += BtnDosyaSec_Click;
            btnYukle.Click += BtnYukle_Click;
            LoadOgrenciDosyalari();
            btnIndir.Click += BtnIndir_Click;
        }

        private void LoadOgrenciler()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var dt = new DataTable();
                var da = new SqlDataAdapter(
    "SELECT Id, Name + ' ' + Surname as AdSoyad FROM Aysstudents WHERE IsDeleted = 0 AND SchoolId = dbo.GetSirketIdByUserId(@UserId) and Id=@Id ORDER BY Name", conn);
                da.SelectCommand.Parameters.AddWithValue("@UserId", UserId);
                da.SelectCommand.Parameters.AddWithValue("@Id", OgrenciId);
                da.Fill(dt);

                var items = new List<OgrenciCombo>();
                foreach (DataRow dr in dt.Rows)
                    items.Add(new OgrenciCombo() { Id = dr["Id"].ToString(), AdSoyad = dr["AdSoyad"].ToString() });

                cmbOgrenciler.DataSource = items;
                cmbOgrenciler.DisplayMember = "AdSoyad";
                cmbOgrenciler.ValueMember = "Id";
            }
        }

        private void CmbOgrenciler_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOgrenciDosyalari();
        }

        private void LoadOgrenciDosyalari()
        {
            dgvDosyalar.DataSource = null;
            if (cmbOgrenciler.SelectedValue == null) return;
            string ogrenciId = cmbOgrenciler.SelectedValue.ToString();

            using (var client = new HttpClient())
            {
                var resp = client.GetAsync($"https://randevu.aslancan.com.tr/api/dosya-arsiv/ogrenci/{ogrenciId}").Result;
                if (resp.IsSuccessStatusCode)
                {
                    var json = resp.Content.ReadAsStringAsync().Result;
                    var dosyalar = JsonConvert.DeserializeObject<List<DosyaArsivModel>>(json);
                    dgvDosyalar.DataSource = dosyalar;
                }
                else
                {
                    MessageBox.Show("Dosyalar alınamadı.");
                }
            }
        }

        private void BtnDosyaSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                    txtDosyaYolu.Text = ofd.FileName;
            }
        }

        private async void BtnYukle_Click(object sender, EventArgs e)
        {
            if (cmbOgrenciler.SelectedValue == null) { MessageBox.Show("Öğrenci seç!"); return; }
            string ogrenciId = cmbOgrenciler.SelectedValue.ToString();
            string dosyaYolu = txtDosyaYolu.Text;
            if (string.IsNullOrEmpty(dosyaYolu))
            {
                MessageBox.Show("Lütfen dosya seçin.");
                return;
            }

            using (var client = new HttpClient())
            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StringContent(ogrenciId), "ogrenciId");
                form.Add(new StreamContent(File.OpenRead(dosyaYolu)), "dosya", Path.GetFileName(dosyaYolu));
                var resp = await client.PostAsync("https://randevu.aslancan.com.tr/api/dosya-arsiv/yukle", form);
                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("Dosya yüklendi.");
                    LoadOgrenciDosyalari();
                }
                else
                    MessageBox.Show("Yükleme hatası: " + resp.ReasonPhrase);
            }
        }

        private async void BtnIndir_Click(object sender, EventArgs e)
        {
            if (dgvDosyalar.SelectedRows.Count == 0) { MessageBox.Show("Dosya seç!"); return; }
            int dosyaId = (int)dgvDosyalar.SelectedRows[0].Cells["Id"].Value;
            string dosyaAdi = dgvDosyalar.SelectedRows[0].Cells["DosyaAdi"].Value.ToString();

            using (var client = new HttpClient())
            {
                var resp = await client.GetAsync($"https://randevu.aslancan.com.tr/api/dosya-arsiv/indir/{dosyaId}");
                if (resp.IsSuccessStatusCode)
                {
                    var data = await resp.Content.ReadAsByteArrayAsync();
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), dosyaAdi);
                    File.WriteAllBytes(path, data);
                    MessageBox.Show("Dosya indirildi: " + path);
                }
                else
                    MessageBox.Show("İndirme hatası: " + resp.ReasonPhrase);
            }
        }

        class OgrenciCombo
        {
            public string Id { get; set; }
            public string AdSoyad { get; set; }
            public override string ToString() => AdSoyad;
        }
        public class DosyaArsivModel
        {
            public int Id { get; set; }
            public string DosyaAdi { get; set; }
            public string DosyaYolu { get; set; }
            public DateTime EklenmeTarihi { get; set; }
        }
    }

}
