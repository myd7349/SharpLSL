//#define STRING_MARSHALING_SCHEME_1
//#define STRING_MARSHALING_SCHEME_2

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SharpLSL.Interop;

using static SharpLSL.Interop.LSL;
using static SharpLSL.LSL;

namespace SharpLSL
{
    /// <summary>
    /// Represents a stream outlet for pushing time series data to all connected
    /// stream inlets in the lab network.
    /// </summary>
    /// <remarks>
    /// <para>
    /// StreamOutlet allows pushing data streams to all connected inlets. The data
    /// is pushed sample-by-sample or chunk-by-chunk, and can consist of single or
    /// multichannel data, regular or irregular sampling rates, with uniform value
    /// types (integers, floats, doubles, strings). Streams can also have arbitrary
    /// XML meta-data (akin to a file header).
    /// </para>
    /// <para>
    /// By creating an outlet, the stream is made visible to a collection of computers
    /// (defined by the network settings/layout) where one can subscribe to it by finding
    /// it via a resolver and connecting a <see cref="StreamInlet"/> to it.
    /// </para>
    /// </remarks>
    public class StreamOutlet : LSLObject
    {
        /// <summary>
        /// Constructs a new stream outlet for pushing time series data (and the
        /// metadata). This makes the stream discoverable.
        /// </summary>
        /// <param name="streamInfo">
        /// The stream information to use for creating this stream outlet. Stays
        /// constant over the lifetime of the outlet. The outlet makes a copy of
        /// the <paramref name="streamInfo"/> object upon construction (so the old
        /// info can still be destroyed).
        /// </param>
        /// <param name="chunkSize">
        /// Specifies the desired chunk granularity (in samples) for transmission.
        /// If specified as 0, each push operation yields one chunk. Stream recipients
        /// (inlets) can override this setting.
        /// </param>
        /// <param name="maxBuffered">
        /// Specifies the maximum amount of data to buffer (in seconds if there
        /// is a nominal sampling rate, otherwise x100 in samples). A good default
        /// is 360, which corresponds to 6 minutes of data. Note that for high
        /// bandwidth data, you will almost certainly want to use a lower value here
        /// to avoid running out of RAM.
        /// </param>
        /// <param name="transportOptions">
        /// Specifies additional options for data transport. Default is <see cref="TransportOptions.Default"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="streamInfo"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="streamInfo"/> wraps an invalid native handle.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown when creating a new instance of <see cref="StreamOutlet"/> fails.
        /// </exception>
        public StreamOutlet(
            StreamInfo streamInfo,
            int chunkSize = 0,
            int maxBuffered = 360,
            TransportOptions transportOptions = TransportOptions.Default)
            : base(CreateOutlet(
                streamInfo,
                chunkSize,
                maxBuffered,
                transportOptions))
        {
            ChannelCount = streamInfo.ChannelCount;
        }

        /// <summary>
        /// Constructs a new instance of <see cref="StreamOutlet"/> object which wraps
        /// a pre-existing stream outlet handle.
        /// </summary>
        /// <param name="handle">
        /// Specifies the handle to be wrapped.
        /// </param>
        /// <param name="ownsHandle">
        /// Specifies whether the wrapped handle should be released during the finalization
        /// phase.
        /// </param>
        /// <exception cref="LSLException">
        /// Thrown if the handle is invalid.
        /// </exception>
        public StreamOutlet(IntPtr handle, bool ownsHandle = true)
            : base(handle, ownsHandle)
        {
            using (var streamInfo = GetStreamInfo())
            {
                ChannelCount = streamInfo.ChannelCount;
            }
        }

        /// <summary>
        /// Gets the number of channels in the stream.
        /// </summary>
        /// <seealso cref="StreamInfo.ChannelCount"/>
        public int ChannelCount { get; }

