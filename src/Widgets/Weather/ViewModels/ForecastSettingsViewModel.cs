using System.Text.Json;
using ReactiveUI;
using uWidgets.Core.Interfaces;
using Weather.Locales;
using Weather.Models;
using Weather.Models.Geocoding;

namespace Weather.ViewModels;

public class ForecastSettingsViewModel(IWidgetLayoutProvider widgetLayoutProvider) : ReactiveObject
{
    private ForecastModel forecastModel = widgetLayoutProvider.Get().GetModel<ForecastModel>() 
                                          ?? new ForecastModel("Cupertino", 37.3230, -122.0322, "celsius");
    
    public City Location
    {
        get => new City { Name = forecastModel.Name };
        set
        {
            var newModel = forecastModel with { Name = value.Name, Latitude = value.Latitude, Longitude = value.Longitude };
            UpdateClockModel(newModel);   
        }
    }
    
    public TemperatureUnitViewModel TemperatureUnit
    {
        get => new(forecastModel.TemperatureUnit, forecastModel.TemperatureUnit == "celsius" 
            ? Locale.Weather_Units_Celsius 
            : Locale.Weather_Units_Fahrenheit);
        set
        {
            var newModel = forecastModel with { TemperatureUnit = value.Value };
            UpdateClockModel(newModel);
        }
    }

    public List<TemperatureUnitViewModel> TemperatureUnits =>
    [
        new TemperatureUnitViewModel("celsius", Locale.Weather_Units_Celsius),
        new TemperatureUnitViewModel("fahrenheit", Locale.Weather_Units_Fahrenheit),
    ];
    
    private void UpdateClockModel(ForecastModel newModel)
    {
        forecastModel = newModel;
        var widgetSettings = widgetLayoutProvider.Get();
        var newSettings = widgetSettings with { Settings = JsonSerializer.SerializeToElement(forecastModel) };
        widgetLayoutProvider.Save(newSettings);
        this.RaisePropertyChanged(nameof(Location));
    }
}