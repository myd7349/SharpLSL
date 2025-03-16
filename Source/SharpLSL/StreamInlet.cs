using System;

using SharpLSL.Interop;

using static SharpLSL.Interop.LSL;
using static SharpLSL.LSL;

namespace SharpLSL
{
    /// <summary>
    /// Represents a stream inlet for receiving time series data from a single connected
    /// outlet.
    /// </summary>
    /// <remarks>
    /// <para>
    /// StreamInlet allows retrieval of samples from the lab network (in-order, with
    /// reliable (re-)transmission, optional type conversion, and optional failure
    /// recovery).
    /// </para>
    /// <para>
    /// In addition to the samples, metadata can be obtained (as an XML blob or
    /// alternatively through a small built-in DOM interface).
    /// </para>
    /// </remarks>
    public class StreamInlet : LSLObject
    {
        /// <summary>
        /// Constructs a new stream inlet from a resolved stream info.
        /// </summary>
        /// <param name="streamInfo">
        /// A resolved <see cref="StreamInfo"/> object, typically obtained from one
        /// of the resolver functions. The inlet creates a copy of this object during
        /// construction.
        /// </param>
        /// <param name="maxChunkLength">
        /// Specifies the maximum size, in samples, at which chunks are transmitted.
        /// Default is 0, which uses the chunk sizes preferred by the sender. Recording
        /// applications can use a generous size here (leaving it to the network how
        /// to pack things), while real-time applications may prefer smaller values
        /// (perhaps 1-sample) for finer granularity.
        /// </param>
        /// <param name="maxBufferLength">
        /// Specifies the maximum amount of data to buffer (in seconds if there
        /// is a nominal sampling rate, otherwise x100 in samples). Larger values are
        /// suitable for recording applications, while real-time applications would
        /// only buffer as much as they need to perform their next calculation. Default
        /// is 360, which corresponds to 6 minutes of data.
        /// </param>
        /// <param name="recover">
        /// Specifies whether to try to silently recover lost streams that are recoverable
        /// (those that have a source ID set). In all other cases (recover is false
        /// or the stream is not recoverable), most outlet functions may throw a <see cref="StreamLostException"/>
        /// if the stream's source is lost (e.g., due to an app or computer crash).
        /// It is generally a good idea to enable this, unless the application wants
        /// to act in a special way when a data provider has temporarily crashed.
        /// </param>
        /// <param name="transportOptions">
        /// Specifies additional options for data transport. Default is <see cref="TransportOptions.Default"/>.
        /// </param>
        /// <exception cref="LSLException">
        /// Thrown when creating a new instance of <see cref="StreamInlet"/> fails.
        /// </exception>
        /// <remarks>
        /// The <see cref="StreamInlet"/> may also be constructed with a fully specified
        /// <see cref="StreamInfo"/>, if the desired channel format and count are already
        /// known up-front, but this is strongly discouraged and should only ever be done
        /// if there is no time to resolve the stream up-front (e.g., due to limitations
        /// in the client program).
        /// </remarks>
        // TODO:
        // Comment on flags from liblsl:
        // An integer that is the result of bitwise OR'ing one or more options from
        // #lsl_transport_options_t together (e.g., #transp_bufsize_samples)
        public StreamInlet(
            StreamInfo streamInfo, int maxChunkLength = 0, int maxBufferLength = 360,
            bool recover = true, TransportOptions transportOptions = TransportOptions.Default)
            : base(lsl_create_inlet_ex(
                streamInfo.DangerousGetHandle(),
                maxBufferLength, maxChunkLength,
                recover ? 1 : 0,
                (lsl_transport_options_t)transportOptions))
        {
            ChannelCount = streamInfo.ChannelCount;
            sampleBytes_ = streamInfo.SampleBytes;
        }

        /// <summary>
        /// Constructs a new instance of the <see cref="StreamInlet"/> class that wraps
        /// a pre-existing stream inlet handle.
        /// </summary>
        /// <param name="handle">
        /// Specifies the handle to be wrapped.
        /// </param>
        /// <param name="ownsHandle">
        /// Specifies whether the wrapped handle should be released during the finalization
        /// phase.
        /// </param>
        /// <exception cref="LSLException">
        /// Thrown if the handle is invalid (IntPtr.Zero).
        /// </exception>
        public StreamInlet(IntPtr handle, bool ownsHandle = true)
            : base(handle, ownsHandle)
        {
            using (var streamInfo = GetStreamInfo())
            {
                ChannelCount = streamInfo.ChannelCount;
                sampleBytes_ = streamInfo.SampleBytes;
            }
        }

