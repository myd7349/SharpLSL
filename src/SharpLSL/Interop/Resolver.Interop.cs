#pragma warning disable CS1591
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using lsl_continuous_resolver = System.IntPtr;
using lsl_streaminfo = System.IntPtr;

namespace SharpLSL.Interop
{
    public static unsafe partial class LSL
    {
#if USE_LIBRARY_IMPORT
        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_continuous_resolver lsl_create_continuous_resolver(double forget_after);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_continuous_resolver lsl_create_continuous_resolver_byprop([NativeTypeName("const char *")] sbyte* prop, [NativeTypeName("const char *")] sbyte* value, double forget_after);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_continuous_resolver lsl_create_continuous_resolver_bypred([NativeTypeName("const char *")] sbyte* pred, double forget_after);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_resolver_results(lsl_continuous_resolver res, [NativeTypeName("lsl_streaminfo *")] IntPtr* buffer, [NativeTypeName("uint32_t")] uint buffer_elements);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_resolver_results(lsl_continuous_resolver res, lsl_streaminfo[] buffer, uint buffer_elements);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial void lsl_destroy_continuous_resolver(lsl_continuous_resolver res);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_resolve_all([NativeTypeName("lsl_streaminfo *")] IntPtr* buffer, [NativeTypeName("uint32_t")] uint buffer_elements, double wait_time);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_resolve_all(lsl_streaminfo[] buffer, uint buffer_elements, double wait_time);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_resolve_byprop([NativeTypeName("lsl_streaminfo *")] IntPtr* buffer, [NativeTypeName("uint32_t")] uint buffer_elements, [NativeTypeName("const char *")] sbyte* prop, [NativeTypeName("const char *")] sbyte* value, [NativeTypeName("int32_t")] int minimum, double timeout);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_resolve_bypred([NativeTypeName("lsl_streaminfo *")] IntPtr* buffer, [NativeTypeName("uint32_t")] uint buffer_elements, [NativeTypeName("const char *")] sbyte* pred, [NativeTypeName("int32_t")] int minimum, double timeout);
#else
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_resolver_results(lsl_continuous_resolver res, [Out] lsl_streaminfo[] buffer, uint buffer_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_resolve_all([Out] lsl_streaminfo[] buffer, uint buffer_elements, double wait_time);
#endif
    }
}

// References:
// [Refactor your code using alias any type](https://devblogs.microsoft.com/dotnet/refactor-your-code-using-alias-any-type/)
