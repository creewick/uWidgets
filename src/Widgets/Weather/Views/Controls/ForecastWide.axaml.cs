using Avalonia.Controls;
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