using Clock.Models;

namespace Clock.ViewModels;

public class WorldClockViewModel(WorldClockModel worldClockModel)
{
    public AnalogClockViewModel First => GetViewModel(worldClockModel, 0);
    public AnalogClockViewModel Second => GetViewModel(worldClockModel, 1);
    public AnalogClockViewModel Third => GetViewModel(worldClockModel, 2);
    public AnalogClockViewModel Fourth => GetViewModel(worldClockModel, 3);
    
    private static AnalogClockViewModel GetViewModel(WorldClockModel worldClockModel, int index) => new(
        new ClockModel(false, false,
            worldClockModel.TimeZones.Count > index
                ? worldClockModel.TimeZones[index]
                : null));
}