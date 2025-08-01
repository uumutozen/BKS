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
        private string _connectionString = "Server=31.186.11.161;Database=asl2e6ancomtr_PaymentDBDB;User Id=asl2e6ancomtr_aslan;Password=Aslan123.@;TrustServerCertificate=True;";
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
            this.Text = "Özel Raporu Çalıştır";
            this.Width = 700; this.Height = 600;

            panelParametreler = new Panel { Left = 20, Top = 20, Width = 630, Height = 120, AutoScroll = true };
            this.Controls.Add(panelParametreler);

            btnCalistir = new Button { Text = "ÇALIŞTIR", Left = 20, Top = 150, Width = 120, Height = 30, BackColor = Color.LightGreen };
            btnCalistir.Click += BtnCalistir_Click;
            this.Controls.Add(btnCalistir);

            gridSonuc = new DataGridView
            {
                Left = 20,
                Top = 200,
                Width = 630,
                Height = 330,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false
            };
            this.Controls.Add(gridSonuc);

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
            return match.Success ? match.Groups[1].Value : "";
        }

        public void KontrolleriEkle(Dictionary<string, string> parametreler, string tablo)
        {
            panelParametreler.Controls.Clear();
            int y = 10;

            foreach (var pair in parametreler)
            {
                string param = pair.Key;    // Parametre adı (:Date)
                string alan = pair.Value;   // Kolon adı (HireDate veya param == alan)

                // LABEL
                Label lbl = new Label
                {
                    Text = param + " :",
                    Left = 10,
                    Top = y + 5,
                    Width = 120,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = ColorTranslator.FromHtml("#333A42")
                };

                // TABLODA VAR MI?
                bool kolonVar = false;
                string veriTuru = "varchar";
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
                            cmd.Parameters.AddWithValue("@param", alan);
                            cmd.Parameters.AddWithValue("@table", tablo);
                            var reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                kolonVar = true;
                                veriTuru = reader["DATA_TYPE"].ToString();
                            }
                        }
                    }
                }
                catch { kolonVar = false; }

                // KONTROL OLUŞTUR
                Control input;
                if (kolonVar)
                {
                    switch (veriTuru.ToLower())
                    {
                        case "int":
                            input = new NumericUpDown { Maximum = int.MaxValue, Minimum = int.MinValue };
                            break;
                        case "decimal":
                        case "numeric":
                        case "float":
                            input = new NumericUpDown { Maximum = 1000000, DecimalPlaces = 2 };
                            break;
                        case "bit":
                            input = new CheckBox();
                            break;
                        case "date":
                        case "datetime":
                            input = new DateTimePicker { Format = DateTimePickerFormat.Short };
                            break;
                        default:
                            input = new TextBox();
                            break;
                    }
                }
                else
                {
                    // Eğer eşleşen kolon yoksa, her zaman TextBox
                    input = new TextBox();
                }

                input.Name = "ctrl_" + param;
                input.Left = 140;
                input.Top = y;
                input.Width = 170;

                panelParametreler.Controls.Add(lbl);
                panelParametreler.Controls.Add(input);

                // Eğer kolon bulunamazsa küçük bir info/uyarı koy
                if (!kolonVar)
                {
                    Label info = new Label
                    {
                        Text = "Tablo kolonu bulunamadı, metin olarak girin.",
                        Left = 320,
                        Top = y + 5,
                        Width = 220,
                        ForeColor = Color.DarkOrange,
                        Font = new Font("Segoe UI", 8, FontStyle.Italic)
                    };
                    panelParametreler.Controls.Add(info);
                }

                y += 38;
            }

            // Sıfırlama Butonu (aynı)
            if (parametreler.Count > 0)
            {
                Button btnClear = new Button
                {
                    Text = "Parametreleri Sıfırla",
                    Left = 10,
                    Top = y + 8,
                    Width = 180,
                    Height = 30,
                    BackColor = Color.LightPink,
                    FlatStyle = FlatStyle.Flat
                };
                btnClear.Click += (s, e) =>
                {
                    foreach (Control c in panelParametreler.Controls)
                    {
                        if (c is TextBox txt) txt.Text = "";
                        if (c is NumericUpDown nud) nud.Value = nud.Minimum;
                        if (c is DateTimePicker dtp) dtp.Value = DateTime.Now;
                        if (c is CheckBox chk) chk.Checked = false;
                    }
                };
                panelParametreler.Controls.Add(btnClear);
            }
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
                    return new NumericUpDown { Maximum = int.MaxValue, Minimum = int.MinValue };
                case "decimal":
                case "numeric":
                case "float":
                    return new NumericUpDown { Maximum = 1000000, DecimalPlaces = 2 };
                case "bit":
                    return new CheckBox();
                case "date":
                case "datetime":
                    return new DateTimePicker { Format = DateTimePickerFormat.Short };
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
                        sorgu = sorgu.Replace(":" + p.Key, "@" + p.Key);
                    }

                    SqlCommand cmd = new SqlCommand(sorgu, conn);

                    foreach (var p in parametreler)
                    {
                        var control = panelParametreler.Controls["ctrl_" + p.Key];
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
                sql = sql.Replace(":" + p.Key, "@" + p.Key);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                foreach (var p in _parametreler)
                {
                    var control = panelParametreler.Controls["ctrl_" + p.Key];
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
