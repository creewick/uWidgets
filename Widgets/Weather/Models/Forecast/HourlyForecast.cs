using System.Text.Json.Serialization;

namespace Weather.Models.Forecast;

public class HourlyForecast
{
    [JsonPropertyName("time")] 
    public List<DateTime> Time { get; set; }

    [JsonPropertyName("temperature_2m")] 
    public List<double> Temperature { get; set; }

    [JsonPropertyName("weathercode")] 
    public List<WeatherCode> WeatherCode { get; set; }
}