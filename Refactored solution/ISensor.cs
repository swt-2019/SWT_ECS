﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactored_solution
{
    interface ISensor
    {
        int GetTemp();
        bool RunSelfTest();
    }
}
