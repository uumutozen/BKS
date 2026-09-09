namespace BKS;
internal sealed class PhotoEditor
{
    private readonly PictureBox picture;
    private readonly Button choose;
    private readonly Button remove;
    private readonly Label status;
    public event Action<byte[]?>? PhotoChanged;
    public PhotoEditor(PictureBox picture, Button choose, Button remove, Label status)
    {
        this.picture = picture;
        this.choose = choose;
        this.remove = remove;
        this.status = status;
        picture.Paint += (_, e) =>
        {
            if (picture.Image == null) TextRenderer.DrawText(e.Graphics, "Fotoğraf ekleyin", picture.Font,
                picture.ClientRectangle, RibbonPalette.CaptionText,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        };
        picture.Disposed += (_, _) =>
        {
            var previous = picture.Image;
            picture.Image = null;
            previous?.Dispose();
        };
        remove.Enabled = picture.Image != null;
    }
    public void RemovePhoto()
    {
        SetBytes(null);
        PhotoChanged?.Invoke(null);
        status.Text = "Fotoğrafı kaldırmak için kaydı güncelleyin.";
    }
    public void SetBusy(bool busy)
    {
        choose.Enabled = picture.Enabled = !busy;
        remove.Enabled = !busy && picture.Image != null;
        if (busy) status.Text = "Fotoğraf yükleniyor…";
    }
    public void ShowError(string message)
    {
        status.ForeColor = Color.Firebrick;
        status.Text = message;
    }
    public void SetBytes(byte[]? bytes)
    {
        Image? next = null;
        if (bytes is
        {
            Length:> 0
        })
        {
            using var stream = new MemoryStream(bytes);
            using var image = Image.FromStream(stream, true, true);
            next = new Bitmap(image);
            // The preview never depends on an open file/stream.
        }
        var previous = picture.Image;
        picture.Image = next;
        previous?.Dispose();
        remove.Enabled = next != null;
        picture.Invalidate();
        status.ForeColor = ModernWinForms.Muted;
        status.Text = next == null ? "JPG, PNG veya BMP · En fazla 10 MB\nFotoğraf kayıtla kaydedilir.": "Fotoğraf hazır. Değişiklikleri kayıtla kaydedin.";
    }
    public void ChoosePhoto()
    {
        if (!choose.Enabled) return;
        using var dialog = new OpenFileDialog
        {
            Title = "Fotoğraf seçin",
            Filter = "Fotoğraflar (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
            CheckFileExists = true,
            Multiselect = false
        };
        if (dialog.ShowDialog(picture.FindForm()) != DialogResult.OK) return;
        try
        {
            var bytes = PhotoFiles.ReadNormalized(dialog.FileName);
            SetBytes(bytes);
            PhotoChanged?.Invoke(bytes);
        }
        catch (Exception)
        {
            ShowError("Fotoğraf açılamadı. Geçerli bir JPG, PNG veya BMP seçin (en fazla 10 MB).");
        }
    }
}
