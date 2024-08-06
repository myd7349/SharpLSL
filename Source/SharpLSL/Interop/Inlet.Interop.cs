using System;
using System.Runtime.InteropServices;

using lsl_inlet = System.IntPtr;

namespace SharpLSL.Interop
{
    public static unsafe partial class LSL
    {
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern lsl_inlet lsl_get_fullinfo(lsl_inlet @in, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void lsl_open_stream([NativeTypeName("lsl_inlet")] IntPtr @in, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_time_correction([NativeTypeName("lsl_inlet")] IntPtr @in, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_time_correction_ex([NativeTypeName("lsl_inlet")] IntPtr @in, ref double remote_time, ref double uncertainty, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_f([NativeTypeName("lsl_inlet")] IntPtr @in, float[] buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_d([NativeTypeName("lsl_inlet")] IntPtr @in, double[] buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_l([NativeTypeName("lsl_inlet")] IntPtr @in, long[] buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_i([NativeTypeName("lsl_inlet")] IntPtr @in, int[] buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_s([NativeTypeName("lsl_inlet")] IntPtr @in, short[] buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_f([NativeTypeName("lsl_inlet")] IntPtr @in, float[] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_d([NativeTypeName("lsl_inlet")] IntPtr @in, double[] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_l([NativeTypeName("lsl_inlet")] IntPtr @in, long[] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_i([NativeTypeName("lsl_inlet")] IntPtr @in, int[] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_s([NativeTypeName("lsl_inlet")] IntPtr @in, short[] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_f([NativeTypeName("lsl_inlet")] IntPtr @in, float[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_d([NativeTypeName("lsl_inlet")] IntPtr @in, double[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_l([NativeTypeName("lsl_inlet")] IntPtr @in, long[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_i([NativeTypeName("lsl_inlet")] IntPtr @in, int[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_s([NativeTypeName("lsl_inlet")] IntPtr @in, short[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        /*
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_inlet")]
        public static extern IntPtr lsl_create_inlet([NativeTypeName("lsl_streaminfo")] IntPtr info, [NativeTypeName("int32_t")] int max_buflen, [NativeTypeName("int32_t")] int max_chunklen, [NativeTypeName("int32_t")] int recover);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_streaminfo")]
        public static extern IntPtr lsl_get_fullinfo([NativeTypeName("lsl_inlet")] IntPtr @in, double timeout, [NativeTypeName("int32_t *")] int* ec);

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
        */
    }
}
