using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using uWidgets.Core.Interfaces;

namespace uWidgets.Views;

public partial class Widget : Window
{
    public Widget(IWidgetSettingsProvider widgetSettings)
    {
        Position = new PixelPoint(
            widgetSettings.Get().X,
            widgetSettings.Get().Y
        );
        Height = widgetSettings.Get().Height;
        Width = widgetSettings.Get().Width;
        
        InitializeComponent();
        RenderOptions.SetTextRenderingMode(this, TextRenderingMode.Antialias);
        PointerPressed += OnPointerPressed;
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        BeginMoveDrag(e);
    }
}