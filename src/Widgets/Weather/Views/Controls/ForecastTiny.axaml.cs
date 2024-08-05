using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
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