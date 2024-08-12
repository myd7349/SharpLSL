using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SharpLSL.Interop;

using static SharpLSL.Interop.LSL;

namespace SharpLSL
{
    /// <summary>
    /// Provides static methods and constants for interacting with the LSL library.
    /// </summary>
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

        /// <summary>
        /// Resolves all streams on the network.
        /// </summary>
        /// <param name="maxCount">
        /// The maximum number of streams to retrieve.
        /// </param>
        /// <param name="waitTime">
        /// The waiting time for the operation, in seconds, to search for streams.
        /// The recommended wait time is 1 second (or 2 for a busy and large recording
        /// operation). If this is too short (&lt;0.5s) only a subset (or none) of the
        /// outlets that are present on the network may be returned.
        /// </param>
        /// <returns>
        /// An array of <see cref="StreamInfo"/> objects representing the resolved
        /// streams.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This function returns all currently available streams from any outlet on
        /// the network. The network is usually the subnet specified at the local router,
        /// but may also include a multicast group of machines (given that the network
        /// supports it), or a list of hostnames.
        /// </para>
        /// <para>
        /// These details may optionally be customized by the experimenter in a
        /// configuration file (see page Network Connectivity in the LSL wiki).
        /// This is the default mechanism used by the browsing programs and the
        /// recording program.
        /// </para>
        /// </remarks>
        // TODO: Exception
        public static StreamInfo[] ResolveAll(int maxCount = 1024, double waitTime = 1.0)
        {
            var streamInfoPointers = new IntPtr[maxCount];

            var result = lsl_resolve_all(streamInfoPointers, (uint)streamInfoPointers.Length, waitTime);
            CheckError(result);

            var streamInfos = new StreamInfo[result];
            for (int i = 0; i < result; ++i)
                streamInfos[i] = new StreamInfo(streamInfoPointers[i], true);

            return streamInfos;
        }

        /// <summary>
        /// Resolves all streams with a specific value for a given property.
        /// </summary>
        /// <param name="property">
        /// The stream info property that should have a specific value (e.g., "name",
        /// "type", "source_id", or "desc/manufacturer").
        /// </param>
        /// <param name="value">
        /// The string value that the property should have (e.g., "EEG" as the type
        /// property).
        /// </param>
        /// <param name="minCount">
        /// The minimum number of streams to retrieve.
        /// </param>
        /// <param name="maxCount">
        /// The maximum number of streams to retrieve.
        /// </param>
        /// <param name="timeout">
        /// Specifies the timeout of the operation in seconds. Default value is
        /// <see cref="Forever"/> which indicates no timeout. If the timeout expires,
        /// less than the desired number of streams (possibly none) will be returned.
        /// </param>
        /// <returns>
        /// An array of <see cref="StreamInfo"/> objects representing the currently
        /// resolved streams. 
        /// </returns>
        /// <remarks>
        /// <para>
        /// If the goal is to resolve a specific stream, this method is preferred
        /// over resolving all streams and then selecting the desired one.
        /// </para>
        /// <para>
        /// The stream infos returned by the resolver are only short versions that do
        /// not include the <see cref="StreamInfo.Description"/> field (which can be
        /// arbitrarily big). To obtain the full stream information you need to call
        /// <see cref="StreamInlet.GetStreamInfo(double)"/> on the inlet after you
        /// have created one.
        /// </para>
        /// </remarks>
        // TODO: Exception, Check minCount, maxCount
        public static StreamInfo[] Resolve(string property, string value, int minCount = 1, int maxCount = 1024, double timeout = Forever)
        {
            var streamInfoPointers = new IntPtr[maxCount];

            var result = lsl_resolve_byprop(streamInfoPointers, (uint)streamInfoPointers.Length, property, value, minCount, timeout);
            CheckError(result);

            var streamInfos = new StreamInfo[result];
            for (int i = 0; i < result; ++i)
                streamInfos[i] = new StreamInfo(streamInfoPointers[i], true);

            return streamInfos;
        }

