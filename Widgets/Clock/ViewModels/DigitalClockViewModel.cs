using Avalonia.Threading;
using Clock.Models;
using ReactiveUI;

namespace Clock.ViewModels;

public class DigitalClockViewModel : ReactiveObject, IDisposable
{
    private readonly ClockModel clockModel;
    private readonly DispatcherTimer timer;

    public DigitalClockViewModel(ClockModel clockModel)
    {
        this.clockModel = clockModel;
        timer = new DispatcherTimer { Interval = GetInitialTimerInterval(DateTime.Now) };
        timer.Tick += Tick;
        timer.Start();
        UpdateTime();
    }
    
    private void Tick(object? sender, EventArgs e)
    {
        timer.Interval = GetTimerInterval();
        UpdateTime();
    }

    private void UpdateTime()
    {
        Time = clockModel.TimeZone.HasValue 
            ? DateTime.UtcNow.AddHours(clockModel.TimeZone.Value) 
            : DateTime.Now;

        this.RaisePropertyChanged(nameof(Text));
    }

    private DateTime time;
    public DateTime Time
    {
        get => time;
        private set => this.RaiseAndSetIfChanged(ref time, value);
    }

    private string HH => clockModel.Use24Hours ? "HH" : "hh";
    private string SS => clockModel.ShowSeconds ? ":ss" : "";
    private string AM => clockModel.Use24Hours ? "" : " tt";

    public string Text => Time.ToString($"{HH}:mm{SS}{AM}");

    private TimeSpan GetInitialTimerInterval(DateTime now) => clockModel.ShowSeconds
        ? TimeSpan.FromMilliseconds(1000 - now.Millisecond)
        : TimeSpan.FromSeconds(60 - now.Second);

    private TimeSpan GetTimerInterval() => clockModel.ShowSeconds 
        ? TimeSpan.FromSeconds(1)
        : TimeSpan.FromSeconds(60);

    public void Dispose()
    {
        timer.Stop();
        timer.Tick -= Tick;
    }
}