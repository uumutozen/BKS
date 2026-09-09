namespace BKS
{
    public partial class Form2 : Form
    {
        private const int StudentModuleTag = 4010;
        private const int PersonelModuleTag = 4020;
        public string connectionString = AppConfiguration.ConnectionString;
        private DataGridView aktifDGV;
        private readonly List<TabPage> _allModulePages = new List<TabPage>();
        public Form2()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            ConfigureMainWindow();
            BuildResponsiveLayout();
            this.Text = "Anaokulu Yönetim Sistemi";
        }
        public Guid sinifid
        {
            get;
            set;
        }
        public Guid UserId
        {
            get;
            set;
        }
        public string Role
        {
            get;
            set;
        }
        public byte[] Photo
        {
            get;
            set;
        }
    }
}
