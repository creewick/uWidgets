using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Microsoft.Extensions.DependencyInjection;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;
using uWidgets.Core.Services;

namespace uWidgets.Settings;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection()
            .AddSingleton<IAppSettingsProvider, AppSettingsProvider>()
            .AddSingleton<ILayoutProvider, LayoutProvider>()
            .AddSingleton<IAssemblyProvider, AssemblyProvider>()
            .BuildServiceProvider();
        
        
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    public void Apply(Theme theme)
    {
        Current!.RequestedThemeVariant = theme.DarkMode switch
        {
            null => ThemeVariant.Default,
            false when !theme.Transparency => ThemeVariant.Light,
            _ => ThemeVariant.Dark,
        };
    }
}