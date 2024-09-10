# SharpLSL Native Assets Packages

The following NuGet packages contain pre-compiled binaries of [liblsl](https://github.com/sccn/liblsl) for various platforms:

| RID           |                        NuGet Package                         |
| :------------ | :----------------------------------------------------------: |
| *             |  [![SharpLSL.Native.all-badge]][SharpLSL.Native.all-nuget]   |
| android-arm   |  [![SharpLSL.Native.android-arm-badge]][SharpLSL.Native.android-arm-nuget]   |
| android-arm64 |  [![SharpLSL.Native.android-arm64-badge]][SharpLSL.Native.android-arm64-nuget]   |
| android-x64   |  [![SharpLSL.Native.android-x64-badge]][SharpLSL.Native.android-x64-nuget]   |
| android-x86   |  [![SharpLSL.Native.android-x86-badge]][SharpLSL.Native.android-x86-nuget]   |
| ios-arm64     |  [![SharpLSL.Native.ios-arm64-badge]][SharpLSL.Native.ios-arm64-nuget]   |
| linux-arm     |  [![SharpLSL.Native.linux-arm-badge]][SharpLSL.Native.linux-arm-nuget]   |
| linux-arm64   |  [![SharpLSL.Native.linux-arm64-badge]][SharpLSL.Native.linux-arm64-nuget]   |
| linux-x64     |  [![SharpLSL.Native.linux-x64-badge]][SharpLSL.Native.linux-x64-nuget]   |
| osx           |  [![SharpLSL.Native.osx-badge]][SharpLSL.Native.osx-nuget]   |
| osx-arm64     |  [![SharpLSL.Native.osx-arm64-badge]][SharpLSL.Native.osx-arm64-nuget]   |
| osx-x64       |  [![SharpLSL.Native.osx-x64-badge]][SharpLSL.Native.osx-x64-nuget]   |
| win-arm64     |  [![SharpLSL.Native.win-arm64-badge]][SharpLSL.Native.win-arm64-nuget]   |
| win-x64       | [![SharpLSL.Native.win-x64-badge]][SharpLSL.Native.win-x64-nuget] |
| win-x86       | [![SharpLSL.Native.win-x86-badge]][SharpLSL.Native.win-x86-nuget] |

[SharpLSL.Native.all-badge]: https://img.shields.io/nuget/v/SharpLSL.Native.all.svg
[SharpLSL.Native.all-nuget]: https://www.nuget.org/packages/SharpLSL.Native.all
[SharpLSL.Native.android-arm-badge]: https://img.shields.io/nuget/v/SharpLSL.Native.android-arm.svg
[SharpLSL.Native.android-arm-nuget]: https://www.nuget.org/packages/SharpLSL.Native.android-arm
[SharpLSL.Native.android-arm64-badge]: https://img.shields.io/nuget/v/SharpLSL.Native.android-arm64.svg
[SharpLSL.Native.android-arm64-nuget]: https://www.nuget.org/packages/SharpLSL.Native.android-arm64
[SharpLSL.Native.android-x64-badge]: https://img.shields.io/nuget/v/SharpLSL.Native.android-x64.svg
[SharpLSL.Native.android-x64-nuget]: https://www.nuget.org/packages/SharpLSL.Native.android-x64
[SharpLSL.Native.android-x86-badge]: https://img.shields.io/nuget/v/SharpLSL.Native.android-x86.svg
[SharpLSL.Native.android-x86-nuget]: https://www.nuget.org/packages/SharpLSL.Native.android-x86
[SharpLSL.Native.ios-arm64-badge]: https://img.shields.io/nuget/v/SharpLSL.Native.ios-arm64.svg
[SharpLSL.Native.ios-arm64-nuget]: https://www.nuget.org/packages/SharpLSL.Native.ios-arm64
[SharpLSL.Native.linux-arm-badge]: https://img.shields.io/nuget/v/SharpLSL.Native.linux-arm.svg
[SharpLSL.Native.linux-arm-nuget]: https://www.nuget.org/packages/SharpLSL.Native.linux-arm
[SharpLSL.Native.linux-arm64-badge]: https://img.shields.io/nuget/v/SharpLSL.Native.linux-arm64.svg
[SharpLSL.Native.linux-arm64-nuget]: https://www.nuget.org/packages/SharpLSL.Native.linux-arm64
[SharpLSL.Native.linux-x64-badge]: https://img.shields.io/nuget/v/SharpLSL.Native.linux-x64.svg
[SharpLSL.Native.linux-x64-nuget]: https://www.nuget.org/packages/SharpLSL.Native.linux-x64
[SharpLSL.Native.osx-badge]: https://img.shields.io/nuget/v/SharpLSL.Native.osx.svg
[SharpLSL.Native.osx-nuget]: https://www.nuget.org/packages/SharpLSL.Native.osx
[SharpLSL.Native.osx-arm64-badge]: https://img.shields.io/nuget/v/SharpLSL.Native.osx-arm64.svg
[SharpLSL.Native.osx-arm64-nuget]: https://www.nuget.org/packages/SharpLSL.Native.osx-arm64
[SharpLSL.Native.osx-x64-badge]: https://img.shields.io/nuget/v/SharpLSL.Native.osx-x64.svg
[SharpLSL.Native.osx-x64-nuget]: https://www.nuget.org/packages/SharpLSL.Native.osx-x64
[SharpLSL.Native.win-arm64-badge]: https://img.shields.io/nuget/v/SharpLSL.Native.win-arm64.svg
[SharpLSL.Native.win-arm64-nuget]: https://www.nuget.org/packages/SharpLSL.Native.win-arm64
[SharpLSL.Native.win-x64-badge]: https://img.shields.io/nuget/v/SharpLSL.Native.win-x64.svg
[SharpLSL.Native.win-x64-nuget]: https://www.nuget.org/packages/SharpLSL.Native.win-x64
[SharpLSL.Native.win-x86-badge]: https://img.shields.io/nuget/v/SharpLSL.Native.win-x86.svg
[SharpLSL.Native.win-x86-nuget]: https://www.nuget.org/packages/SharpLSL.Native.win-x86

During the packaging process, liblsl binary archives are automatically downloaded from the [liblsl-ci-build](https://github.com/myd7349/liblsl-ci-build) release page and then packed into the corresponding NuGet packages named `SharpLSL.Native.[rid]`.

# License

These packages only bundle the dynamic libraries of [liblsl](https://github.com/sccn/liblsl), thus using the same licensing agreement as liblsl. Please refer to the [LICENSE](./LICENSE) file for more information.
