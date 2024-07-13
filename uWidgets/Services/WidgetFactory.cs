using System;
using System.Collections.Generic;
using System.Text.Json;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Services;
using uWidgets.Views;

namespace uWidgets.Services;

public class WidgetFactory(IAssemblyProvider assemblyProvider, ILayoutProvider layoutProvider, IServiceProvider serviceProvider) : IWidgetFactory<Widget>
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

            if (settingsType != null && widgetSettings.Settings.HasValue)
            {
                var settings = widgetSettings.Settings.Value.Deserialize(settingsType) 
                    ?? throw new FormatException($"Can't deserialize {settingsType.Name}");
                args.Add(settings);
            }
            var userControl = assemblyProvider.Activate(assembly, controlType, args.ToArray());

            var widget = (Widget) ActivatorUtilities.CreateInstance(serviceProvider, typeof(Widget), widgetSettingsProvider);
           
            widget.Content = userControl;

            yield return widget;
        }
    }
}