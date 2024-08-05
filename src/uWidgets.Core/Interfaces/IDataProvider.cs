namespace uWidgets.Core.Interfaces;

/// <summary>
/// Service for reading and writing data.
/// </summary>
/// <typeparam name="T">Type of data.</typeparam>
public interface IDataProvider<T>
{
    /// <summary>
    /// Get the data.
    /// </summary>
    /// <returns>Data.</returns>
    public T Get();
    
    /// <summary>
    /// Save the data.
    /// </summary>
    /// <param name="data">New data.</param>
    public void Save(T data);
    
    /// <summary>
    /// Event that is raised before the data is changed.
    /// </summary>
    public event DataChangedEvent<T> DataChanging; 
    
    /// <summary>
    /// Event that is raised after the data is changed.
    /// </summary>
    public event DataChangedEvent<T> DataChanged;
}

/// <summary>
/// Event handler for data changing.
/// </summary>
/// <typeparam name="T">Type of data.</typeparam>
public delegate void DataChangedEvent<in T>(object sender, T? oldData, T newData);
