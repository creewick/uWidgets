<<<<<<< HEAD
using System.Text.Json.Serialization;

namespace Weather.Models.Forecast;

public class DailyForecast
{
    [JsonPropertyName("time")] 
    public List<DateTime> Time { get; set; }

    [JsonPropertyName("temperature_2m_min")]
    public List<double> Min { get; set; }

    [JsonPropertyName("temperature_2m_max")]
    public List<double> Max { get; set; }

    [JsonPropertyName("weathercode")] 
    public List<WeatherCode> WeatherCode { get; set; }
    
    [JsonPropertyName("sunrise")]
    public List<DateTime> Sunrise { get; set; }
    
    [JsonPropertyName("sunset")]
    public List<DateTime> Sunset { get; set; }
=======
using System.Text.Json.Serialization;

namespace Weather.Models.Forecast;

public class DailyForecast
{
    [JsonPropertyName("time")] 
    public List<DateTime> Time { get; set; }

    [JsonPropertyName("temperature_2m_min")]
    public List<double> Min { get; set; }

    [JsonPropertyName("temperature_2m_max")]
    public List<double> Max { get; set; }

    [JsonPropertyName("weathercode")] 
    public List<WeatherCode> WeatherCode { get; set; }
    
    [JsonPropertyName("sunrise")]
    public List<DateTime> Sunrise { get; set; }
    
    [JsonPropertyName("sunset")]
    public List<DateTime> Sunset { get; set; }
>>>>>>> parent of 15524c5 (Delete src directory)
}