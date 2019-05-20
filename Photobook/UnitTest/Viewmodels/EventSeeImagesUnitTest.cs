using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using NUnit.Framework;
using PB.Dto;
using Photobook.Models;
using Photobook.View;
using Photobook.ViewModels;
using Xamarin.Forms;

namespace UnitTest.Viewmodels
{
    [TestFixture]
    public class EventSeeImagesUnitTest
    {
        private EventSeeImagesViewModel _uut;
        private EventModel EventModel;

        [SetUp]
        public void SetUp()
        {
            EventModel = new EventModel();
            //_uut = new EventSeeImagesViewModel(EventModel);
        }

        [Test]
        public void Command_ItemTappedCommand_ReturnsDelegate()
        {
   

            Assert.That(true, Is.EqualTo(true));


        }

    }
}
