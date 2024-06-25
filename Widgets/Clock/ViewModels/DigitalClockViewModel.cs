using System.ComponentModel;
using Avalonia.Threading;
using Clock.Models;

namespace Clock.ViewModels;

public class DigitalClockViewModel : INotifyPropertyChanged
{
    private readonly ClockModel clockModel;
    private DateTime Time { get; set; }
    private string HH => clockModel.Use24Hours ? "HH" : "hh";
    private string SS => clockModel.ShowSeconds ? ":ss" : "";
    private string AM => clockModel.Use24Hours ? "" : " tt";
    public string Text => Time.ToString($"{HH}:mm{SS}{AM}");
    
    private readonly DispatcherTimer timer;
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public DigitalClockViewModel(ClockModel clockModel)
    {
        this.clockModel = clockModel;

        timer = new DispatcherTimer { Interval = GetInitialTimerInterval(DateTime.Now) };
        timer.Tick += (_, _) => Tick();
        timer.Start();
        Tick();
    }
    
    private void Tick()
    {
        timer.Interval = GetTimerInterval();
        
        Time = clockModel.TimeZone.HasValue 
            ? DateTime.UtcNow.AddHours(clockModel.TimeZone.Value) 
            : DateTime.Now;
        
        Update(nameof(Text));
    }

    private TimeSpan GetInitialTimerInterval(DateTime now) => clockModel.ShowSeconds
        ? TimeSpan.FromMilliseconds(1000 - now.Millisecond)
        : TimeSpan.FromSeconds(60 - now.Second);
    
    private TimeSpan GetTimerInterval() => clockModel.ShowSeconds 
        ? TimeSpan.FromSeconds(1)
        : TimeSpan.FromSeconds(60);
    
    private void Update(string propertyName) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}