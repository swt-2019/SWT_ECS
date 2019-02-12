using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ECS.Refactored;

namespace ECS.Test.Unit
{
    public class HeaterFakeReturns : IHeater
    {
        public bool TurnOnCalled { get; private set; } = false;
        public bool TurnOffCalled { get; private set; } = false;
        public void TurnOn()
        {
            TurnOnCalled = true;
        }

        public void TurnOff()
        {
            TurnOffCalled = true;
        }
    }

    public class TempSensorFake : ISensor
    {
        public int GetTemp()
        {
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public class ECSTests
    {
        private Refactored.ECS ECSUnderTest;
        [SetUp]
        public void ECS_Setup()
        {
            ECSUnderTest = new Refactored.ECS(new HeaterFakeReturns(), new TempSensorFake());
        }
    }
}
