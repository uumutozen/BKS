namespace BKS;
internal sealed class EditorGridPanel : Panel
{
    private readonly Control editor;
    private readonly Control grid;
    private readonly float ratio;
    public EditorGridPanel(Control editor, Control grid, float ratio)
    {
        this.editor = editor;
        this.grid = grid is DataGridView table ? new ListSurface(table): grid;
        this.ratio = ratio;
        Dock = DockStyle.Fill;
        BackColor = ModernWinForms.PageBack;
        foreach (var control in new[]
        {
            editor,
            this.grid
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
        if (editor == null || grid == null) return;
        var gap = (int) Math.Ceiling(10 * DeviceDpi / 96F);
        var width = Math.Max(0, ClientSize.Width - gap * 2);
        var height = Math.Max(0, ClientSize.Height - gap * 3);
        var editorHeight = LayoutRules.EditorHeight(height, ratio, DeviceDpi / 96F);
        editor.SetBounds(gap, gap, width, editorHeight);
        grid.SetBounds(gap, gap * 2 + editorHeight, width, Math.Max(0, height - editorHeight));
    }
    protected override void OnDpiChangedAfterParent(EventArgs e)
    {
        base.OnDpiChangedAfterParent(e);
        PerformLayout();
    }
}
