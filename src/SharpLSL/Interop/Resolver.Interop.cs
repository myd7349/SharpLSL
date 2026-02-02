#pragma warning disable CS1591
using System.Runtime.InteropServices;

using lsl_continuous_resolver = System.IntPtr;
using lsl_streaminfo = System.IntPtr;

namespace SharpLSL.Interop
{
    public static partial class LSL
    {
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_resolver_results(lsl_continuous_resolver res, [Out] lsl_streaminfo[] buffer, uint buffer_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_resolve_all([Out] lsl_streaminfo[] buffer, uint buffer_elements, double wait_time);
    }
}

// References:
// [Refactor your code using alias any type](https://devblogs.microsoft.com/dotnet/refactor-your-code-using-alias-any-type/)
