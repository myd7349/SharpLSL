using SharpLSL;

// create stream info and outlet
using StreamInfo info = new StreamInfo("TestCSharp", "EEG", 8, 100, ChannelFormat.Float, "sddsfsdf");
using StreamOutlet outlet = new StreamOutlet(info);
float[] data = new float[8];
int count = 0;
while (count < 200)
{
    // generate random data and send it
    for (int k = 0; k < data.Length; k++)
        data[k] = Random.Shared.Next(-100, 100);
    outlet.PushSample(data);
    Thread.Sleep(10);
    count += 1;
}
