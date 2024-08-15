<<<<<<< HEAD
﻿using Avalonia.Controls;
using uWidgets.Core.Interfaces;
using uWidgets.ViewModels;

namespace uWidgets.Views.Pages;

public partial class Appearance : UserControl
{
    public Appearance(IAppSettingsProvider appSettingsProvider)
    {
        DataContext = new AppearanceViewModel(appSettingsProvider);
        InitializeComponent();
    }
=======
﻿using Avalonia.Controls;
using uWidgets.Core.Interfaces;
using uWidgets.ViewModels;

namespace uWidgets.Views.Pages;

public partial class Appearance : UserControl
{
    public Appearance(IAppSettingsProvider appSettingsProvider)
    {
        DataContext = new AppearanceViewModel(appSettingsProvider);
        InitializeComponent();
    }
>>>>>>> parent of 15524c5 (Delete src directory)
}