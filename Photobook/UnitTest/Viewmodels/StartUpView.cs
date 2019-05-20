using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using NUnit.Framework;
using Photobook.ViewModels;
using Xamarin.Forms;

namespace UnitTest.Viewmodels
{
    [TestFixture]
    class StartUpView
    {
        private StartUpViewViewModel _uut;
        private INavigation navigation;

        [SetUp]
        public void Setup()
        {
            navigation = Substitute.For<INavigation>();
            _uut = new StartUpViewViewModel(navigation);
        }


    }
}
