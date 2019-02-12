using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ECS.Test.Unit
{
    public class HeaterFakeReturns : IHeater
    {
        
    }

    public class TempSensorFake : ISensor
    {

    }

    [TestFixture]
    public class ECSTests
    {
        [SetUp]
        public void ECS_Setup()
        {

        }
    }
}
