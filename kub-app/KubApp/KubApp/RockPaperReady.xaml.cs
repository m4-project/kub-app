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
    public sealed partial class RockPaperReady : Page
    {
        public RockPaperReady()
        {
            this.InitializeComponent();
        }

        private void PVCStart_Click(object sender, RoutedEventArgs e)
        {
            // start game versus computer
            this.Frame.Navigate(typeof(RockPaperPVC));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // navigate back to main page of game
            this.Frame.Navigate(typeof(RockPaperMain));
        }
    }
}
