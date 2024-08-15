<<<<<<< HEAD
using Avalonia.Controls;
using Weather.Models;
using Weather.ViewModels;

namespace Weather.Views;

public partial class UVIndex : UserControl
{
    public UVIndex() : this(new ForecastModel("Cupertino", 37.3230, -122.0322, "celsius")) {}

    public UVIndex(ForecastModel model)
    {
        DataContext = new ForecastViewModel(model);
        InitializeComponent();
    }
=======
using Avalonia.Controls;
using Weather.Models;
using Weather.ViewModels;

namespace Weather.Views;

public partial class UVIndex : UserControl
{
    public UVIndex() : this(new ForecastModel("Cupertino", 37.3230, -122.0322, "celsius")) {}

    public UVIndex(ForecastModel model)
    {
        DataContext = new ForecastViewModel(model);
        InitializeComponent();
    }
>>>>>>> parent of 15524c5 (Delete src directory)
}