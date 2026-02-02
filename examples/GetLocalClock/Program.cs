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
                Console.WriteLine($"{TimestampToSeconds(timestamp2):F7} {elapsedTime2} <- TimestampToSeconds(Stopwatch.GetTimestamp())");
            }

            Console.WriteLine($"Is high resolution? {Stopwatch.IsHighResolution}.");
            Console.WriteLine($"Ticks per second: {TimeSpan.TicksPerSecond}.");
            Console.WriteLine($"Frequency: {Stopwatch.Frequency}.");
            Console.WriteLine($"Tick frequency: {(double)TimeSpan.TicksPerSecond / Stopwatch.Frequency}.");
        }

        static double TimestampToSeconds(long timestamp)
        {
#if false
            var tickFrequency = (double)TimeSpan.TicksPerSecond / Stopwatch.Frequency;
            return timestamp * tickFrequency / TimeSpan.TicksPerSecond;
#else
            return (double)timestamp / Stopwatch.Frequency;
#endif
        }

        static double TimestampToMilliseconds(long timestamp)
        {
            var tickFrequency = (double)TimeSpan.TicksPerSecond / Stopwatch.Frequency;
            return timestamp * tickFrequency / TimeSpan.TicksPerMillisecond;
        }
    }
}


// References:
// [In C# on Linux .NET Core 3.1+, is Stopwatch.GetTimestamp() monotonic?](https://stackoverflow.com/questions/64878510/in-c-sharp-on-linux-net-core-3-1-is-stopwatch-gettimestamp-monotonic)
// [Monotonic timestamps in C#](https://antonymale.co.uk/monotonic-timestamps-in-csharp.html)
// [How do i get from Stopwatch.GetTimestamp() to a DateTime?](https://stackoverflow.com/questions/1438652/how-do-i-get-from-stopwatch-gettimestamp-to-a-datetime)
// [Add a monotonic clock to the framework #15207](https://github.com/dotnet/runtime/issues/15207)
// [How do you convert Stopwatch ticks to nanoseconds, milliseconds and seconds?](https://stackoverflow.com/questions/2329079/how-do-you-convert-stopwatch-ticks-to-nanoseconds-milliseconds-and-seconds)
// [Stopwatch.GetTimestamp Method](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.stopwatch.gettimestamp?view=net-8.0)
// https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/Diagnostics/Stopwatch.cs
// [Stopwatch.Frequency Field](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.stopwatch.frequency?view=net-8.0)
// > Gets the frequency of the timer as the number of ticks per second.
// https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.stopwatch.frequency?view=net-8.0
