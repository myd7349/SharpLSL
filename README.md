# [SharpLSL](https://github.com/myd7349/SharpLSL)

[![NuGet](https://img.shields.io/nuget/v/SharpLSL.svg)](https://www.nuget.org/packages/SharpLSL/) [![Downloads](https://img.shields.io/nuget/dt/SharpLSL)](https://www.nuget.org/packages/SharpLSL) [![Build](https://github.com/myd7349/SharpLSL/actions/workflows/build.yml/badge.svg)](https://github.com/myd7349/SharpLSL/actions) ![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/myd7349/SharpLSL) ![GitHub repo size](https://img.shields.io/github/repo-size/myd7349/SharpLSL) [![License](https://img.shields.io/github/license/myd7349/SharpLSL)](https://github.com/myd7349/SharpLSL/blob/main/LICENSE)

SharpLSL is a cross-platform C# binding of [**L**ab **S**treaming **L**ayer](https://github.com/sccn/labstreaminglayer).

# Using SharpLSL

SharpLSL is available as a convenient NuGet package. You can install SharpLSL using any of the following methods:

.NET CLI:

```
dotnet add package SharpLSL --version <version>
```

NuGet package manager:

```
Install-Package SharpLSL -Version <version>
```

PackageReference:

```
<PackageReference Include="SharpLSL" Version="<version>" />
```

Replace `<version>` with the specific version number of SharpLSL you wish to use.

In addition to installing SharpLSL, you will need to install the appropriate native liblsl binary package(s) for your target platform(s). SharpLSL offers separate NuGet packages for these binaries, named `SharpLSL.Native.[rid]`, where `[rid]` represents the runtime identifier for the specific platform. For details on runtime identifiers, refer to the [RID catalog](https://learn.microsoft.com/en-us/dotnet/core/rid-catalog). You can find the list of available native packages and their platform mappings in the [SharpLSL.Natives README](https://github.com/myd7349/SharpLSL/blob/main/Source/SharpLSL.Natives/README.md).

If your application is cross-platform, you can opt for the `SharpLSL.Native.all` package. This meta-package references all the other platform-specific liblsl packages, making it easier to manage dependencies for different operating systems.

# Related Projects

- [BlueMuse](https://github.com/kowalej/BlueMuse)
- [labstreaminglayer](https://github.com/sccn/labstreaminglayer)
- [LibLSL](https://github.com/Diademics-Pty-Ltd/LibLSL)
- [liblsl](https://github.com/sccn/liblsl)
- [liblsl-Csharp](https://github.com/labstreaminglayer/liblsl-Csharp)
- [lsl](https://github.com/emotional-cities/lsl)
- [LSL4Unity](https://github.com/labstreaminglayer/LSL4Unity)
- [lsl_archived](https://github.com/sccn/lsl_archived)
- [lsl_in_unity](https://github.com/mvidaldp/lsl_in_unity)

# License

MIT
