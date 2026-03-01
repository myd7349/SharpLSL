#pragma warning disable CS1591
using System;
using System.Runtime.InteropServices;

using lsl_outlet = System.IntPtr;

namespace SharpLSL.Interop
{
    public static unsafe partial class LSL
    {
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_strtp(lsl_outlet @out, IntPtr data, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_strtp(lsl_outlet @out, IntPtr[] data, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_vtp(lsl_outlet @out, byte[] data, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_vtp(lsl_outlet @out, IntPtr data, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_buftp(lsl_outlet @out, IntPtr data, uint* lengths, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_buftp(lsl_outlet @out, IntPtr[] data, uint* lengths, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_strtp(lsl_outlet @out, IntPtr data, uint data_elements, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_strtp(lsl_outlet @out, IntPtr[] data, uint data_elements, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_buftp(lsl_outlet @out, IntPtr data, uint* lengths, uint data_elements, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_buftp(lsl_outlet @out, IntPtr[] data, uint* lengths, uint data_elements, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_strtnp(lsl_outlet @out, IntPtr data, uint data_elements, double* timestamps, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_strtnp(lsl_outlet @out, IntPtr[] data, uint data_elements, double* timestamps, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_buftnp(lsl_outlet @out, IntPtr data, uint* lengths, uint data_elements, double[] timestamps, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_buftnp(lsl_outlet @out, IntPtr[] data, uint* lengths, uint data_elements, double[] timestamps, int pushthrough);
    }
}


// References:
// [Marshal an array of strings from C# to C code using p/invoke](https://stackoverflow.com/questions/13317931/marshal-an-array-of-strings-from-c-sharp-to-c-code-using-p-invoke)
