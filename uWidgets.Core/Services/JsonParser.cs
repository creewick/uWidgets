using System.Text.Json;
using uWidgets.Core.Interfaces;

namespace uWidgets.Core.Services;

public class JsonParser<T>(string filePath) : IDataProvider<T>
{
    public event EventHandler<T>? DataChanging;
    public event EventHandler<T>? DataChanged;
    private T? data;

    public T Get()
    {
        if (data != null) return data;

        var json = File.ReadAllText(filePath);

        return data = JsonSerializer.Deserialize<T>(json)
               ?? throw new FormatException($"Can't deserialize {typeof(T).Name}");
    }

    public void Save(T newData)
    {
        DataChanging?.Invoke(this, newData);
        var json = JsonSerializer.Serialize(data = newData);
        
        File.WriteAllText(filePath, json);
        DataChanged?.Invoke(this, newData);
    }
}