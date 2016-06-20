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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KubApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorMatchMenu : Page
    {
        public ColorMatchMenu()
        {
            this.InitializeComponent();
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            // start game
            this.Frame.Navigate(typeof(ColorMatchGetReady));
        }

        private void howtoplay_Click(object sender, RoutedEventArgs e)
        {
            // go to how to page
            this.Frame.Navigate(typeof(ColorMatchHow));
        }

        private void quitGame_Click(object sender, RoutedEventArgs e)
        {
            // quit and go to main kub ap page
            this.Frame.Navigate(typeof(MainGameMain));
        }
    }
}
