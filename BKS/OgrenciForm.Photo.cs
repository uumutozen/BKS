using System.Data;
using System.Data.SqlClient;
namespace BKS;
public partial class OgrenciForm
{
    private void pictureBox1_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Fotoğraf |*.png;*.jpeg";
        openFileDialog.Title = "Bir Fotoğraf Seçin";
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);
            if (pictureBox1.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    Photo = ms.ToArray();
                }
            }
        }
    }
}
