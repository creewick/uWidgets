using Microsoft.Win32;

namespace uWidgets.Core.Services;

public static class StartupService
{
    private const string RegistryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
    private static readonly string ExePath = Environment.ProcessPath!;

    public static bool Set(bool value)
    {
        return value 
            ? RunOnStartup() 
            : RemoveFromStartup();
    }

    private static bool RunOnStartup(bool repeat = true)
    {
        if (!OperatingSystem.IsWindows()) return false;
        
        try
        {
            var rk = Registry.CurrentUser.OpenSubKey(RegistryKey, true);
            rk?.SetValue(Const.AppName, ExePath);
            return true;
        }
        catch (Exception)
        {
            return repeat && RunOnStartup(false);
        }
    }

    private static bool RemoveFromStartup(bool repeat = true)
    {
        if (!OperatingSystem.IsWindows()) return false;
        
        try
        {
            var rk = Registry.CurrentUser.OpenSubKey(RegistryKey, true);
            rk?.DeleteValue(Const.AppName);
            return true;
        }
        catch (Exception)
        {
            return repeat && RemoveFromStartup(false);
        }
    }
}