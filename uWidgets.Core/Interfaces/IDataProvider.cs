namespace uWidgets.Core.Interfaces;

public interface IDataProvider<T>
{
    public T Get();

    public void Save(T data);
    
    public event EventHandler<T> DataChanged; 
}