using System;

namespace SharpLSL
{
    public class LSLException : Exception
    {
        public LSLException()
        {
        }

        public LSLException(string message)
            : base(message)
        {
        }

        public LSLException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
