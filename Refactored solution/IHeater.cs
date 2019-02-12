﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Refactored
{
    public interface IHeater
    {
        void TurnOn();


        void TurnOff();


        bool RunSelfTest();

    }
}
