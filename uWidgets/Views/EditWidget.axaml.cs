using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using uWidgets.Core;
using uWidgets.Core.Interfaces;

namespace uWidgets.Views;

public partial class EditWidget : Window
{
    private readonly IWidgetLayoutProvider widgetLayoutProvider;
    public CornerRadius Radius => new(Const.CornerRadius / (Screens.ScreenFromWindow(this)?.Scaling ?? 1.0));
    public string WidgetType => widgetLayoutProvider.Get().Type;
    public string WidgetSubType => widgetLayoutProvider.Get().SubType;
    
    public EditWidget(IWidgetLayoutProvider widgetLayoutProvider, UserControl control)
    {
        this.widgetLayoutProvider = widgetLayoutProvider;
        DataContext = this;
        Content = control;
        InitializeComponent();
    }

    private void Close(object? sender, RoutedEventArgs e) => Close();
}