        /// <summary>
        /// Gets the number of channels in the stream.
        /// </summary>
        /// <seealso cref="StreamInfo.ChannelCount"/>
        public int ChannelCount { get; }

        /// <summary>
        /// Retrieves the complete information of the given stream, including the
        /// extended description.
        /// </summary>
        /// <param name="timeout">
        /// Specifies the timeout of the operation in seconds. The default value is
        /// <see cref="Forever"/>, which indicates no timeout.
        /// </param>
        /// <returns>
        /// An instance of <see cref="StreamInfo"/> containing a copy of the full stream
        /// information about the stream, including its extended description.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream inlet object is invalid.
        /// </exception>
        /// <exception cref="StreamLostException">
        /// Thrown if the stream source has been lost.
        /// </exception>
        /// <exception cref="TimeoutException">
        /// Thrown if the timeout has expired.
        /// </exception>
        /// <remarks>
        /// This method can be invoked at any time during the stream's lifetime.
        /// </remarks>
        /// <seealso cref="Resolve(int, double)"/>
        /// <seealso cref="Resolve(string, string, int, int, double)"/>
        /// <seealso cref="Resolve(string, int, int, double)"/>
        public StreamInfo GetStreamInfo(double timeout = Forever)
        {
            ThrowIfInvalid();

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var streamInfo = lsl_get_fullinfo(handle, timeout, ref errorCode);
#if false
            if (streamInfo != IntPtr.Zero)
                return new StreamInfo(streamInfo, true);

            CheckError(errorCode);
            // [Is there something like [[noreturn]] in C# to indicate the compiler that the method will never return a value?](https://stackoverflow.com/questions/48232105/is-there-something-like-noreturn-in-c-sharp-to-indicate-the-compiler-that-th)
#else
            CheckError(errorCode);
            return new StreamInfo(streamInfo, true);
#endif
        }

        /// <summary>
        /// Subscribes to the data stream.
        /// </summary>
        /// <param name="timeout">
        /// Specifies the timeout of the operation in seconds. The default value is
        /// <see cref="Forever"/>, which indicates no timeout.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream inlet object is invalid.
        /// </exception>
        /// <exception cref="StreamLostException">
        /// Thrown if the stream source has been lost.
        /// </exception>
        /// <exception cref="TimeoutException">
        /// Thrown if the timeout has expired.
        /// </exception>
        /// <remarks>
        /// All samples pushed in at the other end from this moment onwards will be
        /// queued and eventually be delivered in response to PullSample() or
        /// PullChunk() calls. Pulling a sample without a preceding OpenStream()
        /// call is permitted (the stream will then be opened implicitly).
        /// </remarks>
        /// <seealso cref="CloseStream"/>
        public void OpenStream(double timeout = Forever)
        {
            ThrowIfInvalid();

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            lsl_open_stream(handle, timeout, ref errorCode);
            CheckError(errorCode);
        }

        /// <summary>
        /// Drops the current data stream.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream inlet object is invalid.
        /// </exception>
        /// <remarks>
        /// All samples that are still buffered or in flight will be dropped, and
        /// transmission and buffering of data for this inlet will be stopped. If
        /// an application stops being interested in data from a source (temporarily
        /// or not) but keeps the outlet alive, it should call this method to avoid
        /// wasting unnecessary system and network resources.
        /// </remarks>
        /// <seealso cref="OpenStream(double)"/>
        public void CloseStream()
        {
            ThrowIfInvalid();

            lsl_close_stream(handle);
        }

