namespace BKS;

/// <summary>Office mavisi. Ribbon, sekmeler, menüler ve listeler aynı renkleri kullanır.</summary>
internal static class RibbonPalette
{
    public static readonly Color TabStrip = Color.FromArgb(244, 247, 251);
    public static readonly Color Surface = Color.FromArgb(250, 252, 254);
    public static readonly Color Group = Color.FromArgb(255, 255, 255);
    public static readonly Color Caption = Color.FromArgb(247, 249, 252);
    public static readonly Color CaptionText = Color.FromArgb(68, 87, 111);
    public static readonly Color Border = Color.FromArgb(220, 226, 233);
    public static readonly Color Text = Color.FromArgb(37, 54, 75);
    public static readonly Color Accent = Color.FromArgb(40, 99, 163);
    public static readonly Color ActiveBorder = Color.FromArgb(220, 226, 233);
    public static readonly Color HoverTop = Color.FromArgb(232, 242, 253);
    public static readonly Color HoverBottom = HoverTop;
    public static readonly Color Pressed = Color.FromArgb(210, 229, 249);
    public static readonly Color Disabled = Color.FromArgb(112, 126, 144);
    public static readonly Color Workspace = Color.FromArgb(247, 249, 252);
    public static readonly Color DocumentTab = Color.FromArgb(242, 245, 249);
    public static readonly Color GridHeader = Color.FromArgb(244, 247, 251);
    public static readonly Color GridLine = Color.FromArgb(233, 237, 242);
    public static readonly Color SelectedRow = Color.FromArgb(219, 235, 252);
    public static readonly Color AlternateRow = Color.FromArgb(250, 252, 254);

    public static Color Icon(RibbonIcon icon) => icon == RibbonIcon.Archive
        ? Color.FromArgb(177, 62, 57) : Accent;
}
