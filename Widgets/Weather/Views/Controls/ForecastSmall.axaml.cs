using Avalonia.Controls;
using Weather.ViewModels;

namespace Weather.Views.Controls;

public partial class ForecastSmall : UserControl
{
    public ForecastSmall(ForecastViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}