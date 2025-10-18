// Port of: https://github.com/sccn/liblsl/blob/master/examples/HandleMetaData.cpp
namespace SharpLSL.Examples
{
    internal class HandleMetaData
    {
        static void Main(string[] args)
        {
            try
            {
                // Create a new stream info and declare some meta-data (in accordance with XDF format).
                var name = args.Length >= 1 ? args[0] : "MetaTester";

                var streamInfo = new StreamInfo(name, "EEG", 8, 100, ChannelFormat.Float, "myuid9527");

                var channelInfos = streamInfo.Description.AppendChild("channels");
                var channelNames = new string[] { "C3", "C4", "Cz", "FPz", "POz", "CPz", "O1", "O2" };

                foreach (var channel in channelNames)
                {
                    channelInfos.AppendChild("channel")
                        .AppendChild("label", channel)
                        .AppendChild("unit", "microvolts")
                        .AppendChild("type", "EEG");
                }

                streamInfo.Description.AppendChild("manufacturer", "SCCN");
                streamInfo.Description.AppendChild("cap")
                    .AppendChild("name", "EasyCap")
                    .AppendChild("size", "54")
                    .AppendChild("labelscheme", "10-20");

                // Create outlet for the stream.
                using (var streamOutlet = new StreamOutlet(streamInfo))
                {
                    // The following could run on another computer.

                    // Resolve the stream and open an inlet.
                    var streamInfos = LSL.Resolve("name", name);

                    using (var streamInlet = new StreamInlet(streamInfos[0]))
                    {
                        // Get the full stream info (including custom meta-data) and dissect it.
                        using (var streamInletInfo = streamInlet.GetStreamInfo())
                        {
                            Console.WriteLine("The stream's XML meta-data is:");
                            Console.WriteLine(streamInletInfo.ToXML());

                            Console.WriteLine("\nThe manufacturer is: {0}",
                                streamInletInfo.Description.ChildValue("manufacturer"));

                            Console.WriteLine("\nThe cap circumference is: {0}",
                                streamInletInfo.Description.Child("cap").ChildValue("size"));

                            Console.WriteLine("\nThe channel labels are as follows:\n");

                            var channel = streamInletInfo
                                .Description
                                .Child("channels")
                                .Child("channel");
                            for (int c = 0; c < streamInletInfo.ChannelCount; ++c)
                            {
                                Console.WriteLine("  {0}", channel.ChildValue("label"));
                                channel = channel.NextSibling();
                            }
                        }
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
