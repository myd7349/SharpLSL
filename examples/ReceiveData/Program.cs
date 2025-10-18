// Port of: https://github.com/sccn/liblsl/blob/master/examples/ReceiveData.cpp
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

            // Make an inlet to get data from it.
            using (var streamInlet = new StreamInlet(streamInfos[0]))
            {
                // Start receiving & displaying the data.
                Console.WriteLine("Now pulling samples...");

                var sample = new float[streamInlet.ChannelCount];
                var chunk = new List<float[]>();

                for (int i = 0; i < maxSamples; ++i)
                {
                    // Pull a single sample.
                    streamInlet.PullSample(sample);
                    PrintChunk(sample, streamInlet.ChannelCount);

                    // Sleep so the outlet will have time to push some samples.
                    Thread.Sleep(500);

                    PullChunk(streamInlet, chunk, streamInlet.ChannelCount);
                    PrintChunk(chunk);

                    // Pull a multiplexed chunk into a flat buffer.
                    //Thread.Sleep(500);
                    //inlet.pull_chunk_multiplexed(sample);
                    //printChunk(sample, inlet.get_channel_count());
                }
            }
        }

        // TODO:
        static double PullChunk(StreamInlet streamInlet, List<float[]> chunk, int channelCount)
        {
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

        static void PrintChunk(float[] chunk, int channelCount)
        {
            for (int i = 0; i < chunk.Length; ++i)
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

// TODO: Append array to a list.
