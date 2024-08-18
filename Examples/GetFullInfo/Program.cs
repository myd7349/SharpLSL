// Port of: https://github.com/sccn/liblsl/blob/master/examples/GetFullinfo.cpp
// This example demonstrates how the full version of the stream info (i.e. including the
// potentially large .desc field) can be obtained from an inlet. Note that the output of
// the resolve functions only includes the core information otherwise.
namespace SharpLSL.Examples
{
    internal class GetFullInfo
    {
        static void Main(string[] args)
        {
            string field;
            string value;

            if (args.Length != 2)
            {
                Console.WriteLine("This connects to a stream which has a particular value for a given field and displays its full stream_info contents.");
                Console.WriteLine("Please enter a field name and the desired value (e.g. \"type EEG\" (without the quotes)):");

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

            try
            {
                // Resolve the stream of interest.
                Console.WriteLine("Now resolving streams...");

                var streamInfos = LSL.Resolve(field, value);

                Console.WriteLine("Here is what was resolved:");
                Console.WriteLine(streamInfos[0].ToXML());

                // Make an inlet to get data from it.
                Console.WriteLine("Now creating the inlet...");

                using (var streamInlet = new StreamInlet(streamInfos[0]))
                {
                    // Get & display the info.
                    Console.WriteLine("The information about this stream is displayed in the following:");

                    using (var streamInfo = streamInlet.GetStreamInfo())
                    {
                        Console.WriteLine(streamInfo.ToXML());
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