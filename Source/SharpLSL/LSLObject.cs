using System;
using System.Runtime.InteropServices;

namespace SharpLSL
{
    /// <summary>
    /// Represents a base class for LSL (Lab Streaming Layer) objects, providing a
    /// safe wrapper for native handles to manage underlying native resources.
    /// </summary>
    /// <remarks>
    /// This abstract class extends <see cref="SafeHandle"/> to provide a safe and
    /// managed way to interact with LSL objects. It ensures that the native resources
    /// associated with the LSL objects are properly released when no longer needed.
    /// Derived classes should implement specific functionality for different types
    /// of LSL objects, such as inlets or outlets.
    /// </remarks>
    public abstract class LSLObject : SafeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LSLObject"/> class.
        /// </summary>
        /// <param name="ownsHandle">
        /// Speciies whether the wrapped handle should be released during the finalization
        /// phase.
        /// </param>
        protected LSLObject(bool ownsHandle = true)
            : base(IntPtr.Zero, ownsHandle)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LSLObject"/> class with the
        /// specified LSL native handle value.
        /// </summary>
        /// <param name="handle">
        /// Specifies the handle to be wrapped.
        /// </param>
        /// <param name="ownsHandle">
        /// Speciies whether the wrapped handle should be released during the finalization
        /// phase.
        /// </param>
        /// <exception cref="LSLException">
        /// Thrown if the provided handle is invalid (IntPtr.Zero).
        /// </exception>
        protected LSLObject(IntPtr handle, bool ownsHandle = true)
            : base(IntPtr.Zero, ownsHandle)
        {
            if (handle == IntPtr.Zero)
                throw new LSLException($"Failed to create {GetType().Name} object: {LSL.GetLastError()}.");

            SetHandle(handle);
        }

        /// <summary>
        /// Gets a value indicating whether the handle is invalid (IntPtr.Zero).
        /// </summary>
        public override bool IsInvalid => handle == IntPtr.Zero;

        /// <summary>
        /// Releases the wrapped LSL native handle by calling <see cref="DestroyLSLObject"/>.
        /// </summary>
        /// <returns>A value indicates if the handle is released successfully.</returns>
        /// <seealso cref="DestroyLSLObject"/>
        protected override bool ReleaseHandle()
        {
            DestroyLSLObject();
            // Without this line, IsInvalid == false;
            // SetHandleAsInvalid() won't change the value of handle.
            SetHandle(IntPtr.Zero);
            return true;
        }

        /// <summary>
        /// When overridden in a derived class, ensures that the appropriate LSL
        /// function is called to release the handle of the corresponding LSL object.
        /// </summary>
        /// <seealso cref="ReleaseHandle"/>
        protected abstract void DestroyLSLObject();

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> if the handle is invalid.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the handle is invalid (i.e., <see cref="IsInvalid"/> is true).
        /// </exception>
        protected void ThrowIfInvalid()
        {
            if (IsInvalid)
                throw new InvalidOperationException("The native LSL handle is invalid.");
        }
    }
}


// References:
// [SafeHandle Class](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.safehandle?view=net-8.0)
// [System.Runtime.InteropServices.SafeHandle class](https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-runtime-interopservices-safehandle)
// [SafeHandle.SetHandleAsInvalid Method](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.safehandle.sethandleasinvalid?view=net-8.0)
// > As with the SetHandle method, use SetHandleAsInvalid only if you need to support a pre-existing handle.
// https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/Runtime/InteropServices/SafeHandle.cs
// https://github.com/dotnet/runtime/blob/main/src/libraries/Common/src/System/Net/DebugSafeHandleZeroOrMinusOneIsInvalid.cs
// https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/Microsoft/Win32/SafeHandles/SafeHandleZeroOrMinusOneIsInvalid.cs
// https://github.com/dotnet/runtime/blob/main/src/tests/Interop/PInvoke/SafeHandles/SafeHandles.cs
// https://github.com/dotnet/runtime/blob/main/src/libraries/Common/src/Microsoft/Win32/SafeHandles/SafeCertStoreHandle.cs
// [Cannot pass SafeHandle instance in ReleaseHandle to native method](https://stackoverflow.com/questions/47934248/cannot-pass-safehandle-instance-in-releasehandle-to-native-method)
// https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/Microsoft/Win32/SafeHandles/SafeFileHandle.cs
// > SetHandle(...);
// https://github.com/dotnet/runtime/tree/main/src/libraries/System.Private.CoreLib/src/Microsoft/Win32/SafeHandles
// [SafeHandle used to wrap a non-owned pointer](https://stackoverflow.com/questions/31691183/safehandle-used-to-wrap-a-non-owned-pointer)
// [Are there any benefits for using SafeFileHandle with FileStream constructor](https://stackoverflow.com/questions/58576214/are-there-any-benefits-for-using-safefilehandle-with-filestream-constructor)
// [What is SafeFileHandle in C# and when should I use it?](https://stackoverflow.com/questions/58568415/what-is-safefilehandle-in-c-sharp-and-when-should-i-use-it)
// [How to Close SafeFile Handle properly](https://stackoverflow.com/questions/20827222/how-to-close-safefile-handle-properly)
// https://github.com/dotnet/runtime/blob/main/src/libraries/Common/src/Interop/OSX/Interop.CoreFoundation.CFDate.cs
// > SetHandle(IntPtr.Zero);
// https://github.com/dotnet/runtime/blob/main/src/libraries/System.Net.Quic/src/System/Net/Quic/Internal/MsQuicSafeHandle.cs
// > SetHandle(IntPtr.Zero);
