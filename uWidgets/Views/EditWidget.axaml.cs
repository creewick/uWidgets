using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using uWidgets.Core;
using uWidgets.Core.Interfaces;

namespace uWidgets.Views;

public partial class EditWidget : Window
{
    private readonly IWidgetSettingsProvider widgetSettingsProvider;
    public CornerRadius Radius => new(Const.CornerRadius / (Screens.ScreenFromWindow(this)?.Scaling ?? 1.0));
    public string WidgetType => widgetSettingsProvider.Get().Type;
    public string WidgetSubType => widgetSettingsProvider.Get().SubType;
    
    public EditWidget(IWidgetSettingsProvider widgetSettingsProvider)
    {
        this.widgetSettingsProvider = widgetSettingsProvider;
        DataContext = this;
        Closing += OnClosing;
        InitializeComponent();
    }
    
    private void OnClosing(object? sender, WindowClosingEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}