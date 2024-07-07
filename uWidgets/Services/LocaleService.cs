using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using Avalonia.Markup.Xaml;
using uWidgets.Core.Interfaces;
using uWidgets.Locales;

namespace uWidgets.Services;

public class LocaleService : MarkupExtension
{
    private static IAssemblyProvider? AssemblyProvider { get; set; }
    public string? Key { get; set; }
    public string? Assembly { get; set; }

    public static void Initialize(IAssemblyProvider assemblyProvider)
    {
        AssemblyProvider = assemblyProvider;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (string.IsNullOrEmpty(Key)) 
            return string.Empty;

        var resourceManager = string.IsNullOrEmpty(Assembly)
            ? Locale.ResourceManager
            : GetResourceManager(Assembly);
        
        return resourceManager.GetString(Key) 
               ?? string.Empty;
    }
    
    public static void SetCulture(string cultureName)
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        Locale.Culture = Thread.CurrentThread.CurrentUICulture;
    }

    private static ResourceManager GetResourceManager(string assemblyName)
    {
        var assembly = AssemblyProvider!.LoadAssembly(assemblyName);
        var baseName = AssemblyProvider.GetLocaleBaseName(assembly);

        if (baseName == null)
            throw new NullReferenceException($"LocaleAttribute is not defined in assembly {assemblyName}");

        return new ResourceManager(baseName, assembly);
    }
}