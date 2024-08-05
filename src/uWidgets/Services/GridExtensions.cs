using Avalonia.Controls;

namespace uWidgets.Services;

public static class GridExtensions
{
    public static void SetPosition(this Grid grid, Control element, int column = 0, int row = 0, int columnSpan = 1,
        int rowSpan = 1)
    {
        Grid.SetColumn(element, column);
        Grid.SetRow(element, row);
        Grid.SetColumnSpan(element, columnSpan);
        Grid.SetRowSpan(element, rowSpan);
    }
}