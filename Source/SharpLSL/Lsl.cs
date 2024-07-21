using System;
using System.Runtime.InteropServices;

using SharpLSL.Interop;

namespace SharpLSL
{
    public static class Lsl
    {
        public static string GetLastError()
        {
            var ptr = LSL.lsl_last_error();
            return Marshal.PtrToStringAnsi(ptr);
        }

        public static int GetProtocolVersion() => LSL.lsl_protocol_version();

        public static int GetLibraryVersion() => LSL.lsl_library_version();

        public static string GetLibraryInfo()
        {
            var ptr = LSL.lsl_library_info();
            return Marshal.PtrToStringAnsi(ptr);
        }

        public static double GetLocalClock() => LSL.lsl_local_clock();

        public static void DestroyString(IntPtr str) => LSL.lsl_destroy_string(str);
    }
}
