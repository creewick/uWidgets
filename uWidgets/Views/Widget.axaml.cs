using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using uWidgets.Core;
using uWidgets.Core.Interfaces;
using uWidgets.Locales;

namespace uWidgets.Views;

public partial class Widget : Window
{
    private readonly IWidgetSettingsProvider widgetSettings;
    private readonly IAppSettingsProvider appSettingsProvider;
    private readonly IGridService<Widget> gridService;
    private readonly Settings settingsWindow;

    public string Edit => $"{Locale.Widget_Edit} \"{widgetSettings.Get().Type}\"";
    public CornerRadius Radius => new(Const.CornerRadius / (Screens.ScreenFromWindow(this)?.Scaling ?? 1.0));
    
    public Widget(IWidgetSettingsProvider widgetSettings, IAppSettingsProvider appSettingsProvider, 
        IGridService<Widget> gridService, Settings settingsWindow)
    {
        this.widgetSettings = widgetSettings;
        this.appSettingsProvider = appSettingsProvider;
        this.gridService = gridService;
        this.settingsWindow = settingsWindow;
        Height = widgetSettings.Get().Height;
        Width = widgetSettings.Get().Width;
        DataContext = this;
        
        InitializeComponent();
        Position = new PixelPoint(
            widgetSettings.Get().X,
            widgetSettings.Get().Y
        );
        RenderOptions.SetTextRenderingMode(this, TextRenderingMode.Antialias);
        PointerPressed += OnPointerPressed;
    }
    
    public void ResizeSmall() => Resize(2, 2);
    public void ResizeMedium() => Resize(4, 2);
    public void ResizeLarge() => Resize(4, 4);
    public void ResizeExtraLarge() => Resize(8, 4);
    
    public void OpenSettings()
    {
        settingsWindow.Hide();
        settingsWindow.Show();
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        BeginMoveDrag(e);
        
        var appSettings = appSettingsProvider.Get();

        if (appSettings.Layout.SnapPosition) 
            gridService.SnapPosition(this);
        
        var settings = widgetSettings.Get();
        widgetSettings.Save(settings with { X = Position.X, Y = Position.Y });
    }
    
    private void Resize(int columns, int rows)
    {
        gridService.SetSize(this, columns, rows);
        
        var settings = widgetSettings.Get();
        widgetSettings.Save(settings with { Width = (int)Width, Height = (int)Height });
    }

    public void Remove()
    {
        widgetSettings.Remove();
        Close();
    }
}