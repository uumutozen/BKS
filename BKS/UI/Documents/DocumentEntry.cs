namespace BKS;

internal sealed record DocumentEntry(TabPage Page, Form? Form, string? Permission, bool KeepAlive);
