using Avalonia.Media;

namespace Weather.ViewModels;

public record HourlyForecastViewModel(string Time, StreamGeometry Icon, string Temperature);