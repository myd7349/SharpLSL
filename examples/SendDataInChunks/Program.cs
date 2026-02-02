// Port of: https://github.com/sccn/liblsl/blob/main/examples/SendDataInChunks.cpp
namespace SharpLSL.Examples
{
    internal class SendDataInChunks
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("SendDataInChunks");
            Console.WriteLine("SendDataInChunks StreamName StreamType SampleRate ChannelCount MaxBuffered ChunkRate");
            Console.WriteLine("    - MaxBuffered -- Duration in sec (or x100 samples if SampleRate is 0) to buffer for each outlet.");
            Console.WriteLine("    - ChunkRate -- Number of chunks pushed per second. For this example, make it a common factor of sampling rate and 1000.");

            var streamName = args.Length > 0 ? args[0] : "MyAudioStream";
            var streamType = args.Length > 1 ? args[1] : "Audio";

            // Here we specify srate, but typically this would come from the device.
            var sampleRate = 44100;
            if (args.Length > 2 &&
                int.TryParse(args[2], out var sampleRateArg) &&
                sampleRateArg >= 0)
                sampleRate = sampleRateArg;

            // Here we specify channel count, but typically this would come from theh device.
            var channelCount = 2;
            if (args.Length > 3 &&
                int.TryParse(args[3], out var channelCountArg) &&
                channelCountArg > 0)
                channelCount = channelCountArg;

            var maxBuffered = 360.0;
            if (args.Length > 4 &&
                double.TryParse(args[4], out var maxBufferedArg) &&
                maxBufferedArg > 0.0)
                maxBuffered = maxBufferedArg;

            // Chunks per second
            var chunkRate = 10;
            if (args.Length > 5 &&
                int.TryParse(args[5], out var chunkRateArg) &&
                chunkRateArg > 0)
                chunkRate = chunkRateArg;

            // Samples per chunk
            var chunkSamples = sampleRate > 0 ?
                Math.Max(sampleRate / chunkRate, 1) :
                100;

            // Milliseconds per chunk
            var chunkDuration = 1000 / chunkRate;

            try
            {
                using var streamInfo = new StreamInfo(
                    streamName,
                    streamType,
                    channelCount,
                    sampleRate,
                    ChannelFormat.Int16,
                    "example-SendDataInChunks");

                streamInfo.Description
                    .AppendChildValue("manufacturer", "LSL");

                var channelInfos = streamInfo.Description
                    .AppendChild("channels");
                for (int c = 0; c < channelCount; ++c)
                {
                    var channelInfo = channelInfos
                        .AppendChild("channel")
                        .AppendChildValue("label", $"Chan-{c}")
                        .AppendChildValue("unit", "microvolts")
                        .AppendChildValue("type", streamType);
                }

                var bufferedSamples = Convert.ToInt32(maxBuffered * sampleRate);

                using var streamOutlet = new StreamOutlet(streamInfo, chunkSamples, bufferedSamples);

                using var fullStreamInfo = streamOutlet.GetStreamInfo();
                Console.WriteLine($"Stream UID: {fullStreamInfo.Uid}.");

                // Create a connection to our device.
                var myDevice = new FakeDevice(channelCount, sampleRate);

                // Prepare buffer to get data from 'device'.
                // The buffer should be larger than you think you need. Here we make it 4x as large.
                var chunkBuffer = new short[4 * chunkSamples * channelCount];

                Console.WriteLine("Now sending data...");

                // Your device might have its own timer. Or you can decide how often to poll
                // your device, as we do here.
                using var periodicTimer = new PeriodicTimer(
                    TimeSpan.FromMilliseconds(chunkDuration));

                while (await periodicTimer.WaitForNextTickAsync())
                {
                    var returnedSamples = myDevice.GetData(chunkBuffer);

                    // Send it to the outlet.
                    var timestamp = LSL.GetLocalClock();

                    streamOutlet.PushChunk(
                        chunkBuffer.AsSpan(0, returnedSamples * channelCount),
                        timestamp,
                        true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Got an exception:\n{ex.Message}");
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
