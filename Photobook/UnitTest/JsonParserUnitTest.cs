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
                Email = "testmail",
                Name = "testname",
                Password = "testpassword"
            };

            var output = uut.ParsedData(testHost);
            var c = new TestContains(output.Contains);

            Assert.That(c("Email") && c("testmail") &&
                        c("Name") && c("testname") &&
                        c("Password") && c("testpassword"));
        }
        [Test]
        public void TestNewEventParser()
        {
            uut = new NewEventParser();

            var testEvent = new EventModel
            {
                Description = "testdescription",
                EndDate = DateTime.MinValue,
                Location = "testlocation",
                Name = "testname",
                StartDate = DateTime.MinValue
            };

            
            var parsed = uut.ParsedData(testEvent);
            var c = new TestContains(parsed.Contains);

            Assert.That(c("Description") && c("testdescription") &&
                        c("EndDate") && c(DateTime.MinValue.ToString("yyyy-MM-ddTHH:mm:ss")) &&
                        c("StartDate") && c(DateTime.MinValue.ToString("yyyy-MM-ddTHH:mm:ss")) &&
                        c("Location") && c("testlocation") &&
                        c("Name") && c("testname"));
        }
        [Test]
        public void TestGuestParser()
        {
            uut = new GuestParser();

            var testGuest = new Guest
            {
                Pin = "testpin",
                Username = "testusername"
            };

            var parsed = uut.ParsedData(testGuest);
            var c = new TestContains(parsed.Contains);

            Assert.That(c("Pin") && c("testpin") &&
                        c("Name") && c("testusername"));

        }

        [Test]
        public void TestPhotoParser()
        {
            uut = new PhotoParser();

            var testPhoto = new PhotoToServer
            {
                Bytes = new byte[1234],
                Pin = "testpin"
            };

            var parsed = uut.ParsedData(testPhoto);
            var c = new TestContains(parsed.Contains);

            Assert.That(c("pictureString") && c(Convert.ToBase64String(new byte[1234])) && 
                        c("eventPin") && c("testpin"));
        }

        [Test]
        public void TestHostParser()
        {
            uut = new HostParser();

            var testHost = new Host
            {
                Password = "testpassword",
                Email = "testemail",
                Name = "testname"
            };

            var parsed = uut.ParsedData(testHost);
            var c = new TestContains(parsed.Contains);

            Assert.That(c("Password") && c("testpassword") && 
                        c("UserName") && c("testemail"));

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
