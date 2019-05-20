using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using NUnit.Framework;
using Photobook.View;
using Photobook.ViewModels;
using Xamarin.Forms;

namespace UnitTest.Viewmodels
{
    [TestFixture]
    public class StartUpView
    {
        private StartUpViewViewModel _uut;
        private INavigation navigation;

        [SetUp]
        public void Setup()
        {
            navigation = Substitute.For<INavigation>();
            _uut = new StartUpViewViewModel(navigation);
        }


        [Test]
        public void GuestLogin_Execiute()
        {
            // Fejler ved memory manager
            _uut.GuestLoginCommand.Execute(null);
            navigation.Received(1).PushAsync(Arg.Any<GuestLogin>());
        }



    }
}
