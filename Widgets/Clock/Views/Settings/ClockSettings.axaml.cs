using Avalonia.Controls;
using Clock.ViewModels;
using uWidgets.Core.Interfaces;

namespace Clock.Views.Settings;

public partial class ClockSettings : UserControl
{
    public ClockSettings(IWidgetSettingsProvider widgetSettingsProvider)
    {
        DataContext = new ClockSettingsViewModel(widgetSettingsProvider);
        InitializeComponent();
    }
}