using System;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;

namespace uWidgets.Services;

public class ThemeService : IThemeService
{
    public ThemeService(IAppSettingsProvider appSettingsProvider)
    {
        appSettingsProvider.DataChanging += (_, _, newSettings) => 
            Apply(newSettings.Theme);
    }
    
    private readonly StyleInclude transparentStyle = new(new Uri("avares://uWidgets/"))
    {
        Source = new Uri("avares://uWidgets/Styles/Transparent.axaml")
    };
    
    private readonly StyleInclude monochromeStyle = new(new Uri("avares://uWidgets/"))
    {
        Source = new Uri("avares://uWidgets/Styles/Monochrome.axaml")
    };
    
    public void Apply(Theme theme)
    {
        Application.Current!.RequestedThemeVariant = theme.DarkMode switch
        {
            null => ThemeVariant.Default,
            false => ThemeVariant.Light,
            _ => ThemeVariant.Dark,
        };

        if (theme.AccentColor != null && Color.TryParse(theme.AccentColor, out var color))
        {
            Application.Current.Resources["SystemAccentColor"] = color;
            Application.Current.Resources["SystemAccentColorDark1"] = color;
            Application.Current.Resources["SystemAccentColorLight1"] = color;
        }
        
        SwitchStyle(transparentStyle, theme.Transparency);
        SwitchStyle(monochromeStyle, theme.Monochrome);
    }

    private static void SwitchStyle(StyleInclude style, bool enable)
    {
        if (enable && !Application.Current!.Styles.Contains(style))
            Application.Current.Styles.Add(style);
        if (!enable && Application.Current!.Styles.Contains(style))
            Application.Current.Styles.Remove(style);
    }
}