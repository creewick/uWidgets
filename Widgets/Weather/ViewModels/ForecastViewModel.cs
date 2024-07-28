using Avalonia.Threading;
using ReactiveUI;
using Weather.Models;
using Weather.Services;

namespace Weather.ViewModels;

public class ForecastViewModel : ReactiveObject, IDisposable
{
    private readonly ForecastModel model;
    private readonly OpenMeteoWeatherProvider provider;
    private readonly DispatcherTimer timer;

    public ForecastViewModel(ForecastModel model)
    {
        provider = new OpenMeteoWeatherProvider();
        this.model = model;
        timer = new DispatcherTimer { Interval = TimeSpan.FromHours(1) };
        timer.Tick += Tick;
        timer.Start();
        _ = UpdateForecast();
    }

    private void Tick(object? sender, EventArgs e) => _ = UpdateForecast();

    private async Task UpdateForecast()
    {
        var forecast = await provider.GetForecastAsync(model.Latitude, model.Longitude, model.TemperatureUnit);

        if (forecast is null) return;

        var code = (int) forecast.Current.WeatherCode;

        CurrentTemperature = $"{forecast.Current.Temperature:0}°";
        CurrentCondition = Locales.Locale.ResourceManager.GetString($"Weather_Code_{code}");
        CurrentMinMax = $"{forecast.Daily.Max[0]:0}°  {forecast.Daily.Min[0]:0}°";
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
        timer.Stop();
        timer.Tick -= Tick;
    }
}