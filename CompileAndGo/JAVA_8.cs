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
        public string SourceFilename => "Main.java";
        public string CompileCommand => $"javac -encoding UTF-8 {SourceFilename}";
        public string ExecuteCommand => "java -Dfile.encoding=UTF-8 -XX:+UseSerialGC -Xss64m -Xms1920m -Xmx1920m Main";

        public async Task<string> ProcessCompileErrorAsync(StreamReader sr)
        {
            return await sr.ReadToEndAsync();
        }
    }
}
