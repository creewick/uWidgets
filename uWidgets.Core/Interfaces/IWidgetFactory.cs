using uWidgets.Core.Models;

namespace uWidgets.Core.Interfaces;

public interface IWidgetFactory<out TWindow, out TControl>
{
    public IEnumerable<TWindow> Create();
    public TControl CreateWidgetControl(Type type, object? model);
    public TWindow CreateEditWidgetWindow(Type type, IWidgetSettingsProvider widgetSettingsProvider);
    public TWindow Create(WidgetSettings widgetSettings);
}