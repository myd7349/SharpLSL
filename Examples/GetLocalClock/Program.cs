using System.Diagnostics;

namespace SharpLSL.Examples
{
    internal static class GetLocalClock
    {
        static void Main()
        {
            for (int i = 0; i < 10; ++i)
            {
                var stopwatch = Stopwatch.StartNew();
                var timestamp = LSL.GetLocalClock();
                var elapsedTime = stopwatch.Elapsed;

                stopwatch = Stopwatch.StartNew();
                var timestamp2 = Stopwatch.GetTimestamp();
                var elapsedTime2 = stopwatch.Elapsed;

                Console.WriteLine($"{timestamp:F7} {elapsedTime} <- LSL.GetLocalClock()");
                Console.WriteLine($"{timestamp2 / (double)TimeSpan.TicksPerSecond:F7} {elapsedTime2} <- Stopwatch.GetTimestamp()");
            }

            Console.WriteLine($"Is high resolution? {Stopwatch.IsHighResolution}.");
        }
    }
}


// References:
// [In C# on Linux .NET Core 3.1+, is Stopwatch.GetTimestamp() monotonic?](https://stackoverflow.com/questions/64878510/in-c-sharp-on-linux-net-core-3-1-is-stopwatch-gettimestamp-monotonic)
// [Monotonic timestamps in C#](https://antonymale.co.uk/monotonic-timestamps-in-csharp.html)
// [How do i get from Stopwatch.GetTimestamp() to a DateTime?](https://stackoverflow.com/questions/1438652/how-do-i-get-from-stopwatch-gettimestamp-to-a-datetime)
// [Add a monotonic clock to the framework #15207](https://github.com/dotnet/runtime/issues/15207)
