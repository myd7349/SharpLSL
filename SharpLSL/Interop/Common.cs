/*
 * This file is generated using ClangSharpPInvokeGenerator.
 * Please refrain from manual modifications.
 * If changes are necessary, modify generate.ps1 and regenerate the file.
 */

using System;
using System.Runtime.InteropServices;

namespace SharpLSL.Interop
{
    public static partial class LSL
    {
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern IntPtr lsl_last_error();

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_protocol_version();

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_library_version();

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern IntPtr lsl_library_info();

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_local_clock();

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void lsl_destroy_string([NativeTypeName("char *")] IntPtr s);

        [NativeTypeName("#define LSL_IRREGULAR_RATE 0.0")]
        public const double LSL_IRREGULAR_RATE = 0.0;

        [NativeTypeName("#define LSL_DEDUCED_TIMESTAMP -1.0")]
        public const double LSL_DEDUCED_TIMESTAMP = -1.0;

        [NativeTypeName("#define LSL_FOREVER 32000000.0")]
        public const double LSL_FOREVER = 32000000.0;

        [NativeTypeName("#define LSL_NO_PREFERENCE 0")]
        public const int LSL_NO_PREFERENCE = 0;

        [NativeTypeName("#define LIBLSL_COMPILE_HEADER_VERSION 114")]
        public const int LIBLSL_COMPILE_HEADER_VERSION = 114;
    }
}
