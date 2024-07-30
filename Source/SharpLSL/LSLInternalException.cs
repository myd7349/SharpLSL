using System;

namespace SharpLSL
{
    /// <summary>
    /// Represents an internal error within the LSL (Lab Streaming Layer) framework.
    /// </summary>
    /// <remarks>
    /// This exception is used to indicate errors that occur within the internal of LSL.
    /// This class extends <see cref="LSLException"/> to provide a more specific exception
    /// type for internal LSL errors.
    /// </remarks>
    public class LSLInternalException : LSLException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LSLInternalException"/> class.
        /// </summary>
        public LSLInternalException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LSLInternalException"/> class with
        /// a specified error message.
        /// </summary>
        /// <param name="message">
        /// The error message that describes the reason for the exception.
        /// </param>
        public LSLInternalException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LSLInternalException"/> class with
        /// a specified error message and a reference to the inner exception.
        /// </summary>
        /// <param name="message">
        /// The error message that describes the reason for the exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null
        /// reference if no inner exception is specified.
        /// </param>
        public LSLInternalException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
