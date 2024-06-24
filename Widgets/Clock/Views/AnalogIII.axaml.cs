using Avalonia.Controls;
using Clock.SettingsModels;
using Clock.ViewModels;

namespace Clock.Views;

public partial class AnalogIII : UserControl
{    
    public AnalogIII() : this(new ClockSettings()) {}
    
    public AnalogIII(ClockSettings clockSettings)
    {
        DataContext = new AnalogClockViewModel(clockSettings);
        InitializeComponent();
    }
}