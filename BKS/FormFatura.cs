using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Fonts;
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
using System.Windows.Media;
using PdfSharpCore.Utils;

namespace BKS
{
    public partial class FormFatura : Form
    {
        private readonly string connStr = "Server=31.186.11.161;Database=asl2e6ancomtr_PaymentDBDB;User Id=asl2e6ancomtr_aslan;Password=Aslan123.@;TrustServerCertificate=True;";
        private string pdfKlasoru;
        
        public FormFatura()
        {
            InitializeComponent();

            // Font resolver'i global olarak ayarla
            GlobalFontSettings.FontResolver = new FontResolver();

            pdfKlasoru = Path.Combine(Application.StartupPath, "Faturalar");
            if (!Directory.Exists(pdfKlasoru))
                Directory.CreateDirectory(pdfKlasoru);
           
        }
        public Guid UserId { get; set; }
        private void FormFatura_Load(object sender, EventArgs e)
        {
            dgKalemler.Columns.Clear();
            dgKalemler.Columns.Add("UrunAdi", "Ürün Adı");
            dgKalemler.Columns.Add("Miktar", "Miktar");
            dgKalemler.Columns.Add("BirimFiyat", "Birim Fiyat");
            dgKalemler.Columns.Add("KDV", "KDV");

            FaturalariYukle();
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string faturaNo = txtFaturaNo.Text.Trim();
            string aliciUnvan = txtAliciUnvan.Text.Trim();
            string aliciVkn = txtAliciVkn.Text.Trim();
            DateTime tarih = dtTarih.Value;
         
            if (string.IsNullOrWhiteSpace(faturaNo) || string.IsNullOrWhiteSpace(aliciUnvan) || string.IsNullOrWhiteSpace(aliciVkn))
            {
                MessageBox.Show("Tüm bilgileri doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string pdfYolu = FaturaPdfOlustur(faturaNo, aliciUnvan, aliciVkn, tarih);
            if (pdfYolu != null)
            {
                FaturaVeritabaninaKaydet(faturaNo, aliciUnvan, aliciVkn, tarih, pdfYolu,UserId);
                FaturalariYukle();
                MessageBox.Show("Fatura kaydedildi ve PDF oluşturuldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public string FaturaPdfOlustur(string faturaNo, string aliciUnvan, string aliciVkn, DateTime tarih)
        {
            try
            {
                string pdfKlasoru = Path.Combine(Application.StartupPath, "Faturalar");
                if (!Directory.Exists(pdfKlasoru))
                    Directory.CreateDirectory(pdfKlasoru);

                string dosyaAdi = $"Fatura_{faturaNo}.pdf";
                foreach (char c in Path.GetInvalidFileNameChars())
                    dosyaAdi = dosyaAdi.Replace(c, '_');

                string dosyaYolu = Path.Combine(pdfKlasoru, dosyaAdi);

                using (PdfDocument document = new PdfDocument())
                {
                    document.Info.Title = $"Fatura {faturaNo}";

                    PdfPage page = document.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    // Fontlar
                    XFont fontBaslik = new XFont("Arial", 22, XFontStyle.Bold);
                    XFont fontNormal = new XFont("Arial", 12, XFontStyle.Regular);
                    XFont fontKalemBaslik = new XFont("Arial", 13, XFontStyle.Bold);

                    double yPoint = 40;
                    double leftMargin = 40;
                    double rightMargin = page.Width - 40;
                    double pageWidth = page.Width;

                    // Başlık kutusu
                    XRect headerRect = new XRect(leftMargin, yPoint, pageWidth - leftMargin * 2, 50);
                    gfx.DrawRoundedRectangle(XPens.DarkBlue, XBrushes.LightBlue, headerRect, new XSize(10, 10));
                    gfx.DrawString("E-FATURA", fontBaslik, XBrushes.DarkBlue, headerRect, XStringFormats.Center);
                    yPoint += 70;

                    // Fatura Bilgileri
                    gfx.DrawString($"Fatura No: {faturaNo}", fontNormal, XBrushes.Black, new XPoint(leftMargin, yPoint));
                    yPoint += 22;
                    gfx.DrawString($"Alıcı Ünvan: {aliciUnvan}", fontNormal, XBrushes.Black, new XPoint(leftMargin, yPoint));
                    yPoint += 22;
                    gfx.DrawString($"Alıcı VKN: {aliciVkn}", fontNormal, XBrushes.Black, new XPoint(leftMargin, yPoint));
                    yPoint += 22;
                    gfx.DrawString($"Tarih: {tarih:yyyy-MM-dd}", fontNormal, XBrushes.Black, new XPoint(leftMargin, yPoint));
                    yPoint += 40;

                    // Kalem başlıkları (background)
                    double[] colWidths = { 220, 60, 100, 60 };
                    double colStart = leftMargin;
                    double rowHeight = 25;

                    XBrush kalemBaslikBack = new XSolidBrush(XColor.FromArgb(220, 230, 241)); // Açık mavi
                    XPen kalemBaslikBorder = new XPen(XColors.DarkBlue, 1.2);

                    string[] kalemBasliklar = { "Ürün Adı", "Miktar", "Birim Fiyat", "KDV" };

                    for (int i = 0; i < kalemBasliklar.Length; i++)
                    {
                        var rect = new XRect(colStart, yPoint, colWidths[i], rowHeight);
                        gfx.DrawRectangle(kalemBaslikBorder, kalemBaslikBack, rect);
                        gfx.DrawString(kalemBasliklar[i], fontKalemBaslik, XBrushes.DarkBlue, rect, XStringFormats.Center);
                        colStart += colWidths[i];
                    }
                    yPoint += rowHeight;

                    // Alt çizgi
                    gfx.DrawLine(new XPen(XColors.DarkBlue, 1.5), leftMargin, yPoint, rightMargin, yPoint);
                    yPoint += 5;

                    // Kalemler satırları (zebra)
                    if (dgKalemler == null || dgKalemler.Rows.Count == 0)
                    {
                        MessageBox.Show("Kalem bilgisi yok, PDF oluşturulamadı.");
                        return null;
                    }

                    int satirNo = 0;
                    foreach (DataGridViewRow row in dgKalemler.Rows)
                    {
                        if (row.IsNewRow) continue;
                        XBrush rowBack = (satirNo % 2 == 0) ? XBrushes.White : new XSolidBrush(XColor.FromArgb(245, 245, 245));

                        colStart = leftMargin;
                        string urunAdi = row.Cells["UrunAdi"].Value?.ToString() ?? "";
                        string miktar = row.Cells["Miktar"].Value?.ToString() ?? "0";
                        string birimFiyat = row.Cells["BirimFiyat"].Value?.ToString() ?? "0";
                        string kdv = row.Cells["KDV"].Value?.ToString() ?? "0";

                        string[] satirVeri = { urunAdi, miktar, birimFiyat, kdv };

                        for (int i = 0; i < satirVeri.Length; i++)
                        {
                            var rect = new XRect(colStart, yPoint, colWidths[i], rowHeight);
                            gfx.DrawRectangle(XPens.LightGray, rowBack, rect);
                            gfx.DrawString(satirVeri[i], fontNormal, XBrushes.Black, rect, XStringFormats.CenterLeft);
                            colStart += colWidths[i];
                        }

                        yPoint += rowHeight;
                        satirNo++;

                        // Sayfa sınırı kontrolü
                        if (yPoint > page.Height - 60)
                        {
                            // Alt bilgi
                            gfx.DrawString($"Sayfa {document.PageCount}", fontNormal, XBrushes.Gray,
                                new XRect(0, page.Height - 40, page.Width, 20), XStringFormats.Center);

                            page = document.AddPage();
                            gfx = XGraphics.FromPdfPage(page);
                            yPoint = 40;
                        }
                    }

                    // Son sayfa alt bilgisi
                    gfx.DrawString($"Sayfa {document.PageCount}", fontNormal, XBrushes.Gray,
                        new XRect(0, page.Height - 40, page.Width, 20), XStringFormats.Center);

                    document.Save(dosyaYolu);
                }

                return dosyaYolu;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"PDF oluşturulurken hata: {ex.Message}");
                return null;
            }
        }

        private void FaturaVeritabaninaKaydet(string faturaNo, string aliciUnvan, string aliciVkn, DateTime tarih, string pdfYolu,Guid Userid)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Örneğin ETS ve 25 veriliyor
                    string ucluKod = faturaNo;
                    string yilKod = DateTime.Now.ToString("yy");  // Örneğin "25"
                    string faturaPrefix = $"{ucluKod}{yilKod}";

                    // Mevcut en büyük fatura numarasını al (belirli prefix ile başlayan)
                    string selectQuery = "SELECT MAX(FaturaNo) FROM Faturalar WHERE FaturaNo LIKE @prefix + '%'";
                    string yeniSayi = "00001";

                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@prefix", faturaPrefix);
                        var maxFaturaNo = selectCmd.ExecuteScalar() as string;

                        if (!string.IsNullOrEmpty(maxFaturaNo) && maxFaturaNo.Length >= faturaPrefix.Length + 5)
                        {
                            string mevcutSayiStr = maxFaturaNo.Substring(faturaPrefix.Length, 5);
                            if (int.TryParse(mevcutSayiStr, out int mevcutSayi))
                            {
                                yeniSayi = (mevcutSayi + 1).ToString("D5");
                            }
                        }
                    }

                    faturaNo = $"{faturaPrefix}{yeniSayi}";

                    string insertQuery = "INSERT INTO Faturalar (FaturaNo, AliciUnvan, AliciVKN, Tarih, PdfYolu, SirketId) " +
                                         "VALUES (@no, @unvan, @vkn, @tarih, @pdf, dbo.GetSirketIdByUserId(@SirketId))";

                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@no", faturaNo);
                        insertCmd.Parameters.AddWithValue("@unvan", aliciUnvan);
                        insertCmd.Parameters.AddWithValue("@vkn", aliciVkn);
                        insertCmd.Parameters.AddWithValue("@tarih", tarih);
                        insertCmd.Parameters.AddWithValue("@pdf", pdfYolu);
                        insertCmd.Parameters.AddWithValue("@SirketId", Userid);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanına kaydedilirken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FaturalariYukle()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string sorgu = "SELECT Id, FaturaNo, AliciUnvan, AliciVKN, Tarih, PdfYolu FROM Faturalar";

                    using (SqlDataAdapter da = new SqlDataAdapter(sorgu, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgFaturalar.DataSource = dt;

                        if (dgFaturalar.Columns["PdfYolu"] != null)
                            dgFaturalar.Columns["PdfYolu"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Faturalar yüklenirken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgFaturalar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string pdfYolu = dgFaturalar.Rows[e.RowIndex].Cells["PdfYolu"].Value?.ToString();

            if (!string.IsNullOrEmpty(pdfYolu) && File.Exists(pdfYolu))
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = pdfYolu,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"PDF açılırken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("PDF dosyası bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
