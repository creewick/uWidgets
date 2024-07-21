using Avalonia.Controls;
using Clock.Models;
using Clock.ViewModels;

namespace Clock.Views;

public partial class Digital : UserControl
{
    public Digital() : this(new ClockModel()) {}
    
    public Digital(ClockModel clockModel)
    {
        DataContext = new DigitalClockViewModel(clockModel);
        Unloaded += (_, _) => ((DigitalClockViewModel)DataContext).Dispose();
        InitializeComponent();
    }
}