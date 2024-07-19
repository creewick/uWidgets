namespace uWidgets.Core.Interfaces;

public interface IDataProvider<T>
{
    public T Get();
    public void Save(T data);
    public event DataChangedEvent<T> DataChanging; 
    public event DataChangedEvent<T> DataChanged;
}


public delegate void DataChangedEvent<in T>(object sender, T? oldData, T newData);

