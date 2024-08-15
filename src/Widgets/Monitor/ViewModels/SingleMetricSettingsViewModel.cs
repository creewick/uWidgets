using System.Text.Json;
using Monitor.Locales;
using Monitor.Models;
using ReactiveUI;
using uWidgets.Core.Interfaces;

namespace Monitor.ViewModels;

public class SingleMetricSettingsViewModel(IWidgetLayoutProvider widgetLayoutProvider) : ReactiveObject
{
    private SingleMetricModel model = widgetLayoutProvider.Get().GetModel<SingleMetricModel>() ?? new SingleMetricModel(MetricType.CpuUsage);

    public MetricTypeViewModel[] AllTypes =>
    [
        new MetricTypeViewModel(MetricType.CpuUsage, Locale.Monitor_Metric_0),
        new MetricTypeViewModel(MetricType.RamUsage, Locale.Monitor_Metric_1),
        new MetricTypeViewModel(MetricType.DiskUsage, Locale.Monitor_Metric_2),
        new MetricTypeViewModel(MetricType.NetworkUsage, Locale.Monitor_Metric_3),
        new MetricTypeViewModel(MetricType.BatteryLevel, Locale.Monitor_Metric_4)
    ];
    
    public MetricTypeViewModel SelectedType
    {
        get => AllTypes.First(x => x.Type == model.Metric);
        set
        {
            model = model with { Metric = value.Type };
            var widgetSettings = widgetLayoutProvider.Get();
            widgetLayoutProvider.Save(widgetSettings with { Settings = JsonSerializer.SerializeToElement(model)});
        }
    }
}