using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Exceptions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PB.Dto;
using Photobook.Models;
using Photobook.View;
using Photobook.ViewModels;
using Xamarin.Forms;

namespace UnitTest.Viewmodels
{
    [TestFixture()]
    class UT_EventSeeImages
    {

        private EventSeeImagesViewModel _uut;
        private EventModel Eventmodel;
        private IServerCommunicator com;

        [SetUp]
        public void Setup()
        {
            com = Substitute.For<IServerCommunicator>();


            Eventmodel = new EventModel();
            Eventmodel.Pin = "1234";
            Eventmodel.Description = "Description";
            Eventmodel.EndDate = DateTime.MaxValue;
            Eventmodel.StartDate = DateTime.MinValue;
            Eventmodel.Location = "På skolen";
            Eventmodel.Name = "Navn på event";

            _uut = new EventSeeImagesViewModel(Eventmodel, com);
        }


        [Test]
        public async Task ReloadData_Servercom_Called()
        {
            List<string> tempList = new List<string>();
            tempList.Add("1");
            tempList.Add("2");

            com.GetImages(Arg.Any<EventModel>(), Arg.Any<CookieCollection>()).Returns(tempList);

            _uut.ReloadData();
            // Can't test cookies
            Assert.That(_uut.Items.Count, Is.EqualTo(1));
        }


    }
}
