using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PB.Dto;
using Photobook.Models;

namespace UnitTest
{
    [TestFixture]
    class FromJsonUnitTest
    {
        private IFromJSONParser uut;
        [SetUp]
        public void Setup()
        {
            uut = new FromJsonParser();
        }

        [Test]
        public async Task TestEventModel()
        {
            var dateFormat = "yyyy-MM-ddTHH:mm:ss";
            string toParse = "{\n\t\"Location\":\"testLocation\",\n\t\"Description\":\"testDescription\",\n\t\"Name\":\"testName\",\n\t\"StartDate\":\"2019-05-20T12:00:00\",\n\t\"EndDate\":\"2019-05-20T12:00:00\",\n\t\"Pin\":\"1234\"\n}";
            HttpResponseMessage msg = new HttpResponseMessage
            {
                Content = new StringContent(toParse)
            };


            EventModel testModel = await uut.DeserializedData<EventModel>(msg);

            Assert.That(testModel.Description.Contains("testDescription") && 
                        testModel.EndDate.ToString(dateFormat).Contains("2019-05-20T12:00:00") &&
                        testModel.Location.Contains("testLocation") &&
                        testModel.Name.Contains("testName") && 
                        testModel.Pin.Contains("1234") && 
                        testModel.StartDate.ToString(dateFormat).Contains("2019-05-20T12:00:00"));
        }

        [Test]
        public async Task TestDictionary()
        {
            string toParse = "{\n\t\"testKey\":\"testValue\"\n}";
            HttpResponseMessage msg = new HttpResponseMessage
            {
                Content = new StringContent(toParse)
            };

            Dictionary<string, string> dic = await uut.DeserializedData<Dictionary<string, string>>(msg);

            Assert.That(dic["testKey"], Is.EqualTo("testValue"));
        }

        [Test]
        public async Task TestReturnHost()
        {
            string toParse =
                "{\n\t\"Name\":\"testName\",\n\t\"Email\":\"testEmail\",\n\t\"Events\":[\n\t\t{\n\t\t\t\"Location\":\"testLocation\",\n\t\t\t\"Description\":\"testDescription\",\n\t\t\t\"Name\":\"testName\",\n\t\t\t\"StartDate\":\"2019-05-20T12:00:00\",\n\t\t\t\"EndDate\":\"2019-05-20T12:00:00\",\n\t\t\t\"Pin\":\"1234\"\n\t\t}\n\t\t]\n}";
            HttpResponseMessage msg = new HttpResponseMessage
            {
                Content = new StringContent(toParse)
            };

            ReturnHostModel testHost = await uut.DeserializedData<ReturnHostModel>(msg);

            Assert.That(testHost.Email.Contains("testEmail") &&
                        testHost.Name.Contains("testName") && 
                        testHost.Events.ToList().Count == 1);
        }
    }
}
