using System.Data;
using System.Data.SqlClient;

namespace BKS;

public partial class PersonelForm
{
    private void DisposePhotoLoader()
    {
        if (_photoCancellationDisposed) return;
        _photoCancellationDisposed = true;
        _photoLoadCancellation.Cancel();
        _photoLoadCancellation.Dispose();
    }

    public async void PersonelForm_Load(object sender, EventArgs e)
    {
        txtPersonelKimlik.Enabled = true;
        lblKimlikNum.Visible = true;
        cbxPersoneIIsAyrıldı_CheckedChanged(sender, e);
        if (AppConfiguration.DesignPreview || PersonelId == Guid.Empty) return;
        _photoEditor.SetBusy(true);
        try
        {
            var bytes = await PersonnelPhotos.ReadAsync(connectionString, PersonelId, UserId, _photoLoadCancellation.Token);
            if (!IsDisposed && !_photoChanged)
            {
                _photoEditor.SetBytes(bytes);
                Photo = bytes;
            }
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception)
        {
            if (!IsDisposed) _photoEditor.ShowError("Kayıtlı fotoğraf yüklenemedi. Bağlantınızı kontrol edip kartı yeniden açabilirsiniz.");
        }
        finally
        {
            if (!IsDisposed) _photoEditor.SetBusy(false);
        }
    }

    private void pbxPersonelPicture_Click(object sender, EventArgs e) => _photoEditor.ChoosePhoto();
}
