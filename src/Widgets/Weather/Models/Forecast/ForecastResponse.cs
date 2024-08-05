using System.Text.Json.Serialization;

namespace Weather.Models.Forecast;

public class ForecastResponse
{
    [JsonPropertyName("current")]
    public CurrentForecast Current { get; set; }
    
    [JsonPropertyName("hourly")] 
    public HourlyForecast Hourly { get; set; }

    [JsonPropertyName("daily")] 
    public DailyForecast Daily { get; set; }
        
    [JsonPropertyName("error")]
    public string Error { get; set; }
    
    [JsonPropertyName("reason")]
    public string Reason { get; set; }
}
