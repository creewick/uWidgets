using System;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;

namespace uWidgets.Services;

public class ThemeService : IThemeService
{
    private readonly StyleInclude transparentStyle = LoadStyles("avares://uWidgets/Styles/Transparent.axaml");
    
    private static StyleInclude LoadStyles(string path)
    {
        return new StyleInclude(new Uri(path)) { Source = new Uri(path) };
    }
    
    public void Apply(Theme theme)
    {
        Application.Current!.RequestedThemeVariant = theme.IsDark switch
        {
            null => ThemeVariant.Default,
            false when !theme.UseTransparency => ThemeVariant.Light,
            _ => ThemeVariant.Dark,
        };

        if (theme.UseTransparency)
            Application.Current.Styles.Add(transparentStyle);
        else
            Application.Current.Styles.Remove(transparentStyle);
    }
}