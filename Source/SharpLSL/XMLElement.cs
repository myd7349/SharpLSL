using System;
using System.Diagnostics;

using static SharpLSL.Interop.LSL;
using static SharpLSL.LSL;

namespace SharpLSL
{
    /// <summary>
    /// Represents a lightweight XML element tree, modeling the <see cref="StreamInfo.Description"/>
    /// field of <see cref="StreamInfo"/>.
    /// </summary>
    /// <remarks>
    /// Each element has a name and can contain multiple named child elements or
    /// have text content as its value; attributes are omitted. The interface is
    /// modeled after a subset of pugixml's node type and is compatible with it.
    /// See https://pugixml.org/docs/manual.html#access for more details.
    /// </remarks>
    public class XMLElement // TODO: IEnumerable, ToString, C# XML libraries
    {
        /// <summary>
        /// Constructs a new instance of the <see cref="XMLElement"/> class which
        /// wraps a pre-existing native XML node handle.
        /// </summary>
        /// <param name="handle">
        /// Specifies the handle to be wrapped.
        /// </param>
        internal XMLElement(IntPtr handle)
        {
            handle_ = handle;
        }

        /// <summary>
        /// Gets a value indicating whether the element is null.
        /// </summary>
        /// <seealso cref="Null"/>
        public bool IsNull => handle_ == IntPtr.Zero;

