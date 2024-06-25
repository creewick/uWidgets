using System.ComponentModel;
using Avalonia.Threading;
using Clock.SettingsModels;

namespace Clock.ViewModels;

public class AnalogClockViewModel : INotifyPropertyChanged
{
    private DateTime Time { get; set; }
    public double SecondsAngle => (Time.Second + Time.Millisecond / 1000.0) * 6;
    public double MinutesAngle => (Time.Minute + Time.Second / 60.0) * 6;
    public double HoursAngle => (Time.Hour + Time.Minute / 60.0) * 30;
    public bool ShowSeconds { get; set; }
    public double? TimeZone { get; set; }
    
    private readonly DispatcherTimer timer;
    public event PropertyChangedEventHandler? PropertyChanged;

    public AnalogClockViewModel(ClockSettings clockSettings)
    {
        ShowSeconds = clockSettings.ShowSeconds;
        TimeZone = clockSettings.TimeZone;
        timer = new DispatcherTimer { Interval = GetTimerInterval() };
        timer.Tick += (_, _) => Tick();
        timer.Start();
        Tick();
    }

    private void Tick()
    {
        Time = TimeZone.HasValue 
            ? DateTime.UtcNow.AddHours(TimeZone.Value) 
            : DateTime.Now;
        Update(nameof(SecondsAngle));
        Update(nameof(MinutesAngle));
        Update(nameof(HoursAngle));
    }

    private TimeSpan GetTimerInterval() => ShowSeconds ? TimeSpan.FromSeconds(1d / 10) : TimeSpan.FromSeconds(5);

    private void Update(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}