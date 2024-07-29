using Avalonia.Media;
using ReactiveUI;
using uWidgets.Services;
using Weather.Models;
using Weather.Services;

namespace Weather.ViewModels;

public class ForecastViewModel : ReactiveObject, IDisposable
{
    private readonly ForecastModel model;
    private readonly OpenMeteoWeatherProvider provider;

    public ForecastViewModel(ForecastModel model)
    {
        provider = new OpenMeteoWeatherProvider();
        this.model = model;
        TimerService.Timer1Hour.Subscribe(UpdateForecast);
        UpdateForecast();
    }

    private void UpdateForecast() => _ = UpdateForecastAsync();

    private async Task UpdateForecastAsync()
    {
        var forecast = await provider.GetForecastAsync(model.Latitude, model.Longitude, model.TemperatureUnit);

        if (forecast is null) return;

        var code = (int) forecast.Current.WeatherCode;

        CurrentTemperature = $"{forecast.Current.Temperature:0}°";
        CurrentIcon = WeatherIconProvider.GetIcon(forecast.Current.WeatherCode);
        CurrentCondition = Locales.Locale.ResourceManager.GetString($"Weather_Code_{code}");
        CurrentMinMax = $"{forecast.Daily.Max[0]:0}°  {forecast.Daily.Min[0]:0}°";
    }

    private StreamGeometry currentIcon = new();
    public StreamGeometry CurrentIcon
    {
        get => currentIcon;
        private set => this.RaiseAndSetIfChanged(ref currentIcon, value);
    }
    
    public string CityName => model.Name;

    private string? currentTemperature = "█°";
    public string? CurrentTemperature
    {
        get => currentTemperature;
        private set => this.RaiseAndSetIfChanged(ref currentTemperature, value);
    }
    
    private string? currentCondition = "████";
    public string? CurrentCondition
    {
        get => currentCondition;
        private set => this.RaiseAndSetIfChanged(ref currentCondition, value);
    }
    
    private string? currentMinMax = "█°  █°";
    public string? CurrentMinMax
    {
        get => currentMinMax;
        private set => this.RaiseAndSetIfChanged(ref currentMinMax, value);
    }

    public void Dispose()
    {
        TimerService.Timer1Hour.Unsubscribe(UpdateForecast);
        GC.SuppressFinalize(this);
    }
}