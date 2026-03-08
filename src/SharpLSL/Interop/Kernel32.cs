#if NET35 || NET45
using System.Runtime.InteropServices;

namespace SharpLSL.Interop
{
    internal static class Kernel32
    {
        //[DllImport("KERNEL32.dll", EntryPoint = "SetDllDirectoryW", ExactSpelling = true, SetLastError = true)] // not work, System.BadImageFormatException
        //[DllImport("KERNEL32.dll", CharSet = CharSet.Unicode, SetLastError = true)] // ok
        //[DllImport("KERNEL32.dll", CharSet = CharSet.Unicode, EntryPoint = "SetDllDirectoryW", SetLastError = true)] // ok
        //[DllImport("KERNEL32.dll", EntryPoint = "SetDllDirectoryW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)] // ok
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDllDirectory(string path);
    }
}
#endif
