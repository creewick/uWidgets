using System;
using System.ComponentModel;
using Avalonia.Controls;
using uWidgets.Core.Interfaces;
using uWidgets.Views;

namespace uWidgets;

public partial class Settings : Window, INotifyPropertyChanged
{
    private readonly IAppSettingsProvider appSettingsProvider;
    
    private ListItemTemplate? selectedListItem;

    public UserControl? CurrentPage { get; set; }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    public Settings(IAppSettingsProvider appSettingsProvider)
    {
        CurrentPage = new Appearance(appSettingsProvider);
        this.appSettingsProvider = appSettingsProvider;
        DataContext = this;
        Resized += OnResized;
        InitializeComponent();
    }
    
    public ListItemTemplate[] MenuItems =>
    [
        new ListItemTemplate(typeof(Appearance), "Appearance"),
        new ListItemTemplate(null, "Language"),
        new ListItemTemplate(null, "About")
    ];
    
    private void OnResized(object? sender, WindowResizedEventArgs e)
    {
        SplitView.IsPaneOpen = Width >= 800;
    }

    private void OnMenuItemChanged(object? sender, SelectionChangedEventArgs e)
    {
        CurrentPage = e.AddedItems[0] is ListItemTemplate menuItem && menuItem.Type != null
            ? (UserControl?)Activator.CreateInstance(menuItem.Type, appSettingsProvider)
            : null;
        
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
    }
}