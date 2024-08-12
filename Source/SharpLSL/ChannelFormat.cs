using SharpLSL.Interop;

namespace SharpLSL
{
    /// <summary>
    /// Data format of a channel.
    /// </summary>
    /// <remarks>
    /// This enumeration specifies the format of data transmitted for each channel.
    /// Each transmitted sample contains an array of channels, and this enum describes
    /// the format of the data sent over the wire.
    /// </remarks>
    /// <seealso cref="StreamInfo.ChannelFormat"/>
    public enum ChannelFormat : int
    {
        /// <summary>
        /// Represents a 32-bit floating-point format.
        /// </summary>
        /// <remarks>
        /// This format is used for measurements that require up to 24-bit precision,
        /// such as physical quantities measured in microvolts. Integers within the
        /// range of -16,777,216 to 16,777,216 are represented accurately using this
        /// format.
        /// </remarks>
        Float = lsl_channel_format_t.cft_float32,

        /// <summary>
        /// Represents a 64-bit double-precision floating-point format.
        /// </summary>
        /// <remarks>
        /// This format is used for representing numerical data with high precision
        /// for universal numeric data as long as permitted by network and disk budget.
        /// The largest representable integer is 53-bit.
        /// </remarks>
        Double = lsl_channel_format_t.cft_double64,

        /// <summary>
        /// Represents variable-length ASCII strings or data blobs.
        /// </summary>
        /// <remarks>
        /// This format is suitable for data that cannot be easily represented as
        /// numeric values, such as video frames, or complex event descriptions.
        /// </remarks>
        String = lsl_channel_format_t.cft_string,

        /// <summary>
        /// Represents a 32-bit signed integer format.
        /// </summary>
        /// <remarks>
        /// This format is used for transmitting data that requires 32-bit integer
        /// precision. It is suitable for high-rate digitized formats and cases
        /// where the data is represented as discrete numeric values, such as
        /// application event codes or other coded data. 
        /// </remarks>
        Int32 = lsl_channel_format_t.cft_int32,

        /// <summary>
        /// Represents a 16-bit signed integer format.
        /// </summary>
        /// <remarks>
        /// This format is used for transmitting data with 16-bit integer precision. 
        /// It is ideal for very high-rate signals (40kHz+) or consumer-grade audio.
        /// For professional audio, <see cref="Float"/> is recommended.
        /// </remarks>
        Int16 = lsl_channel_format_t.cft_int16,

        /// <summary>
        /// Represents an 8-bit signed integer format.
        /// </summary>
        /// <remarks>
        /// This format is used for transmitting data with 8-bit integer precision. 
        /// It is suitable for binary signals or other coded data. It is not
        /// recommended for encoding string data.
        /// </remarks>
        Int8 = lsl_channel_format_t.cft_int8,

        /// <summary>
        /// Represents a 64-bit signed integer format.
        /// </summary>
        /// <remarks>
        /// This format is used for transmitting data that requires 64-bit integer
        /// precision. Note that support for `Int64` is not yet exposed in all languages.
        /// Also, some builds of liblsl will not be able to send or receive data of this type.
        /// </remarks>
        Int64 = lsl_channel_format_t.cft_int64,

        /// <summary>
        /// Represents an undefined or unsupported data format.
        /// </summary>
        /// <remarks>
        /// This format indicates that the data format is either not defined or
        /// not supported for transmission.
        /// </remarks>
        Undefined = lsl_channel_format_t.cft_undefined,
    }
}
