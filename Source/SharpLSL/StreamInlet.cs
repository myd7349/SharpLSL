using System;

using SharpLSL.Interop;

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
        /// 
        /// </summary>
        /// <param name="streamInfo"></param>
        /// <param name="maxBufferLength"></param>
        /// <param name="maxChunkLength"></param>
        /// <param name="recover"></param>
        /// <param name="transportOptions"></param>
        public StreamInlet(
            StreamInfo streamInfo, int maxBufferLength = 360, int maxChunkLength = 0,
            bool recover = true, TransportOptions transportOptions = TransportOptions.Default)
            : base(lsl_create_inlet_ex(streamInfo.DangerousGetHandle(), maxBufferLength, maxChunkLength, recover ? 1 : 0, (lsl_transport_options_t)transportOptions))
        {
        }

        public StreamInlet(IntPtr handle, bool ownsHandle = true)
            : base(handle, ownsHandle)
        {
        }

        public void OpenStream(double timeout)
        {
            var errorCode = (int)lsl_error_code_t.lsl_no_error;
            lsl_open_stream(handle, timeout, ref errorCode);
            CheckError(errorCode);
        }

        public void CloseStream() => lsl_close_stream(handle);

        public double TimeCorrection(double timeout = Forever)
        {
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

        protected override void DestroyLSLObject()
        {
            lsl_destroy_inlet(handle);
        }
    }
}
