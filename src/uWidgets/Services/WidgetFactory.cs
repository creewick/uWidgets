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

public class WidgetFactory(IAssemblyProvider assemblyProvider, ILayoutProvider layoutProvider) 
    : IWidgetFactory<Window, UserControl>
{
    public IEnumerable<Window> Create() => layoutProvider.Get().Select(CreateInternal);
    
    public Window Add(WidgetLayout widgetLayout)
    {
        layoutProvider.Save(layoutProvider.Get().Append(widgetLayout).ToList());
        
        return CreateInternal(widgetLayout);
    }

    public UserControl CreateControl(Type type)
    {
        var widgetLayoutProvider = new WidgetLayoutProvider(layoutProvider, null);
        return CreateWidgetControl(type, widgetLayoutProvider, null);
    }
    
    private Window CreateInternal(WidgetLayout widgetLayout)
    {
        var widgetLayoutProvider = new WidgetLayoutProvider(layoutProvider, widgetLayout);
        
        var assembly = assemblyProvider.LoadAssembly(widgetLayout.Type);
        var widgetInfo = GetWidgetInfo(assembly, widgetLayout.SubType);
        var widgetControl = () => CreateWidgetControl(widgetInfo.ViewType, widgetLayoutProvider, widgetLayoutProvider.Get().GetModel(widgetInfo.ModelType));
        var settingsWindow = () => (Settings) assemblyProvider.Activate(typeof(Settings));
        
        var editWidgetWindow = widgetInfo.EditModelViewType != null 
            ? () => CreateEditWidgetWindow(widgetLayoutProvider, widgetInfo.EditModelViewType)
            : (Func<EditWidget>?) null;
        
        if (editWidgetWindow != null)
            return (Widget) assemblyProvider.Activate(typeof(Widget), widgetLayoutProvider, widgetControl, settingsWindow, editWidgetWindow);
        return (Widget) assemblyProvider.Activate(typeof(Widget), widgetLayoutProvider, widgetControl, settingsWindow);
    }

    private UserControl CreateWidgetControl(Type type, WidgetLayoutProvider? widgetLayoutProvider, object? model)
    {
        List<object> args = [];
        
        if (NeedsWidgetLayoutProvider(type) && widgetLayoutProvider != null)
            args.Add(widgetLayoutProvider);
        
        if (model != null)
            args.Add(model);

        return (UserControl) assemblyProvider.Activate(type, args.ToArray());
    }

    private EditWidget CreateEditWidgetWindow(IWidgetLayoutProvider widgetLayoutProvider, Type type)
    {
        var control = (UserControl) assemblyProvider.Activate(type, widgetLayoutProvider);

        return new EditWidget(widgetLayoutProvider, control);
    }

    private bool NeedsWidgetLayoutProvider(Type type)
    {
        return type
            .GetConstructors()
            .Any(constructor => constructor
                .GetParameters()
                .Any(param => param.ParameterType == typeof(IWidgetLayoutProvider)));
    }

    private static WidgetInfoAttribute GetWidgetInfo(Assembly assembly, string typeName)
    {
        var widgetInfo = assembly
            .GetCustomAttributes<WidgetInfoAttribute>()
            .SingleOrDefault(attribute => attribute.ViewType.Name == typeName);
        
        if (widgetInfo == null) 
            throw new ArgumentException($"No suitable WidgetInfoAttribute found for {typeName}");

        return widgetInfo;
    }
}