using Xunit;

namespace SharpLSL.Test;

public class StreamInfoTest
{
    [Fact]
    public void TestStringStream()
    {
        using (var streamInfo = new StreamInfo(
            "MyStream",
            "EEG",
            channelCount: 5,
            channelFormat: ChannelFormat.String))
        {
            Assert.Equal("MyStream", streamInfo.Name);
            Assert.Equal("EEG", streamInfo.Type);
            Assert.Equal(5, streamInfo.ChannelCount);
            Assert.Equal(0, streamInfo.SampleBytes);
            Assert.Equal(ChannelFormat.String, streamInfo.ChannelFormat);
        }
    }
}
