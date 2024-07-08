using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;

namespace uWidgets.Services;

public class ThemeService : IThemeService
{
    private readonly Style transparentStyle = new()
    {
        Resources = new ResourceDictionary
        {
            { "SystemControlBackgroundAltHighBrush", new SolidColorBrush(Color.Parse("#30000000")) },
            { "TextOpacity", 0.8 }
        }
    };
    
    public void Apply(Theme theme)
    {
        Application.Current!.RequestedThemeVariant = theme.DarkMode switch
        {
            null => ThemeVariant.Default,
            false when !theme.Transparency => ThemeVariant.Light,
            _ => ThemeVariant.Dark,
        };

        if (theme.Transparency)
            Application.Current.Styles.Add(transparentStyle);
        else
            Application.Current.Styles.Remove(transparentStyle);
    }
}