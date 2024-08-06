using System.Reflection;

namespace uWidgets.Core.Models;

/// <summary>
/// Information about an assembly.
/// </summary>
/// <param name="filePath">The path to the assembly file.</param>
public class AssemblyInfo(string filePath)
{
    /// <summary>
    /// The path to the assembly file.
    /// </summary>
    public readonly string FilePath = filePath;
    
    /// <summary>
    /// Object containing information about the assembly, such as its name, version, and culture.
    /// </summary>
    public readonly AssemblyName AssemblyName = AssemblyName.GetAssemblyName(filePath);
}