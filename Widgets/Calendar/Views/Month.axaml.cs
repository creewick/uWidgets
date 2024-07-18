using Avalonia.Controls;
using Calendar.Models;
using Calendar.ViewModels;

namespace Calendar.Views;

public partial class Month : UserControl
{
    public Month(MonthCalendarModel monthCalendarModel)
    {
        DataContext = new MonthCalendarViewModel(monthCalendarModel);
        InitializeComponent();
    }
}