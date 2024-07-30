using System;
using System.Runtime.CompilerServices;

using SharpLSL.Interop;

using static SharpLSL.Interop.LSL;
using static SharpLSL.LSL;

namespace SharpLSL
{
    public class StreamOutlet : LSLObject
    {
        public StreamOutlet(StreamInfo streamInfo, int chunkSize = 0, int maxBuffered = 360, TransportOptions transportOptions = TransportOptions.Default)
            : base(lsl_create_outlet_ex(streamInfo.DangerousGetHandle(), chunkSize, maxBuffered, (lsl_transport_options_t)transportOptions))
        {
            channelCount_ = streamInfo.ChannelCount;
        }

        public StreamOutlet(IntPtr handle, bool ownsHandle = true)
            : base(handle, ownsHandle)
        {
        }

        public void PushSample(short[] sampleData)
        {
            CheckSampleData(sampleData);
            CheckError(lsl_push_sample_s(handle, sampleData));
        }

        public void PushSample(int[] sampleData)
        {
            CheckSampleData(sampleData);
            CheckError(lsl_push_sample_i(handle, sampleData));
        }

        public void PushSample(long[] sampleData)
        {
            CheckSampleData(sampleData);
            CheckError(lsl_push_sample_l(handle, sampleData));
        }

        public void PushSample(float[] sampleData)
        {
            CheckSampleData(sampleData);
            CheckError(lsl_push_sample_f(handle, sampleData));
        }

        public void PushSample(double[] sampleData)
        {
            CheckSampleData(sampleData);
            CheckError(lsl_push_sample_d(handle, sampleData));
        }

        public void PushSample(short[] sampleData, double timestamp)
        {
            CheckSampleData(sampleData);
            CheckError(lsl_push_sample_st(handle, sampleData, timestamp));
        }

        public void PushSample(int[] sampleData, double timestamp)
        {
            CheckSampleData(sampleData);
            CheckError(lsl_push_sample_it(handle, sampleData, timestamp));
        }

        public void PushSample(long[] sampleData, double timestamp)
        {
            CheckSampleData(sampleData);
            CheckError(lsl_push_sample_lt(handle, sampleData, timestamp));
        }

        public void PushSample(float[] sampleData, double timestamp)
        {
            CheckSampleData(sampleData);
            CheckError(lsl_push_sample_ft(handle, sampleData, timestamp));
        }

        public void PushSample(double[] sampleData, double timestamp)
        {
            CheckSampleData(sampleData);
            CheckError(lsl_push_sample_dt(handle, sampleData, timestamp));
        }

        public void PushSample(short[] sampleData, double timestamp, bool pushThrough)
        {
            CheckSampleData(sampleData);
            CheckError(lsl_push_sample_stp(handle, sampleData, timestamp, pushThrough ? 1 : 0));
        }

        public void PushSample(int[] sampleData, double timestamp, bool pushThrough)
        {
            CheckSampleData(sampleData);
            CheckError(lsl_push_sample_itp(handle, sampleData, timestamp, pushThrough ? 1 : 0));
        }

        public void PushSample(long[] sampleData, double timestamp, bool pushThrough)
        {
            CheckSampleData(sampleData);
            CheckError(lsl_push_sample_ltp(handle, sampleData, timestamp, pushThrough ? 1 : 0));
        }

        public void PushSample(float[] sampleData, double timestamp, bool pushThrough)
        {
            CheckSampleData(sampleData);
            CheckError(lsl_push_sample_ftp(handle, sampleData, timestamp, pushThrough ? 1 : 0));
        }

        public void PushSample(double[] sampleData, double timestamp, bool pushThrough)
        {
            CheckSampleData(sampleData);
            CheckError(lsl_push_sample_dtp(handle, sampleData, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(short[] data)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_s(handle, data, (uint)data.Length));
        }

        public void PushChunk(int[] data)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_i(handle, data, (uint)data.Length));
        }

