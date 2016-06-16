using KubApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Net;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Windows.Security.Authentication.Web;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Notifications;
using NotificationsExtensions;
using NotificationsExtensions.Toasts;
using Microsoft.QueryStringDotNET;
using Newtonsoft.Json.Linq;
using Windows.UI.Popups;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KubApp_v0._1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage instance;

        private MqttClient client = new MqttClient("home.jk-5.nl", 1883, false, MqttSslProtocols.None);

        public Dictionary<string, Kub> kubs = new Dictionary<string, Kub>();
        
        private Kub selectedKub;
        private DispatcherTimer timer = new DispatcherTimer();
        private DispatcherTimer threadSafeTimer = new DispatcherTimer();
        private bool connected = false;
        private bool wasConnected = false;
        private uint threadSafeTemperature = 0;
        private string kubId;

        private string accessToken;                     
        private DateTime TokenExpiry;                   
        private Facebook.FacebookClient fbClient;       
        private dynamic fbUser;

        private string bodyText = "Like Iced Coffee?";

        private dynamic data { get; set; }

        Windows.Storage.ApplicationDataContainer fbInfo = Windows.Storage.ApplicationData.Current.LocalSettings;    //Creates local storage location for Facebook info.
        Windows.Storage.ApplicationDataContainer kubInfo = Windows.Storage.ApplicationData.Current.LocalSettings;   //Creates local storage location for Kub info.

        public MainPage()
        {
            this.InitializeComponent();
            MainPage.instance = this;
            Connect();
            FillComboBox();

            threadSafeTimer.Interval = new TimeSpan(0, 0, 1);
            threadSafeTimer.Start();
            threadSafeTimer.Tick += ThreadSafeEntry;

            this.NavigationCacheMode = NavigationCacheMode.Required;

            // Sets Facebook client, user and profile picture for last logged in user.
            try
            {               
                if (fbInfo.Values["token"].ToString() != "0")
                {
                    fbClient = new Facebook.FacebookClient(fbInfo.Values["token"].ToString());
                    fbUser = fbClient.GetTaskAsync("me");
                    fbProfilePic.Source = new BitmapImage(new Uri(fbInfo.Values["profilePicUrl"].ToString(), UriKind.Absolute));
                }
            }
            catch
            {
                return;
            }

        }

        /// <summary>
        /// Handles user login.
        /// clientID is from Facebook developer page.
        /// Sets the scope needed to publish messages on users wall.
        /// Creates LoginUrl and redirect URL.
        /// Authentication is handled by WebAuthenticationBroker for save Authentication.
        /// </summary>
        public async void FBLogin()
        {
            try
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

                //Get acces token out of resonse data to create a facebook client and facebook user.
                if (webAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
                {
                    var outputToken = webAuthenticationResult.ResponseData.ToString();

                    var pattern = string.Format("{0}#access_token={1}&expires_in={2}", WebAuthenticationBroker.GetCurrentApplicationCallbackUri(), "(?<access_token>.+)", "(?<expires_in>.+)");
                    var match = Regex.Match(outputToken, pattern);

                    var access_token = match.Groups["access_token"];
                    var expires_in = match.Groups["expires_in"];

                    accessToken = access_token.Value;
                    TokenExpiry = DateTime.Now.AddSeconds(double.Parse(expires_in.Value));

                    fbClient = new Facebook.FacebookClient(accessToken);
                    fbUser = await fbClient.GetTaskAsync("me");

                    WebRequest profilePicRequest = HttpWebRequest.Create(string.Format("https://graph.facebook.com/{0}/picture", fbUser.id));
                    WebResponse response = await profilePicRequest.GetResponseAsync();
                    var pictureUrl = response.ResponseUri.ToString();

                    fbProfilePic.Visibility = Visibility.Visible;
                    fbProfilePic.Source = new BitmapImage(new Uri(pictureUrl, UriKind.Absolute));

                    fbInfo.Values["token"] = accessToken;
                    fbInfo.Values["profilePicUrl"] = pictureUrl;
                }
                else
                {
                    return;
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Navigate user to login page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FBlogin_Click(object sender, RoutedEventArgs e)
        {
            FBLogin();
        }

        /// <summary>
        /// Handles user logout.
        /// Does this by creating a logout URL and by opening this URL in webauthenticationbroker.
        /// Set acces token to 0;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void FBLogout_Click(object sender, RoutedEventArgs e)
        {
            if(fbInfo.Values["token"].ToString() != "0")
            {
                string tokenValue = fbInfo.Values["token"].ToString();

                var redirectUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString();

                Uri startUri = new Uri(@"https://www.facebook.com/logout.php?next=https://www.facebook.com/&access_token=" + fbClient.AccessToken);
                Uri endUri = new Uri(redirectUri, UriKind.Absolute);

                await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri, endUri);

                this.accessToken = "0";
                this.fbInfo.Values["token"] = "0";
                fbProfilePic.Visibility = Visibility.Collapsed;
            }
            else
            {
                var dialog = new MessageDialog("It seems that you're already logged out!");
                await dialog.ShowAsync();
            }

        }

        /// <summary>
        /// Navigates to Facebook page where user is able to post a message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FBLOGO_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FaceBookPage), fbInfo.Values["token"]);
        }

        /// <summary>
        /// Connects the MqttClient with de Kub-App and makes sure that the client is subscribed.
        /// </summary>
        public void Connect()
        {
            try
            {
                client.Connect("kub-app");
                client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
                client.MqttMsgSubscribed += Client_MqttSubscribed;
                client.ConnectionClosed += Client_ConnectionClosed;
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Declares a new toast notification.
        /// </summary>
        /// <param name="content"></param>
        private void showToast(ToastContent content)
        {
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml()));
        }

        /// <summary>
        /// Sets toast notification.
        /// </summary>
        /// <param name="titleText"></param>
        /// <param name="bodyText"></param>
        private void notification(string titleText, string bodyText)
        {
            if(this.bodyText != bodyText)
            {
                showToast(new ToastContent()
                {
                    Visual = new ToastVisual()
                    {
                        TitleText = new ToastText() { Text = titleText },
                        BodyTextLine1 = new ToastText() { Text = bodyText }
                    },

                    Launch = "500",
                });

                this.bodyText = bodyText;
            }
        }

        /// <summary>
        /// On every timer tick event the method of Temperature will be called so that the temperature is refreshed on the INFO page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatcherTimer_Tick(object sender, object e)
        {
            Temperature();
        }

        /// <summary>
        /// Shows the temperature and the status of the Kub.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ThreadSafeEntry(object sender, object args)
        {
            if (!this.wasConnected && this.connected)
            {
                if (!timer.IsEnabled)
                {
                    //Timer to refresh the temperature after 10 seconds, so this stays up-to-date.
                    timer.Tick += DispatcherTimer_Tick;
                    timer.Interval = new TimeSpan(0, 0, 10);
                    timer.Start();
                    Temperature();
                }
            }else if(this.wasConnected && !this.connected)
            {
                this.timer.Stop();
            }
            textBoxkubstatus.Text = this.connected ? "Connected" : "Disconnected";
            
            //Sets the text of the textblock to the temperature Value.
            temperatureKub.Text = this.threadSafeTemperature.ToString();

            //Looks at the temperature of the Kub and decides which color must be shown in the rectangle.
            //It also sets the status of the Kub
            //And sends push notification text to toast notification.
            if (this.threadSafeTemperature >= 65)
            {
                statusColor.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                string status = "Feel the Heat";
                textBlockStatus.Text = "Status: " + status;
                notification("New status: ", status);
            }
            else if (this.threadSafeTemperature < 60 && this.threadSafeTemperature > 48)
            {
                statusColor.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
                string status = "Taste the Flavors";
                textBlockStatus.Text = "Status: " + status;
                notification("New status: ", status);
            }
            else if (this.threadSafeTemperature < 48 && this.threadSafeTemperature > 20)
            {
                statusColor.Fill = new SolidColorBrush(Windows.UI.Colors.Blue);
                string status = "Enjoy the Sweetness";
                textBlockStatus.Text = "Status: " + status;
                notification("New status: ", status);
            }
            else if (this.threadSafeTemperature < 20)
            {
                statusColor.Fill = new SolidColorBrush(Windows.UI.Colors.White);
                string status = "Like Iced Coffee?";
                textBlockStatus.Text = "Status: " + status;
                notification("New status: ", status);
            }

            this.wasConnected = this.connected;
        }

        /// <summary>
        /// If the connection of the MqttClient has been closed, the boolean connected will be set to "false".
        /// This boolean is being used to check if there is a connection between the Kub and the MqttClient.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Client_ConnectionClosed(object sender, object args)
        {
            this.connected = false;
        }

        /// <summary>
        /// If the connection of the MqttClient has been opened, the boolean connected will be set to "true".
        /// This boolean is being used to check if there is a connection between the Kub and the MqttClient.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_MqttSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            connected = true;
        }

        /// <summary>
        /// The received message of the Kub will be checked on the content.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string[] parts = e.Topic.Split('/');

            if (parts.Length != 3 || parts[0] != "kub")
            {
                return;
            }

            string kid = parts[1];

            if (e.Message.Length < 5)
            {
                return;
            }

            byte protocolVersion = e.Message[0];

            byte[] payloadData = new byte[4] { e.Message[4], e.Message[3], e.Message[2], e.Message[1] };
            uint payloadLength = BitConverter.ToUInt32(payloadData, 0);

            if (e.Message.Length < payloadLength + 5)
            {
                return;
            }

            byte[] payload = e.Message.Skip(5).ToArray();
            Kub kub = kubs[kid];
            kub.PacketReceived(payload, parts[2]);
        }

        /// <summary>
        /// Picks up the temperature of the Kub.
        /// </summary>
        public void Temperature()
        {
            if (selectedKub != null)
            {
                this.selectedKub.RequestData(Kub.DataType.Temperature, delegate (uint value)
                {
                    threadSafeTemperature = value;
                });
            }
        }

        /// <summary>
        /// Adds a new Kub through the result of the QR scanner.
        /// </summary>
        /// <param name="QRresult"></param>
        public void AddNewKub(string QRresult)
        {
            if (kubs.ContainsKey(QRresult))
            {
                return;
            }

            data = JObject.Parse(QRresult);

            kubId = (data.kubid);

            Kub newKub = new Kub(kubId, client);
            kubs.Add(kubId, newKub);

            kubInfo.Values["kubStorage"] = string.Join(",", kubs.Keys.Select(k => k).ToArray());

            FillComboBox();
        }

        /// <summary>
        /// Shows the saved Kub(s) in the combobox on the Settings page.
        /// </summary>
        public void FillComboBox()
        {
            if(kubInfo != null)
            {
                if (kubInfo.Values.ContainsKey("kubStorage")){
                    kubs.Clear();
                    string[] kubIds = ((string)kubInfo.Values["kubStorage"]).Split(',');
                    if (kubIds != null)
                    {
                        foreach (string id in kubIds)
                        {
                            Kub kub = new Kub(id, this.client);
                            kubs.Add(id, kub);
                            comboBox.Items.Add(kub);
                            if (kubInfo.Values.ContainsKey("selectedKub") && id == (string) kubInfo.Values["selectedkub"])
                            {
                                this.selectedKub = kub;
                                this.comboBox.SelectedIndex = this.comboBox.Items.IndexOf(kub);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if there is a saved Kub, if so then this one will be selected by default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.comboBox.SelectedItem == null)
            {
                return;
            }
            this.selectedKub = (Kub)this.comboBox.SelectedItem;

            if (kubInfo != null && this.selectedKub != null)
            {
                kubInfo.Values["selectedKub"] = this.selectedKub.id;
            }
        }

        private void INFO_Click(object sender, RoutedEventArgs e)
        {
            kubMenu.SelectedIndex = 1;
        }

        private void LED_Click(object sender, RoutedEventArgs e)
        {
            kubMenu.SelectedIndex = 2;
        }

        private void GAMES_Click(object sender, RoutedEventArgs e)
        {
            kubMenu.SelectedIndex = 3;
        }

        private void RockPaperSciccors_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RockPaperMain));
        }

        private void SnapGame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ColorMatchMenu));
        }

        private void MindGame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MindGameMain));
        }

        private void SETTINGS_Click(object sender, RoutedEventArgs e)
        {
            kubMenu.SelectedIndex = 4;
        }

        /// <summary>
        /// Method to change the color, it gets the color from the colorpicker and creates them in hexadecimal and then makes them RGB.
        /// </summary>
        private void ChangeColor()
        {
            if (toggleSwitchLed.IsOn)
            {
                curColor.Fill = colorp.SelectedColor;

                // Selected color in hexadecimal.
                string hexColor = colorp.SelectedColor.Color.ToString();

                // Selected color in RGB.
                string hexColorSub = hexColor.Substring(3);
                int R = int.Parse(hexColorSub.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                int G = int.Parse(hexColorSub.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                int B = int.Parse(hexColorSub.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                
                selectedKub.SetLed(0, (byte)R, (byte)G, (byte)B);
                selectedKub.SetLed(1, (byte)R, (byte)G, (byte)B);
            }
        }

        /// <summary>
        /// Changes the color on basis of the pointermovement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Colorp_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (toggleSwitchLed.IsOn)
            {
                ChangeColor();
            }
        }

        /// <summary>
        /// Changes the color to the color pressed in the colorpicker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Colorp_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (toggleSwitchLed.IsOn)
            {
                ChangeColor();
            }
        }

        /// <summary>
        /// Changes the color to the color selected when the flyout is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PickColorFlyout_Closed(object sender, object e)
        {
            if (toggleSwitchLed.IsOn)
            {
                ChangeColor();
            }
        }

        /// <summary>
        /// Changes the brightness of the selected color.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            // Selected color in hexadecimal.
            string hexColor = colorp.SelectedColor.Color.ToString();

            // Selected color in RGB.
            string hexColorSub = hexColor.Substring(3);
            int R = int.Parse(hexColorSub.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(hexColorSub.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(hexColorSub.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            double sliderValue = slider.Value;

            double resultR = Math.Round(R * (sliderValue / 100));
            double resultG = Math.Round(G * (sliderValue / 100));
            double resultB = Math.Round(B * (sliderValue / 100));
            
            if(selectedKub != null)
            {
                selectedKub.SetLed(0, (byte)resultR, (byte)resultG, (byte)resultB);
                selectedKub.SetLed(1, (byte)resultR, (byte)resultG, (byte)resultB);
            } 

            SolidColorBrush brush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, (byte)resultR, (byte)resultG, (byte)resultB));
            curColor.Fill = brush;
        }

        private void ScanQR_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(newKub));
        }

        /// <summary>
        /// Makes sure that no manual led controls can be used when the toggle switch is on "OFF".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleSwitchLed_Toggled(object sender, RoutedEventArgs e)
        {
            if (!toggleSwitchLed.IsOn)
            {
                colorp.IsEnabled = false;
                curColor.Opacity = 0.3;
                slider.IsEnabled = false;
                selectedKub.SetMode(Kub.Mode.Temperature);
            }
            else
            {
                colorp.IsEnabled = true;
                curColor.Opacity = 1.0;
                slider.IsEnabled = true;
                selectedKub.SetMode(Kub.Mode.Manual);
            }
        }
    }
}
