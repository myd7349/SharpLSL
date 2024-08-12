#pragma warning disable CS1591
using System.Runtime.InteropServices;

using lsl_xml_ptr = System.IntPtr;

namespace SharpLSL.Interop
{
    // TODO: Encoding
    public static unsafe partial class LSL
    {
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_set_name(
            lsl_xml_ptr e, [MarshalAs(UnmanagedType.LPStr)] string rhs);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_set_value(
            lsl_xml_ptr e, [MarshalAs(UnmanagedType.LPStr)] string rhs);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern lsl_xml_ptr lsl_child(
            lsl_xml_ptr e, [MarshalAs(UnmanagedType.LPStr)] string name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern lsl_xml_ptr lsl_child_value_n(
            lsl_xml_ptr e, [MarshalAs(UnmanagedType.LPStr)] string name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_set_child_value(
            lsl_xml_ptr e, [MarshalAs(UnmanagedType.LPStr)] string name, [MarshalAs(UnmanagedType.LPStr)] string value);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern lsl_xml_ptr lsl_next_sibling_n(
            lsl_xml_ptr e, [MarshalAs(UnmanagedType.LPStr)] string name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern lsl_xml_ptr lsl_previous_sibling_n(
            lsl_xml_ptr e, [MarshalAs(UnmanagedType.LPStr)] string name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern lsl_xml_ptr lsl_append_child(
            lsl_xml_ptr e, [MarshalAs(UnmanagedType.LPStr)] string name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern lsl_xml_ptr lsl_append_child_value(
            lsl_xml_ptr e, [MarshalAs(UnmanagedType.LPStr)] string name, [MarshalAs(UnmanagedType.LPStr)] string value);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern lsl_xml_ptr lsl_prepend_child(
            lsl_xml_ptr e, [MarshalAs(UnmanagedType.LPStr)] string name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern lsl_xml_ptr lsl_prepend_child_value(
            lsl_xml_ptr e, [MarshalAs(UnmanagedType.LPStr)] string name, [MarshalAs(UnmanagedType.LPStr)] string value);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void lsl_remove_child_n(
            lsl_xml_ptr e, [MarshalAs(UnmanagedType.LPStr)] string name);
    }
}
