using System;

namespace SharpLSL
{
    /// <summary>
    /// Represents errors that occur within the LSL (Lab Streaming Layer) library.
    /// </summary>
    /// <remarks>
    /// This exception is used to encapsulate LSL-specific errors, such as problems
    /// with data streams or communication errors. It extends the base <see cref="Exception"/>
    /// class to provide additional context for LSL-related issues.
    /// </remarks>
    public class LSLException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LSLException"/> class.
        /// </summary>
        public LSLException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LSLException"/> class with
        /// a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public LSLException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LSLException"/> class with
        /// a specified error message and a reference to the inner exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null
        /// reference if no inner exception is specified.
        /// </param>
        public LSLException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
