using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Photobook.Models;

namespace UnitTest
{
    [TestFixture]
    class IT_ServerCommunicatorToAPITest
    {
        private ServerCommunicator uut;

        [SetUp]
        public void Setup()
        {
            uut = new ServerCommunicator();
        }
        [Test]
        public async Task LoginAsExistingHostSucceeds()
        {
            Host testHost = new Host
            {
                Email = "v@m.dk",
                Password = "123456"
            };

            Assert.That(await uut.SendDataReturnIsValid(testHost, DataType.Host), Is.True);
        }
        [Test]
        public async Task LoginAsNonExistingHostFails()
        {

            Host testHost = new Host
            {
                Email = "",
                Password = ""
            };

            Assert.That(await uut.SendDataReturnIsValid(testHost, DataType.Host), Is.False);
        }

        [Test]
        public async Task LoginAsGuestAtExistingEventSucceeds()
        {
            Guest testGuest = new Guest
            {
                Pin = "1234",
                Username = $"appTest{new Random().Next(1000, 10000).ToString()}"
            };

            Assert.That(await uut.SendDataReturnIsValid(testGuest, DataType.Guest), Is.True);
        }

        [Test]
        public async Task LoginAsGuestAtNonExistingEventFails()
        {
            Guest testGuest = new Guest();

            Assert.That(await uut.SendDataReturnIsValid(testGuest, DataType.Guest), Is.False);
        }
    }
}
