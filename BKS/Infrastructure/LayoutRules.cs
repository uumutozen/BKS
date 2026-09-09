using System.Drawing;
namespace BKS;
public static class LayoutRules
{
    public static int FieldColumns(int availableWidth, float dpiScale) => Math.Clamp((int)(availableWidth / (265 * Math.Max(.5F,
    dpiScale))), 1, 4);
    public static int EditorHeight(int height, float ratio, float scale)
    {
        // A one-row search field must remain usable even when its old percentage was only 15%.
        var preferred = Math.Max((int) Math.Ceiling(96 * scale), (int)(height * ratio));
        return Math.Clamp(preferred, 0, Math.Max(0, height - Math.Min(height / 3, (int)(120 * scale))));
    }
    public static(int TitleHeight, int RibbonHeight, Rectangle Content, int FooterHeight) Workspace(
    int width, int height, float scale, bool expanded, bool hasTitle, bool hasFooter)
    {
        width = Math.Max(0, width);
        height = Math.Max(0, height);
        int Px(int value) => (int) Math.Ceiling(value * Math.Max(.5F, scale));
        int title = hasTitle ? Math.Min(height, Px(46)): 0;
        int ribbon = Math.Min(height - title, Px(expanded ? 134: 32));
        int footer = hasFooter ? Math.Min(height - title - ribbon, Px(28)): 0;
        return(title, ribbon, new Rectangle(0, title + ribbon, width, height - title - ribbon - footer), footer);
    }
}
