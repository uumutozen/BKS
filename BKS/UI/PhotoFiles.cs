using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace BKS;

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
