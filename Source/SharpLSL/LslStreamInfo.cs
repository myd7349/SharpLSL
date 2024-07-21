using System;
using System.Runtime.InteropServices;

using SharpLSL.Interop;

namespace SharpLSL
{
    public class LslStreamInfo : LslObject
    {
        public LslStreamInfo(
            string name,
            string type,
            int channelCount = 1,
            double nominalSrate = LSL.LSL_IRREGULAR_RATE,
            LslChannelFormat channelFormat = LslChannelFormat.Float,
            string sourceId = "")
            : this(CreateStreamInfo(
                name,
                type,
                channelCount,
                nominalSrate,
                channelFormat,
                sourceId), true)
        {
        }

        public LslStreamInfo(IntPtr handle, bool ownsHandle = true)
            : base(handle, ownsHandle)
        {
        }

        protected override bool ReleaseHandle()
        {
            LSL.lsl_destroy_streaminfo(handle);
            return true;
        }
        
        private static IntPtr CreateStreamInfo(
            string name,
            string type,
            int channelCount,
            double nominalSrate,
            LslChannelFormat channelFormat,
            string sourceId)
        {
            var namePtr = IntPtr.Zero;
            var typePtr = IntPtr.Zero;
            var sourceIdPtr = IntPtr.Zero;
            
            try
            {
                namePtr = Marshal.StringToHGlobalAnsi(name);
                typePtr = Marshal.StringToHGlobalAnsi(type);
                sourceIdPtr = Marshal.StringToHGlobalAnsi(sourceId);

                return LSL.lsl_create_streaminfo(
                    namePtr,
                    typePtr,
                    channelCount,
                    nominalSrate,
                    (lsl_channel_format_t)channelFormat,
                    sourceIdPtr);
            }
            finally
            {
                if (namePtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(namePtr);

                if (typePtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(typePtr);

                if (sourceIdPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(sourceIdPtr);
            }
        }
    }
}


// References:
// https://github.com/labstreaminglayer/liblsl-Csharp/blob/master/LSL.cs
// [make IntPtr in C#.NET point to string value](https://stackoverflow.com/questions/11090427/make-intptr-in-c-net-point-to-string-value)
