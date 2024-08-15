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
}