using uWidgets.Core.Models.Settings;

namespace uWidgets.Core.Interfaces;

/// <summary>
/// Service for setting the application's theme.
/// <para>See <see cref="Theme"/></para>
/// </summary>
public interface IThemeService
{
    /// <summary>
    /// Apply the specified theme.
    /// </summary>
    /// <param name="theme">Theme to apply</param>
    public void Apply(Theme theme);
}