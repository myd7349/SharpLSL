<p align="center">
  <a href="https://github.com/SharpLSL/SharpLSL">
    <img src="https://raw.githubusercontent.com/SharpLSL/SharpLSL/main/docs/images/icon_medium.png" alt="SharpLSL icon" width="256" />
  </a>
</p>

# SharpLSL

[![NuGet](https://img.shields.io/nuget/v/SharpLSL.svg)](https://www.nuget.org/packages/SharpLSL/) [![Downloads](https://img.shields.io/nuget/dt/SharpLSL)](https://www.nuget.org/packages/SharpLSL) [![Build](https://github.com/SharpLSL/SharpLSL/actions/workflows/build.yml/badge.svg)](https://github.com/SharpLSL/SharpLSL/actions) ![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/SharpLSL/SharpLSL) ![GitHub repo size](https://img.shields.io/github/repo-size/SharpLSL/SharpLSL) [![License](https://img.shields.io/github/license/SharpLSL/SharpLSL)](https://github.com/SharpLSL/SharpLSL/blob/main/LICENSE)

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

In addition to installing SharpLSL, you will need to install the appropriate native liblsl binary package(s) for your target platform(s). SharpLSL offers separate NuGet packages for these binaries, named `SharpLSL.Native.[RID]`, where `[RID]` represents the runtime identifier for the specific platform. For details on runtime identifiers, refer to the [RID catalog](https://learn.microsoft.com/en-us/dotnet/core/rid-catalog). You can find the list of available native packages and their platform mappings in the [SharpLSL.Native README](https://github.com/SharpLSL/SharpLSL/blob/main/src/SharpLSL.Native/README.md).

If your application is cross-platform, you can opt for the `SharpLSL.Native.all` package. This meta-package references all the other platform-specific liblsl packages, making it easier to manage dependencies for different operating systems.

# Related Projects

| Project  | Stars |  Description | License |
|--- | ---| ---|--- |
| [BlueMuse](https://github.com/kowalej/BlueMuse)  | [![GitHub stars](https://img.shields.io/github/stars/kowalej/BlueMuse?style=social)](https://github.com/kowalej/BlueMuse/stargazers) | Windows 10 app to stream data from Muse EEG headsets via LSL. | [![GPL-3.0-badge]][GPL-3.0-link] |
| [labstreaminglayer](https://github.com/sccn/labstreaminglayer)  | [![GitHub stars](https://img.shields.io/github/stars/sccn/labstreaminglayer?style=social)](https://github.com/sccn/labstreaminglayer/stargazers) | LabStreamingLayer super repository comprising submodules for LSL and associated apps. | [![MIT-badge]][MIT-link] |
| [LibLSL](https://github.com/Diademics-Pty-Ltd/LibLSL)  | [![GitHub stars](https://img.shields.io/github/stars/Diademics-Pty-Ltd/LibLSL?style=social)](https://github.com/Diademics-Pty-Ltd/LibLSL/stargazers) | Modern C# wrapper for the LabStreamingLayer (LSL). | [![MIT-badge]][MIT-link] |
| [liblsl](https://github.com/sccn/liblsl)  | [![GitHub stars](https://img.shields.io/github/stars/sccn/liblsl?style=social)](https://github.com/sccn/liblsl/stargazers) | C++ lsl library. | [![MIT-badge]][MIT-link] |
| [liblsl-Csharp](https://github.com/labstreaminglayer/liblsl-Csharp)  | [![GitHub stars](https://img.shields.io/github/stars/labstreaminglayer/liblsl-Csharp?style=social)](https://github.com/labstreaminglayer/liblsl-Csharp/stargazers) | C# bindings for liblsl. | [![MIT-badge]][MIT-link] |
| [lsl](https://github.com/emotional-cities/lsl)  | [![GitHub stars](https://img.shields.io/github/stars/emotional-cities/lsl?style=social)](https://github.com/emotional-cities/lsl/stargazers) | Bonsai library containing interfaces for streaming data from devices implementing the LSL protocol. | [![MIT-badge]][MIT-link] |
| [LSL4Unity](https://github.com/labstreaminglayer/LSL4Unity)  | [![GitHub stars](https://img.shields.io/github/stars/labstreaminglayer/LSL4Unity?style=social)](https://github.com/labstreaminglayer/LSL4Unity/stargazers) | A integration approach of the LSL framework for Unity3D. | [![MIT-badge]][MIT-link] |
| [lsl_archived](https://github.com/sccn/lsl_archived)  | [![GitHub stars](https://img.shields.io/github/stars/sccn/lsl_archived?style=social)](https://github.com/sccn/lsl_archived/stargazers) | Archived lsl repository. | |
| [lsl_in_unity](https://github.com/mvidaldp/lsl_in_unity)  | [![GitHub stars](https://img.shields.io/github/stars/mvidaldp/lsl_in_unity?style=social)](https://github.com/mvidaldp/lsl_in_unity/stargazers) | Simple Unity 2D project with changing background color and click audio playing using LabStreamingLayer. | [![MIT-badge]][MIT-link] |

[GPL-3.0-badge]: https://img.shields.io/badge/License-GPL%20v3-blue.svg
[GPL-3.0-link]: https://www.gnu.org/licenses/gpl-3.0.en.html
[MIT-badge]: https://img.shields.io/badge/License-MIT-blue.svg
[MIT-link]: https://opensource.org/licenses/MIT

# License

MIT
