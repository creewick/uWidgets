using System.Text.Json;
using System.Text.Json.Serialization;
using uWidgets.Core.Interfaces;

namespace uWidgets.Core.Services;

public class JsonParser<T> : IDataProvider<T>
{
    private readonly JsonSerializerOptions? options;
    private readonly string filePath;
    private T? data;

    public JsonParser(string filePath, JsonConverter? converter = null)
    {
        this.filePath = filePath;

        options = new JsonSerializerOptions();
        if (converter != null) options.Converters.Add(converter);
    }
    
    public async Task<T> Get()
    {
        if (data != null) return data;

        var json = await File.ReadAllTextAsync(filePath);

        return data = JsonSerializer.Deserialize<T>(json, options)
               ?? throw new FormatException(nameof(T));
    }

    public async Task Save(T newData)
    {
        var json = JsonSerializer.Serialize(data = newData, options);
        
        await File.WriteAllTextAsync(filePath, json);
    }
}