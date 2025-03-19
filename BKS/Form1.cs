using System;
using System.Data.SqlClient;
using MaterialSkin;
using MaterialSkin.Controls;
using BKS;

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

            // Material Skin theme ayarlar�
            var manager = MaterialSkinManager.Instance;
            manager.AddFormToManage(this);
            manager.Theme = MaterialSkinManager.Themes.LIGHT;
            manager.ColorScheme = new ColorScheme(Primary.Blue500, Primary.Blue700, Primary.Blue300, Accent.LightBlue200, TextShade.WHITE);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string username = userName.Text.Trim();
            if (!string.IsNullOrEmpty(username))
            {
                // Son giri� bilgisini y�kle

            }
        }
        private Guid GetUserIdFromDatabase(string username, string password)
        {
            Guid userId = Guid.NewGuid(); // Varsay�lan olarak -1 d�nd�r

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
                    MessageBox.Show("Ba�lant� hatas�: " + ex.Message);
                }
            }
            return userId;
        }
        private void bttnLgn_Click(object sender, EventArgs e)
        {
            // Kullan�c� ad� ve �ifre giri�leri
            string username = userName.Text.Trim();
            string password = passWord.Text.Trim();

            // SQL sorgusu

            string sorgu = "exec ValidateAndUpdateLogin @Email, @Password";

            Form2 form2 = new Form2();
            form2.UserId = GetUserIdFromDatabase(username, password);
            Form1 form1 = new Form1();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand datecom = new SqlCommand(sorgu, connection))
                    {
                        datecom.Parameters.AddWithValue("@Email", username);
                        datecom.Parameters.AddWithValue("@Password", password);

                        using (SqlCommand command = new SqlCommand(sorgu, connection))
                        {

                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@Email", username);
                            command.Parameters.AddWithValue("@Password", password);

                            // Sonucu kontrol et
                            int userCount = (int)command.ExecuteScalar();
                            if (userCount > 0)
                            {
                                MessageBox.Show("Giri� ba�ar�l�!", "Ba�ar�l�", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                this.Hide();  // Form1�i gizle
                                form2.ShowDialog(); // Form2'yi a�, di�er i�lemleri blokla
                                form2.UserId = GetUserIdFromDatabase(username, password);
                                Environment.Exit(0); // Form2 kapand�ktan sonra t�m program� kapat

                            }
                            else
                            {
                                MessageBox.Show("Kullan�c� ad� veya �ifre hatal�!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata olu�tu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0); // T�m program� kapat�r.
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

