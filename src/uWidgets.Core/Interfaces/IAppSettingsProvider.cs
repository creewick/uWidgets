using uWidgets.Core.Models.Settings;

namespace uWidgets.Core.Interfaces;

/// <summary>
/// Service for reading and writing application settings, stored in <c>appsettings.json</c>.
/// <para>See <see cref="AppSettings"/></para>
/// </summary>
public interface IAppSettingsProvider : IDataProvider<AppSettings>;
