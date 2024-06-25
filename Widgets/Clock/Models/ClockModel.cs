namespace Clock.Models;

public record ClockModel(
    bool ShowSeconds = false, 
    bool Use24Hours = false, 
    double? TimeZone = null);
