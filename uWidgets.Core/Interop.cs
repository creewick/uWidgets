using System.Runtime.InteropServices;

namespace uWidgets.Core;

public static class Interop
{
    [DllImport("user32.dll")]
    public static extern int GetDpiForSystem();
}