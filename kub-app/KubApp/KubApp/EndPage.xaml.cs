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

namespace MindGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EndPage : Page
    {
        public EndPage()
        {
            this.InitializeComponent();
        }

        private void Mainbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(KubApp.MindGameMain));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var score = e.Parameter as string;
            ScoreEndPage.Text = score.ToString();
        }
    }
}
