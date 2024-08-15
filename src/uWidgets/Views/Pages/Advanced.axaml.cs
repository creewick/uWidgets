<<<<<<< HEAD
using Avalonia.Controls;
using uWidgets.Core.Interfaces;
using uWidgets.ViewModels;

namespace uWidgets.Views.Pages;

public partial class Advanced : UserControl
{
    public Advanced(IAppSettingsProvider appSettingsProvider)
    {
        DataContext = new AdvancedViewModel(appSettingsProvider);
        InitializeComponent();
    }
=======
using Avalonia.Controls;
using uWidgets.Core.Interfaces;
using uWidgets.ViewModels;

namespace uWidgets.Views.Pages;

public partial class Advanced : UserControl
{
    public Advanced(IAppSettingsProvider appSettingsProvider)
    {
        DataContext = new AdvancedViewModel(appSettingsProvider);
        InitializeComponent();
    }
>>>>>>> parent of 15524c5 (Delete src directory)
}