using System;
using System.Runtime.InteropServices;

using static SharpLSL.Interop.LSL;

namespace SharpLSL
{
    public class XMLElement
    {
        public XMLElement(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
                throw new ArgumentException("Invalid Xml node pointer.");

            handle_ = handle;
        }

        public XMLElement FirstChild() => new XMLElement(lsl_first_child(handle_));

        public XMLElement LastChild() => new XMLElement(lsl_last_child(handle_));

        public XMLElement Child(string name) => new XMLElement(lsl_child(handle_, name));

        public XMLElement NextSibling() => new XMLElement(lsl_next_sibling(handle_));

        public XMLElement NextSibling(string name) => new XMLElement(lsl_next_sibling_n(handle_, name));

        public XMLElement PreviousSibling() => new XMLElement(lsl_previous_sibling(handle_));

        public XMLElement PreviousSibling(string name) => new XMLElement(lsl_previous_sibling_n(handle_, name));

        public XMLElement Parent() => new XMLElement(lsl_parent(handle_));

        public bool IsEmpty => lsl_empty(handle_) != 0;

        public bool IsText => lsl_is_text(handle_) != 0;

        public unsafe string Name => Marshal.PtrToStringAnsi((IntPtr)lsl_name(handle_));

        public unsafe string Value => Marshal.PtrToStringAnsi((IntPtr)lsl_value(handle_));

        public unsafe string ChildValue() => Marshal.PtrToStringAnsi((IntPtr)lsl_child_value(handle_));

        public unsafe string ChildValue(string name) => Marshal.PtrToStringAnsi((IntPtr)lsl_child_value_n(handle_, name));

        public XMLElement AppendChildValue(string name, string value)
        {
            return new XMLElement(lsl_append_child_value(handle_, name, value));
        }

        public XMLElement PrependChildValue(string name, string value)
        {
            return new XMLElement(lsl_prepend_child_value(handle_, name, value));
        }

        public int SetChildValue(string name, string value)
        {
            return lsl_set_child_value(handle_, name, value);
        }

        public int SetName(string name)
        {
            return lsl_set_name(handle_, name);
        }

        public int SetValue(string value)
        {
            return lsl_set_value(handle_, value);
        }

        public XMLElement AppendChild(string name)
        {
            return new XMLElement(lsl_append_child(handle_, name));
        }

        public XMLElement PrependChild(string name)
        {
            return new XMLElement(lsl_prepend_child(handle_, name));
        }

        public XMLElement AppendCopy(XMLElement element)
        {
            return new XMLElement(lsl_append_copy(handle_, element.handle_));
        }

        public XMLElement PrependCopy(XMLElement element)
        {
            return new XMLElement(lsl_prepend_copy(handle_, element.handle_));
        }

        public void RemoveChild(string name)
        {
            lsl_remove_child_n(handle_, name);
        }

        public void RemoveChild(XMLElement element)
        {
            lsl_remove_child(handle_, element.handle_);
        }

        private IntPtr handle_;
    }
}
