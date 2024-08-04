using System;

namespace uWidgets.Services;

public static class TimerService
{
    public static readonly UpdateTimer Timer100Ms = new(TimeSpan.FromMilliseconds(100));
    
    public static readonly UpdateTimer Timer1Second = new(TimeSpan.FromSeconds(1));
    
    public static readonly UpdateTimer Timer5Seconds = new(TimeSpan.FromSeconds(5));
    
    public static readonly UpdateTimer Timer1Minute = new(TimeSpan.FromMinutes(1));
    
    public static readonly UpdateTimer Timer5Minutes = new(TimeSpan.FromMinutes(5));
    
    public static readonly UpdateTimer Timer1Hour = new(TimeSpan.FromHours(1));

    public static readonly UpdateTimer Timer1Day = new(TimeSpan.FromDays(1));
}