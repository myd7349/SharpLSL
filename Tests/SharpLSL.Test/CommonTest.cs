using Xunit;

using static SharpLSL.Interop.LSL;

namespace SharpLSL.Test;

public class CommonTest
{
    [Fact]
    public void Given_When_Then()
    {
        Assert.Equal(4, sizeof(ChannelFormat));
        Assert.Equal(114, LIBLSL_COMPILE_HEADER_VERSION);
    }
}
