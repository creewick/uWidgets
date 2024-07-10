using Avalonia;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Services;
using uWidgets.Services;
using uWidgets.Views;

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
            .AddSingleton<ILocaleService, LocaleService>()
            .AddSingleton<IWidgetFactory<Widget>, WidgetFactory>()
            .BuildServiceProvider();

        var appSettingsProvider = services
            .GetRequiredService<IAppSettingsProvider>();

        var themeService = services
            .GetRequiredService<IThemeService>();
        
        var localeService = services
            .GetRequiredService<ILocaleService>();
        
        localeService.SetCulture(appSettingsProvider.Get().Region.Language);
        themeService.Apply(appSettingsProvider.Get().Theme);

        var widgets = services
            .GetRequiredService<IWidgetFactory<Widget>>()
            .Create();

        foreach (var widget in widgets)
        {
            widget.Show();
        }
        
        new Settings(appSettingsProvider).Show();
        
        base.OnFrameworkInitializationCompleted();
    }
}