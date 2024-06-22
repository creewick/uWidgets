using System.Reflection;

namespace uWidgets.Core.Interfaces;

public interface IAssemblyProvider
{
    public Assembly LoadAssembly(string assemblyName);
    public void UnloadAssembly(string assemblyName);
    public object Activate(string assemblyName, string typeName, Type? parentType = null, params object[] args);
}