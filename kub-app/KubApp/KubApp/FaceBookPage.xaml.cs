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
            PostOnFb();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AccessToken = e.Parameter as string;
        }

        private async void PostOnFb()
        {
            try
            {
                fbClient = new Facebook.FacebookClient(AccessToken);
                fbUser = await fbClient.GetTaskAsync("me");
                await fbClient.PostTaskAsync("/me/feed", new { message = this.message });
            }
            catch(Exception ex)
            {
                PostText.Text += "!! Something went wrong, please try again !!" + ex;
            }
        }
    }
}
