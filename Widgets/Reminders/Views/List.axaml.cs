using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Reminders.Locales;
using Reminders.Models;
using Reminders.ViewModels;
using uWidgets.Core.Interfaces;
using uWidgets.Services;

namespace Reminders.Views;

public partial class List : UserControl
{
    private RemindersListModel model;
    private readonly IWidgetLayoutProvider widgetLayoutProvider;

    public List(IWidgetLayoutProvider widgetLayoutProvider) 
        : this(new RemindersListModel(Locale.Reminders_List_Title, []), widgetLayoutProvider) {}
    
    public List(RemindersListModel model, IWidgetLayoutProvider widgetLayoutProvider)
    {
        this.model = model;
        this.widgetLayoutProvider = widgetLayoutProvider;
        DataContext = new RemindersViewModel(model);
        SizeChanged += OnSizeChanged;
        Unloaded += OnUnloaded;
        InitializeComponent();
    }

    private void OnUnloaded(object? sender, RoutedEventArgs e)
    {
        SizeChanged -= OnSizeChanged;
        Unloaded -= OnUnloaded;
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        const int smallSize = 160;
        var small = e.NewSize.Width < smallSize || e.NewSize.Height < smallSize;
        var wide = e.NewSize.AspectRatio >= 1.5;

        if (small && !wide)
        {
            Margin = new Thickness(12, 8, 6, 4);
            Grid.ColumnDefinitions = new ColumnDefinitions("*, Auto");
            Grid.RowDefinitions = new RowDefinitions("Auto, *, Auto");
            Grid.SetPosition(ListName, 0, 0);
            ListName.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetPosition(Count, 1, 0);
            Divider.IsVisible = false;
            Count.HorizontalAlignment = HorizontalAlignment.Right;
            Count.FontSize = 20;
            Grid.SetPosition(Entries, 0, 1, 2);
            Grid.SetPosition(Input, 0, 2, 2);
        } else if (wide)
        {
            Margin = new Thickness(12, 4, 6, 4);
            Grid.ColumnDefinitions = new ColumnDefinitions("Auto, *");
            Grid.RowDefinitions = new RowDefinitions("*, Auto, Auto");
            Grid.SetPosition(ListName, 0, 2);
            ListName.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetPosition(Count, 0, 1);
            Divider.IsVisible = false;
            Count.HorizontalAlignment = HorizontalAlignment.Left;
            Count.FontSize = 32;
            Grid.SetPosition(Entries, 1, 0, 1, 2);
            Grid.SetPosition(Input, 1, 2);
        }
        else
        {
            Margin = new Thickness(12, 8, 6, 4);
            Grid.ColumnDefinitions = new ColumnDefinitions("*, Auto");
            Grid.RowDefinitions = new RowDefinitions("Auto, Auto, Auto, *, Auto");
            Grid.SetPosition(ListName, 0, 1);
            ListName.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetPosition(Count, 0, 0);
            Count.HorizontalAlignment = HorizontalAlignment.Left;
            Count.FontSize = 32;
            Divider.IsVisible = true;
            Grid.SetPosition(Entries, 0, 3, 2);
            Grid.SetPosition(Input, 0, 4, 2);
        }

        // var wide = e.NewSize.AspectRatio >= 1.5;
        //
        // Grid.ColumnDefinitions = new ColumnDefinitions(wide ? "*,*,*,*" : "*,*");
        // Grid.RowDefinitions = new RowDefinitions(wide ? "*" : "*,*");
        // Grid.SetColumn(Third, wide ? 2 : 0);
        // Grid.SetRow(Third, wide ? 0 : 1);
        // Grid.SetColumn(Fourth, wide ? 3 : 1);
        // Grid.SetRow(Fourth, wide ? 0 : 1);
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
}