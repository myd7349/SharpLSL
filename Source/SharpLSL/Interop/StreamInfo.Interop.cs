using System.Runtime.InteropServices;

using lsl_streaminfo = System.IntPtr;

namespace SharpLSL.Interop
{
    public static partial class LSL
    {
        // TODO: Encoding
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern lsl_streaminfo lsl_create_streaminfo(
            [MarshalAs(UnmanagedType.LPStr)] string name,
            [MarshalAs(UnmanagedType.LPStr)] string type,
            int channel_count,
            double nominal_srate,
            ChannelFormat channel_format,
            [MarshalAs(UnmanagedType.LPStr)] string source_id);

        // TODO: Encoding
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern lsl_streaminfo lsl_streaminfo_from_xml(
            [MarshalAs(UnmanagedType.LPStr)] string xml);

        // TODO: Encoding
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_stream_info_matches_query(
            lsl_streaminfo info,
            [MarshalAs(UnmanagedType.LPStr)] string query);
    }
}


// References:
// [Refactor your code using alias any type](https://devblogs.microsoft.com/dotnet/refactor-your-code-using-alias-any-type/)
