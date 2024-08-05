using ReactiveUI;
using uWidgets.Core.Interfaces;

namespace uWidgets.ViewModels;

public class AdvancedViewModel(IAppSettingsProvider appSettingsProvider) : ReactiveObject
{
    public int Size
    {
        get => appSettingsProvider.Get().Dimensions.Size;
        set
        {
            var settings = appSettingsProvider.Get();
            var dimensions = settings.Dimensions with { Size = value };
            var newSettings = settings with { Dimensions = dimensions };
            appSettingsProvider.Save(newSettings);
        }
    }
    
    public int Margin
    {
        get => appSettingsProvider.Get().Dimensions.Margin;
        set
        {
            var settings = appSettingsProvider.Get();
            var dimensions = settings.Dimensions with { Margin = value };
            var newSettings = settings with { Dimensions = dimensions };
            appSettingsProvider.Save(newSettings);
        }
    }
    
    public int Radius
    {
        get => appSettingsProvider.Get().Dimensions.Radius;
        set
        {
            var settings = appSettingsProvider.Get();
            var dimensions = settings.Dimensions with { Radius = value };
            var newSettings = settings with { Dimensions = dimensions };
            appSettingsProvider.Save(newSettings);
        }
    }
}