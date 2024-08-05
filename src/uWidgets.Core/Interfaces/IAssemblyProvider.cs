using System.Reflection;
using uWidgets.Core.Models;

namespace uWidgets.Core.Interfaces;

/// <summary>
/// Service for loading and unloading assemblies, and activating types.
/// </summary>
public interface IAssemblyProvider
{
    /// <summary>
    /// Get all assembly infos in the specified directory.
    /// </summary>
    /// <param name="directoryPath">Directory path.</param>
    /// <returns>Assembly infos, grouped by name.</returns>
    public ILookup<string, AssemblyInfo> GetAssemblyInfos(string directoryPath);

    /// <summary>
    /// <para>Load the assembly with the specified name.</para>
    /// If there are more than one assembly with the same name,
    /// loads one with the max <see cref="AssemblyVersionAttribute"/>
    /// </summary>
    /// <param name="assemblyName">Assembly name.</param>
    /// <returns>The loaded assembly.</returns>
    public Assembly LoadAssembly(string assemblyName);
    
    /// <summary>
    /// Unload the assembly with the specified name.
    /// </summary>
    /// <param name="assemblyName">Assembly name.</param>
    /// <exception cref="InvalidOperationException">Assembly is not loaded.</exception>
    public void UnloadAssembly(string assemblyName);
    
    /// <summary>
    /// Activate the specified type.
    /// </summary>
    /// <param name="type">Type to activate.</param>
    /// <param name="args">Constructor arguments not provided by the provider.</param>
    /// <returns>Activated object.</returns>
    /// <exception cref="InvalidOperationException">No matching constructor is found.</exception>
    public object Activate(Type type, params object[] args);
}
