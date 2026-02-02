using Xunit;

namespace SharpLSL.Test;

public class ResolverTest
{
    [Fact]
    public void TestDispose()
    {
        var resolver = new ContinuousResolver();
        Assert.False(resolver.IsInvalid);

        resolver.Dispose();
        Assert.True(resolver.IsInvalid);
    }
}

// References:
// https://github.com/dotnet/runtime/blob/main/src/libraries/System.Runtime/tests/System.Runtime.Tests/Microsoft/Win32/SafeHandles/SafeHandleZeroOrMinusOneIsInvalid.cs
