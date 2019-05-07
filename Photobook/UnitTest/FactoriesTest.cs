using System;
using NUnit;
using NUnit.Framework;
using Photobook.Models;

namespace UnitTest
{
    [TestFixture]
    class FactoriesTest
    {
        [Test, TestCaseSource("ParserTypes")]
        public void TestGenerateParserFactory(DataType d, IJSONParser pars)
        {
            IJSONParser testPars = ParserFactory.Generate(d);

            Assert.That(testPars.GetType(), Is.EqualTo(pars.GetType()));
        }

        private static object[] ParserTypes =
        {
            new object[]{DataType.Guest, new GuestParser()},
            new object[]{DataType.Host, new HostParser()},
            new object[]{DataType.NewEvent, new NewEventParser()},
            new object[]{DataType.Picture, new PhotoParser()},  
        };

        [TestCase(DataType.Guest, "Account/Guest")]
        [TestCase(DataType.NewUser, "Account/Host")]
        [TestCase(DataType.Picture, "Picture")]
        [TestCase(DataType.Host, "Account/Login")]
        [TestCase(DataType.NewEvent, "Event")]
        public void TestGenerateUrlFactory(DataType d, string urlSuffix)
        {
            string defaultPath = "https://photobookwebapi1.azurewebsites.net/api/";
            string testUrl = UrlFactory.Generate(d);

            Assert.That(testUrl, Is.EqualTo(defaultPath + urlSuffix));
        }
    }
}
