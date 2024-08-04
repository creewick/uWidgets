namespace Calendar.ViewModels;

public record DayViewModel(string? Day = null, bool IsWeekend = false, bool IsToday = false);