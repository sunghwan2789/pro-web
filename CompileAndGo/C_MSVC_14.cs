using pro_web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.CompileAndGo
{
    public class C_MSVC_14 : ILanguageSdk, ICompiledLanguageSdk
    {
        public string ImageName => "pro/msvc-14";
        public string SourceFilename => "Program.c";
        public string CompileCommand => $"cl /nologo /O2 /Za /utf-8 /F 67108864 /TC {SourceFilename}";
        public string ExecuteCommand => "Program.exe";

        public async Task<string> ProcessCompileErrorAsync(StreamReader sr)
        {
            await sr.ReadLineAsync();
            return await sr.ReadToEndAsync();
        }
    }
}
