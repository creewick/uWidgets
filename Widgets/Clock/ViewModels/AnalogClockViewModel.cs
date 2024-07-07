using Avalonia.Threading;
using Clock.Models;
using ReactiveUI;

namespace Clock.ViewModels;

public class AnalogClockViewModel : ReactiveObject
{
    private readonly ClockModel clockModel;
    private double Diff => (Time - DateTime.Now).TotalMinutes / 60;
        
    public string TimeZoneDiff => clockModel.TimeZone == null 
        ? string.Empty 
        : Math.Abs(Math.Round(Diff) - Diff) > 0.1 
            ? $"{Diff:+0.0;-0.0;0}" 
            : $"{Diff:+0;-0;0}";
        
    private ClockHandViewModel? hourHand;
    public ClockHandViewModel? HourHand
    {
        get => hourHand;
        private set => this.RaiseAndSetIfChanged(ref hourHand, value);
    }

    private ClockHandViewModel? minuteHand;
    public ClockHandViewModel? MinuteHand
    {
        get => minuteHand;
        private set => this.RaiseAndSetIfChanged(ref minuteHand, value);
    }

    private ClockHandViewModel? secondHand;
    public ClockHandViewModel? SecondHand
    {
        get => secondHand;
        private set => this.RaiseAndSetIfChanged(ref secondHand, value);
    }

    private DateTime time;
    private DateTime Time
    {
        get => time;
        set => this.RaiseAndSetIfChanged(ref time, value);
    }

    private double SecondsAngle => (Time.Second + Time.Millisecond / 1000.0) * 6;
    private double MinutesAngle => (Time.Minute + Time.Second / 60.0) * 6;
    private double HoursAngle => (Time.Hour + Time.Minute / 60.0) * 30;
        
    private readonly DispatcherTimer timer;

    public AnalogClockViewModel(ClockModel clockModel)
    {
        this.clockModel = clockModel;
            
        timer = new DispatcherTimer { Interval = GetTimerInterval() };
        timer.Tick += (_, _) => Tick();
        timer.Start();
        Tick();
    }

    private void Tick()
    {
        Time = clockModel.TimeZone.HasValue 
            ? DateTime.UtcNow.AddHours(clockModel.TimeZone.Value) 
            : DateTime.Now;

        HourHand = new ClockHandViewModel(HoursAngle, 190, false);
        MinuteHand = new ClockHandViewModel(MinutesAngle, 365, false);
        SecondHand = new ClockHandViewModel(SecondsAngle, 460, true, clockModel.ShowSeconds);
    }

    private TimeSpan GetTimerInterval() => clockModel.ShowSeconds 
        ? TimeSpan.FromSeconds(1d / 10) 
        : TimeSpan.FromSeconds(5);
}