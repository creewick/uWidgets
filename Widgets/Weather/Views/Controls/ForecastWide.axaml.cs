using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Weather.ViewModels;

namespace Weather.Views.Controls;

public partial class ForecastWide : UserControl
{
    public ForecastWide(ForecastViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}