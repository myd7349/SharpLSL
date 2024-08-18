﻿// Port of: https://github.com/sccn/liblsl/blob/master/examples/SendDataMinimal.cpp
// This is a minimal example of how a multi-channel data stream can be sent to LSL.
// Here, the stream is named SimpleStream, has content-type EEG, 8 channels, and 200Hz.
// The transmitted samples contain random numbers.
namespace SharpLSL.Examples
{
    internal class SendDataMinimal
    {
        private const int Channels = 8;

        static void Main(string[] args)
        {
            // Make a new stream info and open an outlet with it.
            using (var streamInfo = new StreamInfo("SimpleStream", "EEG", Channels, 200.0))
            using (var streamOutlet = new StreamOutlet(streamInfo))
            {
                var sample = new float[Channels];
                var rng = new Random();

                // Send data forever.
                while (true)
                {
                    // Generate random data.
                    for (int c = 0; c < Channels; ++c)
                        sample[c] = Convert.ToSingle(rng.Next(1500) / 500.0 - 1.5);

                    // Send it.
                    streamOutlet.PushSample(sample);

                    // Maintain our desired sampling rate (approximately; real software might do something else).
                    Thread.Sleep(5);
                }
            }
        }
    }
}
