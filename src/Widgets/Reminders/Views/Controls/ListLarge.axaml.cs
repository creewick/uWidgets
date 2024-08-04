using Avalonia.Controls;
using Avalonia.Interactivity;
using Reminders.ViewModels;

namespace Reminders.Views.Controls;

public partial class ListLarge : UserControl
{
    public ListLarge(RemindersViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }

    public void ListNameChanged(object? sender, RoutedEventArgs e) => (Parent as List)!.ListNameChanged(sender, e);
    public void CompleteReminder(object? sender, RoutedEventArgs e) => (Parent as List)!.CompleteReminder(sender, e);
    public void EditReminder(object? sender, RoutedEventArgs e) => (Parent as List)!.EditReminder(sender, e);
    public void CreateReminder(object? sender, RoutedEventArgs e) => (Parent as List)!.CreateReminder(sender, e);

}