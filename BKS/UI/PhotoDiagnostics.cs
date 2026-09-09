using System.Drawing.Imaging;
namespace BKS;
internal static class PhotoDiagnostics
{
    public static void Run(Action<bool, string> check)
    {
        string directory = Path.Combine(Path.GetTempPath(), "bks-photo-check-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        try
        {
            string photo = Path.Combine(directory, "photo.jpg");
            using (var source = new Bitmap(1600, 800)) source.Save(photo, ImageFormat.Jpeg);
            byte[] bytes = PhotoFiles.ReadNormalized(photo);
            using (var stream = new MemoryStream(bytes))
            using (var normalized = Image.FromStream(stream))
            check(normalized.Width == 1024 && normalized.Height == 512, "Photo: aspect ratio and maximum size");
            using (var file = File.Open(photo, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            check(file.Length> 0, "Photo: original file is not locked");
            using (var picture = new PictureBox())
            using (var choose = new Button())
            using (var remove = new Button())
            using (var status = new Label())
            {
                var editor = new PhotoEditor(picture, choose, remove, status);
                int events = 0;
                editor.PhotoChanged += _ => events++;
                editor.SetBytes(bytes);
                check(picture.Image != null, "Photo: preview can load after stream closure");
                editor.SetBytes(null);
                check(picture.Image == null && events == 0, "Photo: programmatic loading does not mark a user edit");
            }
            string invalid = Path.Combine(directory, "invalid.jpg");
            File.WriteAllText(invalid, "not an image");
            bool rejected = false;
            try
            {
                PhotoFiles.ReadNormalized(invalid);
            }
            catch
            {
                rejected = true;
            }
            check(rejected, "Photo: invalid image rejected");
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }
}
