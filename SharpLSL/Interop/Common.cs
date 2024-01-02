/*
 * This file is generated using ClangSharpPInvokeGenerator.
 * Please refrain from manual modifications.
 * If changes are necessary, modify generate.ps1 and regenerate the file.
 */

using System.Runtime.InteropServices;

namespace SharpLSL.Interop
{
    public static unsafe partial class Common
    {
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_last_error@@YAPEBDXZ", ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* lsl_last_error();

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_protocol_version@@YAHXZ", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_protocol_version();

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_library_version@@YAHXZ", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_library_version();

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_library_info@@YAPEBDXZ", ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* lsl_library_info();

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_local_clock@@YANXZ", ExactSpelling = true)]
        public static extern double lsl_local_clock();

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_destroy_string@@YAXPEAD@Z", ExactSpelling = true)]
        public static extern void lsl_destroy_string([NativeTypeName("char *")] sbyte* s);
    }
}
