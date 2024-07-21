using Clock.Models;

namespace Clock.ViewModels;

public class WorldClockViewModel(WorldClockModel worldClockModel) : IDisposable
{
    private readonly List<AnalogClockViewModel> viewModels = Enumerable
        .Range(0, 4)
        .Select(i =>
            new AnalogClockViewModel(new ClockModel(
                false, 
                false, 
                worldClockModel.TimeZones.ElementAtOrDefault(i))))
        .ToList();

    public AnalogClockViewModel? First => viewModels[0];
    public AnalogClockViewModel? Second => viewModels[1];
    public AnalogClockViewModel? Third => viewModels[2];
    public AnalogClockViewModel? Fourth => viewModels[3];

    public void Dispose() => viewModels.ForEach(x => x.Dispose());
}