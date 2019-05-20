using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PB.Dto;
using Photobook.Models;
using Photobook.ViewModels;

namespace IntegrationsTest
{
    [TestFixture]
    public class IT_EventSeeImages
    {
        private EventSeeImagesViewModel EventSeeImages;
        private EventModel Eventmodel;
        private ServerCommunicator com;

        [SetUp]
        public void Setup()
        {
            com = new ServerCommunicator();
            Eventmodel = new EventModel();
            Eventmodel.Pin = "1234";
            Eventmodel.Description = "Description";
            Eventmodel.EndDate = DateTime.MaxValue;
            Eventmodel.StartDate = DateTime.MinValue;
            Eventmodel.Location = "På skolen";
            Eventmodel.Name = "Navn på event";

            EventSeeImages = new EventSeeImagesViewModel(Eventmodel, com);
        }

        [Test]
        public void test()
        {

        }


    }
}
