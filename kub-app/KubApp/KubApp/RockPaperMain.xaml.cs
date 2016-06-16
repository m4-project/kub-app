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
    public sealed partial class RockPaperMain : Page
    {
        public RockPaperMain()
        {
            this.InitializeComponent();
        }

        private void PVC_Click(object sender, RoutedEventArgs e)
        {
            // start game player versus computer
            this.Frame.Navigate(typeof(RockPaperReady));
        }


        private void exit_Click(object sender, object e)
        {
            // navigate back to mainpage
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
