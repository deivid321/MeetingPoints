using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
    public partial class IntroPage : ContentPage
    {
        JObject token;
        bool authenticated = false;
        public IntroPage()
        {
            InitializeComponent();
            token = new JObject();
            // Replace access_token_value with actual value of your access token obtained
            // using the Facebook or Google SDK.
            token.Add("access_token", "312543389139965|F7n2bHasI5nBA-IkO08_ilTddrE");
        }

        async void Handle_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (App.Authenticator != null)
                {
                    authenticated = await App.Authenticator.AuthenticateAsync();
                }

                if (authenticated)
                {
                    await Navigation.PushAsync(new MapPage());
                    //Navigation.InsertPageBefore(new MapPageCS(), this);
                    //await Navigation.PopAsync();
                }
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("Authentication was cancelled"))
                {
                    //messageLabel.Text = "Authentication cancelled by the user";
                }
            }
            catch (Exception)
            {
                //messageLabel.Text = "Authentication failed";
                //await Navigation.PopAsync();
            }
            //
            //Navigation.InsertPageBefore(new MapPage(), this);
            //await Navigation.PopAsync();
        }


    }
}
