namespace Clock.Models;

public record ClockModel(
    bool ShowSeconds = false, 
    bool ShowDate = false,
    bool Use24Hours = false, 
    double? TimeZone = null);
