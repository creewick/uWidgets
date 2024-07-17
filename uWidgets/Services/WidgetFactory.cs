using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;
using uWidgets.Core.Services;
using uWidgets.Views;

namespace uWidgets.Services;

public class WidgetFactory(IAssemblyProvider assemblyProvider, ILayoutProvider layoutProvider) : IWidgetFactory<Window, UserControl>
{
    public IEnumerable<Window> Create() => layoutProvider.Get().Select(CreateInternal);

    public UserControl CreateWidgetControl(Type type, object? model)
    {
        return model != null 
            ? (UserControl) assemblyProvider.Activate(type, model)
            : (UserControl) assemblyProvider.Activate(type);
    }
    
    public Window Create(WidgetSettings widgetSettings)
    {
        layoutProvider.Save(layoutProvider.Get().Append(widgetSettings).ToList());
        
        return CreateInternal(widgetSettings);
    }

    private Window CreateInternal(WidgetSettings widgetSettings)
    {
        var widgetSettingsProvider = new WidgetSettingsProvider(layoutProvider, widgetSettings);
        var assembly = assemblyProvider.LoadAssembly(widgetSettings.Type);
        var widgetType = GetWidgetType(assembly, widgetSettings.SubType);
        var widgetInfo = GetWidgetInfo(assembly, widgetType);
        
        var widget = (Widget) assemblyProvider.Activate(typeof(Widget), widgetSettingsProvider, widgetInfo);
        widget.Content = CreateWidgetControl(widgetInfo.ViewType, widgetSettings.GetModel(widgetInfo.ModelType));
        
        return widget;
    }

    public Window CreateEditWidgetWindow(Type type, IWidgetSettingsProvider widgetSettingsProvider)
    {
        return new EditWidget(widgetSettingsProvider)
        {
            Content = (UserControl) assemblyProvider.Activate(type, widgetSettingsProvider)
        };
    }
    
    private static Type GetWidgetType(Assembly assembly, string typeName)
    {
        var type = assembly
            .GetTypes()
            .SingleOrDefault(type => type.IsAssignableTo(typeof(UserControl)) && typeName == type.Name);
        
        if (type == null)
            throw new ArgumentException($"No suitable class {typeName} found in assembly {assembly.FullName}");

        return type;
    }

    private static WidgetInfoAttribute GetWidgetInfo(Assembly assembly, Type controlType)
    {
        var widgetInfo = assembly
            .GetCustomAttributes<WidgetInfoAttribute>()
            .SingleOrDefault(attribute => attribute.ViewType == controlType);
        
        if (widgetInfo == null) 
            throw new ArgumentException($"No suitable WidgetInfoAttribute found for {controlType.Name}");

        return widgetInfo;
    }
}