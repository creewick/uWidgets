using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;

namespace uWidgets.Core.Services;

public class WidgetSettingsProvider(ILayoutProvider layoutProvider, WidgetSettings settings) 
    : IWidgetSettingsProvider
{
    public event EventHandler<WidgetSettings>? DataChanged;
    public WidgetSettings Get() => settings;

    public void Save(WidgetSettings data)
    {
        DataChanged?.Invoke(this, data);
        var layout = layoutProvider.Get();
        var index = layout.IndexOf(settings);

        if (index == -1) return;
        
        layout[index] = data;
        layoutProvider.Save(layout);
        settings = data;
    }
}