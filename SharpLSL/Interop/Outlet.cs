/*
 * This file is generated using ClangSharpPInvokeGenerator.
 * Please refrain from manual modifications.
 * If changes are necessary, modify generate.ps1 and regenerate the file.
 */

using System;
using System.Runtime.InteropServices;

namespace SharpLSL.Interop
{
    public static unsafe partial class Outlet
    {
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_create_outlet@@YAPEAUlsl_outlet_struct_@@PEAUlsl_streaminfo_struct_@@HH@Z", ExactSpelling = true)]
        [return: NativeTypeName("lsl_outlet")]
        public static extern IntPtr lsl_create_outlet([NativeTypeName("lsl_streaminfo")] IntPtr info, [NativeTypeName("int32_t")] int chunk_size, [NativeTypeName("int32_t")] int max_buffered);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_create_outlet_ex@@YAPEAUlsl_outlet_struct_@@PEAUlsl_streaminfo_struct_@@HHW4lsl_transport_options_t@@@Z", ExactSpelling = true)]
        [return: NativeTypeName("lsl_outlet")]
        public static extern IntPtr lsl_create_outlet_ex([NativeTypeName("lsl_streaminfo")] IntPtr info, [NativeTypeName("int32_t")] int chunk_size, [NativeTypeName("int32_t")] int max_buffered, lsl_transport_options_t flags);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_destroy_outlet@@YAXPEAUlsl_outlet_struct_@@@Z", ExactSpelling = true)]
        public static extern void lsl_destroy_outlet([NativeTypeName("lsl_outlet")] IntPtr @out);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_f@@YAHPEAUlsl_outlet_struct_@@PEBM@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_f([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const float *")] float* data);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_d@@YAHPEAUlsl_outlet_struct_@@PEBN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_d([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const double *")] double* data);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_l@@YAHPEAUlsl_outlet_struct_@@PEB_J@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_l([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int64_t *")] long* data);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_i@@YAHPEAUlsl_outlet_struct_@@PEBH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_i([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int32_t *")] int* data);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_s@@YAHPEAUlsl_outlet_struct_@@PEBF@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_s([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int16_t *")] short* data);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_c@@YAHPEAUlsl_outlet_struct_@@PEBD@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_c([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char *")] sbyte* data);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_str@@YAHPEAUlsl_outlet_struct_@@PEAPEBD@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_str([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_v@@YAHPEAUlsl_outlet_struct_@@PEBX@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_v([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const void *")] void* data);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_ft@@YAHPEAUlsl_outlet_struct_@@PEBMN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_ft([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const float *")] float* data, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_dt@@YAHPEAUlsl_outlet_struct_@@PEBNN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_dt([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const double *")] double* data, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_lt@@YAHPEAUlsl_outlet_struct_@@PEB_JN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_lt([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int64_t *")] long* data, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_it@@YAHPEAUlsl_outlet_struct_@@PEBHN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_it([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int32_t *")] int* data, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_st@@YAHPEAUlsl_outlet_struct_@@PEBFN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_st([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int16_t *")] short* data, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_ct@@YAHPEAUlsl_outlet_struct_@@PEBDN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_ct([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char *")] sbyte* data, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_strt@@YAHPEAUlsl_outlet_struct_@@PEAPEBDN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_strt([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_vt@@YAHPEAUlsl_outlet_struct_@@PEBXN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_vt([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const void *")] void* data, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_ftp@@YAHPEAUlsl_outlet_struct_@@PEBMNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_ftp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const float *")] float* data, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_dtp@@YAHPEAUlsl_outlet_struct_@@PEBNNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_dtp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const double *")] double* data, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_ltp@@YAHPEAUlsl_outlet_struct_@@PEB_JNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_ltp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int64_t *")] long* data, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_itp@@YAHPEAUlsl_outlet_struct_@@PEBHNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_itp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int32_t *")] int* data, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_stp@@YAHPEAUlsl_outlet_struct_@@PEBFNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_stp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int16_t *")] short* data, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_ctp@@YAHPEAUlsl_outlet_struct_@@PEBDNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_ctp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char *")] sbyte* data, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_strtp@@YAHPEAUlsl_outlet_struct_@@PEAPEBDNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_strtp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_vtp@@YAHPEAUlsl_outlet_struct_@@PEBXNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_vtp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const void *")] void* data, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_buf@@YAHPEAUlsl_outlet_struct_@@PEAPEBDPEBI@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_buf([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_buft@@YAHPEAUlsl_outlet_struct_@@PEAPEBDPEBIN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_buft([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_sample_buftp@@YAHPEAUlsl_outlet_struct_@@PEAPEBDPEBINH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_sample_buftp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_f@@YAHPEAUlsl_outlet_struct_@@PEBMK@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_f([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const float *")] float* data, [NativeTypeName("unsigned long")] uint data_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_d@@YAHPEAUlsl_outlet_struct_@@PEBNK@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_d([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const double *")] double* data, [NativeTypeName("unsigned long")] uint data_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_l@@YAHPEAUlsl_outlet_struct_@@PEB_JK@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_l([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int64_t *")] long* data, [NativeTypeName("unsigned long")] uint data_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_i@@YAHPEAUlsl_outlet_struct_@@PEBHK@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_i([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int32_t *")] int* data, [NativeTypeName("unsigned long")] uint data_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_s@@YAHPEAUlsl_outlet_struct_@@PEBFK@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_s([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int16_t *")] short* data, [NativeTypeName("unsigned long")] uint data_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_c@@YAHPEAUlsl_outlet_struct_@@PEBDK@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_c([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char *")] sbyte* data, [NativeTypeName("unsigned long")] uint data_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_str@@YAHPEAUlsl_outlet_struct_@@PEAPEBDK@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_str([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("unsigned long")] uint data_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_ft@@YAHPEAUlsl_outlet_struct_@@PEBMKN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_ft([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const float *")] float* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_dt@@YAHPEAUlsl_outlet_struct_@@PEBNKN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_dt([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const double *")] double* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_lt@@YAHPEAUlsl_outlet_struct_@@PEB_JKN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_lt([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int64_t *")] long* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_it@@YAHPEAUlsl_outlet_struct_@@PEBHKN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_it([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int32_t *")] int* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_st@@YAHPEAUlsl_outlet_struct_@@PEBFKN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_st([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int16_t *")] short* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_ct@@YAHPEAUlsl_outlet_struct_@@PEBDKN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_ct([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char *")] sbyte* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_strt@@YAHPEAUlsl_outlet_struct_@@PEAPEBDKN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_strt([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_ftp@@YAHPEAUlsl_outlet_struct_@@PEBMKNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_ftp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const float *")] float* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_dtp@@YAHPEAUlsl_outlet_struct_@@PEBNKNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_dtp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const double *")] double* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_ltp@@YAHPEAUlsl_outlet_struct_@@PEB_JKNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_ltp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int64_t *")] long* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_itp@@YAHPEAUlsl_outlet_struct_@@PEBHKNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_itp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int32_t *")] int* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_stp@@YAHPEAUlsl_outlet_struct_@@PEBFKNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_stp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int16_t *")] short* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_ctp@@YAHPEAUlsl_outlet_struct_@@PEBDKNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_ctp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char *")] sbyte* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_strtp@@YAHPEAUlsl_outlet_struct_@@PEAPEBDKNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_strtp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_ftn@@YAHPEAUlsl_outlet_struct_@@PEBMKPEBN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_ftn([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const float *")] float* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_dtn@@YAHPEAUlsl_outlet_struct_@@PEBNK1@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_dtn([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const double *")] double* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_ltn@@YAHPEAUlsl_outlet_struct_@@PEB_JKPEBN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_ltn([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int64_t *")] long* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_itn@@YAHPEAUlsl_outlet_struct_@@PEBHKPEBN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_itn([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int32_t *")] int* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_stn@@YAHPEAUlsl_outlet_struct_@@PEBFKPEBN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_stn([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int16_t *")] short* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_ctn@@YAHPEAUlsl_outlet_struct_@@PEBDKPEBN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_ctn([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char *")] sbyte* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_strtn@@YAHPEAUlsl_outlet_struct_@@PEAPEBDKPEBN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_strtn([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_ftnp@@YAHPEAUlsl_outlet_struct_@@PEBMKPEBNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_ftnp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const float *")] float* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_dtnp@@YAHPEAUlsl_outlet_struct_@@PEBNK1H@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_dtnp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const double *")] double* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_ltnp@@YAHPEAUlsl_outlet_struct_@@PEB_JKPEBNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_ltnp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int64_t *")] long* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_itnp@@YAHPEAUlsl_outlet_struct_@@PEBHKPEBNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_itnp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int32_t *")] int* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_stnp@@YAHPEAUlsl_outlet_struct_@@PEBFKPEBNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_stnp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int16_t *")] short* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_ctnp@@YAHPEAUlsl_outlet_struct_@@PEBDKPEBNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_ctnp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char *")] sbyte* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_strtnp@@YAHPEAUlsl_outlet_struct_@@PEAPEBDKPEBNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_strtnp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_buf@@YAHPEAUlsl_outlet_struct_@@PEAPEBDPEBIK@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_buf([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, [NativeTypeName("unsigned long")] uint data_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_buft@@YAHPEAUlsl_outlet_struct_@@PEAPEBDPEBIKN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_buft([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_buftp@@YAHPEAUlsl_outlet_struct_@@PEAPEBDPEBIKNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_buftp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_buftn@@YAHPEAUlsl_outlet_struct_@@PEAPEBDPEBIKPEBN@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_buftn([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_push_chunk_buftnp@@YAHPEAUlsl_outlet_struct_@@PEAPEBDPEBIKPEBNH@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_buftnp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_have_consumers@@YAHPEAUlsl_outlet_struct_@@@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_have_consumers([NativeTypeName("lsl_outlet")] IntPtr @out);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_wait_for_consumers@@YAHPEAUlsl_outlet_struct_@@N@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_wait_for_consumers([NativeTypeName("lsl_outlet")] IntPtr @out, double timeout);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_get_info@@YAPEAUlsl_streaminfo_struct_@@PEAUlsl_outlet_struct_@@@Z", ExactSpelling = true)]
        [return: NativeTypeName("lsl_streaminfo")]
        public static extern IntPtr lsl_get_info([NativeTypeName("lsl_outlet")] IntPtr @out);
    }
}
