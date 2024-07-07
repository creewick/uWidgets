namespace uWidgets.Core.Models;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class LocaleAttribute(Type type) : Attribute
{
    public Type Type { get; } = type;
}