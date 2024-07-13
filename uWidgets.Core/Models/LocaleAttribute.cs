namespace uWidgets.Core.Models;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class LocaleAttribute(Type localeType) : Attribute
{
    public Type LocaleType { get; } = localeType;
}