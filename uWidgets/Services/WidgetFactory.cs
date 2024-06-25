using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia.Controls;
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
            var assembly = assemblyProvider.LoadAssembly(widgetSettings.Type);
            var controlType = assemblyProvider.GetType(assembly, widgetSettings.SubType, typeof(UserControl));
            var settingsType = assemblyProvider.GetWidgetModelType(assembly, controlType);
            var args = new List<object>();

            if (settingsType != null && widgetSettings.Model.HasValue)
            {
                var settings = widgetSettings.Model.Value.Deserialize(settingsType) 
                    ?? throw new FormatException($"Can't deserialize {settingsType.Name}");
                args.Add(settings);
            }
            var userControl = assemblyProvider.Activate(assembly, controlType, args.ToArray());

            yield return new Widget(widgetSettingsProvider)
            {
                Content = userControl
            };
        }
    }
}