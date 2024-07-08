using Avalonia.Controls;
using uWidgets.Core.Interfaces;
using uWidgets.Models;
using uWidgets.ViewModels;

namespace uWidgets.Views;

public partial class Settings : Window
{
    private readonly SettingsViewModel viewModel;

    public Settings(IAppSettingsProvider appSettingsProvider)
    {
        viewModel = new SettingsViewModel(appSettingsProvider);
        DataContext = viewModel;
        Resized += OnResized;
        InitializeComponent();
        viewModel.SetCurrentPage(viewModel.MenuItems[0]);
    }
    
    private void OnResized(object? sender, WindowResizedEventArgs e)
    {
        SplitView.IsPaneOpen = Width >= 800;
    }

    private void OnMenuItemChanged(object? _, SelectionChangedEventArgs e)
    {
        viewModel.SetCurrentPage(e.AddedItems[0] as ListItemTemplate);
    }
}