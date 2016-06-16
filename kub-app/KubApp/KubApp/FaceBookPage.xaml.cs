using KubApp_v0._1;
using KubApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.InteropServices;
using System.Net;
using System.Diagnostics;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Security.Cryptography;
using ColorPicker;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using System.Text.RegularExpressions;
using Windows.Web;
using Windows.UI.WebUI;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Notifications;
using NotificationsExtensions;
using Microsoft.QueryStringDotNET;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KubApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FaceBookPage : Page
    {
        private string message;
        Windows.Storage.ApplicationDataContainer fbInfo = Windows.Storage.ApplicationData.Current.LocalSettings;
        private string AccessToken;
        private DateTime TokenExpiry;
        private Facebook.FacebookClient fbClient;
        private dynamic fbUser;

        public FaceBookPage()
        {
            this.InitializeComponent();
        }

        private void BACK_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void POST_Click(object sender, RoutedEventArgs e)
        {
            this.message = PostText.Text;
            FBPost();
        }

        private async void FBPost()
        {
            if (fbInfo.Values["token"].ToString() == "0")
            {
                FBLogin();
            }
            else
            {
                Object tokenValue = fbInfo.Values["token"];
                fbClient = new Facebook.FacebookClient(tokenValue.ToString());
                fbUser = await fbClient.GetTaskAsync("me");
                await fbClient.PostTaskAsync("/me/feed", new { message = this.message });
            }
        }
        private async void FBLogin()
        {
            //Facebook app id
            var clientId = "1269278043097270";
            //Facebook permissions
            var scope = "public_profile, publish_actions, manage_pages";

            var redirectUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString();
            var fb = new Facebook.FacebookClient();
            Uri loginUrl = fb.GetLoginUrl(new { client_id = clientId, redirect_uri = redirectUri, response_type = "token", scope = scope });

            Uri startUri = loginUrl;
            Uri endUri = new Uri(redirectUri, UriKind.Absolute);

            WebAuthenticationResult webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri, endUri);

            if (webAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
            {
                var outputToken = webAuthenticationResult.ResponseData.ToString();

                var pattern = string.Format("{0}#access_token={1}&expires_in={2}", WebAuthenticationBroker.GetCurrentApplicationCallbackUri(), "(?<access_token>.+)", "(?<expires_in>.+)");
                var match = Regex.Match(outputToken, pattern);

                var access_token = match.Groups["access_token"];
                var expires_in = match.Groups["expires_in"];

                AccessToken = access_token.Value;
                TokenExpiry = DateTime.Now.AddSeconds(double.Parse(expires_in.Value));

                fbClient = new Facebook.FacebookClient(AccessToken);
                fbUser = await fbClient.GetTaskAsync("me");

                WebRequest profilePicRequest = HttpWebRequest.Create(string.Format("https://graph.facebook.com/{0}/picture", fbUser.id));
                WebResponse response = await profilePicRequest.GetResponseAsync();
                var pictureUrl = response.ResponseUri.ToString();
                //image1.Source = new BitmapImage(new Uri(pictureUrl, UriKind.Absolute));

                fbInfo.Values["token"] = AccessToken;
            }
        }
    }
}
