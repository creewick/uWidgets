using uWidgets.Core.Models;

namespace uWidgets.Core.Interfaces;

public interface IWidgetFactory<out T>
{
    public IEnumerable<T> Create();
    public T Create(WidgetSettings widgetSettings);
}