using System.Reflection;
using System.Resources;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyInjection;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;
using uWidgets.Core.Models.Attributes;

namespace uWidgets.Core.Services;
 
/// <inheritdoc />
public class AssemblyProvider : IAssemblyProvider
{
    private readonly Dictionary<string, AssemblyLoadContext> loadedContexts = new();
    private ILookup<string, AssemblyInfo> assemblyCache;
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="AssemblyProvider"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    public AssemblyProvider(IServiceProvider serviceProvider)
    {
        assemblyCache = GetAssemblyInfos(Const.WidgetsFolder);
        this.serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public ILookup<string, AssemblyInfo> GetAssemblyInfos(string directoryPath)
    {
        var assemblies = Directory.Exists(directoryPath)
            ? Directory
                .GetFiles(directoryPath, "*.dll")
                .Select(GetAssemblyInfo)
                .Where(info => info != null)
                .Cast<AssemblyInfo>()
            : [];
        
        return assemblies
            .ToLookup(assembly => assembly.AssemblyName);
    }

    private AssemblyInfo? GetAssemblyInfo(string filePath)
    {
        try
        {
            var context = new AssemblyLoadContext(filePath, true);
            var assembly = context.LoadFromAssemblyPath(filePath);
            var localeAttribute = assembly.GetCustomAttributes<LocaleAttribute>().FirstOrDefault();
            var companyAttribute = assembly.GetCustomAttributes<AssemblyCompanyAttribute>().FirstOrDefault();
            var widgetAttributes = assembly.GetCustomAttributes<WidgetInfoAttribute>();

            if (!widgetAttributes.Any()) return null;

            var assemblyName = assembly.GetName().Name!;
            var version = assembly.GetName().Version!;
            var company = companyAttribute?.Company ?? "";
            var locale = GetLocaleResourceManager(assembly);
            var displayName = locale?.GetString(localeAttribute?.DisplayName ?? "") ?? assemblyName;

            context.Unload();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            return new AssemblyInfo(filePath, assemblyName, displayName, company, version, localeAttribute?.IconData ?? "");
        }
        catch (Exception)
        {
            return null;
        }
       
    }
    
    /// <inheritdoc />
    public Assembly LoadAssembly(string name)
    {
        if (loadedContexts.TryGetValue(name, out var context))
            return context.Assemblies.Single(assembly => 
                assembly.ManifestModule.Name == $"{name}.dll");
        
        var filePath = GetAssemblyPath(name);
        context = new PluginLoadContext(filePath);
        loadedContexts[name] = context;

        return context.LoadFromAssemblyPath(filePath);
    }
    
    /// <inheritdoc />
    public void UnloadAssembly(string name)
    {
        if (!loadedContexts.TryGetValue(name, out var context))
            throw new InvalidOperationException($"Assembly {name} is not loaded");

        context.Unload();
        loadedContexts.Remove(name);
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }

    /// <inheritdoc />
    public object Activate(Type type, params object[] args)
    {
        try
        {
            return ActivatorUtilities.CreateInstance(serviceProvider, type, args);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Failed to create an instance of {type.Name}", e);
        }
    }

    /// <inheritdoc />
    public ResourceManager? GetLocaleResourceManager(Assembly assembly)
    {
        return assembly
            .DefinedTypes
            .FirstOrDefault(type => type.Name == "Locale")?
            .GetProperty(nameof(ResourceManager), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)?
            .GetValue(null) as ResourceManager;
    }
    
    private string GetAssemblyPath(string name, bool updateCache = false)
    {
        if (updateCache) 
            assemblyCache = GetAssemblyInfos(Const.WidgetsFolder);
        
        var assemblyInfo = assemblyCache[name]
            .MaxBy(assembly => assembly.Version);

        if (assemblyInfo != default) 
            return assemblyInfo.FilePath;

        if (!updateCache)
            return GetAssemblyPath(name, true);
        
        throw new FileNotFoundException($"Assembly {name} not found");
    }

    private class PluginLoadContext(string pluginPath) : AssemblyLoadContext(true)
    {
        private readonly AssemblyDependencyResolver resolver = new(pluginPath);

        protected override Assembly? Load(AssemblyName assemblyName)
        {
            var assembly = Default.Assemblies.FirstOrDefault(a => a.GetName().Name == assemblyName.Name);
            if (assembly != null)
            {
                return assembly;
            }

            var assemblyPath = resolver.ResolveAssemblyToPath(assemblyName);
            return assemblyPath != null ? LoadFromAssemblyPath(assemblyPath) : null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            var libraryPath = resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
    
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }

            return IntPtr.Zero;
        }
    }
}