// Port of: https://github.com/sccn/liblsl/blob/main/examples/ReceiveDataInChunks.cpp
using System.Diagnostics;

namespace SharpLSL.Examples
{
    internal class ReceiveDataInChunks
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ReceiveDataInChunks");
            Console.WriteLine("ReceiveDataInChunks StreamName MaxBufferLength Flush");
            Console.WriteLine("    - MaxBufferLength: Duration in seconds (or x100 samples if sample rate is 0) to buffer in the receiver.");
            Console.WriteLine("    - Flush: Set non-zero to flush data instead of pulling; useful for testing throughput.");

            try
            {
                var name = args.Length > 0 ? args[0] : "MyAudioStream";

                var maxBuffered = 360;
                if (args.Length > 1 &&
                    int.TryParse(args[1], out var maxBufferedArg) &&
                    maxBufferedArg >= 0)
                    maxBuffered = maxBufferedArg;

                var flush = false;
                if (args.Length > 2)
                {
                    if (bool.TryParse(args[2], out var flushArg))
                        flush = flushArg;
                    else if (int.TryParse(args[2], out var intArg))
                        flush = intArg != 0;
                }

                // Resolve the stream of interest & make an inlet.
                using (var streamInfo = LSL.Resolve("name", name)[0])
                using (var streamInlet = new StreamInlet(streamInfo, maxBufferLength: maxBuffered))
                {
                    // Use SetPostProcessing to get the timestamps in a common base clock.
                    // Do not use if this application will record timestamps to disk -- it is
                    // better to do posthoc synchronization.
                    streamInlet.SetPostProcessingOptions(PostProcessingOptions.All);

                    // Inlet opening is implicit when doing PullSample or PullChunk.
                    // Here we open the stream explicitly because we might be doing
                    // `flush` only.
                    streamInlet.OpenStream();

                    var startTimestamp = LSL.GetLocalClock();
                    var nextDisplay = startTimestamp + 1;
                    var nextReset = startTimestamp + 10;

                    // And retrieve the chunks.
                    ulong k = 0;
                    ulong numSamples = 0;

                    var chunk = new List<short[]>();

                    var fetchIntervalInMilliseconds = 20;
                    long expectedElapsedMilliseconds = fetchIntervalInMilliseconds;

                    var stopwatch = Stopwatch.StartNew();

                    while (true)
                    {
                        var sleepIntervalInMilliseconds =
                            expectedElapsedMilliseconds - stopwatch.ElapsedMilliseconds;
                        if (sleepIntervalInMilliseconds > 0)
                            Thread.Sleep((int)sleepIntervalInMilliseconds);

                        if (flush)
                        {
                            // You almost certainly don't want to use flush.
                            // This is here so we can test maximum outlet throughput.
                            numSamples += streamInlet.Flush();
                        }
                        else
                        {
                            var timestamp = PullChunk(streamInlet, chunk);
                            if (timestamp != 0.0)
                                numSamples += (uint)chunk.Count;
                        }

                        ++k;
                        expectedElapsedMilliseconds += fetchIntervalInMilliseconds;
                        if (k % 50 == 0)
                        {
                            var now = LSL.GetLocalClock();
                            var srate = numSamples / (now - startTimestamp);

                            Console.WriteLine($"{srate} samples/sec");

                            if (now > nextReset)
                            {
                                Console.WriteLine("Resetting counters...");

                                startTimestamp = now;
                                nextReset = now + 10;
                                numSamples = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Got an exception: {ex}");
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }

        static double PullChunk(StreamInlet streamInlet, List<short[]> chunk)
        {
            double timestamp = 0.0;
            double thisTimestamp;
            var sample = new short[streamInlet.ChannelCount];

            chunk.Clear();

            while ((thisTimestamp = streamInlet.PullSample(sample, 0.0)) != 0.0)
            {
                chunk.Add(sample);
                timestamp = thisTimestamp;
                sample = new short[streamInlet.ChannelCount];
            }

            return timestamp;
        }
    }
}

// References:
// [sleep-until in c#](https://stackoverflow.com/questions/12834659/sleep-until-in-c-sharp)
