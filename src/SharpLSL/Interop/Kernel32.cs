#if NET35
using System.Runtime.InteropServices;

namespace SharpLSL.Interop
{
    internal static class Kernel32
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDllDirectory(string path);
    }
}
#endif
