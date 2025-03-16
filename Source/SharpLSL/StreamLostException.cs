using System;

namespace SharpLSL
{
    /// <summary>
    /// Represents an error that occurs when an LSL stream is unexpectedly lost or
    /// disconnected.
    /// </summary>
    /// <remarks>
    /// This exception is thrown when an application attempts to access an LSL stream
    /// that is no longer available. It indicates that the connection to the stream
    /// has been lost, which may be due to network issues, stream termination, or other
    /// unforeseen circumstances. This exception extends the base <see cref="LSLException"/>
    /// class to provide specific information about stream loss errors.
    /// </remarks>
    /// <seealso cref="StreamInlet(StreamInfo, int, int, bool, TransportOptions)"/>
    public class StreamLostException : LSLException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StreamLostException"/> class.
        /// </summary>
        public StreamLostException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamLostException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public StreamLostException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamLostException"/> class
        /// with a specified error message and a reference to the inner exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null
        /// reference if no inner exception is specified.
        /// </param>
        public StreamLostException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
