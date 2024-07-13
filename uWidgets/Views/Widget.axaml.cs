using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using uWidgets.Core.Interfaces;
using uWidgets.Locales;

namespace uWidgets.Views;

public partial class Widget : Window
{
    private readonly IWidgetSettingsProvider widgetSettings;
    
    public Widget(IWidgetSettingsProvider widgetSettings)
    {
        this.widgetSettings = widgetSettings;
        Position = new PixelPoint(
            widgetSettings.Get().X,
            widgetSettings.Get().Y
        );
        Height = widgetSettings.Get().Height;
        Width = widgetSettings.Get().Width;
        DataContext = this;
        
        InitializeComponent();
        RenderOptions.SetTextRenderingMode(this, TextRenderingMode.Antialias);
        PointerPressed += OnPointerPressed;
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        BeginMoveDrag(e);
    }
}