<<<<<<< HEAD
﻿using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;

namespace uWidgets.Core.Services;

/// <inheritdoc cref="ILayoutProvider" />
=======
﻿using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;

namespace uWidgets.Core.Services;

/// <inheritdoc cref="ILayoutProvider" />
>>>>>>> parent of 15524c5 (Delete src directory)
public class LayoutProvider() : JsonParser<List<WidgetLayout>>(Const.LayoutFile), ILayoutProvider;