using Avalonia.Controls;
using uWidgets.Core.Interfaces;

namespace uWidgets.Views;

public partial class Appearance : UserControl
{
    public Appearance(IAppSettingsProvider appSettingsProvider)
    {
        InitializeComponent();
    }
}