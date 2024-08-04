using System.Text.Json.Serialization;

namespace Weather.Models.Forecast;

public class CurrentForecast
{
    [JsonPropertyName("time")]
    public DateTime Time { get; set; }
    
    [JsonPropertyName("temperature_2m")]
    public double Temperature { get; set; }
    
    [JsonPropertyName("weathercode")]
    public WeatherCode WeatherCode { get; set; }
}