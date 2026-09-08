using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
namespace BKS;
internal sealed class PhotoEditor : Panel
{
    private readonly PictureBox picture;
    private readonly Button choose;
    private readonly Button remove;
    private readonly Label status;
    public event Action<byte[]?>? PhotoChanged;
    public PhotoEditor(PictureBox picture)
    {
        this.picture = picture;
        BackColor = Color.FromArgb(244, 248, 253);
        picture.Dock = DockStyle.None;
        picture.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        picture.BackColor = Color.FromArgb(231, 238, 248);
        picture.SizeMode = PictureBoxSizeMode.Zoom;
        picture.Cursor = Cursors.Hand;
        picture.Click += (_, _) => ChoosePhoto();
        picture.Paint += (_, e) =>
        {
            if (picture.Image == null) TextRenderer.DrawText(e.Graphics, "Fotoğraf ekleyin", Font, picture.ClientRectangle,
            RibbonPalette.CaptionText, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        };
        choose = Screens.Button("Fotoğraf seç", ChoosePhoto);
        choose.BackColor = RibbonPalette.Accent;
        remove = Screens.Button("Kaldır", () =>
        {
            SetBytes(null);
            PhotoChanged?.Invoke(null);
            status.Text = "Fotoğrafı kaldırmak için personel kaydını güncelleyin.";
        });
        remove.BackColor = Color.FromArgb(228, 235, 245);
        remove.ForeColor = RibbonPalette.Text;
        status = new Label
        {
            Text = "JPG, PNG veya BMP · En fazla 10 MB\nFotoğraf personel kaydıyla kaydedilir.",
            ForeColor = ModernWinForms.Muted,
            Font = new Font("Segoe UI", 9F)
        };
        Controls.Add(picture);
        Controls.Add(choose);
        Controls.Add(remove);
        Controls.Add(status);
        remove.Enabled = picture.Image != null;
    }
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            var previous = picture.Image;
            picture.Image = null;
            previous?.Dispose();
        }
        base.Dispose(disposing);
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
        status.Text = next == null ? "JPG, PNG veya BMP · En fazla 10 MB\nFotoğraf personel kaydıyla kaydedilir.": "Fotoğraf hazır. Değişiklikleri personel kaydıyla kaydedin.";
    }
    public void ChoosePhoto()
    {
        if (!choose.Enabled) return;
        using var dialog = new OpenFileDialog
        {
            Title = "Personel fotoğrafını seçin",
            Filter = "Fotoğraflar (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
            CheckFileExists = true,
            Multiselect = false
        };
        if (dialog.ShowDialog(FindForm()) != DialogResult.OK) return;
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
    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e);
        if (picture == null || choose == null || status == null) return;
        int Px(int n) => (int) Math.Ceiling(n * DeviceDpi / 96F);
        int width = Math.Max(0, Width - Px(24));
        picture.SetBounds(Px(12), Px(12), width, Px(172));
        choose.SetBounds(Px(12), Px(194), Math.Max(0, width * 2 / 3 - Px(4)), Px(34));
        remove.SetBounds(Px(16) + width * 2 / 3, Px(194), Math.Max(0, width / 3 - Px(4)), Px(34));
        status.SetBounds(Px(12), Px(238), width, Px(66));
    }
}
internal static class PhotoFiles
{
    public static byte[] ReadNormalized(string path)
    {
        var file = new FileInfo(path);
        if (file.Length == 0 || file.Length> 10 * 1024 * 1024) throw new InvalidDataException("Fotoğraf boyutu uygun değil.");
        using var stream = File.OpenRead(path);
        using var original = Image.FromStream(stream, true, true);
        if ((long) original.Width * original.Height> 40_000_000) throw new InvalidDataException("Fotoğraf çözünürlüğü çok yüksek.");
        if (original.PropertyIdList.Contains(0x0112))
        {
            var orientation = original.GetPropertyItem(0x0112)?.Value;
            if (orientation is
            {
                Length:> 0
            }) original.RotateFlip(orientation[0] switch
            {
                2 => RotateFlipType.RotateNoneFlipX,
                3 => RotateFlipType.Rotate180FlipNone,
                4 => RotateFlipType.Rotate180FlipX,
                5 => RotateFlipType.Rotate90FlipX,
                6 => RotateFlipType.Rotate90FlipNone,
                7 => RotateFlipType.Rotate270FlipX,
                8 => RotateFlipType.Rotate270FlipNone,
                _ => RotateFlipType.RotateNoneFlipNone
            });
        }
        double ratio = Math.Min(1D, 1024D / Math.Max(original.Width, original.Height));
        using var bitmap = new Bitmap(Math.Max(1, (int)(original.Width * ratio)), Math.Max(1, (int)(original.Height * ratio)));
        using (var g = Graphics.FromImage(bitmap))
        {
            g.Clear(Color.White);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(original, 0, 0, bitmap.Width, bitmap.Height);
        }
        using var output = new MemoryStream();
        bitmap.Save(output, ImageFormat.Png);
        return output.ToArray();
    }
}
