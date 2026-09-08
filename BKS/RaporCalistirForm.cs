using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BKS
{
    public partial class RaporCalistirForm : Form
    {
        private int _raporId;
        private string _connectionString = AppConfiguration.ConnectionString;
        private string _sorgu;
        private Dictionary<string, string> _parametreler;
        private string _tabloAdi;
        private Panel panelParametreler;
        private DataGridView gridSonuc;
        private Button btnCalistir;

        public RaporCalistirForm(int raporId)
        {
            _raporId = raporId;
            InitializeComponent();
            this.Load += RaporCalistirForm_Load;
        }

        private void RaporCalistirForm_Load(object sender, EventArgs e)
        {
            panelParametreler = new Panel
            {
                Dock = DockStyle.Fill
            };
            gridSonuc = new DataGridView();
            var ribbon = Screens.Ribbon("Rapor", new RibbonCommand("Çalıştır", RibbonIcon.View, () => BtnCalistir_Click(this,
            EventArgs.Empty)), new RibbonCommand("Kapat", RibbonIcon.Restore, Close));
            Screens.Install(this, Screens.WithEditor(panelParametreler, gridSonuc, .30F), ribbon, "Özel raporu çalıştır");
            if (AppConfiguration.DesignPreview) return;
            // Rapor bilgilerini DB'den çek
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT RaporAdi, Sorgu FROM OzelRaporlar WHERE Id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", _raporId);
                    var rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        this.Text = "Rapor: " + rdr["RaporAdi"].ToString();
                        _sorgu = rdr["Sorgu"].ToString();
                    }
                }
            }
            // Parametreleri bul
            if (string.IsNullOrWhiteSpace(_sorgu)) throw new InvalidOperationException("Rapor bulunamadı veya sorgusu boş.");
            _parametreler = ParametreVeAlanlariBul(_sorgu);
            _tabloAdi = TabloAdiniBul(_sorgu);
            KontrolleriEkle(_parametreler, _tabloAdi);
        }
        // Aşağıdaki fonksiyonları senin ÖzelRapor formundan direkt kopyala/yada static yapıp paylaşabilirsin
        // --- Parametre Regex ---

        public Dictionary<string, string> ParametreVeAlanlariBul(string metin)
        {
            var parametreler = new Dictionary<string, string>();
            // 1. En yaygın: ... Alan = :Param ...
            var regexEq = new Regex(@"(\w+)\s*=\s*:([\wÇŞĞÜÖİçşğüöı]+)", RegexOptions.IgnoreCase);
            foreach (Match match in regexEq.Matches(metin))
            {
                string kolon = match.Groups[1].Value;
                string param = match.Groups[2].Value;
                if (!parametreler.ContainsKey(param))
                parametreler[param] = kolon;
            }
            // 2. Fonksiyonlu: isnull(:Param, Kolon) veya isnull(Kolon, :Param)
            var regexFunc = new Regex(@"isnull\s*\(\s*:([\wÇŞĞÜÖİçşğüöı]+)\s*,\s*(\w+)\s*\)", RegexOptions.IgnoreCase);
            foreach (Match match in regexFunc.Matches(metin))
            {
                string param = match.Groups[1].Value;
                string kolon = match.Groups[2].Value;
                if (!parametreler.ContainsKey(param))
                parametreler[param] = kolon;
            }
            // isnull(Kolon, :Param)
            var regexFunc2 = new Regex(@"isnull\s*\(\s*(\w+)\s*,\s*:([\wÇŞĞÜÖİçşğüöı]+)\s*\)", RegexOptions.IgnoreCase);
            foreach (Match match in regexFunc2.Matches(metin))
            {
                string kolon = match.Groups[1].Value;
                string param = match.Groups[2].Value;
                if (!parametreler.ContainsKey(param))
                parametreler[param] = kolon;
            }
            // 3. Diğer tüm :Param (hiçbir eşleşme yoksa default param adıyla ekle)
            var regexAny = new Regex(@":([\wÇŞĞÜÖİçşğüöı]+)", RegexOptions.IgnoreCase);
            foreach (Match match in regexAny.Matches(metin))
            {
                string param = match.Groups[1].Value;
                if (!parametreler.ContainsKey(param))
                parametreler[param] = param;
            }
            return parametreler;
        }

        public string TabloAdiniBul(string sorgu)
        {
            var regex = new Regex(@"\bFROM\s+(\w+)", RegexOptions.IgnoreCase);
            var match = regex.Match(sorgu);
            return match.Success ? match.Groups[1].Value: "";
        }

        public void KontrolleriEkle(Dictionary<string, string> parametreler, string tablo)
        {
            foreach (Control old in panelParametreler.Controls.Cast<Control>().ToArray()) old.Dispose();
            panelParametreler.Controls.Clear();
            var fields = new ResponsiveFields();
            foreach (var pair in parametreler)
            {
                var input = GetControlForKolon(pair.Value, tablo);
                input.Name = "ctrl_" + pair.Key;
                fields.AddField(pair.Key, input);
            }
            panelParametreler.Controls.Add(fields);
        }

        private string GetDataTypeForParametre(string param, string tableName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT DATA_TYPE
                        FROM INFORMATION_SCHEMA.COLUMNS
                        WHERE COLUMN_NAME = @param AND TABLE_NAME = @table";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@param", param);
                        cmd.Parameters.AddWithValue("@table", tableName);
                        var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        return reader["DATA_TYPE"].ToString();
                    }
                }
            }
            catch
            {
                // Varsayılan olarak metin
            }
            return "varchar";
        }

        private Control GetControlForKolon(string kolonAdi, string table)
        {
            string veriTuru = GetDataTypeForParametre(kolonAdi, table);
            switch (veriTuru.ToLower())
            {
                case "int":
                return new NumericUpDown
                {
                    Maximum = int.MaxValue,
                    Minimum = int.MinValue
                };
                case "decimal":
                case "numeric":
                case "float":
                return new NumericUpDown
                {
                    Maximum = 1000000,
                    DecimalPlaces = 2
                };
                case "bit":
                return new CheckBox();
                case "date":
                case "datetime":
                return new DateTimePicker
                {
                    Format = DateTimePickerFormat.Short
                };
                default:
                return new TextBox();
            }
        }

        private void SorguyuCalistir(string sorgu)
        {
            if (string.IsNullOrWhiteSpace(sorgu))
            {
                MessageBox.Show("Sorgu Boş Olamaz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    var parametreler = ParametreVeAlanlariBul(sorgu);
                    string tablo = TabloAdiniBul(sorgu);
                    foreach (var p in parametreler)
                    {
                        sorgu = Regex.Replace(sorgu, @":" + Regex.Escape(p.Key) + @"(?![\wÇŞĞÜÖİçşğüöı])", "@" + p.Key);
                    }
                    SqlCommand cmd = new SqlCommand(sorgu, conn);
                    foreach (var p in parametreler)
                    {
                        var control = panelParametreler.Controls.Find("ctrl_" + p.Key, true).FirstOrDefault();
                        if (control is TextBox txt)
                        cmd.Parameters.AddWithValue("@" + p.Key, txt.Text);
                        else if (control is DateTimePicker dtp)
                        cmd.Parameters.AddWithValue("@" + p.Key, dtp.Value.Date);
                        else if (control is CheckBox chk)
                        cmd.Parameters.AddWithValue("@" + p.Key, chk.Checked);
                        else if (control is NumericUpDown nud)
                        cmd.Parameters.AddWithValue("@" + p.Key, nud.Value);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridSonuc.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "SQL Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCalistir_Click(object sender, EventArgs e)
        {
            string sql = _sorgu;
            foreach (var p in _parametreler)
            sql = Regex.Replace(sql, @":" + Regex.Escape(p.Key) + @"(?![\wÇŞĞÜÖİçşğüöı])", "@" + p.Key);
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                foreach (var p in _parametreler)
                {
                    var control = panelParametreler.Controls.Find("ctrl_" + p.Key, true).FirstOrDefault();
                    if (control is TextBox txt)
                    cmd.Parameters.AddWithValue("@" + p.Key, txt.Text);
                    else if (control is DateTimePicker dtp)
                    cmd.Parameters.AddWithValue("@" + p.Key, dtp.Value.Date);
                    else if (control is CheckBox chk)
                    cmd.Parameters.AddWithValue("@" + p.Key, chk.Checked);
                    else if (control is NumericUpDown nud)
                    cmd.Parameters.AddWithValue("@" + p.Key, nud.Value);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridSonuc.DataSource = dt;
            }
        }
    }
}
