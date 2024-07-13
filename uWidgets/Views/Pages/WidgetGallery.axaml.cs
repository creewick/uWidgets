using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using Avalonia;
using Avalonia.Controls;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;
using uWidgets.Locales;
using uWidgets.ViewModels;

namespace uWidgets.Views.Pages;

public partial class WidgetGallery : UserControl
{
    private readonly IAppSettingsProvider appSettingsProvider;
    private readonly IAssemblyProvider assemblyProvider;
    private readonly AssemblyInfo assemblyInfo;
    public List<WidgetPreviewViewModel> Widgets => GetWidgets();
    public int WidgetSize => appSettingsProvider.Get().Layout.WidgetSize * 2;
    public CornerRadius Radius => new(16);

    public WidgetGallery(IAppSettingsProvider appSettingsProvider, IAssemblyProvider assemblyProvider, AssemblyInfo assemblyInfo)
    {
        this.appSettingsProvider = appSettingsProvider;
        this.assemblyProvider = assemblyProvider;
        this.assemblyInfo = assemblyInfo;
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
                resources?.GetString(widgetInfo.Title ?? string.Empty),
                resources?.GetString(widgetInfo.Subtitle ?? string.Empty)
            ))
            .ToList();
    }
}