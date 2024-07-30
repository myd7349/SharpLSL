using System;

using static SharpLSL.Interop.LSL;
using static SharpLSL.LSL;

namespace SharpLSL
{
    public class ContinuousResolver : LSLObject
    {
        public ContinuousResolver(double forgetAfter = 5.0)
            : base(lsl_create_continuous_resolver(forgetAfter))
        {
        }

        public ContinuousResolver(string property, string value, double forgetAfter = 5.0)
            : base(lsl_create_continuous_resolver_byprop(property, value, forgetAfter))
        {
        }

        public ContinuousResolver(string predicate, double forgetAfter = 5.0)
            : base(lsl_create_continuous_resolver_bypred(predicate, forgetAfter))
        {
        }

        public ContinuousResolver(IntPtr handle, bool ownsHandle = true)
            : base(handle, ownsHandle)
        {
        }

        public StreamInfo[] Resolve(int maxCount = 1024)
        {
            var streamInfoPointers = new IntPtr[maxCount];

            var result = lsl_resolver_results(handle, streamInfoPointers, (uint)streamInfoPointers.Length);
            CheckError(result);

            var streamInfos = new StreamInfo[result];
            for (int i = 0; i < result; ++i)
                streamInfos[i] = new StreamInfo(streamInfoPointers[i], true);

            return streamInfos;
        }

        public StreamInfo[] ResolveAll(int maxCount = 1024, double waitTime = 1.0)
        {
            var streamInfoPointers = new IntPtr[maxCount];

            var result = lsl_resolve_all(streamInfoPointers, (uint)streamInfoPointers.Length, waitTime);
            CheckError(result);

            var streamInfos = new StreamInfo[result];
            for (int i = 0; i < result; ++i)
                streamInfos[i] = new StreamInfo(streamInfoPointers[i], true);

            return streamInfos;
        }

        public StreamInfo[] Resolve(string property, string value, int minCount, int maxCount = 1024, double timeout = Forever)
        {
            var streamInfoPointers = new IntPtr[maxCount];

            var result = lsl_resolve_byprop(streamInfoPointers, (uint)streamInfoPointers.Length, property, value, minCount, timeout);
            CheckError(result);

            var streamInfos = new StreamInfo[result];
            for (int i = 0; i < result; ++i)
                streamInfos[i] = new StreamInfo(streamInfoPointers[i], true);

            return streamInfos;
        }

        public StreamInfo[] Resolve(string predicate, int minCount, int maxCount = 1024, double timeout = Forever)
        {
            var streamInfoPointers = new IntPtr[maxCount];

            var result = lsl_resolve_bypred(streamInfoPointers, (uint)streamInfoPointers.Length, predicate, minCount, timeout);
            CheckError(result);

            var streamInfos = new StreamInfo[result];
            for (int i = 0; i < result; ++i)
                streamInfos[i] = new StreamInfo(streamInfoPointers[i], true);

            return streamInfos;
        }

        protected override void DestroyLSLObject()
        {
            lsl_destroy_continuous_resolver(handle);
        }
    }
}
