using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using uWidgets.Core;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;
using uWidgets.Locales;

namespace uWidgets.Views;

public partial class Widget : Window
{
    private readonly IWidgetSettingsProvider widgetSettingsProvider;
    private readonly IAppSettingsProvider appSettingsProvider;
    private readonly IGridService<Widget> gridService;
    private readonly IWidgetFactory<Window, UserControl> widgetFactory;
    private readonly Settings settingsWindow;
    private readonly WidgetInfoAttribute widgetInfo;

    public Widget(IAppSettingsProvider appSettingsProvider, IWidgetSettingsProvider widgetSettingsProvider, 
        IWidgetFactory<Window, UserControl> widgetFactory, IGridService<Widget> gridService, Settings settingsWindow,
        WidgetInfoAttribute widgetInfo)
    {
        this.widgetInfo = widgetInfo;
        this.widgetFactory = widgetFactory;
        this.settingsWindow = settingsWindow;
        this.widgetSettingsProvider = widgetSettingsProvider;
        this.appSettingsProvider = appSettingsProvider;
        this.gridService = gridService;
        Height = widgetSettingsProvider.Get().Height;
        Width = widgetSettingsProvider.Get().Width;
        DataContext = this;
        
        InitializeComponent();
        
        Position = new PixelPoint(
            widgetSettingsProvider.Get().X,
            widgetSettingsProvider.Get().Y
        );
        RenderOptions.SetTextRenderingMode(this, TextRenderingMode.Antialias);
        PointerPressed += OnPointerPressed;
        widgetSettingsProvider.DataChanged += UpdateContent;
    }

    private void UpdateContent(object? sender, WidgetSettings e)
    {
        Content = widgetFactory.CreateWidgetControl(widgetInfo.ViewType, e.GetModel(widgetInfo.ModelType));
    }

    public bool ShowEditButton => widgetInfo.EditModelViewType != null;
    public string Edit => $"{Locale.Widget_Edit} \"{widgetSettingsProvider.Get().Type}\"";
    public CornerRadius Radius => new(Const.CornerRadius / (Screens.ScreenFromWindow(this)?.Scaling ?? 1.0));

    public void EditWidget()
    {
        if (widgetInfo.EditModelViewType == null) return;
        widgetFactory
            .CreateEditWidgetWindow(widgetInfo.EditModelViewType, widgetSettingsProvider)
            .Show();
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
        
        var settings = widgetSettingsProvider.Get();
        widgetSettingsProvider.Save(settings with { X = Position.X, Y = Position.Y });
    }
    
    private void Resize(int columns, int rows)
    {
        gridService.SetSize(this, columns, rows);
        
        var settings = widgetSettingsProvider.Get();
        widgetSettingsProvider.Save(settings with { Width = (int)Width, Height = (int)Height });
    }

    public void Remove()
    {
        widgetSettingsProvider.Remove();
        Close();
    }
}