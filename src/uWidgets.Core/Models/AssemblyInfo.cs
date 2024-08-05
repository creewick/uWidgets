using System.Reflection;

namespace uWidgets.Core.Models;

public class AssemblyInfo(string filePath)
{
    public readonly string FilePath = filePath;
    public readonly AssemblyName AssemblyName = AssemblyName.GetAssemblyName(filePath);
}