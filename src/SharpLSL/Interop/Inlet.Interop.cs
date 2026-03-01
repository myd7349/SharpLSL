#pragma warning disable CS1591
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using lsl_inlet = System.IntPtr;
using lsl_streaminfo = System.IntPtr;

namespace SharpLSL.Interop
{
    public static unsafe partial class LSL
    {
#if USE_LIBRARY_IMPORT
        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_inlet lsl_create_inlet(lsl_streaminfo info, [NativeTypeName("int32_t")] int max_buflen, [NativeTypeName("int32_t")] int max_chunklen, [NativeTypeName("int32_t")] int recover);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_inlet lsl_create_inlet_ex(lsl_streaminfo info, [NativeTypeName("int32_t")] int max_buflen, [NativeTypeName("int32_t")] int max_chunklen, [NativeTypeName("int32_t")] int recover, lsl_transport_options_t flags);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial void lsl_destroy_inlet(lsl_inlet @in);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_streaminfo lsl_get_fullinfo(lsl_inlet @in, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial void lsl_open_stream(lsl_inlet @in, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial void lsl_close_stream(lsl_inlet @in);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial double lsl_time_correction(lsl_inlet @in, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial double lsl_time_correction_ex(lsl_inlet @in, double* remote_time, double* uncertainty, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_set_postprocessing(lsl_inlet @in, [NativeTypeName("uint32_t")] uint flags);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial double lsl_pull_sample_f(lsl_inlet @in, float* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial double lsl_pull_sample_d(lsl_inlet @in, double* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial double lsl_pull_sample_l(lsl_inlet @in, [NativeTypeName("int64_t *")] long* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial double lsl_pull_sample_i(lsl_inlet @in, [NativeTypeName("int32_t *")] int* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial double lsl_pull_sample_s(lsl_inlet @in, [NativeTypeName("int16_t *")] short* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial double lsl_pull_sample_c(lsl_inlet @in, [NativeTypeName("char *")] sbyte* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial double lsl_pull_sample_str(lsl_inlet @in, [NativeTypeName("char **")] sbyte** buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial double lsl_pull_sample_str(lsl_inlet @in, IntPtr[] buffer, int buffer_elements, double timeout, int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial double lsl_pull_sample_buf(lsl_inlet @in, [NativeTypeName("char **")] sbyte** buffer, [NativeTypeName("uint32_t *")] uint* buffer_lengths, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial double lsl_pull_sample_buf(lsl_inlet @in, IntPtr[] buffer, uint[] buffer_lengths, int buffer_elements, double timeout, int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial double lsl_pull_sample_v(lsl_inlet @in, void* buffer, [NativeTypeName("int32_t")] int buffer_bytes, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial double lsl_pull_sample_v(lsl_inlet @in, IntPtr buffer, int buffer_bytes, double timeout, int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("unsigned long")]
        public static partial uint lsl_pull_chunk_f(lsl_inlet @in, float* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("unsigned long")]
        public static partial uint lsl_pull_chunk_d(lsl_inlet @in, double* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("unsigned long")]
        public static partial uint lsl_pull_chunk_l(lsl_inlet @in, [NativeTypeName("int64_t *")] long* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("unsigned long")]
        public static partial uint lsl_pull_chunk_i(lsl_inlet @in, [NativeTypeName("int32_t *")] int* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("unsigned long")]
        public static partial uint lsl_pull_chunk_s(lsl_inlet @in, [NativeTypeName("int16_t *")] short* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("unsigned long")]
        public static partial uint lsl_pull_chunk_c(lsl_inlet @in, [NativeTypeName("char *")] sbyte* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("unsigned long")]
        public static partial uint lsl_pull_chunk_str(lsl_inlet @in, [NativeTypeName("char **")] sbyte** data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("uint32_t")]
        public static partial uint lsl_pull_chunk_str(lsl_inlet @in, IntPtr[] data_buffer, double* timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("unsigned long")]
        public static partial uint lsl_pull_chunk_buf(lsl_inlet @in, [NativeTypeName("char **")] sbyte** data_buffer, [NativeTypeName("uint32_t *")] uint* lengths_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("uint32_t")]
        public static partial uint lsl_pull_chunk_buf(lsl_inlet @in, IntPtr[] data_buffer, uint[] lengths_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("uint32_t")]
        public static partial uint lsl_samples_available(lsl_inlet @in);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("uint32_t")]
        public static partial uint lsl_inlet_flush(lsl_inlet @in);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("uint32_t")]
        public static partial uint lsl_was_clock_reset(lsl_inlet @in);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_smoothing_halftime(lsl_inlet @in, float value);
#else
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_str(lsl_inlet @in, IntPtr[] buffer, int buffer_elements, double timeout, int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_buf(lsl_inlet @in, IntPtr[] buffer, uint[] buffer_lengths, int buffer_elements, double timeout, int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double lsl_pull_sample_v(lsl_inlet @in, IntPtr buffer, int buffer_bytes, double timeout, int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint lsl_pull_chunk_str(lsl_inlet @in, IntPtr[] data_buffer, double* timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint lsl_pull_chunk_buf(lsl_inlet @in, IntPtr[] data_buffer, uint[] lengths_buffer, double[] timestamp_buffer, uint data_buffer_elements, uint timestamp_buffer_elements, double timeout, ref int ec);
#endif
    }
}
