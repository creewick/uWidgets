using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using uWidgets.Core.Interfaces;

namespace uWidgets.Views;

public partial class EditWidget : Window
{
    private readonly IWidgetLayoutProvider widgetLayoutProvider;
    public UserControl Control { get; }
    public string WidgetType => widgetLayoutProvider.Get().Type;
    public string WidgetSubType => widgetLayoutProvider.Get().SubType;
    
    public EditWidget(IWidgetLayoutProvider widgetLayoutProvider, UserControl control)
    {
        this.widgetLayoutProvider = widgetLayoutProvider;
        DataContext = this;
        Control = control;
        InitializeComponent();
        Activated += OnInitialized;
    }

    private void OnInitialized(object? sender, EventArgs e)
    {
        Position = new PixelPoint(Math.Max(0, Position.X), Math.Max(0, Position.Y));
    }

    private void Close(object? sender, RoutedEventArgs e) => Close();
}