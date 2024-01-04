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
    --namespace SharpLSL `
    --output ./SharpLSL `
    --remap `
    lsl_channel_format_t=LslChannelFormat `
    lsl_error_code_t=LslErrorCode `
    lsl_processing_options_t=LslProcessingOptions `
    lsl_transport_options_t=LslTransportOptions


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
    --output ./SharpLSL/Interop/Common.cs `
    --exclude `
    lsl_channel_format_t `
    lsl_processing_options_t `
    lsl_error_code_t `
    lsl_transport_options_t `
    --remap `
    lsl_channel_format_t=LslChannelFormat `
    lsl_error_code_t=LslErrorCode `
    lsl_processing_options_t=LslProcessingOptions `
    lsl_transport_options_t=LslTransportOptions

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
    --output ./SharpLSL/Interop/Inlet.cs `
    --remap lsl_inlet=IntPtr lsl_streaminfo=IntPtr

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
    --output ./SharpLSL/Interop/Outlet.cs `
    --remap lsl_outlet=IntPtr lsl_streaminfo=IntPtr

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
    --output ./SharpLSL/Interop/Resolver.cs `
    --remap lsl_continuous_resolver=IntPtr lsl_streaminfo=IntPtr

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
    --output ./SharpLSL/Interop/StreamInfo.cs `
    --remap `
    lsl_streaminfo=IntPtr `
    lsl_xml_ptr=IntPtr `
    const char *=

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
    --output ./SharpLSL/Interop/XML.cs `
    --remap lsl_xml_ptr=IntPtr


# References:
# [Generating .NET interop bindings via ClangSharp](https://sharovarskyi.com/blog/posts/clangsharp-dotnet-interop-bindings/)
# [How to split long commands over multiple lines in PowerShell](https://stackoverflow.com/questions/2608144/how-to-split-long-commands-over-multiple-lines-in-powershell)
# [Structure fields with char* or char[x] do not get translated to string](https://github.com/dotnet/ClangSharp/issues/250)
# [Unclear how --traverse is meant to work](https://github.com/dotnet/ClangSharp/issues/432)
# [Why can't I return a char* string from C++ to C# in a Release build?](https://stackoverflow.com/questions/6300093/why-cant-i-return-a-char-string-from-c-to-c-sharp-in-a-release-build)
# [Returning a string from PInvoke?](https://stackoverflow.com/questions/5298268/returning-a-string-from-pinvoke)
# [Annotating primitive mappings?](https://github.com/dotnet/ClangSharp/issues/428)
