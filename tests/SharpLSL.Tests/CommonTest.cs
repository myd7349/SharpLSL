using System;

using Xunit;

using static SharpLSL.Interop.LSL;

namespace SharpLSL.Tests;

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

    [Fact]
    public void SetConfigFilePathThrowsOnNull()
    {
        Assert.Throws<ArgumentException>(() => LSL.SetConfigFilePath(null));
    }

    [Fact]
    public void SetConfigContentThrowsOnNull()
    {
        Assert.Throws<ArgumentNullException>(() => LSL.SetConfigContent(null));
    }
}
