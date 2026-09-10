namespace BKS;

/// <summary>Office mavisi. Ribbon, sekmeler, menüler ve listeler aynı renkleri kullanır.</summary>
internal static class RibbonPalette
{
    public static readonly Color TabStrip = Color.FromArgb(218, 236, 250);
    public static readonly Color Surface = Color.FromArgb(239, 247, 253);
    public static readonly Color Group = Color.FromArgb(239, 247, 253);
    public static readonly Color Caption = Color.FromArgb(228, 241, 252);
    public static readonly Color CaptionText = Color.FromArgb(68, 87, 111);
    public static readonly Color Border = Color.FromArgb(189, 214, 235);
    public static readonly Color Text = Color.FromArgb(37, 54, 75);
    public static readonly Color Accent = Color.FromArgb(40, 99, 163);
    public static readonly Color ActiveBorder = Color.FromArgb(189, 214, 235);
    public static readonly Color HoverTop = Color.FromArgb(210, 231, 249);
    public static readonly Color HoverBottom = HoverTop;
    public static readonly Color Pressed = Color.FromArgb(190, 218, 243);
    public static readonly Color Disabled = Color.FromArgb(112, 126, 144);
    public static readonly Color Workspace = Color.FromArgb(228, 241, 252);
    public static readonly Color DocumentTab = Color.FromArgb(218, 236, 250);
    public static readonly Color GridRow = Color.FromArgb(250, 253, 255);
    public static readonly Color GridHeader = Color.FromArgb(218, 236, 250);
    public static readonly Color GridLine = Color.FromArgb(218, 233, 246);
    public static readonly Color SelectedRow = Color.FromArgb(200, 224, 246);
    public static readonly Color AlternateRow = Color.FromArgb(239, 247, 253);

    public static Color Icon(RibbonIcon icon) => icon == RibbonIcon.Archive
        ? Color.FromArgb(177, 62, 57) : Accent;
}
