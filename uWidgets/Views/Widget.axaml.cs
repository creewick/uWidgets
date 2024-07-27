using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using uWidgets.Core;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;
using uWidgets.Locales;

namespace uWidgets.Views;

public partial class Widget : Window
{
    private readonly IWidgetLayoutProvider widgetLayoutProvider;
    private readonly IAppSettingsProvider appSettingsProvider;
    private readonly IGridService<Widget> gridService;
    private readonly Func<UserControl> userControl;
    private readonly Func<Settings> settingsWindow;
    private readonly Func<EditWidget>? editWidgetWindow;

    public Widget(IAppSettingsProvider appSettingsProvider, IWidgetLayoutProvider widgetLayoutProvider, Func<UserControl> userControl,
        IGridService<Widget> gridService, Func<Settings> settingsWindow, Func<EditWidget>? editWidgetWindow = null)
    {
        this.settingsWindow = settingsWindow;
        this.editWidgetWindow = editWidgetWindow;
        this.widgetLayoutProvider = widgetLayoutProvider;
        this.userControl = userControl;
        this.appSettingsProvider = appSettingsProvider;
        this.gridService = gridService;
        Height = widgetLayoutProvider.Get().Height;
        Width = widgetLayoutProvider.Get().Width;
        Title = $"{widgetLayoutProvider.Get().Type} {widgetLayoutProvider.Get().SubType}";
        DataContext = this;
        Content = userControl();
        
        InitializeComponent();
        
        Position = new PixelPoint(
            widgetLayoutProvider.Get().X,
            widgetLayoutProvider.Get().Y
        );
        RenderOptions.SetTextRenderingMode(this, TextRenderingMode.Antialias);
        PointerPressed += OnPointerPressed;
        widgetLayoutProvider.DataChanged += UpdateControl;
        Unloaded += OnUnloaded;
    }

    private void OnUnloaded(object? sender, RoutedEventArgs e)
    {
        PointerPressed -= OnPointerPressed;
        widgetLayoutProvider.DataChanged -= UpdateControl;
    }

    private void UpdateControl(object? sender, WidgetLayout? oldLayout, WidgetLayout newLayout)
    {
        if (!Equals(oldLayout?.Settings, newLayout.Settings))
            Content = userControl();
    }

    public bool ShowEditButton => editWidgetWindow != null;
    public string Edit => $"{Locale.Widget_Edit} \"{widgetLayoutProvider.Get().Type}\"";
    public CornerRadius Radius => new(Const.CornerRadius / (Screens.ScreenFromWindow(this)?.Scaling ?? 1.0));
    public void EditWidget() => editWidgetWindow?.Invoke().ShowDialog(this);
    public void ResizeSmall() => Resize(2, 2);
    public void ResizeMedium() => Resize(4, 2);
    public void ResizeLarge() => Resize(4, 4);
    public void ResizeExtraLarge() => Resize(8, 4);
    public void OpenSettings() => settingsWindow.Invoke().Show();

    public void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        BeginMoveDrag(e);
        
        var appSettings = appSettingsProvider.Get();

        if (appSettings.Layout.SnapPosition) 
            gridService.SnapPosition(this);
        
        var settings = widgetLayoutProvider.Get();
        widgetLayoutProvider.Save(settings with { X = Position.X, Y = Position.Y });
    }
    
    private void Resize(int columns, int rows)
    {
        gridService.SetSize(this, columns, rows);
        
        var settings = widgetLayoutProvider.Get();
        widgetLayoutProvider.Save(settings with { Width = (int)Width, Height = (int)Height });
    }

    public void Remove()
    {
        widgetLayoutProvider.Remove();
        Close();
    }
}