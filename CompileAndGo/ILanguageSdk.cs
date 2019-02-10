using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.CompileAndGo
{
    interface ILanguageSdk
    {
        string ImageName { get; }
        string SourceFilename { get; }
        string ExecuteCommand { get; }
    }
}
