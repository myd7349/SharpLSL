using System;
using System.Runtime.CompilerServices;

using static SharpLSL.Interop.LSL;
using static SharpLSL.LSL;

namespace SharpLSL
{
    /// <summary>
    /// Represents a continuous resolver for resolving streams on the lab network.
    /// </summary>
    /// <remarks>
    /// Streams can be resolved at a single time point once or continuously in the
    /// background. The <see cref="ContinuousResolver"/> class provides functionality
    /// to resolve all streams continuously in the background throughout its lifetime
    /// and which can be queried at any time for the set of streams that are currently
    /// visible on the network.
    /// </remarks>
    public class ContinuousResolver : LSLObject
    {
        /// <summary>
        /// Constructs a new continuous resolver that resolves all streams on the
        /// network.
        /// </summary>
        /// <param name="forgetAfter">
        /// The time in seconds after which a stream that is no longer visible on the
        /// network (e.g., due to shutdown) will not be reported by the resolver. The
        /// recommended default value is 5.0 seconds.
        /// </param>
        /// <exception cref="LSLException">
        /// Thrown when creating a new instance of <see cref="ContinuousResolver"/> fails.
        /// </exception>
        /// <remarks>
        /// This is analogous to the functionality offered by the free function
        /// <see cref="LSL.Resolve(int, double)"/>.
        /// </remarks>
        /// <seealso cref="LSL.Resolve(int, double)"/>
        public ContinuousResolver(double forgetAfter = 5.0)
            : base(CreateContinuousResolver(forgetAfter))
        {
        }

        /// <summary>
        /// Constructs a new continuous resolver that resolves all streams on the
        /// network with a specific value for a given property.
        /// </summary>
        /// <param name="property">
        /// The stream info property that should have a specific value (e.g., "name",
        /// "type", "source_id", or "desc/manufacturer" if present).
        /// </param>
        /// <param name="value">
        /// The string value that the property should have (e.g., "EEG" as the "type"
        /// property).
        /// </param>
        /// <param name="forgetAfter">
        /// The time in seconds after which a stream that is no longer visible on the
        /// network (e.g., due to shutdown) will not be reported by the resolver. The
        /// recommended default value is 5.0 seconds.
        /// </param>
        /// <remarks>
        /// This is analogous to the functionality provided by the free function
        /// <see cref="LSL.Resolve(string, string, int, int, double)"/>.
        /// </remarks>
        /// <inheritdoc cref="ContinuousResolver(double)"/>
        /// <seealso cref="Resolve(string, string, int, int, double)"/>
        public ContinuousResolver(string property, string value, double forgetAfter = 5.0)
            : base(CreateContinuousResolver(property, value, forgetAfter))
        {
        }

        /// <summary>
        /// Constructs a new continuous resolver that resolves all streams on the
        /// network that match a given XPath 1.0 predicate.
        /// </summary>
        /// <param name="predicate">
        /// The XPath 1.0 predicate used to filter streams. For example,
        /// `"name='BioSemi'" or "type='EEG' and starts-with(name,'BioSemi') and count(info/desc/channel)=32"`.
        /// </param>
        /// <param name="forgetAfter">
        /// The time in seconds after which a stream that is no longer visible on the
        /// network (e.g., due to shutdown) will not be reported by the resolver. The
        /// recommended default value is 5.0 seconds.
        /// </param>
        /// <remarks>
        /// This is analogous to the functionality provided by the free function
        /// <see cref="LSL.Resolve(string, int, int, double)"/>.
        /// </remarks>
        /// <inheritdoc cref="ContinuousResolver(double)"/>
        /// <seealso cref="Resolve(string, int, int, double)"/>
        public ContinuousResolver(string predicate, double forgetAfter = 5.0)
            : base(CreateContinuousResolver(predicate, forgetAfter))
        {
        }

        /// <summary>
        /// Constructs a new continuous resolver which wraps a pre-existing continuous
        /// resolver handle.
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
        public ContinuousResolver(IntPtr handle, bool ownsHandle = true)
            : base(handle, ownsHandle)
        {
        }

        /// <summary>
        /// Retrieves a list of the currently available streams on the network.
        /// </summary>
        /// <param name="maxCount">
        /// The maximum number of streams to retrieve.
        /// </param>
        /// <returns>
        /// An array of <see cref="StreamInfo"/> objects representing the currently
        /// available streams. The array length may be less than or equal to
        /// <paramref name="maxCount"/> depending on the number of streams detected.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this object is invalid.
        /// </exception>
        /// <remarks>
        /// The stream infos returned by the resolver are only short versions that do
        /// not include the <see cref="StreamInfo.Description"/> field (which can be
        /// arbitrarily big). To obtain the full stream information you need to call
        /// <see cref="StreamInlet.GetStreamInfo(double)"/> on the inlet after you
        /// have created one.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this continuous resolver object is invalid.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="maxCount"/> is less than or equal to 0.
        /// </exception>
        /// <exception cref="LSLInternalException">
        /// Thrown when an internal LSL error occurs.
        /// </exception>
        public StreamInfo[] Results(int maxCount = 1024)
        {
            ThrowIfInvalid();

            if (maxCount <= 0)
                throw new ArgumentException(nameof(maxCount));

            var streamInfoPointers = new IntPtr[maxCount];

            var result = lsl_resolver_results(handle, streamInfoPointers, (uint)streamInfoPointers.Length);
            CheckError(result);

            if (result > 0)
            {
                var streamInfos = new StreamInfo[result];
                for (int i = 0; i < result; ++i)
                    streamInfos[i] = new StreamInfo(streamInfoPointers[i], true);

                return streamInfos;
            }
            else
            {
#if NET35 || NET45
                return new StreamInfo[0];
#else
                return Array.Empty<StreamInfo>();
#endif
            }
        }

        /// <summary>
        /// Destroys the native continuous resolver handle and associated resource.
        /// </summary>
        protected override void DestroyLSLObject()
        {
            lsl_destroy_continuous_resolver(handle);
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static IntPtr CreateContinuousResolver(double forgetAfter)
        {
            var resolver = lsl_create_continuous_resolver(forgetAfter);
            if (resolver == IntPtr.Zero)
                throw new LSLException("Failed to create a continuous resolver.");

            return resolver;
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static unsafe IntPtr CreateContinuousResolver(string property, string value, double forgetAfter)
        {
            var propertyBytes = StringToBytes(property);
            var valueBytes = StringToBytes(value);

            fixed (byte* propertyBuffer = propertyBytes)
            fixed (byte* valueBuffer = valueBytes)
            {
                return lsl_create_continuous_resolver_byprop(
                    (sbyte*)propertyBuffer,
                    (sbyte*)valueBuffer,
                    forgetAfter);
            }
        }

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static unsafe IntPtr CreateContinuousResolver(string predicate, double forgetAfter)
        {
            var predicateBytes = StringToBytes(predicate);

            fixed (byte* predicateBuffer = predicateBytes)
            {
                return lsl_create_continuous_resolver_bypred(
                    (sbyte*)predicateBuffer,
                    forgetAfter);
            }
        }
    }
}
