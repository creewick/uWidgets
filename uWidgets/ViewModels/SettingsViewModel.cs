using System;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Controls.Selection;
using uWidgets.Core.Interfaces;
using uWidgets.Models;
using uWidgets.Views;

namespace uWidgets.ViewModels;

public class SettingsViewModel(IAppSettingsProvider appSettingsProvider) : INotifyPropertyChanged
{
    public UserControl? CurrentPage { get; set; }
    
    public string? CurrentPageTitle { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    
    public ListItemTemplate[] MenuItems =>
    [
        new ListItemTemplate(typeof(Appearance), "Appearance"),
        new ListItemTemplate(null, "Language"),
        new ListItemTemplate(null, "About")
    ];

    public void SetCurrentPage(ListItemTemplate? value)
    {
        CurrentPage = value?.Type != null
            ? (UserControl?)Activator.CreateInstance(value.Type, appSettingsProvider)
            : null;
        CurrentPageTitle = value?.Text;
        Update(nameof(CurrentPage));
        Update(nameof(CurrentPageTitle));
    }
    
    private void Update(string propertyName) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}