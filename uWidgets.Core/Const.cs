namespace uWidgets.Core;

public static class Const
{
    private static string WidgetsFolderName => "Widgets";
    private static string AppSettingsFileName => "appSettings.json";
    private static string LayoutFileName => "layout.json";
    
    public static string CurrentFolder = Directory.GetCurrentDirectory();    
    public static string WidgetsFolder = Path.Combine(CurrentFolder, WidgetsFolderName);
    public static string AppSettingsFile = Path.Combine(CurrentFolder, AppSettingsFileName);
    public static string LayoutFile = Path.Combine(CurrentFolder, LayoutFileName);
    public static float DisplayScaling = Interop.GetDpiForSystem() / 96.0f;
    public static float CornerRadius = 48 / DisplayScaling;
}