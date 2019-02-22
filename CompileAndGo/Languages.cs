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
        [Display(Name = "C++14 (MSVC 14)", ShortName = ".cpp")]
        CPP14_MSVC_14,
        [Display(Name = "Java 8", ShortName = ".java")]
        JAVA_8,
        [Display(Name = "Python 2", ShortName = ".py")]
        PYTHON_2,
        [Display(Name = "Python 3", ShortName = ".py")]
        PYTHON_3,
    }
}
