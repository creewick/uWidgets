using uWidgets.Core.Models;

namespace uWidgets.Core.Interfaces;

/// <summary>
/// Service for reading and writing layout settings of a single widget, stored in <c>layout.json</c>.
/// </summary>
public interface IWidgetLayoutProvider : IDataProvider<WidgetLayout>
{
    /// <summary>
    /// Remove the widget from the collection
    /// </summary>
    public void Remove();
}
