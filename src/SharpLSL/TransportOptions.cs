using SharpLSL.Interop;

namespace SharpLSL
{
    /// <summary>
    /// Specifies the transport options for stream inlets and outlets.
    /// </summary>
    /// <seealso cref="StreamInlet(StreamInfo, int, int, bool, TransportOptions)"/>
    /// <seealso cref="StreamOutlet(StreamInfo, int, int, TransportOptions)"/>
    public enum TransportOptions
    {
        /// <summary>
        /// Default behavior: buffer size is interpreted as time in seconds, and
        /// asynchronous transfer is used.
        /// </summary>
        /// <remarks>
        /// When using this option, the <c>maxBufferLength</c>/<c>maxBuffered</c>
        /// parameter in inlet/outlet creation methods represents the maximum buffer
        /// duration in seconds.
        /// </remarks>
        Default = lsl_transport_options_t.transp_default,

        /// <summary>
        /// Specifies that the buffer size is measured in samples rather than time.
        /// </summary>
        /// <remarks>
        /// When using this option, the <c>maxBufferLength</c>/<c>maxBuffered</c>
        /// parameter in inlet/outlet creation methods represents the maximum number
        /// of samples to buffer.
        /// This option is mutually exclusive with <see cref="BufferSizeThousandths"/>.
        /// </remarks>
        BufferSizeInSamples = lsl_transport_options_t.transp_bufsize_samples,

        /// <summary>
        /// Specifies that the supplied buffer size should be scaled by 0.001.
        /// </summary>
        /// <remarks>
        /// This option is mutually exclusive with <see cref="BufferSizeInSamples"/>.
        /// </remarks>
        BufferSizeThousandths = lsl_transport_options_t.transp_bufsize_thousandths,
    }
}
