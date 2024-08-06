using Xunit;

using static SharpLSL.Interop.LSL;

namespace SharpLSL.Test;

public class CommonTest
{
    [Fact]
    public void TestVersion()
    {
        Assert.Equal(114, LIBLSL_COMPILE_HEADER_VERSION);
    }

    [Fact]
    public void TestChannelFormat()
    {
        Assert.Equal(4, sizeof(ChannelFormat));
    }
}
