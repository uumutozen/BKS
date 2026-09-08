using System.Drawing.Drawing2D;
namespace BKS;
internal sealed class LoginInputPanel : Panel
{
    private readonly TextBox input;
    private readonly Button? action;
    public LoginInputPanel(TextBox input, Button? action = null)
    {
        this.input = input;
        this.action = action;
        BackColor = Color.FromArgb(248, 250, 253);
        DoubleBuffered = true;
        ResizeRedraw = true;
        input.BorderStyle = BorderStyle.None;
        input.BackColor = BackColor;
        input.Font = new Font("Segoe UI", 11F);
        Controls.Add(input);
        if (action != null) Controls.Add(action);
        input.GotFocus += (_, _) => Invalidate();
        input.LostFocus += (_, _) => Invalidate();
    }
    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e);
        if (input == null) return;
        int Px(int n) => (int) Math.Ceiling(n * DeviceDpi / 96F);
        int actionWidth = action == null ? 0: Px(66);
        input.SetBounds(Px(14), Math.Max(Px(3), (Height - input.PreferredHeight) / 2), Math.Max(0, Width - Px(28) - actionWidth),
        input.PreferredHeight);
        action?.SetBounds(Math.Max(0, Width - actionWidth - Px(5)), Px(3), actionWidth, Math.Max(0, Height - Px(6)));
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using var border = new Pen(ContainsFocus ? RibbonPalette.Accent: Color.FromArgb(205, 215, 230), ContainsFocus ? 2: 1);
        if (Width> 2 && Height> 2) e.Graphics.DrawRectangle(border, 1, 1, Width - 3, Height - 3);
    }
}
internal sealed class LoginWorkspace : Panel
{
    private readonly LoginBrandPanel brand = new();
    private readonly Panel viewport = new()
    {
        AutoScroll = true,
        BackColor = Color.FromArgb(240, 245, 251)
    };
    private readonly Control card;
    private bool arranging;
    public LoginWorkspace(Control card)
    {
        this.card = card;
        Dock = DockStyle.Fill;
        BackColor = viewport.BackColor;
        card.Dock = DockStyle.None;
        card.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        viewport.Controls.Add(card);
        Controls.Add(viewport);
        Controls.Add(brand);
        viewport.SizeChanged += (_, _) => ArrangeCard();
    }
    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e);
        if (card == null || brand == null || viewport == null) return;
        float scale = DeviceDpi / 96F;
        int brandWidth = ClientSize.Width >= 920 * scale ? (int)(ClientSize.Width * .40F): 0;
        brand.Visible = brandWidth> 0;
        brand.SetBounds(0, 0, brandWidth, ClientSize.Height);
        viewport.SetBounds(brandWidth, 0, Math.Max(0, ClientSize.Width - brandWidth), ClientSize.Height);
        ArrangeCard();
    }
    private void ArrangeCard()
    {
        if (arranging || card == null) return;
        arranging = true;
        try
        {
            int Px(int n) => (int) Math.Ceiling(n * DeviceDpi / 96F);
            int available = Math.Max(0, viewport.ClientSize.Width - SystemInformation.VerticalScrollBarWidth);
            int width = Math.Max(Px(360), Math.Min(Px(464), available - Px(40)));
            int height = Px(600);
            int x = Math.Max(Px(20), (available - width) / 2);
            int y = Math.Max(Px(20), (viewport.ClientSize.Height - height) / 2);
            var scroll = viewport.AutoScrollPosition;
            card.SetBounds(x + scroll.X, y + scroll.Y, width, height);
            viewport.AutoScrollMinSize = new Size(width + Px(40), height + Px(40));
        }
        finally
        {
            arranging = false;
        }
    }
    protected override void OnDpiChangedAfterParent(EventArgs e)
    {
        base.OnDpiChangedAfterParent(e);
        PerformLayout();
    }
}
internal sealed class LoginBrandPanel : Panel
{
    public LoginBrandPanel()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (Width<1 || Height<1) return;
        var g = e.Graphics;
        using var gradient = new LinearGradientBrush(ClientRectangle, Color.FromArgb(20, 56, 111), Color.FromArgb(43, 112,
        187), 55F);
        g.FillRectangle(gradient, ClientRectangle);
        float scale = DeviceDpi / 96F;
        var state = g.Save();
        g.ScaleTransform(scale, scale);
        float w = Width / scale, h = Height / scale;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using var glow = new SolidBrush(Color.FromArgb(18, Color.White));
        g.FillEllipse(glow, w - 220, - 100, 360, 360);
        g.FillEllipse(glow, - 170, h - 260, 380, 380);
        using var pen = new Pen(Color.White, 2.4F);
        g.DrawEllipse(pen, 42, 48, 36, 36);
        g.DrawEllipse(pen, 54, 60, 12, 12);
        for (int i = 0; i<8; i++)
        {
            double a = i * Math.PI / 4;
            g.DrawLine(pen, 60 + (float) Math.Cos(a) * 7, 66 + (float) Math.Sin(a) * 7, 60 + (float) Math.Cos(a) * 25, 66 + (float) Math.Sin(a) * 25);
        }
        using var brand = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Pixel);
        using var title = new Font("Segoe UI", 34F, FontStyle.Bold, GraphicsUnit.Pixel);
        using var text = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
        using var small = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
        using var white = new SolidBrush(Color.White);
        using var muted = new SolidBrush(Color.FromArgb(211, 229, 250));
        g.DrawString("HELM SOFTWARE", brand, white, new PointF(99, 54));
        g.DrawString("Okulunuzun\nyönetim merkezi.", title, white, new RectangleF(38, 145, Math.Max(0, w - 66), 125));
        g.DrawString("Öğrenci kayıtları, ödemeler ve raporlar.\nGününüzü tek bir yerden yönetin.", text, muted, new RectangleF(42,
        293, Math.Max(0, w - 76), 82));
        if (h >= 610)
        {
            string[] labels =
            {
                "Öğrenci ve sınıf yönetimi",
                "Ödeme ve finans takibi",
                "Raporlar ve dosya arşivi"
            };
            for (int i = 0; i<labels.Length; i++)
            {
                float y = 410 + i * 47;
                g.FillRectangle(glow, 42, y, Math.Max(0, w - 84), 37);
                g.DrawLines(pen, new PointF[]
                {
                    new(55, y + 19),
                    new(60, y + 24),
                    new(68, y + 13)
                });
                g.DrawString(labels[i], small, white, new RectangleF(82, y + 9, Math.Max(0, w - 130), 24));
            }
        }
        if (h >= 440) g.DrawString("BKS  /  ANAOKULU YÖNETİMİ", small, muted, new PointF(42, h - 46));
        g.Restore(state);
    }
}