        /// <summary>
        /// Retrieves a copy of the stream info provided by this outlet.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="StreamInfo"/> which is a copy of the stream
        /// info object that was used to create the stream (and also has the additional
        /// network information fields assigned).
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream outlet object is invalid.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown if getting the stream information fails.
        /// </exception>
        public StreamInfo GetStreamInfo()
        {
            ThrowIfInvalid();

            var streamInfo = lsl_get_info(handle);
            if (streamInfo == IntPtr.Zero)
                throw new LSLException("Failed to get stream info of this outlet.");

            return new StreamInfo(streamInfo, true);
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking and conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with <see cref="GetLocalClock"/>.
        /// If the timestamp is omitted (set to 0.0), the current time is used.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the <paramref name="pushThrough"/>
        /// flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream outlet object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the size of the provided buffer <paramref name="sample"/> does not
        /// match the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(sbyte[] sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_ctp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(ReadOnlySpan<sbyte> sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            unsafe
            {
                fixed (sbyte* buffer = sample)
                {
                    CheckError(lsl_push_sample_ctp(handle, buffer, timestamp, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(short[] sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_stp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(ReadOnlySpan<short> sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            unsafe
            {
                fixed (short* buffer = sample)
                {
                    CheckError(lsl_push_sample_stp(handle, buffer, timestamp, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(int[] sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_itp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(ReadOnlySpan<int> sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            unsafe
            {
                fixed (int* buffer = sample)
                {
                    CheckError(lsl_push_sample_itp(handle, buffer, timestamp, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(long[] sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_ltp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(ReadOnlySpan<long> sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            unsafe
            {
                fixed (long* buffer = sample)
                {
                    CheckError(lsl_push_sample_ltp(handle, buffer, timestamp, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(float[] sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_ftp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(ReadOnlySpan<float> sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            unsafe
            {
                fixed (float* buffer = sample)
                {
                    CheckError(lsl_push_sample_ftp(handle, buffer, timestamp, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(double[] sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_dtp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(ReadOnlySpan<double> sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            unsafe
            {
                fixed (double* buffer = sample)
                {
                    CheckError(lsl_push_sample_dtp(handle, buffer, timestamp, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(string[] sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            var stringPointers = new IntPtr[sample.Length];
            for (int i = 0; i < sample.Length; ++i)
            {
                stringPointers[i] = StringToPtr(sample[i]);
                if (stringPointers[i] == IntPtr.Zero)
                {
                    for (int j = 0; j < i; ++j)
                        Marshal.FreeHGlobal(stringPointers[j]);

                    throw new ArgumentException(
                        $"Failed to convert {sample[i]} to bytes.",
                        nameof(sample));
                }
            }

#if STRING_MARSHALING_SCHEME_1
            var pinnedStringPointers = GCHandle.Alloc(stringPointers, GCHandleType.Pinned);
            var samplePointer = pinnedStringPointers.AddrOfPinnedObject();
#elif STRING_MARSHALING_SCHEME_2
            var samplePointer = Marshal.AllocHGlobal(IntPtr.Size * sample.Length);
            Marshal.Copy(stringPointers, 0, samplePointer, sample.Length);
#endif

            try
            {
#if STRING_MARSHALING_SCHEME_1 || STRING_MARSHALING_SCHEME_2
                CheckError(lsl_push_sample_strtp(handle, samplePointer, timestamp, pushThrough ? 1 : 0));
#else
                CheckError(lsl_push_sample_strtp(handle, stringPointers, timestamp, pushThrough ? 1 : 0));
#endif
            }
            finally
            {
                for (int i = 0; i < sample.Length; ++i)
                    Marshal.FreeHGlobal(stringPointers[i]);

#if STRING_MARSHALING_SCHEME_1
                pinnedStringPointers.Free();
#elif STRING_MARSHALING_SCHEME_2
                Marshal.FreeHGlobal(samplePointer);
#endif
            }
        }

#if !NET35
        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(ReadOnlySpan<string> sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            var stringPointers = new IntPtr[sample.Length];
            for (int i = 0; i < sample.Length; ++i)
            {
                stringPointers[i] = StringToPtr(sample[i]);
                if (stringPointers[i] == IntPtr.Zero)
                {
                    for (int j = 0; j < i; ++j)
                        Marshal.FreeHGlobal(stringPointers[j]);

                    throw new ArgumentException(
                        $"Failed to convert {sample[i]} to bytes.",
                        nameof(sample));
                }
            }

#if STRING_MARSHALING_SCHEME_1
            var pinnedStringPointers = GCHandle.Alloc(stringPointers, GCHandleType.Pinned);
            var samplePointer = pinnedStringPointers.AddrOfPinnedObject();
#elif STRING_MARSHALING_SCHEME_2
            var samplePointer = Marshal.AllocHGlobal(IntPtr.Size * sample.Length);
            Marshal.Copy(stringPointers, 0, samplePointer, sample.Length);
#endif

            try
            {
#if STRING_MARSHALING_SCHEME_1 || STRING_MARSHALING_SCHEME_2
                CheckError(lsl_push_sample_strtp(handle, samplePointer, timestamp, pushThrough ? 1 : 0));
#else
                CheckError(lsl_push_sample_strtp(handle, stringPointers, timestamp, pushThrough ? 1 : 0));
#endif
            }
            finally
            {
                for (int i = 0; i < sample.Length; ++i)
                    Marshal.FreeHGlobal(stringPointers[i]);

#if STRING_MARSHALING_SCHEME_1
                pinnedStringPointers.Free();
#elif STRING_MARSHALING_SCHEME_2
                Marshal.FreeHGlobal(samplePointer);
#endif
            }
        }
#endif

        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(byte[] sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();

            if (sample == null)
                throw new ArgumentNullException(nameof(sample));

            // TODO: Check sample length.

            CheckError(lsl_push_sample_vtp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(ReadOnlySpan<byte> sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();

            // TODO: Check sample length.

            unsafe
            {
                fixed (byte* buffer = sample)
                {
                    CheckError(lsl_push_sample_vtp(handle, buffer, timestamp, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <summary>
        /// Pushes a sample consisting of values pointed to by a pointer into the outlet.
        /// </summary>
        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(IntPtr sample, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();

            if (sample == IntPtr.Zero)
                throw new ArgumentNullException(nameof(sample));

            CheckError(lsl_push_sample_vtp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a sample consisting of variable-length byte arrays into the outlet.
        /// </summary>
        /// <param name="sample">
        /// An array of byte arrays, where each inner array represents data for one
        /// channel. The length of this array must match the <see cref="ChannelCount"/>
        /// of the stream outlet.
        /// </param>
        /// <param name="lengths">
        /// Optional. An array specifying the number of bytes to push for each channel.
        /// If null, the full length of each byte array in <paramref name="sample"/>
        /// is used.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with <see cref="GetLocalClock"/>.
        /// If the timestamp is omitted (set to 0.0), the current time is used.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the <paramref name="pushThrough"/>
        /// flag.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="sample"/> is null, or if any of the inner byte
        /// arrays in <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the length of <paramref name="sample"/> does not match the
        /// channel count of the stream outlet, if any of the inner byte arrays
        /// in <paramref name="sample"/> is empty, if <paramref name="lengths"/>
        /// is provided but its length does not match the channel count, or if any
        /// value in <paramref name="lengths"/> is less than or equal to 0 or greater
        /// than the length of the corresponding byte array in <paramref name="sample"/>.
        /// </exception>
        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(byte[][] sample, int[] lengths = null, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            if (lengths != null)
            {
                CheckLengthBuffer(lengths, ChannelCount);

                for (int i = 0; i < ChannelCount; ++i)
                {
                    if (sample[i] == null)
                        throw new ArgumentNullException(nameof(sample));

                    if (sample[i].Length == 0)
                        throw new ArgumentException(nameof(sample));

                    if (lengths[i] <= 0 || lengths[i] > sample[i].Length)
                        throw new ArgumentException(nameof(lengths));
                }
            }
            else
            {
                lengths = new int[ChannelCount];

                for (int i = 0; i < ChannelCount; ++i)
                {
                    if (sample[i] == null)
                        throw new ArgumentNullException(nameof(sample));

                    if (sample[i].Length == 0)
                        throw new ArgumentException(nameof(sample));

                    lengths[i] = sample[i].Length;
                }
            }

            var bytesPointers = new IntPtr[ChannelCount];
            for (int i = 0; i < ChannelCount; ++i)
            {
                bytesPointers[i] = Marshal.AllocHGlobal(lengths[i]);
                Marshal.Copy(sample[i], 0, bytesPointers[i], lengths[i]);
            }

#if STRING_MARSHALING_SCHEME_2
            var samplePointer = Marshal.AllocHGlobal(IntPtr.Size * ChannelCount);
            Marshal.Copy(bytesPointers, 0, samplePointer, sample.Length);
#endif

            try
            {
                unsafe
                {
                    fixed (int* lengthsBuffer = lengths)
                    {
#if STRING_MARSHALING_SCHEME_2
                        CheckError(lsl_push_sample_buftp(handle, samplePointer, (uint*)lengthsBuffer, timestamp, pushThrough ? 1 : 0));
#else
                        CheckError(lsl_push_sample_buftp(handle, bytesPointers, (uint*)lengthsBuffer, timestamp, pushThrough ? 1 : 0));
#endif
                    }
                }
            }
            finally
            {
                for (int i = 0; i < sample.Length; ++i)
                    Marshal.FreeHGlobal(bytesPointers[i]);

#if STRING_MARSHALING_SCHEME_2
                Marshal.FreeHGlobal(samplePointer);
#endif
            }
        }

        /// <summary>
        /// Pushes a sample consisting of memory buffers into the outlet.
        /// </summary>
        /// <param name="sample">
        /// An array of IntPtr, where each IntPtr points to a memory buffer containing
        /// data for one channel. The length of this array must match the channel
        /// count of the stream outlet.
        /// </param>
        /// <param name="lengths">
        /// An array specifying the number of elements to push for each channel. The
        /// length of this array must match the channel count of the stream outlet.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with <see cref="GetLocalClock"/>.
        /// If the timestamp is omitted (set to 0.0), the current time is used.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the <paramref name="pushThrough"/>
        /// flag.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="sample"/> or <paramref name="lengths"/> is
        /// null, or if any element in <paramref name="sample"/> is IntPtr.Zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the length of <paramref name="sample"/> or <paramref name="lengths"/> 
        /// does not match the channel count (<see cref="ChannelCount"/>) of the
        /// stream outlet, or if any element in <paramref name="lengths"/> is less
        /// than or equal to 0.
        /// </exception>
        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public unsafe void PushSample(IntPtr[] sample, int[] lengths, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);
            CheckLengthBuffer(lengths, ChannelCount);

            for (int i = 0; i < ChannelCount; ++i)
            {
                if (sample[i] == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(sample));

                if (lengths[i] <= 0)
                    throw new ArgumentException(nameof(lengths));
            }

#if STRING_MARSHALING_SCHEME_2
            var samplePointer = Marshal.AllocHGlobal(IntPtr.Size * sample.Length);
            Marshal.Copy(sample, 0, samplePointer, sample.Length);
#endif

            try
            {
                unsafe
                {
                    fixed (int* lengthsBuffer = lengths)
                    {
#if STRING_MARSHALING_SCHEME_2
                        CheckError(lsl_push_sample_buftp(handle, samplePointer, (uint*)lengthsBuffer, timestamp, pushThrough ? 1 : 0));
#else
                        CheckError(lsl_push_sample_buftp(handle, sample, (uint*)lengthsBuffer, timestamp, pushThrough ? 1 : 0));
#endif
                    }
                }
            }
            finally
            {
#if STRING_MARSHALING_SCHEME_2
                Marshal.FreeHGlobal(samplePointer);
#endif
            }
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking and conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with <see cref="GetLocalClock"/>.
        /// If the timestamp is omitted (set to 0.0), the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the pushThrough flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream outlet object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the size of the provided buffer is not a multiple of the stream's
        /// channel count.
        /// </exception>
        public void PushChunk(sbyte[] chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ctp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(ReadOnlySpan<sbyte> chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            unsafe
            {
                fixed (sbyte* buffer = chunk)
                {
                    CheckError(lsl_push_chunk_ctp(handle, buffer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(short[] chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_stp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(ReadOnlySpan<short> chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            unsafe
            {
                fixed (short* buffer = chunk)
                {
                    CheckError(lsl_push_chunk_stp(handle, buffer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(int[] chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_itp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(ReadOnlySpan<int> chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            unsafe
            {
                fixed (int* buffer = chunk)
                {
                    CheckError(lsl_push_chunk_itp(handle, buffer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(long[] chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ltp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(ReadOnlySpan<long> chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            unsafe
            {
                fixed (long* buffer = chunk)
                {
                    CheckError(lsl_push_chunk_ltp(handle, buffer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(float[] chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ftp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(ReadOnlySpan<float> chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            unsafe
            {
                fixed (float* buffer = chunk)
                {
                    CheckError(lsl_push_chunk_ftp(handle, buffer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(double[] chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_dtp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(ReadOnlySpan<double> chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            unsafe
            {
                fixed (double* buffer = chunk)
                {
                    CheckError(lsl_push_chunk_dtp(handle, buffer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(string[] chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            var stringPointers = new IntPtr[chunk.Length];
            for (int i = 0; i < chunk.Length; ++i)
            {
                stringPointers[i] = StringToPtr(chunk[i]);
                if (stringPointers[i] == IntPtr.Zero)
                {
                    for (int j = 0; j < i; ++j)
                        Marshal.FreeHGlobal(stringPointers[j]);

                    throw new ArgumentException(
                        $"Failed to convert {chunk[i]} to bytes.",
                        nameof(chunk));
                }
            }

#if STRING_MARSHALING_SCHEME_1
            var pinnedStringPointers = GCHandle.Alloc(stringPointers, GCHandleType.Pinned);
            var chunkPointer = pinnedStringPointers.AddrOfPinnedObject();
#elif STRING_MARSHALING_SCHEME_2
            var chunkPointer = Marshal.AllocHGlobal(IntPtr.Size * chunk.Length);
            Marshal.Copy(stringPointers, 0, chunkPointer, chunk.Length);
#endif

            try
            {
#if STRING_MARSHALING_SCHEME_1 || STRING_MARSHALING_SCHEME_2
                CheckError(lsl_push_chunk_strtp(handle, chunkPointer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
#else
                CheckError(lsl_push_chunk_strtp(handle, stringPointers, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
#endif
            }
            finally
            {
                for (int i = 0; i < chunk.Length; ++i)
                    Marshal.FreeHGlobal(stringPointers[i]);

#if STRING_MARSHALING_SCHEME_1
                pinnedStringPointers.Free();
#elif STRING_MARSHALING_SCHEME_2
                Marshal.FreeHGlobal(chunkPointer);
#endif
            }
        }

#if !NET35
        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(ReadOnlySpan<string> chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            var stringPointers = new IntPtr[chunk.Length];
            for (int i = 0; i < chunk.Length; ++i)
            {
                stringPointers[i] = StringToPtr(chunk[i]);
                if (stringPointers[i] == IntPtr.Zero)
                {
                    for (int j = 0; j < i; ++j)
                        Marshal.FreeHGlobal(stringPointers[j]);

                    throw new ArgumentException(
                        $"Failed to convert {chunk[i]} to bytes.",
                        nameof(chunk));
                }
            }

#if STRING_MARSHALING_SCHEME_1
            var pinnedStringPointers = GCHandle.Alloc(stringPointers, GCHandleType.Pinned);
            var chunkPointer = pinnedStringPointers.AddrOfPinnedObject();
#elif STRING_MARSHALING_SCHEME_2
            var chunkPointer = Marshal.AllocHGlobal(IntPtr.Size * chunk.Length);
            Marshal.Copy(stringPointers, 0, chunkPointer, chunk.Length);
#endif

            try
            {
#if STRING_MARSHALING_SCHEME_1 || STRING_MARSHALING_SCHEME_2
                CheckError(lsl_push_chunk_strtp(handle, chunkPointer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
#else
                CheckError(lsl_push_chunk_strtp(handle, stringPointers, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
#endif
            }
            finally
            {
                for (int i = 0; i < chunk.Length; ++i)
                    Marshal.FreeHGlobal(stringPointers[i]);

#if STRING_MARSHALING_SCHEME_1
                pinnedStringPointers.Free();
#elif STRING_MARSHALING_SCHEME_2
                Marshal.FreeHGlobal(chunkPointer);
#endif
            }
        }
#endif

        /// <summary>
        /// Pushes a chunk of multiplexed samples consisting of variable-length byte
        /// arrays into the outlet.
        /// </summary>
        /// <param name="chunk">
        /// An array of byte arrays holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="lengths">
        /// Optional. An array specifying the number of bytes to push for each element.
        /// If null, the full length of each byte array in <paramref name="chunk"/>
        /// is used.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with <see cref="GetLocalClock"/>.
        /// If the timestamp is omitted (set to 0.0), the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the pushThrough flag.
        /// </param>
        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(byte[][] chunk, int[] lengths = null, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            if (lengths != null)
            {
                if (lengths.Length != chunk.Length)
                    throw new ArgumentException(nameof(lengths));

                for (int i = 0; i < chunk.Length; ++i)
                {
                    if (chunk[i] == null)
                        throw new ArgumentNullException(nameof(chunk));

                    if (chunk[i].Length == 0)
                        throw new ArgumentException(nameof(chunk));

                    if (lengths[i] <= 0 || lengths[i] > chunk[i].Length)
                        throw new ArgumentException(nameof(lengths));
                }
            }
            else
            {
                lengths = new int[chunk.Length];

                for (int i = 0; i < chunk.Length; ++i)
                {
                    if (chunk[i] == null)
                        throw new ArgumentNullException(nameof(chunk));

                    if (chunk[i].Length == 0)
                        throw new ArgumentException(nameof(chunk));

                    lengths[i] = chunk[i].Length;
                }
            }

            var bytesPointers = new IntPtr[chunk.Length];
            for (int i = 0; i < chunk.Length; ++i)
            {
                bytesPointers[i] = Marshal.AllocHGlobal(lengths[i]);
                Marshal.Copy(chunk[i], 0, bytesPointers[i], lengths[i]);
            }

#if STRING_MARSHALING_SCHEME_2
            var chunkPointer = Marshal.AllocHGlobal(IntPtr.Size * chunk.Length);
            Marshal.Copy(bytesPointers, 0, chunkPointer, chunk.Length);
#endif

            try
            {
                unsafe
                {
                    fixed (int* lengthsBuffer = lengths)
                    {
#if STRING_MARSHALING_SCHEME_2
                        CheckError(lsl_push_chunk_buftp(handle, chunkPointer, (uint*)lengthsBuffer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
#else
                        CheckError(lsl_push_chunk_buftp(handle, bytesPointers, (uint*)lengthsBuffer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
#endif
                    }
                }
            }
            finally
            {
                for (int i = 0; i < chunk.Length; ++i)
                    Marshal.FreeHGlobal(bytesPointers[i]);

#if STRING_MARSHALING_SCHEME_2
                Marshal.FreeHGlobal(chunkPointer);
#endif
            }
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples consisting of memory buffers
        /// into the outlet.
        /// </summary>
        /// <param name="chunk">
        /// An array of IntPtr, where each IntPtr points to a memory buffer containing
        /// data for one channel.
        /// </param>
        /// <param name="lengths">
        /// An array specifying the number of elements to push for each value. The
        /// length of this array must match the length of <paramref name="chunk"/>.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with <see cref="GetLocalClock"/>.
        /// If the timestamp is omitted (set to 0.0), the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the pushThrough flag.
        /// </param>
        /// <inheritdoc cref="PushChunk(byte[][], int[], double, bool)"/>
        public void PushChunk(IntPtr[] chunk, int[] lengths, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            if (lengths == null)
                throw new ArgumentNullException(nameof(lengths));

            if (lengths.Length != chunk.Length)
                throw new ArgumentException(nameof(lengths));

            for (int i = 0; i < lengths.Length; ++i)
            {
                if (chunk[i] == IntPtr.Zero)
                    throw new ArgumentException(nameof(chunk));

                if (lengths[i] <= 0)
                    throw new ArgumentException(nameof(lengths));
            }

#if STRING_MARSHALING_SCHEME_2
            var chunkPointer = Marshal.AllocHGlobal(IntPtr.Size * chunk.Length);
            Marshal.Copy(chunk, 0, chunkPointer, chunk.Length);
#endif

            try
            {
                unsafe
                {
                    fixed (int* lengthsBuffer = lengths)
                    {
#if STRING_MARSHALING_SCHEME_2
                        CheckError(lsl_push_chunk_buftp(handle, chunkPointer, (uint*)lengthsBuffer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
#else
                        CheckError(lsl_push_chunk_buftp(handle, chunk, (uint*)lengthsBuffer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
#endif
                    }
                }
            }
            finally
            {
#if STRING_MARSHALING_SCHEME_2
                Marshal.FreeHGlobal(chunkPointer);
#endif
            }
        }

        /// <exception cref="ArgumentException">
        /// Thrown if the number of columns in <paramref name="chunk"/> does not match
        /// the <see cref="ChannelCount"/>.
        /// </exception>
        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(sbyte[,] chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            unsafe
            {
                fixed (sbyte* buffer = chunk)
                {
                    CheckError(lsl_push_chunk_ctp(handle, buffer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
                }
            }
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double, bool)"/>
        public void PushChunk(short[,] chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            unsafe
            {
                fixed (short* buffer = chunk)
                {
                    CheckError(lsl_push_chunk_stp(handle, buffer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
                }
            }
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double, bool)"/>
        public void PushChunk(int[,] chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            unsafe
            {
                fixed (int* buffer = chunk)
                {
                    CheckError(lsl_push_chunk_itp(handle, buffer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
                }
            }
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double, bool)"/>
        public void PushChunk(long[,] chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            unsafe
            {
                fixed (long* buffer = chunk)
                {
                    CheckError(lsl_push_chunk_ltp(handle, buffer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
                }
            }
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double, bool)"/>
        public void PushChunk(float[,] chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            unsafe
            {
                fixed (float* buffer = chunk)
                {
                    CheckError(lsl_push_chunk_ftp(handle, buffer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
                }
            }
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double, bool)"/>
        public void PushChunk(double[,] chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            unsafe
            {
                fixed (double* buffer = chunk)
                {
                    CheckError(lsl_push_chunk_dtp(handle, buffer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
                }
            }
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double, bool)"/>
        public void PushChunk(string[,] chunk, double timestamp = 0.0, bool pushThrough = true)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            var stringPointers = new IntPtr[chunk.Length];

            int samples = chunk.GetLength(0);

            for (int s = 0; s < samples; ++s)
            {
                for (int c = 0; c < ChannelCount; ++c)
                {
                    var index = s * ChannelCount + c;
                    stringPointers[index] = StringToPtr(chunk[s, c]);
                    if (stringPointers[index] == IntPtr.Zero)
                    {
                        for (int i = 0; i < index; ++i)
                            Marshal.FreeHGlobal(stringPointers[i]);

                        throw new ArgumentException(
                            $"Failed to convert {chunk[s, c]} to bytes.",
                            nameof(chunk));
                    }
                }
            }

#if STRING_MARSHALING_SCHEME_1
            var pinnedStringPointers = GCHandle.Alloc(stringPointers, GCHandleType.Pinned);
            var chunkPointer = pinnedStringPointers.AddrOfPinnedObject();
#elif STRING_MARSHALING_SCHEME_2
            var chunkPointer = Marshal.AllocHGlobal(IntPtr.Size * chunk.Length);
            Marshal.Copy(stringPointers, 0, chunkPointer, chunk.Length);
#endif

            try
            {
#if STRING_MARSHALING_SCHEME_1 || STRING_MARSHALING_SCHEME_2
                CheckError(lsl_push_chunk_strtp(handle, chunkPointer, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
#else
                CheckError(lsl_push_chunk_strtp(handle, stringPointers, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
#endif
            }
            finally
            {
                for (int i = 0; i < stringPointers.Length; ++i)
                    Marshal.FreeHGlobal(stringPointers[i]);

#if STRING_MARSHALING_SCHEME_1
                pinnedStringPointers.Free();
#elif STRING_MARSHALING_SCHEME_2
                Marshal.FreeHGlobal(chunkPointer);
#endif
            }
        }

        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the samples through to the receivers instead of buffering
        /// them with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the pushThrough flag.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the size of the provided buffer is not a multiple of the stream's
        /// channel count, or if <paramref name="timestamps"/> is provided but its length
        /// does not match the number of samples in the chunk.
        /// </exception>
        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(sbyte[] chunk, double[] timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_ctnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(ReadOnlySpan<sbyte> chunk, ReadOnlySpan<double> timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            unsafe
            {
                fixed (sbyte* chunkBuffer = chunk)
                fixed (double* timestampsBuffer = timestamps)
                {
                    CheckError(lsl_push_chunk_ctnp(handle, chunkBuffer, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(short[] chunk, double[] timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_stnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(ReadOnlySpan<short> chunk, ReadOnlySpan<double> timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            unsafe
            {
                fixed (short* chunkBuffer = chunk)
                fixed (double* timestampsBuffer = timestamps)
                {
                    CheckError(lsl_push_chunk_stnp(handle, chunkBuffer, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(int[] chunk, double[] timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_itnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(ReadOnlySpan<int> chunk, ReadOnlySpan<double> timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            unsafe
            {
                fixed (int* chunkBuffer = chunk)
                fixed (double* timestampsBuffer = timestamps)
                {
                    CheckError(lsl_push_chunk_itnp(handle, chunkBuffer, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(long[] chunk, double[] timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_ltnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(ReadOnlySpan<long> chunk, ReadOnlySpan<double> timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            unsafe
            {
                fixed (long* chunkBuffer = chunk)
                fixed (double* timestampsBuffer = timestamps)
                {
                    CheckError(lsl_push_chunk_ltnp(handle, chunkBuffer, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(float[] chunk, double[] timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_ftnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(ReadOnlySpan<float> chunk, ReadOnlySpan<double> timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            unsafe
            {
                fixed (float* chunkBuffer = chunk)
                fixed (double* timestampsBuffer = timestamps)
                {
                    CheckError(lsl_push_chunk_ftnp(handle, chunkBuffer, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(double[] chunk, double[] timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_dtnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

#if !NET35
        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(ReadOnlySpan<double> chunk, ReadOnlySpan<double> timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            unsafe
            {
                fixed (double* chunkBuffer = chunk)
                fixed (double* timestampsBuffer = timestamps)
                {
                    CheckError(lsl_push_chunk_dtnp(handle, chunkBuffer, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
                }
            }
        }
#endif

        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(string[] chunk, double[] timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            var stringPointers = new IntPtr[chunk.Length];
            for (int i = 0; i < chunk.Length; ++i)
            {
                stringPointers[i] = StringToPtr(chunk[i]);
                if (stringPointers[i] == IntPtr.Zero)
                {
                    for (int j = 0; j < i; ++j)
                        Marshal.FreeHGlobal(stringPointers[j]);

                    throw new ArgumentException(
                        $"Failed to convert {chunk[i]} to bytes.",
                        nameof(chunk));
                }
            }

#if STRING_MARSHALING_SCHEME_1
            var pinnedStringPointers = GCHandle.Alloc(stringPointers, GCHandleType.Pinned);
            var chunkPointer = pinnedStringPointers.AddrOfPinnedObject();
#elif STRING_MARSHALING_SCHEME_2
            var chunkPointer = Marshal.AllocHGlobal(IntPtr.Size * chunk.Length);
            Marshal.Copy(stringPointers, 0, chunkPointer, chunk.Length);
#endif

            try
            {
                unsafe
                {
                    fixed (double* timestampsBuffer = timestamps)
                    {
#if STRING_MARSHALING_SCHEME_1 || STRING_MARSHALING_SCHEME_2
                        CheckError(lsl_push_chunk_strtnp(handle, chunkPointer, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
#else
                        CheckError(lsl_push_chunk_strtnp(handle, stringPointers, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
#endif
                    }
                }
            }
            finally
            {
                for (int i = 0; i < chunk.Length; ++i)
                    Marshal.FreeHGlobal(stringPointers[i]);

#if STRING_MARSHALING_SCHEME_1
                pinnedStringPointers.Free();
#elif STRING_MARSHALING_SCHEME_2
                Marshal.FreeHGlobal(chunkPointer);
#endif
            }
        }

#if !NET35
        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(ReadOnlySpan<string> chunk, ReadOnlySpan<double> timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            var stringPointers = new IntPtr[chunk.Length];
            for (int i = 0; i < chunk.Length; ++i)
            {
                stringPointers[i] = StringToPtr(chunk[i]);
                if (stringPointers[i] == IntPtr.Zero)
                {
                    for (int j = 0; j < i; ++j)
                        Marshal.FreeHGlobal(stringPointers[j]);

                    throw new ArgumentException(
                        $"Failed to convert {chunk[i]} to bytes.",
                        nameof(chunk));
                }
            }

#if STRING_MARSHALING_SCHEME_1
            var pinnedStringPointers = GCHandle.Alloc(stringPointers, GCHandleType.Pinned);
            var chunkPointer = pinnedStringPointers.AddrOfPinnedObject();
#elif STRING_MARSHALING_SCHEME_2
            var chunkPointer = Marshal.AllocHGlobal(IntPtr.Size * chunk.Length);
            Marshal.Copy(stringPointers, 0, chunkPointer, chunk.Length);
#endif

            try
            {
                unsafe
                {
                    fixed (double* timestampsBuffer = timestamps)
                    {
#if STRING_MARSHALING_SCHEME_1 || STRING_MARSHALING_SCHEME_2
                        CheckError(lsl_push_chunk_strtnp(handle, chunkPointer, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
#else
                        CheckError(lsl_push_chunk_strtnp(handle, stringPointers, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
#endif
                    }
                }
            }
            finally
            {
                for (int i = 0; i < chunk.Length; ++i)
                    Marshal.FreeHGlobal(stringPointers[i]);

#if STRING_MARSHALING_SCHEME_1
                pinnedStringPointers.Free();
#elif STRING_MARSHALING_SCHEME_2
                Marshal.FreeHGlobal(chunkPointer);
#endif
            }
        }
#endif

        /// <summary>
        /// Pushes a chunk of multiplexed samples consisting of variable-length byte
        /// arrays into the outlet.
        /// </summary>
        /// <param name="chunk">
        /// An array of byte arrays holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="lengths">
        /// Optional. An array specifying the number of bytes to push for each element.
        /// If null, the full length of each byte array in <paramref name="chunk"/>
        /// is used.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the pushThrough flag.
        /// </param>
        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(byte[][] chunk, int[] lengths, double[] timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            if (lengths != null)
            {
                if (lengths.Length != chunk.Length)
                    throw new ArgumentException(nameof(lengths));

                for (int i = 0; i < chunk.Length; ++i)
                {
                    if (chunk[i] == null)
                        throw new ArgumentNullException(nameof(chunk));

                    if (chunk[i].Length == 0)
                        throw new ArgumentException(nameof(chunk));

                    if (lengths[i] <= 0 || lengths[i] > chunk[i].Length)
                        throw new ArgumentException(nameof(lengths));
                }
            }
            else
            {
                lengths = new int[chunk.Length];

                for (int i = 0; i < chunk.Length; ++i)
                {
                    if (chunk[i] == null)
                        throw new ArgumentNullException(nameof(chunk));

                    if (chunk[i].Length == 0)
                        throw new ArgumentException(nameof(chunk));

                    lengths[i] = chunk[i].Length;
                }
            }

            var bytesPointers = new IntPtr[chunk.Length];
            for (int i = 0; i < chunk.Length; ++i)
            {
                bytesPointers[i] = Marshal.AllocHGlobal(lengths[i]);
                Marshal.Copy(chunk[i], 0, bytesPointers[i], lengths[i]);
            }

#if STRING_MARSHALING_SCHEME_2
            var chunkPointer = Marshal.AllocHGlobal(IntPtr.Size * chunk.Length);
            Marshal.Copy(bytesPointers, 0, chunkPointer, chunk.Length);
#endif

            try
            {
                unsafe
                {
                    fixed (int* lengthsBuffer = lengths)
                    {
#if STRING_MARSHALING_SCHEME_2
                        CheckError(lsl_push_chunk_buftnp(handle, chunkPointer, (uint*)lengthsBuffer, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
#else
                        CheckError(lsl_push_chunk_buftnp(handle, bytesPointers, (uint*)lengthsBuffer, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
#endif
                    }
                }
            }
            finally
            {
                for (int i = 0; i < chunk.Length; ++i)
                    Marshal.FreeHGlobal(bytesPointers[i]);

#if STRING_MARSHALING_SCHEME_2
                Marshal.FreeHGlobal(chunkPointer);
#endif
            }
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples consisting of memory buffers
        /// into the outlet.
        /// </summary>
        /// <param name="chunk">
        /// An array of IntPtr, where each IntPtr points to a memory buffer containing
        /// data for one channel.
        /// </param>
        /// <param name="lengths">
        /// An array specifying the number of elements to push for each value. The
        /// length of this array must match the length of <paramref name="chunk"/>.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the pushThrough flag.
        /// </param>
        /// <inheritdoc cref="PushChunk(byte[][], int[], double, bool)"/>
        public void PushChunk(IntPtr[] chunk, int[] lengths, double[] timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);

            if (lengths == null)
                throw new ArgumentNullException(nameof(lengths));

            if (lengths.Length != chunk.Length)
                throw new ArgumentException(nameof(lengths));

            CheckTimestampBuffer(timestamps, samples);

            for (int i = 0; i < lengths.Length; ++i)
            {
                if (chunk[i] == IntPtr.Zero)
                    throw new ArgumentException(nameof(chunk));

                if (lengths[i] <= 0)
                    throw new ArgumentException(nameof(lengths));
            }

#if STRING_MARSHALING_SCHEME_2
            var chunkPointer = Marshal.AllocHGlobal(IntPtr.Size * chunk.Length);
            Marshal.Copy(chunk, 0, chunkPointer, chunk.Length);
#endif

            try
            {
                unsafe
                {
                    fixed (int* lengthsBuffer = lengths)
                    {
#if STRING_MARSHALING_SCHEME_2
                        CheckError(lsl_push_chunk_buftnp(handle, chunkPointer, (uint*)lengthsBuffer, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
#else
                        CheckError(lsl_push_chunk_buftnp(handle, chunk, (uint*)lengthsBuffer, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
#endif
                    }
                }
            }
            finally
            {
#if STRING_MARSHALING_SCHEME_2
                Marshal.FreeHGlobal(chunkPointer);
#endif
            }
        }

        /// <exception cref="ArgumentException">
        /// Thrown if the number of columns in <paramref name="chunk"/> does not match
        /// the <see cref="ChannelCount"/>, or if <paramref name="timestamps"/> is
        /// provided but its length does not match the number of samples in the chunk.
        /// </exception>
        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(sbyte[,] chunk, double[] timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            unsafe
            {
                fixed (sbyte* chunkBuffer = chunk)
                fixed (double* timestampsBuffer = timestamps)
                {
                    CheckError(lsl_push_chunk_ctnp(handle, chunkBuffer, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
                }
            }
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[], bool)"/>
        public void PushChunk(short[,] chunk, double[] timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            unsafe
            {
                fixed (short* chunkBuffer = chunk)
                fixed (double* timestampsBuffer = timestamps)
                {
                    CheckError(lsl_push_chunk_stnp(handle, chunkBuffer, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
                }
            }
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[], bool)"/>
        public void PushChunk(int[,] chunk, double[] timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            unsafe
            {
                fixed (int* chunkBuffer = chunk)
                fixed (double* timestampsBuffer = timestamps)
                {
                    CheckError(lsl_push_chunk_itnp(handle, chunkBuffer, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
                }
            }
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[], bool)"/>
        public void PushChunk(long[,] chunk, double[] timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            unsafe
            {
                fixed (long* chunkBuffer = chunk)
                fixed (double* timestampsBuffer = timestamps)
                {
                    CheckError(lsl_push_chunk_ltnp(handle, chunkBuffer, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
                }
            }
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[], bool)"/>
        public void PushChunk(float[,] chunk, double[] timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            unsafe
            {
                fixed (float* chunkBuffer = chunk)
                fixed (double* timestampsBuffer = timestamps)
                {
                    CheckError(lsl_push_chunk_ftnp(handle, chunkBuffer, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
                }
            }
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[], bool)"/>
        public void PushChunk(double[,] chunk, double[] timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            unsafe
            {
                fixed (double* chunkBuffer = chunk)
                fixed (double* timestampsBuffer = timestamps)
                {
                    CheckError(lsl_push_chunk_dtnp(handle, chunkBuffer, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
                }
            }
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[], bool)"/>
        public void PushChunk(string[,] chunk, double[] timestamps, bool pushThrough = true)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            var stringPointers = new IntPtr[chunk.Length];

            int samples = chunk.GetLength(0);

            for (int s = 0; s < samples; ++s)
            {
                for (int c = 0; c < ChannelCount; ++c)
                {
                    var index = s * ChannelCount + c;
                    stringPointers[index] = StringToPtr(chunk[s, c]);
                    if (stringPointers[index] == IntPtr.Zero)
                    {
                        for (int i = 0; i < index; ++i)
                            Marshal.FreeHGlobal(stringPointers[i]);

                        throw new ArgumentException(
                            $"Failed to convert {chunk[s, c]} to bytes.",
                            nameof(chunk));
                    }
                }
            }

#if STRING_MARSHALING_SCHEME_1
            var pinnedStringPointers = GCHandle.Alloc(stringPointers, GCHandleType.Pinned);
            var chunkPointer = pinnedStringPointers.AddrOfPinnedObject();
#elif STRING_MARSHALING_SCHEME_2
            var chunkPointer = Marshal.AllocHGlobal(IntPtr.Size * chunk.Length);
            Marshal.Copy(stringPointers, 0, chunkPointer, chunk.Length);
#endif

            try
            {
                unsafe
                {
                    fixed (double* timestampsBuffer = timestamps)
                    {
#if STRING_MARSHALING_SCHEME_1 || STRING_MARSHALING_SCHEME_2
                        CheckError(lsl_push_chunk_strtnp(handle, chunkPointer, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
#else
                        CheckError(lsl_push_chunk_strtnp(handle, stringPointers, (uint)chunk.Length, timestampsBuffer, pushThrough ? 1 : 0));
#endif
                    }
                }
            }
            finally
            {
                for (int i = 0; i < stringPointers.Length; ++i)
                    Marshal.FreeHGlobal(stringPointers[i]);

#if STRING_MARSHALING_SCHEME_1
                pinnedStringPointers.Free();
#elif STRING_MARSHALING_SCHEME_2
                Marshal.FreeHGlobal(chunkPointer);
#endif
            }
        }

        /// <summary>
        /// Checks whether consumers are currently registered.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether consumers are currently registered.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream outlet object is invalid.
        /// </exception>
        /// <remarks>
        /// While it does not hurt, there is technically no reason to push samples
        /// if there are no consumers.
        /// </remarks>
        public bool HaveConsumers()
        {
            ThrowIfInvalid();

            return Convert.ToBoolean(lsl_have_consumers(handle));
        }

        /// <summary>
        /// Waits until some consumer shows up (without wasting resources).
        /// </summary>
        /// <param name="timeout">
        /// Specifies the timeout of the operation in seconds. Default value is
        /// <see cref="Forever"/> which indicates no timeout.
        /// </param>
        /// <returns>
        /// A boolean value indicating whether the wait was successful. Returns true
        /// if the wait was successful, false if the timeout expired.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream outlet object is invalid.
        /// </exception>
        public bool WaitForConsumers(double timeout = Forever)
        {
            ThrowIfInvalid();

            return Convert.ToBoolean(lsl_wait_for_consumers(handle, timeout));
        }

        /// <summary>
        /// Destroys the underlying native stream outlet handle.
        /// </summary>
        /// <remarks>
        /// The outlet will no longer be discoverable after destruction, and all
        /// connected inlets will stop delivering data.
        /// </remarks>
        protected override void DestroyLSLObject()
        {
            lsl_destroy_outlet(handle);
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static IntPtr CreateOutlet(
            StreamInfo streamInfo,
            int chunkSize,
            int maxBuffered,
            TransportOptions transportOptions)
        {
            if (streamInfo == null)
                throw new ArgumentNullException(nameof(streamInfo));

            if (streamInfo.DangerousGetHandle() == IntPtr.Zero)
                throw new ArgumentException(nameof(streamInfo));

            return lsl_create_outlet_ex(
                streamInfo.DangerousGetHandle(),
                chunkSize,
                maxBuffered,
                (lsl_transport_options_t)transportOptions);
        }
    }
}


// References:
// [How To P/Invoke char* [] in C#](https://stackoverflow.com/questions/25137788/how-to-p-invoke-char-in-c-sharp)
// [PInvoke an Array of a Byte Arrays](https://stackoverflow.com/questions/778475/pinvoke-an-array-of-a-byte-arrays)
// [Fixed statement with jagged array](https://stackoverflow.com/questions/4033054/fixed-statement-with-jagged-array)
// [Understanding GCHandle.Alloc pinning in C#](https://stackoverflow.com/questions/45620700/understanding-gchandle-alloc-pinning-in-c-sharp)
// [Keyword "unsafe" - before method or block of code?](https://stackoverflow.com/questions/9459419/keyword-unsafe-before-method-or-block-of-code)
// [How should I pass an array of strings to a C library using P/Invoke?](https://stackoverflow.com/questions/63077017/how-should-i-pass-an-array-of-strings-to-a-c-library-using-p-invoke)
// [Marshal an array of strings from C# to C code using p/invoke](https://stackoverflow.com/questions/13317931/marshal-an-array-of-strings-from-c-sharp-to-c-code-using-p-invoke)
// [Marshal.StringToHGlobalAnsi(String) Method](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.marshal.stringtohglobalansi?view=net-10.0)
// https://github.com/MicrosoftDocs/cpp-docs/blob/main/docs/dotnet/how-to-marshal-ansi-strings-using-cpp-interop.md
// [Fixed statement with jagged array](https://stackoverflow.com/questions/4033054/fixed-statement-with-jagged-array)
