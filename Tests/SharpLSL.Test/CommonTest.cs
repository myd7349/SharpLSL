using Xunit;

using SharpLSL.Interop;

namespace SharpLSL.Test;

public class CommonTest
{
    [Fact]
    public void Given_When_Then()
    {
        Assert.Equal(4, sizeof(LslChannelFormat));
        Assert.Equal(114, LSL.LIBLSL_COMPILE_HEADER_VERSION);
    }
}
