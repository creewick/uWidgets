using Avalonia.Controls;
using Weather.ViewModels;

namespace Weather.Views.Controls;

public partial class ForecastTiny : UserControl
{
    public ForecastTiny(ForecastViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}