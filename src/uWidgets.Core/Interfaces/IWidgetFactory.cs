using uWidgets.Core.Models;

namespace uWidgets.Core.Interfaces;

public interface IWidgetFactory<out TWindow, out TControl>
{
    public IEnumerable<TWindow> Create();
    public TControl CreateControl(Type type);
    public TWindow Add(WidgetLayout widgetLayout);
}