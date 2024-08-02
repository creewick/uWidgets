using Avalonia;
using System;
using System.IO;
using uWidgets.Core;

namespace uWidgets;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        try
        {
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }
        catch (Exception e)
        {
            var fileName = Path.Combine(Const.CurrentFolder, "crash_log.txt");
            File.WriteAllText(fileName, $"{e.Message}{Environment.NewLine}{e.StackTrace}");
            throw;
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UseWin32()
            .UsePlatformDetect()
            .WithInterFont()
            .With(new Win32PlatformOptions
            {
                CompositionMode = new [] { Win32CompositionMode.WinUIComposition },
                WinUICompositionBackdropCornerRadius = Const.CornerRadius,
            })
            .LogToTrace();
}