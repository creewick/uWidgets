using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;

namespace uWidgets.Core.Services;

public class WidgetSettingsProvider(ILayoutProvider layoutProvider, WidgetSettings settings) 
    : IWidgetSettingsProvider
{
    public Task<WidgetSettings> Get() => Task.FromResult(settings);

    public async Task Save(WidgetSettings data)
    {
        var layout = await layoutProvider.Get();
        var index = layout.IndexOf(data);

        if (index > -1)
        {
            layout[index] = data;
            await layoutProvider.Save(layout);
        }
    }
}