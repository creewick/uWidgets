using System.Text.Json;
using Clock.Locales;
using Clock.Models;
using ReactiveUI;
using uWidgets.Core.Interfaces;

namespace Clock.ViewModels;

public class WorldClockSettingsViewModel(IWidgetLayoutProvider widgetLayoutProvider) : ReactiveObject
{
    private readonly WorldClockModel clockModel = GetInitialModel(widgetLayoutProvider);
    private static WorldClockModel GetInitialModel(IWidgetLayoutProvider widgetLayoutProvider)
    {
        var model = widgetLayoutProvider.Get().GetModel<WorldClockModel>();

        if (model?.TimeZones == null || model.TimeZones.Count < 4)
            return new WorldClockModel([null, null, null, null]);

        return model;
    }
    
    public TimeZoneInfo[] TimeZones => TimeZoneInfo.GetSystemTimeZones().Append(TimeZone1).Append(TimeZone2).Append(TimeZone3).Append(TimeZone4).ToArray();

    public string TimeZone1Title => $"{Locale.Clock_TimeZone} 1";
    public string TimeZone2Title => $"{Locale.Clock_TimeZone} 2";
    public string TimeZone3Title => $"{Locale.Clock_TimeZone} 3";
    public string TimeZone4Title => $"{Locale.Clock_TimeZone} 4";
    
    public TimeZoneInfo TimeZone1
    {
        get => Get(0);
        set => Set(0, value);
    }
    
    public TimeZoneInfo TimeZone2
    {
        get => Get(1);
        set => Set(1, value);
    }
    
    public TimeZoneInfo TimeZone3
    {
        get => Get(2);
        set => Set(2, value);
    }
    
    public TimeZoneInfo TimeZone4
    {
        get => Get(3);
        set => Set(3, value);
    }

    private TimeZoneInfo Get(int index)
    {
        var baseUtcOffset = clockModel.TimeZones[index];
        var timespan = TimeSpan.FromHours(baseUtcOffset ?? 0);
        var name = $"({(timespan.Hours >= 0 ? "UTC+" : "UTC")}{timespan.Hours:00}:{timespan.Minutes:D2})";
        return TimeZoneInfo.CreateCustomTimeZone(name, timespan, name, name);
    }

    private void Set(int index, TimeZoneInfo timeZone)
    {
        clockModel.TimeZones[index] = timeZone.BaseUtcOffset.TotalMinutes / 60;
        UpdateClockModel();
    }
    
    private void UpdateClockModel()
    {
        var widgetSettings = widgetLayoutProvider.Get();
        var newSettings = widgetSettings with { Settings = JsonSerializer.SerializeToElement(clockModel) };
        widgetLayoutProvider.Save(newSettings);
    }
}