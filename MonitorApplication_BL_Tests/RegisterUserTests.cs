using NUnit.Framework;
using System;

namespace MonitorApplication_BL_Tests
{
    [TestFixture]
    public class RegisterUserTests
    {
        [SetUp]
        public void SpecSetUp()
        {
            Console.WriteLine("setUp is run here --> the test runs here [foo]");
        }

        [Test]
        public void AreEqual()
        {
            Console.WriteLine("--> the test runs here [foo]");
            Assert.AreEqual(1, 1);
        }

    }
}
