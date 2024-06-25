using Avalonia.Media;

namespace Clock.ViewModels;

public record ClockHandViewModel(double RotateAngle, int Length, bool IsSecondHand)
{
    public PathGeometry Geometry => PathGeometry.Parse($"M 500,420 V {420 - Length}");
    public bool IsHourMinuteHand => !IsSecondHand;
}