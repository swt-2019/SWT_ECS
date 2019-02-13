using ECS.Refactored;

namespace ECS.Test.Unit
{
    internal class HeaterFakeReturns : IHeater
    {
        public int TurnOnCalled { get; private set; } = 0;
        public int TurnOffCalled { get; private set; } = 0;
        public bool SelfTestResult { private get; set; } = false;

        public void TurnOn()
        {
            //If turned off called?
            TurnOnCalled++;
        }

        public void TurnOff()
        {
            //If turned on called?
            TurnOffCalled++;
        }

        public bool RunSelfTest() => SelfTestResult;
    }

    internal class TempSensorFake : ISensor
    {
        public bool SelfTestResult { private get; set; } = false;
        public int TemperatureResult { private get; set; } = 0;

        public int GetTemp()
        {
            return TemperatureResult;
        }

        public bool RunSelfTest()
        {
            return SelfTestResult;
        }
    }

}
