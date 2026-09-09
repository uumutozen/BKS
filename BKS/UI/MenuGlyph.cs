using System.Drawing.Drawing2D;

namespace BKS;

internal static class MenuGlyph
{
    public static Bitmap Create(RibbonIcon icon, int size = 24)
    {
        var bitmap = new Bitmap(size, size);
        using var g = Graphics.FromImage(bitmap);
        g.Clear(Color.Transparent);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.ScaleTransform(size / 24F, size / 24F);
        g.TranslateTransform(2, 2);
        g.ScaleTransform(.7F, .7F);
        using var pen = new Pen(RibbonPalette.Icon(icon), 2.3F)
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round,
            LineJoin = LineJoin.Round
        };
        switch (icon)
        {
            case RibbonIcon.Add:
            g.DrawRectangle(pen, 3, 3, 22, 24);
            g.DrawLine(pen, 14, 9, 14, 21);
            g.DrawLine(pen, 8, 15, 20, 15);
            break;
            case RibbonIcon.Edit:
            g.DrawLines(pen, new Point[]
            {
                new(4, 22),
                new(6, 15),
                new(21, 0),
                new(27, 6),
                new(12, 21),
                new(4, 22)
            });
            break;
            case RibbonIcon.Archive:
            g.DrawRectangle(pen, 3, 8, 23, 18);
            g.DrawRectangle(pen, 1, 2, 27, 6);
            g.DrawLine(pen, 11, 14, 18, 14);
            break;
            case RibbonIcon.Restore:
            g.DrawArc(pen, 4, 4, 22, 22, 210, 295);
            g.DrawLines(pen, new Point[]
            {
                new(1, 5),
                new(4, 13),
                new(12, 11)
            });
            break;
            case RibbonIcon.Refresh:
            g.DrawArc(pen, 3, 3, 23, 23, 35, 285);
            g.DrawLines(pen, new Point[]
            {
                new(20, 1),
                new(27, 8),
                new(18, 9)
            });
            break;
            case RibbonIcon.Export:
            g.DrawLines(pen, new Point[]
            {
                new(3, 18),
                new(3, 26),
                new(26, 26),
                new(26, 18)
            });
            g.DrawLine(pen, 14, 1, 14, 19);
            g.DrawLines(pen, new Point[]
            {
                new(8, 13),
                new(14, 19),
                new(20, 13)
            });
            break;
            case RibbonIcon.Backup:
            g.DrawRectangle(pen, 3, 2, 23, 25);
            g.DrawRectangle(pen, 8, 2, 13, 8);
            g.DrawRectangle(pen, 8, 17, 13, 10);
            break;
            case RibbonIcon.Folder:
            g.DrawLines(pen, new Point[]
            {
                new(1, 25),
                new(1, 3),
                new(11, 3),
                new(15, 8),
                new(27, 8),
                new(27, 25),
                new(1, 25)
            });
            break;
            case RibbonIcon.Print:
            g.DrawRectangle(pen, 1, 9, 27, 13);
            g.DrawRectangle(pen, 6, 1, 17, 8);
            g.DrawRectangle(pen, 6, 18, 17, 11);
            break;
            case RibbonIcon.Search:
            g.DrawEllipse(pen, 2, 1, 18, 18);
            g.DrawLine(pen, 18, 18, 27, 27);
            break;
            default:
            g.DrawRectangle(pen, 2, 3, 25, 23);
            g.DrawLine(pen, 2, 10, 27, 10);
            g.DrawLine(pen, 10, 10, 10, 26);
            break;
        }
        return bitmap;
    }
}
