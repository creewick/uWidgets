using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;
using uWidgets.Core.Services;
using uWidgets.Views;

namespace uWidgets.Services;

public class WidgetFactory(IAssemblyProvider assemblyProvider, ILayoutProvider layoutProvider, 
    IServiceProvider serviceProvider) : IWidgetFactory<Widget>
{
    public IEnumerable<Widget> Create() => layoutProvider.Get().Select(Create);

    public Widget Open(WidgetSettings widgetSettings)
    {
        var widget = Create(widgetSettings);
        layoutProvider.Save(layoutProvider.Get().Append(widgetSettings).ToList());
        widget.Show();
        
        return widget;
    }

    private Widget Create(WidgetSettings widgetSettings)
    {
        var widgetSettingsProvider = new WidgetSettingsProvider(layoutProvider, widgetSettings);
        var assembly = assemblyProvider.LoadAssembly(widgetSettings.Type);
        var controlType = assemblyProvider.GetType(assembly, widgetSettings.SubType, typeof(UserControl));
        var settingsType = GetWidgetModelType(assembly, controlType);
        var settings = settingsType != null && widgetSettings.Settings.HasValue
            ? widgetSettings.Settings.Value.Deserialize(settingsType)
              ?? throw new FormatException($"Can't deserialize {settingsType.Name}")
            : null;

        var userControl = settings != null
            ? assemblyProvider.Activate(assembly, controlType, settings)
            : assemblyProvider.Activate(assembly, controlType);

        var widget = (Widget) ActivatorUtilities.CreateInstance(serviceProvider, typeof(Widget), widgetSettingsProvider);
           
        widget.Content = userControl;

        return widget;
    }

    private static Type? GetWidgetModelType(Assembly assembly, Type controlType)
    {
        return assembly
            .GetCustomAttributes<WidgetInfoAttribute>()
            .SingleOrDefault(attribute => attribute.ViewType == controlType)
            ?.ModelType;
    }
}