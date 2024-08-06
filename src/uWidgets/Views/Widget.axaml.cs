using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;
using uWidgets.Core.Models.Settings;
using uWidgets.Locales;
using uWidgets.Services;

namespace uWidgets.Views;

public partial class Widget : Window
{
    private readonly IWidgetLayoutProvider widgetLayoutProvider;
    private readonly IAppSettingsProvider appSettingsProvider;
    private readonly IGridService<Widget> gridService;
    private readonly Func<UserControl> userControl;
    private readonly Func<Settings> settingsWindow;
    private readonly Func<EditWidget>? editWidgetWindow;

    public Widget(IAppSettingsProvider appSettingsProvider, IWidgetLayoutProvider widgetLayoutProvider, 
        IGridService<Widget> gridService, Func<UserControl> userControl, Func<Settings> settingsWindow, 
        Func<EditWidget>? editWidgetWindow = null)
    {
        this.settingsWindow = settingsWindow;
        this.editWidgetWindow = editWidgetWindow;
        this.widgetLayoutProvider = widgetLayoutProvider;
        this.userControl = userControl;
        this.appSettingsProvider = appSettingsProvider;
        this.gridService = gridService;
        InitializeComponent();
        
        Height = widgetLayoutProvider.Get().Height;
        Width = widgetLayoutProvider.Get().Width;
        Position = new PixelPoint(widgetLayoutProvider.Get().X, widgetLayoutProvider.Get().Y);
        Title = $"{widgetLayoutProvider.Get().Type} {widgetLayoutProvider.Get().SubType}";
        ContentPresenter.Content = userControl();
        DataContext = this;
        
        InteropService.RemoveWindowFromAltTab(this);
        RenderOptions.SetTextRenderingMode(this, TextRenderingMode.Antialias);
        
        PointerPressed += OnPointerPressed;
        PointerReleased += OnPointerReleased;
        widgetLayoutProvider.DataChanged += UpdateControl;
        appSettingsProvider.DataChanged += MoveResize;
        Unloaded += OnUnloaded;
    }
    
    public bool ShowEditButton => editWidgetWindow != null;
    public string Edit => $"{Locale.Widget_Edit} \"{widgetLayoutProvider.Get().Type}\"";
    public CornerRadius Radius => new(appSettingsProvider.Get().Dimensions.Radius / (Screens.ScreenFromWindow(this)?.Scaling ?? 1.0));
    public void EditWidget() => editWidgetWindow?.Invoke().ShowDialog(this);
    public void ResizeSmall() => _ = Resize(2, 2);
    public void ResizeMedium() => _ = Resize(4, 2);
    public void ResizeLarge() => _ = Resize(4, 4);
    public void ResizeExtraLarge() => _ = Resize(8, 4);
    public void OpenSettings() => settingsWindow.Invoke().Show();

    private void OnPointerReleased(object? sender, PointerReleasedEventArgs e) => AfterMove();

    private void Scale()
    {
        var scaleFactor = appSettingsProvider.Get().Dimensions.Size / 72.0;
        ContentPresenter.Width = Width / scaleFactor;
        ContentPresenter.Height = Height / scaleFactor;
        if (Math.Abs(scaleFactor - 1.0) < 0.01) return;
        
        ContentPresenter.RenderTransform = new ScaleTransform(scaleFactor, scaleFactor);
    }

    private void MoveResize(object sender, AppSettings? olddata, AppSettings newdata)
    {
        if (olddata?.Dimensions == newdata.Dimensions) return;
        
        AfterMove();
        AfterResize();
    }

    private void OnUnloaded(object? sender, RoutedEventArgs e)
    {
        PointerPressed -= OnPointerPressed;
        PointerReleased -= OnPointerReleased;
        Unloaded -= OnUnloaded;
        widgetLayoutProvider.DataChanged -= UpdateControl;
        appSettingsProvider.DataChanged -= MoveResize;
    }

    private void UpdateControl(object? sender, WidgetLayout? oldLayout, WidgetLayout newLayout)
    {
        if (!Equals(oldLayout?.Settings, newLayout.Settings))
            ContentPresenter.Content = userControl();
    }

    public void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (appSettingsProvider.Get().Layout.LockPosition) return;
        
        ToolTip.SetIsOpen(this, false);
        if (e.GetCurrentPoint(this).Properties.IsRightButtonPressed) return;
        
        BeginMoveDrag(e);
    }

    private void AfterMove()
    {
        var appSettings = appSettingsProvider.Get();
        
        if (appSettings.Layout.SnapPosition) 
            gridService.SnapPosition(this);
        
        var settings = widgetLayoutProvider.Get();
        widgetLayoutProvider.Save(settings with { X = Position.X, Y = Position.Y });
    }

    private async Task Resize(int columns, int rows)
    {
        Border.Transitions = new Transitions
        {
            new DoubleTransition { Property = WidthProperty, Duration = TimeSpan.FromMilliseconds(300) },
            new DoubleTransition { Property = HeightProperty, Duration = TimeSpan.FromMilliseconds(300) }
        };
        gridService.SetSize(this, columns, rows);
        AfterResize();
        await Task.Delay(300);
        Border.Transitions = null;
    }

    private void AfterResize()
    {
        if (appSettingsProvider.Get().Layout.SnapSize)
            gridService.SnapSize(this);
        
        Scale();
        var settings = widgetLayoutProvider.Get();
        widgetLayoutProvider.Save(settings with { Width = (int)Width, Height = (int)Height });
    }

    public void Remove()
    {
        widgetLayoutProvider.Remove();
        Close();
    }

    private void Resize(object? sender, PointerPressedEventArgs e)
    {
        if (appSettingsProvider.Get().Layout.LockSize) return;

        CanResize = true;
        BeginResizeDrag(WindowEdge.SouthEast, e);
        AfterResize();
        e.Handled = true;
        CanResize = false;
    }
}