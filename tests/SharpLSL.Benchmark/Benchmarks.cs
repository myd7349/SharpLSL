using System.Diagnostics;

using BenchmarkDotNet.Attributes;
using Microsoft.VSDiagnostics;

namespace SharpLSL.Benchmark
{
    // For more information on the VS BenchmarkDotNet Diagnosers see https://learn.microsoft.com/visualstudio/profiling/profiling-with-benchmark-dotnet
    [CPUUsageDiagnoser]
    public class Benchmarks
    {
        [GlobalSetup]
        public void Setup()
        {
        }

        [Benchmark]
        public double GetLocalClock()
        {
            return LSL.GetLocalClock();
        }

        [Benchmark]
        public double GetLocalClockManaged()
        {
            return Stopwatch.GetTimestamp() / (double)Stopwatch.Frequency;
        }

        [Benchmark]
        public double GetLocalClockManagedV2()
        {
            return Stopwatch.GetElapsedTime(0).TotalSeconds;
        }
    }
}
