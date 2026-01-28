// Port of: https://github.com/sccn/liblsl/blob/main/examples/SendData.cpp
// This example program offers an 8-channel stream, float-formatted, that resembles EEG data.
// The example demonstrates also how per-channel meta-data can be specified using the .desc() field
// of the stream information object.
// Note that the timer used in the send loop of this program is not particularly accurate.
using System.Diagnostics;

namespace SharpLSL.Examples
{
    internal class SendData
    {
        static string[] Channels = new string[]
        {
            "C3", "C4", "Cz", "FPz", "POz", "CPz", "O1", "O2"
        };

        static int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        static int AlignUp(int a, int b)
        {
            return (a + b - 1) / b * b;
        }

        static int GetTimeSliceInMilliseconds(int srate, int perfectTimeSliceInMilliseconds = 50)
        {
            var gcd = GCD(1000, srate);
            var minimumTimeSlice = 1000 / gcd;
            if (minimumTimeSlice < perfectTimeSliceInMilliseconds)
                return AlignUp(perfectTimeSliceInMilliseconds, minimumTimeSlice);
            else
                return minimumTimeSlice;
        }

        static void Main(string[] args)
        {
            string name;
            string type;

            if (args.Length < 2)
            {
                Console.WriteLine("This opens a stream under some user-defined name and with a user-defined content type.");
                Console.WriteLine("SendData Name Type [n_channels=8] [srate=100] [max_buffered=360]");

                Console.WriteLine("Please enter the stream name and the stream type (e.g. \"BioSemi EEG\" (without the quotes)):");
                var input = Console.ReadLine();
                if (input == null)
                    return;

                var inputParts = input.Split();
                if (inputParts.Length != 2)
                    return;

                name = inputParts[0];
                type = inputParts[1];
            }
            else
            {
                name = args[0];
                type = args[1];
            }

            var channels = 8;
            if (args.Length >= 3 &&
                int.TryParse(args[2], out var channelsArg) &&
                channelsArg > 0)
                channels = channelsArg;

            var srate = 100;
            if (args.Length >= 4 &&
                int.TryParse(args[3], out var srateArg) &&
                (srateArg > 0 || srateArg == LSL.IrregularRate))
                srate = srateArg;

            var maxBuffered = 360;
            if (args.Length >= 5 &&
                int.TryParse(args[4], out var maxBufferedArg) &&
                maxBufferedArg >= 0)
                maxBuffered = maxBufferedArg;

            try
            {
                var streamInfo = new StreamInfo(
                    name,
                    type,
                    channels,
                    srate,
                    ChannelFormat.Float,
                    name + type);

                streamInfo.Description.AppendChildValue("manufacturer", "LSL");

                var channelInfos = streamInfo.Description.AppendChild("channels");
                for (int c = 0; c < channels; ++c)
                {
                    channelInfos.AppendChild("channel")
                        .AppendChildValue("label", c < 8 ? Channels[c] : $"CH{c + 1}")
                        .AppendChildValue("unit", "microvolts")
                        .AppendChildValue("type", type);
                }

                // Make a new outlet
                using var streamOutlet = new StreamOutlet(streamInfo, 0, maxBuffered);

                var sample = new float[channels];

                // Your device might have its own timer. Or you can decide how often to poll
                // your device, as we do here.
                var timeSliceInMilliseconds = GetTimeSliceInMilliseconds(srate > 0 ? srate : 100);
                var samples = Convert.ToInt32(timeSliceInMilliseconds / 1000.0 * (srate > 0 ? srate : 100));

                Console.WriteLine($"Time slice: {timeSliceInMilliseconds}ms, samples: {samples}.");
                Console.WriteLine("Now sending data...");

                var rng = new Random();

                uint t = 0;

                var stopwatch = Stopwatch.StartNew();
                long expectedElapsedMs = 0;

                while (true)
                {
                    for (int s = 0; s < samples; ++s, ++t)
                    {
                        // Create random data for the first 8 channels.
                        for (int c = 0; c < Math.Min(channels, 8); ++c)
                            sample[c] = (rng.Next() % 1500) / 500f - 1.5f;

                        // For the remaining channels, fill them with a sample counter (wraps at 1M).
                        for (int c = 8; c < channels; ++c)
                            sample[c] = t % 1000000;

                        Console.WriteLine($"{sample[0]}\t{sample[channels - 1]}");
                        streamOutlet.PushSample(sample);
                    }

                    expectedElapsedMs += timeSliceInMilliseconds;
                    var elapsedMs = stopwatch.ElapsedMilliseconds;
                    if (expectedElapsedMs > elapsedMs)
                    {
                        // Wait until the next expected sample time.
                        Thread.Sleep((int)(expectedElapsedMs - elapsedMs));
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
    }
}
