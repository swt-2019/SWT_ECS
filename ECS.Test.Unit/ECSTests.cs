using ECS.Refactored;
using NUnit.Framework;

namespace ECS.Test.Unit
{
    [TestFixture]
    public class ECSTests
    {
        private Refactored.ECS _ecsUUT;
        private HeaterFakeReturns _heaterFake;
        private TempSensorFake _sensorFake;
        private int _tresholdTest;

        [SetUp]
        public void ECS_Setup()
        {
            _tresholdTest = 0;
            _heaterFake =new HeaterFakeReturns();
            _sensorFake = new TempSensorFake();
            _ecsUUT = new Refactored.ECS(_tresholdTest, _sensorFake, _heaterFake);
        }

        [TestCase(20,20)]
        [TestCase(0, 0)]
        [TestCase(2, 2)]
        [TestCase(-20,-20)]
        public void ECS_SetThreshold_ThresholdCorrect(int treshold,int result)
        {
            _ecsUUT.SetThreshold(treshold);
            Assert.That(_ecsUUT.GetThreshold(),Is.EqualTo(result));
        }

        [TestCase(20,20)]
        [TestCase(5,5)]
        [TestCase(-5,-5)]
        [TestCase(100,100)]
        public void ECS_SetCurrentTemperature_TemperatureCorrect(int temperature, int result)
        {
            _sensorFake.TemperatureResult = temperature;
            Assert.That(_ecsUUT.GetCurTemp(),Is.EqualTo(result));
        }
        
        [TestCase(5, 20,1)]
        [TestCase(10, 5, 0)]
        [TestCase(-10, -5,1)]
        [TestCase(110, 100,0)]
        public void ECS_Regulate_HeaterTurnOnCalledCorrectly(int temperature, int treshold, int result)
        {
            _ecsUUT.SetThreshold(treshold);
            _sensorFake.TemperatureResult = temperature;
            _ecsUUT.Regulate();
            Assert.That(_heaterFake.TurnOnCalled, Is.EqualTo(result));
        }

        
        [TestCase(5, 20, 0)]
        [TestCase(10, 5, 1)]
        [TestCase(-10, -5,0)]
        [TestCase(110, 100,1)]
        public void ECS_Regulate_HeaterTurnOffCalledCorrectly(int temperature, int treshold, int result)
        {
            _ecsUUT.SetThreshold(treshold);
            _sensorFake.TemperatureResult = temperature;
            _ecsUUT.Regulate();
            Assert.That(_heaterFake.TurnOffCalled, Is.EqualTo(result));
        }

        [TestCase(3)]
        [TestCase(1)]
        [TestCase(0)]
        [TestCase(10)]
        public void ECS_RegulateMultipleTimes_HeaterTurnOnMultipleTimes(int times)
        {
            _ecsUUT.SetThreshold(20);
            _sensorFake.TemperatureResult = 15;
            for (int i = 0; i < times; i++)
            {
                _ecsUUT.Regulate();
            }
            Assert.That(_heaterFake.TurnOnCalled, Is.EqualTo(times));
        }

        [TestCase(3)]
        [TestCase(1)]
        [TestCase(0)]
        [TestCase(10)]
        public void ECS_RegulateMultipleTimes_HeaterTurnOffMultipleTimes(int times)
        {
            _ecsUUT.SetThreshold(10);
            _sensorFake.TemperatureResult = 15;
            for (int i = 0; i < times; i++)
            {
                _ecsUUT.Regulate();
            }
            Assert.That(_heaterFake.TurnOffCalled, Is.EqualTo(times));
        }

        
        [Test]
        public void ECS_RunSelfTestsTrue_ECSSelfTestIsTrue()
        {
            _ecsUUT.SetThreshold(20);
            _sensorFake.SelfTestResult = true;
            _heaterFake.SelfTestResult = true;
            var result = _ecsUUT.RunSelfTest();

            Assert.That(result, Is.True);
        }
    }
}
