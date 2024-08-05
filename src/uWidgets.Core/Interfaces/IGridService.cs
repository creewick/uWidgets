namespace uWidgets.Core.Interfaces;

/// <summary>
/// Service for resizing and positioning windows in a grid.
/// </summary>
/// <typeparam name="T">Type of window. For Avalonia, use <c>Avalonia.Controls.Window</c></typeparam>
public interface IGridService<in T>
{
    /// <summary>
    /// Set the width and height of the window in grid units.
    /// </summary>
    /// <example>
    /// <c>SetSize(this, 2, 2);</c> - small size<br/>
    /// <c>SetSize(this, 4, 2);</c> - medium size<br/>
    /// <c>SetSize(this, 4, 4);</c> - large size<br/>
    /// <c>SetSize(this, 8, 4);</c> - extra large size
    /// </example>
    /// <param name="window">Window to resize</param>
    /// <param name="columns">Columns span</param>
    /// <param name="rows">Rows span</param>
    public void SetSize(T window, int columns, int rows);
    
    /// <summary>
    /// Set the width and height of the window to the nearest whole number of grid units.
    /// </summary>
    /// <param name="window">Window to resize</param>
    public void SnapSize(T window);
    
    /// <summary>
    /// Set the position of the window to the nearest whole number of grid units.
    /// </summary>
    /// <param name="window">Window to reposition</param>
    public void SnapPosition(T window);
}
