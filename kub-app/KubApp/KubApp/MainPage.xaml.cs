using KubApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Runtime.InteropServices;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Net;
using ColorPicker;
using System.Diagnostics;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using Windows.Security.Cryptography;

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
        
        private Dictionary<string, Kub> kubs = new Dictionary<string, Kub>();

        public MainPage()
        {
            this.InitializeComponent();
            Connect();
        }

        public void getID()
        {
            //string kubId = textbox.Text;
            //int number;
            //bool containNumber = int.TryParse(kubId, out number);

            //if(containNumber == false)
            //{
            //  MessageBox.Show("Please enter the serial number of your Kub");
            //}
            //else if(containNumber == true)
            //{
            //  kubId = Conver.ToString(number);
            //}
        }

        public void Connect()
        {
            client.Connect("kub-app");
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            //TODO: maak instellingenpagina om kubs te koppelen
            Kub kub = new Kub("1234", client);
            this.kubs.Add("1234",kub);
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

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            kubMenu.SelectedIndex = 1;
            //this.Frame.Navigate(typeof(TemperaturePage), kubs["1234"]);// TODO: maak dit configureerbaar
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
            curColor.Fill = colorp.SelectedColor;

            // geselecteerde kleur in hexadecimaal
            string hexColor = colorp.SelectedColor.Color.ToString();

            //geselecteerde kleur in RGB
            string hexColorSub = hexColor.Substring(3);

            int R = int.Parse(hexColorSub.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(hexColorSub.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(hexColorSub.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);


        }

        private void pickColorFlyout_Closed(object sender, object e)
        {
            changeColor();
        }

        private void colorp_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            changeColor();
        }

        private void colorp_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            changeColor();
        }

        private void slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            // geselecteerde kleur in hexadecimaal
            string hexColor = colorp.SelectedColor.Color.ToString();

            //geselecteerde kleur in RGB
            string hexColorSub = hexColor.Substring(3);

            int R = int.Parse(hexColorSub.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(hexColorSub.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(hexColorSub.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            double sliderValue = e.NewValue;

            double result = R / 100 * sliderValue;

            textBox2.Text = R.ToString();
            textBox1.Text = result.ToString();
        }
    }
}
