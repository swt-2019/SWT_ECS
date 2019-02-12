using System;
<<<<<<< HEAD

namespace ECS.Refactored
=======
namespace ECS.Refactored
>>>>>>> 89dc4175285b241ba9fa177cae2e6852edc9ed74
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