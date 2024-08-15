<<<<<<< HEAD
﻿using uWidgets.Core.Models;
using uWidgets.Core.Models.Settings;

namespace uWidgets.Core.Interfaces;

/// <summary>
/// Service for reading and writing layout settings, stored in <c>layout.json</c>.
/// <para>See <see cref="Layout"/></para>
/// </summary>
=======
﻿using uWidgets.Core.Models;
using uWidgets.Core.Models.Settings;

namespace uWidgets.Core.Interfaces;

/// <summary>
/// Service for reading and writing layout settings, stored in <c>layout.json</c>.
/// <para>See <see cref="Layout"/></para>
/// </summary>
>>>>>>> parent of 15524c5 (Delete src directory)
public interface ILayoutProvider : IDataProvider<List<WidgetLayout>>;