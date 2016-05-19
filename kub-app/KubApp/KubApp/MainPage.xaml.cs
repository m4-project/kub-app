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
        
        public MainPage()
        {
            this.InitializeComponent();
            Connect();
        }

        public void Connect()
        {
            client.Connect("kub-app");
            client.Subscribe(new string[] { "test" }, new byte[] { 0 });
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Debug.WriteLine(System.Text.Encoding.UTF8.GetString(e.Message));
        }

        //private void colorChange()
        //{
        //    SolidColorBrush brush = colorpp.SelectedColor;
        //    currentColor.Fill = brush;
        //}

        //private void colorp_PointerMoved(object sender, PointerRoutedEventArgs e)
        //{
        //    colorChange();
        //}

        private void Temperature_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TemperaturePage), null);
        }

        private void Led_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LedPage), null);
        }
    }
}
