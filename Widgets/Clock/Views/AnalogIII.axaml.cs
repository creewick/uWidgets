using Avalonia.Controls;
using Clock.Models;
using Clock.ViewModels;

namespace Clock.Views;

public partial class AnalogIII : UserControl
{    
    public AnalogIII() : this(new ClockModel()) {}
    
    public AnalogIII(ClockModel clockModel)
    {
        DataContext = new AnalogClockViewModel(clockModel);
        Unloaded += (_, _) => ((AnalogClockViewModel)DataContext).Dispose();
        InitializeComponent();
    }
}