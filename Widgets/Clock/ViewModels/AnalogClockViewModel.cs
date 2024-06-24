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
    
    private readonly DispatcherTimer timer;
    public event PropertyChangedEventHandler? PropertyChanged;

    public AnalogClockViewModel(ClockSettings clockSettings)
    {
        ShowSeconds = clockSettings.ShowSeconds;
        timer = new DispatcherTimer { Interval = GetTimerInterval() };
        timer.Tick += (_, _) =>
        {
            Time = DateTime.Now;
            Update(nameof(SecondsAngle));
            Update(nameof(MinutesAngle));
            Update(nameof(HoursAngle));
        };
        timer.Start();
    }

    private TimeSpan GetTimerInterval() => ShowSeconds ? TimeSpan.FromSeconds(1d / 10) : TimeSpan.FromSeconds(5);

    protected virtual void Update(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}