using Avalonia.Controls;
using uWidgets.Core.Interfaces;
using uWidgets.Models;
using uWidgets.ViewModels;

namespace uWidgets.Views;

public partial class Settings : Window
{

    public Settings(IAppSettingsProvider appSettingsProvider)
    {
        DataContext = new SettingsViewModel(appSettingsProvider);
        Resized += OnResized;
        InitializeComponent();
    }
    
    private void OnResized(object? sender, WindowResizedEventArgs e)
    {
        SplitView.IsPaneOpen = Width >= 800;
    }

    private void OnMenuItemChanged(object? _, SelectionChangedEventArgs e)
    {
        (DataContext as SettingsViewModel)!.SetCurrentPage(e.AddedItems[0] as ListItemTemplate);
    }
}