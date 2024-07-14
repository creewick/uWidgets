namespace uWidgets.Core.Models;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class WidgetInfoAttribute(Type viewType, Type? modelType = null, Type? editModelViewType = null, string? title = null, string? subtitle = null) : Attribute
{
    public Type ViewType { get; } = viewType;
    public Type? ModelType { get; } = modelType;
    public Type? EditModelViewType { get; } = editModelViewType;
    public string? Title { get; } = title;
    public string? Subtitle { get; } = subtitle;
}