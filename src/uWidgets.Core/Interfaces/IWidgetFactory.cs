using uWidgets.Core.Models;

namespace uWidgets.Core.Interfaces;

/// <summary>
/// Factory for creating widgets.
/// </summary>
/// <typeparam name="TWindow">Type of window. For Avalonia, use <c>Avalonia.Controls.Window</c></typeparam>
/// <typeparam name="TControl">Type of user control. For Avalonia, use <c>Avalonia.Controls.UserControl</c></typeparam>
public interface IWidgetFactory<out TWindow, out TControl>
{
    /// <summary>
    /// Creates widgets from current layout settings.
    /// </summary>
    /// <returns>Collection of windows.</returns>
    public IEnumerable<TWindow> Create();
    
    /// <summary>
    /// Creates a user control of the specified type.
    /// <para>May be used to show widget's content in another window.</para>
    /// </summary>
    /// <param name="type">Type of user control.</param>
    /// <returns>Activated user control.</returns>
    public TControl CreateControl(Type type);
    
    /// <summary>
    /// Creates a widget from a specified layout, and adds it to the collection.
    /// </summary>
    /// <param name="widgetLayout">Layout for the widget.</param>
    /// <returns>Activated window.</returns>
    public TWindow Add(WidgetLayout widgetLayout);
}