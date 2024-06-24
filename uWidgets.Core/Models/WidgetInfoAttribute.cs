namespace uWidgets.Core.Models;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class WidgetInfoAttribute(Type controlType, Type? settingsType) : Attribute
{
    public Type ControlType { get; } = controlType;
    public Type? SettingsType { get; } = settingsType;
}