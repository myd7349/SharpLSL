#if NET35
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace SharpLSL.Interop
{
    public static partial class LSL
    {
        static LSL()
        {
            var searchPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //if (Environment.Is64BitProcess)
            if (IntPtr.Size == 8)
                searchPath = Path.Combine(searchPath, "x64");
            else
                searchPath = Path.Combine(searchPath, "x86");

            if (Directory.Exists(searchPath))
            {
                Trace.WriteLine($"Search LSL dynamic library in {searchPath}.");
                Kernel32.SetDllDirectory(searchPath);
            }
        }
    }
}
#endif


// References:
// [C# preprocessor directives](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/preprocessor-directives)
