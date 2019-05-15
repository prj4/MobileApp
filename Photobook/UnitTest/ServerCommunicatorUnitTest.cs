
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using PB.Dto;
using Photobook.Models;
using Photobook.Models.ServerClasses;
using Photobook.Models.ServerDataClasses;

namespace UnitTest
{
    [TestFixture]
    class ServerCommunicatorUnitTest
    {
        private HttpClient mockClient;
        private HttpClientHandler mockClientHandler;
        private CookieContainer mockCookieContainer;
        private IServerErrorcodeHandler mockErrorcodeHandler;
        private IServerDataHandler mockDataHandler;
        private IServerCommunicator uut;

        [SetUp]
        public void Setup()
        {
            mockClient = Substitute.For<HttpClient>();
            mockClientHandler = Substitute.For<HttpClientHandler>();
            mockCookieContainer = Substitute.For<CookieContainer>();
            mockErrorcodeHandler = Substitute.For<IServerErrorcodeHandler>();
            mockDataHandler = Substitute.For<IServerDataHandler>();
            uut = new ServerCommunicator(mockClient, 
                mockClientHandler, 
                mockCookieContainer, 
                mockDataHandler, 
                mockErrorcodeHandler
                );
        }

        [Test]
        public async Task TestResponseIsNullResultIsFalse()
        {
            Host h = new Host();
            Assert.That(await uut.SendDataReturnIsValid(h, DataType.Host), Is.False);
        }

        

        [TestCase(HttpStatusCode.OK, true)]
        [TestCase(HttpStatusCode.NotFound, false)]
        [TestCase(HttpStatusCode.InternalServerError, false)]
        public async Task TestErrorCodeReturnsCorrect(HttpStatusCode code, bool response)
        {
            HttpResponseMessage rep = new HttpResponseMessage
            {
                Content = new StringContent("test"),
                StatusCode = code
            };

            Task<HttpResponseMessage> msg = Task.FromResult(rep);

            Host h = new Host();

            mockClient.ReturnsForAll(msg);
            
            Assert.That(await uut.SendDataReturnIsValid(h, DataType.Host), Is.EqualTo(response));
        }
    }
}
