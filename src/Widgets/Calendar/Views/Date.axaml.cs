<<<<<<< HEAD
using Avalonia.Controls;
using Calendar.ViewModels;

namespace Calendar.Views;

public partial class Date : UserControl
{
    public Date()
    {
        DataContext = new DateCalendarViewModel();
        Unloaded += (_, _) => ((DateCalendarViewModel)DataContext).Dispose();
        InitializeComponent();
    }
=======
using Avalonia.Controls;
using Calendar.ViewModels;

namespace Calendar.Views;

public partial class Date : UserControl
{
    public Date()
    {
        DataContext = new DateCalendarViewModel();
        Unloaded += (_, _) => ((DateCalendarViewModel)DataContext).Dispose();
        InitializeComponent();
    }
>>>>>>> parent of 15524c5 (Delete src directory)
}