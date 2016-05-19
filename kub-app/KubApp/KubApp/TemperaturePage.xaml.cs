using KubApp_v0._1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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
        private Kub kub;
        private Timer timer;

        public TemperaturePage()
        {
            this.InitializeComponent();     
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.kub = (Kub)e.Parameter;

            Temperature();
            setTemperatureKubStatus();
        }

        public void Temperature()
        {
            this.kub.RequestData(Kub.DataType.Temperature, delegate (int value)
            {
                System.Diagnostics.Debug.WriteLine("Temperature: " + value);

                //Zet de text van de textblock naar "Temperature Kub = " + value + " °C"
                TemperatureKub.Text = "Kub " + value + " °C";

                //Kijkt naar de temperatuur van de Kub en bepaald daarmee de kleur van de achtergrond.
                if (value >= 60)
                {
                    //Verandert de achtergrond kleur van LayoutGrid t.o.v. de temperatuur
                    LayoutGrid.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                }
                else if (value < 60 && value > 30)
                {
                    //Verandert de achtergrond kleur van LayoutGrid t.o.v. de temperatuur
                    LayoutGrid.Background = new SolidColorBrush(Windows.UI.Colors.LightGreen);
                }
                else if (value < 30 && value > 15)
                {
                    //Verandert de achtergrond kleur van LayoutGrid t.o.v. de temperatuur
                    LayoutGrid.Background = new SolidColorBrush(Windows.UI.Colors.LightBlue);
                }
                else if (value < 15 && value > 0)
                {
                    //Verandert de achtergrond kleur van LayoutGrid t.o.v. de temperatuur
                    LayoutGrid.Background = new SolidColorBrush(Windows.UI.Colors.White);
                }
            });
        }

        public void setTemperatureKubStatus()
        {
            //zet de kubs status op connected
            TemperatureKubStatus.Text = "Kub: Connected";
        }

        private void GoToMainPage_Click(object sender, RoutedEventArgs e)
        {
            //Zorgt ervoor dat als de gebruiker op deze knop drukt hij/zij terug gaat naar de beginpagina.
            this.Frame.Navigate(typeof(MainPage), null);
        }

        private void toLedPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LedPage), null);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
