using SharpLSL.Interop;

namespace SharpLSL
{
    public enum LslChannelFormat
    {
        Float = lsl_channel_format_t.cft_float32,
        Double = lsl_channel_format_t.cft_double64,
        String = lsl_channel_format_t.cft_string,
        Int32 = lsl_channel_format_t.cft_int32,
        Int16 = lsl_channel_format_t.cft_int16,
        Int8 = lsl_channel_format_t.cft_int8,
        Int64 = lsl_channel_format_t.cft_int64,
        Undefined = lsl_channel_format_t.cft_undefined,
    }
}
