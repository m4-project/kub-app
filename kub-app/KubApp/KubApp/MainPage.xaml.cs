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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KubApp_v0._1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //Maakt een nieuwe MqttClient aan
        private MqttClient client = new MqttClient("home.jk-5.nl", 1883, false, MqttSslProtocols.None);
        
        public Dictionary<string, Kub> kubs = new Dictionary<string, Kub>();

        //private Kub kub;


        public MainPage()
        {
            this.InitializeComponent();
            Connect();
            Temperature();
            setKubStatus();
            setComboBox();
        }

        public void Connect()
        {
            client.Connect("kub-app");
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            //TODO: maak instellingenpagina om kubs te koppelen
            Kub kub = new Kub("1234", client);
            this.kubs.Add("1234",kub);

            //Timer om de temperatuur te refreshen na 1 minuut zodat dit up to date blijft
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            Temperature();
        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string[] parts = e.Topic.Split('/');

            if(parts.Length != 3 || parts[0] != "kub")
            {
                return;
            }

            string kid = parts[1];

            if(e.Message.Length < 5)
            {
                return;
            }

            byte protocolVersion = e.Message[0];

            int payloadLength = BitConverter.ToInt32(e.Message, 1);

            if(e.Message.Length < payloadLength + 5)
            {
                return;
            }

            byte[] payload = e.Message.Skip(5).ToArray();
            Kub kub = kubs[kid];
            kub.PacketReceived(payload, parts[2]);
        }

        public void Temperature()
        {
            //int value = 25;
            //temperatureKub.Text = value.ToString();

            //this.kub.RequestData(Kub.DataType.Temperature, delegate (int value)
            //{
                //System.Diagnostics.Debug.WriteLine("Temperature: " + value);

                int value = 70;

                //Zet de text van de textblock naar "Temperature Kub = " + value + " °C"
                temperatureKub.Text = value.ToString();

                //Kijkt naar de temperatuur van de Kub en bepaald daarmee de kleur van de achtergrond.
                if (value >= 65)
                {
                    statusColor.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                    textBlockStatus.Text = "Status: Feel the Heat";
                }
                else if (value < 60 && value > 48)
                {
                    statusColor.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
                    textBlockStatus.Text = "Status: Taste the Flavors";
                }
                else if (value < 48 && value > 20)
                {
                    statusColor.Fill = new SolidColorBrush(Windows.UI.Colors.Blue);
                    textBlockStatus.Text = "Status: Enjoy the Sweetness";
                }
                else if(value < 20)
                {
                    statusColor.Fill = new SolidColorBrush(Windows.UI.Colors.White);
                    textBlockStatus.Text = "Status: Like Iced Coffee?";
                }
            //});
        }

        private void setKubStatus()
        {
            if (client.IsConnected)
            {
                textBoxkubstatus.Text = "Connected";
            }
            else
            {
                textBoxkubstatus.Text = "Disconnected";
            }
        }

        private void setComboBox()
        {
            foreach(KeyValuePair<string, Kub> kub in kubs)
            {
                comboBox.Items.Add(kub);
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            kubMenu.SelectedIndex = 1;
            //this.kub = (Kub)e.Parameter;
            //this.Frame.Navigate(typeof(TemperaturePage), kubs["1234"]); // TODO: maak dit configureerbaar
        }

        private void Led_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LedPage));
        }

        private void LED_Click_1(object sender, RoutedEventArgs e)
        {
            kubMenu.SelectedIndex = 2;
        }

        private void GAMES_Click(object sender, RoutedEventArgs e)
        {
            kubMenu.SelectedIndex = 3;
        }

        private void SETTINGS_Click(object sender, RoutedEventArgs e)
        {
            kubMenu.SelectedIndex = 4;
        }

        private void changeColor()
        {
            if (toggleSwitchLed.IsOn)
            {
                curColor.Fill = colorp.SelectedColor;

                // geselecteerde kleur in hexadecimaal
                string hexColor = colorp.SelectedColor.Color.ToString();

                // geselecteerde kleur in RGB
                string hexColorSub = hexColor.Substring(3);
                int R = int.Parse(hexColorSub.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                int G = int.Parse(hexColorSub.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                int B = int.Parse(hexColorSub.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

                //Kub kub = this.kubs["1234"];
                //kub.SetLed(0, (byte)R, (byte)G, (byte)B);
                //kub.SetLed(1, (byte)R, (byte)G, (byte)B);
            }
        }

        private void colorp_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (toggleSwitchLed.IsOn)
            {
                changeColor();
            }
        }

        private void colorp_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (toggleSwitchLed.IsOn)
            {
                changeColor();
            }
        }

        private void pickColorFlyout_Closed(object sender, object e)
        {
            if (toggleSwitchLed.IsOn)
            {
                changeColor();
            }
        }

        private void slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            // geselecteerde kleur in hexadecimaal
            string hexColor = colorp.SelectedColor.Color.ToString();

            // geselecteerde kleur in RGB
            string hexColorSub = hexColor.Substring(3);
            int R = int.Parse(hexColorSub.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(hexColorSub.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(hexColorSub.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            double sliderValue = slider.Value;

            double resultR = Math.Round(R * (sliderValue / 100));
            double resultG = Math.Round(G * (sliderValue / 100));
            double resultB = Math.Round(B * (sliderValue / 100));

            //Kub kub = this.kubs["1234"];
            //kub.SetLed(0, (byte)resultR, (byte)resultG, (byte)resultB);
            //kub.SetLed(1, (byte)resultR, (byte)resultG, (byte)resultB);

            SolidColorBrush brush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, (byte)resultR, (byte)resultG, (byte)resultB));
            curColor.Fill = brush;
        }

        private void ScanQR_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(newKub));
        }

        private void toggleSwitchLed_Toggled(object sender, RoutedEventArgs e)
        {
            if (!toggleSwitchLed.IsOn)
            {
                colorp.IsEnabled = false;
                curColor.Opacity = 0.3;
                slider.IsEnabled = false;
            }
            else
            {
                colorp.IsEnabled = true;
                curColor.Opacity = 1.0;
                slider.IsEnabled = true;
            }
        }
    }
}
