using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PB.Dto;
using Photobook.Models;
using Photobook.Models.ServerClasses;

namespace UnitTest
{
    [TestFixture]
    class PostfixTest
    {
        private IUrlPostfix uut;

        [Test]
        public void TestGuestPostfix()
        {
            uut = new GuestPostfix();
            Guest g = new Guest
            {
                Username = "testUser"
            };

            Assert.That(uut.Generate(g), Is.EqualTo("testUser"));
        }

        [Test]
        public void TestPicturePostfix()
        {
            uut = new PicturePostfix();
            TestImage ti = new TestImage
            {
                PinId = "testId"
            };

            Assert.That(uut.Generate(ti), Is.EqualTo("testId"));
        }

        [Test]
        public void TestEventModel()
        {
            uut = new EventPostFix();
            EventModel em = new EventModel
            {
                Pin = "testPin"
            };

            Assert.That(uut.Generate(em), Is.EqualTo("testPin"));
        }
    }
}
