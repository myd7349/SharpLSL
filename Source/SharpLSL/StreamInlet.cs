using SharpLSL.Interop;
using System;
using static SharpLSL.Interop.LSL;
using static SharpLSL.LSL;

namespace SharpLSL
{
    /// <summary>
    /// Represents a stream inlet object for receiving streaming data (and meta-data)
    /// from the lab network.
    /// </summary>
    public class StreamInlet : LSLObject
    {
        /// <summary>
        /// Constructs a new stream inlet from a resolved stream info.
        /// </summary>
        /// <param name="streamInfo">
        /// <para>
        /// A resolved <see cref="StreamInfo"/> object (as coming from one of the
        /// resolver functions).
        /// </para>
        /// <para>
        /// The inlet will makes a copy of the <paramref name="streamInfo"/> object
        /// at its construction.
        /// </para>
        /// <para>
        /// The <see cref="StreamInlet"/> may also be constructed with a fully specified
        /// <see cref="StreamInfo"/>, if the desired channel format and count is already
        /// known up-front, but this is strongly discouraged and should only ever be done
        /// if there is no time to resolve the stream up-front (e.g., due to limitations
        /// in the client program).
        /// </para>
        /// </param>
        /// <param name="maxBufferLength">
        /// <para>
        /// Specifies the maximum amount of the data to buffer (in seconds if there
        /// is a nominal sampling rate, otherwise x100 in samples).
        /// </para>
        /// <para>
        /// Recording applications want to use a fairly large buffer size here, while
        /// real-time applications would only buffer as much as they need to perform
        /// their next calculation. A good default is 360, which corresponds to 6
        /// minutes of data.
        /// </para>
        /// </param>
        /// <param name="maxChunkLength">
        /// Specifies the maximum size, in samples, at which chunks are transmitted.
        /// If set as 0, the chunk sizes preferred by the sender are used. Recording
        /// applications can use a generous size here (leaving it to the network how
        /// to pack things), while real time applications may want a finer (perhaps
        /// 1-sample) granularity.
        /// </param>
        /// <param name="recover">
        /// Specifies whether try to silently recover lost streams that are recoverable
        /// (those that have a source id set). In all other cases (recover is false or
        /// the stream is not recoverable) functions may throw a <see cref="StreamLostException"/>
        /// if the stream's source is lost (e.g., due to an app or computer crash).
        /// </param>
        /// <param name="transportOptions">
        /// Specifies the transport options.
        /// </param>
        /// <exception cref="LSLException">
        /// Thrown when creating a new instance of <see cref="StreamInlet"/> fails.
        /// </exception>
        public StreamInlet(
            StreamInfo streamInfo, int maxBufferLength = 360, int maxChunkLength = 0,
            bool recover = true, TransportOptions transportOptions = TransportOptions.Default)
            : base(lsl_create_inlet_ex(streamInfo.DangerousGetHandle(), maxBufferLength, maxChunkLength, recover ? 1 : 0, (lsl_transport_options_t)transportOptions))
        {
        }

        /// <summary>
        /// Constructs a new instance of <see cref="StreamInlet"/> object which wraps
        /// a pre-existing stream inlet handle.
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
        public StreamInlet(IntPtr handle, bool ownsHandle = true)
            : base(handle, ownsHandle)
        {
        }

