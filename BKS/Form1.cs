using System;
using System.Data.SqlClient;
using MaterialSkin;
using MaterialSkin.Controls;
using BKS;
using System.Net.Http.Json;

namespace BKS
{
    public partial class Form1 : MaterialForm
    {
        string connectionString = "Server=31.186.11.161;Database=asl2e6ancomtr_PaymentDBDB;User Id=asl2e6ancomtr_aslan;Password=Aslan123.@;TrustServerCertificate=True;";
        private LoginHistoryService loginHistoryService;

        public Form1()
        {
            InitializeComponent();
            loginHistoryService = new LoginHistoryService(connectionString);

            // Material Skin theme ayarları
            var manager = MaterialSkinManager.Instance;
            manager.AddFormToManage(this);
            manager.Theme = MaterialSkinManager.Themes.LIGHT;
            manager.ColorScheme = new ColorScheme(Primary.Blue500, Primary.Blue700, Primary.Blue300, Accent.LightBlue200, TextShade.WHITE);
        }

        [System.ComponentModel.Browsable(false)]
        public System.Windows.Forms.AutoScaleMode AutoScaleMode { get; set; }
        private static extern bool SetProcessDPIAware();



        private void Form1_Load(object sender, EventArgs e)
        {
            string username = userName.Text.Trim();
            if (!string.IsNullOrEmpty(username))
            {
                // Son giriş bilgisini yükle

            }
        }
        private Guid GetUserIdFromDatabase(string username, string password)
        {
            Guid userId = Guid.NewGuid(); // Varsayılan olarak -1 döndür

            string query = "SELECT UserId FROM CompanyUsers ce  \r\njoin Companies c on c.CompanyId= ce.CompanyId\r\nWHERE ce.IsActive=1 and c.IsActive=1 and Email = @Username AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            userId = (Guid)result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bağlantı hatası: " + ex.Message);
                }
            }
            return userId;
        }
        private async void bttnLgn_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı ve şifre girişleri
            string username = userName.Text.Trim();
            string password = passWord.Text.Trim();

            var loginRequest = new
            {
                Email = username,
                Password = password
            };

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://randevu.aslancan.com.tr/"); // ← Burayı API adresinle değiştir
                var response = await client.PostAsJsonAsync("api/Login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    if (loginResponse.Success)
                    {
                        MessageBox.Show("Giriş Başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Form2 form2 = new Form2
                        {
                            UserId = Guid.Parse(loginResponse.UserId)
                        };
                        this.Hide();
                        form2.ShowDialog();
                        Environment.Exit(0);
                    }
                    else
                    {
                        MessageBox.Show(loginResponse.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı Şifre Hatalı", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0); // Tüm programı kapatır.
        }
        public class LoginResponse
        {
            public bool Success { get; set; }
            public string UserId { get; set; }
            public string Message { get; set; }
        }
        public class LoginHistoryService
        {
            private readonly string _connectionString;

            public LoginHistoryService(string connectionString)
            {
                _connectionString = connectionString;
            }

            //public string GetLastLoginTime(string username)
            //{
            //    string lastLoginTime = "Bilinmiyor";
            //    string query = "SELECT Songiriszamani FROM Bksusers WHERE Username=@Username";

            //    using (SqlConnection connection = new SqlConnection(_connectionString))
            //    {
            //        try
            //        {
            //            connection.Open();
            //            using (SqlCommand command = new SqlCommand(query, connection))
            //            {
            //                command.Parameters.AddWithValue("@Username", username);
            //                object result = command.ExecuteScalar();
            //                if (result != null)
            //                {
            //                    lastLoginTime = result.ToString();
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine("Hata: " + ex.Message);
            //        }
            //    }

            //    return lastLoginTime;
        }

        private void materialLabel3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}

