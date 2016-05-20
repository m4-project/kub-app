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

        public void Connect()
        {
            client.Connect("kub-app");
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            
            //TODO: maak instellingenpagina om kubs te koppelen
            this.kubs.Add("1234", new Kub("1234", client));
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
    }
}
