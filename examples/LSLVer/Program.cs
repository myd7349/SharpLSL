// Port of https://github.com/sccn/liblsl/blob/master/testing/lslver.c
namespace SharpLSL.Examples
{
    internal class LSLVer
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"LSL version: {LSL.GetLibraryVersion()}");
            Console.WriteLine(LSL.GetLibraryInfo());
            Console.WriteLine(LSL.GetLocalClock());
        }
    }
}
