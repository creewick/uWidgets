namespace uWidgets.Core.Models;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class LocaleAttribute(Type localeType, string? displayName = null) : Attribute
{
    public Type LocaleType { get; } = localeType;
    public string? DisplayName { get; } = displayName;
}