using System;

using SharpLSL.Interop;

using static SharpLSL.Interop.LSL;
using static SharpLSL.LSL;

namespace SharpLSL
{
    /// <summary>
    /// Represents a stream outlet for pushing time series data to the lab network.
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
        /// If unspecified, each push operation yields one chunk. Inlets can override
        /// this setting.
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
        /// TODO:
        /// </param>
        /// <exception cref="LSLException">
        /// Thrown when creating a new instance of <see cref="StreamOutlet"/> fails.
        /// </exception>
        public StreamOutlet(StreamInfo streamInfo, int chunkSize = 0, int maxBuffered = 360, TransportOptions transportOptions = TransportOptions.Default)
            : base(lsl_create_outlet_ex(streamInfo.DangerousGetHandle(), chunkSize, maxBuffered, (lsl_transport_options_t)transportOptions))
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

            return new StreamInfo(streamInfo);
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking and conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
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
        public void PushSample(sbyte[] sample)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_c(handle, sample));
        }

        /// <inheritdoc cref="PushSample(sbyte[])"/>
        public void PushSample(short[] sample)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_s(handle, sample));
        }

        /// <inheritdoc cref="PushSample(sbyte[])"/>
        public void PushSample(int[] sample)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_i(handle, sample));
        }

        /// <inheritdoc cref="PushSample(sbyte[])"/>
        public void PushSample(long[] sample)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_l(handle, sample));
        }

        /// <inheritdoc cref="PushSample(sbyte[])"/>
        public void PushSample(float[] sample)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_f(handle, sample));
        }

        /// <inheritdoc cref="PushSample(sbyte[])"/>
        public void PushSample(double[] sample)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_d(handle, sample));
        }

        /// <inheritdoc cref="PushSample(sbyte[])"/>
        public void PushSample(string[] sample)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_str(handle, sample)); // TODO: Test
        }

        /// <inheritdoc cref="PushSample(sbyte[])"/>
        public void PushSample(byte[][] sample)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            
        }

        /// <inheritdoc cref="PushSample(sbyte[])"/>
        public void PushSample(byte[] sample)
        {
            ThrowIfInvalid();

            if (sample == null)
                throw new ArgumentNullException(nameof(sample));

            CheckError(lsl_push_sample_v(handle, sample));
        }

        /// <summary>
        /// Pushes a pointer to some values as a sample into the outlet.
        /// </summary>
        /// <inheritdoc cref="PushSample(sbyte[])"/>
        public unsafe void PushSample(IntPtr sample)
        {
            ThrowIfInvalid();

            if (sample == IntPtr.Zero)
                throw new ArgumentNullException(nameof(sample));

            CheckError(lsl_push_sample_v(handle, sample.ToPointer()));
        }

        /// <summary>
        /// Pushes a sample consisting of variable-length byte arrays into the outlet.
        /// </summary>
        /// <param name="sample">
        /// An array of byte arrays, where each inner array represents data for one
        /// channel. The length of this array must match the channel count of the
        /// stream outlet.
        /// </param>
        /// <param name="lengths">
        /// Optional. An array specifying the number of bytes to push for each channel.
        /// If null, the full length of each byte array in <paramref name="sample"/>
        /// is used.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="sample"/> is null, or if any of the inner byte
        /// arrays in <paramref name="sample"/> are null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the length of <paramref name="sample"/> does not match the
        /// channel count of the stream outlet, if any of the inner byte arrays
        /// in <paramref name="sample"/> are empty, if <paramref name="lengths"/>
        /// is provided but its length does not match the channel count, or if any
        /// value in <paramref name="lengths"/> is less than or equal to 0 or greater
        /// than the length of the corresponding byte array in <paramref name="sample"/>.
        /// </exception>
        /// <inheritdoc cref="PushSample(sbyte[])"/>
        // TODO: Test
        public unsafe void PushSample(byte[][] sample, int[] lengths = null)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            if (lengths != null)
                CheckLengthBuffer(lengths, ChannelCount);

            var ulengths = stackalloc uint[ChannelCount];
            for (int i = 0; i < ChannelCount; ++i)
            {
                if (sample[i] == null)
                    throw new ArgumentNullException(nameof(sample));

                if (sample[i].Length == 0)
                    throw new ArgumentException(nameof(sample));

                if (lengths == null)
                {
                    ulengths[i] = (uint)sample[i].Length;
                }
                else
                {
                    if (lengths[i] <= 0 || lengths[i] > sample[i].Length)
                        throw new ArgumentException(nameof(lengths));

                    ulengths[i] = (uint)lengths[i];
                }
            }

            CheckError(lsl_push_sample_buf(handle, sample, ulengths));
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
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="sample"/> or <paramref name="lengths"/> is
        /// null, or if any element in <paramref name="sample"/> is <see cref="IntPtr.Zero"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the length of <paramref name="sample"/> or <paramref name="lengths"/> 
        /// does not match the channel count (<see cref="ChannelCount"/>) of the
        /// stream outlet, or if any element in <paramref name="lengths"/> is less
        /// than or equal to 0.
        /// </exception>
        /// <inheritdoc cref="PushSample(sbyte[])"/>
        public unsafe void PushSample(IntPtr[] sample, int[] lengths)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);
            CheckLengthBuffer(lengths, ChannelCount);

            var ulengths = stackalloc uint[ChannelCount];
            for (int i = 0; i < ChannelCount; ++i)
            {
                if (sample[i] == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(sample));

                if (lengths[i] <= 0)
                    throw new ArgumentException(nameof(lengths));

                ulengths[i] = (uint)lengths[i];
            }

            CheckError(lsl_push_sample_buf(handle, sample, ulengths));
        }

        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with <see cref="GetLocalClock"/>.
        /// If the timestamp is 0.0, the current time is used.
        /// </param>
        /// <inheritdoc cref="PushSample(sbyte[])"/>
        public void PushSample(sbyte[] sample, double timestamp)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_ct(handle, sample, timestamp));
        }

        /// <inheritdoc cref="PushSample(sbyte[], double)"/>
        public void PushSample(short[] sample, double timestamp)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_st(handle, sample, timestamp));
        }

        /// <inheritdoc cref="PushSample(sbyte[], double)"/>
        public void PushSample(int[] sample, double timestamp)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_it(handle, sample, timestamp));
        }

        /// <inheritdoc cref="PushSample(sbyte[], double)"/>
        public void PushSample(long[] sample, double timestamp)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_lt(handle, sample, timestamp));
        }

        /// <inheritdoc cref="PushSample(sbyte[], double)"/>
        public void PushSample(float[] sample, double timestamp)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_ft(handle, sample, timestamp));
        }

        /// <inheritdoc cref="PushSample(sbyte[], double)"/>
        public void PushSample(double[] sample, double timestamp)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_dt(handle, sample, timestamp));
        }

        /// <inheritdoc cref="PushSample(sbyte[], double)"/>
        public void PushSample(string[] sample, double timestamp)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_strt(handle, sample, timestamp));
        }

        /// <inheritdoc cref="PushSample(sbyte[], double)"/>
        public void PushSample(byte[] sample, double timestamp)
        {
            ThrowIfInvalid();

            if (sample == null)
                throw new ArgumentNullException(nameof(sample));

            CheckError(lsl_push_sample_vt(handle, sample, timestamp));
        }

        /// <summary>
        /// Pushes a sample consisting of values pointed to by a pointer into the outlet.
        /// </summary>
        /// <inheritdoc cref="PushSample(sbyte[], double)"/>
        public unsafe void PushSample(IntPtr sample, double timestamp)
        {
            ThrowIfInvalid();

            if (sample == IntPtr.Zero)
                throw new ArgumentNullException(nameof(sample));

            CheckError(lsl_push_sample_vt(handle, sample.ToPointer(), timestamp));
        }

        /// <summary>
        /// Pushes a sample consisting of variable-length byte arrays into the outlet.
        /// </summary>
        /// <param name="sample">
        /// An array of byte arrays, where each inner array represents data for one
        /// channel. The length of this array must match the channel count of the
        /// stream outlet.
        /// </param>
        /// <param name="lengths">
        /// Optional. An array specifying the number of bytes to push for each channel.
        /// If null, the full length of each byte array in <paramref name="sample"/>
        /// is used.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with <see cref="GetLocalClock"/>.
        /// If the timestamp is 0.0, the current time is used.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="sample"/> is null, or if any of the inner byte
        /// arrays in <paramref name="sample"/> are null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the length of <paramref name="sample"/> does not match the
        /// channel count of the stream outlet, if any of the inner byte arrays
        /// in <paramref name="sample"/> are empty, if <paramref name="lengths"/>
        /// is provided but its length does not match the channel count, or if any
        /// value in <paramref name="lengths"/> is less than or equal to 0 or greater
        /// than the length of the corresponding byte array in <paramref name="sample"/>.
        /// </exception>
        /// <inheritdoc cref="PushSample(byte[][], int[])"/>
        // TODO: Test
        public unsafe void PushSample(byte[][] sample, int[] lengths, double timestamp)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            if (lengths != null)
                CheckLengthBuffer(lengths, ChannelCount);

            var ulengths = stackalloc uint[ChannelCount];
            for (int i = 0; i < ChannelCount; ++i)
            {
                if (sample[i] == null)
                    throw new ArgumentNullException(nameof(sample));

                if (sample[i].Length == 0)
                    throw new ArgumentException(nameof(sample));

                if (lengths == null)
                {
                    ulengths[i] = (uint)sample[i].Length;
                }
                else
                {
                    if (lengths[i] <= 0 || lengths[i] > sample[i].Length)
                        throw new ArgumentException(nameof(lengths));

                    ulengths[i] = (uint)lengths[i];
                }
            }

            CheckError(lsl_push_sample_buft(handle, sample, ulengths, timestamp));
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
        /// If the timestamp is 0.0, the current time is used.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="sample"/> or <paramref name="lengths"/> is
        /// null, or if any element in <paramref name="sample"/> is <see cref="IntPtr.Zero"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the length of <paramref name="sample"/> or <paramref name="lengths"/> 
        /// does not match the channel count (<see cref="ChannelCount"/>) of the
        /// stream outlet, or if any element in <paramref name="lengths"/> is less
        /// than or equal to 0.
        /// </exception>
        /// <inheritdoc cref="PushSample(IntPtr[], int[])"/>
        public unsafe void PushSample(IntPtr[] sample, int[] lengths, double timestamp)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);
            CheckLengthBuffer(lengths, ChannelCount);

            var ulengths = stackalloc uint[ChannelCount];
            for (int i = 0; i < ChannelCount; ++i)
            {
                if (sample[i] == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(sample));

                if (lengths[i] <= 0)
                    throw new ArgumentException(nameof(lengths));

                ulengths[i] = (uint)lengths[i];
            }

            CheckError(lsl_push_sample_buft(handle, sample, ulengths, timestamp));
        }

        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with <see cref="GetLocalClock"/>.
        /// If the timestamp is 0.0, the current time is used.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the <paramref name="pushThrough"/>
        /// flag.
        /// </param>
        /// <inheritdoc cref="PushSample(sbyte[], double)"/>
        public void PushSample(sbyte[] sample, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_ctp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(short[] sample, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_stp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(int[] sample, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_itp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(long[] sample, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_ltp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(float[] sample, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_ftp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(double[] sample, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_dtp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(string[] sample, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_strtp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushSample(sbyte[], double, bool)"/>
        public void PushSample(byte[] sample, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();

            if (sample == null)
                throw new ArgumentNullException(nameof(sample));

            CheckError(lsl_push_sample_vtp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }
        
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with <see cref="GetLocalClock"/>.
        /// If the timestamp is 0.0, the current time is used.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the <paramref name="pushThrough"/>
        /// flag.
        /// </param>
        /// <inheritdoc cref="PushSample(IntPtr, double)"/>
        public unsafe void PushSample(IntPtr sample, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();

            if (sample == IntPtr.Zero)
                throw new ArgumentNullException(nameof(sample));

            CheckError(lsl_push_sample_vtp(handle, sample.ToPointer(), timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a sample consisting of variable-length byte arrays into the outlet.
        /// </summary>
        /// <param name="sample">
        /// An array of byte arrays, where each inner array represents data for one
        /// channel. The length of this array must match the channel count of the
        /// stream outlet.
        /// </param>
        /// <param name="lengths">
        /// Optional. An array specifying the number of bytes to push for each channel.
        /// If null, the full length of each byte array in <paramref name="sample"/>
        /// is used.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with <see cref="GetLocalClock"/>.
        /// If the timestamp is 0.0, the current time is used.
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
        /// <inheritdoc cref="PushSample(byte[][], int[], double)"/>
        // TODO: Test
        public unsafe void PushSample(byte[][] sample, int[] lengths, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            if (lengths != null)
                CheckLengthBuffer(lengths, ChannelCount);

            var ulengths = stackalloc uint[ChannelCount];
            for (int i = 0; i < ChannelCount; ++i)
            {
                if (sample[i] == null)
                    throw new ArgumentNullException(nameof(sample));

                if (sample[i].Length == 0)
                    throw new ArgumentException(nameof(sample));

                if (lengths == null)
                {
                    ulengths[i] = (uint)sample[i].Length;
                }
                else
                {
                    if (lengths[i] <= 0 || lengths[i] > sample[i].Length)
                        throw new ArgumentException(nameof(lengths));

                    ulengths[i] = (uint)lengths[i];
                }
            }
            
            CheckError(lsl_push_sample_buftp(handle, sample, ulengths, timestamp, pushThrough ? 1 : 0));
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
        /// If the timestamp is 0.0, the current time is used.
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
        /// <inheritdoc cref="PushSample(IntPtr[], int[], double)"/>
        public unsafe void PushSample(IntPtr[] sample, int[] lengths, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);
            CheckLengthBuffer(lengths, ChannelCount);

            var ulengths = stackalloc uint[ChannelCount];
            for (int i = 0; i < ChannelCount; ++i)
            {
                if (sample[i] == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(sample));

                if (lengths[i] <= 0)
                    throw new ArgumentException(nameof(lengths));

                ulengths[i] = (uint)lengths[i];
            }

            CheckError(lsl_push_sample_buftp(handle, sample, ulengths, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking and conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
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
        public void PushChunk(sbyte[] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_c(handle, chunk, (uint)chunk.Length));
        }

        /// <inheritdoc cref="PushChunk(sbyte[])"/>
        public void PushChunk(short[] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_s(handle, chunk, (uint)chunk.Length));
        }

        /// <inheritdoc cref="PushChunk(sbyte[])"/>
        public void PushChunk(int[] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_i(handle, chunk, (uint)chunk.Length));
        }

        /// <inheritdoc cref="PushChunk(sbyte[])"/>
        public void PushChunk(long[] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_l(handle, chunk, (uint)chunk.Length));
        }

        /// <inheritdoc cref="PushChunk(sbyte[])"/>
        public void PushChunk(float[] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_f(handle, chunk, (uint)chunk.Length));
        }

        /// <inheritdoc cref="PushChunk(sbyte[])"/>
        public void PushChunk(double[] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_d(handle, chunk, (uint)chunk.Length));
        }

        /// <inheritdoc cref="PushChunk(sbyte[])"/>
        public void PushChunk(string[] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_str(handle, chunk, (uint)chunk.Length));
        }

        /// <exception cref="ArgumentException">
        /// Thrown if the number of columns in <paramref name="chunk"/> does not match
        /// the <see cref="ChannelCount"/>.
        /// </exception>
        /// <inheritdoc cref="PushChunk(sbyte[])"/>
        public void PushChunk(sbyte[,] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_c(handle, chunk, (uint)chunk.Length));
        }

        /// <inheritdoc cref="PushChunk(sbyte[])"/>
        public void PushChunk(short[,] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_s(handle, chunk, (uint)chunk.Length));
        }

        /// <inheritdoc cref="PushChunk(sbyte[])"/>
        public void PushChunk(int[,] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_i(handle, chunk, (uint)chunk.Length));
        }

        /// <inheritdoc cref="PushChunk(sbyte[])"/>
        public void PushChunk(long[,] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_l(handle, chunk, (uint)chunk.Length));
        }

        /// <inheritdoc cref="PushChunk(sbyte[])"/>
        public void PushChunk(float[,] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_f(handle, chunk, (uint)chunk.Length));
        }

        /// <inheritdoc cref="PushChunk(sbyte[])"/>
        public void PushChunk(double[,] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_d(handle, chunk, (uint)chunk.Length));
        }

        /// <inheritdoc cref="PushChunk(sbyte[])"/>
        public void PushChunk(string[,] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_str(handle, chunk, (uint)chunk.Length));
        }

        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>. If omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <inheritdoc cref="PushChunk(sbyte[])"/>
        public void PushChunk(sbyte[] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ct(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double)"/>
        public void PushChunk(short[] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_st(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double)"/>
        public void PushChunk(int[] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_it(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double)"/>
        public void PushChunk(long[] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_lt(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double)"/>
        public void PushChunk(float[] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ft(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double)"/>
        public void PushChunk(double[] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_dt(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double)"/>
        public void PushChunk(string[] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_strt(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <exception cref="ArgumentException">
        /// Thrown if the number of columns in <paramref name="chunk"/> does not match
        /// the <see cref="ChannelCount"/>.
        /// </exception>
        /// <inheritdoc cref="PushChunk(sbyte[], double)"/>
        public void PushChunk(sbyte[,] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ct(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double)"/>
        public void PushChunk(short[,] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_st(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double)"/>
        public void PushChunk(int[,] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_it(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double)"/>
        public void PushChunk(long[,] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_lt(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double)"/>
        public void PushChunk(float[,] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ft(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double)"/>
        public void PushChunk(double[,] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_dt(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double)"/>
        public void PushChunk(string[,] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_strt(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>. If omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the pushThrough flag.
        /// </param>
        /// <inheritdoc cref="PushChunk(sbyte[], double)"/>
        public void PushChunk(sbyte[] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ctp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(short[] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_stp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(int[] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_itp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(long[] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ltp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(float[] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ftp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(double[] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_dtp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(string[] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_strtp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <exception cref="ArgumentException">
        /// Thrown if the number of columns in <paramref name="chunk"/> does not match
        /// the <see cref="ChannelCount"/>.
        /// </exception>
        /// <inheritdoc cref="PushChunk(sbyte[], double, bool)"/>
        public void PushChunk(sbyte[,] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ctp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double, bool)"/>
        public void PushChunk(short[,] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_stp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double, bool)"/>
        public void PushChunk(int[,] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_itp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double, bool)"/>
        public void PushChunk(long[,] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ltp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double, bool)"/>
        public void PushChunk(float[,] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ftp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double, bool)"/>
        public void PushChunk(double[,] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_dtp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double, bool)"/>
        public void PushChunk(string[,] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_strtp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
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
        /// <inheritdoc cref="PushChunk(sbyte[])"/>
        public void PushChunk(sbyte[] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_ctn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double[])"/>
        public void PushChunk(short[] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_stn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double[])"/>
        public void PushChunk(int[] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_itn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double[])"/>
        public void PushChunk(long[] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_ltn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double[])"/>
        public void PushChunk(float[] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_ftn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double[])"/>
        public void PushChunk(double[] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_dtn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double[])"/>
        public void PushChunk(string[] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_strtn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double[])"/>
        public void PushChunk(sbyte[,] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_ctn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[])"/>
        public void PushChunk(short[,] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_stn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[])"/>
        public void PushChunk(int[,] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_itn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[])"/>
        public void PushChunk(long[,] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_ltn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[])"/>
        public void PushChunk(float[,] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_ftn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[])"/>
        public void PushChunk(double[,] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_dtn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[])"/>
        public void PushChunk(string[,] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_strtn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// A buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the samples through to the receivers instead of buffering
        /// them with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the pushThrough flag.
        /// </param>
        /// <inheritdoc cref="PushChunk(sbyte[], double[])"/>
        public void PushChunk(sbyte[] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_ctnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(short[] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_stnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(int[] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_itnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(long[] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_ltnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(float[] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_ftnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(double[] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_dtnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(string[] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_strtnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <exception cref="ArgumentException">
        /// Thrown if the number of columns in <paramref name="chunk"/> does not match
        /// the <see cref="ChannelCount"/>, or if <paramref name="timestamps"/> is
        /// provided but its length does not match the number of samples in the chunk.
        /// </exception>
        /// <inheritdoc cref="PushChunk(sbyte[], double[], bool)"/>
        public void PushChunk(sbyte[,] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_ctnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[], bool)"/>
        public void PushChunk(short[,] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_stnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[], bool)"/>
        public void PushChunk(int[,] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_itnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[], bool)"/>
        public void PushChunk(long[,] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_ltnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[], bool)"/>
        public void PushChunk(float[,] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_ftnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[], bool)"/>
        public void PushChunk(double[,] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_dtnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <inheritdoc cref="PushChunk(sbyte[,], double[], bool)"/>
        public void PushChunk(string[,] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_strtnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
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
    }
}


// References:
// [How To P/Invoke char* [] in C#](https://stackoverflow.com/questions/25137788/how-to-p-invoke-char-in-c-sharp)
// [PInvoke an Array of a Byte Arrays](https://stackoverflow.com/questions/778475/pinvoke-an-array-of-a-byte-arrays)
