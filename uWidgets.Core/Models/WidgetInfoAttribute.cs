namespace uWidgets.Core.Models;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class WidgetInfoAttribute(Type viewType, Type? modelType, string? title = null, string? subtitle = null) : Attribute
{
    public Type ViewType { get; } = viewType;
    public Type? ModelType { get; } = modelType;
    public string? Title { get; } = title;
    public string? Subtitle { get; } = subtitle;
}