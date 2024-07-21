using System;
using System.Runtime.InteropServices;

namespace SharpLSL
{
    public abstract class LslObject : SafeHandle
    {
        protected LslObject(IntPtr lslHandle, bool ownsHandle = true)
            : base(IntPtr.Zero, ownsHandle)
        {
            if (lslHandle == IntPtr.Zero)
                throw new LSLException($"Failed to create {GetType().Name} object: {Lsl.GetLastError()}.");

            SetHandle(lslHandle);
        }

        public override bool IsInvalid => handle != IntPtr.Zero;
    }
}
