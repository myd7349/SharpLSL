using System;
using System.Runtime.CompilerServices;

using SharpLSL.Interop;

using static SharpLSL.Interop.LSL;
using static SharpLSL.LSL;

namespace SharpLSL
{
    /// <summary>
    /// Represents a stream outlet object for pushing streaming data (and meta-data)
    /// to the lab network.
    /// </summary>
    public class StreamOutlet : LSLObject
    {
        /// <summary>
        /// Constructs a new stream outlet for pushing streaming data (and meta-data).
        /// This makes the stream discoverable.
        /// </summary>
        /// <param name="streamInfo">
        /// The stream information to use for creating this stream outlet. Stays
        /// constant over the lifetime of the outlet. The outlet makes a copy of
        /// the <paramref name="streamInfo"/> object upon construction (so the old
        /// info should still be destroyed).
        /// </param>
        /// <param name="chunkSize">
        /// Specifies the desired chunk granularity (in samples) for transmission.
        /// If unspecified, each push operation yields one chunk. Inlets can override
        /// this setting.
        /// </param>
        /// <param name="maxBuffered">
        /// Specifies the maximum amount of the data to buffer (in seconds if there
        /// is a nominal sampling rate, otherwise x100 in samples). A good default
        /// is 360, which corresponds to 6 minutes of data. Note that, for high
        /// bandwidth data you will almost certainly want to use a lower value here
        /// to avoid  running out of RAM.
        /// </param>
        /// <param name="transportOptions">
        /// Specifies the transport options.
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
        /// Speciies whether the wrapped handle should be released during the finalization phase.
        /// </param>
        /// <exception cref="LSLException">
        /// Thrown if the handle is invalid.
        /// </exception>
        public StreamOutlet(IntPtr handle, bool ownsHandle = true)
            : base(handle, ownsHandle)
        {
        }

        /// <summary>
        /// Gets number of channels of the stream.
        /// </summary>
        public int ChannelCount { get; }

