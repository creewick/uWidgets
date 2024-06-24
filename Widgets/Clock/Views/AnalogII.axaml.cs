using Avalonia.Controls;
using Clock.SettingsModels;
using Clock.ViewModels;

namespace Clock.Views;

public partial class AnalogII : UserControl
{
    public AnalogII() : this(new ClockSettings()) {}
    
    public AnalogII(ClockSettings clockSettings) 
    {
        DataContext = new AnalogClockViewModel(clockSettings);
        InitializeComponent();
    }
}