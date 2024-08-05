using System.Text.Json;
using uWidgets.Core.Interfaces;

namespace uWidgets.Core.Services;

/// <inheritdoc />
public class JsonParser<T>(string filePath) : IDataProvider<T>
{
    private T? data;
    /// <inheritdoc />
    public event DataChangedEvent<T>? DataChanging;
    /// <inheritdoc />
    public event DataChangedEvent<T>? DataChanged;

    /// <inheritdoc />
    public T Get()
    {
        if (data != null) return data;

        var json = File.ReadAllText(filePath);

        return data = JsonSerializer.Deserialize<T>(json)
               ?? throw new FormatException($"Can't deserialize {typeof(T).Name}");
    }

    /// <inheritdoc />
    public void Save(T newData)
    {
        var oldData = data;
        DataChanging?.Invoke(this, data, newData);
        var json = JsonSerializer.Serialize(data = newData);
        
        File.WriteAllText(filePath, json);
        DataChanged?.Invoke(this, oldData, newData);
    }
}