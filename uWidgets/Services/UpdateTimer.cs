using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Threading;
using Microsoft.Win32;

namespace uWidgets.Services;

public class UpdateTimer : IDisposable
{
    private readonly DispatcherTimer timer;
    private readonly List<Action> subscribers = [];

    public UpdateTimer(TimeSpan interval)
    {
        timer = new DispatcherTimer { Interval = interval };
        timer.Tick += OnTimerTick;
        
        if (OperatingSystem.IsWindows())
            SystemEvents.SessionSwitch += SystemEventsOnSessionSwitch;
    }

    public void Subscribe(Action action)
    {
        lock (subscribers)
        {
            if (subscribers.Contains(action)) return;
            subscribers.Add(action);
            if (subscribers.Count > 0) timer.Start();
        }
    }

    public void Unsubscribe(Action action)
    {
        lock (subscribers)
        {
            if (!subscribers.Contains(action)) return;
            subscribers.Remove(action);
            if (subscribers.Count == 0) timer.Stop();
        }
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        lock (subscribers)
        {
            subscribers.ToList().ForEach(action => action());
        }
    }
    
    private void SystemEventsOnSessionSwitch(object sender, SessionSwitchEventArgs e)
    {
        if (OperatingSystem.IsWindows() && e.Reason != SessionSwitchReason.SessionUnlock) return;
        if (subscribers.Count > 0) OnTimerTick(this, EventArgs.Empty);
    }

    public void Dispose()
    {
        timer.Stop();
        timer.Tick -= OnTimerTick;
        if (OperatingSystem.IsWindows())
            SystemEvents.SessionSwitch -= SystemEventsOnSessionSwitch;
        GC.SuppressFinalize(this);
    }
}