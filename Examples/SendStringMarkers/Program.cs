// Port of: https://github.com/sccn/liblsl/blob/master/examples/SendStringMarkers.cpp
// This example program offers a 1-channel stream which contains strings.
// The stream has the "Marker" content type and irregular sampling rate.
// The name of the stream can be chosen as a startup parameter.
namespace SharpLSL.Examples
{
    internal class SendStringMarkers
    {
        static void Main(string[] args)
        {
            var name = args.Length > 0 ? args[0] : "MyEventStream";

            try
            {
                // Make a new stream info and stream outlet.
                using (var streamInfo = new StreamInfo(name, "Markers", 1, LSL.IrregularRate, ChannelFormat.String, "id9527"))
                using (var streamOutlet = new StreamOutlet(streamInfo))
                {
                    // Send random marker strings.
                    Console.WriteLine("Now sending markers...");

                    var markerTypes = new string[]
                    {
                        "Test", "Blah", "Marker", "XXX", "Testtest", "Test-1-2-3"
                    };

                    var rng = new Random();
                    var marker = new string[1];

                    while (true)
                    {
                        // Wait for 0~1000ms.
                        Thread.Sleep(rng.Next(1000));

                        // And choose the marker to send.
                        marker[0] = markerTypes[rng.Next(markerTypes.Length)];
                        Console.WriteLine($"Now sending: {marker[0]}.");

                        streamOutlet.PushSample(marker);
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
