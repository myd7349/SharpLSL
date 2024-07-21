// <auto-generated/>
// 
// This file is generated using ClangSharpPInvokeGenerator.
// Please refrain from manual modifications.
// If changes are necessary, modify generate.ps1 and regenerate the file.
//

using System;
using System.Runtime.InteropServices;

namespace SharpLSL.Interop
{
    public static unsafe partial class LSL
    {
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_inlet")]
        public static extern IntPtr lsl_create_inlet([NativeTypeName("lsl_streaminfo")] IntPtr info, [NativeTypeName("int32_t")] int max_buflen, [NativeTypeName("int32_t")] int max_chunklen, [NativeTypeName("int32_t")] int recover);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_inlet")]
        public static extern IntPtr lsl_create_inlet_ex([NativeTypeName("lsl_streaminfo")] IntPtr info, [NativeTypeName("int32_t")] int max_buflen, [NativeTypeName("int32_t")] int max_chunklen, [NativeTypeName("int32_t")] int recover, lsl_transport_options_t flags);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void lsl_destroy_inlet([NativeTypeName("lsl_inlet")] IntPtr @in);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_streaminfo")]
        public static extern IntPtr lsl_get_fullinfo([NativeTypeName("lsl_inlet")] IntPtr @in, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void lsl_open_stream([NativeTypeName("lsl_inlet")] IntPtr @in, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void lsl_close_stream([NativeTypeName("lsl_inlet")] IntPtr @in);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_time_correction([NativeTypeName("lsl_inlet")] IntPtr @in, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_time_correction_ex([NativeTypeName("lsl_inlet")] IntPtr @in, double* remote_time, double* uncertainty, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_set_postprocessing([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("uint32_t")] uint flags);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_f([NativeTypeName("lsl_inlet")] IntPtr @in, float* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_d([NativeTypeName("lsl_inlet")] IntPtr @in, double* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_l([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("int64_t *")] long* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_i([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("int32_t *")] int* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_s([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("int16_t *")] short* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_c([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("char *")] sbyte* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_str([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("char **")] sbyte** buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_buf([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("char **")] sbyte** buffer, [NativeTypeName("uint32_t *")] uint* buffer_lengths, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_v([NativeTypeName("lsl_inlet")] IntPtr @in, void* buffer, [NativeTypeName("int32_t")] int buffer_bytes, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern uint lsl_pull_chunk_f([NativeTypeName("lsl_inlet")] IntPtr @in, float* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern uint lsl_pull_chunk_d([NativeTypeName("lsl_inlet")] IntPtr @in, double* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern uint lsl_pull_chunk_l([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("int64_t *")] long* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern uint lsl_pull_chunk_i([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("int32_t *")] int* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern uint lsl_pull_chunk_s([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("int16_t *")] short* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern uint lsl_pull_chunk_c([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("char *")] sbyte* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern uint lsl_pull_chunk_str([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("char **")] sbyte** data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern uint lsl_pull_chunk_buf([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("char **")] sbyte** data_buffer, [NativeTypeName("uint32_t *")] uint* lengths_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint lsl_samples_available([NativeTypeName("lsl_inlet")] IntPtr @in);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint lsl_inlet_flush([NativeTypeName("lsl_inlet")] IntPtr @in);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint lsl_was_clock_reset([NativeTypeName("lsl_inlet")] IntPtr @in);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_smoothing_halftime([NativeTypeName("lsl_inlet")] IntPtr @in, float value);
    }
}
