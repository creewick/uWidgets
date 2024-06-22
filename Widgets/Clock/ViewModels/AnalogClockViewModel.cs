using System.ComponentModel;
using Avalonia.Threading;
using uWidgets.Core.Interfaces;

namespace Clock.ViewModels;

public class AnalogClockViewModel : INotifyPropertyChanged
{
    public DateTime Time { get; set; }
    public double SecondsAngle => (Time.Second + Time.Millisecond / 1000.0) * 6;
    public double MinutesAngle => (Time.Minute + Time.Second / 60.0) * 6;
    public double HoursAngle => (Time.Hour + Time.Minute / 60.0) * 30;
    // public Visibility SecondsVisibility => clockSettings.ShowSeconds ? Visibility.Visible : Visibility.Collapsed;
    
    // private ClockSettings clockSettings;
    private readonly DispatcherTimer timer;
    public event PropertyChangedEventHandler? PropertyChanged;

    public AnalogClockViewModel(IAppSettingsProvider appSettings, IWidgetSettingsProvider widgetSettings)
    {
        timer = new DispatcherTimer { Interval = GetTimerInterval() };
        timer.Tick += (_, _) =>
        {
            Time = DateTime.Now;
            Update(nameof(SecondsAngle));
            Update(nameof(MinutesAngle));
            Update(nameof(HoursAngle));
        };
        timer.Start();
    }

    private TimeSpan GetTimerInterval()
    {
        return TimeSpan.FromSeconds(1d / 10);
    }

    protected virtual void Update(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}