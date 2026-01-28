// Port of: https://github.com/sccn/liblsl/blob/main/examples/ReceiveData.cpp
// This example demonstrates how to resolve a specific stream on the lab
// network and how to connect to it in order to receive data.
namespace SharpLSL.Examples
{
    internal class ReceiveData
    {
        static void Main(string[] args)
        {
            string field;
            string value;

            if (args.Length < 2)
            {
                Console.WriteLine("This connects to a stream which has a particular value for a given field and receives data.");
                Console.WriteLine("Please enter a field name and the desired value (e.g. \"type EEG\" (without the quotes)): ");

                var input = Console.ReadLine();
                if (input == null)
                    return;

                var inputParts = input.Split();
                if (inputParts?.Length != 2)
                    return;

                field = inputParts[0];
                value = inputParts[1];
            }
            else
            {
                field = args[0];
                value = args[1];
            }

            int maxSamples = 10;
            if (args.Length > 2 &&
                int.TryParse(args[2], out var maxSamplesArg) &&
                maxSamplesArg > 0)
                maxSamples = maxSamplesArg;

            Console.WriteLine("Now resolving streams...");

            var streamInfos = LSL.Resolve(field, value);
            if (!(streamInfos?.Length > 0))
            {
                Console.WriteLine("No stream found.");
                return;
            }

            Console.WriteLine("Here is what was resolved:");
            Console.WriteLine(streamInfos[0].ToXML());

            Console.WriteLine("Now creating the inlet...");

            // Make an inlet to get data from it.
            using (var streamInlet = new StreamInlet(streamInfos[0]))
            {
                // Start receiving & displaying the data.
                Console.WriteLine("Now pulling samples...");

                var sample = new float[streamInlet.ChannelCount];
                var chunk = new List<float[]>();
                var chunkMultiplexed = new List<float>();

                for (int i = 0; i < maxSamples; ++i)
                {
                    // Pull a single sample.
                    streamInlet.PullSample(sample);
                    PrintChunk(sample, streamInlet.ChannelCount);

                    // Sleep so the outlet will have time to push some samples.
                    Thread.Sleep(500);

                    PullChunk(streamInlet, chunk);
                    PrintChunk(chunk);

                    Thread.Sleep(500);

                    // Pull a multiplexed chunk into a flat buffer.
                    PullChunkMultiplexed(streamInlet, chunkMultiplexed);
                    PrintChunk(chunkMultiplexed, streamInlet.ChannelCount);
                }
            }

            if (args.Length == 0)
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }

        static double PullChunk(StreamInlet streamInlet, List<float[]> chunk)
        {
            int channelCount = streamInlet.ChannelCount;
            double timestamp = 0.0;
            double thisTimestamp;
            var sample = new float[channelCount];

            chunk.Clear();

            while ((thisTimestamp = streamInlet.PullSample(sample)) != 0.0)
            {
                chunk.Add(sample);
                timestamp = thisTimestamp;
                sample = new float[channelCount];
            }

            return timestamp;
        }

        static bool PullChunkMultiplexed(
            StreamInlet streamInlet,
            List<float> chunk,
            List<double>? timestamps = null,
            double timeout = 0.0,
            bool append = false)
        {
            if (!append)
            {
                chunk.Clear();
                timestamps?.Clear();
            }

            var sample = new float[streamInlet.ChannelCount];
            var timestamp = streamInlet.PullSample(sample, timeout);
            if (timestamp == 0.0)
                return false;

            chunk.AddRange(sample);
            timestamps?.Append(timestamp);

            while ((timestamp= streamInlet.PullSample(sample)) != 0.0)
            {
                chunk.AddRange(sample);
                timestamps?.Append(timestamp);
            }

            return true;
        }

        static void PrintChunk(IList<float> chunk, int channelCount)
        {
            for (int i = 0; i < chunk.Count; ++i)
            {
                Console.Write(chunk[i]);

                if (i % channelCount == channelCount - 1)
                    Console.Write('\n');
                else
                    Console.Write(' ');
            }
        }

        static void PrintChunk(List<float[]> chunk)
        {
            foreach (var sample in chunk)
                PrintChunk(sample, sample.Length);
        }
    }
}
