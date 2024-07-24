using System.Linq;
using ReactiveUI;
using uWidgets.Core.Interfaces;
using uWidgets.Locales;

namespace uWidgets.ViewModels;

public class AppearanceViewModel(IAppSettingsProvider appSettingsProvider) : ReactiveObject
{
    public ThemeViewModel[] Themes =>
    [
        new ThemeViewModel(Locale.Settings_Appearance_DarkMode_False, false),
        new ThemeViewModel(Locale.Settings_Appearance_DarkMode_True, true),
        new ThemeViewModel(Locale.Settings_Appearance_DarkMode_Null, null)
    ];

    public ThemeViewModel? DarkMode
    {
        get => Themes.FirstOrDefault(theme => theme.Value == appSettingsProvider.Get().Theme.DarkMode);
        set
        {
            var settings = appSettingsProvider.Get();
            var newTheme = settings.Theme with { DarkMode = value?.Value };
            var newSettings = settings with { Theme = newTheme };
            appSettingsProvider.Save(newSettings);
        }
    }

    public bool Transparency
    {
        get => appSettingsProvider.Get().Theme.Transparency;
        set
        {
            var settings = appSettingsProvider.Get();
            var newTheme = settings.Theme with { Transparency = value };
            var newSettings = settings with { Theme = newTheme };
            appSettingsProvider.Save(newSettings);
        }
    }
    
    public bool Monochrome
    {
        get => appSettingsProvider.Get().Theme.Monochrome;
        set
        {
            var settings = appSettingsProvider.Get();
            var newTheme = settings.Theme with { Monochrome = value };
            var newSettings = settings with { Theme = newTheme };
            appSettingsProvider.Save(newSettings);
        }
    }
    
    public bool SnapPosition
    {
        get => appSettingsProvider.Get().Layout.SnapPosition;
        set
        {
            var settings = appSettingsProvider.Get();
            var newLayout = settings.Layout with { SnapPosition = value };
            var newSettings = settings with { Layout = newLayout };
            appSettingsProvider.Save(newSettings);
        }
    }

    public int WidgetSize
    {
        get => appSettingsProvider.Get().Layout.WidgetSize;
        set
        {
            var settings = appSettingsProvider.Get();
            var newLayout = settings.Layout with { WidgetSize = value };
            var newSettings = settings with { Layout = newLayout };
            appSettingsProvider.Save(newSettings);
        }
    }
    
    public int WidgetMargin
    {
        get => appSettingsProvider.Get().Layout.WidgetMargin;
        set
        {
            var settings = appSettingsProvider.Get();
            var newLayout = settings.Layout with { WidgetMargin = value };
            var newSettings = settings with { Layout = newLayout };
            appSettingsProvider.Save(newSettings);
        }
    }
}