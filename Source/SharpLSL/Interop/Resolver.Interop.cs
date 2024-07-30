using System;
using System.Runtime.InteropServices;

using lsl_continuous_resolver = System.IntPtr;

namespace SharpLSL.Interop
{
    public static unsafe partial class LSL
    {
        // TODO: Encoding
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern lsl_continuous_resolver lsl_create_continuous_resolver_byprop(
            [MarshalAs(UnmanagedType.LPStr)] string prop,
            [MarshalAs(UnmanagedType.LPStr)] string value,
            double forget_after);

        // TODO: Encoding
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern lsl_continuous_resolver lsl_create_continuous_resolver_bypred(
            [MarshalAs(UnmanagedType.LPStr)] string pred,
            double forget_after);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_resolver_results(lsl_continuous_resolver res, IntPtr[] buffer, uint buffer_elements);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_resolve_all(IntPtr[] buffer, uint buffer_elements, double wait_time);

        // TODO: Encoding
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_resolve_byprop(
            IntPtr[] buffer,
            uint buffer_elements,
            [MarshalAs(UnmanagedType.LPStr)] string prop,
            [MarshalAs(UnmanagedType.LPStr)] string value,
            int minimum,
            double timeout);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_resolve_bypred(
            IntPtr[] buffer,
            uint buffer_elements,
            [MarshalAs(UnmanagedType.LPStr)] string pred,
            int minimum,
            double timeout);
    }
}
