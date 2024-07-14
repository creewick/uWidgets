using Avalonia;
using Avalonia.Media;

namespace Calendar.ViewModels;

public record DayViewModel(string? Day = null, bool IsWeekend = false, bool IsToday = false)
{
    public double Opacity => IsWeekend && !IsToday ? 0.5 : 1;

    public SolidColorBrush Foreground => IsToday
        ? new SolidColorBrush(Colors.White)
        : new SolidColorBrush((Color)Application.Current!.Resources["SystemBaseHighColor"]!);
}