using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using DynamicData;
using Reminders.Locales;
using Reminders.Models;
using Reminders.ViewModels;
using uWidgets.Core.Interfaces;

namespace Reminders.Views;

public partial class RemindersList : UserControl
{
    private RemindersListModel model;
    private readonly IWidgetLayoutProvider widgetLayoutProvider;

    public RemindersList(IWidgetLayoutProvider widgetLayoutProvider) 
        : this(new RemindersListModel(Locale.Reminders_List_Title, []), widgetLayoutProvider) {}
    
    public RemindersList(RemindersListModel model, IWidgetLayoutProvider widgetLayoutProvider)
    {
        this.model = model;
        this.widgetLayoutProvider = widgetLayoutProvider;
        DataContext = new RemindersViewModel(model);
        LostFocus += OnLostFocus;
        Unloaded += OnUnloaded;
        InitializeComponent();
    }
    

    private void OnUnloaded(object? sender, RoutedEventArgs e)
    {
        PointerExited -= OnLostFocus;
    }
    
    private void ListNameChanged(object? sender, RoutedEventArgs e)
    {
        var listName = (sender as TextBox)!.Text;
        UpdateModel(model with { ListName = listName });
    }

    private void UpdateModel(RemindersListModel newModel)
    {
        model = newModel;
        DataContext = new RemindersViewModel(newModel);
        var newSettings = JsonSerializer.SerializeToElement(newModel);
        var newLayout = widgetLayoutProvider.Get() with { Settings = newSettings };
        
        widgetLayoutProvider.Save(newLayout);
    }

    private void CompleteReminder(object? sender, RoutedEventArgs e)
    {
        var reminder = (sender as Button)!.DataContext as ReminderModel;
        var index = model.Reminders.IndexOf(reminder!);
        model.Reminders[index] = model.Reminders[index] with { Completed = !model.Reminders[index].Completed };
        UpdateModel(model);
    }
    
    private void EditReminder(object? sender, RoutedEventArgs e)
    {
        var text = (sender as TextBox)!.Text;
        var reminder = (sender as TextBox)!.DataContext as ReminderModel;
        var index = model.Reminders.IndexOf(reminder!);

        if (string.IsNullOrEmpty(text))
            model.Reminders.Remove(reminder!);
        else
            model.Reminders[index] = model.Reminders[index] with { Title = (sender as TextBox)!.Text ?? "" };
        
        UpdateModel(model);
    }
    
    private void CreateReminder(object? sender, RoutedEventArgs e)
    {
        var text = (sender as TextBox)!.Text;
        
        if (string.IsNullOrEmpty(text))
            return;
        
        model.Reminders.Add(new ReminderModel(false, text));

        (sender as TextBox)!.Text = "";
        UpdateModel(model);
    }
    
    private void OverlayClick(object? sender, PointerPressedEventArgs e) => Overlay.IsVisible = false;
    private void OnLostFocus(object? sender, RoutedEventArgs e) => Overlay.IsVisible = true;
}