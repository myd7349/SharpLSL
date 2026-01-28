#pragma warning disable CS1591
using System;
using System.Runtime.InteropServices;

using lsl_outlet = System.IntPtr;

namespace SharpLSL.Interop
{
    public static unsafe partial class LSL
    {
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_c(lsl_outlet @out, sbyte[] data);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_s(lsl_outlet @out, short[] data);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_i(lsl_outlet @out, int[] data);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_l(lsl_outlet @out, long[] data);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_f(lsl_outlet @out, float[] data);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_d(lsl_outlet @out, double[] data);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_str(lsl_outlet @out, IntPtr data);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_v(lsl_outlet @out, byte[] data);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_buf(lsl_outlet @out, IntPtr data, uint* lengths);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_ct(lsl_outlet @out, sbyte[] data, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_st(lsl_outlet @out, short[] data, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_it(lsl_outlet @out, int[] data, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_lt(lsl_outlet @out, long[] data, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_ft(lsl_outlet @out, float[] data, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_dt(lsl_outlet @out, double[] data, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_strt(lsl_outlet @out, IntPtr data, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_vt(lsl_outlet @out, byte[] data, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_buft(lsl_outlet @out, IntPtr data, uint* lengths, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_ctp(lsl_outlet @out, sbyte[] data, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_stp(lsl_outlet @out, short[] data, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_itp(lsl_outlet @out, int[] data, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_ltp(lsl_outlet @out, long[] data, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_ftp(lsl_outlet @out, float[] data, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_dtp(lsl_outlet @out, double[] data, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_strtp(lsl_outlet @out, IntPtr data, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_vtp(lsl_outlet @out, byte[] data, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_sample_buftp(lsl_outlet @out, IntPtr data, uint* lengths, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_c(lsl_outlet @out, sbyte[] data, uint data_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_s(lsl_outlet @out, short[] data, uint data_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_i(lsl_outlet @out, int[] data, uint data_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_l(lsl_outlet @out, long[] data, uint data_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_f(lsl_outlet @out, float[] data, uint data_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_d(lsl_outlet @out, double[] data, uint data_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_str(lsl_outlet @out, IntPtr data, uint data_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_ct(lsl_outlet @out, sbyte[] data, uint data_elements, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_st(lsl_outlet @out, short[] data, uint data_elements, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_it(lsl_outlet @out, int[] data, uint data_elements, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_lt(lsl_outlet @out, long[] data, uint data_elements, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_ft(lsl_outlet @out, float[] data, uint data_elements, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_dt(lsl_outlet @out, double[] data, uint data_elements, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_strt(lsl_outlet @out, IntPtr data, uint data_elements, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_ctp(lsl_outlet @out, sbyte[] data, uint data_elements, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_stp(lsl_outlet @out, short[] data, uint data_elements, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_itp(lsl_outlet @out, int[] data, uint data_elements, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_ltp(lsl_outlet @out, long[] data, uint data_elements, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_ftp(lsl_outlet @out, float[] data, uint data_elements, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_dtp(lsl_outlet @out, double[] data, uint data_elements, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_strtp(lsl_outlet @out, IntPtr data, uint data_elements, double timestamp, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_ctn(lsl_outlet @out, sbyte[] data, uint data_elements, double[] timestamps);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_stn(lsl_outlet @out, short[] data, uint data_elements, double[] timestamps);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_itn(lsl_outlet @out, int[] data, uint data_elements, double[] timestamps);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_ltn(lsl_outlet @out, long[] data, uint data_elements, double[] timestamps);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_ftn(lsl_outlet @out, float[] data, uint data_elements, double[] timestamps);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_dtn(lsl_outlet @out, double[] data, uint data_elements, double[] timestamps);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_strtn(lsl_outlet @out, IntPtr data, uint data_elements, double* timestamps);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_ctnp(lsl_outlet @out, sbyte[] data, uint data_elements, double[] timestamps, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_stnp(lsl_outlet @out, short[] data, uint data_elements, double[] timestamps, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_itnp(lsl_outlet @out, int[] data, uint data_elements, double[] timestamps, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_ltnp(lsl_outlet @out, long[] data, uint data_elements, double[] timestamps, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_ftnp(lsl_outlet @out, float[] data, uint data_elements, double[] timestamps, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_dtnp(lsl_outlet @out, double[] data, uint data_elements, double[] timestamps, int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_push_chunk_strtnp(lsl_outlet @out, IntPtr data, uint data_elements, double* timestamps, int pushthrough);

        /*
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_buf(lsl_outlet @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, [NativeTypeName("unsigned long")] uint data_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_buft(lsl_outlet @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_buftp(lsl_outlet @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_buftn(lsl_outlet @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_push_chunk_buftnp(lsl_outlet @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);
        */
    }
}


// References:
// [Marshal an array of strings from C# to C code using p/invoke](https://stackoverflow.com/questions/13317931/marshal-an-array-of-strings-from-c-sharp-to-c-code-using-p-invoke)
