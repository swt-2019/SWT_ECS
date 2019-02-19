using ECS.Refactored;
using NSubstitute;
using NUnit.Framework;

namespace ECS.Test.Unit
{
    [TestFixture]
    public class NSubTests
    {
        private Refactored.ECS _ecsUUT;
        private IHeater _heater;
        private ISensor _sensor;
        private int _tresholdTest;

        [SetUp]
        public void ECS_Setup()
        {

            _tresholdTest = 30;
            _heater = Substitute.For<IHeater>();
            _sensor = Substitute.For<ISensor>();
            _ecsUUT = new Refactored.ECS(_tresholdTest, _sensor, _heater);
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
            _sensor.GetTemp().Returns(temperature);
            Assert.That(_ecsUUT.GetCurTemp(),Is.EqualTo(result));
        }
        
        [TestCase(5, 20,1)]
        [TestCase(10, 5, 0)]
        [TestCase(-10, -5,1)]
        [TestCase(110, 100,0)]
        public void ECS_Regulate_HeaterTurnOnCalledCorrectly(int temperature, int treshold, int result)
        {
            _sensor.GetTemp().Returns(temperature);
            _ecsUUT.SetThreshold(treshold);
            _ecsUUT.Regulate();
            _heater.Received(result).TurnOn();
        }
        
        
        [TestCase(5, 20, 0)]
        [TestCase(10, 5, 1)]
        [TestCase(-10, -5,0)]
        [TestCase(110, 100,1)]
        public void ECS_Regulate_HeaterTurnOffCalledCorrectly(int temperature, int treshold, int result)
        {
            _sensor.GetTemp().Returns(temperature);
            _ecsUUT.SetThreshold(treshold);
            _ecsUUT.Regulate();
            _heater.Received(result).TurnOff();
        }
        
        [TestCase(3)]
        [TestCase(1)]
        [TestCase(0)]
        [TestCase(10)]
        public void ECS_RegulateMultipleTimes_HeaterTurnOnMultipleTimes(int times)
        {
            _sensor.GetTemp().Returns(_tresholdTest-10);
            for (int i = 0; i < times; i++)
            {
                _ecsUUT.Regulate();
            }
            _heater.Received(times).TurnOn();
        }
        
        [TestCase(3)]
        [TestCase(1)]
        [TestCase(0)]
        [TestCase(10)]
        public void ECS_RegulateMultipleTimes_HeaterTurnOffMultipleTimes(int times)
        {
            _sensor.GetTemp().Returns(_tresholdTest+10);
            for (int i = 0; i < times; i++)
            {
                _ecsUUT.Regulate();
            }
            _heater.Received(times).TurnOff();
        }
        
        
        [Test]
        public void ECS_RunSelfTestsTrue_ECSSelfTestIsTrue()
        {
            _sensor.RunSelfTest().Returns(true);
            _heater.RunSelfTest().Returns(true);
            var result = _ecsUUT.RunSelfTest();

            Assert.That(result, Is.True);
        }
        
        
    }
}