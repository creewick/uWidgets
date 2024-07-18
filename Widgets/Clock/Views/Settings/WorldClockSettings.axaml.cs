using Avalonia.Controls;
using Clock.ViewModels;
using uWidgets.Core.Interfaces;

namespace Clock.Views.Settings;

public partial class WorldClockSettings : UserControl
{
    public WorldClockSettings(IWidgetLayoutProvider widgetLayoutProvider)
    {
        DataContext = new WorldClockSettingsViewModel(widgetLayoutProvider);
        InitializeComponent();
    }
}