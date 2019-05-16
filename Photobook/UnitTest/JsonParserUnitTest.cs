using System;
using NUnit.Framework;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Execution;
using PB.Dto;
using Photobook.Models;

namespace UnitTest
{
    [TestFixture]
    public class JsonParserUnitTest
    {
        private delegate bool TestContains(string x);
        private IJSONParser uut;
        [Test]
        public void TestNewHostParser()
        {
            uut = new NewHostParser();

            Host testHost = new Host
            {
                Email = "testMail",
                Name = "testName",
                Password = "testPassword"
            };

            var output = uut.ParsedData(testHost);
            var c = new TestContains(output.Contains);

            Assert.That(c("Email") && c("testMail") &&
                        c("Name") && c("testName") &&
                        c("Password") && c("testPassword"));
        }
        [Test]
        public void TestNewEventParser()
        {
            uut = new NewEventParser();

            var testEvent = new EventModel
            {
                Description = "testDescription",
                EndDate = DateTime.MinValue,
                Location = "testLocation",
                Name = "testName",
                StartDate = DateTime.MinValue
            };

            
            var parsed = uut.ParsedData(testEvent);
            var c = new TestContains(parsed.Contains);

            Assert.That(c("Description") && c("testDescription") &&
                        c("EndDate") && c(DateTime.MinValue.ToString("yyyy-MM-ddTHH:mm:ss")) &&
                        c("StartDate") && c(DateTime.MinValue.ToString("yyyy-MM-ddTHH:mm:ss")) &&
                        c("Location") && c("testLocation") &&
                        c("Name") && c("testName"));
        }
        [Test]
        public void TestGuestParser()
        {
            uut = new GuestParser();

            var testGuest = new Guest
            {
                Pin = "testPin",
                Username = "testUsername"
            };

            var parsed = uut.ParsedData(testGuest);
            var c = new TestContains(parsed.Contains);

            Assert.That(c("Pin") && c("testPin") &&
                        c("Username") && c("testUsername"));

        }

        [Test]
        public void TestPhotoParser()
        {
            uut = new PhotoParser();

            var testPhoto = new PhotoToServer
            {
                Bytes = new byte[1234],
                Pin = "testPin"
            };

            var parsed = uut.ParsedData(testPhoto);
            var c = new TestContains(parsed.Contains);

            Assert.That(c("pictureString") && c(Convert.ToBase64String(new byte[1234])) && 
                        c("eventPin") && c("testPin"));
        }

        [Test]
        public void TestHostParser()
        {
            uut = new HostParser();

            var testHost = new Host
            {
                Password = "testPassword",
                Email = "testEmail",
                Name = "testName"
            };

            var parsed = uut.ParsedData(testHost);
            var c = new TestContains(parsed.Contains);

            Assert.That(c("Password") && c("testPassword") && 
                        c("UserName") && c("testEmail"));

        }

        [Test]
        public void TestNewHostCorrectNoOfColons()
        {
            uut = new NewHostParser();

            Host testHost = new Host();

            var props = testHost.GetType().GetProperties();
            var noOfProps = props.Length;

            var parsed = uut.ParsedData(testHost);
            var noOfColons = parsed.Split(':').Length - 1;

            Assert.That(noOfProps, Is.EqualTo(noOfColons));
        }
    }
}
