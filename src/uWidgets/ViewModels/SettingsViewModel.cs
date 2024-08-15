<<<<<<< HEAD
﻿using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;
using uWidgets.Core;
using uWidgets.Core.Interfaces;
using uWidgets.Locales;
using uWidgets.Views.Pages;

namespace uWidgets.ViewModels;

public class SettingsViewModel(IAppSettingsProvider appSettingsProvider, IAssemblyProvider assemblyProvider, 
    ILayoutProvider layoutProvider, IWidgetFactory<Window, UserControl> widgetFactory) : ReactiveObject
{
    private UserControl? currentPage;
    public UserControl? CurrentPage
    {
        get => currentPage;
        set => this.RaiseAndSetIfChanged(ref currentPage, value);
    }

    private string? currentPageTitle;
    public string? CurrentPageTitle
    {
        get => currentPageTitle;
        set => this.RaiseAndSetIfChanged(ref currentPageTitle, value);
    }

    public PageViewModel[] AllItems => MenuItems.Concat(widgetItems).ToArray();

    public static readonly PageViewModel[] MenuItems =
    [
        new PageViewModel(typeof(General), GetIcon(nameof(General)), Locale.Settings_General),
        new PageViewModel(typeof(Appearance), GetIcon(nameof(Appearance)), Locale.Settings_Appearance),
        new PageViewModel(typeof(Advanced), GetIcon(nameof(Advanced)), Locale.Settings_Advanced),
        new PageViewModel(typeof(About), GetIcon(nameof(About)),  Locale.Settings_About),
        new PageViewModel(null, null, null)
    ];

    private readonly PageViewModel[] widgetItems =
     assemblyProvider
            .GetAssemblyInfos(Const.WidgetsFolder)
            .ToDictionary(
                group => group.Key, 
                group => group.MaxBy(assembly => assembly.Version)!)
            .Select(assembly => new PageViewModel(
                    typeof(Gallery), StreamGeometry.Parse(assembly.Value.IconData), assembly.Value.DisplayName, assembly.Value 
                    ))
            .OrderBy(page => page.Text)
            .ToArray();

    private static StreamGeometry? GetIcon(string name) =>
        (StreamGeometry?)(Application.Current!.TryFindResource(name, out var icon) ? icon : null);

    public void SetCurrentPage(PageViewModel? value)
    {
        if (value?.Type == null) return;
        CurrentPage = value.AssemblyInfo == null
            ? (UserControl?)Activator.CreateInstance(value.Type, appSettingsProvider)
            : new Gallery(appSettingsProvider, layoutProvider, assemblyProvider, value.AssemblyInfo, widgetFactory);
        CurrentPageTitle = value?.Text;
    }
=======
﻿using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;
using uWidgets.Core;
using uWidgets.Core.Interfaces;
using uWidgets.Locales;
using uWidgets.Views.Pages;

namespace uWidgets.ViewModels;

public class SettingsViewModel(IAppSettingsProvider appSettingsProvider, IAssemblyProvider assemblyProvider, 
    ILayoutProvider layoutProvider, IWidgetFactory<Window, UserControl> widgetFactory) : ReactiveObject
{
    private UserControl? currentPage;
    public UserControl? CurrentPage
    {
        get => currentPage;
        set => this.RaiseAndSetIfChanged(ref currentPage, value);
    }

    private string? currentPageTitle;
    public string? CurrentPageTitle
    {
        get => currentPageTitle;
        set => this.RaiseAndSetIfChanged(ref currentPageTitle, value);
    }

    public PageViewModel[] AllItems => MenuItems.Concat(widgetItems).ToArray();

    public static readonly PageViewModel[] MenuItems =
    [
        new PageViewModel(typeof(General), GetIcon(nameof(General)), Locale.Settings_General),
        new PageViewModel(typeof(Appearance), GetIcon(nameof(Appearance)), Locale.Settings_Appearance),
        new PageViewModel(typeof(Advanced), GetIcon(nameof(Advanced)), Locale.Settings_Advanced),
        new PageViewModel(typeof(About), GetIcon(nameof(About)),  Locale.Settings_About),
        new PageViewModel(null, null, null)
    ];

    private readonly PageViewModel[] widgetItems =
     assemblyProvider
            .GetAssemblyInfos(Const.WidgetsFolder)
            .ToDictionary(
                group => group.Key, 
                group => group.MaxBy(assembly => assembly.Version)!)
            .Select(assembly => new PageViewModel(
                    typeof(Gallery), StreamGeometry.Parse(assembly.Value.IconData), assembly.Value.DisplayName, assembly.Value 
                    ))
            .OrderBy(page => page.Text)
            .ToArray();

    private static StreamGeometry? GetIcon(string name) =>
        (StreamGeometry?)(Application.Current!.TryFindResource(name, out var icon) ? icon : null);

    public void SetCurrentPage(PageViewModel? value)
    {
        if (value?.Type == null) return;
        CurrentPage = value.AssemblyInfo == null
            ? (UserControl?)Activator.CreateInstance(value.Type, appSettingsProvider)
            : new Gallery(appSettingsProvider, layoutProvider, assemblyProvider, value.AssemblyInfo, widgetFactory);
        CurrentPageTitle = value?.Text;
    }
>>>>>>> parent of 15524c5 (Delete src directory)
}