        public void PushChunk(long[] data)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_l(handle, data, (uint)data.Length));
        }

        public void PushChunk(float[] data)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_f(handle, data, (uint)data.Length));
        }

        public void PushChunk(double[] data)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_d(handle, data, (uint)data.Length));
        }

        public void PushChunk(short[,] data)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_s(handle, data, (uint)data.Length));
        }

        public void PushChunk(int[,] data)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_i(handle, data, (uint)data.Length));
        }

        public void PushChunk(long[,] data)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_l(handle, data, (uint)data.Length));
        }

        public void PushChunk(float[,] data)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_f(handle, data, (uint)data.Length));
        }

        public void PushChunk(double[,] data)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_d(handle, data, (uint)data.Length));
        }

        public void PushChunk(short[] data, double timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_st(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(int[] data, double timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_it(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(long[] data, double timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_lt(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(float[] data, double timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_ft(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(double[] data, double timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_dt(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(short[,] data, double timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_st(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(int[,] data, double timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_it(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(long[,] data, double timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_lt(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(float[,] data, double timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_ft(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(double[,] data, double timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_dt(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(short[] data, double timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_stp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(int[] data, double timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_itp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(long[] data, double timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_ltp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(float[] data, double timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_ftp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(double[] data, double timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_dtp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(short[,] data, double timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_stp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(int[,] data, double timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_itp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(long[,] data, double timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_ltp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(float[,] data, double timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_ftp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(double[,] data, double timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_dtp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(short[] data, double[] timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_stn(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(int[] data, double[] timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_itn(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(long[] data, double[] timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_ltn(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(float[] data, double[] timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_ftn(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(double[] data, double[] timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_dtn(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(short[,] data, double[] timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_stn(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(int[,] data, double[] timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_itn(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(long[,] data, double[] timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_ltn(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(float[,] data, double[] timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_ftn(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(double[,] data, double[] timestamp)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_dtn(handle, data, (uint)data.Length, timestamp));
        }

        public void PushChunk(short[] data, double[] timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_stnp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(int[] data, double[] timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_itnp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(long[] data, double[] timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_ltnp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(float[] data, double[] timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_ftnp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(double[] data, double[] timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_dtnp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(short[,] data, double[] timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_stnp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(int[,] data, double[] timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_itnp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(long[,] data, double[] timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_ltnp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(float[,] data, double[] timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_ftnp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public void PushChunk(double[,] data, double[] timestamp, bool pushThrough)
        {
            CheckChunkData(data);
            CheckError(lsl_push_chunk_dtnp(handle, data, (uint)data.Length, timestamp, pushThrough ? 1 : 0));
        }

        public bool HaveConsumers() => lsl_have_consumers(handle) > 0;

        public bool WaitForConsumers(double timeout) => lsl_wait_for_consumers(handle, timeout) > 0;

        protected override void DestroyLSLObject()
        {
            lsl_destroy_outlet(handle);
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void CheckSampleData<T>(T[] sampleData)
        {
            if (sampleData == null)
                throw new ArgumentNullException(nameof(sampleData));

            CheckChannelCount(sampleData.Length);
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void CheckChunkData<T>(T[] chunkData)
        {
            if (chunkData == null)
                throw new ArgumentNullException(nameof(chunkData));

            if (chunkData.Length == 0 || chunkData.Length % channelCount_ != 0)
                throw new ArgumentException(nameof(chunkData));
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void CheckChunkData<T>(T[,] chunkData)
        {
            if (chunkData == null)
                throw new ArgumentNullException(nameof(chunkData));

            if (chunkData.GetLength(0) == 0 || chunkData.GetLength(1) != channelCount_)
                throw new ArgumentException(nameof(chunkData));
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void CheckChannelCount(int channelCount)
        {
            if (channelCount < channelCount_)
                throw new ArgumentException($"Provided buffer's channel count ({channelCount}) doesn't match the stream's channel count ({channelCount_}).");
        }

        private readonly int channelCount_;
    }
}
