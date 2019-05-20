using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Photobook.Models;

namespace UnitTest
{
    [TestFixture]
    class ServerErrorcodeHandlerTest
    {
        private IServerErrorcodeHandler uut;
        [Test]
        public void TestDefaultErrorMessage()
        {
            uut = new GuestLoginErrorcodeHandler();

            Assert.That(uut.Message, Is.EqualTo("Error."));
        }

        [TestCase(HttpStatusCode.NoContent, "Name is already taken.")]
        [TestCase(HttpStatusCode.NotFound, "Pin is wrong.")]
        [TestCase(HttpStatusCode.InternalServerError, "Something went wrong. Try again later.")]
        public void TestCorrectErrorMessage(HttpStatusCode code, string message)
        {
            uut = new GuestLoginErrorcodeHandler();
            HttpResponseMessage msg = new HttpResponseMessage
            {
                StatusCode = code
            };
            uut.Handle(msg);

            
            Assert.That(uut.Message, Is.EqualTo(message));
        }

        [Test]
        public void TestDefaultHandleMessage()
        {
            uut = new GuestLoginErrorcodeHandler();
            HttpResponseMessage msg = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadGateway //Noget andet end det, der er taget højde for
            };
            uut.Handle(msg);

            Assert.That(uut.Message, Is.EqualTo("Error logging in."));
        }

    }
}
