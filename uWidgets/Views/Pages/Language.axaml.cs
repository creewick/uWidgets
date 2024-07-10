using Avalonia.Controls;
using uWidgets.Core.Interfaces;
using uWidgets.ViewModels;

namespace uWidgets.Views.Pages;

public partial class Language : UserControl
{
    public Language(IAppSettingsProvider appSettingsProvider)
    {
        DataContext = new LanguageViewModel(appSettingsProvider);
        InitializeComponent();
    }
}