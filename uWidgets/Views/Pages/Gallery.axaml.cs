using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using uWidgets.Core;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;
using uWidgets.Locales;
using uWidgets.ViewModels;

namespace uWidgets.Views.Pages;

public partial class Gallery : UserControl
{
    private readonly IAppSettingsProvider appSettingsProvider;
    private readonly ILayoutProvider layoutProvider;
    private readonly IAssemblyProvider assemblyProvider;
    private readonly AssemblyInfo assemblyInfo;
    private readonly IWidgetFactory<Window, UserControl> widgetFactory;
    public List<WidgetPreviewViewModel> Widgets => GetWidgets();
    public int WidgetSize => 160;
    public CornerRadius Radius => new(appSettingsProvider.Get().Dimensions.Radius / (VisualRoot?.RenderScaling ?? 1.0));

    public Gallery(IAppSettingsProvider appSettingsProvider, ILayoutProvider layoutProvider, IAssemblyProvider assemblyProvider, 
        AssemblyInfo assemblyInfo, IWidgetFactory<Window, UserControl> widgetFactory)
    {
        this.appSettingsProvider = appSettingsProvider;
        this.layoutProvider = layoutProvider;
        this.assemblyProvider = assemblyProvider;
        this.assemblyInfo = assemblyInfo;
        this.widgetFactory = widgetFactory;
        DataContext = this;
        Unloaded += OnUnloaded;
        
        InitializeComponent();
    }

    private List<WidgetPreviewViewModel> GetWidgets()
    {
        var assembly = assemblyProvider
            .LoadAssembly(assemblyInfo.AssemblyName.Name!);

        var resources = (ResourceManager?) assembly
            .GetTypes()
            .FirstOrDefault(type => type.Name == nameof(Locale))?
            .GetProperty(nameof(ResourceManager), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)?
            .GetValue(null);

        return assembly
            .GetCustomAttributes<WidgetInfoAttribute>()
            .Select(widgetInfo => new WidgetPreviewViewModel(
                widgetFactory.CreateControl(widgetInfo.ViewType),
                assemblyInfo.AssemblyName.Name!,
                widgetInfo.ViewType.Name,
                resources?.GetString(widgetInfo.Title ?? string.Empty),
                resources?.GetString(widgetInfo.Subtitle ?? string.Empty)
            ))
            .ToList();
    }
    
    private void OnUnloaded(object? sender, RoutedEventArgs e)
    {
        if (layoutProvider.Get().All(x => x.Type != assemblyInfo.AssemblyName.Name!))
        {
            assemblyProvider.UnloadAssembly(assemblyInfo.AssemblyName.Name!);
        }
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var viewModel = button!.DataContext as WidgetPreviewViewModel;
        var dimensions = appSettingsProvider.Get().Dimensions;
        var size = 2 * dimensions.Size + dimensions.Margin;
        var position = button!.PointToScreen(new Point(0, 0));

        var widgetSettings = new WidgetLayout(viewModel!.Type, viewModel.Subtype, position.X, position.Y, size, size, null);
        widgetFactory.Add(widgetSettings).Show();
    }
}