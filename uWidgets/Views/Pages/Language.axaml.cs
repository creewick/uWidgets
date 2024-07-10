using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using uWidgets.Core.Interfaces;

namespace uWidgets.Views.Pages;

public partial class Language : UserControl
{
    public Language(IAppSettingsProvider appSettingsProvider)
    {
        InitializeComponent();
    }
}