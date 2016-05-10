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
    public sealed partial class LedPage : Page
    {
        public LedPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Verandert kleur obv user input
        /// </summary>
        private void Stemming_Click(object sender, RoutedEventArgs e)
        {
            currentColor.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 175, 27, 27));
        }

        /// <summary>
        /// Linkt naar main page
        /// </summary>
        private void LinkMainPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);
        }
    }
}
