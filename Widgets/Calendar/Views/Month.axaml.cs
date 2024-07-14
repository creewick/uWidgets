using Avalonia.Controls;
using Calendar.ViewModels;

namespace Calendar.Views;

public partial class Month : UserControl
{
    public Month()
    {
        DataContext = new MonthCalendarViewModel();
        InitializeComponent();
    }
}