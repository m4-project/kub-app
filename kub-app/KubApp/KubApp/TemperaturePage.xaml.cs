using KubApp_v0._1;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KubApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TemperaturePage : Page
    {
        //De byte die de MQTT broker verstuurd heeft 
        byte temperature;

        public TemperaturePage()
        {
            this.InitializeComponent();
            Temperature();
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
                LayoutGrid.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else if (temperature < 60 && temperature > 30)
            {
                //Verandert de achtergrond kleur van LayoutGrid t.o.v. de temperatuur
                LayoutGrid.Background = new SolidColorBrush(Windows.UI.Colors.LightGreen);
            }
            else if (temperature < 30 && temperature > 0)
            {
                //Verandert de achtergrond kleur van LayoutGrid t.o.v. de temperatuur
                LayoutGrid.Background = new SolidColorBrush(Windows.UI.Colors.LightBlue);
            }
        }

        private void GoToMainPage_Click(object sender, RoutedEventArgs e)
        {
            //Zorgt ervoor dat als de gebruiker op deze knop drukt hij/zij terug gaat naar de beginpagina.
            this.Frame.Navigate(typeof(MainPage), null);
        }
    }
}
