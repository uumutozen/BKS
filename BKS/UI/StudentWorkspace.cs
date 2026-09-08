namespace BKS;

internal sealed class StudentWorkspace : Panel
{
    private readonly Control students;
    private readonly Control classes;
    private bool arranging;
    public StudentWorkspace(Control students, Control classes)
    {
        this.students = students; this.classes = classes;
        Dock = DockStyle.Fill; AutoScroll = true; BackColor = ModernWinForms.PageBack;
        foreach (var child in new[] { students, classes }) { child.Dock = DockStyle.None; child.Anchor = AnchorStyles.Top | AnchorStyles.Left; Controls.Add(child); }
    }
    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e); if (arranging || students == null || classes == null) return;
        arranging = true;
        try
        {
            float scale = DeviceDpi / 96F;
            int width = Math.Max(0, ClientSize.Width - SystemInformation.VerticalScrollBarWidth);
            bool wide = width >= 1000 * scale;
            int height = Math.Max(ClientSize.Height, (int)((wide ? 420 : 720) * scale));
            var scroll = AutoScrollPosition;
            if (wide)
            {
                int first = (int)(width * .64F);
                students.SetBounds(scroll.X, scroll.Y, first, height);
                classes.SetBounds(first + scroll.X, scroll.Y, width - first, height);
            }
            else
            {
                int first = (int)(height * .60F);
                students.SetBounds(scroll.X, scroll.Y, width, first);
                classes.SetBounds(scroll.X, first + scroll.Y, width, height - first);
            }
            AutoScrollMinSize = new Size(0, height);
        }
        finally { arranging = false; }
    }
}
