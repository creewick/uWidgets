<<<<<<< HEAD
﻿using uWidgets.Core.Interfaces;
using uWidgets.Core.Models.Settings;

namespace uWidgets.Core.Services;

/// <inheritdoc cref="IAppSettingsProvider"/>
=======
﻿using uWidgets.Core.Interfaces;
using uWidgets.Core.Models.Settings;

namespace uWidgets.Core.Services;

/// <inheritdoc cref="IAppSettingsProvider"/>
>>>>>>> parent of 15524c5 (Delete src directory)
public class AppSettingsProvider() : JsonParser<AppSettings>(Const.AppSettingsFile), IAppSettingsProvider;