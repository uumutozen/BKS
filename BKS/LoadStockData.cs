using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKS
{
    public class LoadStockData
    {
        private Guid _UserId;
        public LoadStockData(Guid UserId) 
        {
            _UserId = UserId;
        }
        //public void LoadStockData(Guid UserId)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        string query = "exec GetStudent @UserId";
        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        cmd.Parameters.AddWithValue("@UserId", UserId);

        //        DataTable dt = new DataTable();
        //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //        adapter.Fill(dt);
        //        dataGridViewStok.DataSource = dt;
        //        dataGridViewStok.Refresh();

        //    }
        //}
    }
}
