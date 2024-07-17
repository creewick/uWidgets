using System.Text.Json;
using Clock.Models;
using ReactiveUI;
using uWidgets.Core.Interfaces;

namespace Clock.ViewModels;

public class ClockSettingsViewModel(IWidgetSettingsProvider widgetSettingsProvider) : ReactiveObject
{
    private ClockModel clockModel = widgetSettingsProvider.Get().GetModel<ClockModel>() ?? new ClockModel();

    public bool ShowSeconds
    {
        get => clockModel.ShowSeconds;
        set => UpdateClockModel(clockModel with { ShowSeconds = value });
    }

    public bool Use24Hours
    {
        get => clockModel.Use24Hours;
        set => UpdateClockModel(clockModel with { Use24Hours = value });
    }

    public bool ShowTimeZones => clockModel.TimeZone.HasValue;

    public bool UseLocalTimeZone
    {
        get => !clockModel.TimeZone.HasValue;
        set => UpdateClockModel(clockModel with { TimeZone = value ? null : TimeZoneInfo.Local.BaseUtcOffset.Hours });
    }

    public TimeZoneInfo TimeZone
    {
        get
        {
            var timespan = TimeSpan.FromHours(clockModel.TimeZone ?? 0);
            var name = $"UTC{(timespan >= TimeSpan.Zero ? "+" : "-")}{timespan.Hours:D2}:{timespan.Minutes:D2}";
            return TimeZoneInfo.CreateCustomTimeZone(name, timespan, name, name);
        }
        set => UpdateClockModel(clockModel with { TimeZone = value.BaseUtcOffset.Hours });
    }

    public TimeZoneInfo[] TimeZones => TimeZoneInfo.GetSystemTimeZones().ToArray();

    private void UpdateClockModel(ClockModel newClockModel)
    {
        clockModel = newClockModel;
        var widgetSettings = widgetSettingsProvider.Get();
        var newSettings = widgetSettings with { Settings = JsonSerializer.SerializeToElement(clockModel) };
        widgetSettingsProvider.Save(newSettings);
        this.RaisePropertyChanged(nameof(ShowSeconds));
        this.RaisePropertyChanged(nameof(Use24Hours));
        this.RaisePropertyChanged(nameof(ShowTimeZones));
        this.RaisePropertyChanged(nameof(UseLocalTimeZone));
        this.RaisePropertyChanged(nameof(TimeZone));
    }
}