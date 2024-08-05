namespace uWidgets.Core.Interfaces;

/// <summary>
/// Service for setting the application's locale.
/// </summary>
public interface ILocaleService
{
    /// <summary>
    /// Set the application's locale.
    /// </summary>
    /// <param name="cultureName">Name of the culture to set.</param>
    public void SetCulture(string cultureName);
}