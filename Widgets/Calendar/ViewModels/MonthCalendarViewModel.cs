using Avalonia.Threading;
using ReactiveUI;

namespace Calendar.ViewModels;

public class MonthCalendarViewModel : ReactiveObject
{
    private string? month;
    public string? Month 
    {
        get => month;
        private set => this.RaiseAndSetIfChanged(ref month, value);
    }

    private List<DayViewModel>? days;
    public List<DayViewModel>? Days
    {
        get => days;
        private set => this.RaiseAndSetIfChanged(ref days, value);
    }
    
    private readonly DispatcherTimer timer;
    
    public MonthCalendarViewModel()
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
        
        Month = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetMonthName(now.Month).ToUpper();
        Days = GetWeekDays().Concat(GetEmptyDays(now)).Concat(GetDaysOfMonth(now)).ToList();
    }

    private static IEnumerable<DayViewModel> GetDaysOfMonth(DateTime now)
    {
        return Enumerable
            .Range(1, DateTime.DaysInMonth(now.Year, now.Month))
            .Select(day => new DayViewModel(
                day.ToString(),
                IsWeekend(new DateTime(now.Year, now.Month, day).DayOfWeek),
                day == now.Day));
    }

    private static IEnumerable<DayViewModel> GetEmptyDays(DateTime now)
    {
        var count = ((int)new DateTime(now.Year, now.Month, 1).DayOfWeek + 6) % 7;
        
        return Enumerable.Range(0, count).Select(_ => new DayViewModel());
    }

    private static IEnumerable<DayViewModel> GetWeekDays()
    {
        return Enumerable.Range(0, 7).Select(i => new DayViewModel(Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetShortestDayName((DayOfWeek)((i + 1) % 7))));
    }
    
    private static bool IsWeekend(DayOfWeek dayOfWeek) => dayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    
    private TimeSpan GetInitialTimerInterval(DateTime now) => TimeSpan.FromHours(24) - now.TimeOfDay;
    private TimeSpan GetTimerInterval() => TimeSpan.FromHours(24);
}