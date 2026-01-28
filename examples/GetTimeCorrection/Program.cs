// Port of: https://github.com/sccn/liblsl/blob/main/examples/GetTimeCorrection.cpp
// This example demonstrates how a time correction value can be obtained on demand for a particular
// stream on the net. This time correction value, when added to the timestamp of an obtained
// sample, remaps the sample's timestamp into the local clock domain (so it is in the same domain
// as what LSL.GetLocalClock() would return). For streams coming from the same computer, this value
// should be approx. 0 (up to some tolerance).
namespace SharpLSL.Examples
{
    internal class GetTimeCorrection
    {
        static void Main(string[] args)
        {
            string field;
            string value;

            if (args.Length < 2)
            {
                Console.WriteLine("This connects to a stream which has a particular value for a given field and gets the time synchronization information for it.");

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
                    // Start receiving & displaying the data.
                    Console.WriteLine("Press [Enter] to query a new time correction value (clocks may drift)...");

                    while (true)
                    {
                        Console.WriteLine(streamInlet.TimeCorrection());
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Got an exception:\n{ex}");
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
