namespace uWidgets.Core.Models;

public record AppSettings(Theme Theme, Layout Layout, Region Region, bool RunOnStartup, string? IgnoreUpdate);
