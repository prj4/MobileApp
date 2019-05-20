using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PB.Dto;
using Photobook.ViewModels;

namespace UnitTest.Viewmodels
{
    [TestFixture]
    class EventSeeSingleImageUnitTest
    {
        private GuestLoginViewModel _uut;
        [SetUp]
        public void SetUp()
        {
            _uut = new GuestLoginViewModel();
        }

        public void Test()
        {
            Assert.That(true, Is.EqualTo(true));
        }


    }
}
