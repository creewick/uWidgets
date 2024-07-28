using System.Text.Json;
using Weather.Models.Forecast;
using Weather.Models.Geocoding;

namespace Weather.Services;

public class OpenMeteoWeatherProvider
{
    private readonly HttpClient httpClient;

    public OpenMeteoWeatherProvider()
    {
        httpClient = new HttpClient();
    }
    
    public async Task<ForecastResponse?> GetForecastAsync(double latitude, double longitude, string temperatureUnit)
    {
        try
        {
            var url = $"https://api.open-meteo.com/v1/forecast?" +
                      $"latitude={latitude:F4}&" +
                      $"longitude={longitude:F4}&" +
                      $"temperature_unit={temperatureUnit}&" +
                      $"current=temperature_2m,weathercode&" +
                      $"hourly=temperature_2m,weathercode&" +
                      $"daily=temperature_2m_min,temperature_2m_max,weathercode&" +
                      $"timezone=auto";
            
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            var forecast = JsonSerializer.Deserialize<ForecastResponse>(json);

            return forecast;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<List<City>?> GetCitiesAsync(string cityName)
    {
        try
        {
            var url = $"https://geocoding-api.open-meteo.com/v1/search?" +
                      $"name={cityName}&" +
                      $"language={Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName}";

            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            var searchResults = JsonSerializer.Deserialize<GeocodingResponse>(json);

            return searchResults?.Cities;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}