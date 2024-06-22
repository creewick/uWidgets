namespace uWidgets.Core.Interfaces;

public interface IWidgetFactory<out T>
{
    public IEnumerable<T> Create();
}