using System.ComponentModel;
using Avalonia.Threading;
using Clock.SettingsModels;

namespace Clock.ViewModels;

public class AnalogClockViewModel : INotifyPropertyChanged
{
    public ClockHandViewModel? HourHand { get; set; }
    public ClockHandViewModel? MinuteHand { get; set; }
    public ClockHandViewModel? SecondHand { get; set; }
    private double SecondsAngle => (Time.Second + Time.Millisecond / 1000.0) * 6;
    private double MinutesAngle => (Time.Minute + Time.Second / 60.0) * 6;
    private double HoursAngle => (Time.Hour + Time.Minute / 60.0) * 30;
    private DateTime Time { get; set; }
    private bool ShowSeconds { get; set; }
    private double? TimeZone { get; set; }
    
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
        
        HourHand = new ClockHandViewModel(HoursAngle, 190, false);
        MinuteHand = new ClockHandViewModel(MinutesAngle, 365, false);
        SecondHand = new ClockHandViewModel(SecondsAngle, 460, true);
        Update(nameof(HourHand));
        Update(nameof(MinuteHand));
        Update(nameof(SecondHand));
    }

    private TimeSpan GetTimerInterval() => ShowSeconds 
        ? TimeSpan.FromSeconds(1d / 10) 
        : TimeSpan.FromSeconds(5);

    private void Update(string propertyName) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}