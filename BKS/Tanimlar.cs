using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKS
{
    public class Tanimlar
    {
        public static void ekle(string tanimAdi,int Code,string groupCode,Guid SchoolId,Guid tanimId)
        {
            string tanimadi = tanimAdi;
            int code = Code;
            string groupcode = groupCode;
            Guid schoolId = SchoolId;
            Guid tanimid = tanimId;
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO FixedDefinitions (FixedDefinitionId, GroupCode,Code,Name,SchoolId) VALUES (@TanimId, @GroupCode, @Code,@Name,@SchoolId)", conn);
                    cmd.Parameters.AddWithValue("@Name", tanimadi);
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.Parameters.AddWithValue("@GroupCode", groupcode);
                    cmd.Parameters.AddWithValue("@TanimId", tanimid);
                    cmd.Parameters.AddWithValue("@SchoolId", schoolId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Tanım Eklendi. Sistemini Kontrol Et.", tanimadi, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {

                MessageBox.Show("Tanım Eklenemedi Verileri Kontrol Et.", tanimadi, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
           

        }
      

    }
}
