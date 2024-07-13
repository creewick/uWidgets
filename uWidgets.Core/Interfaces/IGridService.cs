using System.Drawing;

namespace uWidgets.Core.Interfaces;

public interface IGridService<in T>
{
    public void SetSize(T window, int columns, int rows);
    public void SnapSize(T window);
    public void SnapPosition(T window);
}