using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

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
        /// This constant serves as an optional optimization to transmit less data
        /// per sample. When used, the timestamp is then automatically deduced from
        /// the preceding one according to the stream's sampling rate. For streams
        /// with an irregular sampling rate, the timestamp is assumed to be the same
        /// as the previous sample's timestamp.
        /// </remarks>
        public const double DeducedTimestamp = LSL_DEDUCED_TIMESTAMP;

        /// <summary>
        /// Represents an extremely long duration, approximately equivalent to 1 year.
        /// </summary>
        /// <remarks>
        /// This constant can be used for specifying timeouts where you want to effectively
        /// indicate an indefinite or very long duration.
        /// </remarks>
        public const double Forever = LSL_FOREVER;

        /// <summary>
        /// Constant to indicate that there is no preference about how a data stream
        /// shall be chunked for transmission.
        /// </summary>
        /// <remarks>
        /// This constant can be used for the chunking parameters in the inlet or the
        /// outlet.
        /// </remarks>
        public const int NoPreference = LSL_NO_PREFERENCE;

        /// <summary>
        /// Constant to indicate the LSL version against which this library was compiled.
        /// </summary>
        /// <remarks>
        /// This constant is used either to check if the same version is used:
        /// <code>
        /// if (LSL.GetProtocolVersion() != LSL.CompileHeaderVersion)
        /// {
        ///     // Do stuff...
        /// }
        /// </code>
        /// or to require a certain set of features:
        /// <code>
        /// if (LSL.CompileHeaderVersion > 113)
        /// {
        ///     // Do stuff...
        /// }
        /// </code>
        /// </remarks>
        /// <seealso cref="GetProtocolVersion"/>
        public const int CompileHeaderVersion = LIBLSL_COMPILE_HEADER_VERSION;

#if NET6_0_OR_GREATER
        /// <summary>
        /// Specifies whether strings are encoded and decoded using UTF-8 (<c>true</c>)
        /// or ANSI (<c>false</c>) encoding.
        /// </summary>
        public static bool UseUTF8 = false;
