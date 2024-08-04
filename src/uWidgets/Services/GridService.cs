using System;
using Avalonia;
using uWidgets.Core.Interfaces;
using uWidgets.Views;

namespace uWidgets.Services;

public class GridService(IAppSettingsProvider appSettingsProvider) : IGridService<Widget>
{
    public void SetSize(Widget window, int columns, int rows)
    {
        window.Width = GetSize(columns);
        window.Height = GetSize(rows);
    }

    public void SnapSize(Widget window)
    {
        window.Width = SnapDimension(window.Width);
        window.Height = SnapDimension(window.Height);
    }

    public void SnapPosition(Widget window)
    {
        var scaling = window.Screens.ScreenFromWindow(window)?.Scaling ?? 1.0;
        
        window.Position = new PixelPoint(
            SnapDimension(window.Position.X, scaling, true, 0),
            SnapDimension(window.Position.Y, scaling, true, 0));
    }
    
    private int SnapDimension(double pixels, double scaling = 1.0, bool addMargin = false, int minValue = 1)
    {
        var (size, margin, _) = appSettingsProvider.Get().Dimensions;
        var units = (int) Math.Max(minValue, Math.Round(pixels / (scaling * (size + margin))));

        return GetSize(units, scaling, addMargin);
    }

    private int GetSize(int units, double scaling = 1.0, bool addMargin = false)
    {
        var (size, margin, _) = appSettingsProvider.Get().Dimensions;
        
        if (addMargin)
            return (int) (scaling * units * (size + margin) + scaling * margin);

        return (int) (scaling * units * (size + margin) - scaling * margin);
    }
}