using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyInjection;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;

namespace uWidgets.Core.Services;
 
public class AssemblyProvider : IAssemblyProvider
{
    private readonly Dictionary<string, AssemblyLoadContext> loadedContexts = new();
    private ILookup<string, AssemblyInfo> assemblyCache;
    private readonly IServiceProvider serviceProvider;

    public AssemblyProvider(IServiceProvider serviceProvider)
    {
        assemblyCache = GetAssemblyInfos(Const.WidgetsFolder);
        this.serviceProvider = serviceProvider;
    }
    
    public ILookup<string, AssemblyInfo> GetAssemblyInfos(string directoryPath)
    {
        var assemblies = Directory.Exists(directoryPath)
            ? Directory
                .GetFiles(directoryPath, "*.dll")
                .Select(filePath => new AssemblyInfo(filePath))
            : [];
        
        return assemblies
            .ToLookup(assembly => assembly.AssemblyName.Name!);
    }
    
    public Assembly LoadAssembly(string name)
    {
        if (loadedContexts.TryGetValue(name, out var context))
            return context.Assemblies.Single(assembly => 
                assembly.ManifestModule.Name == $"{name}.dll");
        
        context = new AssemblyLoadContext(name, true);
        var filePath = GetAssemblyPath(name);
        loadedContexts[name] = context;
        
        return context.LoadFromAssemblyPath(filePath);
    }

    public void UnloadAssembly(string name)
    {
        if (!loadedContexts.TryGetValue(name, out var context))
            throw new InvalidOperationException($"Assembly {name} is not loaded");

        context.Unload();
        loadedContexts.Remove(name);
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }

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
    
    private string GetAssemblyPath(string name, bool updateCache = false)
    {
        if (updateCache) 
            assemblyCache = GetAssemblyInfos(Const.WidgetsFolder);
        
        var assemblyInfo = assemblyCache[name]
            .MaxBy(assembly => assembly.AssemblyName.Version);

        if (assemblyInfo != default) 
            return assemblyInfo.FilePath;

        if (!updateCache)
            return GetAssemblyPath(name, true);
        
        throw new FileNotFoundException($"Assembly {name} not found");
    }
}