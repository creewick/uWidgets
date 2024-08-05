using Microsoft.Win32;
using uWidgets.Core.Interfaces;

namespace uWidgets.Core.Services;

/// <inheritdoc />
public class StartupService : IStartupService
{
    private const string RegistryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
    private static readonly string ExePath = Environment.ProcessPath!;

    /// <inheritdoc />
    public bool SetRunOnStartup(bool value) => SetRunOnStartupInternal(value);

    private static bool SetRunOnStartupInternal(bool value, bool repeat = true)
    {
        if (!OperatingSystem.IsWindows()) return false;
        
        try
        {
            var rk = Registry.CurrentUser.OpenSubKey(RegistryKey, true);
            if (value)
                rk?.SetValue(Const.AppName, ExePath);
            else
                rk?.DeleteValue(Const.AppName);
            return true;
        }
        catch (Exception)
        {
            return repeat && SetRunOnStartupInternal(value, false);
        }
    }
}