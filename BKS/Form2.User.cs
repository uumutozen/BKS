using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.ComponentModel;
using System.Collections;
namespace BKS;
public partial class Form2
{
    private void LoadModulesFromApi(Guid userId, string role)
    {
        _ = InitializeSessionAsync();
    }
    public string GetLastLoginTime(Guid userId)
    {
        try
        {
            return ExecuteStringOrDefault(
            "SELECT PreviousLogin FROM CompanyUsers WHERE UserId = @UserId",
            "Bilinmiyor",
            DbParam("@UserId", userId));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hata: " + ex.Message);
            return "Bilinmiyor";
        }
    }
    public string GetLastUser(Guid userId)
    {
        try
        {
            return ExecuteStringOrDefault(
            "SELECT Firstname FROM CompanyUsers WHERE UserId = @UserId",
            "Bilinmiyor",
            DbParam("@UserId", userId));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hata: " + ex.Message);
            return "Bilinmiyor";
        }
    }
    private string GetCompanyName(Guid userId)
    {
        try
        {
            return ExecuteStringOrDefault(
            @"SELECT ad = (
                      SELECT ce.CompanyName
                      FROM Companies ce
                      WHERE ce.CompanyId = c.CompanyId
                  )
                  FROM CompanyUsers c
                  WHERE c.UserId = @UserId",
            "Bilinmiyor",
            DbParam("@UserId", userId));
        }
        catch (Exception ex)
        {
            MessageBox.Show("Bağlantı hatası: " + ex.Message);
            return "Bilinmiyor";
        }
    }
}