#endif

        /// <summary>
        /// Retrieves the explanation for the most recent error in the LSL library.
        /// </summary>
        /// <returns>
        /// A string containing the explanation for the most recent error.
        /// </returns>
        public static string GetLastError() => PtrToString(lsl_last_error());

        /// <summary>
        /// Retrieves the LSL protocol version encoded as a single integer.
        /// </summary>
        /// <returns>The LSL protocol version as an integer.</returns>
        /// <remarks>
        /// The protocol version is encoded as follows:
        /// <list type="bullet">
        ///     <item>Major version = <c>GetProtocolVersion() / 100</c></item>
        ///     <item>Minor version = <c>GetProtocolVersion() % 100</c></item>
        /// </list>
        /// Clients with different minor versions are considered protocol-compatible,
        /// while clients with different major versions will refuse to work together.
        /// </remarks>
        /// <seealso cref="CompileHeaderVersion"/>
        /// <seealso cref="StreamInfo.Version"/>
        public static int GetProtocolVersion() => lsl_protocol_version();

        /// <summary>
        /// Retrieves the version of the underlying liblsl library encoded as a
        /// single integer.
        /// </summary>
        /// <returns>The liblsl library version as an integer.</returns>
        /// <remarks>
        /// The library version is encoded as follows:
        /// <list type="bullet">
        ///     <item>Major version = <c>GetLibraryVersion() / 100</c></item>
        ///     <item>Minor version = <c>GetLibraryVersion() % 100</c></item>
        /// </list>
        /// </remarks>
        public static int GetLibraryVersion() => lsl_library_version();

        /// <summary>
        /// Retrieves a string containing detailed information about the liblsl library.
        /// </summary>
        /// <returns>A string containing library information.</returns>
        /// <remarks>
        /// This method provides detailed information about the liblsl library, which
        /// can be useful for debugging purposes.
        /// </remarks>
        public static string GetLibraryInfo() => PtrToString(lsl_library_info());

        /// <summary>
        /// Retrieves the current local system timestamp in seconds with high precision.
        /// </summary>
        /// <returns>The current local system timestamp in seconds.</returns>
        /// <remarks>
        /// This function provides a high-precision local system timestamp with
        /// resolution better than a millisecond. It can be used to assign timestamps to
        /// samples as they are being acquired. If the "age" of a sample is known at a
        /// particular time (e.g., from USB transmission delays), it can be used as an
        /// offset to <see cref="GetLocalClock"/> to obtain a better estimate of when a
        /// sample was actually captured.
        /// <code>
        /// double sampleAge = knownDelayInSeconds;
        /// double adjustedTimestamp = LSL.GetLocalClock() - sampleAge;
        /// </code>
        /// </remarks>
        /// <seealso cref="StreamOutlet.PushSample(sbyte[], double)"/>
        /// <seealso cref="StreamOutlet.PushSample(short[], double)"/>
        /// <seealso cref="StreamOutlet.PushSample(int[], double)"/>
        /// <seealso cref="StreamOutlet.PushSample(long[], double)"/>
        /// <seealso cref="StreamOutlet.PushSample(float[], double)"/>
        /// <seealso cref="StreamOutlet.PushSample(double[], double)"/>
        /// <seealso cref="StreamOutlet.PushSample(string[], double)"/>
        /// <seealso cref="StreamOutlet.PushSample(byte[], double)"/>
        /// <seealso cref="StreamOutlet.PushSample(IntPtr, double)"/>
        /// <seealso cref="StreamOutlet.PushSample(sbyte[], double, bool)"/>
        /// <seealso cref="StreamOutlet.PushSample(short[], double, bool)"/>
        /// <seealso cref="StreamOutlet.PushSample(int[], double, bool)"/>
        /// <seealso cref="StreamOutlet.PushSample(long[], double, bool)"/>
        /// <seealso cref="StreamOutlet.PushSample(float[], double, bool)"/>
        /// <seealso cref="StreamOutlet.PushSample(double[], double, bool)"/>
        /// <seealso cref="StreamOutlet.PushSample(string[], double, bool)"/>
        /// <seealso cref="StreamOutlet.PushSample(byte[], double, bool)"/>
        /// <seealso cref="StreamOutlet.PushSample(IntPtr, double, bool)"/>
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
        /// <para>
        /// The stream infos returned by the resolver are only short versions that do
        /// not include the <see cref="StreamInfo.Description"/> field (which can be
        /// arbitrarily big). To obtain the full stream information you need to call
        /// <see cref="StreamInlet.GetStreamInfo(double)"/> on the inlet after you
        /// have created one.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="maxCount"/> is less than or equal to 0.
        /// </exception>
        /// <exception cref="LSLInternalException">
        /// Thrown when an internal LSL error occurs.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown when an unknown LSL error occurs.
        /// </exception>
        /// <seealso cref="Resolve(string, string, int, int, double)"/>
        /// <seealso cref="Resolve(string, int, int, double)"/>
        /// <seealso cref="ContinuousResolver(double)"/>
        public static StreamInfo[] Resolve(int maxCount = 1024, double waitTime = 1.0)
        {
            if (maxCount <= 0)
                throw new ArgumentException(nameof(maxCount));

            if (waitTime < 0)
                throw new ArgumentException(nameof(waitTime));

            var streamInfoPointers = new IntPtr[maxCount];

            var result = lsl_resolve_all(streamInfoPointers, (uint)streamInfoPointers.Length, waitTime);
            CheckError(result);

            if (result > 0)
            {
                var streamInfos = new StreamInfo[result];
                for (int i = 0; i < result; ++i)
                    streamInfos[i] = new StreamInfo(streamInfoPointers[i], true);

                return streamInfos;
            }

#if NET35 || NET45
            return new StreamInfo[0];
#else
            return Array.Empty<StreamInfo>();
#endif
        }

        /// <summary>
        /// Resolves all streams with a specific value for a given property.
        /// </summary>
        /// <param name="property">
        /// The stream info property that should have a specific value (e.g., "name",
        /// "type", "source_id", or "desc/manufacturer" if present).
        /// </param>
        /// <param name="value">
        /// The string value that the property should have (e.g., "EEG" as the "type"
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
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="value"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="property"/> is null or an empty string,
        /// or when <paramref name="minCount"/> is less than or equal to 0,
        /// or when <paramref name="maxCount"/> is less than <paramref name="minCount"/>.
        /// </exception>
        /// <exception cref="LSLInternalException">
        /// Thrown when an internal LSL error occurs.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown when an unknown LSL error occurs.
        /// </exception>
        /// <seealso cref="Resolve(int, double)"/>
        /// <seealso cref="Resolve(string, int, int, double)"/>
        /// <seealso cref="ContinuousResolver(string, string, double)"/>
        public static StreamInfo[] Resolve(string property, string value, int minCount = 1, int maxCount = 1024, double timeout = Forever)
        {
            if (minCount <= 0)
                throw new ArgumentException(nameof(minCount));

            if (maxCount < minCount)
                throw new ArgumentException(nameof(maxCount));

            var propertyBytes = StringToBytes(property);
            if (propertyBytes == null)
                throw new ArgumentException(nameof(property));

            var valueBytes = StringToBytes(value);
            if (valueBytes == null)
                throw new ArgumentException(nameof(value));

            var streamInfoPointers = new IntPtr[maxCount];

            int result;

            unsafe
            {
                fixed (IntPtr* streamInfoPointersBuffer = streamInfoPointers)
                fixed (byte* propertyBuffer = propertyBytes)
                fixed (byte* valueBuffer = valueBytes)
                {
                    result = lsl_resolve_byprop(
                        streamInfoPointersBuffer,
                        (uint)streamInfoPointers.Length,
                        (sbyte*)propertyBuffer,
                        (sbyte*)valueBuffer,
                        minCount,
                        timeout);
                }
            }

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
        /// <see href="http://en.wikipedia.org/w/index.php?title=XPath_1.0"/>
        /// for the `&lt;info&gt;` node (omitting the surrounding []'s).
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="predicate"/> is null or an empty string,
        /// or when <paramref name="minCount"/> is less than or equal to 0,
        /// or when <paramref name="maxCount"/> is less than <paramref name="minCount"/>.
        /// </exception>
        /// <exception cref="LSLInternalException">
        /// Thrown when an internal LSL error occurs.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown when an unknown LSL error occurs.
        /// </exception>
        /// <seealso cref="Resolve(int, double)"/>
        /// <seealso cref="Resolve(string, string, int, int, double)"/>
        /// <seealso cref="ContinuousResolver(string, double)"/>
        public static StreamInfo[] Resolve(string predicate, int minCount = 1, int maxCount = 1024, double timeout = Forever)
        {
            if (minCount <= 0)
                throw new ArgumentException(nameof(minCount));

            if (maxCount < minCount)
                throw new ArgumentException(nameof(maxCount));

            var predicateBytes = StringToBytes(predicate);
            if (predicateBytes == null)
                throw new ArgumentNullException(nameof(predicate));

            var streamInfoPointers = new IntPtr[maxCount];

            int result;

            unsafe
            {
                fixed (IntPtr* streamInfoPointersBuffer = streamInfoPointers)
                fixed (byte* predicateBuffer = predicateBytes)
                {
                    result = lsl_resolve_bypred(
                        streamInfoPointersBuffer,
                        (uint)streamInfoPointers.Length,
                        (sbyte*)predicateBuffer,
                        minCount,
                        timeout);
                }
            }

            CheckError(result);

            var streamInfos = new StreamInfo[result];
            for (int i = 0; i < result; ++i)
                streamInfos[i] = new StreamInfo(streamInfoPointers[i], true);

            return streamInfos;
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static string PtrToString(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;

#if NET6_0_OR_GREATER
            return UseUTF8 ?
                Marshal.PtrToStringUTF8(ptr) :
                Marshal.PtrToStringAnsi(ptr);
#else
            return Marshal.PtrToStringAnsi(ptr);
#endif
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static string PtrToXmlString(IntPtr ptr)
        {
            return PtrToString(ptr);
        }

        internal static byte[] StringToBytes(string str)
        {
            if (str == null)
                return null;

            var chars = str.ToCharArray();

#if NET6_0_OR_GREATER
            var encoding = UseUTF8 ? Encoding.UTF8 : Encoding.Default;
#else
            var encoding = Encoding.Default;
#endif

            var length = encoding.GetByteCount(chars);
            var bytes = new byte[length + 1];
            encoding.GetBytes(chars, 0, chars.Length, bytes, 0);
            Debug.Assert(bytes[bytes.Length - 1] == 0);

            return bytes;
        }

        internal static IntPtr StringToPtr(string str)
        {
            if (str == null)
                return IntPtr.Zero;

#if NET6_0_OR_GREATER
            var encoding = UseUTF8 ? Encoding.UTF8 : Encoding.Default;
#else
            var encoding = Encoding.Default;
#endif

            var length = encoding.GetByteCount(str);
            var pointer = Marshal.AllocHGlobal(length + 1);

            unsafe
            {
                byte* buffer = (byte*)pointer;
                fixed (char* chars = str)
                {
                    encoding.GetBytes(chars, str.Length, buffer, length + 1);
                }
                buffer[length] = 0; // null-terminator
            }

            return pointer;
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static void CheckError(int errorCode) // TODO: GetLastError
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
        internal static void CheckSampleBuffer<T>(ReadOnlySpan<T> sample, int channelCount)
        {
            CheckChannelCount(channelCount, sample.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void CheckSampleBuffer<T>(Span<T> sample, int channelCount)
        {
            CheckChannelCount(channelCount, sample.Length);
        }
#endif

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static void CheckLengthBuffer<T>(T[] lengths, int channelCount)
        {
            if (lengths == null)
                throw new ArgumentNullException(nameof(lengths));

            CheckChannelCount(channelCount, lengths.Length);
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static int CheckChunkBuffer<T>(T[] chunk, int channelCount)
        {
            if (chunk == null)
                throw new ArgumentNullException(nameof(chunk));

            // TODO: chunk.Length == 0
            if (chunk.Length == 0 || (chunk.Length % channelCount) != 0)
                throw new ArgumentException($"The number of data values ({chunk.Length}) in the chunk must be a multiple of the channel count ({channelCount}).", nameof(chunk));

            return chunk.Length / channelCount;
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int CheckChunkBuffer<T>(ReadOnlySpan<T> chunk, int channelCount)
        {
            if (chunk.Length == 0 || (chunk.Length % channelCount) != 0)
                throw new ArgumentException($"The number of data values ({chunk.Length}) in the chunk must be a multiple of the channel count ({channelCount}).", nameof(chunk));

            return chunk.Length / channelCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int CheckChunkBuffer<T>(Span<T> chunk, int channelCount)
        {
            if (chunk.Length == 0 || (chunk.Length % channelCount) != 0)
                throw new ArgumentException($"The number of data values ({chunk.Length}) in the chunk must be a multiple of the channel count ({channelCount}).", nameof(chunk));

            return chunk.Length / channelCount;
        }
#endif

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
                throw new ArgumentException($"The number of columns in chunk ({chunk.GetLength(1)}) does not match the expected channel count ({channelCount}).");
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
                    throw new ArgumentException($"The timestamp buffer size ({timestamps.Length}) does not match the number of samples ({samples}).", nameof(timestamps));
            }
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void CheckTimestampBufferAllowNull(Span<double> timestamps, int samples)
        {
            Debug.Assert(samples > 0);

            if (timestamps.Length > 0)
            {
                if (timestamps.Length != samples)
                    throw new ArgumentException($"The timestamp buffer size ({timestamps.Length}) does not match the number of samples ({samples}).", nameof(timestamps));
            }
        }
#endif

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static void CheckTimestampBuffer(double[] timestamps, int samples)
        {
            Debug.Assert(samples > 0);

            if (timestamps == null)
                throw new ArgumentNullException(nameof(timestamps));

            if (timestamps.Length != samples)
                throw new ArgumentException($"The timestamp buffer size ({timestamps.Length}) does not match the number of samples ({samples}).", nameof(timestamps));
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void CheckTimestampBuffer(ReadOnlySpan<double> timestamps, int samples)
        {
            Debug.Assert(samples > 0);

            if (timestamps.Length != samples)
                throw new ArgumentException($"The timestamp buffer size ({timestamps.Length}) does not match the number of samples ({samples}).", nameof(timestamps));
        }
#endif
    }
}
