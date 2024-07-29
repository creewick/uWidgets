using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using uWidgets.Locales;
using uWidgets.Services;

namespace uWidgets.Views;

public partial class UpdatePopup : Window
{
    private readonly Version version;
    private readonly UpdateService updateService;
    
    public UpdatePopup(Version version, UpdateService updateService)
    {
        this.version = version;
        this.updateService = updateService;
        DataContext = this;
        InitializeComponent();
    }
    
    public string UpdateText => string.Format(Locale.Update_UpdateFormat, version);

    private void DownloadUpdate(object? sender, RoutedEventArgs e)
    {
        Process.Start(
            new ProcessStartInfo("https://www.github.com/creewick/uWidgets/releases/latest") { UseShellExecute = true });
        Close();
    }

    private void Later(object? sender, RoutedEventArgs e) => Close();

    private void SkipThisVersion(object? sender, RoutedEventArgs e)
    {
        updateService.SkipVersion(version);
        Close();
    }
}