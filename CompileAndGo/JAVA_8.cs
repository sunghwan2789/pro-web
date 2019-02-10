using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.CompileAndGo
{
    public class JAVA_8 : ILanguageSdk, ICompiledLanguageSdk
    {
        public string ImageName => "pro/java-8";
        public string SourceFilename => "Program.java";
        public string CompileCommand => $"javac -encoding utf-8 {SourceFilename}";
        public string ExecuteCommand => "java Program";

        public async Task<string> ProcessCompileErrorAsync(StreamReader sr)
        {
            return await sr.ReadToEndAsync();
        }
    }
}
