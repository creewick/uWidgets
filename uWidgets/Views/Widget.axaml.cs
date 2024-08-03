using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using uWidgets.Core;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;
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
        
        InitializeComponent();
        InteropService.RemoveWindowFromAltTab(this);

        ContentPresenter.Content = userControl();
        var scaleFactor = appSettingsProvider.Get().Layout.WidgetSize / 72f;
        ContentPresenter.Width = Width / scaleFactor;
        ContentPresenter.Height = Height / scaleFactor;
        ContentPresenter.RenderTransform = new ScaleTransform(scaleFactor, scaleFactor);
        
        Position = new PixelPoint(
            widgetLayoutProvider.Get().X,
            widgetLayoutProvider.Get().Y
        );
        RenderOptions.SetTextRenderingMode(this, TextRenderingMode.Antialias);
        PointerPressed += OnPointerPressed;
        widgetLayoutProvider.DataChanged += UpdateControl;
        appSettingsProvider.DataChanged += MoveResize;
        Unloaded += OnUnloaded;
    }

    private void MoveResize(object sender, AppSettings? olddata, AppSettings newdata)
    {
        if (olddata?.Layout == newdata.Layout) return;
        
        AfterMove();
        AfterResize();
    }

    private void OnUnloaded(object? sender, RoutedEventArgs e)
    {
        PointerPressed -= OnPointerPressed;
        widgetLayoutProvider.DataChanged -= UpdateControl;
        appSettingsProvider.DataChanged -= MoveResize;
    }

    private void UpdateControl(object? sender, WidgetLayout? oldLayout, WidgetLayout newLayout)
    {
        if (!Equals(oldLayout?.Settings, newLayout.Settings))
            ContentPresenter.Content = userControl();
    }

    public bool ShowEditButton => editWidgetWindow != null;
    public string Edit => $"{Locale.Widget_Edit} \"{widgetLayoutProvider.Get().Type}\"";
    public CornerRadius Radius => new(Const.CornerRadius / (Screens.ScreenFromWindow(this)?.Scaling ?? 1.0));
    public void EditWidget() => editWidgetWindow?.Invoke().ShowDialog(this);
    public void ResizeSmall() => _ = Resize(2, 2);
    public void ResizeMedium() => _ = Resize(4, 2);
    public void ResizeLarge() => _ = Resize(4, 4);
    public void ResizeExtraLarge() => _ = Resize(8, 4);
    public void OpenSettings() => settingsWindow.Invoke().Show();

    public void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (appSettingsProvider.Get().Layout.LockPosition) return;
        
        ToolTip.SetIsOpen(this, false);
        if (e.GetCurrentPoint(this).Properties.IsRightButtonPressed) return;
        
        BeginMoveDrag(e);
        AfterMove();
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
        var scaleFactor = appSettingsProvider.Get().Layout.WidgetSize / 72f;
        ContentPresenter.Width = Width / scaleFactor;
        ContentPresenter.Height = Height / scaleFactor;
        ContentPresenter.RenderTransform = new ScaleTransform(scaleFactor, scaleFactor);
        
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
        
        BeginResizeDrag(WindowEdge.SouthEast, e);
        AfterResize();
        e.Handled = true;
    }
}