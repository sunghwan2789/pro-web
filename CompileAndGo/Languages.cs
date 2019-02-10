using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.CompileAndGo
{
    public enum Languages
    {
        [Display(Name = "C (MSVC 14)", ShortName = ".c")]
        C_MSVC_14,
    }
}