        /// <summary>
        /// Retrieves a copy of the stream info provided by this outlet.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="StreamInfo"/> which was a copy of the stream
        /// info object that used to create the stream (and also has the additional
        /// network information fields assigned).
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
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
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(sbyte[] sample)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_c(handle, sample));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(short[] sample)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_s(handle, sample));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(int[] sample)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_i(handle, sample));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(long[] sample)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_l(handle, sample));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(float[] sample)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_f(handle, sample));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(double[] sample)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_d(handle, sample));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(string[] sample)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_str(handle, sample)); // TODO: Test
        }

        // TODO: lsl_push_sample_v

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(sbyte[] sample, double timestamp)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_ct(handle, sample, timestamp));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(short[] sample, double timestamp)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_st(handle, sample, timestamp));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(int[] sample, double timestamp)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_it(handle, sample, timestamp));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(long[] sample, double timestamp)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_lt(handle, sample, timestamp));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(float[] sample, double timestamp)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_ft(handle, sample, timestamp));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(double[] sample, double timestamp)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_dt(handle, sample, timestamp));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(string[] sample, double timestamp)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_strt(handle, sample, timestamp));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(sbyte[] sample, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_ctp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(short[] sample, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_stp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(int[] sample, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_itp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(long[] sample, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_ltp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(float[] sample, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_ftp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(double[] sample, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_dtp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a sample into the outlet. Handles type checking & conversion.
        /// </summary>
        /// <param name="sample">
        /// The sample data to push.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer <paramref name="sample"/> does not
        /// the channel count (<see cref="ChannelCount"/>) of the stream outlet.
        /// </exception>
        public void PushSample(string[] sample, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            CheckError(lsl_push_sample_strtp(handle, sample, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(sbyte[] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_c(handle, chunk, (uint)chunk.Length));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(short[] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_s(handle, chunk, (uint)chunk.Length));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(int[] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_i(handle, chunk, (uint)chunk.Length));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(long[] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_l(handle, chunk, (uint)chunk.Length));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(float[] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_f(handle, chunk, (uint)chunk.Length));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(double[] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_d(handle, chunk, (uint)chunk.Length));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(string[] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_str(handle, chunk, (uint)chunk.Length));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(sbyte[,] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_c(handle, chunk, (uint)chunk.Length));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(short[,] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_s(handle, chunk, (uint)chunk.Length));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(int[,] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_i(handle, chunk, (uint)chunk.Length));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(long[,] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_l(handle, chunk, (uint)chunk.Length));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(float[,] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_f(handle, chunk, (uint)chunk.Length));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(double[,] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_d(handle, chunk, (uint)chunk.Length));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(string[,] chunk)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_str(handle, chunk, (uint)chunk.Length));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(sbyte[] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ct(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(short[] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_st(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(int[] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_it(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(long[] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_lt(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(float[] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ft(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(double[] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_dt(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(string[] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_strt(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(sbyte[,] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ct(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(short[,] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_st(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(int[,] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_it(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(long[,] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_lt(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(float[,] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ft(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(double[,] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_dt(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(string[,] chunk, double timestamp)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_strt(handle, chunk, (uint)chunk.Length, timestamp));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(sbyte[] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ctp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(short[] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_stp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(int[] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_itp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(long[] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ltp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(float[] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ftp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(double[] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_dtp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(string[] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_strtp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(sbyte[,] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ctp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(short[,] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_stp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(int[,] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_itp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(long[,] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ltp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(float[,] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_ftp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(double[,] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_dtp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamp">
        /// The capture time of the most recent sample, in agreement with
        /// <see cref="GetLocalClock"/>; if omitted, the current time is used.
        /// The timestamps of other samples are automatically derived based on the
        /// sampling rate of the stream.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(string[,] chunk, double timestamp, bool pushThrough)
        {
            ThrowIfInvalid();
            CheckChunkBuffer(chunk, ChannelCount);

            CheckError(lsl_push_chunk_strtp(handle, chunk, (uint)chunk.Length, timestamp, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(sbyte[] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_ctn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(short[] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_stn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(int[] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_itn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(long[] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_ltn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(float[] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_ftn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(double[] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_dtn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(string[] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, samples);

            CheckError(lsl_push_chunk_strtn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(sbyte[,] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_ctn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(short[,] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_stn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(int[,] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_itn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(long[,] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_ltn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(float[,] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_ftn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(double[,] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_dtn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(string[,] chunk, double[] timestamps)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_strtn(handle, chunk, (uint)chunk.Length, timestamps));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(sbyte[] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_ctnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(short[] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_stnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(int[] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_itnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(long[] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_ltnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(float[] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_ftnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(double[] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_dtnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(string[] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_strtnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(sbyte[,] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_ctnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(short[,] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_stnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(int[,] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_itnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(long[,] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_ltnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(float[,] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_ftnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
        public void PushChunk(double[,] chunk, double[] timestamps, bool pushThrough)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBuffer(timestamps, chunk.GetLength(0));

            CheckError(lsl_push_chunk_dtnp(handle, chunk, (uint)chunk.Length, timestamps, pushThrough ? 1 : 0));
        }

        /// <summary>
        /// Pushes a chunk of multiplexed samples into the outlet. Handles type
        /// checking & conversion.
        /// </summary>
        /// <param name="chunk">
        /// A buffer of channel values holding the data for zero or more successive
        /// samples to send.
        /// </param>
        /// <param name="timestamps">
        /// The buffer holding one timestamp for each sample in the data buffer.
        /// </param>
        /// <param name="pushThrough">
        /// Whether to push the sample through to the receivers instead of buffering
        /// it with subsequent samples. Note that the chunk size, if specified at
        /// outlet construction, takes precedence over the push through flag.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided data buffer <paramref name="chunk"/> or timestamp
        /// buffer <paramref name="timestamps"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if size of the provided buffer is too small.
        /// </exception>
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
        /// A boolean value indicates whether consumers are currently registered.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <remarks>
        /// While it does not hurt, there is technically no reason to push samples
        /// if there is no consumer.
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
        /// A boolean value indicates whether the wait was successful. Returns true
        /// if the wait was successful, false if the timeout expired.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        public bool WaitForConsumers(double timeout = Forever)
        {
            ThrowIfInvalid();

            return Convert.ToBoolean(lsl_wait_for_consumers(handle, timeout));
        }

        /// <summary>
        /// Destroys the outlet and associated underlying native resource.
        /// </summary>
        /// <remarks>
        /// The outlet will no longer be discoverable after destruction and all
        /// connected inlets will stop delivering data.
        /// </remarks>
        protected override void DestroyLSLObject()
        {
            lsl_destroy_outlet(handle);
        }
    }
}
