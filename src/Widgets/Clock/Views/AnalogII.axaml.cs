using Avalonia.Controls;
using Clock.Models;
using Clock.ViewModels;

namespace Clock.Views;

public partial class AnalogII : UserControl
{
    public AnalogII() : this(new ClockModel()) {}
    
    public AnalogII(ClockModel clockModel) 
    {
        DataContext = new AnalogClockViewModel(clockModel);
        Unloaded += (_, _) => ((AnalogClockViewModel)DataContext).Dispose();
        InitializeComponent();
    }
}