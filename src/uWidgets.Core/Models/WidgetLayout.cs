using System.Text.Json;

namespace uWidgets.Core.Models;

/// <summary>
/// Layout of a single widget, stored in <c>layout.json</c>.
/// </summary>
/// <param name="Type">Assembly name of the widget.</param>
/// <param name="SubType">UserControl name of the widget.</param>
/// <param name="X">X coordinate of the widget's top-left corner.</param>
/// <param name="Y">Y coordinate of the widget's top-left corner.</param>
/// <param name="Width">Width of the widget.</param>
/// <param name="Height">Height of the widget.</param>
/// <param name="Settings">Widget's model as <see cref="JsonElement"/></param>
public record WidgetLayout(string Type, string SubType, int X, int Y, int Width, int Height, JsonElement? Settings)
{
    /// <summary>
    /// Get the widget's model.
    /// </summary>
    /// <typeparam name="T">Type of the widget's model.</typeparam>
    /// <returns>Widget's model.</returns>
    public T? GetModel<T>() => (T?) GetModel(typeof(T));
    
    /// <summary>
    /// Get the widget's model.
    /// </summary>
    /// <param name="type">Type of the widget's model.</param>
    /// <returns>Widget's model.</returns>
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