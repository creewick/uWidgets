namespace uWidgets.Core.Models.Attributes;

/// <summary>
/// <para>Attribute to mark an assembly's locale.</para>
/// Use it in your <c>AssemblyInfo</c>.
/// </summary>
/// <example><c>[assembly: Locale(typeof(Locale), "MyWidget_DisplayName")]</c></example>
/// <param name="localeType">Type of auto-generated <c>Locale.Designer.cs</c> class</param>
/// <param name="displayName">Resource key of your assembly's display name</param>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class LocaleAttribute(Type localeType, string? displayName = null) : Attribute
{
    /// <summary>
    /// Type of auto-generated <c>Locale.Designer.cs</c> class.
    /// </summary>
    public Type LocaleType { get; } = localeType;
    
    /// <summary>
    /// Resource key of your assembly's display name.
    /// </summary>
    public string? DisplayName { get; } = displayName;
}