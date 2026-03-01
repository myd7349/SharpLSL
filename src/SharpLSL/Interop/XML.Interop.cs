#if USE_LIBRARY_IMPORT
#pragma warning disable CS1591

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using lsl_xml_ptr = System.IntPtr;

namespace SharpLSL.Interop
{
    public static partial class LSL
    {
        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_xml_ptr lsl_first_child(lsl_xml_ptr e);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_xml_ptr lsl_last_child(lsl_xml_ptr e);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_xml_ptr lsl_next_sibling(lsl_xml_ptr e);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_xml_ptr lsl_previous_sibling(lsl_xml_ptr e);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_xml_ptr lsl_parent(lsl_xml_ptr e);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_xml_ptr lsl_child(lsl_xml_ptr e, [NativeTypeName("const char *")] IntPtr name);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_xml_ptr lsl_next_sibling_n(lsl_xml_ptr e, [NativeTypeName("const char *")] IntPtr name);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_xml_ptr lsl_previous_sibling_n(lsl_xml_ptr e, [NativeTypeName("const char *")] IntPtr name);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_empty(lsl_xml_ptr e);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_is_text(lsl_xml_ptr e);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("const char *")]
        public static partial IntPtr lsl_name(lsl_xml_ptr e);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("const char *")]
        public static partial IntPtr lsl_value(lsl_xml_ptr e);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("const char *")]
        public static partial IntPtr lsl_child_value(lsl_xml_ptr e);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("const char *")]
        public static partial IntPtr lsl_child_value_n(lsl_xml_ptr e, [NativeTypeName("const char *")] IntPtr name);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_xml_ptr lsl_append_child_value(lsl_xml_ptr e, [NativeTypeName("const char *")] IntPtr name, [NativeTypeName("const char *")] IntPtr value);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_xml_ptr lsl_prepend_child_value(lsl_xml_ptr e, [NativeTypeName("const char *")] IntPtr name, [NativeTypeName("const char *")] IntPtr value);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_set_child_value(lsl_xml_ptr e, [NativeTypeName("const char *")] IntPtr name, [NativeTypeName("const char *")] IntPtr value);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_set_name(lsl_xml_ptr e, [NativeTypeName("const char *")] IntPtr rhs);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: NativeTypeName("int32_t")]
        public static partial int lsl_set_value(lsl_xml_ptr e, [NativeTypeName("const char *")] IntPtr rhs);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_xml_ptr lsl_append_child(lsl_xml_ptr e, [NativeTypeName("const char *")] IntPtr name);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_xml_ptr lsl_prepend_child(lsl_xml_ptr e, [NativeTypeName("const char *")] IntPtr name);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_xml_ptr lsl_append_copy(lsl_xml_ptr e, lsl_xml_ptr e2);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial lsl_xml_ptr lsl_prepend_copy(lsl_xml_ptr e, lsl_xml_ptr e2);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial void lsl_remove_child_n(lsl_xml_ptr e, [NativeTypeName("const char *")] IntPtr name);

        [LibraryImport("lsl")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial void lsl_remove_child(lsl_xml_ptr e, lsl_xml_ptr e2);
    }
}
#endif
