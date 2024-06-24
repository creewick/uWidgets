using Avalonia.Controls;
using Clock.SettingsModels;
using Clock.ViewModels;

namespace Clock.Views;

public partial class AnalogI : UserControl
{
    public AnalogI() : this(new ClockSettings()) {}
    
    public AnalogI(ClockSettings clockSettings)
    {
        DataContext = new AnalogClockViewModel(clockSettings);
        InitializeComponent();
    }
}