using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyInjection;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;

namespace uWidgets.Core.Services;

public class AssemblyProvider(IServiceProvider serviceProvider) : IAssemblyProvider
{
    private ILookup<string, AssemblyInfo> assemblyCache = GetAssembliesMetadata(Const.WidgetsFolder);
    private readonly Dictionary<string, AssemblyLoadContext> loadedContexts = new();
    
    private static ILookup<string, AssemblyInfo> GetAssembliesMetadata(string directoryPath)
    {
        return Directory
            .GetFiles(directoryPath, "*.dll")
            .Select(filePath => new AssemblyInfo(AssemblyName.GetAssemblyName(filePath), filePath))
            .ToLookup(assembly => assembly.AssemblyName.Name!);
    }

    private string GetAssemblyPath(string name, bool updateCache = false)
    {
        if (updateCache) 
            assemblyCache = GetAssembliesMetadata(Const.WidgetsFolder);
        
        var assemblyInfo = assemblyCache[name]
            .MaxBy(assembly => assembly.AssemblyName.Version);

        if (assemblyInfo != default) 
            return assemblyInfo.FilePath;

        if (!updateCache)
            return GetAssemblyPath(name, true);
        
        throw new FileNotFoundException($"Assembly {name} not found");
    }
    
    public Assembly LoadAssembly(string name)
    {
        if (loadedContexts.TryGetValue(name, out var context))
            return context.Assemblies.Single();
        
        var filePath = GetAssemblyPath(name);
        context = new AssemblyLoadContext(name, true);
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

    public object Activate(string assemblyName, string typeName, Type? parentType = null, params object[] args)
    {
        var type = LoadAssembly(assemblyName)
            .GetTypes()
            .SingleOrDefault(type => (parentType == null || type.IsAssignableTo(parentType)) && 
                                     typeName == type.Name);
        
        if (type == null)
            throw new ArgumentException($"No suitable class {typeName} found in assembly {assemblyName}");

        try
        {
            return ActivatorUtilities.CreateInstance(serviceProvider, type, args);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Failed to create an instance of {typeName}", e);
        }
    }
}