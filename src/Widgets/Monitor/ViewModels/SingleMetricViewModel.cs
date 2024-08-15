using Monitor.Models;
using Monitor.Services;
using ReactiveUI;
using uWidgets.Services;

namespace Monitor.ViewModels;

public class SingleMetricViewModel : ReactiveObject
{
    private readonly SingleMetricModel model;

    public SingleMetricViewModel(SingleMetricModel model)
    {
        this.model = model;
        metric = new MetricViewModel(0d);
        TimerService.Timer1Second.Subscribe(Update);
    }
    
    private MetricViewModel? metric;
    public MetricViewModel? Metric 
    {
        get => metric;
        private set => this.RaiseAndSetIfChanged(ref metric, value);
    }

    private void Update() => _ = UpdateAsync();

    private async Task UpdateAsync()
    {
        var value = await MetricService.GetMetricValue(model.Metric);
        var icon = MetricService.GetMetricIcon(model.Metric);
        if (value.HasValue)
            Metric = new MetricViewModel(value.Value, icon);
    }
}