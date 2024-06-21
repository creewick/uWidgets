namespace uWidgets.Core.Interfaces;

public interface IDataProvider<T>
{
    public Task<T> Get();

    public Task Save(T data);
}