        /// <summary>
        /// Retrieves an estimated time correction offset for the given stream.
        /// </summary>
        /// <param name="timeout">
        /// Specifies the timeout to acquire the first time correction estimate
        /// in seconds. The default value is <see cref="Forever"/>, which indicates
        /// no timeout.
        /// </param>
        /// <returns>
        /// The time correction estimate. This is the number that needs to be added
        /// to a timestamp that was remotely generated via <see cref="GetLocalClock"/>
        /// to map it into the local clock domain of this machine.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream inlet object is invalid.
        /// </exception>
        /// <exception cref="StreamLostException">
        /// Thrown if the stream source has been lost.
        /// </exception>
        /// <exception cref="TimeoutException">
        /// Thrown if the timeout has expired.
        /// </exception>
        /// <remarks>
        /// <para>
        /// The first call to this function takes several milliseconds until a reliable
        /// first estimate is obtained. Subsequent calls are instantaneous (and rely
        /// on periodic background updates).
        /// </para>
        /// <para>
        /// On a well-behaved network, the precision of these estimates should be below
        /// 1 ms (empirically it is within +/-0.2 ms).
        /// </para>
        /// <para>
        /// To get a measure of whether the network is well-behaved, use the extended
        /// version <see cref="TimeCorrection(ref double, ref double, double)"/> and
        /// check the uncertainty (i.e., the round-trip time). 0.2 ms is typical of
        /// wired networks. 2 ms is typical of wireless networks. The number can be
        /// much higher on poor networks.
        /// </para>
        /// </remarks>
        /// <seealso cref="TimeCorrection(ref double, ref double, double)"/>
        /// <seealso cref="PostProcessingOptions.ClockSync"/>
        public double TimeCorrection(double timeout = Forever)
        {
            ThrowIfInvalid();

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_time_correction(handle, timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        /// <param name="remoteTime">
        /// The current time of the remote computer that was used to generate this
        /// time correction. If desired, the client can fit time correction versus
        /// <paramref name="remoteTime"/> to improve the real-time time correction
        /// further.
        /// </param>
        /// <param name="uncertainty">
        /// The maximum uncertainty of the given time correction.
        /// </param>
        /// <param name="timeout">
        /// Specifies the timeout to acquire the first time correction estimate
        /// in seconds. The default value is <see cref="Forever"/>, which indicates
        /// no timeout.
        /// </param>
        /// <seealso cref="TimeCorrection(double)"/>
        /// <inheritdoc cref="TimeCorrection(double)"/>
        public double TimeCorrection(ref double remoteTime, ref double uncertainty, double timeout = Forever)
        {
            ThrowIfInvalid();

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_time_correction_ex(handle, ref remoteTime, ref uncertainty, timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        /// <summary>
        /// Sets post-processing flags to use.
        /// </summary>
        /// <param name="postProcessingOptions">
        /// The post-processing flags to use. This is the result of bitwise OR'ing
        /// one or more options from <see cref="PostProcessingOptions"/> together.
        /// The default is to use <see cref="PostProcessingOptions.All"/> to enable
        /// all options.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream inlet object is invalid.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the specified post-processing flags are invalid.
        /// </exception>
        /// <remarks>
        /// <para>
        /// By default, the inlet performs no post-processing and returns the ground
        /// truth timestamps, which can then be manually synchronized using
        /// <see cref="TimeCorrection(double)"/>, and then smoothed/dejittered if desired.
        /// </para>
        /// <para>
        /// This function allows automating these two and possibly more operations.
        /// </para>
        /// <para>
        /// When you enable this, you will no longer receive or be able to recover
        /// the original timestamps.
        /// </para>
        /// </remarks>
        public void SetPostProcessingOptions(
            PostProcessingOptions postProcessingOptions = PostProcessingOptions.All)
        {
            ThrowIfInvalid();

            CheckError(lsl_set_postprocessing(handle, (uint)postProcessingOptions));
        }

#if false
        public double PullSample(byte[] sample, double timeout = Forever)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var timestamp = lsl_pull_sample_c(handle, sample, sample.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return timestamp;
        }
#endif

        /// <summary>
        /// Pulls a sample from the inlet and reads it into an array of values. Handles
        /// type checking and conversion.
        /// </summary>
        /// <param name="sample">
        /// The buffer to hold the resulting values.
        /// </param>
        /// <param name="timeout">
        /// Specifies the timeout of the operation in seconds, if any. Default value
        /// is <see cref="Forever"/> which indicates no timeout. Use 0.0 to make a
        /// non-blocking call; in this case, a sample is only returned if one is
        /// currently buffered.
        /// </param>
        /// <returns>
        /// The capture time of the sample on the remote machine. Returns 0.0 if no
        /// new sample was available or the timeout expires. To remap this timestamp
        /// to the local clock, add the value returned by <see cref="TimeCorrection(double)"/>
        /// to it.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream inlet object is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the size of the provided buffer <paramref name="sample"/> does not
        /// match the channel count (<see cref="ChannelCount"/>) of the stream inlet.
        /// </exception>
        /// <exception cref="StreamLostException">
        /// Thrown if the stream source has been lost.
        /// </exception>
        /// <remarks>
        /// If the timeout expires before a new sample is received, the function returns 0.0.  
        /// This is not considered an error condition, so calling <see cref="GetLastError"/>
        /// will not return a timeout error.
        /// </remarks>
        public double PullSample(sbyte[] sample, double timeout = Forever)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var timestamp = lsl_pull_sample_c(handle, sample, sample.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return timestamp;
        }

        /// <inheritdoc cref="PullSample(sbyte[], double)"/>
        public double PullSample(short[] sample, double timeout = Forever)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var timestamp = lsl_pull_sample_s(handle, sample, sample.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return timestamp;
        }

        /// <inheritdoc cref="PullSample(sbyte[], double)"/>
        public double PullSample(int[] sample, double timeout = Forever)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var timestamp = lsl_pull_sample_i(handle, sample, sample.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return timestamp;
        }

        /// <inheritdoc cref="PullSample(sbyte[], double)"/>
        public double PullSample(long[] sample, double timeout = Forever)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var timestamp = lsl_pull_sample_l(handle, sample, sample.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return timestamp;
        }

        /// <inheritdoc cref="PullSample(sbyte[], double)"/>
        public double PullSample(float[] sample, double timeout = Forever)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var timestamp = lsl_pull_sample_f(handle, sample, sample.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return timestamp;
        }

        /// <inheritdoc cref="PullSample(sbyte[], double)"/>
        public double PullSample(double[] sample, double timeout = Forever)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var timestamp = lsl_pull_sample_d(handle, sample, sample.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return timestamp;
        }

        /// <inheritdoc cref="PullSample(sbyte[], double)"/>
        // TODO: Pull as byte sequence without calling PtrToString.
        public double PullSample(string[] sample, double timeout = Forever)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var buffer = new IntPtr[ChannelCount];

            try
            {
                // lsl_pull_sample_str automatically appends a '\0' at the end.
                var timestamp = lsl_pull_sample_str(handle, buffer, buffer.Length, timeout, ref errorCode);
                CheckError(errorCode);

                for (int i = 0; i < buffer.Length; ++i)
                    sample[i] = PtrToString(buffer[i]);

                return timestamp;
            }
            finally
            {
                for (int i = 0; i < buffer.Length; ++i)
                    lsl_destroy_string(buffer[i]);
            }
        }

        /// <summary>
        /// Pulls a sample from the inlet and reads it into a jagged array of byte
        /// values.
        /// </summary>
        /// <param name="sample">
        /// A jagged array to hold the resulting values. The outer array length must
        /// match the channel count of the stream. Inner arrays will be resized if
        /// necessary. 
        /// </param>
        /// <param name="timeout">
        /// Specifies the timeout of the operation in seconds, if any. The default value
        /// is <see cref="Forever"/>, which indicates no timeout. Use 0.0 to make a
        /// non-blocking call, in which case a sample is only returned if one is
        /// currently buffered.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="sample"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the size of the provided buffer <paramref name="sample"/> does not
        /// match the channel count (<see cref="ChannelCount"/>) of the stream inlet.
        /// </exception>
        /// <inheritdoc cref="PullSample(sbyte[], double)"/>
#if false
        public unsafe double PullSample(byte[][] sample, int[] lengths = null, double timeout = Forever)
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

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var timestamp = lsl_pull_sample_buf(handle, sample, ulengths, sample.Length, timeout, ref errorCode);
            CheckError(errorCode);

            return timestamp;
        }
#else
        // TODO: List version.
        public unsafe double PullSample(byte[][] sample, double timeout = Forever)
        {
            ThrowIfInvalid();
            CheckSampleBuffer(sample, ChannelCount);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var buffer = new IntPtr[ChannelCount];
            var lengths = stackalloc uint[ChannelCount];

            try
            {
                // lsl_pull_sample_buf does not add a '\0' at the end.
                // Instead, lsl_pull_sample_buf returns the length of the sequence
                // along with the received data.
                // Besides, the received sample data may contain 0's.
                var timestamp = lsl_pull_sample_buf(handle, buffer, lengths, buffer.Length, timeout, ref errorCode);
                CheckError(errorCode);

                for (int i = 0; i < buffer.Length; ++i)
                {
                    if (sample[i] == null || sample[i].Length != lengths[i])
                    {
#if NET35
                        sample[i] = new byte[lengths[i]];
#else
                        sample[i] = lengths[i] > 0 ? new byte[lengths[i]] : Array.Empty<byte>();
#endif
                    }

                    if (lengths[i] > 0)
                    {
                        fixed (byte* dest = sample[i])
                        {
#if NET35
                            byte* src = (byte*)buffer[i].ToPointer();
                            for (int c = 0; c < lengths[i]; ++c)
                                dest[c] = src[c];
#else
                            Buffer.MemoryCopy(
                                buffer[i].ToPointer(),
                                dest,
                                lengths[i],
                                lengths[i]);
#endif
                        }
                    }
                }

                return timestamp;
            }
            finally
            {
                for (int i = 0; i < buffer.Length; ++i)
                    lsl_destroy_string(buffer[i]);
            }
        }
#endif

        /// <summary>
        /// Pulls a sample from the inlet and reads it into a custom buffer. Overall
        /// size checking is performed, but no type checking or conversion is done.
        /// </summary>
        /// <param name="sample">
        /// The buffer to hold the resulting values. The buffer length must match
        /// the <see cref="StreamInfo.SampleBytes"/> of the stream.
        /// </param>
        /// <param name="timeout">
        /// Specifies the timeout of the operation in seconds, if any. The default value
        /// is <see cref="Forever"/>, which indicates no timeout. Use 0.0 to make a
        /// non-blocking call; in this case, a sample is only returned if one is
        /// currently buffered.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the size of the provided buffer is too small to hold the data of
        /// a sample.
        /// </exception>
        /// <remarks>
        /// <para>
        /// If the timeout expires before a new sample is received, the function
        /// returns 0.0; this case is not considered an error condition.
        /// </para>
        /// <para>
        /// Do not use this method for variable-size or string-formatted streams.
        /// </para>
        /// </remarks>
        /// <inheritdoc cref="PullSample(sbyte[], double)"/>
        public double PullSample(byte[] sample, double timeout = Forever)
        {
            ThrowIfInvalid();

            if (sample == null)
                throw new ArgumentNullException(nameof(sample));

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var timestamp = lsl_pull_sample_v(handle, sample, sample.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return timestamp;
        }

        /// <inheritdoc cref="PullSample(byte[], double)"/>
        public double PullSample(IntPtr sample, /*int length,*/ double timeout = Forever)
        {
            ThrowIfInvalid();

            if (sample == IntPtr.Zero)
                throw new ArgumentNullException(nameof(sample));

            //if (length <= 0)
            //    throw new ArgumentException(nameof(length));

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var timestamp = lsl_pull_sample_v(handle, sample, sampleBytes_, timeout, ref errorCode);
            CheckError(errorCode);
            return timestamp;
        }

        /// <summary>
        /// Pulls a chunk of data from the inlet and reads it into a buffer. Handles
        /// type checking and conversion.
        /// </summary>
        /// <param name="chunk">
        /// The buffer where the returned data chunk will be stored.
        /// <para>
        /// Note that the size of the provided data buffer must be a multiple of the
        /// stream's channel count.
        /// </para>
        /// </param>
        /// <param name="timestamps">
        /// The buffer for storing returned timestamps. If null, no timestamps will be
        /// returned. If provided, it must correspond to the same number of samples as
        /// <paramref name="chunk"/>.
        /// </param>
        /// <param name="timeout">
        /// Specifies the timeout of the operation in seconds, if any. When the timeout
        /// expires, the function may return before the entire buffer is filled. The
        /// default value of 0.0 will retrieve only data available for immediate pickup.
        /// </param>
        /// <returns>
        /// The number of channel data elements written to the data buffer.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided buffer <paramref name="chunk"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the size of the provided buffer is not a multiple of the stream's
        /// channel count, or if <paramref name="timestamps"/> is provided but its length
        /// does not match the number of samples in the chunk.
        /// </exception>
        /// <inheritdoc cref="PullSample(sbyte[], double)"/>
        public uint PullChunk(sbyte[] chunk, double[] timestamps, double timeout = 0.0)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBufferAllowNull(timestamps, samples);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_c(
                handle, chunk, timestamps, (uint)chunk.Length,
                (uint)(timestamps?.Length ?? 0), timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        /// <inheritdoc cref="PullChunk(sbyte[], double[], double)"/>
        public uint PullChunk(short[] chunk, double[] timestamps, double timeout = 0.0)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBufferAllowNull(timestamps, samples);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_s(
                handle, chunk, timestamps, (uint)chunk.Length,
                (uint)(timestamps?.Length ?? 0), timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        /// <inheritdoc cref="PullChunk(sbyte[], double[], double)"/>
        public uint PullChunk(int[] chunk, double[] timestamps, double timeout = 0.0)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBufferAllowNull(timestamps, samples);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_i(
                handle, chunk, timestamps, (uint)chunk.Length,
                (uint)(timestamps?.Length ?? 0), timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        /// <inheritdoc cref="PullChunk(sbyte[], double[], double)"/>
        public uint PullChunk(long[] chunk, double[] timestamps, double timeout = 0.0)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBufferAllowNull(timestamps, samples);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_l(
                handle, chunk, timestamps, (uint)chunk.Length,
                (uint)(timestamps?.Length ?? 0), timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        /// <inheritdoc cref="PullChunk(sbyte[], double[], double)"/>
        public uint PullChunk(float[] chunk, double[] timestamps, double timeout = 0.0)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBufferAllowNull(timestamps, samples);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_f(
                handle, chunk, timestamps, (uint)chunk.Length,
                (uint)(timestamps?.Length ?? 0), timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        /// <inheritdoc cref="PullChunk(sbyte[], double[], double)"/>
        public uint PullChunk(double[] chunk, double[] timestamps, double timeout = 0.0)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBufferAllowNull(timestamps, samples);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_d(
                handle, chunk, timestamps, (uint)chunk.Length,
                (uint)(timestamps?.Length ?? 0), timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        /// <inheritdoc cref="PullChunk(sbyte[], double[], double)"/>
        public uint PullChunk(string[] chunk, double[] timestamps, double timeout = 0.0)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBufferAllowNull(timestamps, samples);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var buffer = new IntPtr[chunk.Length];
            uint result = 0;

            try
            {
                result = lsl_pull_chunk_str(
                    handle, buffer, timestamps, (uint)chunk.Length,
                    (uint)(timestamps?.Length ?? 0), timeout, ref errorCode);
                CheckError(errorCode);

                for (int i = 0; i < result; ++i)
                    chunk[i] = PtrToString(buffer[i]);

                return result;
            }
            finally
            {
                for (int i = 0; i < result; ++i)
                    lsl_destroy_string(buffer[i]);
            }
        }

        /// <summary>
        /// Pulls a chunk of data from the inlet and reads it into a jagged array of
        /// byte buffers. Inner arrays will be resized if necessary. 
        /// </summary>
        /// <param name="chunk">
        /// A jagged array to hold the resulting values. The outer array length must
        /// be a multiple of the stream's channel count. Inner arrays will be resized
        /// if necessary. 
        /// </param>
        /// <param name="timestamps">
        /// The buffer where the returned timestamps will be stored. If it is null,
        /// no timestamps will be returned.
        /// </param>
        /// <param name="timeout">
        /// Specifies the timeout of the operation in seconds, if any. When the timeout
        /// expires, the function may return before the entire buffer is filled. The
        /// default value of 0.0 will retrieve only data available for immediate pickup. 
        /// </param>
        /// <inheritdoc cref="PullChunk(sbyte[], double[], double)"/>
        // TODO: Test
        public unsafe uint PullChunk(byte[][] chunk, double[] timestamps, double timeout = 0.0)
        {
            ThrowIfInvalid();

            var samples = CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBufferAllowNull(timestamps, samples);

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var buffer = new IntPtr[chunk.Length];
            var lengths = stackalloc uint[ChannelCount];
            uint result = 0;

            try
            {
                result = lsl_pull_chunk_buf(
                    handle, buffer, lengths, timestamps, (uint)chunk.Length,
                    (uint)(timestamps?.Length ?? 0), timeout, ref errorCode);
                CheckError(errorCode);

                for (int i = 0; i < result; ++i)
                {
                    if (chunk[i] == null || chunk[i].Length != lengths[i])
                    {
#if NET35
                        chunk[i] = new byte[lengths[i]];
#else
                        chunk[i] = lengths[i] > 0 ? new byte[lengths[i]] : Array.Empty<byte>();
#endif
                    }

                    if (lengths[i] > 0)
                    {
                        fixed (byte* dest = chunk[i])
                        {
#if NET35
                            byte* src = (byte*)buffer[i].ToPointer();
                            for (int c = 0; c < lengths[i]; ++c)
                                dest[c] = src[c];
#else
                            Buffer.MemoryCopy(
                                buffer[i].ToPointer(),
                                dest,
                                lengths[i],
                                lengths[i]);
#endif
                        }
                    }
                }

                return result;
            }
            finally
            {
                for (int i = 0; i < result; ++i)
                    lsl_destroy_string(buffer[i]);
            }
        }

        /// <exception cref="ArgumentException">
        /// Thrown if the number of columns in <paramref name="chunk"/> does not match
        /// the <see cref="ChannelCount"/>, or if <paramref name="timestamps"/> is not null
        /// and its length does not equal the number of rows in <paramref name="chunk"/>.
        /// </exception>
        /// <inheritdoc cref="PullChunk(sbyte[], double[], double)"/>
        public uint PullChunk(sbyte[,] chunk, double[] timestamps, double timeout = 0.0)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBufferAllowNull(timestamps, chunk.GetLength(0));

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_c(
                handle, chunk, timestamps, (uint)chunk.Length,
                (uint)(timestamps?.Length ?? 0), timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        /// <inheritdoc cref="PullChunk(sbyte[,], double[], double)"/>
        public uint PullChunk(short[,] chunk, double[] timestamps, double timeout = 0.0)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBufferAllowNull(timestamps, chunk.GetLength(0));

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_s(
                handle, chunk, timestamps, (uint)chunk.Length,
                (uint)(timestamps?.Length ?? 0), timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        /// <inheritdoc cref="PullChunk(sbyte[,], double[], double)"/>
        public uint PullChunk(int[,] chunk, double[] timestamps, double timeout = 0.0)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBufferAllowNull(timestamps, chunk.GetLength(0));

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_i(
                handle, chunk, timestamps, (uint)chunk.Length,
                (uint)(timestamps?.Length ?? 0), timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        /// <inheritdoc cref="PullChunk(sbyte[,], double[], double)"/>
        public uint PullChunk(long[,] chunk, double[] timestamps, double timeout = 0.0)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBufferAllowNull(timestamps, chunk.GetLength(0));

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_l(
                handle, chunk, timestamps, (uint)chunk.Length,
                (uint)(timestamps?.Length ?? 0), timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        /// <inheritdoc cref="PullChunk(sbyte[,], double[], double)"/>
        public uint PullChunk(float[,] chunk, double[] timestamps, double timeout = 0.0)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBufferAllowNull(timestamps, chunk.GetLength(0));

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_f(
                handle, chunk, timestamps, (uint)chunk.Length,
                (uint)(timestamps?.Length ?? 0), timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        /// <inheritdoc cref="PullChunk(sbyte[,], double[], double)"/>
        public uint PullChunk(double[,] chunk, double[] timestamps, double timeout = 0.0)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBufferAllowNull(timestamps, chunk.GetLength(0));

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_d(
                handle, chunk, timestamps, (uint)chunk.Length,
                (uint)(timestamps?.Length ?? 0), timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        /// <inheritdoc cref="PullChunk(sbyte[,], double[], double)"/>
        public uint PullChunk(string[,] chunk, double[] timestamps, double timeout = 0.0)
        {
            ThrowIfInvalid();

            CheckChunkBuffer(chunk, ChannelCount);
            CheckTimestampBufferAllowNull(timestamps, chunk.GetLength(0));

            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var buffer = new IntPtr[chunk.GetLength(0), chunk.GetLength(1)];
            uint samples = 0;

            try
            {
                var result = lsl_pull_chunk_str(handle, buffer, timestamps, (uint)chunk.Length, (uint)(timestamps?.Length ?? 0), timeout, ref errorCode);
                CheckError(errorCode);

                samples = result / (uint)ChannelCount;
                for (int s = 0; s < samples; ++s)
                {
                    for (int i = 0; i < chunk.GetLength(1); ++i)
                        chunk[s, i] = PtrToString(buffer[s, i]);
                }

                return result;
            }
            finally
            {
                for (int s = 0; s < samples; ++s)
                {
                    for (int i = 0; i < buffer.GetLength(1); ++i)
                        lsl_destroy_string(buffer[s, i]);
                }
            }
        }

        /// <summary>
        /// Queries whether samples are currently available for immediate pickup.
        /// </summary>
        /// <returns>
        /// The number of samples available, if the underlying implementation supports
        /// it; otherwise it will be 1 or 0.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream inlet object is invalid.
        /// </exception>
        /// <remarks>
        /// Note that it is not a good idea to use this method to determine whether
        /// a pull call would block. To be sure, set the pull timeout to 0.0 or an
        /// acceptably low value.
        /// </remarks>
        public uint SamplesAvailable()
        {
            ThrowIfInvalid();

            return lsl_samples_available(handle);
        }

        /// <summary>
        /// Drops all queued but not yet pulled samples.
        /// </summary>
        /// <returns>
        /// The number of dropped samples.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream inlet object is invalid.
        /// </exception>
        public uint Flush()
        {
            ThrowIfInvalid();

            return lsl_inlet_flush(handle);
        }

        /// <summary>
        /// Queries whether the clock was potentially reset since the last call to
        /// <see cref="WasClockReset"/>.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the clock was potentially reset.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream inlet object is invalid.
        /// </exception>
        /// <remarks>
        /// This is a rarely used function that is only needed for applications that
        /// combine multiple time correction values to estimate precise clock drift
        /// if they should tolerate cases where the source machine was hot-swapped
        /// or restarted.
        /// </remarks>
        public bool WasClockReset()
        {
            ThrowIfInvalid();

            return Convert.ToBoolean(lsl_was_clock_reset(handle));
        }

        /// <summary>
        /// Overrides the half-time (forget factor) of the timestamp smoothing.
        /// </summary>
        /// <param name="halfTime">
        /// Specifies the half-time of the timestamp smoothing, in seconds. This
        /// is the time after which a past sample will be weighted by 1/2 in the
        /// exponential smoothing window. 
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream inlet object is invalid.
        /// </exception>
        /// <remarks>
        /// The default is 90 seconds unless a different value is set in the config
        /// file. Using a longer window will yield lower jitter in the timestamps,
        /// but longer windows will have trouble tracking changes in the clock rate
        /// (usually due to temperature changes). The default is able to track changes
        /// of up to 10 degrees C per minute sufficiently well.
        /// </remarks>
        public void SetSmoothingHalfTime(float halfTime)
        {
            ThrowIfInvalid();

            CheckError(lsl_smoothing_halftime(handle, halfTime));
        }

        /// <summary>
        /// Destroys the underlying native stream inlet handle.
        /// </summary>
        /// <remarks>
        /// The inlet will automatically disconnect if destroyed.
        /// </remarks>
        protected override void DestroyLSLObject()
        {
            lsl_destroy_inlet(handle);
        }

        private int sampleBytes_;
    }
}


// References:
// [Array versus List<T>: When to use which?](https://stackoverflow.com/questions/434761/array-versus-listt-when-to-use-which)
// https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.unsafe.copyblock?view=net-8.0
// [How to Convert IntPtr to native c++ object](https://stackoverflow.com/questions/4277681/how-to-convert-intptr-to-native-c-object)
// [Documenting overloaded methods with the same XML comments](https://stackoverflow.com/questions/3667784/documenting-overloaded-methods-with-the-same-xml-comments)
// [Alternative to Buffer.MemoryCopy pre .NET 4.6](https://stackoverflow.com/questions/54453119/alternative-to-buffer-memorycopy-pre-net-4-6)
