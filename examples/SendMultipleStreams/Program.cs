// Port of: https://github.com/sccn/liblsl/blob/main/examples/SendMultipleStreams.cpp
using System.Diagnostics;

namespace SharpLSL.Examples
{
    internal class SendMultipleStreams
    {
        static void Main(string[] args)
        {
            try
            {
                var name = args.Length > 0 ? args[0] : "MultiStream";

                var srate = 1000;
                if (args.Length > 1 &&
                    int.TryParse(args[1], out var srateArg) &&
                    srateArg >= 0)
                    srate = srateArg;

                var streamOutlets = new List<StreamOutlet>();

                var formats = new ChannelFormat[]
                {
                    ChannelFormat.Int16,
                    ChannelFormat.Int32,
                    ChannelFormat.Int64,
                    ChannelFormat.Double,
                    ChannelFormat.String,
                };

                foreach (var format in formats)
                {
                    using var streamInfo = new StreamInfo(
                        name + format.ToString(),
                        "Example",
                        1,
                        srate,
                        format);
                    var streamOutlet = new StreamOutlet(
                        streamInfo, 0, 360);
                    streamOutlets.Add(streamOutlet);
                }

                Console.WriteLine("Now sending data...");

                var chunk = new int[srate];

                var stopwatch = Stopwatch.StartNew();
                long expectedElapsedTime = 1000; // In ms.

                for (int c = 0; c < srate * 600;)
                {
                    // Increment the sample counter.
                    for (int s = 0; s < chunk.Length; ++s)
                        chunk[s] = c++;

                    var sleepInterval = Convert.ToInt32(
                        expectedElapsedTime - stopwatch.ElapsedMilliseconds);
                    if (sleepInterval > 0)
                        Thread.Sleep(sleepInterval);

                    expectedElapsedTime += 1000;

                    var timestamp = LSL.GetLocalClock();

                    foreach (var streamOutlet in streamOutlets)
                        streamOutlet.PushChunk(chunk, timestamp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Got an exception: {ex}");
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
