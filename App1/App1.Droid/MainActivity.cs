using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Android.Webkit;
using Newtonsoft.Json.Linq;

namespace App1.Droid
{
    [Activity(Label = "App1", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IAuthenticate
    {
        public static string azureUrl = @"https://meetingpoints.azurewebsites.net";
        //public static MobileServiceClient client = new MobileServiceClient("https://meetingpoints.azurewebsites.net");
        MobileServiceUser user;
        /*public MobileServiceClient client
        {
            get
            {
                if (client == null) return new MobileServiceClient(azureUrl);
                else return client;
            }
        } */
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            App.Init((IAuthenticate)this);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public async Task<bool> AuthenticateAsync()
        {
            var token = new JObject();
            // Replace access_token_value with actual value of your access token obtained
            // using the Facebook or Google SDK.
            token.Add("access_token", "312543389139965|F7n2bHasI5nBA-IkO08_ilTddrE");
            bool success = false;
            try
            {
                if (user == null)
                {
                    // The authentication provider could also be Facebook, Twitter, or Microsoft
                    //user = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook);
                    //var cl = client;
                    user = await Manager.DefaultManager.CurrentClient.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook);
                    if (user != null)
                    {
                        CreateAndShowDialog(string.Format("You are now logged in - {0}", user.UserId), "Logged in!");
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex.Message, "Authentication failed");
            }
            return success;
        }

        public async Task<bool> LogoutAsync()
        {
            bool success = false;
            try
            {
                if (user != null)
                {
                    CookieManager.Instance.RemoveAllCookie();
                    await Manager.DefaultManager.CurrentClient.LogoutAsync();
                    CreateAndShowDialog(string.Format("You are now logged out - {0}", user.UserId), "Logged out!");
                }
                user = null;
                success = true;
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex.Message, "Logout failed");
            }

            return success;
        }

        void CreateAndShowDialog(string message, string title)
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.SetNeutralButton("OK", (sender, args) => {
            });
            builder.Create().Show();
        }

    }
}

