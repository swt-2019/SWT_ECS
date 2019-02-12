using System;
using Refactored_solution;

namespace ECS.Legacy
{
    internal class TempSensor : ISensor
    {
        private Random gen = new Random();

        public int GetTemp()
        {
            return gen.Next(-5, 45);
        }

        public bool RunSelfTest()
        {
            return true;
        }
    }
}