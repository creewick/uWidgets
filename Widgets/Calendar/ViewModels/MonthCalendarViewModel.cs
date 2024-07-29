using System.Globalization;
using Calendar.Models;
using ReactiveUI;
using uWidgets.Services;

namespace Calendar.ViewModels;

public class MonthCalendarViewModel : ReactiveObject, IDisposable
{
    private readonly MonthCalendarModel monthCalendarModel;
    private DateTime currentDate;
    
    public MonthCalendarViewModel(MonthCalendarModel monthCalendarModel)
    {
        this.monthCalendarModel = monthCalendarModel;
        TimerService.Timer5Minutes.Subscribe(UpdateTime);
        UpdateTime();
    }

    public void Dispose()
    {
        TimerService.Timer5Minutes.Unsubscribe(UpdateTime);
        GC.SuppressFinalize(this);
    }
    
    private void UpdateTime()
    {
        var now = DateTime.Now;
        var format = Thread.CurrentThread.CurrentUICulture.DateTimeFormat;

        if (currentDate.Date == now.Date) return;

        currentDate = now;
        Month = format.GetMonthName(now.Month).ToUpper();
        Days = GetWeekDays(format)
            .Concat(GetEmptyDays(now))
            .Concat(GetDaysOfMonth(now))
            .ToList();
    }
    
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
    
    private static IEnumerable<DayViewModel> GetDaysOfMonth(DateTime now)
    {
        return Enumerable
            .Range(1, DateTime.DaysInMonth(now.Year, now.Month))
            .Select(day => new DayViewModel(
                day.ToString(),
                IsWeekend(new DateTime(now.Year, now.Month, day).DayOfWeek),
                day == now.Day));
    }

    private IEnumerable<DayViewModel> GetEmptyDays(DateTime now)
    {
        var startOfMonthDayOfWeek = (int) new DateTime(now.Year, now.Month, 1).DayOfWeek;
        var firstDayOfWeek = (int) monthCalendarModel.FirstDayOfWeek;
        
        var count = (startOfMonthDayOfWeek + firstDayOfWeek + 5) % 7;
        
        return Enumerable.Range(0, count).Select(_ => new DayViewModel());
    }

    private IEnumerable<DayViewModel> GetWeekDays(DateTimeFormatInfo format)
    {
        var offset = (int)monthCalendarModel.FirstDayOfWeek;
        
        return Enumerable.Range(0, 7)
            .Select(i => (DayOfWeek)((i + offset) % 7))
            .Select(day => new DayViewModel(
                format.GetShortestDayName(day),
                IsWeekend(day)));
    }
    
    private static bool IsWeekend(DayOfWeek dayOfWeek) => dayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
}