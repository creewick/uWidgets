using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Calendar.Models;
using Calendar.ViewModels;

namespace Calendar.Views;

public partial class Month : UserControl
{
    public Month() : this(new MonthCalendarModel(DayOfWeek.Monday)) {}
    
    public Month(MonthCalendarModel monthCalendarModel)
    {
        DataContext = new MonthCalendarViewModel(monthCalendarModel);
        Unloaded += OnUnloaded;
        SizeChanged += OnSizeChanged;
        InitializeComponent();
    }

    private void OnUnloaded(object? sender, RoutedEventArgs e)
    {
        SizeChanged -= OnSizeChanged;
        ((MonthCalendarViewModel)DataContext!).Dispose();
    }

    public static readonly StyledProperty<double> TextSizeProperty = 
        AvaloniaProperty.Register<Month, double>(nameof(TextSize), 12);

    public double TextSize
    {
        get => GetValue(TextSizeProperty);
        set => SetValue(TextSizeProperty, value);
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        var fontSize = e.NewSize.Height / 12;

        TextSize = fontSize;
    }
}