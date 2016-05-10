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
        byte temperature;

        public TemperaturePage()
        {
            this.InitializeComponent();
            Temperature();
        }

        public void Temperature()
        {
            temperature = 45;

            TemperatureKub.Text = "Temperature Kub = " + temperature + " °C";

            if (temperature >= 60)
            {
                LayoutGrid.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else if (temperature < 60 && temperature > 30)
            {
                LayoutGrid.Background = new SolidColorBrush(Windows.UI.Colors.Green);
            }
            else if (temperature < 30 && temperature > 0)
            {
                LayoutGrid.Background = new SolidColorBrush(Windows.UI.Colors.Blue);
            }
        }

        private void GoToMainPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);
        }
    }
}
