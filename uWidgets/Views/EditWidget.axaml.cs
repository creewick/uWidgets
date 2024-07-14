using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using uWidgets.Core.Interfaces;

namespace uWidgets.Views;

public partial class EditWidget : Window
{
    public EditWidget(UserControl userControl)
    {
        InitializeComponent();
    }
}