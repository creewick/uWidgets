namespace uWidgets.Core;

public static class Const
{
    public static string AppName = "uWidgets";
    private static string WidgetsFolderName => "Widgets";
    private static string AppSettingsFileName => "appSettings.json";
    private static string LayoutFileName => "layout.json";
    
    public static string CurrentFolder = Path.GetDirectoryName(Environment.ProcessPath)!;    
    public static string WidgetsFolder = Path.Combine(CurrentFolder, WidgetsFolderName);
    public static string AppSettingsFile = Path.Combine(CurrentFolder, AppSettingsFileName);
    public static string LayoutFile = Path.Combine(CurrentFolder, LayoutFileName);
}