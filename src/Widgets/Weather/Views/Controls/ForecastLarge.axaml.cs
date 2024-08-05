using Avalonia.Controls;
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