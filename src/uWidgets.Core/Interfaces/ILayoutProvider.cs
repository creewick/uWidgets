using uWidgets.Core.Models;

namespace uWidgets.Core.Interfaces;

/// <summary>
/// Service for reading and writing layout settings, stored in <c>layout.json</c>.
/// <para>See <see cref="Layout"/></para>
/// </summary>
public interface ILayoutProvider : IDataProvider<List<WidgetLayout>>;