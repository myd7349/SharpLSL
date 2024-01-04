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
        [return: NativeTypeName("lsl_streaminfo")]
        public static extern IntPtr lsl_create_streaminfo([NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const char *")] sbyte* type, [NativeTypeName("int32_t")] int channel_count, double nominal_srate, lsl_channel_format_t channel_format, [NativeTypeName("const char *")] sbyte* source_id);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void lsl_destroy_streaminfo([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_streaminfo")]
        public static extern IntPtr lsl_copy_streaminfo([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* lsl_get_name([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* lsl_get_type([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_get_channel_count([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_get_nominal_srate([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern lsl_channel_format_t lsl_get_channel_format([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* lsl_get_source_id([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_get_version([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_get_created_at([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* lsl_get_uid([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* lsl_get_session_id([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* lsl_get_hostname([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_xml_ptr")]
        public static extern IntPtr lsl_get_desc([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("char *")]
        public static extern sbyte* lsl_get_xml([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_get_channel_bytes([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_get_sample_bytes([NativeTypeName("lsl_streaminfo")] IntPtr info);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_stream_info_matches_query([NativeTypeName("lsl_streaminfo")] IntPtr info, [NativeTypeName("const char *")] sbyte* query);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_streaminfo")]
        public static extern IntPtr lsl_streaminfo_from_xml([NativeTypeName("const char *")] sbyte* xml);
    }
}
