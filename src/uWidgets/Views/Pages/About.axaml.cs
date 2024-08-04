using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using uWidgets.Core.Interfaces;
using uWidgets.Locales;
using uWidgets.Services;

namespace uWidgets.Views.Pages;

public partial class About : UserControl
{
    private readonly IAppSettingsProvider appSettingsProvider;
    public string? Version => Assembly.GetExecutingAssembly().GetName().Version?.ToString();
    
    public About(IAppSettingsProvider appSettingsProvider)
    {
        this.appSettingsProvider = appSettingsProvider;
        DataContext = this;
        _ = CheckForUpdates();
        InitializeComponent();
    }
    
    private async Task CheckForUpdates()
    {
        var version = await new UpdateService(appSettingsProvider).GetUpdateVersionAsync();
        if (version == null) return;
        
        Update.IsVisible = true;
        Update.Subtitle = string.Format(Locale.Update_UpdateFormat, version);
    }
    
    private void GoToUpdate(object? sender, RoutedEventArgs e)
    {
        Process.Start(
            new ProcessStartInfo("https://www.github.com/creewick/uWidgets/releases/latest") { UseShellExecute = true });
    }

    private void GoToRepository(object? sender, RoutedEventArgs e)
    {
        Process.Start(
            new ProcessStartInfo("https://www.github.com/creewick/uWidgets") { UseShellExecute = true });
    }

    private void GoToIssues(object? sender, RoutedEventArgs e)
    {
        Process.Start(
            new ProcessStartInfo("https://github.com/creewick/uWidgets/issues") { UseShellExecute = true });
    }
}