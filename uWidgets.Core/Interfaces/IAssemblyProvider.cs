using System.Reflection;
using uWidgets.Core.Models;

namespace uWidgets.Core.Interfaces;

public interface IAssemblyProvider
{
    public ILookup<string, AssemblyInfo> GetAssemblyInfos(string directoryPath);
    public Assembly LoadAssembly(string assemblyName);
    public void UnloadAssembly(string assemblyName);
    public object Activate(Type type, params object[] args);
}