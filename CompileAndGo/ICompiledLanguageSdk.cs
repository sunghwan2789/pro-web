using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.CompileAndGo
{
    interface ICompiledLanguageSdk
    {
        string CompileCommand { get; }
        Task<string> ProcessCompileErrorAsync(StreamReader streamReader);
    }
}
