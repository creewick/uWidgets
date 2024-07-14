using Avalonia.Controls;
using Calendar.ViewModels;

namespace Calendar.Views;

public partial class Date : UserControl
{
    public Date()
    {
        DataContext = new DateCalendarViewModel();
        InitializeComponent();
    }
}