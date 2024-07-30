using System;

using SharpLSL.Interop;

namespace SharpLSL
{
    /// <summary>
    /// Specifies the transport options for stream inlets and outlets.
    /// </summary>
    [Flags]
    public enum TransportOptions
    {
        // TODO: argument name & see also
        /// <summary>
        /// Default behavior: `max_buffered` / `max_buflen` is interpreted as time in seconds, and asynchronous transfer is used.
        /// </summary>
        Default = lsl_transport_options_t.transp_default,

        // TODO: argument name & see also
        /// <summary>
        /// Specifies the supplied `max_buf` value is in samples.
        /// </summary>
        BufferSizeInSamples = lsl_transport_options_t.transp_bufsize_samples,

        // TODO: argument name & see also
        /// <summary>
        /// Specifies the supplied `max_buf` value should be scaled by 0.001.
        /// </summary>
        BufferSizeThousandths = lsl_transport_options_t.transp_bufsize_thousandths,
    }
}
