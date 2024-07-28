using Avalonia.Controls;
using Weather.Models;
using Weather.ViewModels;

namespace Weather.Views;

public partial class Forecast : UserControl
{
    public Forecast() : this(new ForecastModel("Cupertino", 37.3230, -122.0322, "celsius")) {}
    
    public Forecast(ForecastModel model)
    {
        DataContext = new ForecastViewModel(model);
        InitializeComponent();
    }
}