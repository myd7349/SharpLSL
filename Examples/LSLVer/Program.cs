// Port of https://github.com/sccn/liblsl/blob/master/testing/lslver.c
using SharpLSL;

namespace LSLVer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"LSL version: {LSL.GetLibraryVersion()}");
            Console.WriteLine(LSL.GetLibraryInfo());
            Console.WriteLine(LSL.GetLocalClock());
        }
    }
}