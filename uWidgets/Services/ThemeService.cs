using System;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;
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
        Source = new Uri("avares://uWidgets/Transparent.axaml")
    };
    
    public void Apply(Theme theme)
    {
        Application.Current!.RequestedThemeVariant = theme.DarkMode switch
        {
            null => ThemeVariant.Default,
            false => ThemeVariant.Light,
            _ => ThemeVariant.Dark,
        };

        if (theme.Transparency && !Application.Current.Styles.Contains(transparentStyle))
            Application.Current.Styles.Add(transparentStyle);
        if (!theme.Transparency && Application.Current.Styles.Contains(transparentStyle))
            Application.Current.Styles.Remove(transparentStyle);
    }
}