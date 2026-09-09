namespace BKS;
public enum RibbonIcon
{
    Add, Edit, Archive, Restore, Refresh, Export, Backup, Folder, View, Help, Print, Search
}
public enum RibbonButtonSize
{
    Small, Large
}
/// <summary>One action shared by the ribbon, keyboard and context menu.</summary>
public sealed record RibbonCommand(
    string Text,
    RibbonIcon Icon,
    Action Execute,
    Func<bool>? CanExecute = null,
    Func<bool>? IsSelected = null)
{
    public string CommandId { get; init; } = string.Empty;
    public string? Permission { get; init; }
    public string Tooltip { get; init; } = string.Empty;
    public Keys Shortcut { get; init; }
    public RibbonButtonSize Size { get; init; } = RibbonButtonSize.Small;
    public int Order { get; init; }
    public string Group { get; init; } = string.Empty;
    public bool IsExecuting { get; private set; }

    public bool IsEnabled => !IsExecuting && CanExecute?.Invoke() != false;
    public event EventHandler? StateChanged;
    public void Invoke()
    {
        if (!IsEnabled) return;
        IsExecuting = true;
        StateChanged?.Invoke(this, EventArgs.Empty);
        try
        {
            UiActions.Run(Execute);
        }
        finally
        {
            IsExecuting = false;
            StateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
