/*
 * This file is generated using ClangSharpPInvokeGenerator.
 * Please refrain from manual modifications.
 * If changes are necessary, modify generate.ps1 and regenerate the file.
 */

using System;
using System.Runtime.InteropServices;

namespace SharpLSL.Interop
{
    public static unsafe partial class LSL
    {
        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_xml_ptr")]
        public static extern IntPtr lsl_first_child([NativeTypeName("lsl_xml_ptr")] IntPtr e);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_xml_ptr")]
        public static extern IntPtr lsl_last_child([NativeTypeName("lsl_xml_ptr")] IntPtr e);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_xml_ptr")]
        public static extern IntPtr lsl_next_sibling([NativeTypeName("lsl_xml_ptr")] IntPtr e);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_xml_ptr")]
        public static extern IntPtr lsl_previous_sibling([NativeTypeName("lsl_xml_ptr")] IntPtr e);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_xml_ptr")]
        public static extern IntPtr lsl_parent([NativeTypeName("lsl_xml_ptr")] IntPtr e);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_xml_ptr")]
        public static extern IntPtr lsl_child([NativeTypeName("lsl_xml_ptr")] IntPtr e, [NativeTypeName("const char *")] sbyte* name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_xml_ptr")]
        public static extern IntPtr lsl_next_sibling_n([NativeTypeName("lsl_xml_ptr")] IntPtr e, [NativeTypeName("const char *")] sbyte* name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_xml_ptr")]
        public static extern IntPtr lsl_previous_sibling_n([NativeTypeName("lsl_xml_ptr")] IntPtr e, [NativeTypeName("const char *")] sbyte* name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_empty([NativeTypeName("lsl_xml_ptr")] IntPtr e);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_is_text([NativeTypeName("lsl_xml_ptr")] IntPtr e);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* lsl_name([NativeTypeName("lsl_xml_ptr")] IntPtr e);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* lsl_value([NativeTypeName("lsl_xml_ptr")] IntPtr e);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* lsl_child_value([NativeTypeName("lsl_xml_ptr")] IntPtr e);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* lsl_child_value_n([NativeTypeName("lsl_xml_ptr")] IntPtr e, [NativeTypeName("const char *")] sbyte* name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_xml_ptr")]
        public static extern IntPtr lsl_append_child_value([NativeTypeName("lsl_xml_ptr")] IntPtr e, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const char *")] sbyte* value);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_xml_ptr")]
        public static extern IntPtr lsl_prepend_child_value([NativeTypeName("lsl_xml_ptr")] IntPtr e, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const char *")] sbyte* value);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_set_child_value([NativeTypeName("lsl_xml_ptr")] IntPtr e, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const char *")] sbyte* value);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_set_name([NativeTypeName("lsl_xml_ptr")] IntPtr e, [NativeTypeName("const char *")] sbyte* rhs);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int lsl_set_value([NativeTypeName("lsl_xml_ptr")] IntPtr e, [NativeTypeName("const char *")] sbyte* rhs);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_xml_ptr")]
        public static extern IntPtr lsl_append_child([NativeTypeName("lsl_xml_ptr")] IntPtr e, [NativeTypeName("const char *")] sbyte* name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_xml_ptr")]
        public static extern IntPtr lsl_prepend_child([NativeTypeName("lsl_xml_ptr")] IntPtr e, [NativeTypeName("const char *")] sbyte* name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_xml_ptr")]
        public static extern IntPtr lsl_append_copy([NativeTypeName("lsl_xml_ptr")] IntPtr e, [NativeTypeName("lsl_xml_ptr")] IntPtr e2);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("lsl_xml_ptr")]
        public static extern IntPtr lsl_prepend_copy([NativeTypeName("lsl_xml_ptr")] IntPtr e, [NativeTypeName("lsl_xml_ptr")] IntPtr e2);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void lsl_remove_child_n([NativeTypeName("lsl_xml_ptr")] IntPtr e, [NativeTypeName("const char *")] sbyte* name);

        [DllImport("lsl", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void lsl_remove_child([NativeTypeName("lsl_xml_ptr")] IntPtr e, [NativeTypeName("lsl_xml_ptr")] IntPtr e2);
    }
}
