using System.Drawing;
using uWidgets.Core.Interfaces;

namespace uWidgets.Core.Services;

public class GridService(IAppSettingsProvider appSettingsProvider) : IGridService
{
    public Size GetSize(int columns, int rows) => 
        new(GetSize(columns), GetSize(rows));

    public Size SnapSize(Size size) => 
        new(SnapDimension(size.Width), SnapDimension(size.Height));

    public Point SnapPosition(Point position) =>
        new(SnapDimension(position.X, true), SnapDimension(position.Y, true));
    
    private int SnapDimension(double pixels, bool addMargin = false)
    {
        var (size, margin, _, _) = appSettingsProvider.Get().Layout;
        var marginToAdd = addMargin ? margin : 0;

        return (int) Math.Round((pixels + margin) / (size + margin)) + marginToAdd;
    }

    private int GetSize(int units)
    {
        var (size, margin, _, _) = appSettingsProvider.Get().Layout;

        return units * (size + margin) - margin;
    }
}