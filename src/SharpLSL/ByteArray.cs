using System;

namespace SharpLSL
{
    /// <summary>
    /// 
    /// </summary>
    public struct ByteArray
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="length"></param>
        public ByteArray(byte[] buffer, int length)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            if (length > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(length));

            _buffer = buffer;
            _length = length;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Length => _length;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public ByteArray Resize(int length)
        {
            return default;
        }

        private readonly byte[] _buffer;
        private int _length;
    }
}
