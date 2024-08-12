#pragma warning disable CS1591
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
        public static extern void lsl_open_stream(lsl_inlet @in, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_time_correction(lsl_inlet @in, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_time_correction_ex(lsl_inlet @in, ref double remote_time, ref double uncertainty, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_c(lsl_inlet @in, sbyte[] buffer, int buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_c(lsl_inlet @in, byte[] buffer, int buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_s(lsl_inlet @in, short[] buffer, int buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_i(lsl_inlet @in, int[] buffer, int buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_l(lsl_inlet @in, long[] buffer, int buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_f(lsl_inlet @in, float[] buffer, int buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_d(lsl_inlet @in, double[] buffer, int buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_str(lsl_inlet @in, IntPtr[] buffer, int buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_buf(lsl_inlet @in, IntPtr[] buffer, uint[] buffer_lengths, int buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_v(lsl_inlet @in, byte[] buffer, int buffer_bytes, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_c(lsl_inlet @in, sbyte[] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_s(lsl_inlet @in, short[] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_i(lsl_inlet @in, int[] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_l(lsl_inlet @in, long[] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_f(lsl_inlet @in, float[] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_d(lsl_inlet @in, double[] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_str(lsl_inlet @in, IntPtr[] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_buf(lsl_inlet @in, IntPtr[] data_buffer, uint[] lengths_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_c(lsl_inlet @in, sbyte[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_s(lsl_inlet @in, short[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_i(lsl_inlet @in, int[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_l(lsl_inlet @in, long[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_f(lsl_inlet @in, float[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_d(lsl_inlet @in, double[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_str(lsl_inlet @in, IntPtr[,] data_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);
    }
}
