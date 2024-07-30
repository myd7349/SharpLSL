using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SharpLSL.Interop;
using static SharpLSL.Interop.LSL;

namespace SharpLSL
{
    public static class LSL
    {
        /// <summary>
        /// Constant to indicate that a stream has variable sampling rate.
        /// </summary>
        /// <seealso cref="StreamInfo.NominalSrate"/>
        public const double IrregularRate = LSL_IRREGULAR_RATE;

        /// <summary>
        /// Constant to indicate that a sample has the next successive timestamp.
        /// </summary>
        /// <remarks>
        /// This is an optional optimization to transmit less data per sample.
        /// The timestamp is then deduced from the preceding one according to the stream's
        /// sampling rate. If the sampling rate is irregular, the time stamp will be assumed
        /// to be the same as the previous sample's timestamp.
        /// </remarks>
        public const double DeducedTimestamp = LSL_DEDUCED_TIMESTAMP;

        /// <summary>
        /// Constant to indicate a very large time value, approximately equivalent to 1 year.
        /// This constant can be used for specifying timeouts where you want to effectively
        /// indicate an indefinite or very long duration.
        /// </summary>
        public const double Forever = LSL_FOREVER;

        /// <summary>
        /// Constant to indicate that there is no preference about how a data stream shall be
        /// chunked for transmission.
        /// </summary>
        /// <remarks>
        /// This constant can be used for the chunking parameters in the inlet or the outlet.
        /// </remarks>
        public const int NoPreference = LSL_NO_PREFERENCE;

        /// <summary>
        /// Constant to indicate the LSL version the binary was compiled against.
        /// </summary>
        /// <remarks>
        /// This constant is used either to check if the same version is used:
        /// <code>
        /// if (LSL.GetProtocolVersion() != LSL.CompileHeaderVersion)
        /// {
        ///     // Do stuff...
        /// }
        /// </code>
        /// or to require a certain set of features::
        /// <code>
        /// if (LSL.CompileHeaderVersion > 113)
        /// {
        ///     // Do stuff...
        /// }
        /// </code>
        /// </remarks>
        /// <seealso cref="GetProtocolVersion"/>
        public const int CompileHeaderVersion = LIBLSL_COMPILE_HEADER_VERSION;

        /// <summary>
        /// Gets the last error message from the LSL (Lab Streaming Layer) library.
        /// </summary>
        /// <returns>
        /// A string containing the most recent error message from the LSL library.
        /// </returns>
        public static string GetLastError() => PtrToString(lsl_last_error());

        /// <summary>
        /// Gets the LSL protocol version encoded as a single integer.
        /// </summary>
        /// <returns>The LSL protocol version encoded as a single integer.</returns>
        /// <remarks>
        /// The protocol version is encoded as a single integer, where:
        /// <list type="bullet">
        ///     <item>The major version can be obtained by dividing the protocol version by 100 (i.e., <c>LSL.GetProtocolVersion() / 100</c>).</item>
        ///     <item>The minor version can be obtained by taking the remainder of the protocol version divided by 100 (i.e., <c>LSL.GetProtocolVersion() % 100</c>).</item>
        /// </list>
        /// Clients with different minor versions are considered protocol-compatible,
        /// while clients with different major versions are not compatible and will
        /// refuse to work together.
        /// </remarks>
        /// <seealso cref="CompileHeaderVersion"/>
        /// <seealso cref="StreamInfo.Version"/>
        public static int GetProtocolVersion() => lsl_protocol_version();

        /// <summary>
        /// Gets the underlying liblsl version number encoded as a single integer. 
        /// </summary>
        /// <returns>The liblsl version number encoded as a single integer. </returns>
        /// <remarks>
        /// The liblsl version number is encoded as a single integer, where:
        /// <list type="bullet">
        ///     <item>The major version can be obtained by dividing the version by 100 (i.e., <c>LSL.GetLibraryVersion() / 100</c>).</item>
        ///     <item>The minor version can be obtained by taking the remainder of the version divided by 100 (i.e., <c>LSL.GetLibraryVersion() % 100</c>).</item>
        /// </list>
        /// </remarks>
        public static int GetLibraryVersion() => lsl_library_version();

        /// <summary>
        /// Gets a string containing library information.
        /// </summary>
        /// <returns>The string containing library information.</returns>
        /// <remarks>
        /// The format of the string shouldn't be used for anything important except
        /// giving a debugging person a good idea which exact library version is used.
        /// </remarks>
        public static string GetLibraryInfo() => PtrToString(lsl_library_info());

        /// <summary>
        /// Gets the current local system timestamp in seconds with high resolution.
        /// </summary>
        /// <returns>The current local system timestamp in seconds.</returns>
        /// <remarks>
        /// This function returns a local system timestamp in seconds which has a
        /// high resolution better than a milliseconds. The returned timestamp can
        /// be used to assign timestamps to samples as they are being acquired.
        /// If the "age" of a sample is known at a particular time (e.g., from USB
        /// transmission delays), it can be used as an offset to <seealso cref="GetLocalClock"/>
        /// to obtain a better estimate of when a sample was actually captured.
        /// </remarks>
        /// <seealso cref="StreamOutlet.PushSample(short[], double)"/>
        /// <seealso cref="StreamOutlet.PushSample(int[], double)"/>
        /// <seealso cref="StreamOutlet.PushSample(long[], double)"/>
        /// <seealso cref="StreamOutlet.PushSample(float[], double)"/>
        /// <seealso cref="StreamOutlet.PushSample(double[], double)"/>
        /// <seealso cref="StreamOutlet.PushSample(short[], double, bool)"/>
        /// <seealso cref="StreamOutlet.PushSample(int[], double, bool)"/>
        /// <seealso cref="StreamOutlet.PushSample(long[], double, bool)"/>
        /// <seealso cref="StreamOutlet.PushSample(float[], double, bool)"/>
        /// <seealso cref="StreamOutlet.PushSample(double[], double, bool)"/>
        // TODO: Update seealso
        public static double GetLocalClock() => lsl_local_clock();

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static void CheckError(int errorCode)
        {
            if (errorCode < 0)
            {
                switch ((lsl_error_code_t)errorCode)
                {
                    //case lsl_error_code_t.lsl_no_error:
                    //    break;
                    case lsl_error_code_t.lsl_timeout_error:
                        throw new TimeoutException("The operation failed due to a timeout.");
                    case lsl_error_code_t.lsl_lost_error:
                        throw new StreamLostException("The stream has been lost.");
                    case lsl_error_code_t.lsl_argument_error:
                        throw new ArgumentException("An argument was incorrectly specified.");
                    case lsl_error_code_t.lsl_internal_error:
                        throw new LSLInternalException("An internal error has occurred.");
                    default:
                        throw new LSLException("An unknown error has occurred.");
                }
            }
        }

        // TODO: Encoding
#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static string PtrToString(IntPtr ptr)
        {
            return Marshal.PtrToStringAnsi(ptr);
        }

        // TODO: Encoding
#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static string PtrToXmlString(IntPtr ptr)
        {
            return Marshal.PtrToStringAnsi(ptr);
        }
    }
}
