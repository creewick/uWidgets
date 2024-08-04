using ReactiveUI;
using uWidgets.Services;

namespace Calendar.ViewModels;

public class DateCalendarViewModel : ReactiveObject, IDisposable
{
    private DateTime currentDate;
    
    public DateCalendarViewModel()
    {
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
}