namespace uWidgets.Core.Models;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class WidgetInfoAttribute(Type viewType, Type? modelType) : Attribute
{
    public Type ViewType { get; } = viewType;
    public Type? ModelType { get; } = modelType;
}