#if NET35 || NET45
using System.Runtime.InteropServices;

namespace SharpLSL.Interop
{
    internal static class Kernel32
    {
        [DllImport("KERNEL32.dll", EntryPoint = "SetDllDirectoryW", ExactSpelling = true, SetLastError = true)]
        public static extern bool SetDllDirectory(string path);
    }
}
#endif
