#pragma warning disable CS1591
using System;
using System.Runtime.InteropServices;

using lsl_continuous_resolver = System.IntPtr;

namespace SharpLSL.Interop
{
    public static unsafe partial class LSL
    {
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_resolver_results(lsl_continuous_resolver res, [Out] IntPtr[] buffer, uint buffer_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_resolve_all([Out] IntPtr[] buffer, uint buffer_elements, double wait_time);
    }
}

// References:
// [Refactor your code using alias any type](https://devblogs.microsoft.com/dotnet/refactor-your-code-using-alias-any-type/)
