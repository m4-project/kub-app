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

        byte temperature;
        
        public MainPage()
        {
            this.InitializeComponent();
            Temperature();

            client.Connect("kub-app");
            client.Subscribe(new string[]{ "test" }, new byte[] { 0 });
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        }

        public void Temperature()
        {
            temperature = 45;

            //Zet de text van de textblock naar "Temperature Kub = " + temperature + " °C"
            TemperatureKub.Text = "Temperature Kub = " + temperature + " °C";

            //Kijkt naar de temperatuur van de Kub en bepaald daarmee de kleur van de achtergrond.
            if (temperature >= 60)
            {
                //Verandert de achtergrond kleur van LayoutGrid t.o.v. de temperatuur
                PivotTemperature.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else if (temperature < 60 && temperature > 30)
            {
                //Verandert de achtergrond kleur van LayoutGrid t.o.v. de temperatuur
                PivotTemperature.Background = new SolidColorBrush(Windows.UI.Colors.LightGreen);
            }
            else if (temperature < 30 && temperature > 0)
            {
                //Verandert de achtergrond kleur van LayoutGrid t.o.v. de temperatuur
                PivotTemperature.Background = new SolidColorBrush(Windows.UI.Colors.LightBlue);
            }
        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Debug.WriteLine(System.Text.Encoding.UTF8.GetString(e.Message));
        }
    }
}
