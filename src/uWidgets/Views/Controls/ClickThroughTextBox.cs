using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Styling;

namespace uWidgets.Views.Controls;

public class ClickThroughTextBox : TextBox, IStyleable
{
    Type IStyleable.StyleKey => typeof(TextBox);
    public FlyoutBase? DefaultContextFlyout { get; set; }
    
    public ClickThroughTextBox()
    {
        Focusable = false;
        AddHandler(PointerPressedEvent, OnPointerPressed, RoutingStrategies.Tunnel);
        KeyDown += OnKeyDown;
        LostFocus += OnLostFocus;
        Initialized += OnInitialized;
        Unloaded += OnUnloaded;
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (VisualRoot is Widget widget &&
            ((e.Key == Key.Enter && !AcceptsReturn) || 
             (e.Key == Key.Tab && !AcceptsTab) ||
             (e.Key == Key.Escape)))
        {
            widget.FocusManager?.ClearFocus();
        }
    }

    private void OnUnloaded(object? sender, RoutedEventArgs e)
    {
        RemoveHandler(PointerPressedEvent, OnPointerPressed);
        KeyDown -= OnKeyDown;
        LostFocus -= OnLostFocus;
        Initialized -= OnInitialized;
        Unloaded -= OnUnloaded;
    }

    private void OnInitialized(object? sender, EventArgs e)
    {
        DefaultContextFlyout = ContextFlyout;
        ContextFlyout = null;
    }

    private void OnLostFocus(object? sender, RoutedEventArgs e)
    {
        if (ContextFlyout is { IsOpen: true })
        {
            e.Handled = true;
        }
        else
        {
            ContextFlyout = null;
            Focusable = false;
            ClearSelection();
        }
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (!IsFocused && e.ClickCount == 1 && VisualRoot is Widget widget)
        {
            widget.OnPointerPressed(sender, e);
            e.Handled = true;
        } else
        {
            Focusable = true;
            ContextFlyout = DefaultContextFlyout;
        }
    }
}