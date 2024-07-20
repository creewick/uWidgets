using Avalonia.Media;

namespace Clock.ViewModels;

public record ClockHandViewModel(double RotateAngle, int Length, bool IsSecondHand, bool ShowSeconds = true)
{
    private DateTime Time { get; set; }
    public bool IsVisible => !IsSecondHand || ShowSeconds;
    public string Text => Time.ToString("hh:mm"); 
    public PathGeometry Geometry => PathGeometry.Parse($"M 500,420 V {420 - Length}");
    public bool IsHourMinuteHand => !IsSecondHand;
}