ClangSharpPInvokeGenerator `
    --config multi-file generate-helper-types `
    --file common.h `
    --file-directory ./liblsl/include/lsl `
    --headerFile SharpLSL.Header.cs `
    --libraryPath lsl `
    --language c++ `
    --methodClassName Common `
    --namespace SharpLSL.Interop `
    --output ./SharpLSL/Interop

ClangSharpPInvokeGenerator `
    --config multi-file `
    --file inlet.h `
    --file-directory ./liblsl/include/lsl `
    --headerFile SharpLSL.Header.cs `
    --libraryPath lsl `
    --language c++ `
    --methodClassName Inlet `
    --namespace SharpLSL.Interop `
    --output ./SharpLSL/Interop `
    -r lsl_inlet=IntPtr lsl_streaminfo=IntPtr

ClangSharpPInvokeGenerator `
    --config multi-file `
    --file outlet.h `
    --file-directory ./liblsl/include/lsl `
    --headerFile SharpLSL.Header.cs `
    --libraryPath lsl `
    --language c++ `
    --methodClassName Outlet `
    --namespace SharpLSL.Interop `
    --output ./SharpLSL/Interop `
    -r lsl_outlet=IntPtr lsl_streaminfo=IntPtr

ClangSharpPInvokeGenerator `
    --config multi-file `
    --file resolver.h `
    --file-directory ./liblsl/include/lsl `
    --headerFile SharpLSL.Header.cs `
    --libraryPath lsl `
    --language c++ `
    --methodClassName Resolver `
    --namespace SharpLSL.Interop `
    --output ./SharpLSL/Interop `
    -r lsl_continuous_resolver=IntPtr lsl_streaminfo=IntPtr

ClangSharpPInvokeGenerator `
    --config multi-file `
    --file streaminfo.h `
    --file-directory ./liblsl/include/lsl `
    --headerFile SharpLSL.Header.cs `
    --libraryPath lsl `
    --language c++ `
    --methodClassName StreamInfo `
    --namespace SharpLSL.Interop `
    --output ./SharpLSL/Interop `
    -r lsl_streaminfo=IntPtr lsl_xml_ptr=IntPtr

ClangSharpPInvokeGenerator `
    --config multi-file `
    --file xml.h `
    --file-directory ./liblsl/include/lsl `
    --headerFile SharpLSL.Header.cs `
    --libraryPath lsl `
    --language c++ `
    --methodClassName Xml `
    --namespace SharpLSL.Interop `
    --output ./SharpLSL/Interop `
    -r lsl_xml_ptr=IntPtr

# References:
# [Generating .NET interop bindings via ClangSharp](https://sharovarskyi.com/blog/posts/clangsharp-dotnet-interop-bindings/)
# [How to split long commands over multiple lines in PowerShell](https://stackoverflow.com/questions/2608144/how-to-split-long-commands-over-multiple-lines-in-powershell)
