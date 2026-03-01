#if USE_LIBRARY_IMPORT
#pragma warning disable CS1591

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using lsl_streaminfo = System.IntPtr;
using lsl_xml_ptr = System.IntPtr;

namespace SharpLSL.Interop
{
    public static partial class LSL
    {
        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_streaminfo lsl_create_streaminfo([NativeTypeName("const char *")] IntPtr name, [NativeTypeName("const char *")] IntPtr type, [NativeTypeName("int32_t")] int channel_count, double nominal_srate, lsl_channel_format_t channel_format, [NativeTypeName("const char *")] IntPtr source_id);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial void lsl_destroy_streaminfo(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_streaminfo lsl_copy_streaminfo(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("const char *")]
        public static partial IntPtr lsl_get_name(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("const char *")]
        public static partial IntPtr lsl_get_type(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_get_channel_count(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial double lsl_get_nominal_srate(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_channel_format_t lsl_get_channel_format(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("const char *")]
        public static partial IntPtr lsl_get_source_id(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_get_version(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial double lsl_get_created_at(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("const char *")]
        public static partial IntPtr lsl_get_uid(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("const char *")]
        public static partial IntPtr lsl_get_session_id(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("const char *")]
        public static partial IntPtr lsl_get_hostname(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_xml_ptr lsl_get_desc(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("char *")]
        public static partial IntPtr lsl_get_xml(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_get_channel_bytes(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_get_sample_bytes(lsl_streaminfo info);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_stream_info_matches_query(lsl_streaminfo info, [NativeTypeName("const char *")] IntPtr query);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("lsl_streaminfo")]
        public static partial IntPtr lsl_streaminfo_from_xml([NativeTypeName("const char *")] IntPtr xml);
    }
}
#endif
