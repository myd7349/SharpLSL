// Port of: https://github.com/sccn/liblsl/blob/master/examples/TestSyncWithoutData.cpp

namespace SharpLSL.Examples
{
    internal class TestSyncWithoutData
    {
        static void Main(string[] args)
        {
            try
            {
                using (var streamInfo = new StreamInfo("SyncTest", "Test", 1, LSL.IrregularRate, ChannelFormat.Int16, "id9527"))
                using (var streamOutlet = new StreamOutlet(streamInfo))
                {
                    var foundStreamInfos = LSL.Resolve("name", "SyncTest");
                    if (foundStreamInfos.Length == 0)
                    {
                        Console.WriteLine("Sender outlet not found!");
                        return;
                    }

                    var firstStreamInfo = foundStreamInfos[0];
                    Console.WriteLine($"Found {firstStreamInfo.Name}@{firstStreamInfo.HostName}.");
                    
                    using (var streamInlet = new StreamInlet(firstStreamInfo))
                    {
                        var pushThread = new Thread(() => PushThread(streamOutlet));
                        pushThread.Start();

                        var endTime = LSL.GetLocalClock() + 20;
                        while (LSL.GetLocalClock() < endTime)
                        {
                            try
                            {
                                Console.WriteLine($"Got time correction data: {streamInlet.TimeCorrection(1)}.");
                                Thread.Sleep(1);
                            }
                            catch (Exception ex2)
                            {
                                Console.WriteLine($"Error getting time correction data: {ex2}");
                            }
                        }

                        pushThread.Join();
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

        static void PushThread(StreamOutlet streamOutlet)
        {
            Thread.Sleep(10000);

            Console.WriteLine("Pushing data now.");

            var sample = new short[1];
            for (short i = 0; i < 10; ++i)
            {
                sample[0] = i;
                streamOutlet.PushSample(sample);

                Thread.Sleep(1000);
            }
        }
    }
}