using Clock.Models;
using ReactiveUI;
using uWidgets.Services;

namespace Clock.ViewModels;

public class DigitalClockViewModel : ReactiveObject, IDisposable
{
    private readonly ClockModel clockModel;
    private readonly UpdateTimer timer;

    public DigitalClockViewModel(ClockModel clockModel)
    {
        this.clockModel = clockModel;
        timer = clockModel.ShowSeconds ? TimerService.Timer1Second : TimerService.Timer1Minute;
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
        Time = clockModel.TimeZone.HasValue 
            ? DateTime.UtcNow.AddHours(clockModel.TimeZone.Value) 
            : DateTime.Now;

        this.RaisePropertyChanged(nameof(TimeText));
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

    public string TimeText => Time.ToString($"{HH}:mm{SS}{AM}");
    public string DateText => Time.ToString("D", Thread.CurrentThread.CurrentUICulture); 
    public bool ShowDate => clockModel.ShowDate;
}