using System.Text.Json;

namespace uWidgets.Core.Models;

public record WidgetSettings(string Type, string SubType, int X, int Y, int Width, int Height, JsonElement? Settings)
{
    public T? GetModel<T>() 
    {
        if (!Settings.HasValue) return default;

        try
        {
            return Settings.Value.Deserialize<T>() ?? default;
        }
        catch (Exception)
        {
            return default;
        }
    }
}