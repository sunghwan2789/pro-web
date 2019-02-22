using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.CompileAndGo
{
    public class PYTHON_3 : ILanguageSdk, ICompiledLanguageSdk
    {
        public string ImageName => "pro/python-3";
        public string SourceFilename => "Main.py";
        public string CompileCommand => $@"python -c ""import py_compile; py_compile.compile(r'{SourceFilename}')""";
        public string ExecuteCommand => $"python {SourceFilename}";

        public Task<string> ProcessCompileErrorAsync(StreamReader sr)
        {
            return sr.ReadToEndAsync();
        }
    }
}
