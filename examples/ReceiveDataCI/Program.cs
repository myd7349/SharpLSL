using SharpLSL;

Console.WriteLine($"64 bit? {Environment.Is64BitProcess}");

// If this line of code runs successfully, it means
// the liblsl dynamic library has been loaded successfully.
Console.WriteLine($"liblsl version: {LSL.GetLibraryVersion()}");
Console.WriteLine($"liblsl library info: {LSL.GetLibraryInfo()}");

StreamInfo[] results = LSL.Resolve("type", "EEG");

// open an inlet and print some interesting info about the stream (meta-data, etc.)
using StreamInlet inlet = new StreamInlet(results[0]);
Console.Write(inlet.GetStreamInfo().ToXML());

// read samples
float[] sample = new float[8];
int count = 0;
while (count < 100)
{
    inlet.PullSample(sample);
    foreach (float f in sample)
        Console.Write("\t{0}", f);
    Console.WriteLine();
    count += 1;
}
