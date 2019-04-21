using System;
using DLToolkit.Forms.Controls;
using Photobook.Models;
using Photobook.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Photobook
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            FlowListView.Init();
            Xamarin.Forms.DependencyService.Register<ICameraAPI>();
        }
        

        protected override void OnStart()
        {
            //MainPage = new NavigationPage(new StartUpView());
            MainPage = new NavigationPage(new EventSeeImages());
        }

        protected override void OnSleep()
        {
           
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
