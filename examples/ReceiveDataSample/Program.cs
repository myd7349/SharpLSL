// Port of: https://github.com/sccn/liblsl/blob/master/examples/ReceiveDataSimple.cpp
// This is a minimal example that demonstrates how a multi-channel stream (here 128ch) of a
// particular name (here: SimpleStream) can be resolved into an inlet, and how the raw sample
// data & timestamps are pulled from the inlet.
// This example doesn't display the obtained data.
namespace SharpLSL.Examples
{
    internal class ReceiveDataSample
    {
        static void Main(string[] args)
        {
            var name = args.Length >= 1 ? args[0] : "SimpleStream";

            var streamInfos = LSL.Resolve("name", name);

            // Resolve the stream of interest & make an inlet to get data from the first result.
            using (var streamInlet = new StreamInlet(streamInfos[0]))
            {
                // Receive data & timestamps forever (not displaying them here).
                var sample = new float[streamInlet.ChannelCount];

                while (true)
                {
                    var timestamp = streamInlet.PullSample(sample);

                    Console.Write(timestamp);

                    if (streamInlet.ChannelCount >= 2)
                        Console.WriteLine("\t{0}\t{1}...", sample[0], sample[1]);
                    else
                        Console.WriteLine("\t{0}...", sample[0]);
                }
            }
        }
    }
}
