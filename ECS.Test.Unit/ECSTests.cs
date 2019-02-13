using ECS.Refactored;
using NUnit.Framework;

namespace ECS.Test.Unit
{
    [TestFixture]
    public class ECSTests
    {
        private Refactored.ECS _ecsUUT;

        [SetUp]
        public void ECS_Setup()
        {
            _ecsUUT = new Refactored.ECS(20, new TempSensorFake(), new HeaterFakeReturns());
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
    }
}