        /// <summary>
        /// Retrieves the complete information of the given stream, including the
        /// extended description.
        /// </summary>
        /// <param name="timeout">
        /// Specifies the timeout of the operation in seconds. Default value is
        /// <see cref="Forever"/> which indicates no timeout.
        /// </param>
        /// <returns>
        /// An instance of <see cref="StreamInfo"/> containing detailed information
        /// about the stream, including its extended description.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <exception cref="StreamLostException">
        /// Thrown if the stream source has been lost.
        /// </exception>
        /// <exception cref="TimeoutException">
        /// Thrown if the timeout has expired.
        /// </exception>
        /// <remarks>
        /// This method can be invoked at any time of the stream's lifetime.
        /// </remarks>
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
        /// Specifies the timeout of the operation in seconds. Default value is
        /// <see cref="Forever"/> which indicates no timeout.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
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
        /// PullChunk() calls. Pulling a sample without some preceding OpenStream()
        /// is permitted (the stream will then be opened implicitly).
        /// </remarks>
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
        /// Thrown if this object is invalid.
        /// </exception>
        /// <remarks>
        /// All samples that are still buffered or in flight will be dropped and
        /// transmission and buffering of data for this inlet will be stopped. If
        /// an application stops being interested in data from a source (temporarily
        /// or not) but keeps the outlet alive, it should call CloseStream() to not
        /// waste unnecessary system and network resources.
        /// </remarks>
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
        /// in seconds. Default value is <see cref="Forever"/> which indicates
        /// no timeout.
        /// </param>
        /// <returns>
        /// The time correction estimate. This is the number that needs to be added
        /// to a timestamp that was remotely generated via <see cref="GetLocalClock"/>
        /// to map it into the local clock domain of this machine.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
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
        /// on periodic background updates). On a well-behaved network, the precision
        /// of these estimates should be below 1 ms(empirically it is within +/-0.2 ms).
        /// </para>
        /// <para>
        /// To get a measure of whether the network is well-behaved, use the extended
        /// version <see cref="TimeCorrection(ref double, ref double, double)"/> and
        /// check uncertainty (i.e. the round-trip-time).
        /// </para>
        /// <para>
        /// 0.2 ms is typical of wired networks. 2 ms is typical of wireless networks.
        /// The number can be much higher on poor networks.
        /// </para>
        /// </remarks>
        public double TimeCorrection(double timeout = Forever)
        {
            ThrowIfInvalid();
            
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_time_correction(handle, timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        public double TimeCorrection(ref double remoteTime, ref double uncertainty, double timeout = Forever)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_time_correction_ex(handle, ref remoteTime, ref uncertainty, timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        public void SetPostProcessingOptions(PostProcessingOptions postProcessingOptions)
        {
            CheckError(lsl_set_postprocessing(handle, (uint)postProcessingOptions));
        }

        public double PullSample(short[] sampleData, double timeout)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var timestamp = lsl_pull_sample_s(handle, sampleData, sampleData.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return timestamp;
        }

        public double PullSample(int[] sampleData, double timeout)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var timestamp = lsl_pull_sample_i(handle, sampleData, sampleData.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return timestamp;
        }

        public double PullSample(long[] sampleData, double timeout)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var timestamp = lsl_pull_sample_l(handle, sampleData, sampleData.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return timestamp;
        }

        public double PullSample(float[] sampleData, double timeout)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var timestamp = lsl_pull_sample_f(handle, sampleData, sampleData.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return timestamp;
        }

        public double PullSample(double[] sampleData, double timeout)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var timestamp = lsl_pull_sample_d(handle, sampleData, sampleData.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return timestamp;
        }

        public uint PullChunk(short[] chunkData, double[] timestamps, double timeout)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_s(handle, chunkData, timestamps, (uint)chunkData.Length, (uint)timestamps.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        public uint PullChunk(int[] chunkData, double[] timestamps, double timeout)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_i(handle, chunkData, timestamps, (uint)chunkData.Length, (uint)timestamps.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        public uint PullChunk(long[] chunkData, double[] timestamps, double timeout)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_l(handle, chunkData, timestamps, (uint)chunkData.Length, (uint)timestamps.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        public uint PullChunk(float[] chunkData, double[] timestamps, double timeout)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_f(handle, chunkData, timestamps, (uint)chunkData.Length, (uint)timestamps.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        public uint PullChunk(double[] chunkData, double[] timestamps, double timeout)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_d(handle, chunkData, timestamps, (uint)chunkData.Length, (uint)timestamps.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        public uint PullChunk(short[,] chunkData, double[] timestamps, double timeout)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_s(handle, chunkData, timestamps, (uint)chunkData.Length, (uint)timestamps.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        public uint PullChunk(int[,] chunkData, double[] timestamps, double timeout)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_i(handle, chunkData, timestamps, (uint)chunkData.Length, (uint)timestamps.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        public uint PullChunk(long[,] chunkData, double[] timestamps, double timeout)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_l(handle, chunkData, timestamps, (uint)chunkData.Length, (uint)timestamps.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        public uint PullChunk(float[,] chunkData, double[] timestamps, double timeout)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_f(handle, chunkData, timestamps, (uint)chunkData.Length, (uint)timestamps.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        public uint PullChunk(double[,] chunkData, double[] timestamps, double timeout)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            var result = lsl_pull_chunk_d(handle, chunkData, timestamps, (uint)chunkData.Length, (uint)timestamps.Length, timeout, ref errorCode);
            CheckError(errorCode);
            return result;
        }

        public bool SamplesAvailable() => lsl_samples_available(handle) > 0;

        public uint Flush() => lsl_inlet_flush(handle);

        public uint WasClockReset() => lsl_was_clock_reset(handle);

        public void SetSmoothingHalfTime(float value)
        {
            CheckError(lsl_smoothing_halftime(handle, value));
        }

        /// <summary>
        /// Disconnects the inlet and destroys the underlying native resource.
        /// </summary>
        protected override void DestroyLSLObject()
        {
            lsl_destroy_inlet(handle);
        }
    }
}
