using Avalonia.Media;
using ReactiveUI;
using uWidgets.Services;
using Weather.Models;
using Weather.Models.Forecast;
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

        var currentHour = DateTime.Now.Hour;

        CurrentTemperature = $"{forecast.Current.Temperature:0}°";
        CurrentIcon = WeatherIconProvider.GetIcon(forecast.Current.WeatherCode);
        CurrentCondition = GetCondition(forecast.Current.WeatherCode);
        CurrentMinMax = $"{forecast.Daily.Max[0]:0}°  {forecast.Daily.Min[0]:0}°";
        HourlyForecast = Enumerable
            .Range(currentHour, forecast.Hourly.Temperature.Count - currentHour)
            .Select(hour => GetHourlyForecast(forecast, hour % 24));
        DailyForecast = Enumerable
            .Range(0, forecast.Daily.Min.Count)
            .Select(day => GetDailyForecast(forecast, day));
    }

    private DailyForecastViewModel GetDailyForecast(ForecastResponse forecast, int day)
    {
        var dayOfWeek = (int) forecast.Daily.Time[day].DayOfWeek;
        var dayOfWeekName = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames[dayOfWeek];
        var code = forecast.Daily.WeatherCode[day];
        var icon = WeatherIconProvider.GetIcon(code);
        var min = forecast.Daily.Min[day];
        var max = forecast.Daily.Max[day];
        
        return new DailyForecastViewModel(dayOfWeekName, icon, $"{min:0}°", $"{max:0}°");
    }

    private HourlyForecastViewModel GetHourlyForecast(ForecastResponse forecast, int hour)
    {
        var time = hour.ToString();
        var code = forecast.Hourly.WeatherCode[hour];
        var icon = WeatherIconProvider.GetIcon(code);
        var temperature = forecast.Hourly.Temperature[hour];

        return new HourlyForecastViewModel(time, icon, $"{temperature:0}°");
    }
    
    private string? GetCondition(WeatherCode code) => Locales.Locale.ResourceManager.GetString($"Weather_Code_{(int)code}");

    private StreamGeometry currentIcon = new();
    public StreamGeometry CurrentIcon
    {
        get => currentIcon;
        private set => this.RaiseAndSetIfChanged(ref currentIcon, value);
    }

    private IEnumerable<HourlyForecastViewModel> hourlyForecast = [];
    public IEnumerable<HourlyForecastViewModel> HourlyForecast
    {
        get => hourlyForecast;
        private set => this.RaiseAndSetIfChanged(ref hourlyForecast, value);
    }

    private IEnumerable<DailyForecastViewModel> dailyForecast = [];
    public IEnumerable<DailyForecastViewModel> DailyForecast
    {
        get => dailyForecast;
        private set => this.RaiseAndSetIfChanged(ref dailyForecast, value);
    }
    
    public string CityName => model.Name;

    private string? currentTemperature = "--°";
    public string? CurrentTemperature
    {
        get => currentTemperature;
        private set => this.RaiseAndSetIfChanged(ref currentTemperature, value);
    }
    
    private string? currentCondition = "--------";
    public string? CurrentCondition
    {
        get => currentCondition;
        private set => this.RaiseAndSetIfChanged(ref currentCondition, value);
    }
    
    private string? currentMinMax = "--°  --°";
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