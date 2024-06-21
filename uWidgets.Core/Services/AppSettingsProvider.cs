using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;

namespace uWidgets.Core.Services;

public class AppSettingsProvider() : JsonParser<SettingsModel>(Const.AppSettingsFile), IAppSettingsProvider;