        /// <summary>
        /// Resolves all streams on the network that match a given XPath 1.0 predicate.
        /// </summary>
        /// <param name="predicate">
        /// The XPath 1.0 predicate used to filter streams. For example,
        /// `"name='BioSemi'" or "type='EEG' and starts-with(name,'BioSemi') and count(info/desc/channel)=32"`.
        /// </param>
        /// <param name="minCount">
        /// The minimum number of streams to retrieve.
        /// </param>
        /// <param name="maxCount">
        /// The maximum number of streams to retrieve.
        /// </param>
        /// <param name="timeout">
        /// Specifies the timeout of the operation in seconds. Default value is
        /// <see cref="Forever"/> which indicates no timeout. If the timeout expires,
        /// less than the desired number of streams (possibly none) will be returned.
        /// </param>
        /// <returns>
        /// An array of <see cref="StreamInfo"/> objects representing the currently
        /// resolved streams. 
        /// </returns>
        /// <remarks>
        /// <para>
        /// The stream infos returned by the resolver are only short versions that do
        /// not include the <see cref="StreamInfo.Description"/> field (which can be
        /// arbitrarily big). To obtain the full stream information you need to call
        /// <see cref="StreamInlet.GetStreamInfo(double)"/> on the inlet after you
        /// have created one.
        /// </para>
        /// <para>
        /// Advanced query that allows to impose more conditions on the retrieved
        /// streams; the given string is an
        /// [XPath 1.0 predicate](http://en.wikipedia.org/w/index.php?title=XPath_1.0)
        /// for the `&lt;info&gt;` node (omitting the surrounding []'s).
        /// </para>
        /// </remarks>
        // TODO: Exception minCount maxCount 
        public static StreamInfo[] Resolve(string predicate, int minCount = 1, int maxCount = 1024, double timeout = Forever)
        {
            var streamInfoPointers = new IntPtr[maxCount];

            var result = lsl_resolve_bypred(streamInfoPointers, (uint)streamInfoPointers.Length, predicate, minCount, timeout);
            CheckError(result);

            var streamInfos = new StreamInfo[result];
            for (int i = 0; i < result; ++i)
                streamInfos[i] = new StreamInfo(streamInfoPointers[i], true);

            return streamInfos;
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

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static void CheckChannelCount(int expected, int actual)
        {
            if (actual != expected)
                throw new ArgumentException($"Provided buffer's channel count ({actual}) doesn't match the stream's channel count ({expected}).");
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static void CheckSampleBuffer<T>(T[] sample, int channelCount)
        {
            if (sample == null)
                throw new ArgumentNullException(nameof(sample));

            CheckChannelCount(channelCount, sample.Length);
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static int CheckChunkBuffer<T>(T[] chunk, int channelCount)
        {
            if (chunk == null)
                throw new ArgumentNullException(nameof(chunk));

            if (chunk.Length == 0 || (chunk.Length % channelCount) != 0)
                throw new ArgumentException(nameof(chunk));

            return chunk.Length / channelCount;
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static void CheckChunkBuffer<T>(T[,] chunk, int channelCount)
        {
            if (chunk == null)
                throw new ArgumentNullException(nameof(chunk));

            if (chunk.Length == 0)
                throw new ArgumentException(nameof(chunk));

            if (chunk.GetLength(1) != channelCount)
                throw new ArgumentException($"Chunk's second dimension size ({chunk.GetLength(1)}) does not match the expected channel count ({channelCount}).");
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static void CheckTimestampBufferAllowNull(double[] timestamps, int samples)
        {
            Debug.Assert(samples > 0);

            if (timestamps != null)
            {
                if (timestamps.Length != samples)
                    throw new ArgumentException($"Timestamps buffer size ({timestamps.Length}) does not match the number of samples ({samples}).");
            }
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static void CheckTimestampBuffer(double[] timestamps, int samples)
        {
            Debug.Assert(samples > 0);

            if (timestamps == null)
                throw new ArgumentNullException(nameof(timestamps));

            if (timestamps.Length != samples)
                throw new ArgumentException($"Timestamps buffer size ({timestamps.Length}) does not match the number of samples ({samples}).");
        }
    }
}
