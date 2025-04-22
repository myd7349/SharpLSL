#if NET7_0_OR_GREATER
#pragma warning disable CS1591

using System;
using System.Runtime.InteropServices;

namespace SharpLSL.Interop
{
    public static partial class LSL
    {
        [LibraryImport("lsl")]
        public static partial IntPtr lsl_last_error();

        [LibraryImport("lsl")]
        public static partial int lsl_protocol_version();

        [LibraryImport("lsl")]
        public static partial int lsl_library_version();

        [LibraryImport("lsl")]
        public static partial IntPtr lsl_library_info();

        [LibraryImport("lsl")]
        public static partial double lsl_local_clock();

        [LibraryImport("lsl")]
        public static partial void lsl_destroy_string([NativeTypeName("char *")] IntPtr s);

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
#endif
