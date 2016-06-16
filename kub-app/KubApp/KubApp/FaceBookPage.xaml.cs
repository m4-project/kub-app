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
using Windows.UI.Popups;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KubApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FaceBookPage : Page
    {
        private string AccessToken;                     //Save acces token received from main page with the OnNavigatedTo method().

        public FaceBookPage()
        {
            this.InitializeComponent();
        }

        private void BACK_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        /// <summary>
        /// Executes PostOnFB() method to post a message on Facebook.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void POST_Click(object sender, RoutedEventArgs e)
        {
            PostOnFb(PostText.Text);
        }

        /// <summary>
        /// Receive acces token from main page.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AccessToken = e.Parameter as string;
        }

        /// <summary>
        /// Post message on Facebook.
        /// This is done by creating a facebook client and user using the acces token received with the OnNavigatedTo() method.
        /// The message is typed in by the user.
        /// </summary>
        private async void PostOnFb(string fbMessage)
        {
            //Checks if user is logged in.
            if(AccessToken == "0")
            {
                var dialog = new MessageDialog("You need to login before you can post on Facebook");
                await dialog.ShowAsync();
            }
            else
            {
                try
                {
                    Facebook.FacebookClient fbClient = new Facebook.FacebookClient(AccessToken);
                    dynamic fbUser = await fbClient.GetTaskAsync("me");
                    if (fbMessage == "")
                    {
                        errorTextBlock.Text = "You cannot place an empty message";
                    }
                    else
                    {
                        await fbClient.PostTaskAsync("/me/feed", new { message = fbMessage });
                    }
                }
                catch (Exception ex)
                {
                    var dialog = new MessageDialog("An error occured while posting your message on Facebook: " + ex);
                    await dialog.ShowAsync();
                }
            }
        }
    }
}
