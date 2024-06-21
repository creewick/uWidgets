using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;
using uWidgets.Core.Services;
using uWidgets.Services;

namespace uWidgets;

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
            .AddSingleton<IWidgetSettingsProvider, WidgetSettingsProvider>()
            .AddSingleton<IThemeService, ThemeService>()
            .BuildServiceProvider();
        
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new Widget();
        }
        
        services.GetRequiredService<IThemeService>().Apply(new Theme(true, false));


        base.OnFrameworkInitializationCompleted();
    }
}