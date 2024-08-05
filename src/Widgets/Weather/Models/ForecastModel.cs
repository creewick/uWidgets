namespace Weather.Models;

public record ForecastModel(
    string Name,
    double Latitude,
    double Longitude,
    string TemperatureUnit);