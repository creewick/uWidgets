namespace uWidgets.Core;

/// <summary>
/// Constants for the application.
/// </summary>
public static class Const
{
    /// <summary>
    /// The name of the application.
    /// </summary>
    public const string AppName = "uWidgets";
    /// <summary>
    /// The folder with the application.
    /// </summary>
    public static readonly string CurrentFolder = Path.GetDirectoryName(Environment.ProcessPath)!;    
    /// <summary>
    /// The folder with the widgets.
    /// </summary>
    public static readonly string WidgetsFolder = Path.Combine(CurrentFolder, WidgetsFolderName);
    /// <summary>
    /// The path to the application settings file.
    /// </summary>
    public static readonly string AppSettingsFile = Path.Combine(CurrentFolder, AppSettingsFileName);
    /// <summary>
    /// The path to the layout file.
    /// </summary>
    public static readonly string LayoutFile = Path.Combine(CurrentFolder, LayoutFileName);
    
    private static string WidgetsFolderName => "Widgets";
    private static string AppSettingsFileName => "appSettings.json";
    private static string LayoutFileName => "layout.json";
}