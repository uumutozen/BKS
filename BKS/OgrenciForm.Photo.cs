namespace BKS;

public partial class OgrenciForm
{
    private void pictureBox1_Click(object sender, EventArgs e) => _photoEditor.ChoosePhoto();

    internal void SetPhoto(byte[]? bytes)
    {
        Photo = bytes;
        try { _photoEditor.SetBytes(bytes); }
        catch (Exception error) when (error is ArgumentException or OutOfMemoryException or System.Runtime.InteropServices.ExternalException)
        {
            // Bozuk bir eski fotoğraf öğrenci kartının açılmasını engellemez; özgün veri korunur.
            _photoEditor.ShowError("Kayıtlı fotoğraf görüntülenemedi. Yeni bir fotoğraf seçebilirsiniz.");
        }
    }
}
