<<<<<<< HEAD
using Avalonia.Controls;
using Weather.Models;
using Weather.ViewModels;

namespace Weather.Views;

public partial class Temperature : UserControl
{
    public Temperature() : this(new ForecastModel("Cupertino", 37.3230, -122.0322, "celsius")) {}

    public Temperature(ForecastModel model)
    {
        DataContext = new ForecastViewModel(model);
        InitializeComponent();
    }
=======
using Avalonia.Controls;
using Weather.Models;
using Weather.ViewModels;

namespace Weather.Views;

public partial class Temperature : UserControl
{
    public Temperature() : this(new ForecastModel("Cupertino", 37.3230, -122.0322, "celsius")) {}

    public Temperature(ForecastModel model)
    {
        DataContext = new ForecastViewModel(model);
        InitializeComponent();
    }
>>>>>>> parent of 15524c5 (Delete src directory)
}