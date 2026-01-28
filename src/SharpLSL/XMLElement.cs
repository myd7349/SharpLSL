using System;
using System.Diagnostics;

using static SharpLSL.Interop.LSL;
using static SharpLSL.LSL;

namespace SharpLSL
{
    /// <summary>
    /// Represents a lightweight XML element tree that models the <see cref="StreamInfo.Description"/>  
    /// property of <see cref="StreamInfo"/>.
    /// </summary>
    /// <remarks>
    /// Each element has a name and can contain multiple named child elements or
    /// have text content as its value; attributes are omitted. The interface is
    /// modeled after a subset of pugixml's node type and is compatible with it.
    /// See <see href="https://pugixml.org/docs/manual.html#access"/> for more details.
    /// </remarks>
    // TODO: IEnumerable, ToString, C# XML libraries
    public class XMLElement
    {
        /// <summary>
        /// Constructs a new instance of the <see cref="XMLElement"/> class that
        /// wraps a pre-existing native XML node handle.
        /// </summary>
        /// <param name="handle">
        /// Specifies the handle to be wrapped.
        /// </param>
        internal XMLElement(IntPtr handle)
        {
            _handle = handle;
        }

        /// <summary>
        /// Gets a value indicating whether the element is empty.
        /// </summary>
        /// <seealso cref="Empty"/>
        public bool IsEmpty
        {
            get
            {
                return Convert.ToBoolean(lsl_empty(_handle));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this element is a text body (instead of
        /// an XML element). True for both plain character data and CData.
        /// </summary>
        public bool IsText
        {
            get
            {
                return Convert.ToBoolean(lsl_is_text(_handle));
            }
        }

        /// <summary>
        /// Gets or sets the name of the element.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when the value is invalid.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown when setting the element's name fails, either due to an empty node
        /// or insufficient memory.
        /// </exception>
        public string Name
        {
            get
            {
                unsafe
                {
                    return PtrToXmlString((IntPtr)lsl_name(_handle));
                }
            }

            set
            {
                var bytes = StringToBytes(value);
                if (bytes == null)
                    throw new ArgumentException(nameof(Name));

                bool result;
                unsafe
                {
                    fixed (byte* buffer = bytes)
                    {
                        result = Convert.ToBoolean(lsl_set_name(_handle, (sbyte*)buffer));
                    }
                }

                if (!result)
                    throw new LSLException($"Failed to set name of element to {value}.");
            }
        }

        /// <summary>
        /// Gets or sets the value of the element.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when the value is invalid.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown when setting the element's value fails, either due to an empty node
        /// or insufficient memory.
        /// </exception>
        public string Value
        {
            get
            {
                unsafe
                {
                    return PtrToXmlString((IntPtr)lsl_value(_handle));
                }
            }

            set
            {
                var bytes = StringToBytes(value);
                if (bytes == null)
                    throw new ArgumentException(nameof(Value));

                bool result;
                unsafe
                {
                    fixed (byte* buffer = bytes)
                    {
                        result = Convert.ToBoolean(lsl_set_value(_handle, (sbyte*)buffer));
                    }
                }

                if (!result)
                    throw new LSLException($"Failed to set value of element to {value}.");
            }
        }

        /// <summary>
        /// Gets the parent node of the element.
        /// </summary>
        /// <returns>
        /// The parent node of the element, or <see cref="Empty"/> if the parent node
        /// doesn't exist.
        /// </returns>
        public XMLElement Parent
        {
            get
            {
                var parent = lsl_parent(_handle);
                if (parent != IntPtr.Zero)
                    return new XMLElement(parent);

                return Empty;
            }
        }

        /// <summary>
        /// Gets the first child of the element.
        /// </summary>
        /// <returns>
        /// The first child of the element, or <see cref="Empty"/> if the element has
        /// no children.
        /// </returns>
        /// <seealso cref="LastChild"/>
        public XMLElement FirstChild()
        {
            var node = lsl_first_child(_handle);
            if (node != IntPtr.Zero)
                return new XMLElement(node);

            return Empty;
        }

        /// <summary>
        /// Gets the last child of the element.
        /// </summary>
        /// <returns>
        /// The last child of the element, or <see cref="Empty"/> if the element has
        /// no children.
        /// </returns>
        /// <seealso cref="FirstChild"/>
        public XMLElement LastChild()
        {
            var node = lsl_last_child(_handle);
            if (node != IntPtr.Zero)
                return new XMLElement(node);

            return Empty;
        }

        /// <summary>
        /// Gets a child of the element with the specified name.
        /// </summary>
        /// <param name="name">The name of the child.</param>
        /// <returns>
        /// A child of the element with the specified name, or <see cref="Empty"/>
        /// if no such child exists.
        /// </returns>
        public XMLElement Child(string name)
        {
            var bytes = StringToBytes(name);
            if (bytes == null)
                return Empty;

            unsafe
            {
                fixed (byte* buffer = bytes)
                {
                    var node = lsl_child(_handle, (sbyte*)buffer);
                    if (node != IntPtr.Zero)
                        return new XMLElement(node);
                }
            }

            return Empty;
        }

        /// <summary>
        /// Gets the value of the first child that is text.
        /// </summary>
        /// <returns>
        /// The value of the first child that is text, or <see cref="string.Empty"/>
        /// if no such child exists.
        /// </returns>
        /// <seealso cref="ChildValue(string)"/>
        public string ChildValue()
        {
            unsafe
            {
                var str = (IntPtr)lsl_child_value(_handle);
                if (str != IntPtr.Zero)
                    return PtrToXmlString(str);
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the value of the first child element with the specified name that
        /// contains text.
        /// </summary>
        /// <param name="name">The name of the child.</param>
        /// <returns>
        /// The value of the first child that is text, or empty string if no such child exists.
        /// </returns>
        /// <seealso cref="ChildValue()"/>
        /// <seealso cref="SetChildValue(string, string)"/>
        public string ChildValue(string name)
        {
            var bytes = StringToBytes(name);
            if (bytes == null)
                return string.Empty;

            unsafe
            {
                fixed (byte* buffer = bytes)
                {
                    var str = lsl_child_value_n(_handle, (sbyte*)buffer);
                    if (str != null)
                        return PtrToXmlString((IntPtr)str);
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Sets the text value of the (nameless) plain-text child of a named child node.
        /// </summary>
        /// <param name="name">The name of the child.</param>
        /// <param name="value">The value of the child.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if the name or value is invalid.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown if setting the child value fails.
        /// </exception>
        /// <seealso cref="ChildValue(string)"/>
        public void SetChildValue(string name, string value)
        {
            var nameBytes = StringToBytes(name);
            if (nameBytes == null)
                throw new ArgumentException(nameof(name));

            var valueBytes = StringToBytes(value);
            if (valueBytes == null)
                throw new ArgumentException(nameof(value));

            bool result;
            unsafe
            {
                fixed (byte* nameBuffer = nameBytes)
                fixed (byte* valueBuffer = valueBytes)
                {
                    result = Convert.ToBoolean(lsl_set_child_value(_handle, (sbyte*)nameBuffer, (sbyte*)valueBuffer));
                }
            }

            if (!result)
                throw new LSLException($"Failed to set child value: {name}={value}.");
        }

        /// <summary>
        /// Gets the next sibling of the element in the parent's child list.
        /// </summary>
        /// <returns>
        /// The next sibling of the element in the parent's child list, or
        /// <see cref="Empty"/> if the element is the last node in the list.
        /// </returns>
        /// <seealso cref="NextSibling(string)"/>
        /// <seealso cref="PreviousSibling()"/>
        public XMLElement NextSibling()
        {
            var node = lsl_next_sibling(_handle);
            if (node != IntPtr.Zero)
                return new XMLElement(node);

            return Empty;
        }

        /// <summary>
        /// Gets the next sibling of the element in the parent's child list with the
        /// specified name.
        /// </summary>
        /// <param name="name">The name of the sibling.</param>
        /// <returns>
        /// The next sibling of the element with the specified name, or <see cref="Empty"/>
        /// if no such sibling exists.
        /// </returns>
        /// <seealso cref="NextSibling()"/>
        /// <seealso cref="PreviousSibling(string)"/>
        public XMLElement NextSibling(string name)
        {
            var bytes = StringToBytes(name);
            if (bytes == null)
                return Empty;

            unsafe
            {
                fixed (byte* buffer = bytes)
                {
                    var node = lsl_next_sibling_n(_handle, (sbyte*)buffer);
                    if (node != IntPtr.Zero)
                        return new XMLElement(node);
                }
            }

            return Empty;
        }

        /// <summary>
        /// Gets the previous sibling of the element in the parent's child list.
        /// </summary>
        /// <returns>
        /// The previous sibling of the element in the parent's child list, or
        /// <see cref="Empty"/> if the element is the first node in the list.
        /// </returns>
        /// <seealso cref="PreviousSibling(string)"/>
        /// <seealso cref="NextSibling()"/>
        public XMLElement PreviousSibling()
        {
            var node = lsl_previous_sibling(_handle);
            if (node != IntPtr.Zero)
                return new XMLElement(node);

            return Empty;
        }

        /// <summary>
        /// Gets the previous sibling of the element in the parent's child list with the
        /// specified name.
        /// </summary>
        /// <param name="name">The name of the sibling.</param>
        /// <returns>
        /// The previous sibling of the element with the specified name, or <see cref="Empty"/>
        /// if no such sibling exists.
        /// </returns>
        /// <seealso cref="PreviousSibling()"/>
        /// <seealso cref="NextSibling(string)"/>
        public XMLElement PreviousSibling(string name)
        {
            var bytes = StringToBytes(name);
            if (bytes == null)
                return Empty;

            unsafe
            {
                fixed (byte* buffer = bytes)
                {
                    var node = lsl_previous_sibling_n(_handle, (sbyte*)buffer);
                    if (node != IntPtr.Zero)
                        return new XMLElement(node);
                }
            }

            return Empty;
        }

        /// <summary>
        /// Appends a new child element with the specified name to the current element.
        /// </summary>
        /// <param name="name">The name of the child element.</param>
        /// <returns>
        /// An <see cref="XMLElement"/> representing the newly created child element.
        /// </returns>
        /// <seealso cref="AppendChildValue(string, string)"/>
        /// <seealso cref="AppendChild(XMLElement)"/>
        /// <seealso cref="PrependChild(string)"/>
        public unsafe XMLElement AppendChild(string name)
        {
            var bytes = StringToBytes(name);
            fixed (byte* buffer = bytes)
            {
                return new XMLElement(lsl_append_child(_handle, (sbyte*)buffer));
            }
        }

        /// <summary>
        /// Appends a child element with a given name, which has a (nameless) plain-text
        /// child with the given text value.
        /// </summary>
        /// <param name="name">The name of the child.</param>
        /// <param name="value">The value of the child.</param>
        /// <returns>The current element.</returns>
        /// <seealso cref="AppendChild(string)"/>
        /// <seealso cref="PrependChildValue(string, string)"/>
        /// <seealso cref="AppendChild(XMLElement)"/>
        public unsafe XMLElement AppendChildValue(string name, string value)
        {
            var nameBytes = StringToBytes(name);
            var valueBytes = StringToBytes(value);

            fixed (byte* nameBuffer = nameBytes)
            fixed (byte* valueBuffer = valueBytes)
            {
                var node = lsl_append_child_value(_handle, (sbyte*)nameBuffer, (sbyte*)valueBuffer);
                Debug.Assert(node == _handle);
                return this;
            }
        }

        /// <summary>
        /// Appends a copy of the specified element as a child.
        /// </summary>
        /// <param name="element">The original element to be copied.</param>
        /// <returns>
        /// An <see cref="XMLElement"/> representing the newly created child element.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the element to be copied is null.
        /// </exception>
        /// <seealso cref="AppendChild(string)"/>
        /// <seealso cref="AppendChildValue(string, string)"/>
        /// <seealso cref="PrependChild(XMLElement)"/>
        public XMLElement AppendChild(XMLElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var node = lsl_append_copy(_handle, element._handle);
            if (node != IntPtr.Zero)
                return new XMLElement(node);

            return Empty;
        }

        /// <summary>
        /// Prepends a new child element with the specified name to the current element.
        /// </summary>
        /// <param name="name">The name of the child.</param>
        /// <returns>
        /// An <see cref="XMLElement"/> representing the newly created child element.
        /// </returns>
        /// <seealso cref="PrependChildValue(string, string)"/>
        /// <seealso cref="PrependChild(XMLElement)"/>
        /// <seealso cref="AppendChild(string)"/>
        public XMLElement PrependChild(string name)
        {
            var bytes = StringToBytes(name);
            unsafe
            {
                fixed (byte* buffer = bytes)
                {
                    return new XMLElement(lsl_prepend_child(_handle, (sbyte*)buffer));
                }
            }
        }

        /// <summary>
        /// Prepends a child element with a given name, which has a (nameless) plain-text
        /// child with the given text value.
        /// </summary>
        /// <param name="name">The name of the child element.</param>
        /// <param name="value">The text value of the child element.</param>
        /// <returns>The current element.</returns>
        /// <seealso cref="PrependChild(string)"/>
        /// <seealso cref="PrependChild(XMLElement)"/>
        /// <seealso cref="AppendChildValue(string, string)"/>
        public unsafe XMLElement PrependChildValue(string name, string value)
        {
            var nameBytes = StringToBytes(name);
            var valueBytes = StringToBytes(value);

            fixed (byte* nameBuffer = nameBytes)
            fixed (byte* valueBuffer = valueBytes)
            {
                var node = lsl_prepend_child_value(_handle, (sbyte*)nameBuffer, (sbyte*)valueBuffer);
                Debug.Assert(node == _handle);
                return this;
            }
        }

        /// <summary>
        /// Prepends a copy of the specified element as a child.
        /// </summary>
        /// <param name="element">The original element to be copied.</param>
        /// <returns>
        /// An <see cref="XMLElement"/> representing the newly created child element.
        /// </returns>
        /// <seealso cref="PrependChild(string)"/>
        /// <seealso cref="PrependChildValue(string, string)"/>
        /// <seealso cref="AppendChild(XMLElement)"/>
        public XMLElement PrependChild(XMLElement element) // TODO: Rename to AppendCopy, like the C++ API?
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var node = lsl_prepend_copy(_handle, element._handle);
            if (node != IntPtr.Zero)
                return new XMLElement(node);

            return Empty;
        }

        /// <summary>
        /// Removes a child of the element with the specified name.
        /// </summary>
        /// <param name="name">The name of the child.</param>
        /// <seealso cref="RemoveChild(XMLElement)"/>
        public void RemoveChild(string name)
        {
            var bytes = StringToBytes(name);
            unsafe
            {
                fixed (byte* buffer = bytes)
                {
                    lsl_remove_child_n(_handle, (sbyte*)buffer);
                }
            }
        }

        /// <summary>
        /// Removes a specified child element.
        /// </summary>
        /// <param name="element">The child element to be removed.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the element to be removed is null.
        /// </exception>
        /// <seealso cref="RemoveChild(string)"/>
        public void RemoveChild(XMLElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            lsl_remove_child(_handle, element._handle);
        }

        /// <summary>
        /// Represents a empty XML element.
        /// </summary>
        /// <remarks>
        /// The traversal functions provided by <see cref="XMLElement"/> may return
        /// this value if no element is found.
        /// </remarks>
        /// <seealso cref="IsEmpty"/>
        public static readonly XMLElement Empty = new XMLElement(IntPtr.Zero);

        private readonly IntPtr _handle;
    }
}


// References:
// https://github.com/labstreaminglayer/liblsl-Csharp/blob/master/LSL.cs
// https://pugixml.org/docs/manual.html#access
// https://github.com/zeux/pugixml/blob/master/src/pugixml.hpp
