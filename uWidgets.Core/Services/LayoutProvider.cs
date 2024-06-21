using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;

namespace uWidgets.Core.Services;

public class LayoutProvider() : JsonParser<List<WidgetModel>>(Const.LayoutFile), ILayoutProvider;