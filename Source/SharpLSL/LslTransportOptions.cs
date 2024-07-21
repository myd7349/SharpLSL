using SharpLSL.Interop;

namespace SharpLSL
{
    public enum LslTransportOptions
    {
        Default = lsl_transport_options_t.transp_default,
        BufferSizeSamples = lsl_transport_options_t.transp_bufsize_samples,
        BufferSizeThousandths = lsl_transport_options_t.transp_bufsize_thousandths,
    }
}
