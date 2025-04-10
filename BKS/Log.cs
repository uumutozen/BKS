using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKS
{
    public class Log
    {
        Form2 form2 = new Form2();
        public void DeleteAndLog(string tableName, string primaryKeyColumn, Guid id)
        {
            using (SqlConnection conn = new SqlConnection(form2.connectionString))
            {
                conn.Open();

                // Silinecek kaydı önce JSON formatında al
                string selectQuery = $"SELECT * FROM {tableName} WHERE {primaryKeyColumn} = @Id";
                string deletedDataJson = "";

                using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                {
                    selectCmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            deletedDataJson = JsonConvert.SerializeObject(dt);

                            // Debugging için: JSON verisini konsola yazdır
                            Console.WriteLine("Deleted Data JSON: " + deletedDataJson);
                        }
                        else
                        {
                            Console.WriteLine("No data found for the provided ID.");
                        }
                    }
                }

                // Eğer silinecek veri bulunduysa log'a kaydet
                if (!string.IsNullOrEmpty(deletedDataJson))
                {
                    string logQuery = "INSERT INTO DeleteLog (TableName, DeletedData, DeletedAt) VALUES (@TableName, @DeletedData, @DeletedAt)";
                    using (SqlCommand logCmd = new SqlCommand(logQuery, conn))
                    {
                        logCmd.Parameters.AddWithValue("@TableName", tableName);
                        logCmd.Parameters.AddWithValue("@DeletedData", deletedDataJson);
                        logCmd.Parameters.AddWithValue("@DeletedAt", DateTime.Now);
                        logCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    Console.WriteLine("No data to log.");
                }

                // Şimdi asıl kaydı sil
                string deleteQuery = $"DELETE FROM {tableName} WHERE {primaryKeyColumn} = @Id";
                using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                {
                    deleteCmd.Parameters.AddWithValue("@Id", id);
                    deleteCmd.ExecuteNonQuery();
                }
            }
        }

    }
}
