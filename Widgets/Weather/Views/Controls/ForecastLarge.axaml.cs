using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Weather.ViewModels;

namespace Weather.Views.Controls;

public partial class ForecastLarge : UserControl
{
    public ForecastLarge(ForecastViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}