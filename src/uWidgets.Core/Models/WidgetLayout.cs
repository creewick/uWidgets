using System.Text.Json;

namespace uWidgets.Core.Models;

public record WidgetLayout(string Type, string SubType, int X, int Y, int Width, int Height, JsonElement? Settings)
{
    public T? GetModel<T>() => (T?) GetModel(typeof(T));
    
    public object? GetModel(Type? type)
    {
        if (type == null || !Settings.HasValue) 
            return null;

        try
        {
            return Settings.Value.Deserialize(type);
        }
        catch (Exception)
        {
            return null;
        }
    }
}