#pragma warning disable CS1591
using System;
using System.Runtime.InteropServices;

using lsl_inlet = System.IntPtr;

namespace SharpLSL.Interop
{
    public static unsafe partial class LSL
    {
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_str(lsl_inlet @in, IntPtr[] buffer, int buffer_elements, double timeout, int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_buf(lsl_inlet @in, IntPtr[] buffer, uint[] buffer_lengths, int buffer_elements, double timeout, int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_v(lsl_inlet @in, IntPtr buffer, int buffer_bytes, double timeout, int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_str(lsl_inlet @in, IntPtr[] data_buffer, double* timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint lsl_pull_chunk_buf(lsl_inlet @in, IntPtr[] data_buffer, uint[] lengths_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);
    }
}
