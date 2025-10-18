// Port of: https://github.com/sccn/liblsl/blob/master/examples/ReceiveStringMarkers.cpp
// This example program demonstrates how a marker string stream on the network (here, of any name)
// can be resolved, and how the marker strings (assumed to be delivered at irregular rate) can be
// pulled from their source.
namespace SharpLSL.Examples
{
    internal class ReceiveStringMarkers
    {
        static void Main(string[] args)
        {
            var streamInlets = new List<StreamInlet>();

            foreach (var streamInfo in LSL.Resolve("type", "Markers"))
            {
                if (streamInfo.ChannelCount != 1)
                {
                    Console.WriteLine($"Skipping stream {streamInfo.Name} because it has more than one channel.");
                }
                else
                {
                    streamInlets.Add(new StreamInlet(streamInfo));
                    Console.WriteLine($"Listening to {streamInfo.Name}...");
                }
            }

            if (streamInlets.Count == 0)
            {
                Console.WriteLine("No marker stream found.");
                return;
            }

            var sample = new string[1];
            var startTimestamp = LSL.GetLocalClock();

            while (true)
            {
                foreach (var streamInlet in streamInlets)
                {
                    var timestamp = streamInlet.PullSample(sample, 0.2);
                    if (timestamp != 0.0)
                    {
                        Console.WriteLine("{0}\t{1}",
                            timestamp - streamInlet.TimeCorrection(1) - startTimestamp,
                            sample[0]);
                    }
                }
            }
        }
    }
}
