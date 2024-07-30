using System;
using System.Runtime.InteropServices;

namespace SharpLSL.Interop
{
    public static unsafe partial class LSL
    {
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern IntPtr lsl_child([NativeTypeName("lsl_xml_ptr")] IntPtr e, string name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern IntPtr lsl_next_sibling_n([NativeTypeName("lsl_xml_ptr")] IntPtr e, string name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern IntPtr lsl_previous_sibling_n([NativeTypeName("lsl_xml_ptr")] IntPtr e, string name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern IntPtr lsl_child_value_n([NativeTypeName("lsl_xml_ptr")] IntPtr e, string name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern IntPtr lsl_append_child_value([NativeTypeName("lsl_xml_ptr")] IntPtr e, string name, string value);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern IntPtr lsl_prepend_child_value([NativeTypeName("lsl_xml_ptr")] IntPtr e, string name, string value);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_set_child_value([NativeTypeName("lsl_xml_ptr")] IntPtr e, string name, string value);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_set_name([NativeTypeName("lsl_xml_ptr")] IntPtr e, string rhs);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int lsl_set_value([NativeTypeName("lsl_xml_ptr")] IntPtr e, string rhs);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern IntPtr lsl_append_child([NativeTypeName("lsl_xml_ptr")] IntPtr e, string name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern IntPtr lsl_prepend_child([NativeTypeName("lsl_xml_ptr")] IntPtr e, string name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void lsl_remove_child_n([NativeTypeName("lsl_xml_ptr")] IntPtr e, string name);

        /*
 
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void lsl_remove_child([NativeTypeName("lsl_xml_ptr")] IntPtr e, [NativeTypeName("lsl_xml_ptr")] IntPtr e2);
        */
    }
}
