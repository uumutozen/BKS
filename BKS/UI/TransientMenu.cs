namespace BKS;

/// <summary>Keep a popup alive until WinForms finishes dispatching its closing click.</summary>
internal static class TransientMenu
{
    public static ContextMenuStrip Create(Control owner)
    {
        var menu = new ContextMenuStrip();
        EventHandler release = (_, _) => menu.Dispose();
        owner.Disposed += release;
        menu.Disposed += (_, _) => owner.Disposed -= release;
        menu.Closed += (_, _) =>
        {
            // Disposing inside Closed invalidates the menu while ToolStrip still uses it.
            if (!owner.IsDisposed && owner.IsHandleCreated)
                owner.BeginInvoke(new Action(() => menu.Dispose()));
        };
        return menu;
    }
}
