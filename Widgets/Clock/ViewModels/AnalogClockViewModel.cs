using Avalonia.Threading;
using Clock.Models;
using ReactiveUI;
using uWidgets.Services;

namespace Clock.ViewModels;

public class AnalogClockViewModel : ReactiveObject, IDisposable
{
    private readonly ClockModel clockModel;
    private readonly UpdateTimer timer;

    public AnalogClockViewModel(ClockModel clockModel)
    {
        this.clockModel = clockModel;

        timer = clockModel.ShowSeconds ? TimerService.Timer100Ms : TimerService.Timer5Seconds;
        timer.Subscribe(UpdateTime);
        
        UpdateTime();
    }
    
    public void Dispose()
    {
        timer.Unsubscribe(UpdateTime);
        GC.SuppressFinalize(this);
    }

    private void UpdateTime()
    {
        var time = Time;

        HourHand = new ClockHandViewModel(GetHoursAngle(time), 190, false);
        MinuteHand = new ClockHandViewModel(GetMinutesAngle(time), 365, false);
        SecondHand = new ClockHandViewModel(GetSecondsAngle(time), 460, true, clockModel.ShowSeconds);
    }

    private DateTime Time => clockModel.TimeZone.HasValue 
        ? DateTime.UtcNow.AddHours(clockModel.TimeZone.Value) 
        : DateTime.Now;

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

    private static double GetSecondsAngle(DateTime time) => (time.Second + time.Millisecond / 1000.0) * 6;
    private static double GetMinutesAngle(DateTime time) => (time.Minute + time.Second / 60.0) * 6;
    private static double GetHoursAngle(DateTime time) => (time.Hour % 12 + time.Minute / 60.0) * 30;
}