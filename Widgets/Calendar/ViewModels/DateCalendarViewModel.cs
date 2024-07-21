using Avalonia.Threading;
using ReactiveUI;

namespace Calendar.ViewModels;

public class DateCalendarViewModel : ReactiveObject, IDisposable
{
    private readonly DispatcherTimer timer;
    
    public DateCalendarViewModel()
    {
        timer = new DispatcherTimer { Interval = GetInitialTimerInterval(DateTime.Now) };
        timer.Tick += Tick;
        timer.Start();
        UpdateTime();
    }
    
    public void Dispose()
    {
        timer.Stop();
        timer.Tick -= Tick;
    }
    
    private void Tick(object? sender, EventArgs e)
    {
        timer.Interval = GetTimerInterval();
        UpdateTime();
    }

    private void UpdateTime()
    {
        var now = DateTime.Now;
        var format = Thread.CurrentThread.CurrentUICulture.DateTimeFormat;
        
        Month = format.GetAbbreviatedMonthName(now.Month);
        DayOfWeek = format.GetAbbreviatedDayName(now.DayOfWeek).Replace(".", "");
        Day = now.Day.ToString();
    }
    
    private string? month;
    public string? Month 
    {
        get => month;
        private set => this.RaiseAndSetIfChanged(ref month, value);
    }

    private string? dayOfWeek;
    public string? DayOfWeek
    {
        get => dayOfWeek;
        private set => this.RaiseAndSetIfChanged(ref dayOfWeek, value);
    }

    private string? day;
    public string? Day
    {
        get => day;
        private set => this.RaiseAndSetIfChanged(ref day, value);
    }

    private TimeSpan GetInitialTimerInterval(DateTime now) => TimeSpan.FromHours(24) - now.TimeOfDay;
    private TimeSpan GetTimerInterval() => TimeSpan.FromHours(24);
}