# common.h: functions & macros & enums
ClangSharpPInvokeGenerator `
    --config generate-helper-types generate-macro-bindings log-visited-files multi-file windows-types `
    --file lsl_c.h `
    --file-directory ./liblsl/include `
    --headerFile SharpLSL.Header.cs `
    --traverse ./liblsl/include/lsl/common.h `
    --libraryPath lsl `
    --language c++ `
    --methodClassName Common `
    --namespace SharpLSL.Interop `
    --additional -m64 `
    --output ../src/SharpLSL/Interop

Move-Item -Path ../src/SharpLSL/Interop/Common.cs -Destination ../src/SharpLSL/Interop/Common.g.cs -Force
Move-Item -Path ../src/SharpLSL/Interop/lsl_channel_format_t.cs -Destination ../src/SharpLSL/Interop/lsl_channel_format_t.g.cs -Force
Move-Item -Path ../src/SharpLSL/Interop/lsl_error_code_t.cs -Destination ../src/SharpLSL/Interop/lsl_error_code_t.g.cs -Force
Move-Item -Path ../src/SharpLSL/Interop/lsl_processing_options_t.cs -Destination ../src/SharpLSL/Interop/lsl_processing_options_t.g.cs -Force
Move-Item -Path ../src/SharpLSL/Interop/lsl_transport_options_t.cs -Destination ../src/SharpLSL/Interop/lsl_transport_options_t.g.cs -Force
Move-Item -Path ../src/SharpLSL/Interop/NativeAnnotationAttribute.cs -Destination ../src/SharpLSL/Interop/NativeAnnotationAttribute.g.cs -Force
Move-Item -Path ../src/SharpLSL/Interop/NativeTypeNameAttribute.cs -Destination ../src/SharpLSL/Interop/NativeTypeNameAttribute.g.cs -Force

# common.h: regenerate functions & macros
ClangSharpPInvokeGenerator `
    --config generate-macro-bindings log-exclusions log-visited-files windows-types `
    --file lsl_c.h `
    --file-directory ./liblsl/include `
    --headerFile SharpLSL.Header.cs `
    --traverse ./liblsl/include/lsl/common.h `
    --libraryPath lsl `
    --language c++ `
    --methodClassName LSL `
    --namespace SharpLSL.Interop `
    --additional -m64 `
    --output ../src/SharpLSL/Interop/Common.g.cs `
    --remap `
    sbyte*=IntPtr `
    --exclude `
    lsl_channel_format_t `
    lsl_processing_options_t `
    lsl_error_code_t `
    lsl_transport_options_t

# inlet.h
ClangSharpPInvokeGenerator `
    --config generate-macro-bindings log-visited-files windows-types `
    --file lsl_c.h `
    --file-directory ./liblsl/include `
    --headerFile SharpLSL.Header.cs `
    --traverse ./liblsl/include/lsl/inlet.h `
    --libraryPath lsl `
    --language c++ `
    --methodClassName LSL `
    --namespace SharpLSL.Interop `
    --additional -m64 `
    --output ../src/SharpLSL/Interop/Inlet.g.cs `
    --remap `
    lsl_inlet=IntPtr `
    lsl_streaminfo=IntPtr

# outlet.h
ClangSharpPInvokeGenerator `
    --config generate-macro-bindings log-visited-files windows-types `
    --file lsl_c.h `
    --file-directory ./liblsl/include `
    --headerFile SharpLSL.Header.cs `
    --traverse ./liblsl/include/lsl/outlet.h `
    --libraryPath lsl `
    --language c++ `
    --methodClassName LSL `
    --namespace SharpLSL.Interop `
    --additional -m64 `
    --output ../src/SharpLSL/Interop/Outlet.g.cs `
    --remap `
    lsl_outlet=IntPtr `
    lsl_streaminfo=IntPtr

# resolver.h
ClangSharpPInvokeGenerator `
    --config generate-macro-bindings log-visited-files windows-types `
    --file lsl_c.h `
    --file-directory ./liblsl/include `
    --headerFile SharpLSL.Header.cs `
    --traverse ./liblsl/include/lsl/resolver.h `
    --libraryPath lsl `
    --language c++ `
    --methodClassName LSL `
    --namespace SharpLSL.Interop `
    --additional -m64 `
    --output ../src/SharpLSL/Interop/Resolver.g.cs `
    --remap `
    lsl_continuous_resolver=IntPtr `
    lsl_streaminfo=IntPtr

# streaminfo.h
ClangSharpPInvokeGenerator `
    --config generate-macro-bindings log-visited-files windows-types `
    --file lsl_c.h `
    --file-directory ./liblsl/include `
    --headerFile SharpLSL.Header.cs `
    --traverse ./liblsl/include/lsl/streaminfo.h `
    --libraryPath lsl `
    --language c++ `
    --methodClassName LSL `
    --namespace SharpLSL.Interop `
    --additional -m64 `
    --output ../src/SharpLSL/Interop/StreamInfo.g.cs `
    --remap `
    lsl_streaminfo=IntPtr `
    lsl_xml_ptr=IntPtr sbyte*=IntPtr

# xml.h
ClangSharpPInvokeGenerator `
    --config generate-macro-bindings log-visited-files windows-types `
    --file lsl_c.h `
    --file-directory ./liblsl/include `
    --headerFile SharpLSL.Header.cs `
    --traverse ./liblsl/include/lsl/xml.h `
    --libraryPath lsl `
    --language c++ `
    --methodClassName LSL `
    --namespace SharpLSL.Interop `
    --additional -m64 `
    --output ../src/SharpLSL/Interop/XML.g.cs `
    --remap `
    lsl_xml_ptr=IntPtr


# References:
# [Generating .NET interop bindings via ClangSharp](https://sharovarskyi.com/blog/posts/clangsharp-dotnet-interop-bindings/)
# [How to split long commands over multiple lines in PowerShell](https://stackoverflow.com/questions/2608144/how-to-split-long-commands-over-multiple-lines-in-powershell)
# [Structure fields with char* or char[x] do not get translated to string](https://github.com/dotnet/ClangSharp/issues/250)
# [Unclear how --traverse is meant to work](https://github.com/dotnet/ClangSharp/issues/432)
# [Why can't I return a char* string from C++ to C# in a Release build?](https://stackoverflow.com/questions/6300093/why-cant-i-return-a-char-string-from-c-to-c-sharp-in-a-release-build)
# [Returning a string from PInvoke?](https://stackoverflow.com/questions/5298268/returning-a-string-from-pinvoke)
# [Annotating primitive mappings?](https://github.com/dotnet/ClangSharp/issues/428)
# [Support for new LibraryImport attribute](https://github.com/dotnet/ClangSharp/issues/427)
# [What's the best way to determine the location of the current PowerShell script?](https://stackoverflow.com/questions/5466329/whats-the-best-way-to-determine-the-location-of-the-current-powershell-script)
# [Spaces cause split in path with PowerShell](https://stackoverflow.com/questions/18537098/spaces-cause-split-in-path-with-powershell)
# [How to add a suffix to all the files](https://stackoverflow.com/questions/57041671/how-to-add-a-suffix-to-all-the-files)
# [rename-item and override if filename exists](https://stackoverflow.com/questions/32311875/rename-item-and-override-if-filename-exists)
