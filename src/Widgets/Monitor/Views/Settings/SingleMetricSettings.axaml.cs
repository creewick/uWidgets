<<<<<<< HEAD
using Avalonia.Controls;
using Monitor.ViewModels;
using uWidgets.Core.Interfaces;

namespace Monitor.Views.Settings;

public partial class SingleMetricSettings : UserControl
{
    public SingleMetricSettings(IWidgetLayoutProvider widgetLayoutProvider)
    {
        DataContext = new SingleMetricSettingsViewModel(widgetLayoutProvider);
        InitializeComponent();
    }
=======
using Avalonia.Controls;
using Monitor.ViewModels;
using uWidgets.Core.Interfaces;

namespace Monitor.Views.Settings;

public partial class SingleMetricSettings : UserControl
{
    public SingleMetricSettings(IWidgetLayoutProvider widgetLayoutProvider)
    {
        DataContext = new SingleMetricSettingsViewModel(widgetLayoutProvider);
        InitializeComponent();
    }
>>>>>>> parent of 15524c5 (Delete src directory)
}