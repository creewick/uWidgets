using System.Globalization;
using Avalonia.Threading;
using ReactiveUI;

namespace Calendar.ViewModels;

public class DateCalendarViewModel : ReactiveObject
{
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
    
    private readonly DispatcherTimer timer;
    
    public DateCalendarViewModel()
    {
        timer = new DispatcherTimer { Interval = GetInitialTimerInterval(DateTime.Now) };
        timer.Tick += (_, _) => Tick();
        timer.Start();
        Tick();
    }

    private void Tick()
    {
        var now = DateTime.Now;
        timer.Interval = GetTimerInterval();

        Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(now.Month);
        DayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(now.DayOfWeek).Replace(".", "");
        Day = now.Day.ToString();
    }

    private TimeSpan GetInitialTimerInterval(DateTime now) => TimeSpan.FromHours(24) - now.TimeOfDay;
    private TimeSpan GetTimerInterval() => TimeSpan.FromHours(24);
}