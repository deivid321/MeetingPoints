using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public class App : Application
    {
        public static IAuthenticate Authenticator { get; private set; }
        public static double ScreenHeight;
        public static double ScreenWidth;

        public App()
        {
            // The root page of your application
            var content = new ContentPage
            {
                Title = "App1",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!"
                        }
                    }
                }
            };

            MainPage = new NavigationPage(new IntroPage())
            {
                BarTextColor = Color.White,
                BarBackgroundColor = Color.FromHex("#FB7A52")
            };
        }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            base.OnResume();
        }
    }
}
