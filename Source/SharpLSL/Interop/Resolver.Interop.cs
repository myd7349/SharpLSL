using System;
using System.Runtime.InteropServices;

namespace SharpLSL.Interop
{
    public static unsafe partial class LSL
    {
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern IntPtr lsl_create_continuous_resolver_byprop(string prop, string value, double forget_after);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern IntPtr lsl_create_continuous_resolver_bypred(string pred, double forget_after);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_resolver_results([NativeTypeName("lsl_continuous_resolver")] IntPtr res, IntPtr[] buffer, [NativeTypeName("uint32_t")] uint buffer_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_resolve_all(IntPtr[] buffer, [NativeTypeName("uint32_t")] uint buffer_elements, double wait_time);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_resolve_byprop(IntPtr[] buffer, uint buffer_elements, string prop, string value, int minimum, double timeout);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_resolve_bypred(IntPtr[] buffer, uint buffer_elements, string pred, int minimum, double timeout);
    }
}
