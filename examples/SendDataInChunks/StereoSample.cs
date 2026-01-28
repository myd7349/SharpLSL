using System.Runtime.InteropServices;

namespace SharpLSL.Examples
{
    // Define a packed sample struct (here: a 16 bit stereo sample)
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StereoSample
    {
        public short Left;

        public short Right;
    }
}
