using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using uWidgets.Core.Interfaces;

namespace uWidgets.Views;

public partial class Widget : Window
{
    private readonly IWidgetSettingsProvider widgetSettings;
    private readonly IGridService gridService;
    
    public Widget(IWidgetSettingsProvider widgetSettings, IGridService gridService)
    {
        this.widgetSettings = widgetSettings;
        this.gridService = gridService;
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

    public void ResizeSmall() => Resize(2, 2);
    public void ResizeMedium() => Resize(4, 2);
    public void ResizeLarge() => Resize(4, 4);
    public void ResizeExtraLarge() => Resize(8, 4);

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        BeginMoveDrag(e);
    }
    
    private void Resize(int columns, int rows)
    {
        var newSize = gridService.GetSize(columns, rows);
        Width = newSize.Width;
        Height = newSize.Height;
    }
}