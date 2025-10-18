// Port of https://github.com/sccn/liblsl/blob/master/examples/GetAllStreams.cpp
// This example program shows how all streams that are currently present on the
// lab network can be resolved and displayed. This is useful for browsing applications.
namespace SharpLSL.Examples
{
    internal class GetAllStreams
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Here is a one-shot resolve of all current streams:");

                // Discover all streams on the network
                var streamInfos = LSL.Resolve();

                var foundStreams = new Dictionary<string, StreamInfo>();
                foreach (var streamInfo in streamInfos)
                {
                    foundStreams[streamInfo.Uid] = streamInfo;
                    Console.WriteLine(streamInfo.ToXML());
                    Console.WriteLine();
                }

                Console.WriteLine("Press any key to switch to the continuous resolver test.");
                Console.ReadKey();

                using (var continuousResolver = new ContinuousResolver())
                {
                    while (true)
                    {
                        var resolvedStreams = continuousResolver.Results();
                        foreach (var streamInfo in resolvedStreams)
                        {
                            var streamUid = streamInfo.Uid;
                            if (!foundStreams.ContainsKey(streamUid))
                            {
                                foundStreams[streamUid] = streamInfo;
                                Console.WriteLine($"Found {streamInfo.Name}@{streamInfo.HostName}");
                            }
                        }

                        var missingStreams = foundStreams.Values
                            .Where(streamInfo => resolvedStreams.All(
                                resolvedStreamInfo => resolvedStreamInfo.Uid != streamInfo.Uid));
                        foreach (var streamInfo in missingStreams)
                        {
                            Console.WriteLine($"Lost {streamInfo.Name}@{streamInfo.HostName}");
                            foundStreams.Remove(streamInfo.Uid);
                        }

                        Thread.Sleep(1000);
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
