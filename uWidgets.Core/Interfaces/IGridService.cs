using System.Drawing;

namespace uWidgets.Core.Interfaces;

public interface IGridService
{
    public Size GetSize(int columns, int rows);
    public Size SnapSize(Size size);
    public Point SnapPosition(Point position);
}