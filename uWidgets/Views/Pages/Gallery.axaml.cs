using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;
using uWidgets.Locales;
using uWidgets.ViewModels;

namespace uWidgets.Views.Pages;

public partial class Gallery : UserControl
{
    private readonly IAppSettingsProvider appSettingsProvider;
    private readonly IAssemblyProvider assemblyProvider;
    private readonly AssemblyInfo assemblyInfo;
    private readonly IWidgetFactory<Widget> widgetFactory;
    public List<WidgetPreviewViewModel> Widgets => GetWidgets();
    public int WidgetSize => appSettingsProvider.Get().Layout.WidgetSize * 2;
    public CornerRadius Radius => new(16);

    public Gallery(IAppSettingsProvider appSettingsProvider, IAssemblyProvider assemblyProvider, 
        AssemblyInfo assemblyInfo, IWidgetFactory<Widget> widgetFactory)
    {
        this.appSettingsProvider = appSettingsProvider;
        this.assemblyProvider = assemblyProvider;
        this.assemblyInfo = assemblyInfo;
        this.widgetFactory = widgetFactory;
        DataContext = this;
        
        InitializeComponent();
    }

    public List<WidgetPreviewViewModel> GetWidgets()
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
                (UserControl) assemblyProvider.Activate(assembly, widgetInfo.ViewType),
                assemblyInfo.AssemblyName.Name!,
                widgetInfo.ViewType.Name,
                resources?.GetString(widgetInfo.Title ?? string.Empty),
                resources?.GetString(widgetInfo.Subtitle ?? string.Empty)
            ))
            .ToList();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var viewModel = button!.DataContext as WidgetPreviewViewModel;
        var layout = appSettingsProvider.Get().Layout;
        var size = 2 * layout.WidgetSize + layout.WidgetMargin;
        var position = button!.PointToScreen(new Point(0, 0));

        var widgetSettings = new WidgetSettings(viewModel!.Type, viewModel.Subtype, position.X, position.Y, size, size, null);
        widgetFactory.Open(widgetSettings);
    }
}