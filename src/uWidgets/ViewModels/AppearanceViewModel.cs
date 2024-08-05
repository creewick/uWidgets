using System.Linq;
using Avalonia.Media;
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

    public AccentColorViewModel[] AccentComboboxItems =>
    [
        new AccentColorViewModel(Locale.Settings_Appearance_AccentColor_Null, null),
        new AccentColorViewModel(Locale.Settings_Appearance_AccentColor_Manual, "#3376CD")
    ];

    public bool ShowColorPalette => appSettingsProvider.Get().Theme.AccentColor != null;
    
    public AccentColorViewModel AccentMode
    {
        get => appSettingsProvider.Get().Theme.AccentColor == null ? AccentComboboxItems[0] : AccentComboboxItems[1];
        set
        {
            var settings = appSettingsProvider.Get();
            var newTheme = settings.Theme with { AccentColor = value.Value };
            var newSettings = settings with { Theme = newTheme };
            appSettingsProvider.Save(newSettings);
            this.RaisePropertyChanged(nameof(ShowColorPalette));
        }
    }

    public Color AccentColor
    {
        get => Color.TryParse(appSettingsProvider.Get().Theme.AccentColor, out var color) ? color : Colors.DodgerBlue;
        set
        {
            var settings = appSettingsProvider.Get();
            var newTheme = settings.Theme with { AccentColor = value.ToString() };
            var newSettings = settings with { Theme = newTheme };
            appSettingsProvider.Save(newSettings);
        }
    }

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
    
    public bool SnapSize 
    {
        get => appSettingsProvider.Get().Layout.SnapSize;
        set
        {
            var settings = appSettingsProvider.Get();
            var newLayout = settings.Layout with { SnapSize = value };
            var newSettings = settings with { Layout = newLayout };
            appSettingsProvider.Save(newSettings);
        }
    }
    
    public bool LockPosition
    {
        get => appSettingsProvider.Get().Layout.LockPosition;
        set
        {
            var settings = appSettingsProvider.Get();
            var newLayout = settings.Layout with { LockPosition = value };
            var newSettings = settings with { Layout = newLayout };
            appSettingsProvider.Save(newSettings);
        }
    }
    
    public bool LockSize
    {
        get => appSettingsProvider.Get().Layout.LockSize;
        set
        {
            var settings = appSettingsProvider.Get();
            var newLayout = settings.Layout with { LockSize = value };
            var newSettings = settings with { Layout = newLayout };
            appSettingsProvider.Save(newSettings);
        }
    }
}