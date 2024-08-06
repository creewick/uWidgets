namespace uWidgets.Core.Interfaces;

/// <summary>
/// Service for setting the application to run on startup.
/// </summary>
public interface IStartupService
{
    /// <summary>
    /// Set whether the application should run on startup.
    /// </summary>
    /// <param name="value">
    /// <c>true</c> to run on startup, <c>false</c> to not run on startup.
    /// </param>
    /// <returns>
    /// <c>true</c> if the operation was successful, <c>false</c> otherwise.
    /// </returns>
    public bool SetRunOnStartup(bool value);
}