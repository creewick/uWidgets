namespace uWidgets.Core.Models;

public record AppSettings(Theme Theme, Layout Layout, Dimensions Dimensions, Region Region, bool RunOnStartup, string? IgnoreUpdate);
