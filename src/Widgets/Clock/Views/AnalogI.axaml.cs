using Avalonia.Controls;
using Clock.Models;
using Clock.ViewModels;

namespace Clock.Views;

public partial class AnalogI : UserControl
{
    public AnalogI() : this(new ClockModel()) {}
    
    public AnalogI(ClockModel clockModel)
    {
        DataContext = new AnalogClockViewModel(clockModel);
        Unloaded += (_, _) => ((AnalogClockViewModel)DataContext).Dispose();
        InitializeComponent();
    }
}