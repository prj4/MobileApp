using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Photobook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace IntegrationsTest.ViewModels
{
    [TestFixture]
    class IT_StartUpViewNavigation
    {
        private StartUpViewViewModel uut;
        private INavigation nav;

        [SetUp]
        public void Setup()
        {
            nav = new NavigationProxy();
            uut = new StartUpViewViewModel(nav);
            
        }
        [Test]
        public void TestNoAction()
        {
            Assert.That(nav.NavigationStack.Count == 0);
        }

        [Test]
        public void TestGuestLoginIsPushed()
        {
            uut.GuestLoginCommand.Execute(null);

            Assert.That(nav.NavigationStack.Count == 1);
        }
    }
}
