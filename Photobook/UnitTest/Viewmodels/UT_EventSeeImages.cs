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
        private ServerCommunicator com;
        private INavigation nav;


        [SetUp]
        public void Setup()
        {
            com = Substitute.For<ServerCommunicator>();
            nav = Substitute.For<INavigation>();

            Eventmodel = new EventModel();
            Eventmodel.Pin = "1234";
            Eventmodel.Description = "Description";
            Eventmodel.EndDate = DateTime.MaxValue;
            Eventmodel.StartDate = DateTime.MinValue;
            Eventmodel.Location = "På skolen";
            Eventmodel.Name = "Navn på event";

            _uut = new EventSeeImagesViewModel(Eventmodel, com, nav);
        }


        [Test]
        public async Task ReloadData_Servercom_Called()
        {
            _uut.ReloadData();
            // Can't test cookies
            Assert.That(_uut.Items.Count, Is.EqualTo(1));
        }


        [Test]
        public void ItemTappedCommand_Image()
        {
            _uut.LastTappedItem = new TestImage
            {
                FileName = "Filename",
                ImagePath = "Path",
                PinId = "1",
            };

            _uut.ItemTappedCommand.Execute(null);
            nav.Received(1).PushAsync(Arg.Is(new EventSeeSingleImage((TestImage)_uut.LastTappedItem)));

        }








    }
}
