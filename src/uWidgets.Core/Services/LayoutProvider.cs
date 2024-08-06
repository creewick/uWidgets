using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;

namespace uWidgets.Core.Services;

/// <inheritdoc cref="ILayoutProvider" />
public class LayoutProvider() : JsonParser<List<WidgetLayout>>(Const.LayoutFile), ILayoutProvider;