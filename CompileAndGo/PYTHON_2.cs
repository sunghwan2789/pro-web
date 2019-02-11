using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.CompileAndGo
{
    public class PYTHON_2 : ILanguageSdk, ICompiledLanguageSdk
    {
        public string ImageName => "pro/python-2";
        public string SourceFilename => "Main.py";
        public string CompileCommand => $@"python -c ""import py_compile; py_compile.compile(r'{SourceFilename}')""";
        public string ExecuteCommand => $"python {SourceFilename}";

        public async Task<string> ProcessCompileErrorAsync(StreamReader sr)
        {
            return await sr.ReadToEndAsync();
        }
    }
}
