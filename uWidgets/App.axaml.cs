using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using uWidgets.Core.Interfaces;
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
            .AddSingleton<ILayoutProvider, LayoutProvider>()
            .AddSingleton<IAssemblyProvider, AssemblyProvider>()
            .AddSingleton<IThemeService, ThemeService>()
            .AddSingleton<IWidgetFactory<Widget>, WidgetFactory>()
            .BuildServiceProvider();

        var settings = services
            .GetRequiredService<IAppSettingsProvider>()
            .Get();

        services
            .GetRequiredService<IThemeService>()
            .Apply(settings.Theme);

        var widgets = services
            .GetRequiredService<IWidgetFactory<Widget>>()
            .Create();

        foreach (var widget in widgets)
        {
            widget.Show();
        }
        
        new Settings().Show();
        
        base.OnFrameworkInitializationCompleted();
    }
}