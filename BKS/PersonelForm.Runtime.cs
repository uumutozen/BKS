namespace BKS;

public partial class PersonelForm
{
    private void InitializeRuntimeState()
    {
        _photoEditor = new PhotoEditor(pbxPersonelPicture, btnChoosePhoto, btnRemovePhoto, lblPhotoStatus);
        _photoEditor.PhotoChanged += bytes => { Photo = bytes; _photoChanged = true; };
        Shown += (_, _) => RefreshRecordCommands();
        Activated += (_, _) => RefreshRecordCommands();
        KeyDown += Record_KeyDown;
        pbxPersonelPicture.Click += pbxPersonelPicture_Click;
        Screens.PrepareDesignerForm(this);
    }
    private void RefreshRecordCommands()
    {
        btnPersonelKaydet.Enabled = !AppConfiguration.DesignPreview && PersonelId == Guid.Empty;
        btnPersonelGuncelle.Enabled = btnPersonelSil.Enabled = !AppConfiguration.DesignPreview && PersonelId != Guid.Empty;
    }
    private void Record_KeyDown(object? sender, KeyEventArgs e)
    {
        if (!e.Control || e.KeyCode != Keys.S || AppConfiguration.DesignPreview) return;
        UiActions.Run(() => { if (PersonelId == Guid.Empty) RunPersonnelSave(); else RunPersonnelUpdate(); });
        RefreshRecordCommands();
        e.SuppressKeyPress = true;
    }
    private void ChoosePhoto_Click(object? sender, EventArgs e) => _photoEditor.ChoosePhoto();
    private void RemovePhoto_Click(object? sender, EventArgs e) => _photoEditor.RemovePhoto();
    private void CloseRecord_Click(object? sender, EventArgs e) => Close();
}
