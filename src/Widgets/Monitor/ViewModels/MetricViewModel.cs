using Avalonia.Media;

namespace Monitor.ViewModels;

public record MetricViewModel(double Value, StreamGeometry? Icon = null)
{
    public double StrokeDashOffset => 31.4 * (1 - Value);
    public string Text => $"{Value:P0}";
};