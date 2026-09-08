namespace BKS;
/// <summary>Explicit, non-overlapping regions; independent of WinForms docking z-order.</summary>
internal sealed class RibbonWorkspace : Panel
{
    private readonly Control content;
    private readonly BksRibbon ribbon;
    private readonly Control? title;
    private Control? footer;
    public BksRibbon Ribbon => ribbon;
    public bool Embedded
    {
        get;
        set;
    }
    public void Embed()
    {
        Embedded = true;
        ribbon.Visible = false;
        PerformLayout();
    }
    public RibbonWorkspace(Control content, BksRibbon ribbon, Control? title = null, Control? footer = null)
    {
        this.content = content;
        this.ribbon = ribbon;
        this.title = title;
        this.footer = footer;
        Dock = DockStyle.Fill;
        Margin = Padding.Empty;
        BackColor = ModernWinForms.PageBack;
        foreach (var control in new Control?[]
        {
            content,
            ribbon,
            title,
            footer
        })
        if (control != null) Attach(control);
        ribbon.ExpandedChanged += (_, _) => PerformLayout();
        _ = new RecordContextMenus(this, ribbon);
    }
    private void Attach(Control control)
    {
        control.Dock = DockStyle.None;
        control.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        control.Margin = Padding.Empty;
        Controls.Add(control);
    }
    public void SetFooter(Control control)
    {
        if (footer != null) Controls.Remove(footer);
        footer = control;
        Attach(control);
        PerformLayout();
    }
    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e);
        if (content == null || ribbon == null) return;
        var regions = LayoutRules.Workspace(ClientSize.Width, ClientSize.Height, DeviceDpi / 96F,
        ribbon.IsExpanded, title != null, footer != null);
        if (Embedded)
        {
            int embeddedFooter = footer == null ? 0 : Math.Min(Height, (int)Math.Ceiling(28 * DeviceDpi / 96F));
            content.SetBounds(0, 0, Width, Math.Max(0, Height - embeddedFooter));
            footer?.SetBounds(0, Height - embeddedFooter, Width, embeddedFooter);
            if (title != null) title.Visible = false;
            ribbon.Visible = false;
            return;
        }
        title?.SetBounds(0, 0, ClientSize.Width, regions.TitleHeight);
        ribbon.SetBounds(0, regions.TitleHeight, ClientSize.Width, regions.RibbonHeight);
        content.Bounds = regions.Content;
        footer?.SetBounds(0, regions.Content.Bottom, ClientSize.Width, regions.FooterHeight);
    }
    protected override void OnDpiChangedAfterParent(EventArgs e)
    {
        base.OnDpiChangedAfterParent(e);
        PerformLayout();
    }
}
