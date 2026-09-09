using System.Data;
using System.Data.SqlClient;

namespace BKS;

public partial class PersonelForm
{
    private bool ValidatePersonnelIdentity()
    {
        foreach (var item in new(Control Input, string Name)[]
        {
            (txtPersonelAd, "Ad"),
            (txtPersonelSoyad, "Soyad"),
            (txtPersonelKimlik, "Kimlik / pasaport numarası"),
            (txtPersonelMail, "E-posta")
        })
        {
            if (!string.IsNullOrWhiteSpace(item.Input.Text)) continue;
            Screens.RevealAndFocus(item.Input);
            MessageBox.Show(item.Name + " alanını doldurun.", "Eksik bilgi");
            item.Input.Focus();
            return false;
        }
        return true;
    }

    public bool IsValidTCKimlikNo(string tc)
    {
        if (tc.Length != 11 || !tc.All(char.IsDigit) || tc.StartsWith("0"))
        return false;
        int[] digits = tc.Select(ch => int.Parse(ch.ToString())).ToArray();
        int toplam1 = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
        int toplam2 = digits[1] + digits[3] + digits[5] + digits[7];
        int onuncu = ((toplam1 * 7) - toplam2) % 10;
        int onbirinci = digits.Take(10).Sum() % 10;
        return digits[9] == onuncu && digits[10] == onbirinci;
    }
}
