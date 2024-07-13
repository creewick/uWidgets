using System.Reflection;
using uWidgets.Core.Models;

namespace uWidgets.Core.Interfaces;

public interface IAssemblyProvider
{
    public ILookup<string, AssemblyInfo> GetAssembliesMetadata(string directoryPath);
    public Assembly LoadAssembly(string assemblyName);
    public void UnloadAssembly(string assemblyName);
    public Type GetType(Assembly assembly, string typeName, Type? parentType = null);
    public object Activate(Assembly assembly, Type type, params object[] args);
    public Type? GetWidgetModelType(Assembly assembly, Type controlType);
    public string? GetLocaleBaseName(Assembly assembly);
}