<<<<<<< HEAD
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
=======
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
>>>>>>> parent of 15524c5 (Delete src directory)
}