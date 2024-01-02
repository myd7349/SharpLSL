/*
 * This file is generated using ClangSharpPInvokeGenerator.
 * Please refrain from manual modifications.
 * If changes are necessary, modify generate.ps1 and regenerate the file.
 */

using System;
using System.Runtime.InteropServices;

namespace SharpLSL.Interop
{
    public static unsafe partial class Inlet
    {
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_create_inlet@@YAPEAUlsl_inlet_struct_@@PEAUlsl_streaminfo_struct_@@HHH@Z", ExactSpelling = true)]
        [return: NativeTypeName("lsl_inlet")]
        public static extern IntPtr lsl_create_inlet([NativeTypeName("lsl_streaminfo")] IntPtr info, [NativeTypeName("int32_t")] int max_buflen, [NativeTypeName("int32_t")] int max_chunklen, [NativeTypeName("int32_t")] int recover);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_create_inlet_ex@@YAPEAUlsl_inlet_struct_@@PEAUlsl_streaminfo_struct_@@HHHW4lsl_transport_options_t@@@Z", ExactSpelling = true)]
        [return: NativeTypeName("lsl_inlet")]
        public static extern IntPtr lsl_create_inlet_ex([NativeTypeName("lsl_streaminfo")] IntPtr info, [NativeTypeName("int32_t")] int max_buflen, [NativeTypeName("int32_t")] int max_chunklen, [NativeTypeName("int32_t")] int recover, lsl_transport_options_t flags);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_destroy_inlet@@YAXPEAUlsl_inlet_struct_@@@Z", ExactSpelling = true)]
        public static extern void lsl_destroy_inlet([NativeTypeName("lsl_inlet")] IntPtr @in);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_get_fullinfo@@YAPEAUlsl_streaminfo_struct_@@PEAUlsl_inlet_struct_@@NPEAH@Z", ExactSpelling = true)]
        [return: NativeTypeName("lsl_streaminfo")]
        public static extern IntPtr lsl_get_fullinfo([NativeTypeName("lsl_inlet")] IntPtr @in, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_open_stream@@YAXPEAUlsl_inlet_struct_@@NPEAH@Z", ExactSpelling = true)]
        public static extern void lsl_open_stream([NativeTypeName("lsl_inlet")] IntPtr @in, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_close_stream@@YAXPEAUlsl_inlet_struct_@@@Z", ExactSpelling = true)]
        public static extern void lsl_close_stream([NativeTypeName("lsl_inlet")] IntPtr @in);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_time_correction@@YANPEAUlsl_inlet_struct_@@NPEAH@Z", ExactSpelling = true)]
        public static extern double lsl_time_correction([NativeTypeName("lsl_inlet")] IntPtr @in, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_time_correction_ex@@YANPEAUlsl_inlet_struct_@@PEAN1NPEAH@Z", ExactSpelling = true)]
        public static extern double lsl_time_correction_ex([NativeTypeName("lsl_inlet")] IntPtr @in, double* remote_time, double* uncertainty, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_set_postprocessing@@YAHPEAUlsl_inlet_struct_@@I@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_set_postprocessing([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("uint32_t")] uint flags);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_sample_f@@YANPEAUlsl_inlet_struct_@@PEAMHNPEAH@Z", ExactSpelling = true)]
        public static extern double lsl_pull_sample_f([NativeTypeName("lsl_inlet")] IntPtr @in, float* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_sample_d@@YANPEAUlsl_inlet_struct_@@PEANHNPEAH@Z", ExactSpelling = true)]
        public static extern double lsl_pull_sample_d([NativeTypeName("lsl_inlet")] IntPtr @in, double* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_sample_l@@YANPEAUlsl_inlet_struct_@@PEA_JHNPEAH@Z", ExactSpelling = true)]
        public static extern double lsl_pull_sample_l([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("int64_t *")] long* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_sample_i@@YANPEAUlsl_inlet_struct_@@PEAHHN1@Z", ExactSpelling = true)]
        public static extern double lsl_pull_sample_i([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("int32_t *")] int* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_sample_s@@YANPEAUlsl_inlet_struct_@@PEAFHNPEAH@Z", ExactSpelling = true)]
        public static extern double lsl_pull_sample_s([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("int16_t *")] short* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_sample_c@@YANPEAUlsl_inlet_struct_@@PEADHNPEAH@Z", ExactSpelling = true)]
        public static extern double lsl_pull_sample_c([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("char *")] sbyte* buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_sample_str@@YANPEAUlsl_inlet_struct_@@PEAPEADHNPEAH@Z", ExactSpelling = true)]
        public static extern double lsl_pull_sample_str([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("char **")] sbyte** buffer, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_sample_buf@@YANPEAUlsl_inlet_struct_@@PEAPEADPEAIHNPEAH@Z", ExactSpelling = true)]
        public static extern double lsl_pull_sample_buf([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("char **")] sbyte** buffer, [NativeTypeName("uint32_t *")] uint* buffer_lengths, [NativeTypeName("int32_t")] int buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_sample_v@@YANPEAUlsl_inlet_struct_@@PEAXHNPEAH@Z", ExactSpelling = true)]
        public static extern double lsl_pull_sample_v([NativeTypeName("lsl_inlet")] IntPtr @in, void* buffer, [NativeTypeName("int32_t")] int buffer_bytes, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_chunk_f@@YAKPEAUlsl_inlet_struct_@@PEAMPEANKKNPEAH@Z", ExactSpelling = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern uint lsl_pull_chunk_f([NativeTypeName("lsl_inlet")] IntPtr @in, float* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_chunk_d@@YAKPEAUlsl_inlet_struct_@@PEAN1KKNPEAH@Z", ExactSpelling = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern uint lsl_pull_chunk_d([NativeTypeName("lsl_inlet")] IntPtr @in, double* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_chunk_l@@YAKPEAUlsl_inlet_struct_@@PEA_JPEANKKNPEAH@Z", ExactSpelling = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern uint lsl_pull_chunk_l([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("int64_t *")] long* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_chunk_i@@YAKPEAUlsl_inlet_struct_@@PEAHPEANKKN1@Z", ExactSpelling = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern uint lsl_pull_chunk_i([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("int32_t *")] int* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_chunk_s@@YAKPEAUlsl_inlet_struct_@@PEAFPEANKKNPEAH@Z", ExactSpelling = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern uint lsl_pull_chunk_s([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("int16_t *")] short* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_chunk_c@@YAKPEAUlsl_inlet_struct_@@PEADPEANKKNPEAH@Z", ExactSpelling = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern uint lsl_pull_chunk_c([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("char *")] sbyte* data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_chunk_str@@YAKPEAUlsl_inlet_struct_@@PEAPEADPEANKKNPEAH@Z", ExactSpelling = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern uint lsl_pull_chunk_str([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("char **")] sbyte** data_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_pull_chunk_buf@@YAKPEAUlsl_inlet_struct_@@PEAPEADPEAIPEANKKNPEAH@Z", ExactSpelling = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern uint lsl_pull_chunk_buf([NativeTypeName("lsl_inlet")] IntPtr @in, [NativeTypeName("char **")] sbyte** data_buffer, [NativeTypeName("uint32_t *")] uint* lengths_buffer, double* timestamp_buffer, [NativeTypeName("unsigned long")] uint data_buffer_elements, [NativeTypeName("unsigned long")] uint timestamp_buffer_elements, double timeout, [NativeTypeName("int32_t *")] int* ec);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_samples_available@@YAIPEAUlsl_inlet_struct_@@@Z", ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint lsl_samples_available([NativeTypeName("lsl_inlet")] IntPtr @in);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_inlet_flush@@YAIPEAUlsl_inlet_struct_@@@Z", ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint lsl_inlet_flush([NativeTypeName("lsl_inlet")] IntPtr @in);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_was_clock_reset@@YAIPEAUlsl_inlet_struct_@@@Z", ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint lsl_was_clock_reset([NativeTypeName("lsl_inlet")] IntPtr @in);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?lsl_smoothing_halftime@@YAHPEAUlsl_inlet_struct_@@M@Z", ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_smoothing_halftime([NativeTypeName("lsl_inlet")] IntPtr @in, float value);
    }
}
