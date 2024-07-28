// Port of https://github.com/sccn/liblsl/blob/master/testing/lslver.c
using SharpLSL;
using SharpLSL.Interop;

namespace LSLVer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"LSL version: {LSL.lsl_library_version()}");
            Console.WriteLine(Lsl.GetLibraryInfo());
            Console.WriteLine(Lsl.GetLocalClock());
        }
    }
}