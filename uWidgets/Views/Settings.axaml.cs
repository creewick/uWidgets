using Avalonia.Controls;
using uWidgets.Core.Interfaces;
using uWidgets.ViewModels;

namespace uWidgets.Views;

public partial class Settings : Window
{
    private readonly SettingsViewModel viewModel;

    public Settings(IAppSettingsProvider appSettingsProvider, IAssemblyProvider assemblyProvider, IWidgetFactory<Widget> widgetFactory)
    {
        viewModel = new SettingsViewModel(appSettingsProvider, assemblyProvider, widgetFactory);
        DataContext = viewModel;
        Resized += OnResized;
        KeyDown += (_, _) => Title.Text = "UwUidgets";
        InitializeComponent();
        ListBox.SelectedItem = SettingsViewModel.MenuItems[0];
    }

    private void OnResized(object? sender, WindowResizedEventArgs e) => 
        SplitView.IsPaneOpen = Width >= 800;

    private void OnMenuItemChanged(object? _, SelectionChangedEventArgs e) => 
        viewModel.SetCurrentPage(e.AddedItems[0] as PageViewModel);
}