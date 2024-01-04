/*
 * This file is generated using ClangSharpPInvokeGenerator.
 * Please refrain from manual modifications.
 * If changes are necessary, modify generate.ps1 and regenerate the file.
 */

using System;
using System.Runtime.InteropServices;

namespace SharpLSL.Interop
{
    public static unsafe partial class LSL
    {
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_continuous_resolver")]
        public static extern IntPtr lsl_create_continuous_resolver(double forget_after);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_continuous_resolver")]
        public static extern IntPtr lsl_create_continuous_resolver_byprop([NativeTypeName("const char *")] sbyte* prop, [NativeTypeName("const char *")] sbyte* value, double forget_after);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_continuous_resolver")]
        public static extern IntPtr lsl_create_continuous_resolver_bypred([NativeTypeName("const char *")] sbyte* pred, double forget_after);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_resolver_results([NativeTypeName("lsl_continuous_resolver")] IntPtr res, [NativeTypeName("lsl_streaminfo *")] IntPtr* buffer, [NativeTypeName("uint32_t")] uint buffer_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void lsl_destroy_continuous_resolver([NativeTypeName("lsl_continuous_resolver")] IntPtr res);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_resolve_all([NativeTypeName("lsl_streaminfo *")] IntPtr* buffer, [NativeTypeName("uint32_t")] uint buffer_elements, double wait_time);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_resolve_byprop([NativeTypeName("lsl_streaminfo *")] IntPtr* buffer, [NativeTypeName("uint32_t")] uint buffer_elements, [NativeTypeName("const char *")] sbyte* prop, [NativeTypeName("const char *")] sbyte* value, [NativeTypeName("int32_t")] int minimum, double timeout);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_resolve_bypred([NativeTypeName("lsl_streaminfo *")] IntPtr* buffer, [NativeTypeName("uint32_t")] uint buffer_elements, [NativeTypeName("const char *")] sbyte* pred, [NativeTypeName("int32_t")] int minimum, double timeout);
    }
}
