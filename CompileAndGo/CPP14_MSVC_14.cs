using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.CompileAndGo
{
    public class CPP14_MSVC_14 : ILanguageSdk, ICompiledLanguageSdk
    {
        public string ImageName => "pro/msvc-14";
        public string SourceFilename => "Main.cpp";
        public string CompileCommand => $"cl /nologo /O2 /Za /utf-8 /F 67108864 /std:c++14 /EHsc /TP {SourceFilename}";
        public string ExecuteCommand => "Main.exe";

        public async Task<string> ProcessCompileErrorAsync(StreamReader sr)
        {
            await sr.ReadLineAsync();
            return await sr.ReadToEndAsync();
        }
    }
}
