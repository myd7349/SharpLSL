// Port of: https://github.com/sccn/liblsl/blob/master/examples/SendDataSimple.cpp
// This is an example of how a simple data stream can be offered on the network.
// Here, the stream is named SimpleStream, has content-type EEG, and 8 channels.
// The transmitted samples contain random numbers (and the sampling rate is irregular).
namespace SharpLSL.Examples
{
    internal class SendDataSimple
    {
        private const int Channels = 8;

        static void Main(string[] args)
        {
            var name = args.Length > 0 ? args[0] : "SimpleStream";

            // Make a new stream info and open an outlet with it.
            using (var streamInfo = new StreamInfo(name, "EEG", Channels))
            using (var streamOutlet = new StreamOutlet(streamInfo))
            {
                while (!streamOutlet.WaitForConsumers(120))
                {
                }

                var sample = new float[Channels];
                var rng = new Random();

                // Send data forever.
                while (streamOutlet.HaveConsumers())
                {
                    // Generate random data.
                    for (int c = 0; c < Channels; ++c)
                        sample[c] = Convert.ToSingle(rng.Next(1500) / 500.0 - 1.5);

                    // Send it.
                    streamOutlet.PushSample(sample);

                    Thread.Sleep(5);
                }
            }
        }
    }
}