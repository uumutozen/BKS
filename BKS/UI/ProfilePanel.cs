namespace BKS;
internal sealed class ProfilePanel : Panel
{
    private readonly Control photo;
    private readonly Control fields;
    private bool arranging;
    public ProfilePanel(Control photo, Control fields)
    {
        this.photo = photo;
        this.fields = fields;
        Dock = DockStyle.Fill;
        AutoScroll = true;
        BackColor = Color.White;
        foreach (var control in new[]
        {
            photo,
            fields
        })
        {
            control.Dock = DockStyle.None;
            control.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            Controls.Add(control);
        }
    }
    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e);
        if (arranging || photo == null || fields == null) return;
        arranging = true;
        try
        {
            int Px(int n) => (int) Math.Ceiling(n * DeviceDpi / 96F);
            int width = Math.Max(0, ClientSize.Width - SystemInformation.VerticalScrollBarWidth);
            bool wide = width >= Px(720);
            int height = Math.Max(ClientSize.Height, Px(wide ? 390: 660));
            var scroll = AutoScrollPosition;
            if (wide)
            {
                photo.SetBounds(Px(12) + scroll.X, Px(8) + scroll.Y, Px(236), Px(310));
                fields.SetBounds(Px(260) + scroll.X, scroll.Y, Math.Max(0, width - Px(260)), height);
            }
            else
            {
                photo.SetBounds(Math.Max(Px(12), (width - Px(236)) / 2) + scroll.X, Px(8) + scroll.Y, Math.Min(Px(236), Math.Max(0,
                width - Px(24))), Px(310));
                fields.SetBounds(scroll.X, Px(330) + scroll.Y, width, height - Px(330));
            }
            AutoScrollMinSize = new Size(0, height);
        }
        finally
        {
            arranging = false;
        }
    }
}
