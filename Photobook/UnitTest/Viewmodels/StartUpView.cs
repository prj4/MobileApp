using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using Photobook.Models;
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
            IMemoryManager mem = Substitute.For<IMemoryManager>();
            MemoryManager.Instance = mem;
            var list = new List<GuestAtEvent>();
            var ret = Task.FromResult(list);

            mem.ReturnsForAll(ret);

            
            _uut.GuestLoginCommand.Execute(null);
            navigation.Received(1).PushAsync(Arg.Any<GuestLogin>());
        }



    }
}
