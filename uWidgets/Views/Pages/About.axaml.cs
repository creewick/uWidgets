using System.Diagnostics;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Input;
using uWidgets.Core.Interfaces;

namespace uWidgets.Views.Pages;

public partial class About : UserControl
{
    public string? Version => Assembly.GetExecutingAssembly().GetName().Version?.ToString();
    
    public About(IAppSettingsProvider appSettingsProvider)
    {
        DataContext = this;
        InitializeComponent();
    }

    private void GoToRepository(object? sender, PointerPressedEventArgs e)
    {
        Process.Start(
            new ProcessStartInfo("https://www.github.com/creewick/uWidgets") { UseShellExecute = true });
    }

    private void GoToIssues(object? sender, PointerPressedEventArgs e)
    {
        Process.Start(
            new ProcessStartInfo("https://github.com/creewick/uWidgets/issues") { UseShellExecute = true });
    }
}