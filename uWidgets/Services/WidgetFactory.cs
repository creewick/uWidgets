using System.Collections.Generic;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Services;

namespace uWidgets.Services;

public class WidgetFactory(IAssemblyProvider assemblyProvider, ILayoutProvider layoutProvider) : IWidgetFactory<Widget>
{
    public IEnumerable<Widget> Create()
    {
        foreach (var widgetSettings in layoutProvider.Get())
        {
            var widgetSettingsProvider = new WidgetSettingsProvider(layoutProvider, widgetSettings);

            yield return (Widget) assemblyProvider.Activate(
                widgetSettings.Type,
                widgetSettings.SubType,
                typeof(Widget),
                widgetSettingsProvider);
        }
    }
}