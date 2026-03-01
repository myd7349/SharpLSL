#pragma warning disable CS1591
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using lsl_outlet = System.IntPtr;

namespace SharpLSL.Interop
{
    public static unsafe partial class LSL
    {
#if USE_LIBRARY_IMPORT
        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("lsl_outlet")]
        public static partial IntPtr lsl_create_outlet([NativeTypeName("lsl_streaminfo")] IntPtr info, [NativeTypeName("int32_t")] int chunk_size, [NativeTypeName("int32_t")] int max_buffered);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("lsl_outlet")]
        public static partial IntPtr lsl_create_outlet_ex([NativeTypeName("lsl_streaminfo")] IntPtr info, [NativeTypeName("int32_t")] int chunk_size, [NativeTypeName("int32_t")] int max_buffered, lsl_transport_options_t flags);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial void lsl_destroy_outlet([NativeTypeName("lsl_outlet")] IntPtr @out);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_f([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const float *")] float* data);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_d([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const double *")] double* data);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_l([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int64_t *")] long* data);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_i([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int32_t *")] int* data);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_s([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int16_t *")] short* data);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_c([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char *")] sbyte* data);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_str([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_v([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const void *")] void* data);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_ft([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const float *")] float* data, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_dt([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const double *")] double* data, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_lt([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int64_t *")] long* data, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_it([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int32_t *")] int* data, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_st([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int16_t *")] short* data, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_ct([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char *")] sbyte* data, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_strt([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_vt([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const void *")] void* data, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_ftp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const float *")] float* data, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_dtp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const double *")] double* data, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_ltp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int64_t *")] long* data, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_itp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int32_t *")] int* data, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_stp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int16_t *")] short* data, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_ctp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char *")] sbyte* data, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_strtp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial int lsl_push_sample_strtp(lsl_outlet @out, IntPtr data, double timestamp, int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial int lsl_push_sample_strtp(lsl_outlet @out, IntPtr[] data, double timestamp, int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_vtp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const void *")] void* data, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial int lsl_push_sample_vtp(lsl_outlet @out, byte[] data, double timestamp, int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial int lsl_push_sample_vtp(lsl_outlet @out, IntPtr data, double timestamp, int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_buf([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_buft([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_sample_buftp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial int lsl_push_sample_buftp(lsl_outlet @out, IntPtr data, uint* lengths, double timestamp, int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial int lsl_push_sample_buftp(lsl_outlet @out, IntPtr[] data, uint* lengths, double timestamp, int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_f([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const float *")] float* data, [NativeTypeName("unsigned long")] uint data_elements);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_d([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const double *")] double* data, [NativeTypeName("unsigned long")] uint data_elements);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_l([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int64_t *")] long* data, [NativeTypeName("unsigned long")] uint data_elements);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_i([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int32_t *")] int* data, [NativeTypeName("unsigned long")] uint data_elements);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_s([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int16_t *")] short* data, [NativeTypeName("unsigned long")] uint data_elements);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_c([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char *")] sbyte* data, [NativeTypeName("unsigned long")] uint data_elements);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_str([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("unsigned long")] uint data_elements);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_ft([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const float *")] float* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_dt([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const double *")] double* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_lt([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int64_t *")] long* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_it([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int32_t *")] int* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_st([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int16_t *")] short* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_ct([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char *")] sbyte* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_strt([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_ftp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const float *")] float* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_dtp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const double *")] double* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_ltp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int64_t *")] long* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_itp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int32_t *")] int* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_stp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int16_t *")] short* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_ctp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char *")] sbyte* data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_strtp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial int lsl_push_chunk_strtp(lsl_outlet @out, IntPtr data, uint data_elements, double timestamp, int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial int lsl_push_chunk_strtp(lsl_outlet @out, IntPtr[] data, uint data_elements, double timestamp, int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_ftn([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const float *")] float* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_dtn([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const double *")] double* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_ltn([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int64_t *")] long* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_itn([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int32_t *")] int* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_stn([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int16_t *")] short* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_ctn([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char *")] sbyte* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_strtn([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_ftnp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const float *")] float* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_dtnp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const double *")] double* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_ltnp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int64_t *")] long* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_itnp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int32_t *")] int* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_stnp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const int16_t *")] short* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_ctnp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char *")] sbyte* data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_strtnp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial int lsl_push_chunk_strtnp(lsl_outlet @out, IntPtr data, uint data_elements, double* timestamps, int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial int lsl_push_chunk_strtnp(lsl_outlet @out, IntPtr[] data, uint data_elements, double* timestamps, int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_buf([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, [NativeTypeName("unsigned long")] uint data_elements);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_buft([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, [NativeTypeName("unsigned long")] uint data_elements, double timestamp);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_buftp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, [NativeTypeName("unsigned long")] uint data_elements, double timestamp, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial int lsl_push_chunk_buftp(lsl_outlet @out, IntPtr data, uint* lengths, uint data_elements, double timestamp, int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial int lsl_push_chunk_buftp(lsl_outlet @out, IntPtr[] data, uint* lengths, uint data_elements, double timestamp, int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_buftn([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_push_chunk_buftnp([NativeTypeName("lsl_outlet")] IntPtr @out, [NativeTypeName("const char **")] sbyte** data, [NativeTypeName("const uint32_t *")] uint* lengths, [NativeTypeName("unsigned long")] uint data_elements, [NativeTypeName("const double *")] double* timestamps, [NativeTypeName("int32_t")] int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial int lsl_push_chunk_buftnp(lsl_outlet @out, IntPtr data, uint* lengths, uint data_elements, double[] timestamps, int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial int lsl_push_chunk_buftnp(lsl_outlet @out, IntPtr[] data, uint* lengths, uint data_elements, double[] timestamps, int pushthrough);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_have_consumers([NativeTypeName("lsl_outlet")] IntPtr @out);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_wait_for_consumers([NativeTypeName("lsl_outlet")] IntPtr @out, double timeout);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("lsl_streaminfo")]
        public static partial IntPtr lsl_get_info([NativeTypeName("lsl_outlet")] IntPtr @out);
#else
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
#endif
    }
}


// References:
// [Marshal an array of strings from C# to C code using p/invoke](https://stackoverflow.com/questions/13317931/marshal-an-array-of-strings-from-c-sharp-to-c-code-using-p-invoke)