        /// <summary>
        /// Gets a value indicating whether the element is empty.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the handle is invalid.
        /// </exception>
        public bool IsEmpty
        {
            get
            {
                ThrowIfInvalid();
                return Convert.ToBoolean(lsl_empty(handle_));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this element is a text body (instead of
        /// an XML element). True both for plain char data and CData.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the handle is invalid.
        /// </exception>
        public bool IsText
        {
            get
            {
                ThrowIfInvalid();
                return Convert.ToBoolean(lsl_is_text(handle_));
            }
        }

        /// <summary>
        /// Gets or sets the name of the element.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the handle is invalid.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown if setting name of the element fails.
        /// </exception>
        public string Name
        {
            get
            {
                ThrowIfInvalid();

                unsafe
                {
                    return PtrToXmlString((IntPtr)lsl_name(handle_));
                }
            }

            set
            {
                ThrowIfInvalid();

                var result = Convert.ToBoolean(lsl_set_name(handle_, value));
                if (!result)
                    throw new LSLException($"Failed to set name of element to {value}.");
            }
        }

        /// <summary>
        /// Gets or sets the value of the element.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the handle is invalid.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown if setting value of the element fails.
        /// </exception>
        public string Value
        {
            get
            {
                ThrowIfInvalid();

                unsafe
                {
                    return PtrToXmlString((IntPtr)lsl_value(handle_));
                }
            }

            set
            {
                ThrowIfInvalid();

                var result = Convert.ToBoolean(lsl_set_value(handle_, value));
                if (!result)
                    throw new LSLException($"Failed to set value of element to {value}.");
            }
        }

        /// <summary>
        /// Gets the parent node of the element.
        /// </summary>
        /// <returns>
        /// The parent node of the element, or <see cref="Null"/> if the parent node
        /// doesn't exist.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element is invalid.
        /// </exception>
        public XMLElement GetParent()
        {
            ThrowIfInvalid();

            var parent = lsl_parent(handle_);
            if (parent != IntPtr.Zero)
                return new XMLElement(parent);

            return Null;
        }

        /// <summary>
        /// Finds the first child of the element.
        /// </summary>
        /// <returns>
        /// The first child of the element, or <see cref="Null"/> if the element has
        /// no children.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element is invalid.
        /// </exception>
        public XMLElement FindFirstChild()
        {
            ThrowIfInvalid();

            var node = lsl_first_child(handle_);
            if (node != IntPtr.Zero)
                return new XMLElement(node);

            return Null;
        }

        /// <summary>
        /// Finds the last child of the element.
        /// </summary>
        /// <returns>
        /// The last child of the element, or <see cref="Null"/> if the element has
        /// no children.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element is invalid.
        /// </exception>
        public XMLElement FindLastChild()
        {
            ThrowIfInvalid();

            var node = lsl_last_child(handle_);
            if (node != IntPtr.Zero)
                return new XMLElement(node);

            return Null;
        }

        /// <summary>
        /// Finds a child of the element with the specified name.
        /// </summary>
        /// <param name="name">The name of the child.</param>
        /// <returns>
        /// A child of the element with the specified name, or <see cref="Null"/>
        /// if no such child exists.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element is invalid.
        /// </exception>
        public XMLElement FindChild(string name)
        {
            ThrowIfInvalid();

            var node = lsl_child(handle_, name);
            if (node != IntPtr.Zero)
                return new XMLElement(node);

            return Null;
        }

        /// <summary>
        /// Gets the value of the first child that is text.
        /// </summary>
        /// <returns>
        /// The value of the first child that is text, or null if no such child exists.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element is invalid.
        /// </exception>
        public string GetChildValue()
        {
            ThrowIfInvalid();

            unsafe
            {
                var str = (IntPtr)lsl_child_value(handle_);
                if (str != IntPtr.Zero) // TODO:
                    return PtrToXmlString(str);
            }

            return null;
        }

        /// <summary>
        /// Gets the value of the first child element with the specified name that
        /// contains text.
        /// </summary>
        /// <param name="name">The name of the child.</param>
        /// <returns>
        /// The value of the first child that is text, or null if no such child exists.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element is invalid.
        /// </exception>
        public string GetChildValue(string name)
        {
            ThrowIfInvalid();

            var str = lsl_child_value_n(handle_, name);
            if (str != IntPtr.Zero) // TODO:
                return PtrToXmlString(str);

            return null;
        }

        /// <summary>
        /// Sets the text value of the (nameless) plain-text child of a named child node.
        /// </summary>
        /// <param name="name">The name of the child.</param>
        /// <param name="value">The value of the child.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element is invalid.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown if setting child value fails.
        /// </exception>
        public void SetChildValue(string name, string value)
        {
            ThrowIfInvalid();

            var result = Convert.ToBoolean(lsl_set_child_value(handle_, name, value));
            if (!result)
                throw new LSLException($"Failed to set child value: {name}={value}.");
        }

        /// <summary>
        /// Finds the next sibling of the element.
        /// </summary>
        /// <returns>
        /// The next sibling of the element, or <see cref="Null"/> if the element
        /// is the last node in the list.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element is invalid.
        /// </exception>
        public XMLElement FindNextSibling()
        {
            ThrowIfInvalid();

            var node = lsl_next_sibling(handle_);
            if (node != IntPtr.Zero)
                return new XMLElement(node);

            return Null;
        }

        /// <summary>
        /// Finds the next sibling of the element with the specified name.
        /// </summary>
        /// <param name="name">The name of the sibling.</param>
        /// <returns>
        /// The next sibling of the element with the specified name, or <see cref="Null"/>
        /// if no such sibling exists.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element is invalid.
        /// </exception>
        public XMLElement FindNextSibling(string name)
        {
            ThrowIfInvalid();

            var node = lsl_next_sibling_n(handle_, name);
            if (node != IntPtr.Zero)
                return new XMLElement(node);

            return Null;
        }

        /// <summary>
        /// Finds the previous sibling of the element.
        /// </summary>
        /// <returns>
        /// The previous sibling of the element, or <see cref="Null"/> if the element
        /// is the first node in the list.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element is invalid.
        /// </exception>
        public XMLElement FindPreviousSibling()
        {
            ThrowIfInvalid();

            var node = lsl_previous_sibling(handle_);
            if (node != IntPtr.Zero)
                return new XMLElement(node);

            return Null;
        }

        /// <summary>
        /// Finds the previous sibling of the element with the specified name.
        /// </summary>
        /// <param name="name">The name of the sibling.</param>
        /// <returns>
        /// The previous sibling of the element with the specified name, or <see cref="Null"/>
        /// if no such sibling exists.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element is invalid.
        /// </exception>
        public XMLElement FindPreviousSibling(string name)
        {
            ThrowIfInvalid();

            var node = lsl_previous_sibling_n(handle_, name);
            if (node != IntPtr.Zero)
                return new XMLElement(node);

            return Null;
        }

        /// <summary>
        /// Appends a new child element with the specified name to the current element.
        /// </summary>
        /// <param name="name">The name of the child.</param>
        /// <returns>
        /// An <see cref="XMLElement"/> representing the newly created child element.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element is invalid.
        /// </exception>
        public XMLElement AppendChild(string name)
        {
            ThrowIfInvalid();

            var node = lsl_append_child(handle_, name);
            if (node != IntPtr.Zero)
                return new XMLElement(node);

            throw new LSLException($"Failed to append child with name {name}.");
        }

        /// <summary>
        /// Appends a child element with a given name, which has a (nameless) plain-text
        /// child with the given text value.
        /// </summary>
        /// <param name="name">The name of the child.</param>
        /// <param name="value">The value of the child.</param>
        /// <returns>The current element.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element is invalid.
        /// </exception>
        public XMLElement AppendChild(string name, string value)
        {
            ThrowIfInvalid();

            var node = lsl_append_child_value(handle_, name, value);
            Debug.Assert(node == handle_); // TODO:
            return this; // TODO:
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
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element or the element to be copied is invalid.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown if appending a copy of the specified element as child fails.
        /// </exception>
        public XMLElement AppendChild(XMLElement element)
        {
            ThrowIfInvalid();

            if (element == null)
                throw new ArgumentNullException(nameof(element));

            element.ThrowIfInvalid();

            var node = lsl_append_copy(handle_, element.handle_);
            if (node != IntPtr.Zero) // TODO:
                return new XMLElement(node);

            throw new LSLException("Failed to append a copy of the specified element as a child.");
        }

        /// <summary>
        /// Prepends a new child element with the specified name to the current element.
        /// </summary>
        /// <param name="name">The name of the child.</param>
        /// <returns>
        /// An <see cref="XMLElement"/> representing the newly created child element.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element is invalid.
        /// </exception>
        public XMLElement PrependChild(string name)
        {
            ThrowIfInvalid();

            var node = lsl_prepend_child(handle_, name);
            if (node != IntPtr.Zero)
                return new XMLElement(node);

            throw new LSLException($"Failed to prepend child with name {name}.");
        }

        /// <summary>
        /// Prepends a child element with a given name, which has a (nameless) plain-text
        /// child with the given text value.
        /// </summary>
        /// <param name="name">The name of the child.</param>
        /// <param name="value">The value of the child.</param>
        /// <returns>The current element.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element is invalid.
        /// </exception>
        public XMLElement PrependChild(string name, string value)
        {
            ThrowIfInvalid();

            var node = lsl_prepend_child_value(handle_, name, value);
            Debug.Assert(node == handle_); // TODO:
            return this; // TODO:
        }

        /// <summary>
        /// Prepends a copy of the specified element as a child.
        /// </summary>
        /// <param name="element">The original element to be copied.</param>
        /// <returns>
        /// An <see cref="XMLElement"/> representing the newly created child element.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the element to be copied is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element or the element to be copied is invalid.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown if prepending a copy of the specified element as child fails.
        /// </exception>
        public XMLElement PrependChild(XMLElement element)
        {
            ThrowIfInvalid();

            if (element == null)
                throw new ArgumentNullException(nameof(element));

            element.ThrowIfInvalid();

            var node = lsl_prepend_copy(handle_, element.handle_);
            if (node != IntPtr.Zero) // TODO:
                return new XMLElement(node);

            throw new LSLException("Failed to prepend a copy of the specified element as a child.");
        }

        /// <summary>
        /// Removes a child of the element with the specified name.
        /// </summary>
        /// <param name="name">The name of the child.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the handle is invalid.
        /// </exception>
        public void RemoveChild(string name)
        {
            ThrowIfInvalid();

            lsl_remove_child_n(handle_, name);
        }

        /// <summary>
        /// Remove a specified child element.
        /// </summary>
        /// <param name="element">The child element to be removed.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the element to be removed is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if current element or the element to be removed is invalid.
        /// </exception>
        public void RemoveChild(XMLElement element)
        {
            ThrowIfInvalid();

            if (element == null)
                throw new ArgumentNullException(nameof(element));

            element.ThrowIfInvalid();

            lsl_remove_child(handle_, element.handle_);
        }

        /// <summary>
        /// Represents a null XML element.
        /// </summary>
        /// <remarks>
        /// The traversal functions provided by <see cref="XMLElement"/> may return
        /// this value if no element is found.
        /// </remarks>
        /// <seealso cref="IsNull"/>
        public static readonly XMLElement Null = new XMLElement(IntPtr.Zero);

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> if the handle is invalid.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the handle is invalid.
        /// </exception>
        protected void ThrowIfInvalid()
        {
            if (handle_ == IntPtr.Zero)
                throw new InvalidOperationException("The XML node handle is invalid.");
        }

        private readonly IntPtr handle_;
    }
}


// References:
// https://github.com/labstreaminglayer/liblsl-Csharp/blob/master/LSL.cs
// https://pugixml.org/docs/manual.html#access
// https://github.com/zeux/pugixml/blob/master/src/pugixml.hpp
