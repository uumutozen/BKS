namespace BKS;

public partial class OgrenciForm
{
    private void InitializeRuntimeState()
    {
        _photoEditor = new PhotoEditor(pictureBox1, btnChoosePhoto, btnRemovePhoto, lblPhotoStatus);
        _photoEditor.PhotoChanged += bytes => { Photo = bytes;  };
        Shown += (_, _) => RefreshRecordCommands();
        Activated += (_, _) => RefreshRecordCommands();
        KeyDown += Record_KeyDown;
        Screens.PrepareDesignerForm(this);
    }
    private void RefreshRecordCommands()
    {
        btnAddStock.Enabled = !AppConfiguration.DesignPreview && StudentId == Guid.Empty;
        btnGuncelle.Enabled = btnOgrenciYonetimiSil.Enabled = !AppConfiguration.DesignPreview && StudentId != Guid.Empty;
    }
    private void Record_KeyDown(object? sender, KeyEventArgs e)
    {
        if (!e.Control || e.KeyCode != Keys.S || AppConfiguration.DesignPreview) return;
        UiActions.Run(() => { if (StudentId == Guid.Empty) RunStudentSave(); else RunStudentUpdate(); });
        RefreshRecordCommands();
        e.SuppressKeyPress = true;
    }
    private void ChoosePhoto_Click(object? sender, EventArgs e) => _photoEditor.ChoosePhoto();
    private void RemovePhoto_Click(object? sender, EventArgs e) => _photoEditor.RemovePhoto();
    private void CloseRecord_Click(object? sender, EventArgs e) => Close();
    private void ClearStudent_Click(object? sender, EventArgs e)
    {
        txtOgrenciAd.Clear(); textSoyad.Clear(); textOgrenciKod.Clear(); textOgrenciDetay.Clear();
        txtBabaAd.Clear(); txtAnneAd.Clear(); txtBabaTel.Clear(); txtAnneTel.Clear();
        txtBabaEvAdres.Clear(); txtAnneEvAdres.Clear(); cmbogrsınıf.SelectedIndex = -1;
        checkEvet.Checked = checkAktif.Checked = checkOdemeDurum.Checked = false;
        numericPrice.Value = 0; dateDogum.Value = DateTime.Today;
        _photoEditor.RemovePhoto();
    }
}
