using System;
using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace uWidgets;

public partial class Settings : Window
{
    public ListItemTemplate[] MenuItems =>
    [
        new ListItemTemplate(typeof(Settings), "Appearance"),
        new ListItemTemplate(typeof(Settings), "Language"),
        new ListItemTemplate(typeof(Settings), "About")
    ];
    

    public Settings()
    {        DataContext = this;
        Resized += OnResized;
        InitializeComponent();
    }
    
    private void OnResized(object? sender, WindowResizedEventArgs e)
    {
        SplitView.IsPaneOpen = Width >= 800;
    }
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (OperatingSystem.IsWindows())
        {
            var platformHandle = this.TryGetPlatformHandle();
            if (platformHandle != null)
            {
                var hwnd = platformHandle.Handle;
                SetCornerPreference(hwnd);
            }
        }
    }

    [DllImport("dwmapi.dll", PreserveSig = true)]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, uint attr, ref int attrValue, int attrSize);

    private const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
    private const int DWMWCP_DEFAULT = 0;

    private void SetCornerPreference(IntPtr hwnd)
    {
        int preference = DWMWCP_DEFAULT;
        DwmSetWindowAttribute(hwnd, DWMWA_WINDOW_CORNER_PREFERENCE, ref preference, sizeof(int));
    }
}